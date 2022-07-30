using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using HZY.EFCore.PagingViews;
using HZY.Infrastructure;
using HZY.Infrastructure.Controllers;
using HZY.Infrastructure.Filters;
using HZY.Infrastructure.Permission;
using HZY.Infrastructure.Permission.Attributes;
using HZY.Models.BO;
using HZY.Models.Consts;
using HZY.Models.DTO;
using HZY.Models.DTO.Framework;
using HZY.Models.Entities.Framework;
using HZY.Services.Admin.Framework;
using HZY.Services.Admin;
using HZY.Models.Entities;
using HZY.Models.DTO.WxBot;

namespace HZY.Controllers.Admin
{
    /// <summary>
    /// 首页 控制器
    /// </summary>
    [ApiExplorerSettings(GroupName = nameof(ApiVersions.WxBot))]
    [Route("api/admin/home")]
    public class WxHomeController : AdminBaseController<WxHomeService>
    {
        public WxHomeController(WxHomeService defaultService) 
            : base(defaultService)
        {

        }
       
        /// <summary>
        /// 获取微信登录二维码
        /// </summary>
        /// <returns></returns>
        [ActionDescriptor(DisplayName = "获取微信登录二维码")]
        [HttpGet("login-qrcode")]
        public async Task<string> GetLoginQrCodeAsync() 
            => await this._defaultService.GetLoginQrCodeAsync();

        /// <summary>
        /// 获取微信登录用户信息
        /// </summary>
        /// <returns></returns>
        [ActionDescriptor(DisplayName = "获取微信用户信息")]
        [HttpGet("user-info")]
        public async Task<WxUserInfoDTO> GetWxUserInfoAsync() => 
            await this._defaultService.GetWxUserInfoAsync();

    }
}