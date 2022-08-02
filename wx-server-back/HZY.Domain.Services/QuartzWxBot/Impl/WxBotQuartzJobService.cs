using HZY.Domain.Services.Quartz.Jobs;
using HZY.Domain.Services.QuartzWxBot.Model;
using Quartz;
using Quartz.Impl.Matchers;
using Quartz.Impl.Triggers;

namespace HZY.Domain.Services.QuartzWxBot.Impl
{
    public class WxBotQuartzJobService: IWxBotQuartzJobService
    {
        private readonly ISchedulerFactory _schedulerFactory;
        private readonly ResultfulApiJobFactory _resultfulApiJobFactory;

        public WxBotQuartzJobService(ISchedulerFactory schedulerFactory, ResultfulApiJobFactory resultfulApiJobFactory)
        {
            _schedulerFactory = schedulerFactory;
            _resultfulApiJobFactory = resultfulApiJobFactory;
        }
        /// <summary>
        /// 开始运行一个调度器
        /// </summary>
        /// <param name="jobSchedule"></param>
        /// <returns></returns>
        public async Task RunAsync(WxBotJobSchedule jobSchedule)
        {
            //先关闭这个任务
            await CloseAsync(jobSchedule);
            //1、通过调度工厂获得调度器
            var scheduler = await _schedulerFactory.GetScheduler();
            //2、创建一个触发器
            var trigger = TriggerBuilder.Create()
                .WithIdentity(jobSchedule.TaskName, jobSchedule.GroupName)
                .StartNow()
                // 触发表达式 0 0 0 1 1 ?
                .WithCronSchedule(jobSchedule.Cron)
                .Build();

            //3、创建任务
            var jobDetail = JobBuilder.Create(jobSchedule.JobType)
                            .WithIdentity(jobSchedule.TaskName, jobSchedule.GroupName)
                            .UsingJobData("JobData", jobSchedule.JobData)
                            .Build();

            //4、写入 Job 实例工厂 解决 Job 中取 ioc 对象
            scheduler.JobFactory = _resultfulApiJobFactory;
            //5、将触发器和任务器绑定到调度器中
            await scheduler.ScheduleJob(jobDetail, trigger);
            //6、开启调度器
            await scheduler.Start();
        }

        /// <summary>
        /// 关闭调度器
        /// </summary>
        /// <param name="jobSchedule"></param>
        /// <returns></returns>
        public async Task CloseAsync(WxBotJobSchedule jobSchedule)
        {
            IScheduler scheduler = await _schedulerFactory.GetScheduler();
            var jobKeys = (await scheduler
                .GetJobKeys(GroupMatcher<JobKey>.GroupEquals(jobSchedule.GroupName)))
                .ToList();
            if (jobKeys == null || jobKeys.Count == 0)
            {
                return;
            }

            JobKey jobKey = jobKeys
            .FirstOrDefault(w => scheduler.GetTriggersOfJob(w).Result.Any(x => (x as CronTriggerImpl).Name == jobSchedule.TaskName));

            if (jobKey == null)
            {
                return;
            }
            //
            var triggers = await scheduler.GetTriggersOfJob(jobKey);
            ITrigger trigger = triggers?.Where(x => (x as CronTriggerImpl).Name == jobSchedule.TaskName).FirstOrDefault();

            if (trigger == null)
            {
                return;
            }
            await scheduler.PauseTrigger(trigger.Key);
            await scheduler.UnscheduleJob(trigger.Key);// 移除触发器
            await scheduler.DeleteJob(trigger.JobKey);
        }
    }
}
