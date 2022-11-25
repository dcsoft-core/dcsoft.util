using DCSoft.Apis.Base;
using DCSoft.Applications.Dtos.Commons;
using DCSoft.Applications.Requests.Commons;
using DCSoft.Applications.Services.Abstractions.Commons;
using DCSoft.Data.Queries.Commons;
using DCSoft.Logging.Serilog;
using DCSoft.Web.Core.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Util.Applications.Properties;
using Util.Exceptions;
using ILogger = DCSoft.Logging.Serilog.ILogger;

namespace DCSoft.Apis.Admin.Commons
{
    /// <summary>
    /// 字典数据控制器
    /// </summary>
    [Router("commons", "dict/data")]
    public class DictDataController : BaseTreeController<DictDataDto, DictDataQuery>
    {
        /// <summary>
        /// 初始化字典数据控制器
        /// </summary>
        /// <param name="service">字典数据服务</param>
        /// <param name="queryService">字典数据查询服务</param>
        /// <param name="logger">日志服务</param>
        public DictDataController(IDictDataService service,
            IQueryDictDataService queryService,
            ILogger logger) : base(queryService)
        {
            _service = service;
            _queryService = queryService;
            _logger = logger;
        }

        /// <summary>
        /// 字典数据服务
        /// </summary>
        private readonly IDictDataService _service;

        /// <summary>
        /// 字典数据查询服务
        /// </summary>
        private readonly IQueryDictDataService _queryService;

        /// <summary>
        /// 日志操作者
        /// </summary>
        private readonly ILogger _logger;

        /// <summary>
        /// 创建字典数据
        /// </summary>
        /// <param name="request">创建参数</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateDictDataAsync([FromBody] CreateDictDataRequest request)
        {
            if (request == null)
                throw new Warning(WebApiResource.CreateRequestIsEmpty);
            var id = await _service.CreateAsync(request);
            _logger.Operate("字典数据", BusinessType.Insert, CurrentMethodName);
            return Success(id);
        }

        /// <summary>
        /// 修改字典数据
        /// </summary>
        /// <param name="request">修改参数</param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> UpdateDictDataAsync([FromBody] DictDataDto request)
        {
            if (request == null)
                throw new Warning(WebApiResource.UpdateRequestIsEmpty);
            await _service.UpdateAsync(request);
            _logger.Operate("字典数据", BusinessType.Update, CurrentMethodName);
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
        /// 列表查询
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpGet]
        public new async Task<IActionResult> QueryAsync([FromQuery] DictDataQuery query)
        {
            return await base.QueryAsync(query);
        }

        /// <summary>
        /// 获取列表数据
        /// </summary>
        /// <param name="code">字典编码</param>
        /// <param name="query">查询参数</param>
        /// <returns></returns>
        [HttpGet("list/{code}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetListByCodeAsync(string code, [FromQuery] DictDataQuery query)
        {
            var result = await _service.GetListByCodeAsync(code, query);
            return Success(result);
        }

        /// <summary>
        /// 获取树形数据
        /// </summary>
        /// <param name="code">字典编码</param>
        /// <param name="query">查询参数</param>
        /// <returns></returns>
        [HttpGet("tree/{code}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetTreeByCodeAsync(string code, [FromQuery] DictDataQuery query)
        {
            var result = await _service.GetTreeByCodeAsync(code, query);
            return Success(result);
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
    }
}