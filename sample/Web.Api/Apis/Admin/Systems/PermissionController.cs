using DCSoft.Apis.Base;
using DCSoft.Applications.Requests.Systems;
using DCSoft.Applications.Services.Abstractions.Systems;
using DCSoft.Data.Queries.Systems;
using DCSoft.Logging.Serilog;
using DCSoft.Web.Core.Attributes;
using Microsoft.AspNetCore.Mvc;
using Util.Applications.Properties;
using Util.Exceptions;
using Util.Extras.Applications.Properties;
using ILogger = DCSoft.Logging.Serilog.ILogger;

namespace DCSoft.Apis.Admin.Systems
{
    /// <summary>
    /// 权限控制器
    /// </summary>
    [Router("systems", "permission")]
    public class PermissionController : BaseController
    {
        #region 初始化控制器

        /// <summary>
        /// 初始化控制器
        /// </summary>
        /// <param name="logger">日志服务</param>
        /// <param name="permitService">权限服务</param>
        public PermissionController(ILogger logger,
            IPermissionService permitService)
        {
            _logger = logger;
            _permissionService = permitService;
        }

        #endregion

        #region 属性定义

        /// <summary>
        /// 日志操作者
        /// </summary>
        private readonly ILogger _logger;

        /// <summary>
        /// 权限服务
        /// </summary>
        private readonly IPermissionService _permissionService;

        #endregion

        #region 获取资源标识列表

        /// <summary>
        /// 获取资源标识列表
        /// </summary>
        /// <param name="query">查询参数</param>
        [HttpGet("resourceIds")]
        public async Task<IActionResult> GetResourceIdsAsync([FromQuery] PermissionQuery query)
        {
            var result = await _permissionService.GetResourceIdsAsync(query);
            return Success(result.ToList());
        }

        #endregion

        #region 保存权限

        /// <summary>
        /// 保存权限
        /// </summary>
        /// <param name="request">参数</param>
        [HttpPost("save")]
        public async Task<IActionResult> SavePermitAsync([FromBody] SavePermissionRequest request)
        {
            if (request == null)
                throw new Warning(AppRes.RequestIsEmpty);
            await _permissionService.SaveAsync(request);
            _logger.Operate("用户权限", BusinessType.Save, CurrentMethodName);
            return Success();
        }

        #endregion
    }
}