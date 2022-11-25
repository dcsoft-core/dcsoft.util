using DCSoft.Apis.Base;
using DCSoft.Applications.Dtos.Systems;
using DCSoft.Applications.Services.Abstractions.Systems;
using DCSoft.Data.Queries.Systems;
using DCSoft.Logging.Serilog;
using DCSoft.Web.Core.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ILogger = DCSoft.Logging.Serilog.ILogger;

namespace DCSoft.Apis.Admin.Systems
{
    /// <summary>
    /// 应用程序控制器
    /// </summary>
    [Router("systems", "application")]
    public class ApplicationController : BaseController
    {
        /// <summary>
        /// 初始化应用程序控制器
        /// </summary>
        /// <param name="logger">日志服务</param>
        /// <param name="service">应用程序服务</param>
        public ApplicationController(ILogger logger, IApplicationService service)
        {
            _logger = logger;
            _applicationService = service;
        }

        /// <summary>
        /// 日志操作者
        /// </summary>
        private readonly ILogger _logger;

        /// <summary>
        /// 应用程序服务
        /// </summary>
        private readonly IApplicationService _applicationService;

        #region 获取全部

        /// <summary>
        /// 获取全部
        /// </summary>
        [HttpGet("all")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _applicationService.GetAllAsync();
            return Success(result);
        }

        #endregion

        #region 创建

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="request">创建参数</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] ApplicationDto request)
        {
            var id = await _applicationService.CreateAsync(request);
            _logger.Operate("应用程序", BusinessType.Insert, CurrentMethodName);
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
            var result = await _applicationService.GetByIdAsync(id);
            return Success(result);
        }

        #endregion

        #region 修改

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="request">修改参数</param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] ApplicationDto request)
        {
            await _applicationService.UpdateAsync(request);
            _logger.Operate("应用程序", BusinessType.Update, CurrentMethodName);
            return Success();
        }

        #endregion

        #region 删除

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id">标识</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            await _applicationService.DeleteAsync(id);
            _logger.Operate("应用程序", BusinessType.Delete, CurrentMethodName);
            return Success();
        }

        #endregion

        #region 批量删除

        /// <summary>
        /// 批量删除，注意：body参数需要添加引号，"'1,2,3'"而不是"1,2,3"
        /// </summary>
        /// <remarks>
        /// 调用范例:
        /// POST   
        /// /api/customer/delete
        /// body: "'1,2,3'"
        /// </remarks>
        /// <param name="ids">标识列表，多个Id用逗号分隔，范例：1,2,3</param>
        [HttpPost("delete")]
        public async Task<IActionResult> BatchDeleteAsync([FromBody] string ids)
        {
            await _applicationService.DeleteAsync(ids);
            _logger.Operate("应用程序", BusinessType.BatchDelete, CurrentMethodName);
            return Success();
        }

        #endregion

        #region 分页查询

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="query">查询参数</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> PagerQueryAsync([FromQuery] ApplicationQuery query)
        {
            var result = await _applicationService.PageQueryAsync(query);
            return Success(result);
        }

        #endregion
    }
}