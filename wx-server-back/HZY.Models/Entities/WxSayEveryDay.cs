using System;
using System.ComponentModel.DataAnnotations;
using HZY.Models.Entities.BaseEntitys;
using HZY.Models.Enums;

namespace HZY.Models.Entities
{
    /// <summary>
    /// 情侣每日说
    /// </summary>
    public class WxSayEveryDay : DefaultBaseEntity<Guid>
    {

        /// <summary>
        ///  应用Token => 备注: 应用Token
        /// </summary>
        public String ApplicationToken { get; set; }


        /// <summary>
        ///  接受对象(wxId,用逗号隔开) => 备注: 接受对象(wxId,用逗号隔开)
        /// </summary>
        public String ReceivingObjectWxId { get; set; }

        /// <summary>
        ///  接受对象(wxName,用逗号隔开) => 备注: 接受对象(wxName,用逗号隔开)
        /// </summary>
        public String ReceivingObjectName { get; set; }


        /// <summary>
        ///  发送时间(cron表达式) => 备注: 发送时间(cron表达式)
        /// </summary>
        public String SendTime { get; set; }


        /// <summary>
        ///  所在城市 => 备注: 所在城市
        /// </summary>
        public String City { get; set; }


        /// <summary>
        ///  结尾备注 => 备注: 结尾备注
        /// </summary>
        public String ClosingRemarks { get; set; }


        /// <summary>
        ///  纪念日 => 备注: 纪念日
        /// </summary>
        public DateTime AnniversaryDay { get; set; } = DateTime.Now;
        /// <summary>
        /// 任务状态(1=运行中,2=未运行,3=已停止) => 备注: 任务状态
        /// </summary>
        public ETaskState TaskState { set; get; } = ETaskState.NOTRUNNING;
        /// <summary>
        ///  机器人微信id => 备注: 机器人微信id
        /// </summary>
        public String RobotWxId { get; set; }


    }
}