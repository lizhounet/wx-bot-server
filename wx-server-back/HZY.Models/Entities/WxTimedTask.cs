using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HZY.Models.Entities.BaseEntitys;
using HZY.Models.Enums;

namespace HZY.Models.Entities
{
    /// <summary>
    /// 微信机器人定时任务
    /// </summary>
    public class WxTimedTask : DefaultBaseEntity<Guid>
    {

        /// <summary>
        ///  应用Token => 备注: 应用Token
        /// </summary>
        public String ApplicationToken { get; set; }


        /// <summary>
        ///  接受对象(WxId) => 备注: 接受对象(联系人Id)
        /// </summary>
        public String ReceivingObjectWxId { get; set; }

        /// <summary>
        ///  接受对象(WxId) => 备注: 接受对象(联系人Id)
        /// </summary>
        public String ReceivingObjectName { get; set; }


        /// <summary>
        ///  发送类型 => 备注: 发送类型
        /// </summary>
        public ETimedTaskSendType SendType { get; set; } = ETimedTaskSendType.WBNR;


        /// <summary>
        ///  发送内容(发送类型为文本时生效) => 备注: 发送内容(发送类型为文本时生效)
        /// </summary>
        public String SendContent { get; set; }

        /// <summary>
        ///  http请求url => 备注: http请求url
        /// </summary>
        public String HttpSendUrl { get; set; }


        /// <summary>
        ///  发送时间(cron表达式) => 备注: 发送时间(cron表达式)
        /// </summary>
        public String SendTime { get; set; }
        /// <summary>
        ///  结尾备注 => 备注: 结尾备注
        /// </summary>
        public String ClosingRemarks { get; set; }
        /// <summary>
        /// http内容解析表达式
        /// </summary>
        public string AnalyzeExpression { set; get; }
        /// <summary>
        /// 消息类型
        /// </summary>
        public EMessageType MessageType { set; get; }
    }
}