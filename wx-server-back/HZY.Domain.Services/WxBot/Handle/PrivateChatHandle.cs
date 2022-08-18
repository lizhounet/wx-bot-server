using HZY.Models.DTO.WxBot;
using HZY.Models.Entities;
using HzyScanDiService;
using xYohttp_dotnet.Domain.Model.CallBackMsg;
using xYohttp_dotnet.Http;

namespace HZY.Domain.Services.WxBot.Handle
{
    public class PrivateChatHandle : BaseHandle, IScopedSelfDependency
    {

        public PrivateChatHandle(WxAccountService wxAccountService, ContentSendService contentSendService)
              : base(wxAccountService, contentSendService)
        {
        }
        public async Task OnEventPrivateChatAsync(EventPrivateChatMsg msg, string applictionToken)
        {
            switch (msg.Type)
            {
                case xYohttp_dotnet.Common.Enums.EMsgType.文本:
                    await HandleTextAsync(msg, applictionToken);
                    break;
                default:
                    break;
            }
        }
        /// <summary>
        /// 文本消息处理
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="applictionToken"></param>
        /// <returns></returns>
        private async Task HandleTextAsync(EventPrivateChatMsg msg, string applictionToken)
        {
            WxBotConfig wxBotConfig = GetWxBotConfig(applictionToken);
            WxUserInfoDTO wxUserInfoDTO = GetWxUserInfoDTO(applictionToken);
            XyoHttpApi xyoHttpApi = GetXyoHttpApi(applictionToken);
            //判断私聊回复开启
            if (wxBotConfig.TalkPrivateAutoReplyFlag == 1)
            {
                //获取关键字回复
                string content = await _contentSendService.GetkeyWorldContentAsync(msg.Msg, applictionToken);
                if (string.IsNullOrEmpty(content))
                {
                    //获取机器人回复
                    content = await _contentSendService.BotReplyAsync(applictionToken, msg.Msg, msg.FromWxId);
                }
                await xyoHttpApi.SendTextMsgAsync(wxUserInfoDTO.WxId, msg.FromWxId, content);

            }


        }
    }
}
