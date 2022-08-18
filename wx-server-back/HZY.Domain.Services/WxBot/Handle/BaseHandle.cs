using HZY.Models.DTO.WxBot;
using HZY.Models.Entities;
using HzyScanDiService;
using xYohttp_dotnet.Http;

namespace HZY.Domain.Services.WxBot.Handle
{
    public class BaseHandle : IScopedSelfDependency
    {
        protected readonly WxAccountService _wxAccountService;
        protected readonly ContentSendService _contentSendService;
        public BaseHandle(WxAccountService wxAccountService, ContentSendService contentSendService)
        {
            _wxAccountService = wxAccountService;
            _contentSendService = contentSendService;
        }

        protected WxBotConfig GetWxBotConfig(string applictionToken)
        {
            return _wxAccountService.GetWxBotConfigByApplictionTokenAsync(applictionToken).Result;
        }
        protected XyoHttpApi GetXyoHttpApi(string applictionToken)
        {
            return _wxAccountService.GetXyoHttpApiAsync(applictionToken).Result;
        }
        protected WxUserInfoDTO GetWxUserInfoDTO(string applictionToken)
        {
            return _wxAccountService.GetWxUserInfoByApplictionTokenAsync(applictionToken).Result;
        }

    }
}
