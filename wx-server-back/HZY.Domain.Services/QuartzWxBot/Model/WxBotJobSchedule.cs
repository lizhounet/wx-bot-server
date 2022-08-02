using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HZY.Domain.Services.QuartzWxBot.Model
{
    /// <summary>
    /// 微信机器人job任务描述实体
    /// </summary>
    public class WxBotJobSchedule
    {
        /// <summary>
        /// 任务需要的参数
        /// </summary>
        public string JobData { get; set; }
        /// <summary>
        /// 任务名称
        /// </summary>
        public string TaskName { get; set; }

        /// <summary>
        /// 分组名称
        /// </summary>
        public string GroupName { get; set; }

        /// <summary>
        /// 间隔表达式
        /// </summary>
        public string Cron { get; set; }
        /// <summary>
        /// job类型
        /// </summary>
        public Type JobType { get; set; }
    }
}
