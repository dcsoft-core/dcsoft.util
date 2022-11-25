using DCSoft.Apis.Base;
using DCSoft.Applications.Dtos.Systems;
using DCSoft.Applications.Extensions.Systems;
using DCSoft.Applications.Requests.Systems;
using DCSoft.Applications.Responses.Systems;
using DCSoft.Applications.Services.Abstractions.Systems;
using DCSoft.Data.Queries.Systems;
using DCSoft.Logging.Serilog;
using DCSoft.Web.Core.Attributes;
using Microsoft.AspNetCore.Mvc;
using ILogger = DCSoft.Logging.Serilog.ILogger;

namespace DCSoft.Apis.Admin.Systems
{
    /// <summary>
    /// 资源控制器
    /// </summary>
    [Router("systems", "module")]
    public class ModuleController : BaseTreeController<ModuleDto, ResourceQuery>
    {
        /// <summary>
        /// 初始化资源控制器
        /// </summary>
        /// <param name="service">模块服务</param>
        /// <param name="permissionService">权限服务</param>
        /// <param name="logger">日志服务</param>
        public ModuleController(IModuleService service,
            IPermissionService permissionService,
            ILogger logger) : base(service)
        {
            _moduleService = service;
            _permissionService = permissionService;
            _logger = logger;
        }

        /// <summary>
        /// 模块服务
        /// </summary>
        private readonly IModuleService _moduleService;

        /// <summary>
        /// 权限服务
        /// </summary>
        private readonly IPermissionService _permissionService;

        /// <summary>
        /// 日志操作者
        /// </summary>
        private readonly ILogger _logger;

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="request">创建参数</param>
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CreateModuleRequest request)
        {
            var id = await _moduleService.CreateAsync(request);
            _logger.Operate("功能模块", BusinessType.Insert, CurrentMethodName);
            return Success(id);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="request">修改参数</param>
        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] ModuleDto request)
        {
            await _moduleService.UpdateAsync(request);
            _logger.Operate("功能模块", BusinessType.Update, CurrentMethodName);
            return Success();
        }

        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public new async Task<IActionResult> GetAsync(string id)
        {
            return await base.GetAsync(id);
        }

        /// <summary>
        /// 删除，注意：该方法用于删除单个实体
        /// </summary>
        /// <param name="id">标识</param>
        [HttpDelete("{id}")]
        public new async Task<IActionResult> DeleteAsync(string id)
        {
            return await base.DeleteAsync(id);
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="ids">标识列表，多个Id用逗号分隔，范例：1,2,3</param>
        [HttpPost("delete")]
        public async Task<IActionResult> BatchDeleteAsync([FromBody] string ids)
        {
            return await base.DeleteAsync(ids);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpGet]
        public new async Task<IActionResult> QueryAsync([FromQuery] ResourceQuery query)
        {
            return await base.QueryAsync(query);
        }

        /// <summary>
        /// 获取权限
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpGet("permission")]
        public async Task<IActionResult> GetPermissionAsync([FromQuery] ResourceQuery query)
        {
            query.Order = "Path";
            var module = await _moduleService.QueryAsync(query);
            var resIds = await _permissionService.GetResourceIdsAsync(new PermissionQuery()
            {
                ApplicationId = query.ApplicationId,
                RoleId = query.RoleId,
                IsDeny = false
            });
            var result = module.ToPermissionData(resIds.ToList());
            return Success(result);
        }

        /// <summary>
        /// 获取树形数据
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpGet("tree")]
        public async Task<IActionResult> GetTreeAsync([FromQuery] ResourceQuery query)
        {
            query.Order = "Path";
            var module = await _moduleService.QueryAsync(query);
            var result = module.ToTreeData().ToList();
            var moduleTreeResponse = new ModuleTreeResponse
            {
                Nodes = result,
                ExpandedKeys = new[] { result[0].Id }
            };
            return Success(moduleTreeResponse);
        }
    }
}