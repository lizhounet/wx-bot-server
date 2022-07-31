using HZY.EFCore.Aop;
using HZY.EFCore.Repositories.Admin.Core;
using HZY.Infrastructure;
using HZY.Infrastructure.ApiResultManage;
using HZY.Infrastructure.Email;
using HZY.Infrastructure.Token;
using HZY.Models.BO;
using HZY.Models.Consts;
using HZY.Models.DTO.Framework;
using HZY.Models.Entities;
using HZY.Models.Entities.Framework;
using Masuit.Tools;
using Masuit.Tools.Strings;
using Microsoft.Extensions.Caching.Memory;

namespace HZY.Domain.Services.Accounts.Impl;

/// <summary>
/// 当前登录账户服务
/// </summary>
public class AccountDomainServiceImpl : IAccountDomainService
{
    private readonly string AccountInfoCacheName = "AccountInfo";
    private readonly AccountInfo _accountInfo;
    private readonly AppConfiguration _appConfiguration;
    private readonly TokenService _tokenService;
    private readonly IAdminRepository<SysOrganization> _sysOrganizationRepository;
    private readonly IAdminRepository<SysUser> _sysUserRepository;
    private readonly IMemoryCache _memoryCache;
    private readonly IAdminRepository<SysUserRole> _sysUserRoleRepository;
    private readonly IAdminRepository<SysRole> _sysRoleRepository;
    private readonly IAdminRepository<SysPost> _sysPostRepository;
    private readonly IAdminRepository<SysUserPost> _sysUserPostRepository;
    private readonly IAdminRepository<WxBotConfig> _wxBotConfigRepository;
    private readonly EmailService _emailService;

    public AccountDomainServiceImpl(IAdminRepository<SysUser> sysUserRepository,
        IAdminRepository<SysOrganization> sysOrganizationRepository,
        AppConfiguration appConfiguration,
        TokenService tokenService,
        IMemoryCache memoryCache,
        IAdminRepository<SysUserRole> sysUserRoleRepository,
        IAdminRepository<SysRole> sysRoleRepository,
        IAdminRepository<SysPost> sysPostRepository,
        IAdminRepository<SysUserPost> sysUserPostRepository,
        EmailService emailService,
        IAdminRepository<WxBotConfig> wxBotConfigRepository)
    {
        _sysUserRepository = sysUserRepository;
        _appConfiguration = appConfiguration;
        _tokenService = tokenService;
        _sysOrganizationRepository = sysOrganizationRepository;
        _memoryCache = memoryCache;
        _sysUserRoleRepository = sysUserRoleRepository;
        _sysRoleRepository = sysRoleRepository;
        _sysPostRepository = sysPostRepository;
        _sysUserPostRepository = sysUserPostRepository;
        _emailService = emailService;
        this._accountInfo = this.FindAccountInfoByToken();
        _wxBotConfigRepository = wxBotConfigRepository;
    }

    /// <summary>
    /// 根据用户信息获取 Account 对象
    /// </summary>
    /// <returns></returns>
    private AccountInfo FindAccountInfoByToken()
    {
        var id = _tokenService.GetAccountIdByToken();

        if (id == Guid.Empty || id == default)
        {
            return default;
        }

        var accountInfo = this.GetCacheAccountInfoById(id.ToString());

        if (accountInfo != null)
        {
            return accountInfo;
        }

        var sysUser = this._sysUserRepository.FindById(id);
        if (sysUser == null) return default;
        var sysRoles = (
            from sysUserRole in this._sysUserRoleRepository.Select
            from sysRole in this._sysRoleRepository.Select.Where(w => w.Id == sysUserRole.RoleId).DefaultIfEmpty()
            where sysUserRole.UserId == id
            select sysRole
            ).ToList();

        var sysPosts = (
            from sysUserPost in this._sysUserPostRepository.Select
            from sysPost in this._sysPostRepository.Select.Where(w => w.Id == sysUserPost.PostId).DefaultIfEmpty()
            where sysUserPost.UserId == id
            select sysPost
            ).ToList();

        var sysOrganization = this._sysOrganizationRepository.FindById(sysUser.OrganizationId);

        accountInfo = new AccountInfo();
        accountInfo = sysUser.MapTo<SysUser, AccountInfo>();
        accountInfo.IsAdministrator = sysRoles.Any(w => w.Id == this._appConfiguration.Configs.AdminRoleId);
        accountInfo.SysRoles = sysRoles;
        accountInfo.SysPosts = sysPosts;
        accountInfo.SysOrganization = sysOrganization;
        //缓存
        return this.SetCacheByAccountInfo(accountInfo);
    }

    private WxBotConfig FindWxBotConfigByToken()
    {
        //获取用户id
        var id = _tokenService.GetAccountIdByToken();
        if (id == Guid.Empty || id == default)
        {
            return default;
        }
        //先取缓存
        WxBotConfig wxBotConfig = _memoryCache.Get<WxBotConfig>(string.Format(CacheKeyConsts.WxBotConfigKey, id));
        if (wxBotConfig != null) return wxBotConfig;
        wxBotConfig = _wxBotConfigRepository.Find(w => w.ApplicationToken == id.ToString());
        if (wxBotConfig == null) return default;
        //设置缓存
        return _memoryCache.Set(string.Format(CacheKeyConsts.WxBotConfigKey, id), wxBotConfig, DateTime.Now.AddHours(1));
    }

    /// <summary>
    /// 获取当前登录账户信息
    /// </summary>
    /// <returns></returns>
    public virtual AccountInfo GetAccountInfo() => this._accountInfo ?? FindAccountInfoByToken();
    /// <summary>
    /// 获取当前个人小助手配置信息
    /// </summary>
    /// <returns></returns>
    public virtual WxBotConfig GetWxBotConfig() => FindWxBotConfigByToken();

    /// <summary>
    /// 检查账户 登录信息 并返回 token
    /// </summary>
    /// <param name="name"></param>
    /// <param name="password"></param>
    /// <param name="code"></param>
    /// <returns></returns>
    public virtual async Task<string> CheckAccountAsync(string name, string password, string code)
    {
        if (string.IsNullOrWhiteSpace(name))
            MessageBox.Show("请输入账户名!");
        if (string.IsNullOrWhiteSpace(password))
            MessageBox.Show("请输入密码！");
        // if (string.IsNullOrWhiteSpace(code))
        //  MessageBox.Show("请输入验证码!");
        var sysUser = await this._sysUserRepository.FindAsync(w => w.LoginName == name);
        if (sysUser == null)
        {
            MessageBox.Show("账户或者密码错误!");
        }

        if (sysUser.Password.Trim() != Tools.Md5Encrypt(password))
        {
            MessageBox.Show("账户或者密码错误!");
        }
        if (sysUser.UserState != 1)
        {
            MessageBox.Show("您的账号已停用或未激活!");
        }
        //string code = Tools.GetCookie("loginCode");
        //if (string.IsNullOrEmpty(code)) throw new MessageBox("验证码失效");
        //if (!code.ToLower().Equals(loginCode.ToLower())) throw new MessageBox("验证码不正确");

        return _tokenService.CreateTokenByAccountId(sysUser.Id);
    }

    [Transactional]
    public virtual async Task<UserRegisterDto> RegisterAsync(UserRegisterDto userRegisterDto)
    {
        if (this._sysUserRepository.Any(r => r.Email.Equals(userRegisterDto.Email)))
        {
            MessageBox.Show("邮箱已经被注册!");
        }
        //验证验证码
        string code = _memoryCache.Get<string>(string.Format(CacheKeyConsts.EmailVerifyCodeCacheKey, userRegisterDto.Email));
        if (string.IsNullOrEmpty(code))
        {
            MessageBox.Show("验证码已失效,请重新发送!");
        }
        if (!userRegisterDto.VerifyCode.Equals(code))
        {
            MessageBox.Show("验证码不正确!");
        }
        //保存账号
        SysUser user = new()
        {
            DeleteLock = false,
            Name = userRegisterDto.Name,
            Email = userRegisterDto.Email,
            LoginName = userRegisterDto.Email,
            Password = Tools.Md5Encrypt(userRegisterDto.Password),
            UserState = 1,
            OrganizationId = 1
        };
        SysUser sysUser = await _sysUserRepository.InsertAsync(user);
        //处理默认角色岗位
        SysRole sysRole = await _sysRoleRepository.FindAsync(w => w.Name == "微信平台管理员");
        SysUserRole sysUserRole = new()
        {
            Id = Guid.NewGuid(),
            RoleId = sysRole.Id,
            UserId = sysUser.Id
        };
        await this._sysUserRoleRepository.InsertAsync(sysUserRole);
        //处理岗位
        SysPost sysPost = await _sysPostRepository.FindAsync(w => w.Name == "普通员工");
        SysUserPost sysUserPost = new()
        {
            Id = Guid.NewGuid(),
            PostId = sysPost.Id,
            UserId = sysUser.Id
        };
        await this._sysUserPostRepository.InsertAsync(sysUserPost);
        return userRegisterDto;


    }
    public void SendEmailVerifyCodeAsync(SendEmailVerifyCodeDto emailVerifyCodeDto)
    {
        string email = emailVerifyCodeDto.Email;
        if (!email.MatchEmail(true).isMatch) { MessageBox.Show("邮箱格式不正确!"); }

        //生成验证码
        string code = new NumberFormater(62).ToString(new Random().Next(100000, int.MaxValue));
        string subject = "【个微小助手】请查收您的邮箱验证码";
        string body = $"您的邮箱为：{email}，验证码为：{code} \n验证码的有效期为5分钟，请在有效期内输入！";
        _emailService.SendEmail(email, subject, body);
        _memoryCache.Set(string.Format(CacheKeyConsts.EmailVerifyCodeCacheKey, email), code, DateTime.Now.AddMinutes(5));
    }

    /// <summary>
    /// 修改密码
    /// </summary>
    /// <param name="oldPassword"></param>
    /// <param name="newPassword"></param>
    /// <returns></returns>
    public virtual async Task<int> ChangePasswordAsync(string oldPassword, string newPassword)
    {
        if (string.IsNullOrEmpty(oldPassword)) MessageBox.Show("旧密码不能为空！");
        if (string.IsNullOrEmpty(newPassword)) MessageBox.Show("新密码不能为空！");
        oldPassword = Tools.Md5Encrypt(oldPassword);
        var sysUser = await this._sysUserRepository.FindByIdAsync(this.GetAccountInfo().Id);
        if (sysUser.Password != oldPassword) MessageBox.Show("旧密码不正确！");
        sysUser.Password = Tools.Md5Encrypt(newPassword);
        this.DeleteCacheAccountInfoById(sysUser.Id.ToString());
        return await this._sysUserRepository.UpdateAsync(sysUser);
    }

    /// <summary>
    /// 修改用户基础信息
    /// </summary>
    /// <param name="form"></param>
    /// <returns></returns>
    public virtual async Task<SysUser> ChangeUserAsync(SysUser form)
    {
        var sysUser = await _sysUserRepository.FindByIdAsync(_accountInfo.Id);
        sysUser.Name = form.Name;
        sysUser.LoginName = form.LoginName;
        sysUser.Email = form.Email;
        sysUser.Phone = form.Phone;
        this.DeleteCacheAccountInfoById(sysUser.Id.ToString());
        return await _sysUserRepository.InsertOrUpdateAsync(sysUser);
    }

    /// <summary>
    /// 根据账户信息缓存
    /// </summary>
    /// <param name="accountInfo"></param>
    /// <returns></returns>
    public virtual AccountInfo SetCacheByAccountInfo(AccountInfo accountInfo)
    {
        //缓存 1 小时
        return _memoryCache.Set(GetCacheKeyById(accountInfo.Id.ToString()), accountInfo, DateTime.Now.AddHours(1));
    }

    /// <summary>
    /// 获取缓存中的账户信息
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public virtual AccountInfo GetCacheAccountInfoById(string id)
    {
        //缓存
        return _memoryCache.Get<AccountInfo>(GetCacheKeyById(id));
    }

    /// <summary>
    /// 删除缓存账户信息 根据id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public virtual bool DeleteCacheAccountInfoById(string id)
    {
        _memoryCache.Remove(GetCacheKeyById(id));
        this.DeleteCacheWxBotConfigById(id);
        return true;
    }

    /// <summary>
    /// 删除个微小助手基础配置 根据id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public virtual bool DeleteCacheWxBotConfigById(string id)
    {
        _memoryCache.Remove(string.Format(CacheKeyConsts.WxBotConfigKey, id));
        return true;
    }

    #region 私有方法

    private string GetCacheKeyById(string id) => AccountInfoCacheName + id;





    #endregion
}