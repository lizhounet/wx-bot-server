using HZY.Models.Entities;
using HZY.Services.Admin.Core;
using HZY.EFCore.Repositories.Admin.Core;
using HZY.Services.Admin.WxBot.Http;
using HZY.Models.Enums;
using HzyScanDiService;

namespace HZY.Services.Admin
{
    /// <summary>
    /// 机器人聊天回复 服务 
    /// </summary>
    public class BotReplyService : IScopedSelfDependency
    {
        private readonly TianXingService _tianXingService;
        private readonly XiaoiBotService _xiaoiBotService;
        IAdminRepository<WxBotConfig> _wxBotConfigRepository;
        public BotReplyService(IAdminRepository<WxKeywordReply> defaultRepository,
            TianXingService tianXingService,
              IAdminRepository<WxBotConfig> wxBotConfigRepository,
              XiaoiBotService xiaoiBotService)
        {
            _tianXingService = tianXingService;
            _wxBotConfigRepository = wxBotConfigRepository;
            _xiaoiBotService = xiaoiBotService;
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
    }
}