using HZY.Domain.Services.Accounts;
using HZY.EFCore.PagingViews;
using HZY.Infrastructure;
using HZY.Models.Entities;
using HZY.Services.Admin.Core;
using HZY.EFCore.Repositories.Admin.Core;
using HZY.Infrastructure.ApiResultManage;
using Quartz;
using HZY.Models.BO;
using HZY.Domain.Services.WxBot;
using HZY.Domain.Services.QuartzWxBot;
using HZY.Domain.Services.QuartzWxBot.Model;
using HZY.Domain.Services.QuartzWxBot.Jobs;
using HZY.Models.Enums;
using Masuit.Tools.Reflection;
using HZY.Models.Consts;

namespace HZY.Services.Admin
{
    /// <summary>
    /// 微信机器人定时任务 服务 WxTimedTaskService
    /// </summary>
    public class WxTimedTaskService : AdminBaseService<IAdminRepository<WxTimedTask>>
    {
        private readonly ContentSendService _contentSendService;
        private readonly IAdminRepository<WxBotConfig> _wxBotConfigRepository;
        private readonly AccountInfo _accountInfo;
        private readonly IWxBotQuartzJobService _quartzJobService;
        public WxTimedTaskService(IAdminRepository<WxTimedTask> defaultRepository,
            IAdminRepository<WxBotConfig> wxBotConfigRepository,
           IAccountDomainService accountService,
           ContentSendService contentSendService,
          IWxBotQuartzJobService quartzJobService)
            : base(defaultRepository)
        {
            _wxBotConfigRepository = wxBotConfigRepository;
            _accountInfo = accountService.GetAccountInfo();
            _contentSendService = contentSendService;
            _quartzJobService = quartzJobService;
        }

        /// <summary>
        /// 获取列表数据
        /// </summary>
        /// <param name="page">page</param>
        /// <param name="size">size</param>
        /// <param name="search">search</param>
        /// <returns></returns>
        public async Task<PagingView> FindListAsync(int page, int size, WxTimedTask search)
        {
            //var query1 = (from task in this._defaultRepository.Select.OrderByDescending(w => w.CreationTime)
            //              from contact in this._wxContactService._defaultRepository.Select.Where(w => w.Id == task.).DefaultIfEmpty()
            //              select new { t1 = member, t2 = user }
            //    ).qu;

            var query = this._defaultRepository.Select
                    .OrderByDescending(w => w.CreationTime)
                     .Where(w => w.ApplicationToken.Equals(_accountInfo.Id.ToStr()))
                    .Select(w => new
                    {
                        w.Id,
                        w.ApplicationToken,
                        w.ReceivingObjectWxId,
                        w.ReceivingObjectName,
                        w.SendType,
                        SendTypeText = w.SendType.ToDescriptionOrString(),
                        w.SendContent,
                        w.SendTime,
                        w.ClosingRemarks,
                        w.TaskState,
                        TaskStateText= w.TaskState.GetDescription(),
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
                await StopTimdTaskAsync(id);
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
        public Task<WxTimedTask> SaveFormAsync(WxTimedTask form)
        {
            if (!CronExpression.IsValidExpression(form.SendTime)) MessageBox.Show("cron表达式不合法,请重新生成");
            return this._defaultRepository.InsertOrUpdateAsync(form);
        }

        /// <summary>
        /// 获取定时任务发送内容
        /// </summary>
        /// <param name="applicationToken">应用token</param>
        /// <param name="taskId">定时任务id</param>
        /// <returns></returns>
        public async Task<string> GetTaskSendContentAsync(string applicationToken, Guid taskId)
        {
            WxTimedTask wxTimedTask = await this._defaultRepository.FindByIdAsync(taskId);
            WxBotConfig wxBotConfig = await _wxBotConfigRepository.FindAsync(w => w.ApplicationToken == applicationToken);
            return await _contentSendService.GetTimedTaskContentAsync(wxTimedTask, wxBotConfig);

        }
        /// <summary>
        /// 启动定时任务
        /// </summary>
        /// <param name="timedTaskId"></param>
        /// <returns></returns>

        public async Task<bool> StartTimdTaskAsync(Guid timedTaskId)
        {
            WxTimedTask wxTimedTask = await this._defaultRepository.FindByIdAsync(timedTaskId);
            var jobSchedule = new WxBotJobSchedule
            {
                JobData = wxTimedTask.Id.ToString(),
                Cron = wxTimedTask.SendTime,
                GroupName = "定时任务",
                JobType = typeof(TimedTaskJob),
                TaskName = $"定时任务-{wxTimedTask.Id}"

            };
            await _quartzJobService.RunAsync(jobSchedule);
            //修改任务为运行中
            wxTimedTask.TaskState = ETaskState.RUNNING;
            await _defaultRepository.UpdateByIdAsync(wxTimedTask);
            return true;
        }
        /// <summary>
        /// 停止定时任务
        /// </summary>
        /// <param name="timedTaskId"></param>
        /// <returns></returns>
        public async Task<bool> StopTimdTaskAsync(Guid timedTaskId)
        {
            WxTimedTask wxTimedTask = await this._defaultRepository.FindByIdAsync(timedTaskId);
            var jobSchedule = new WxBotJobSchedule
            {
                JobData = wxTimedTask.Id.ToString(),
                Cron = wxTimedTask.SendTime,
                GroupName = "定时任务",
                JobType = typeof(TimedTaskJob),
                TaskName = $"定时任务-{wxTimedTask.Id}"

            };
            await _quartzJobService.CloseAsync(jobSchedule);
            //修改任务为已停止
            wxTimedTask.TaskState = ETaskState.STOP;
            await _defaultRepository.UpdateByIdAsync(wxTimedTask);
            return true;
        }
        /// <summary>
        /// 查询定时任务运行日志
        /// </summary>
        /// <param name="timedTaskId"></param>
        /// <returns></returns>
        public async Task<List<string>> QueryRunLogAsync(Guid timedTaskId) {
          return  (await RedisHelper.LRangeAsync(string.Format(CacheKeyConsts.JobTimedTaskExecLogKey, timedTaskId), 0, 50))?.ToList();
        }



    }
}