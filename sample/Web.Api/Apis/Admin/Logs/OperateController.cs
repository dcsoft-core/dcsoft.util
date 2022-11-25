using DCSoft.Apis.Base;
using DCSoft.Applications.Services.Abstractions.Logs;
using DCSoft.Data.Queries.Logs;
using DCSoft.Web.Core.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace DCSoft.Apis.Admin.Logs
{
    /// <summary>
    /// 操作日志控制器
    /// </summary>
    [Router("logs", "operate-log")]
    public class OperateController : BaseController
    {
        #region 初始化控制器

        /// <summary>
        /// 初始化控制器
        /// </summary>
        /// <param name="operateService">操作日志服务</param>
        public OperateController(IOperateService operateService)
        {
            _operateService = operateService;
        }

        #endregion

        #region 属性定义

        /// <summary>
        /// 操作日志服务
        /// </summary>
        private readonly IOperateService _operateService;

        #endregion

        #region 分页查询

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="query">查询参数</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> PagerQueryAsync([FromQuery] OperateQuery query)
        {
            var result = await _operateService.PageQueryAsync(query);
            return Success(result);
        }

        #endregion
    }
}