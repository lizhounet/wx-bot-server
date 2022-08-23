using HZY.Models.Entities;
using HZY.EFCore.Repositories.Admin.Core;
using HZY.Models.Enums;
using HzyScanDiService;
using xYohttp_dotnet.Http;
using HZY.Domain.Services.WxBot.Http;
using HZY.Infrastructure;
using HZY.Models.DTO.WxBot;

namespace HZY.Domain.Services.WxBot
{
    /// <summary>
    /// 内容发送 服务 
    /// </summary>
    public class ContentSendService : IScopedSelfDependency
    {
        private readonly TianXingService _tianXingService;
        private readonly XiaoiBotService _xiaoiBotService;
        private readonly IAdminRepository<WxTimedTask> _timedTaskRepository;
        private readonly IAdminRepository<WxSayEveryDay> _sayEveryDayRepository;
        private readonly IAdminRepository<WxKeywordReply> _keywordReplyRepository;
        private readonly HttpService _httpService;
        private readonly WxAccountService _wxAccountService;
        private readonly IAdminRepository<WxKeywordItem> _wxKeywordItemRepository;
        public ContentSendService(TianXingService tianXingService,
              XiaoiBotService xiaoiBotService,
              HttpService httpService,
              IAdminRepository<WxTimedTask> timedTaskRepository,
              IAdminRepository<WxSayEveryDay> sayEveryDayRepository,
              WxAccountService wxAccountService,
              IAdminRepository<WxKeywordReply> keywordReplyRepository,
              IAdminRepository<WxKeywordItem> wxKeywordItemRepository)
        {
            _tianXingService = tianXingService;
            _xiaoiBotService = xiaoiBotService;
            _httpService = httpService;
            _timedTaskRepository = timedTaskRepository;
            _sayEveryDayRepository = sayEveryDayRepository;
            _wxAccountService = wxAccountService;
            _keywordReplyRepository = keywordReplyRepository;
            _wxKeywordItemRepository = wxKeywordItemRepository;
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
            WxBotConfig wxBotConfig = await _wxAccountService.GetWxBotConfigByApplictionTokenAsync(applicationToken);
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
        /// <param name="keyword">关键词</param>
        /// <param name="applicationToken">applicationToken</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<string> GetkeyWorldContentAsync(string keyword, string applicationToken)
        {
            WxBotConfig wxBotConfig = await _wxAccountService.GetWxBotConfigByApplictionTokenAsync(applicationToken);
            //精确匹配优先级高于模糊匹配
            List<WxKeywordItem> wxKeywordItems = _wxKeywordItemRepository.Select.Where(w => w.ApplicationToken == applicationToken)
                .Where(w => keyword.Contains(w.KeyWords)).ToList();
            if (wxKeywordItems != null && wxKeywordItems.Count > 0)
            {
                WxKeywordReply keywordReply = null;
                //先取精确匹配
                WxKeywordItem wxKeywordItem = wxKeywordItems.FirstOrDefault(w => w.KeyWords.Equals(keyword) && w.MatchType == EMatchType.JINGQUE);
                if (wxKeywordItem == null)
                {
                    //取模糊匹配
                    wxKeywordItem = wxKeywordItems.FirstOrDefault(w => keyword.Contains(w.KeyWords) && w.MatchType == EMatchType.MOHU);
                }
                if (wxKeywordItem == null) return null;
                keywordReply = await _keywordReplyRepository.FindByIdAsync(wxKeywordItem.KeyWordReplyId);
                if (keywordReply == null) return null;
                return keywordReply.SendType switch
                {
                    ETimedTaskSendType.WBNR => keywordReply.SendContent,
                    ETimedTaskSendType.XWZX => await _tianXingService.GetNewsAsync(wxBotConfig.TianXingApiKey),
                    ETimedTaskSendType.GSDQ => await _tianXingService.GetStoryAsync(wxBotConfig.TianXingApiKey),
                    ETimedTaskSendType.TWQH => await _tianXingService.GetLoveWordsAsync(wxBotConfig.TianXingApiKey),
                    ETimedTaskSendType.XHDQ => await _tianXingService.GetJokesAsync(wxBotConfig.TianXingApiKey),
                    ETimedTaskSendType.HTTP => await _httpService.GetAsync(keywordReply.HttpSendUrl),
                    _ => throw new NotImplementedException(),
                };
            }
            return null;
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
            //计算下次周年日期
            int anniversary = (DateTime.Now.Date.Year - sayEveryDay.AnniversaryDay.Date.Year);
            anniversary = anniversary == 0 ? 1 : anniversary;
            if (sayEveryDay.AnniversaryDay.Date.AddYears(anniversary)
                < DateTime.Now.Date) anniversary++;
            //计算下次周年还有多少天
            int anniversaryDays = (sayEveryDay.AnniversaryDay.Date.AddYears(anniversary) - DateTime.Now.Date).Days;
            //计算生日还有多少天
            int birthdays = (sayEveryDay.BirthdayDate.Date - DateTime.Now.Date).Days;

            string result = $"[emoji=\\uD83D\\uDE18]{DateTime.Now:yyyy-MM-dd HH:mm} {Tools.GetWeekByDate(DateTime.Now)}\n" +
                $"\n[emoji=\\u2600]元气满满的一天开始啦,要开心噢^_^" +
                $"\n[emoji=\\uD83D\\uDC6B]宝贝,今天是我们相恋的第{days}天" +
                $"\n[emoji=\\uD83D\\uDC6B]距离你的生日还有{birthdays}天" +
                $"\n[emoji=\\uD83D\\uDC6B]距离我们恋爱{anniversary}周年纪念日还有{anniversaryDays}天" +
                $"\n\n[emoji=\\uD83C\\uDF08]天气情况:" +
                $"\n{weather}" +
                $"\n\n[emoji=\\uD83D\\uDCAA]每日一句:" +
                $"\n{dayOne}" +
                $"\n\n[emoji=\\uD83D\\uDC91]情话对你说:" +
                $"\n{loveWords}" +
                $"\n————————{sayEveryDay.ClosingRemarks}";
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
                WxBotConfig wxBotConfig = await _wxAccountService.GetWxBotConfigByApplictionTokenAsync(sayEveryDay.ApplicationToken);
                WxUserInfoDTO wxUserInfoDTO = await _wxAccountService.GetWxUserInfoByApplictionTokenAsync(sayEveryDay.ApplicationToken);
                //获取发送内容
                string content = await this.GetSayEveryDayTextAsync(sayEveryDay, wxBotConfig);

                var xyoHttp = await _wxAccountService.GetXyoHttpApiAsync(sayEveryDay.ApplicationToken);
                //需要发送的微信
                var wxIds = sayEveryDay.ReceivingObjectWxId.Split(",").ToList();
                var wxNames = sayEveryDay.ReceivingObjectName.Split(",").ToList();
                foreach (var wxId in wxIds)
                {
                    await xyoHttp.SendTextMsgAsync(wxUserInfoDTO.WxId, wxId, content);
                    log += $"\n{wxUserInfoDTO.WxId}发送消息给{wxNames[wxIds.IndexOf(wxId)]}\n消息内容：\n{content}";
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
                WxBotConfig wxBotConfig = await _wxAccountService.GetWxBotConfigByApplictionTokenAsync(wxTimedTask.ApplicationToken);
                WxUserInfoDTO wxUserInfoDTO = await _wxAccountService.GetWxUserInfoByApplictionTokenAsync(wxTimedTask.ApplicationToken);
                //获取发送内容
                string content = await this.GetTimedTaskContentAsync(wxTimedTask, wxBotConfig);
                var xyoHttp = await _wxAccountService.GetXyoHttpApiAsync(wxTimedTask.ApplicationToken);
                //需要发送的微信
                var wxIds = wxTimedTask.ReceivingObjectWxId.Split(",").ToList();
                var wxNames = wxTimedTask.ReceivingObjectName.Split(",").ToList();
                foreach (var wxId in wxIds)
                {
                    await xyoHttp.SendTextMsgAsync(wxUserInfoDTO.WxId, wxId, content);
                    log += $"\n{wxUserInfoDTO.WxId}发送消息给{wxNames[wxIds.IndexOf(wxId)]}\n消息内容：\n{content}\r\n";
                }
            }
            return log;
        }

    }
}