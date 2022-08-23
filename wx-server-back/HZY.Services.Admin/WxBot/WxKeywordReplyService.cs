using HZY.Domain.Services.Accounts;
using HZY.EFCore.PagingViews;
using HZY.Infrastructure;
using HZY.Models.Entities;
using HZY.Services.Admin.Core;
using HZY.EFCore.Repositories.Admin.Core;
using HZY.Models.Enums;
using HZY.Models.BO;
using HZY.Domain.Services.WxBot;
using HZY.Domain.Services.WxBot.Http;
using HZY.Models.DTO.WxBot;
using System.Linq;

namespace HZY.Services.Admin
{
    /// <summary>
    /// 关键词回复表 服务 WxKeywordReplyService
    /// </summary>
    public class WxKeywordReplyService : AdminBaseService<IAdminRepository<WxKeywordReply>>
    {
        private readonly IAdminRepository<WxKeywordItem> _wxKeywordItemRepository;
        private readonly AccountInfo _accountInfo;
        public WxKeywordReplyService(IAdminRepository<WxKeywordReply> defaultRepository,
              IAdminRepository<WxKeywordItem> wxKeywordItemRepository,
              IAccountDomainService accountService)
            : base(defaultRepository)
        {
            _wxKeywordItemRepository = wxKeywordItemRepository;
            _accountInfo = accountService.GetAccountInfo();
        }

        /// <summary>
        /// 获取列表数据
        /// </summary>
        /// <param name="page">page</param>
        /// <param name="size">size</param>
        /// <param name="search">search</param>
        /// <returns></returns>
        public async Task<PagingView> FindListAsync(int page, int size, WxKeywordReply search)
        {
            var query = this._defaultRepository.Select
                    .OrderByDescending(w => w.CreationTime)
                    .Where(w => w.ApplicationToken.Equals(_accountInfo.Id.ToStr()))
                    .Select(w => new
                    {
                        w.Id,
                        w.ApplicationToken,
                        w.SendType,
                        SendTypeText = w.SendType.ToDescriptionOrString(),
                        w.SendContent,
                        w.TakeEffectType,
                        KeyWord = string.Join(",", _wxKeywordItemRepository.Select.Where(r => r.KeyWordReplyId == w.Id).Select(r => r.KeyWords).ToList()),
                        w.MatchType,
                        MatchTypeText = w.MatchType.ToDescriptionOrString(),
                        LastModificationTime = w.LastModificationTime.ToString("yyyy-MM-dd"),
                        CreationTime = w.CreationTime.ToString("yyyy-MM-dd")
                    })
                ;

            var result = await this._defaultRepository.AsPagingViewAsync(query, page, size);
            // result.Column(query, w => w.OrganizationName).Mapping<SysOrganization>(w => w.Name);
            // result.Column(query, w => w.OrganizationName).Mapping(title:"所属组织");
            return result;
        }

        /// <summary>
        /// 根据id数组删除
        /// </summary>
        /// <param name="ids">ids</param>
        /// <returns></returns>
        public async Task DeleteListAsync(List<Guid> ids)
        {
            await this._defaultRepository.DeleteByIdsAsync(ids);
        }

        /// <summary>
        /// 查询表单数据
        /// </summary>
        /// <param name="id">id</param>
        /// <returns></returns>
        public async Task<Dictionary<string, object>> FindFormAsync(Guid id)
        {
            var res = new Dictionary<string, object>();
            var form = await this._defaultRepository.FindByIdAsync(id);
            form = form.NullSafe();
            List<WxKeywordItem> wxKeywordItems = _wxKeywordItemRepository.ToList(w => w.KeyWordReplyId == form.Id);

            res[nameof(id)] = id == Guid.Empty ? "" : id;
            res[nameof(form)] = form;
            res[nameof(wxKeywordItems)] = wxKeywordItems;
            return res;
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="form">form</param>
        /// <returns></returns>
        public async Task<WxKeywordReply> SaveFormAsync(WxKeywordReply form)
        {
            var keyWords = form.KeyWord.Split(",").ToList();
            WxKeywordReply keywordReply = await this._defaultRepository.InsertOrUpdateAsync(form);
            //删除对应关键词
            await _wxKeywordItemRepository.DeleteAsync(w => w.KeyWordReplyId == keywordReply.Id);
            //添加关键词
            if (keyWords.Count > 0)
            {
                List<WxKeywordItem> wxKeywordItems = new List<WxKeywordItem>();
                keyWords.ForEach(w =>
                        {
                            wxKeywordItems.Add(new WxKeywordItem
                            {
                                KeyWordReplyId = keywordReply.Id,
                                KeyWords = w,
                                ApplicationToken= keywordReply.ApplicationToken,
                                MatchType=keywordReply.MatchType
                            });
                        });
                await _wxKeywordItemRepository.InsertRangeAsync(wxKeywordItems);
            }
            return keywordReply;
        }

        /// <summary>
        /// 导出Excel
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public async Task<byte[]> ExportExcelAsync(WxKeywordReply search)
        {
            var tableViewModel = await this.FindListAsync(0, 0, search);
            return this.ExportExcelByPagingView(tableViewModel, null, "Id");
        }
    }
}