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
        [Description("图片")]
        IMAGE = 2,
        [Description("文本")]
        TEXT = 1
    }
}
