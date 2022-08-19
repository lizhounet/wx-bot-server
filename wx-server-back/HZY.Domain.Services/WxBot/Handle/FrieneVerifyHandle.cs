using HZY.Models.DTO.WxBot;
using HZY.Models.Entities;
using HzyScanDiService;
using xYohttp_dotnet.Domain.Model.CallBackMsg;
using xYohttp_dotnet.Http;

namespace HZY.Domain.Services.WxBot.Handle
{
    public class FrieneVerifyHandle : BaseHandle, IScopedSelfDependency
    {
        public FrieneVerifyHandle(WxAccountService wxAccountService, ContentSendService contentSendService)
            : base(wxAccountService, contentSendService)
        {
        }
        public async Task OnEventFrieneVerifyAsync(EventFrieneVerifyMsg msg, string applictionToken)
        {
            WxBotConfig wxBotConfig = GetWxBotConfig(applictionToken);
            WxUserInfoDTO wxUserInfoDTO = GetWxUserInfoDTO(applictionToken);
            if (wxBotConfig.AutoAgreeFriendVerify == 0) return;
            //获取验证信息
            string fromContent = msg.JsonMsg?["from_content"]?.ToString();
            if (!string.IsNullOrEmpty(fromContent))
            {
                if (wxBotConfig != null)
                {
                    XyoHttpApi xyoHttpApi = GetXyoHttpApi(applictionToken);
                    string[] rrules = wxBotConfig.AutoAgreeFriendVerifRrule?.Split(",");
                    if (rrules.Length <= 0)
                    {
                        await xyoHttpApi.AgreeFriendVerifyAsync(wxUserInfoDTO.WxId, msg.V1, msg.V2, msg.Type);
                    }
                    else
                    {
                        foreach (var rule in rrules)
                        {
                            if (fromContent.Contains(rule)) {
                                await xyoHttpApi.AgreeFriendVerifyAsync(wxUserInfoDTO.WxId, msg.V1, msg.V2, msg.Type);
                                break;
                            }

                        }
                    }
                }

            }
        }

    }
}
