﻿using HZY.Domain.Services.Accounts;
using HZY.Infrastructure;
using HZY.Infrastructure.Controllers;
using HZY.Infrastructure.Filters;
using HZY.Models.DTO;
using HZY.Models.DTO.Framework;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace HZY.WebHost.Controllers.Public
{
    /// <summary>
    /// 账户控制器
    /// </summary>
    [ApiResultFilter]
    [ApiController]
    [Route("api/[controller]")]
    [ApiExplorerSettings(GroupName = nameof(ApiVersions.Public))]
    public class AccountController : ApiBaseController
    {
        private const string tokenType = "Bearer ";
        private readonly IAccountDomainService _accountService;

        public AccountController(IAccountDomainService accountService)
        {
            _accountService = accountService;
        }

        /// <summary>
        /// 检查帐户并获取 token
        /// </summary>
        /// <param name="authUserDto">Dto</param>
        /// <returns></returns>
        [HttpPost("Check")]
        public async Task<dynamic> CheckAsync([FromBody] AuthUserFormDto authUserDto)
        {
            var token = await this._accountService
                .CheckAccountAsync(authUserDto.UserName, authUserDto.UserPassword, authUserDto.LoginCode);
            return new { token = token, tokenType };
        }

        /// <summary>
        /// 注册用户
        /// </summary>
        /// <param name="userRegisterDto">Dto</param>
        /// <returns></returns>
        [HttpPost("register")]
        [ApiCheckModel]
        public async Task<UserRegisterDto> RegisterAsync([FromBody] UserRegisterDto userRegisterDto) => await this._accountService
                .RegisterAsync(userRegisterDto);

        /// <summary>
        /// 发送邮箱验证码
        /// </summary>
        /// <param name="emailVerifyCodeDto">发送邮箱验证码 dto</param>
        /// <returns></returns>
        [HttpPost("register-verifyCode")]
        [RequestLimitFilter(Duration = 60, LimitCount = 1, Message = "请60s后再发送")]
        public bool SendEmailVerifyCodeAsync([FromBody] SendEmailVerifyCodeDto emailVerifyCodeDto) {
            this._accountService
                    .SendEmailVerifyCodeAsync(emailVerifyCodeDto);
            return true;
        }
    }
}
