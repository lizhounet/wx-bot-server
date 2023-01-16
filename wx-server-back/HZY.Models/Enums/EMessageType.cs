using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HZY.Models.Enums
{
    /// <summary>
    /// 消息类型
    /// </summary>
    public enum EMessageType
    {
        [Description("文本")]
        IMAGE = 1,
        [Description("图片")]
        TEXT = 2
    }
}
