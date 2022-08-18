using HZY.Models.DTO.WxBot;
using HZY.Models.Entities;
using HzyScanDiService;
using xYohttp_dotnet.Domain.Model.CallBackMsg;
using xYohttp_dotnet.Http;

namespace HZY.Domain.Services.WxBot.Handle
{
    public class GroupChatHandle : BaseHandle, IScopedSelfDependency
    {
        public GroupChatHandle(WxAccountService wxAccountService, ContentSendService contentSendService)
            : base(wxAccountService, contentSendService)
        {
        }
        public async Task OnEventGroupChatAsync(EventGroupChatMsg msg, string applictionToken)
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
        private async Task HandleTextAsync(EventGroupChatMsg msg, string applictionToken)
        {

            WxUserInfoDTO wxUserInfoDTO = GetWxUserInfoDTO(applictionToken);
            var atUserList = msg.MsgSource?.AtUserLists;
            var message = msg.Msg;
            //判断消息是否艾特机器人了
            if (atUserList != null && atUserList.Any(w => w.WxId == wxUserInfoDTO.WxId))
            {
                atUserList.ForEach(w => message = message.Replace($"[at={w.WxId}]", ""));
                XyoHttpApi xyoHttpApi = GetXyoHttpApi(applictionToken);
                WxBotConfig wxBotConfig = GetWxBotConfig(applictionToken);
                //判断群聊回复开启
                if (wxBotConfig.GroupAutoReplyFlag == 1)
                {
                    //获取关键字回复
                    string content = await _contentSendService.GetkeyWorldContentAsync(message, applictionToken);
                    if (string.IsNullOrEmpty(content))
                    {
                        //获取机器人回复
                        content = await _contentSendService.BotReplyAsync(applictionToken, message, msg.FromWxId);
                    }
                    await xyoHttpApi.SendGroupMsgAndAtAsync(wxUserInfoDTO.WxId, msg.FromGroup, msg.FromWxId, msg.FromName, content);

                }
            }

        }
    }
}
