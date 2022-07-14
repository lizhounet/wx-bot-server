using HZY.Infrastructure;
using HZY.Infrastructure.Filters;
using HZY.Models.DTO;
using HZY.Models.DTO.Framework;
using HZY.Services.Admin.Framework;
using HZY.Services.Admin.WxBot.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HZY.WebHost.Controllers.Public;

/// <summary>
/// 个非寻机器人
/// </summary>
[Route("api/feixunbot")]
[ApiExplorerSettings(GroupName = nameof(ApiVersions.Public))]

public class GeFeiXunBotController : ControllerBase
{
    private readonly GeFeiXunBotService _geFeiXunBotService;
    private readonly ILogger _logger;

    public GeFeiXunBotController(GeFeiXunBotService geFeiXunBotService, ILogger<GeFeiXunBotController> logger)
    {
        _geFeiXunBotService = geFeiXunBotService;
        _logger = logger;
    }

    /// <summary>
    /// 获取登录二维码
    /// </summary>
    /// <returns></returns>
    [HttpGet("login-qrcode")]
    public async Task<string> GetLoginQrCodeAsync()
    {
        return await this._geFeiXunBotService.GetLoginQrCodeAsync();
    }
    /// <summary>
    /// 检测是否扫码成功
    /// </summary>
    /// <param name="salt">获取登录二维码时的返回值</param>
    /// <returns></returns>
    [HttpGet("check-login/{salt}")]
    public async Task<string> GetLoginQrCodeAsync([FromRoute] string salt)
    {
        return await this._geFeiXunBotService.CheckLoginAsync(salt);
    }

}