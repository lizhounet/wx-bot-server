using HZY.Domain.Services.QuartzWxBot.Jobs;
using HZY.Domain.Services.QuartzWxBot.Model;
using HZY.EFCore.Repositories.Admin.Core;
using HZY.Models.Entities;
using HzyScanDiService;
using Microsoft.Extensions.Logging;

namespace HZY.Domain.Services.QuartzWxBot
{
    /// <summary>
    /// 微信机器人任务服务
    /// </summary>
    public class WxBotTaskService: IScopedSelfDependency
    {
        private readonly IAdminRepository<WxTimedTask> _wxTimedTaskRepository;
        private readonly IAdminRepository<WxSayEveryDay> _wxSayEveryDayRepository;
        private readonly ILogger<WxBotTaskService> _logger;
        private readonly IWxBotQuartzJobService _wxBotQuartzJobService;
        public WxBotTaskService(IAdminRepository<WxSayEveryDay> wxSayEveryDayRepository,
                IAdminRepository<WxTimedTask> wxTimedTaskRepository,
                ILogger<WxBotTaskService> logger,
                IWxBotQuartzJobService wxBotQuartzJobService)
        {
            _wxSayEveryDayRepository = wxSayEveryDayRepository;
            _wxTimedTaskRepository = wxTimedTaskRepository;
            _logger = logger;
            _wxBotQuartzJobService = wxBotQuartzJobService;
        }


        /// <summary>
        /// 恢复所有数据库状态正在运行的任务
        /// </summary>
        /// <returns></returns>
        public async Task<bool> RecoveryTaskAsync()
        {
            try
            {
                List<WxBotJobSchedule> jobSchedules = (await QueryRunningWxTimedTasks()).Select(w => new WxBotJobSchedule
                {
                    JobData = w.Id.ToString(),
                    Cron = w.SendTime,
                    GroupName = "定时任务",
                    JobType = typeof(TimedTaskJob),
                    TaskName = $"定时任务-{w.Id}"

                }).ToList();
                jobSchedules.AddRange((await QueryRunningWxSayEveryDays()).Select(w => new WxBotJobSchedule
                {
                    JobData = w.Id.ToString(),
                    Cron = w.SendTime,
                    GroupName = "情侣每日说",
                    JobType = typeof(SayEveryDayJob),
                    TaskName = $"情侣每日说-{w.Id}"

                }));
                foreach (var jobSchedule in jobSchedules)
                {
                  await  _wxBotQuartzJobService.RunAsync(jobSchedule);
                }
            }
            catch (Exception ex)
            {
               _logger.LogError(ex, $"自动开启任务错误 [{DateTime.Now}] : {ex.Message}");
            }

            return true;
        }
        /// <summary>
        /// 查询数据库正在运行的微信定时任务
        /// </summary>
        /// <returns></returns>
        private async Task<List<WxTimedTask>> QueryRunningWxTimedTasks() {
            List<WxTimedTask> wxTimedTasks = await _wxTimedTaskRepository.ToListAsync(w => w.TaskState == Models.Enums.ETaskState.RUNNING);
            return wxTimedTasks ?? new List<WxTimedTask>();
        }
        /// <summary>
        /// 查询数据库正在运行的情侣每日说
        /// </summary>
        /// <returns></returns>
        private async Task<List<WxSayEveryDay>> QueryRunningWxSayEveryDays()
        {
            List<WxSayEveryDay> wxSayEveryDays = await _wxSayEveryDayRepository.ToListAsync(w => w.TaskState == Models.Enums.ETaskState.RUNNING);
            return wxSayEveryDays ?? new List<WxSayEveryDay>();
        }

    }
}
