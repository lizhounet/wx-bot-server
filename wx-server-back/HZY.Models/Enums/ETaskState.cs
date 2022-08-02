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
    /// 任务状态 枚举
    /// </summary>
    public enum ETaskState
    {
        [Description("运行中")]
        RUNNING = 1,
        [Description("未运行")]
        NOTRUNNING = 2,
        [Description("已停止")]
        STOP = 3,
    }
}
