using HZY.Models.Entities;
using HZY.Services.Admin.Core;
using HZY.EFCore.Repositories.Admin.Core;
using HZY.Services.Admin.WxBot.Http;
using HZY.Models.Enums;
using HzyScanDiService;

namespace HZY.Services.Admin
{
    /// <summary>
    /// 内容发送 服务 
    /// </summary>
    public class ContentSendService : IScopedSelfDependency
    {
        private readonly TianXingService _tianXingService;
        private readonly XiaoiBotService _xiaoiBotService;
        IAdminRepository<WxBotConfig> _wxBotConfigRepository;
        private readonly HttpService _httpService;
        public ContentSendService(TianXingService tianXingService,
              IAdminRepository<WxBotConfig> wxBotConfigRepository,
              XiaoiBotService xiaoiBotService,
              HttpService httpService)
        {
            _tianXingService = tianXingService;
            _wxBotConfigRepository = wxBotConfigRepository;
            _xiaoiBotService = xiaoiBotService;
            _httpService = httpService;
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
        /// 获取发送内容
        /// </summary>
        /// <param name="tianXingApiKey">天行key</param>
        /// <param name="sendObj">发送内容参数</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<string> GetSendContentAsync(string tianXingApiKey, (ETimedTaskSendType, string, string) sendObj)
        {
            return sendObj.Item1 switch
            {
                ETimedTaskSendType.WBNR => sendObj.Item2,
                ETimedTaskSendType.XWZX => await _tianXingService.GetNewsAsync(tianXingApiKey),
                ETimedTaskSendType.GSDQ => await _tianXingService.GetStoryAsync(tianXingApiKey),
                ETimedTaskSendType.TWQH => await _tianXingService.GetLoveWordsAsync(tianXingApiKey),
                ETimedTaskSendType.XHDQ => await _tianXingService.GetJokesAsync(tianXingApiKey),
                ETimedTaskSendType.HTTP => await _httpService.GetAsync(sendObj.Item3),
                _ => throw new NotImplementedException(),
            };
        }
    }
}