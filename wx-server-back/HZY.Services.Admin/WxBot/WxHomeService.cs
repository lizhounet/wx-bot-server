using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;

using HzyEFCoreRepositories.Extensions;

using HZY.Domain.Services.Accounts;
using HZY.Domain.Services.Upload;
using HZY.EFCore.PagingViews;
using HZY.Infrastructure;
using HZY.Models.DTO;
using HZY.Models.Entities;
using HZY.Models.Entities.Framework;
using HZY.Services.Admin.Core;
using HZY.Services.Admin.Framework;
using HZY.EFCore.Repositories.Admin.Core;
using HZY.Models.BO;
using Microsoft.Extensions.Caching.Memory;
using HZY.Models.DTO.WxBot;
using HZY.Models.Consts;
using xYohttp_dotnet.Http;
using xYohttp_dotnet.Domain.Model.Vo;
using HzyScanDiService;

namespace HZY.Services.Admin
{
    /// <summary>
    /// 首页 服务 WxBotConfigService
    /// </summary>
    public class WxHomeService : IScopedSelfDependency
    {
        private readonly XyoHttpApi xyoHttpApi;
        public WxHomeService(IAccountDomainService accountService)
        {
            WxBotConfig wxBotConfig = accountService.GetWxBotConfig();
            xyoHttpApi = new XyoHttpApi(wxBotConfig.VlwHttpUrl, wxBotConfig.ApplicationToken);
        }
        /// <summary>
        /// 获取登录二维码
        /// </summary>
        /// <returns></returns>
        public async Task<string> GetLoginQrCodeAsync()
        {
            //退出已打开的微信
            await xyoHttpApi.ExitWeChatLoginWinAsync();
            //获取登录二维码
            string loginQrCodeBase64 = await xyoHttpApi.StartWeChatAsync();
            //设置缓存
            return "data:image/png;base64," + loginQrCodeBase64;
        }
        /// <summary>
        /// 获取微信登录用户信息
        /// </summary>
        /// <returns></returns>
        public async Task<WxUserInfoDTO> GetWxUserInfoAsync()
        {
            //获取登录的机器人列表
            GetRobotListVo robotList = await xyoHttpApi.GetRobotListAsync();
            Robot robot = robotList.Data.FirstOrDefault();
            return new WxUserInfoDTO
            {
                WxCode = robot?.WxNum,
                WxId = robot?.WxId,
                WxName = robot?.UserName,
                AvatarUrl = robot?.WxHeadImgurl
            };
        }

    }
}