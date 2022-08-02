using HZY.Domain.Services.QuartzWxBot.Model;
using HzyScanDiService;


namespace HZY.Domain.Services.QuartzWxBot
{
    /// <summary>
    /// 微信机器人Quartz服务
    /// </summary>
    public interface IWxBotQuartzJobService : IScopedDependency
    {
        /// <summary>
        /// 开始运行一个任务调度器
        /// </summary>
        /// <returns></returns>
        Task RunAsync(WxBotJobSchedule jobSchedule);

        /// <summary>
        /// 关闭任务调度
        /// </summary>
        /// <returns></returns>
        Task CloseAsync(WxBotJobSchedule jobSchedule);
    }
}
