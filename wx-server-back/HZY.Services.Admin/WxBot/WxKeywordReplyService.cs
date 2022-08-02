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

namespace HZY.Services.Admin
{
    /// <summary>
    /// 关键词回复表 服务 WxKeywordReplyService
    /// </summary>
    public class WxKeywordReplyService : AdminBaseService<IAdminRepository<WxKeywordReply>>
    {
        private readonly ContentSendService _contentSendService;
        private readonly TianXingService _tianXingService;
        private readonly IAdminRepository<WxBotConfig> _wxBotConfigRepository;
        private readonly AccountInfo _accountInfo;
        public WxKeywordReplyService(IAdminRepository<WxKeywordReply> defaultRepository,
            TianXingService tianXingService,
              IAdminRepository<WxBotConfig> wxBotConfigRepository,
              IAccountDomainService accountService,
              ContentSendService contentSendService)
            : base(defaultRepository)
        {
            _tianXingService = tianXingService;
            _wxBotConfigRepository = wxBotConfigRepository;
            _accountInfo = accountService.GetAccountInfo();
            _contentSendService = contentSendService;
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
                        w.KeyWord,
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

            res[nameof(id)] = id == Guid.Empty ? "" : id;
            res[nameof(form)] = form;
            return res;
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="form">form</param>
        /// <returns></returns>
        public Task<WxKeywordReply> SaveFormAsync(WxKeywordReply form)
        {
            return this._defaultRepository.InsertOrUpdateAsync(form);
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
        /// <summary>
        /// 关键词回复
        /// </summary>
        /// <param name="applicationToken">applicationToken</param>
        /// <param name="keyword">关键字</param>
        /// <returns></returns>
        public async Task<string> KeywordReply(string applicationToken, string keyword)
        {
            WxBotConfig wxBotConfig = await _wxBotConfigRepository.FindAsync(w => w.ApplicationToken == applicationToken);
            List<WxKeywordReply> keywordReplys = this._defaultRepository.Select.Where(w => w.ApplicationToken == applicationToken)
                .Where(w => w.KeyWord.Contains(keyword)).ToList();
            if (keywordReplys != null && keywordReplys.Count > 0)
            {
                //精确匹配优先级高于模糊匹配
                WxKeywordReply jqReply = keywordReplys.FirstOrDefault(w => w.MatchType == EMatchType.JINGQUE
                && w.KeyWord.Split(",").Contains(keyword));
                if (jqReply != null)
                {
                    return await _contentSendService.GetkeyWorldContentAsync(jqReply, wxBotConfig);
                }
                else
                {
                    WxKeywordReply mhReply = keywordReplys.FirstOrDefault(w => w.MatchType == EMatchType.MOHU);
                    return await _contentSendService.GetkeyWorldContentAsync(mhReply, wxBotConfig);
                }
            }
            return null;

        }
    }
}