using DCSoft.Apis.Base;
using DCSoft.Applications.Requests.Systems;
using DCSoft.Applications.Services.Abstractions.Systems;
using DCSoft.Data.Queries.Systems;
using DCSoft.Logging.Serilog;
using DCSoft.Web.Core.Attributes;
using Microsoft.AspNetCore.Mvc;
using Util;
using Util.Applications.Properties;
using Util.Exceptions;
using ILogger = DCSoft.Logging.Serilog.ILogger;

namespace DCSoft.Apis.Admin.Systems
{
    /// <summary>
    /// 角色控制器
    /// </summary>
    [Router("systems", "role")]
    public class RoleController : BaseController
    {
        #region 初始化控制器

        /// <summary>
        /// 初始化控制器
        /// </summary>
        /// <param name="logger">日志服务</param>
        /// <param name="roleService">角色服务</param>
        public RoleController(ILogger logger,
            IRoleService roleService)
        {
            _logger = logger;
            _roleService = roleService;
        }

        #endregion

        #region 属性定义

        /// <summary>
        /// 日志操作者
        /// </summary>
        private readonly ILogger _logger;

        /// <summary>
        /// 角色服务
        /// </summary>
        private readonly IRoleService _roleService;

        #endregion

        #region 获取全部

        /// <summary>
        /// 获取全部
        /// </summary>
        [HttpGet("all")]
        public async Task<IActionResult> GetAllAsync()
        {
            var roles = await _roleService.GetEnabledRolesAsync();
            var result = roles.OrderBy(t => t.SortId).Select(t => new Item(t.Name, t.Id)).ToList();
            return Success(result);
        }

        #endregion

        #region 创建

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="request">创建参数</param>
        [HttpPost]
        public async Task<IActionResult> CreateRoleAsync([FromBody] CreateRoleRequest request)
        {
            if (request == null)
                throw new Warning(WebApiResource.CreateRequestIsEmpty);
            var id = await _roleService.CreateAsync(request);
            _logger.Operate("用户角色", BusinessType.Insert, CurrentMethodName);
            return Success(id);
        }

        #endregion

        #region 详情

        /// <summary>
        /// 详情
        /// </summary>
        /// <param name="id">标识</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(string id)
        {
            if (id.IsEmpty())
                throw new Warning(WebApiResource.IdIsEmpty);
            var result = await _roleService.GetByIdAsync(id);
            return Success(result);
        }

        #endregion

        #region 修改

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="request">修改参数</param>
        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] UpdateRoleRequest request)
        {
            if (request == null)
                throw new Warning(WebApiResource.UpdateRequestIsEmpty);
            await _roleService.UpdateAsync(request);
            _logger.Operate("用户角色", BusinessType.Update, CurrentMethodName);
            return Success();
        }

        #endregion

        #region 删除

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id">标识</param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            if (id.IsEmpty())
                throw new Warning(WebApiResource.IdIsEmpty);
            await _roleService.DeleteAsync(id);
            _logger.Operate("用户角色", BusinessType.Delete, CurrentMethodName);
            return Success();
        }

        #endregion

        #region 批量删除

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="ids">标识列表，多个Id用逗号分隔，范例：1,2,3</param>
        [HttpPost("delete")]
        public async Task<IActionResult> BatchDeleteAsync([FromBody] string ids)
        {
            if (ids.IsEmpty())
                throw new Warning(WebApiResource.IdIsEmpty);
            await _roleService.DeleteAsync(ids);
            _logger.Operate("用户角色", BusinessType.BatchDelete, CurrentMethodName);
            return Success();
        }

        #endregion

        #region 分页查询

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <remarks> 
        /// 调用范例: 
        /// GET
        /// /api/customer?name=a
        /// </remarks>
        /// <param name="query">查询参数</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> PagerQueryAsync([FromQuery] RoleQuery query)
        {
            var result = await _roleService.PageQueryAsync(query);
            return Success(result);
        }

        #endregion
    }
}