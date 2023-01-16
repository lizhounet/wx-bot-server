using HZY.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HZY.Models.VO
{
    public class MessageVO
    {
        /// <summary>
        /// 是否需要解析
        /// </summary>
        public bool IsAnalyze { get; set; } = false;
        /// <summary>
        /// 结果
        /// </summary>
        public string Result { get; set; }
        /// <summary>
        /// 解析表达式
        /// </summary>
        public string AnalyzeExpression { set; get; }
        /// <summary>
        /// 消息类型
        /// </summary>
        public EMessageType MessageType { set; get; }
        /// <summary>
        ///  结尾备注 => 备注: 结尾备注
        /// </summary>
        public String ClosingRemarks { get; set; }
    }
}
