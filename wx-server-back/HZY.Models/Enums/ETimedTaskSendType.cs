using System.ComponentModel;

namespace HZY.Models.Enums
{
    /// <summary>
    /// 定时任务发送类型 枚举
    /// </summary>
    public enum ETimedTaskSendType
    {
        [Description("文本内容")]
        WBNR = 1,
        [Description("新闻咨询")]
        XWZX = 2,
        [Description("故事大全")]
        GSDQ = 3,
        [Description("土味情话")]
        TWQH = 4,
        [Description("笑话大全")]
        XHDQ = 5,
        [Description("HTTP请求")]
        HTTP = 6,
    }
}
