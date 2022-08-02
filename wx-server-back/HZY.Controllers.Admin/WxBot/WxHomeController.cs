using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using HZY.Models.DTO.WxBot;
using HZY.Domain.Services.WxBot;
using HZY.Infrastructure;
using HZY.Infrastructure.Controllers;
using HZY.Infrastructure.Permission.Attributes;

namespace HZY.Controllers.Admin
{
    /// <summary>
    /// 首页 控制器
    /// </summary>
    [ApiExplorerSettings(GroupName = nameof(ApiVersions.WxBot))]
    [Route("api/admin/home")]
    public class WxHomeController : AdminBaseController<WxAccountService>
    {
        public WxHomeController(WxAccountService defaultService) 
            : base(defaultService)
        {
        }
       
        /// <summary>
        /// 获取微信登录二维码
        /// </summary>
        /// <returns></returns>
        [ActionDescriptor(DisplayName = "获取微信登录二维码")]
        [HttpGet("login-qrcode/{applictionToken}")]
        public async Task<string> GetLoginQrCodeAsync([FromRoute]string applictionToken) 
            => await this._defaultService.GetLoginQrCodeAsync(applictionToken);

        /// <summary>
        /// 获取微信登录用户信息
        /// </summary>
        /// <returns></returns>
        [ActionDescriptor(DisplayName = "获取微信用户信息")]
        [HttpGet("user-info/{applictionToken}")]
        public async Task<WxUserInfoDTO> GetWxUserInfoAsync([FromRoute] string applictionToken) => 
            await this._defaultService.GetWxUserInfoByApplictionTokenAsync(applictionToken);

    }
}