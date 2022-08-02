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
using HZY.Domain.Services.WxBot;

namespace HZY.Controllers.Admin
{
    /// <summary>
    /// 情侣每日说 控制器
    /// </summary>
    [ControllerDescriptor(MenuId = "42", DisplayName = "情侣每日说")]
    [ApiExplorerSettings(GroupName = nameof(ApiVersions.WxBot))]
    public class WxSayEveryDayController : AdminBaseController<WxSayEveryDayService>
    {
        private readonly ContentSendService _contentSendService;
        public WxSayEveryDayController(WxSayEveryDayService defaultService,ContentSendService contentSendService) 
            : base(defaultService)
        {
            _contentSendService = contentSendService;
        }
        
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="size">size</param>
        /// <param name="page">page</param>
        /// <param name="search">search</param>
        /// <returns></returns>
        [ActionDescriptor(AdminFunctionConsts.Function_Display, DisplayName = "查看数据")]
        [HttpPost("FindList/{size}/{page}")]
        public Task<PagingView> FindListAsync([FromRoute] int size, [FromRoute] int page, [FromBody] WxSayEveryDay search)
        {
            return this._defaultService.FindListAsync(page, size, search);
        }

        /// <summary>
        /// 根据id数组删除
        /// </summary>
        /// <param name="ids">ids</param>
        /// <returns></returns>
        [ActionDescriptor(AdminFunctionConsts.Function_Delete, DisplayName = "删除数据")]
        [HttpPost("DeleteList")]
        public async Task<bool> DeleteListAsync([FromBody] List<Guid> ids)
        {
            await this._defaultService.DeleteListAsync(ids);
            return true;
        }

        /// <summary>
        /// 查询表单数据
        /// </summary>
        /// <param name="id">id</param>
        /// <returns></returns>
        [ActionDescriptor(DisplayName = "查看表单")]
        [HttpGet("FindForm/{id?}")]
        public Task<Dictionary<string, object>> FindFormAsync([FromRoute] Guid id)
        {
            return this._defaultService.FindFormAsync(id);
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="form">form</param>
        /// <returns></returns>
        [ActionDescriptor(DisplayName = "保存/编辑数据")]
        [ApiCheckModel]
        [HttpPost("SaveForm")]
        public Task<WxSayEveryDay> SaveFormAsync([FromBody] WxSayEveryDay form)
        {
            return this._defaultService.SaveFormAsync(form);
        }
        /// <summary>
        /// 启动情侣每日说
        /// </summary>
        /// <param name="everyDayId">每日说id</param>
        /// <returns></returns>
        [ActionDescriptor(DisplayName = "启动情侣每日说")]
        [HttpPost("start/{everyDayId}")]
        public async Task<bool> StartSayEveryDayAsync([FromRoute] Guid everyDayId)
        {
            return await this._defaultService.StartSayEveryDayAsync(everyDayId);
        }
        /// <summary>
        /// 执行情侣每日说
        /// </summary>
        /// <param name="everyDayId">每日说id</param>
        /// <returns></returns>
        [ActionDescriptor(DisplayName = "执行情侣每日说")]
        [HttpPost("exec/{everyDayId}")]
        public async Task<bool> ExecSayEveryDayAsync([FromRoute] Guid everyDayId)
        {
            await this._contentSendService.ExecSayEveryDayAsync(everyDayId);
            return true;
        }
        /// <summary>
        /// 停止情侣每日说
        /// </summary>
        /// <param name="everyDayId">每日说id</param>
        /// <returns></returns>
        [ActionDescriptor(DisplayName = "停止情侣每日说")]
        [HttpPost("stop/{everyDayId}")]
        public async Task<bool> StopSayEveryDayAsync([FromRoute] Guid everyDayId)
        {
            return await this._defaultService.StopSayEveryDayAsync(everyDayId);
        }
        /// <summary>
        /// 查询情侣每日说运行日志
        /// </summary>
        /// <param name="everyDayId">每日说id</param>
        /// <returns></returns>
        [ActionDescriptor(DisplayName = "查询情侣每日说运行日志")]
        [HttpPost("queryRunLog/{everyDayId}")]
        public async Task<List<string>> QueryRunLogAsync([FromRoute] Guid everyDayId)
        {
            return await this._defaultService.QueryRunLogAsync(everyDayId);
        }

    }
}