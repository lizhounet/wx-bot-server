using System;
using System.ComponentModel.DataAnnotations;
using HZY.Models.Entities.BaseEntitys;
using HZY.Models.Enums;

namespace HZY.Models.Entities
{
    /// <summary>
    /// 关键词回复表
    /// </summary>
    public class WxKeywordReply : DefaultBaseEntity<Guid>
    {

        /// <summary>
        ///  应用Token => 备注: 应用Token
        /// </summary>
        public String ApplicationToken { get; set; }


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
        ///  关键词 => 备注: 关键词
        /// </summary>
        public String KeyWord { get; set; }


        /// <summary>
        ///  匹配类型(模糊匹配,精确匹配) => 备注: 匹配类型(模糊匹配,精确匹配)
        /// </summary>
        public EMatchType MatchType { get; set; } = EMatchType.MOHU;

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