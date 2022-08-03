using HZY.Infrastructure.Controllers;
using Microsoft.AspNetCore.Mvc;
using xYohttp_dotnet.Controllers;
using xYohttp_dotnet.Domain.Model.CallBackMsg;
using xYohttp_dotnet.Domain.Model.Dto;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace HZY.WebHost.Controllers;

/// <summary>
/// xyo 消息回调 Controller
/// </summary>
[ApiController]
[Route("api/xyoMsg")]
public class XyoMsgController: XyoMsgControllerBase
{
    [HttpPost("callback")]
    public override XyoHttpReplyDto ProcessMessage(XyoHttpCallBackDto callBackDto)
    {
        return base.ProcessMessage(callBackDto);
    }
    public override int OnEventGroupEstablish(EventGroupEstablishMsg msg)
    {
        return 0;
    }

    public override int OnEventGroupMemberDecrease(EventGroupMemberDecreaseMsg msg)
    {
        return 0;
    }

    public override int OnEventGroupNameChange(EventGroupNameChangeMsg msg)
    {
        return 0;
    }

    public override int OnEventGroupMemberAdd(EventGroupMemberAddMsg msg)
    {
        return 0;
    }

    public override int OnEventGroupChat(EventGroupChatMsg msg)
    {
        return 0;
    }

    public override int OnEventQRcodePayment(EventQRcodePaymentMsg msg)
    {
        return 0;
    }

    public override int OnEventFrieneVerify(EventFrieneVerifyMsg msg)
    {
        return 0;
    }

    public override int OnEventDownloadFile(EventDownloadFileMsg msg)
    {
        return 0;
    }

    public override int OnEventpublicChat(EventPrivateChatMsg msg)
    {
        return 0;
    }

    public override int OnEventDeviceCallback(EventDeviceCallbackMsg msg)
    {
        return 0;
    }
    /// <summary>
    /// 新的账号登录成功/下线
    /// </summary>
    /// <param name="msg">事件消息实体</param>
    /// <returns></returns>
    public override int OnLogin(LoginMsg msg)
    {
        return 0;
    }
    /// <summary>
    /// 被邀请入群事件 PS: 企业微信不传递此事件
    /// </summary>
    /// <param name="msg">事件消息实体</param>
    /// <returns></returns>
    public override int OnEventInvitedInGroup(EventInvitedInGroupMsg msg)
    {
        return 0;
    }
}