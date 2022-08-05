using Microsoft.AspNetCore.Mvc;
using xYohttp_dotnet.Controllers;
using xYohttp_dotnet.Domain.Model.CallBackMsg;
using xYohttp_dotnet.Domain.Model.Dto;

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

    public override int OnEventPrivateChat(EventPrivateChatMsg msg)
    {
        return 0;
    }

    public override int OnEventDeviceCallback(EventDeviceCallbackMsg msg)
    {
        return 0;
    }
    public override int OnLogin(LoginMsg msg)
    {
        return 0;
    }
    public override int OnEventInvitedInGroup(EventInvitedInGroupMsg msg)
    {
        return 0;
    }
}