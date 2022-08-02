using HZY.Domain.Services.WxBot;
using HZY.EFCore.Repositories.Admin.Core;
using HZY.Models.Consts;
using HzyScanDiService;
using Masuit.Tools.DateTimeExt;
using Microsoft.Extensions.Logging;
using Quartz;
using StackExchange.Redis;
using System.Diagnostics;

namespace HZY.Domain.Services.QuartzWxBot.Jobs
{
    /// <summary>
    /// 定时任务 job
    /// </summary>
    [DisallowConcurrentExecution]
    public class TimedTaskJob : IJob, ITransientSelfDependency
    {
        private readonly ILogger<TimedTaskJob> _logger;
        private readonly Stopwatch _stopwatch;
        public TimedTaskJob(ILogger<TimedTaskJob> logger)
        {
            _logger = logger;
            _stopwatch = new Stopwatch();
        }
        public async Task Execute(IJobExecutionContext context)
        {
            //日志文本
            string logText = "";
            var timedTaskId = "";
            try
            {
                using var scope = IOCUtil.CreateScope();
                var _contentSendService = scope.ServiceProvider.GetService(typeof(ContentSendService)) as ContentSendService;
                _stopwatch.Restart();
                //取job传进来的定时任务的id
                timedTaskId = context.MergedJobDataMap.GetString("JobData");
                logText += $"定时任务({timedTaskId})开始,开始时间{DateTime.Now:yyy-MM-dd HH:mm:ss}\r\n";
                //执行
                await Task.Delay(1000);
                logText += await _contentSendService.ExecTimedTaskAsync(Guid.Parse(timedTaskId));
                logText += $"耗时{_stopwatch.ElapsedMilliseconds} 毫秒|结果=成功\r\n";
            }
            catch (Exception ex)
            {
                logText += $@"发生错误:Message={ex.Message}\r\n
                                StackTrace={ex.StackTrace}\r\n
                                Source={ex.Source}\r\n";
            }
            finally
            {
                _stopwatch.Stop();
                logText += $"定时任务({timedTaskId})结束,结束时间{DateTime.Now:yyy-MM-dd HH:mm:ss}\r\n";
                _logger.LogInformation(logText);
                //日志写入redis
                await RedisHelper.LPushAsync(string.Format(CacheKeyConsts.JobTimedTaskExecLogKey, timedTaskId), logText);

            }
        }
    }
}
