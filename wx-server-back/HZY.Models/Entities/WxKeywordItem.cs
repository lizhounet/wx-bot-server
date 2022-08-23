using System;
using System.ComponentModel.DataAnnotations;
using HZY.Models.Entities.BaseEntitys;
using HZY.Models.Enums;

namespace HZY.Models.Entities
{
    /// <summary>
    /// 关键词回复的关键词
    /// </summary>
    public class WxKeywordItem : DefaultBaseEntity<Guid>
    {
        /// <summary>
        ///  应用Token => 备注: 应用Token
        /// </summary>
        public String ApplicationToken { get; set; }
        /// <summary>
        ///  关键词回复id => 备注: 关键词回复id
        /// </summary>
        public Guid KeyWordReplyId { get; set; }
        /// <summary>
        ///  关键词 => 备注: 关键词
        /// </summary>
        public string KeyWords { get; set; }
        /// <summary>
        ///  匹配类型(模糊匹配,精确匹配) => 备注: 匹配类型(模糊匹配,精确匹配)
        /// </summary>
        public EMatchType MatchType { get; set; } = EMatchType.MOHU;



    }
}