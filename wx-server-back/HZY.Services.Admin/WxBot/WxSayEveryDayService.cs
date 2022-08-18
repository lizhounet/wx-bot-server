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
using Quartz;
using HZY.Infrastructure.ApiResultManage;
using HZY.Models.BO;
using HZY.Domain.Services.WxBot.Http;
using HZY.Domain.Services.WxBot;
using HZY.Domain.Services.QuartzWxBot.Model;
using HZY.Domain.Services.QuartzWxBot.Jobs;
using HZY.Domain.Services.QuartzWxBot;
using HZY.Models.Enums;
using Masuit.Tools.Reflection;
using HZY.Models.Consts;

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
        private readonly ContentSendService _contentSendService;
        private readonly IWxBotQuartzJobService _quartzJobService;
        public WxSayEveryDayService(IAdminRepository<WxSayEveryDay> defaultRepository,
            TianXingService tianXingService,
            IAdminRepository<WxBotConfig> wxBotConfigRepository,
            IAccountDomainService accountService,
            ContentSendService contentSendService,
           IWxBotQuartzJobService quartzJobService)
            : base(defaultRepository)
        {
            _tianXingService = tianXingService;
            _wxBotConfigRepository = wxBotConfigRepository;
            _contentSendService = contentSendService;
            _accountInfo = accountService.GetAccountInfo();
            _quartzJobService = quartzJobService;
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
                        BirthdayDate = w.BirthdayDate.ToString("yyyy-MM-dd"),
                        w.TaskState,
                        TaskStateText = w.TaskState.GetDescription(),
                        w.RobotWxId,
                        w.LastModificationTime,
                        w.CreationTime
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
            foreach (var id in ids)
                await StopSayEveryDayAsync(id);
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
        public async Task<string> GetSayEveryDayTextAsync(string applicationToken, Guid everyDayId)
        {
            //获取机器人
            WxBotConfig wxBotConfig = await _wxBotConfigRepository.FindAsync(w => w.ApplicationToken == applicationToken);
            WxSayEveryDay wxSayEveryDay = await this._defaultRepository.FindByIdAsync(everyDayId);
            return await _contentSendService.GetSayEveryDayTextAsync(wxSayEveryDay, wxBotConfig);
        }

        /// <summary>
        /// 启动定时任务
        /// </summary>
        /// <param name="everyDayId"></param>
        /// <returns></returns>

        public async Task<bool> StartSayEveryDayAsync(Guid everyDayId)
        {
            WxSayEveryDay sayEveryDay = await this._defaultRepository.FindByIdAsync(everyDayId);
            var jobSchedule = new WxBotJobSchedule
            {
                JobData = sayEveryDay.Id.ToString(),
                Cron = sayEveryDay.SendTime,
                GroupName = "情侣每日说",
                JobType = typeof(SayEveryDayJob),
                TaskName = $"情侣每日说-{sayEveryDay.Id}"

            };
            await _quartzJobService.RunAsync(jobSchedule);
            //修改任务为运行中
            sayEveryDay.TaskState = ETaskState.RUNNING;
            await _defaultRepository.UpdateByIdAsync(sayEveryDay);
            return true;
        }
        /// <summary>
        /// 停止情侣每日说
        /// </summary>
        /// <param name="everyDayId"></param>
        /// <returns></returns>
        public async Task<bool> StopSayEveryDayAsync(Guid everyDayId)
        {
            WxSayEveryDay sayEveryDay = await this._defaultRepository.FindByIdAsync(everyDayId);
            var jobSchedule = new WxBotJobSchedule
            {
                JobData = sayEveryDay.Id.ToString(),
                Cron = sayEveryDay.SendTime,
                GroupName = "情侣每日说",
                JobType = typeof(SayEveryDayJob),
                TaskName = $"情侣每日说-{sayEveryDay.Id}"

            };
            await _quartzJobService.CloseAsync(jobSchedule);
            //修改任务为已停止
            sayEveryDay.TaskState = ETaskState.STOP;
            await _defaultRepository.UpdateByIdAsync(sayEveryDay);
            return true;
        }
        /// <summary>
        /// 查询情侣每日说运行日志
        /// </summary>
        /// <param name="everyDayId"></param>
        /// <returns></returns>
        public async Task<List<string>> QueryRunLogAsync(Guid everyDayId)
        {
            return (await RedisHelper.LRangeAsync(string.Format(CacheKeyConsts.JobSayEveryDayLogKey, everyDayId), 0, 50))?.ToList();
        }
    }
}