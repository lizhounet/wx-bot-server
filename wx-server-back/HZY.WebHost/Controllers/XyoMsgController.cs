using HZY.Domain.Services.WxBot;
using HZY.Domain.Services.WxBot.Handle;
using HZY.Models.DTO.WxBot;
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
public class XyoMsgController : XyoMsgControllerBase
{

    private readonly PrivateChatHandle _privateChatHandle;
    private readonly GroupChatHandle _groupChatHandle;
    private string applictionToken;

    public XyoMsgController(
        PrivateChatHandle privateChatHandle, GroupChatHandle groupChatHandle)
    {
        _privateChatHandle = privateChatHandle;
        _groupChatHandle = groupChatHandle;
    }
    [HttpPost("callback1")]
    public override async Task<XyoHttpReplyDto> ProcessMessageAsync(XyoHttpCallBackDto callBackDto)
    {
        applictionToken = Request.Query["applictionToken"].ToString();
        if (string.IsNullOrEmpty(applictionToken)) return new XyoHttpReplyDto(0);
        return await base.ProcessMessageAsync(callBackDto);
    }
    /// <summary>
    /// 创建新的群聊事件
    /// </summary>
    /// <param name="msg">事件消息实体</param>
    /// <returns></returns>

    public override async Task<int> OnEventGroupEstablishAsync(EventGroupEstablishMsg msg)
    {
        return await Task.FromResult(0);
    }
    /// <summary>
    /// 群成员减少事件 PS: 群成员退出
    /// </summary>
    /// <param name="msg">事件消息实体</param>
    /// <returns></returns>

    public override async Task<int> OnEventGroupMemberDecreaseAsync(EventGroupMemberDecreaseMsg msg)
    {
        return await Task.FromResult(0);
    }
    /// <summary>
    /// 群名称变动事件
    /// </summary>
    /// <param name="msg">事件消息实体</param>
    /// <returns></returns>

    public override async Task<int> OnEventGroupNameChangeAsync(EventGroupNameChangeMsg msg)
    {
        return await Task.FromResult(0);
    }
    /// <summary>
    /// 群成员增加事件 PS: 新人进群
    /// </summary>
    /// <param name="msg">事件消息实体</param>
    /// <returns></returns>


    public override async Task<int> OnEventGroupMemberAddAsync(EventGroupMemberAddMsg msg)
    {
        return await Task.FromResult(0);
    }
    /// <summary>
    /// 群消息事件
    /// </summary>
    /// <param name="msg">事件消息实体</param>
    /// <returns></returns>

    public override async Task<int> OnEventGroupChatAsync(EventGroupChatMsg msg)
    {
        _groupChatHandle.OnEventGroupChatAsync(msg, applictionToken);
        return await Task.FromResult(0);
    }
    /// <summary>
    /// 面对面收款事件
    /// </summary>
    /// <param name="msg">事件消息实体</param>
    /// <returns></returns>

    public override async Task<int> OnEventQRcodePaymentAsync(EventQRcodePaymentMsg msg)
    {
        return await Task.FromResult(0);
    }
    /// <summary>
    /// 好友请求事件
    /// </summary>
    /// <param name="msg">事件消息实体</param>
    /// <returns></returns>

    public override async Task<int> OnEventFrieneVerifyAsync(EventFrieneVerifyMsg msg)
    {
        return await Task.FromResult(0);
    }
    /// <summary>
    /// 文件下载结束事件
    /// </summary>
    /// <param name="msg">事件消息实体</param>
    /// <returns></returns>

    public override async Task<int> OnEventDownloadFileAsync(EventDownloadFileMsg msg)
    {
        return await Task.FromResult(0);
    }
    /// <summary>
    /// 私聊消息事件
    /// </summary>
    /// <param name="msg">事件消息实体</param>
    /// <returns></returns>

    public override async Task<int> OnEventPrivateChatAsync(EventPrivateChatMsg msg)
    {
        await _privateChatHandle.OnEventPrivateChatAsync(msg, applictionToken);
        return 0;
    }
    /// <summary>
    /// 设备回调事件
    /// </summary>
    /// <param name="msg">事件消息实体</param>
    /// <returns></returns>

    public override async Task<int> OnEventDeviceCallbackAsync(EventDeviceCallbackMsg msg)
    {
        return await Task.FromResult(0);
    }
    /// <summary>
    /// 新的账号登录成功/下线
    /// </summary>
    /// <param name="msg">事件消息实体</param>
    /// <returns></returns>
    public override async Task<int> OnLoginAsync(LoginMsg msg)
    {
        return await Task.FromResult(0);
    }
    /// <summary>
    /// 被邀请入群事件 PS: 企业微信不传递此事件
    /// </summary>
    /// <param name="msg">事件消息实体</param>
    /// <returns></returns>
    public override async Task<int> OnEventInvitedInGroupAsync(EventInvitedInGroupMsg msg)
    {
        return await Task.FromResult(0);
    }
}