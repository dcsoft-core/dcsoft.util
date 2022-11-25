using DCSoft.Apis.Base;
using DCSoft.Applications.Dtos.Commons;
using DCSoft.Applications.Extensions.Commons;
using DCSoft.Applications.Requests.Commons;
using DCSoft.Applications.Responses.Commons;
using DCSoft.Applications.Services.Abstractions.Commons;
using DCSoft.Data.Queries.Commons;
using DCSoft.Logging.Serilog;
using DCSoft.Web.Core.Attributes;
using Microsoft.AspNetCore.Mvc;
using Util.Applications.Properties;
using Util.Exceptions;
using ILogger = DCSoft.Logging.Serilog.ILogger;

namespace DCSoft.Apis.Admin.Commons
{
    /// <summary>
    /// 组织机构控制器
    /// </summary>
    [Router("commons", "department")]
    public class DepartmentController : BaseTreeController<DepartmentDto, DepartmentQuery>
    {
        /// <summary>
        /// 初始化组织机构控制器
        /// </summary>
        /// <param name="service">部门服务</param>
        /// <param name="queryService">部门查询服务</param>
        /// <param name="logger">日志服务</param>
        public DepartmentController(IDepartmentService service, 
            IQueryDepartmentService queryService,
            ILogger logger) : base(queryService)
        {
            _queryDepartmentService = queryService;
            _departmentService = service;
            _logger = logger;
        }

        /// <summary>
        /// 查询部门服务
        /// </summary>
        private readonly IQueryDepartmentService _queryDepartmentService;

        /// <summary>
        /// 部门服务
        /// </summary>
        private readonly IDepartmentService _departmentService;

        /// <summary>
        /// 日志操作者
        /// </summary>
        private readonly ILogger _logger;

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="request">创建参数</param>
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CreateDepartmentRequest request)
        {
            if (request == null)
                throw new Warning(WebApiResource.CreateRequestIsEmpty);
            var id = await _departmentService.CreateAsync(request);
            _logger.Operate("组织机构", BusinessType.Insert, CurrentMethodName);
            return Success(id);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="request">修改参数</param>
        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] DepartmentDto request)
        {
            if (request == null)
                throw new Warning(WebApiResource.UpdateRequestIsEmpty);
            await _departmentService.UpdateAsync(request);
            _logger.Operate("组织机构", BusinessType.Update, CurrentMethodName);
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
        /// 查询
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpGet]
        public new async Task<IActionResult> QueryAsync([FromQuery] DepartmentQuery query)
        {
            return await base.QueryAsync(query);
        }

        /// <summary>
        /// 获取树形数据
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpGet("tree")]
        public async Task<IActionResult> GetTree([FromQuery] DepartmentQuery query)
        {
            query.Order = "Code";
            var list = await _queryDepartmentService.QueryAsync(query);
            var department = list.ToTreeData();
            var result = new DepartmentTreeResponse
            {
                Nodes = department,
                ExpandedKeys = new[] { department?[0].Id }
            };
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