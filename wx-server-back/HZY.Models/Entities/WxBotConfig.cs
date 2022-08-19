using System;
using System.ComponentModel.DataAnnotations;
using HZY.Models.Entities.BaseEntitys;
using HZY.Models.Enums;

namespace HZY.Models.Entities
{
    /// <summary>
    /// 个微小助手基础配置
    /// </summary>
    public class WxBotConfig : DefaultBaseEntity<Guid>
    {

        /// <summary>
        ///  应用Token => 备注: 应用Token
        /// </summary>
        public String ApplicationToken { get; set; }
        /// <summary>
        ///  机器人微信id => 备注: 机器人微信id
        /// </summary>
        public String RobotWxId { get; set; }

        /// <summary>
        ///  群聊自动回复是否开启 => 备注: 群聊自动回复是否开启
        /// </summary>
        public Int32 GroupAutoReplyFlag { get; set; } = 1;


        /// <summary>
        ///  私聊自动回复是否开启 => 备注: 私聊自动回复是否开启
        /// </summary>
        public Int32 TalkPrivateAutoReplyFlag { get; set; } = 0;


        /// <summary>
        ///  回复机器人类型(1=天行机器人,2=腾讯闲聊机器人) => 备注: 回复机器人类型(1=天行机器人,2=腾讯闲聊机器人)
        /// </summary>
        public EWxBotType ReplyBotType { get; set; } = EWxBotType.TIANXING;

        /// <summary>
        ///  天行机器人key => 备注: 天行机器人key
        /// </summary>
        public String TianXingApiKey { get; set; }

        /// <summary>
        ///  vlw,我的框架的http调用地址 => 备注: vlw,我的框架的http调用地址
        /// </summary>
        public String VlwHttpUrl { get; set; }

        /// <summary>
        ///  自动同意好友请求是否开启 => 备注: 自动同意好友请求是否开启
        /// </summary>
        public Int32 AutoAgreeFriendVerify { get; set; } = 0;
        /// <summary>
        ///  自动同意好友请求验证规则 => 备注: 自动同意好友请求验证规则
        /// </summary>
        public string AutoAgreeFriendVerifRrule { set; get; }

    }
}