using HZY.Models.Entities;
using HZY.Services.Admin.Core;
using HZY.EFCore.Repositories.Admin.Core;
using HZY.Services.Admin.WxBot.Http;
using HZY.Models.Enums;
using HzyScanDiService;
using HZY.Models.VO;

namespace HZY.Services.Admin
{
    /// <summary>
    /// 内容发送 服务 
    /// </summary>
    public class ContentSendService : IScopedSelfDependency
    {
        private readonly TianXingService _tianXingService;
        private readonly XiaoiBotService _xiaoiBotService;
        private readonly ChatGptService _chatGptService;
        IAdminRepository<WxBotConfig> _wxBotConfigRepository;
        private readonly HttpService _httpService;
        public ContentSendService(TianXingService tianXingService,
              IAdminRepository<WxBotConfig> wxBotConfigRepository,
              XiaoiBotService xiaoiBotService,
              ChatGptService chatGptService,
              HttpService httpService)
        {
            _tianXingService = tianXingService;
            _wxBotConfigRepository = wxBotConfigRepository;
            _xiaoiBotService = xiaoiBotService;
            _httpService = httpService;
            _chatGptService = chatGptService;
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
                EWxBotType.CHATGPT => await _chatGptService.GetBotReplyAsync(wxBotConfig.ChatGptKey, keyword, uniqueid),
                _ => "我什么都不知道",
            };
        }

        /// <summary>
        /// 获取定时任务发送内容
        /// </summary>
        /// <param name="tianXingApiKey">天行key</param>
        /// <param name="userContent">用户发送的内容</param>
        /// <param name="timedTask">定时任务实体</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<MessageVO> GetSendContentAsync(string tianXingApiKey, string userContent, WxTimedTask timedTask)
        {
            return timedTask.SendType switch
            {
                ETimedTaskSendType.WBNR => new MessageVO { Result = timedTask.SendContent, MessageType = timedTask.MessageType, ClosingRemarks = timedTask.ClosingRemarks },
                ETimedTaskSendType.XWZX => new MessageVO { Result = await _tianXingService.GetNewsAsync(tianXingApiKey), MessageType = timedTask.MessageType, ClosingRemarks = timedTask.ClosingRemarks },
                ETimedTaskSendType.GSDQ => new MessageVO { Result = await _tianXingService.GetStoryAsync(tianXingApiKey), MessageType = timedTask.MessageType, ClosingRemarks = timedTask.ClosingRemarks },
                ETimedTaskSendType.TWQH => new MessageVO { Result = await _tianXingService.GetLoveWordsAsync(tianXingApiKey), MessageType = timedTask.MessageType, ClosingRemarks = timedTask.ClosingRemarks },
                ETimedTaskSendType.XHDQ => new MessageVO { Result = await _tianXingService.GetJokesAsync(tianXingApiKey), MessageType = timedTask.MessageType, ClosingRemarks = timedTask.ClosingRemarks },
                ETimedTaskSendType.HTTP => new MessageVO
                {
                    Result = await _httpService.GetAsync(timedTask.HttpSendUrl, userContent),
                    IsAnalyze = true,
                    AnalyzeExpression = timedTask.AnalyzeExpression,
                    MessageType = timedTask.MessageType,
                    ClosingRemarks = timedTask.ClosingRemarks
                },
                _ => throw new NotImplementedException(),
            };
        }
        /// <summary>
        /// 获取关键字发送内容
        /// </summary>
        /// <param name="tianXingApiKey">天行key</param>
        /// <param name="userContent">用户发送的内容</param>
        /// <param name="keywordReply">关键字实体</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<MessageVO> GetSendContentAsync(string tianXingApiKey, string userContent, WxKeywordReply keywordReply)
        {
            return keywordReply.SendType switch
            {
                ETimedTaskSendType.WBNR => new MessageVO { Result = keywordReply.SendContent, MessageType = keywordReply.MessageType },
                ETimedTaskSendType.XWZX => new MessageVO { Result = await _tianXingService.GetNewsAsync(tianXingApiKey), MessageType = keywordReply.MessageType },
                ETimedTaskSendType.GSDQ => new MessageVO { Result = await _tianXingService.GetStoryAsync(tianXingApiKey), MessageType = keywordReply.MessageType },
                ETimedTaskSendType.TWQH => new MessageVO { Result = await _tianXingService.GetLoveWordsAsync(tianXingApiKey), MessageType = keywordReply.MessageType },
                ETimedTaskSendType.XHDQ => new MessageVO { Result = await _tianXingService.GetJokesAsync(tianXingApiKey), MessageType = keywordReply.MessageType },
                ETimedTaskSendType.HTTP => new MessageVO
                {
                    Result = await _httpService.GetAsync(keywordReply.HttpSendUrl, userContent),
                    IsAnalyze = true,
                    AnalyzeExpression = keywordReply.AnalyzeExpression,
                    MessageType = keywordReply.MessageType
                },
                _ => throw new NotImplementedException(),
            };
        }
    }
}