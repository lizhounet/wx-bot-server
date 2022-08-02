using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HZY.Models.Consts
{
    public class CacheKeyConsts
    {
        public const string WxUserInfoKey = "WxUserInfo:{0}";
        /// <summary>
        /// 个微小助手基础配置 缓存key
        /// </summary>
        public const string WxBotConfigKey = "WxBotConfig:{0}";
        /// <summary>
        /// 邮箱验证缓存key
        /// </summary>
        public const string EmailVerifyCodeCacheKey = "EmailVerifyCode:{0}";
        /// <summary>
        /// Job定时任务日志key
        /// </summary>
        public const string JobTimedTaskExecLogKey = "WxBot:Job:TimedTaskExecLog:{0}";
        /// <summary>
        /// Job情侣每日说日志key
        /// </summary>
        public const string JobSayEveryDayLogKey = "WxBot:Job:SayEveryDayLog:{0}";
    }
}
