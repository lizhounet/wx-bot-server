using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;

using HzyEFCoreRepositories.Extensions;

using HZY.Domain.Services.Accounts;
using HZY.Domain.Services.Upload;
using HZY.EFCore.PagingViews;
using HZY.Infrastructure;
using HZY.Models.DTO;
using HZY.Models.Entities;
using HZY.Services.Admin.Core;
using HZY.EFCore.Repositories.Admin.Core;
using HZY.Services.Admin.WxBot.Http;
using Quartz;
using HZY.Infrastructure.ApiResultManage;
using HZY.Models.BO;
using HZY.Models.VO;
using HZY.Models.Enums;

namespace HZY.Services.Admin
{
    /// <summary>
    /// 情侣每日说 服务 WxSayEveryDayService
    /// </summary>
    public class WxSayEveryDayService : AdminBaseService<IAdminRepository<WxSayEveryDay>>
    {
        private readonly TianXingService _tianXingService;
        private readonly IAdminRepository<WxBotConfig> _wxBotConfigRepository;
        private readonly AccountInfo _accountInfo;
        public WxSayEveryDayService(IAdminRepository<WxSayEveryDay> defaultRepository,
            TianXingService tianXingService,
            IAdminRepository<WxBotConfig> wxBotConfigRepository,
            IAccountDomainService accountService)
            : base(defaultRepository)
        {
            _tianXingService = tianXingService;
            _wxBotConfigRepository = wxBotConfigRepository;
            _accountInfo = accountService.GetAccountInfo();
        }

        /// <summary>
        /// 获取列表数据
        /// </summary>
        /// <param name="page">page</param>
        /// <param name="size">size</param>
        /// <param name="search">search</param>
        /// <returns></returns>
        public async Task<PagingView> FindListAsync(int page, int size, WxSayEveryDay search)
        {
            var query = this._defaultRepository.Select
                    .OrderByDescending(w => w.CreationTime)
                     .Where(w => w.ApplicationToken.Equals(_accountInfo.Id.ToStr()))
                    .Select(w => new
                    {
                        w.Id,
                        w.ApplicationToken,
                        w.ReceivingObjectWxId,
                        w.ReceivingObjectName,
                        w.SendTime,
                        w.City,
                        w.ClosingRemarks,
                        AnniversaryDay = w.AnniversaryDay.ToString("yyyy-MM-dd"),
                        LastModificationTime = w.LastModificationTime.ToString("yyyy-MM-dd"),
                        CreationTime = w.CreationTime.ToString("yyyy-MM-dd"),
                        w.BirthdayDate
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
            var receivingObjectWxIds = form.ReceivingObjectWxId?.Split(",");
            var receivingObjectNames = form.ReceivingObjectName?.Split(",");
            res[nameof(id)] = id == Guid.Empty ? "" : id;
            res[nameof(form)] = form;
            res["receivingObjects"] = receivingObjectWxIds?.Select((t, index) => new
            {
                value = t,
                label = receivingObjectNames?[index]
            });
            return res;
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="form">form</param>
        /// <returns></returns>
        public Task<WxSayEveryDay> SaveFormAsync(WxSayEveryDay form)
        {
            if (!CronExpression.IsValidExpression(form.SendTime)) MessageBox.Show("cron表达式不合法,请重新生成");
            return this._defaultRepository.InsertOrUpdateAsync(form);
        }

        /// <summary>
        /// 导出Excel
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public async Task<byte[]> ExportExcelAsync(WxSayEveryDay search)
        {
            var tableViewModel = await this.FindListAsync(0, 0, search);
            return this.ExportExcelByPagingView(tableViewModel, null, "Id");
        }
        /// <summary>
        /// 获取每日说文本
        /// </summary>
        /// <param name="applicationToken">应用token</param>
        /// <param name="everyDayId">每日说id</param>
        /// <returns></returns>
        public async Task<MessageVO> GetSayEveryDayTextAsync(string applicationToken, Guid everyDayId)
        {
            var message = new MessageVO
            {
                Result = "",
                IsAnalyze = false,
                MessageType = EMessageType.TEXT
            };
            //获取机器人
            WxBotConfig wxBotConfig = await _wxBotConfigRepository.FindAsync(w => w.ApplicationToken == applicationToken);
            WxSayEveryDay sayEveryDay = await this._defaultRepository.FindByIdAsync(everyDayId);
            if (sayEveryDay == null) return message;
            //获取天气
            string weather = await _tianXingService.GetWeatherAsync(wxBotConfig.TianXingApiKey, sayEveryDay.City);
            //获取每日一句
            string dayOne = await _tianXingService.GetDayOneAsync(wxBotConfig.TianXingApiKey);
            //获取情话
            string loveWords = await _tianXingService.GetLoveWordsAsync(wxBotConfig.TianXingApiKey);
            //计算在一起多少天
            int days = (DateTime.Now.Date - sayEveryDay.AnniversaryDay.Date).Days;
            //计算下次周年日期
            int anniversary = (DateTime.Now.Date.Year - sayEveryDay.AnniversaryDay.Date.Year);
            anniversary = anniversary == 0 ? 1 : anniversary;
            if (sayEveryDay.AnniversaryDay.Date.AddYears(anniversary)
                < DateTime.Now.Date) anniversary++;
            //计算下次周年还有多少天
            int anniversaryDays = (sayEveryDay.AnniversaryDay.Date.AddYears(anniversary) - DateTime.Now.Date).Days;
            //计算生日还有多少天
            DateTime birthdayDate = sayEveryDay.BirthdayDate?.Date ?? DateTime.Now;
            int birthdays = (birthdayDate - DateTime.Now.Date).Days;
            message.Result = $"😘{DateTime.Now:yyyy-MM-dd HH:mm} {Tools.GetWeekByDate(DateTime.Now)}\n" +
               $"\n🤗元气满满的一天开始啦,要开心噢^_^" +
               $"\n👫宝贝,今天是我们相恋的第{days}天" +
               $"\n👫距离你的生日还有{birthdays}天" +
               $"\n👫距离我们恋爱{anniversary}周年纪念日还有{anniversaryDays}天" +
               $"\n\n🌤天气情况:" +
               $"\n{weather}" +
               $"\n\n💪每日一句:" +
               $"\n{dayOne}" +
               $"\n\n💑情话对你说:" +
               $"\n{loveWords}" +
               $"\n————————{sayEveryDay.ClosingRemarks}";
            return message;
        }
    }
}