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
    }
}
