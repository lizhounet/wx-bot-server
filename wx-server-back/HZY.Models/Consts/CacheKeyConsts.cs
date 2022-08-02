using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HZY.Models.Consts
{
    public class CacheKeyConsts
    {
        /// <summary>
        /// 登录微信的信息 缓存key 参数0=用户id
        /// </summary>
        public const string OnlineWxUserInfoKey = "WxBot:WxUserInfo:{0}";
        /// <summary>
        /// 个微小助手基础配置 缓存key 参数0=用户id
        /// </summary>
        public const string WxBotConfigKey = "WxBot:WxBotConfig:{0}";
        /// <summary>
        /// 邮箱验证缓存key 参数0=邮箱地址
        /// </summary>
        public const string EmailVerifyCodeCacheKey = "WxBot:EmailVerifyCode:{0}";
        /// <summary>
        /// Job定时任务日志key 参数0=用户id
        /// </summary>
        public const string JobTimedTaskExecLogKey = "WxBot:Job:TimedTaskExecLog:{0}";
        /// <summary>
        /// Job情侣每日说日志key 参数0=用户id
        /// </summary>
        public const string JobSayEveryDayLogKey = "WxBot:Job:SayEveryDayLog:{0}";
    }
}
