using HZY.Domain.Services.WxBot;
using HZY.Models.Consts;
using HzyScanDiService;
using Microsoft.Extensions.Logging;
using Quartz;
using System.Diagnostics;

namespace HZY.Domain.Services.QuartzWxBot.Jobs
{
    /// <summary>
    /// 情侣每日说 job
    /// </summary>
    [DisallowConcurrentExecution]
    public class SayEveryDayJob : IJob, ITransientSelfDependency
    {
        private readonly ILogger<SayEveryDayJob> _logger;
        private readonly Stopwatch _stopwatch;
        public SayEveryDayJob(ILogger<SayEveryDayJob> logger)
        {
            _logger = logger;
            _stopwatch = new Stopwatch();
        }
        public async Task Execute(IJobExecutionContext context)
        {
            //日志文本
            string logText = "";
            var everyDayId = "";
            try
            {
                using var scope = IOCUtil.CreateScope();
                var _contentSendService = scope.ServiceProvider.GetService(typeof(ContentSendService)) as ContentSendService;
                _stopwatch.Restart();
                //取job传进来的定时任务的id
                everyDayId = context.MergedJobDataMap.GetString("JobData");
                logText += $"\n情侣每日说({everyDayId})开始执行,开始时间{DateTime.Now:yyy-MM-dd HH:mm:ss}";
                //执行
                await Task.Delay(1000);
                logText += await _contentSendService.ExecSayEveryDayAsync(Guid.Parse(everyDayId));
                logText += $"\n耗时{_stopwatch.ElapsedMilliseconds} 毫秒|结果=成功\r\n";
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
                logText += $"\n情侣每日说({everyDayId})结束执行,结束时间{DateTime.Now:yyy-MM-dd HH:mm:ss}";
                _logger.LogInformation(logText);
                //日志写入redis
                await RedisHelper.LPushAsync(string.Format(CacheKeyConsts.JobSayEveryDayLogKey, everyDayId), logText);
            }
        }
    }
}
