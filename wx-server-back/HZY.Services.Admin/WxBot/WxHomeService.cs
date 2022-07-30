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

namespace HZY.Services.Admin
{
    /// <summary>
    /// 首页 服务 WxBotConfigService
    /// </summary>
    public class WxHomeService : AdminBaseService<IAdminRepository<WxBotConfig>>
    {
        private readonly AccountInfo _accountInfo;
        private readonly XyoHttpApi xyoHttpApi;
        public WxHomeService(IAdminRepository<WxBotConfig> defaultRepository,
            IAccountDomainService accountService)
            : base(defaultRepository)
        {
            this._accountInfo = accountService.GetAccountInfo();
            xyoHttpApi = new XyoHttpApi("http://127.0.0.1:10086", this._accountInfo.Id.ToString());
        }
        public async Task<string> GetLoginQrCodeAsync()
        {
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