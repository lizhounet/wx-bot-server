using HZY.Models.Entities;
using HZY.EFCore.Repositories.Admin.Core;
using HZY.Models.Enums;
using HzyScanDiService;
using xYohttp_dotnet.Http;
using HZY.Domain.Services.WxBot.Http;
using HZY.Infrastructure;

namespace HZY.Domain.Services.WxBot
{
    /// <summary>
    /// 内容发送 服务 
    /// </summary>
    public class ContentSendService : IScopedSelfDependency
    {
        private readonly TianXingService _tianXingService;
        private readonly XiaoiBotService _xiaoiBotService;
        private readonly IAdminRepository<WxBotConfig> _wxBotConfigRepository;
        private readonly IAdminRepository<WxTimedTask> _timedTaskRepository;
        private readonly IAdminRepository<WxSayEveryDay> _sayEveryDayRepository;
        private readonly HttpService _httpService;
        public ContentSendService(TianXingService tianXingService,
              IAdminRepository<WxBotConfig> wxBotConfigRepository,
              XiaoiBotService xiaoiBotService,
              HttpService httpService,
              IAdminRepository<WxTimedTask> timedTaskRepository,
              IAdminRepository<WxSayEveryDay> sayEveryDayRepository)
        {
            _tianXingService = tianXingService;
            _wxBotConfigRepository = wxBotConfigRepository;
            _xiaoiBotService = xiaoiBotService;
            _httpService = httpService;
            _timedTaskRepository = timedTaskRepository;
            _sayEveryDayRepository = sayEveryDayRepository;
        }

        /// <summary>
        /// 机器人回复
        /// </summary>
        /// <param name="applicationToken">applicationToken</param>
        /// <param name="keyword">关键字</param>
        ///  <param name="uniqueid">用户唯一身份ID，方便上下文关联</param>
        /// <returns></returns>
        public async Task<string> BotReplyAsync(string applicationToken, string keyword, string uniqueid)
        {
            WxBotConfig wxBotConfig = await _wxBotConfigRepository.FindAsync(w => w.ApplicationToken == applicationToken);
            return wxBotConfig.ReplyBotType switch
            {
                EWxBotType.TIANXING => await _tianXingService.GetBotReplyAsync(wxBotConfig.TianXingApiKey, keyword, uniqueid),
                EWxBotType.XIAOI => await _xiaoiBotService.GetBotReplyAsync(keyword, uniqueid),
                _ => "我什么都不知道",
            };
        }
        /// <summary>
        /// 获取定时任务发送内容
        /// </summary>
        /// <param name="wxTimedTask">微信机器人定时任务</param>
        /// <param name="wxBotConfig">个微小助手基础配置</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<string> GetTimedTaskContentAsync(WxTimedTask wxTimedTask, WxBotConfig wxBotConfig)
        {
            string content = wxTimedTask.SendType switch
            {
                ETimedTaskSendType.WBNR => wxTimedTask.SendContent,
                ETimedTaskSendType.XWZX => await _tianXingService.GetNewsAsync(wxBotConfig.TianXingApiKey),
                ETimedTaskSendType.GSDQ => await _tianXingService.GetStoryAsync(wxBotConfig.TianXingApiKey),
                ETimedTaskSendType.TWQH => await _tianXingService.GetLoveWordsAsync(wxBotConfig.TianXingApiKey),
                ETimedTaskSendType.XHDQ => await _tianXingService.GetJokesAsync(wxBotConfig.TianXingApiKey),
                ETimedTaskSendType.HTTP => await _httpService.GetAsync(wxTimedTask.HttpSendUrl),
                _ => throw new NotImplementedException(),
            };

            if (string.IsNullOrEmpty(wxTimedTask.ClosingRemarks)) return content;
            return $"{content}\n\n————————{wxTimedTask.ClosingRemarks}";
        }
        /// <summary>
        /// 获取关键字回复发送内容
        /// </summary>
        /// <param name="keywordReply">关键词回复表</param>
        /// <param name="wxBotConfig">个微小助手基础配置</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<string> GetkeyWorldContentAsync(WxKeywordReply keywordReply, WxBotConfig wxBotConfig)
        {
            string content = keywordReply.SendType switch
            {
                ETimedTaskSendType.WBNR => keywordReply.SendContent,
                ETimedTaskSendType.XWZX => await _tianXingService.GetNewsAsync(wxBotConfig.TianXingApiKey),
                ETimedTaskSendType.GSDQ => await _tianXingService.GetStoryAsync(wxBotConfig.TianXingApiKey),
                ETimedTaskSendType.TWQH => await _tianXingService.GetLoveWordsAsync(wxBotConfig.TianXingApiKey),
                ETimedTaskSendType.XHDQ => await _tianXingService.GetJokesAsync(wxBotConfig.TianXingApiKey),
                ETimedTaskSendType.HTTP => await _httpService.GetAsync(keywordReply.HttpSendUrl),
                _ => throw new NotImplementedException(),
            };
            return content;
        }
        /// <summary>
        /// 获取每日说文本
        /// </summary>
        /// <param name="sayEveryDay">每日说实体</param>
        /// <param name="wxBotConfig">个微小助手基础配置</param>
        /// <returns></returns>
        public async Task<string> GetSayEveryDayTextAsync(WxSayEveryDay sayEveryDay, WxBotConfig wxBotConfig)
        {
            //获取天气
            string weather = await _tianXingService.GetWeatherAsync(wxBotConfig.TianXingApiKey, sayEveryDay.City);
            //获取每日一句
            string dayOne = await _tianXingService.GetDayOneAsync(wxBotConfig.TianXingApiKey);
            //获取情话
            string loveWords = await _tianXingService.GetLoveWordsAsync(wxBotConfig.TianXingApiKey);
            //计算在一起多少天
            int days = (DateTime.Now.Date - sayEveryDay.AnniversaryDay.Date).Days;
            string result = $"😘{DateTime.Now:yyyy-MM-dd HH:mm} {Tools.GetWeekByDate(DateTime.Now)}\n\n👫宝贝,今天是我们在一起的第{days}天啦" +
                $"\n\n☀️元气满满的一天开始啦,要开心噢^_^" +
                $"\n\n{sayEveryDay.City} 今日天气:" +
                $"\n{weather}" +
                $"\n\n💪每日一句:" +
                $"\n{dayOne}" +
                $"\n\n💑情话对你说:" +
                $"\n{loveWords}" +
                $"\n\n————————{sayEveryDay.ClosingRemarks}";
            return result;
        }
        /// <summary>
        /// 执行情侣每日说
        /// </summary>
        /// <param name="everyDayId">每日说id</param>
        /// <returns></returns>
        public async Task<string> ExecSayEveryDayAsync(Guid everyDayId)
        {
            string log = "";
            WxSayEveryDay sayEveryDay = await _sayEveryDayRepository.FindByIdAsync(everyDayId);
            if (sayEveryDay != null)
            {
                WxBotConfig wxBotConfig = await _wxBotConfigRepository.FindAsync(w => w.ApplicationToken == sayEveryDay.ApplicationToken);
                //获取发送内容
                string content = await this.GetSayEveryDayTextAsync(sayEveryDay, wxBotConfig);
                var xyoHttp = new XyoHttpApi(wxBotConfig.VlwHttpUrl, sayEveryDay.ApplicationToken);
                //需要发送的微信
                var wxIds = sayEveryDay.ReceivingObjectWxId.Split(",").ToList();
                foreach (var wxId in wxIds)
                {
                    await xyoHttp.SendTextMsgAsync(wxBotConfig.RobotWxId, wxId, content);
                    log += $"{wxBotConfig.RobotWxId}发送消息给{wxId},消息内容：{content}\r\n";
                }
            }
            return log;
        }

        /// <summary>
        /// 执行定时任务
        /// </summary>
        /// <param name="timedTaskId">定时任务id</param>
        /// <returns></returns>
        public async Task<string> ExecTimedTaskAsync(Guid timedTaskId)
        {
            string log = "";
            WxTimedTask wxTimedTask = await _timedTaskRepository.FindByIdAsync(timedTaskId);
            if (wxTimedTask != null)
            {
                WxBotConfig wxBotConfig = await _wxBotConfigRepository.FindAsync(w => w.ApplicationToken == wxTimedTask.ApplicationToken);
                //获取发送内容
                string content = await this.GetTimedTaskContentAsync(wxTimedTask, wxBotConfig);
                var xyoHttp = new XyoHttpApi(wxBotConfig.VlwHttpUrl, wxTimedTask.ApplicationToken);
                //需要发送的微信
                var wxIds = wxTimedTask.ReceivingObjectWxId.Split(",").ToList();
                foreach (var wxId in wxIds)
                {
                    await xyoHttp.SendTextMsgAsync(wxBotConfig.RobotWxId, wxId, content);
                    log += $"{wxBotConfig.RobotWxId}发送消息给{wxId},消息内容：{content}\r\n";
                }
            }
            return log;
        }

    }
}