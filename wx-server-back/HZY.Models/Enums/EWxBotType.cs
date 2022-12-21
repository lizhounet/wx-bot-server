using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HZY.Models.Enums
{
    /// <summary>
    /// 聊天机器人类型 枚举
    /// </summary>
    public enum EWxBotType
    {
        [Description("天行机器人")]
        TIANXING = 1,
        [Description("小i机器人")]
        XIAOI = 2,
        [Description("CHATGPT")]
        CHATGPT = 3
    }
}
