using DCSoft.Apis.Base;
using DCSoft.Applications.Dtos.Systems;
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
    /// 声明控制器
    /// </summary>
    [Router("systems", "claim")]
    public class ClaimController : BaseController
    {
        #region 初始化控制器

        /// <summary>
        /// 初始化控制器
        /// </summary>
        /// <param name="logger">日志服务</param>
        /// <param name="claimService">声明服务</param>
        public ClaimController(ILogger logger,
            IClaimService claimService)
        {
            _logger = logger;
            _claimService = claimService;
        }

        #endregion

        #region 属性定义

        /// <summary>
        /// 日志操作者
        /// </summary>
        private readonly ILogger _logger;

        /// <summary>
        /// 声明服务
        /// </summary>
        private readonly IClaimService _claimService;

        #endregion

        #region 获取全部

        /// <summary>
        /// 获取全部
        /// </summary>
        [HttpGet("all")]
        public async Task<IActionResult> GetAllAsync()
        {
            var claims = await _claimService.GetEnabledClaimsAsync();
            var result = claims.OrderBy(t => t.SortId).Select(t => new Item(t.Name, t.Name)).ToList();
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
        public async Task<IActionResult> CreateAsync([FromBody] ClaimDto request)
        {
            if (request == null)
                throw new Warning(WebApiResource.CreateRequestIsEmpty);
            var id = await _claimService.CreateAsync(request);
            _logger.Operate("声明", BusinessType.Insert, CurrentMethodName);
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
            var result = await _claimService.GetByIdAsync(id);
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
        public async Task<IActionResult> UpdateAsync([FromBody] ClaimDto request)
        {
            if (request == null)
                throw new Warning(WebApiResource.UpdateRequestIsEmpty);
            await _claimService.UpdateAsync(request);
            _logger.Operate("声明", BusinessType.Update, CurrentMethodName);
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
            if (id.IsEmpty())
                throw new Warning(WebApiResource.IdIsEmpty);
            await _claimService.DeleteAsync(id);
            _logger.Operate("声明", BusinessType.Delete, CurrentMethodName);
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
            if (ids.IsEmpty())
                throw new Warning(WebApiResource.IdIsEmpty);
            await _claimService.DeleteAsync(ids);
            _logger.Operate("声明", BusinessType.BatchDelete, CurrentMethodName);
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
        public async Task<IActionResult> PagerQueryAsync([FromQuery] ClaimQuery query)
        {
            var result = await _claimService.PageQueryAsync(query);
            return Success(result);
        }

        #endregion
    }
}