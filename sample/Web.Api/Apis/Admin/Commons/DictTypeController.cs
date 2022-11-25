using DCSoft.Apis.Base;
using DCSoft.Applications.Dtos.Commons;
using DCSoft.Applications.Services.Abstractions.Commons;
using DCSoft.Data.Queries.Commons;
using Microsoft.AspNetCore.Mvc;

namespace DCSoft.Apis.Admin.Commons
{
    /// <summary>
    /// 字典类型控制器
    /// </summary>
    public class DictTypeController : BaseCrudController<DictTypeDto, DictTypeQuery>
    {
        /// <summary>
        /// 初始化字典类型控制器
        /// </summary>
        /// <param name="service">字典类型服务</param>
        /// <param name="logger">日志服务</param>
        public DictTypeController(IDictTypeService service,
            ILogger logger) : base(service)
        {
            _service = service;
            _logger = logger;
        }

        /// <summary>
        /// 字典类型服务
        /// </summary>
        private readonly IDictTypeService _service;

        /// <summary>
        /// 日志操作者
        /// </summary>
        private readonly ILogger _logger;

        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="id">标识</param>
        [HttpGet("{id}")]
        public new async Task<IActionResult> GetAsync(string id)
        {
            return await base.GetAsync(id);
        }

        /// <summary>
        /// 列表查询
        /// </summary>
        /// <param name="query">查询参数</param>
        [HttpGet("query")]
        public new async Task<IActionResult> QueryAsync([FromQuery] DictTypeQuery query)
        {
            return await base.QueryAsync(query);
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="query">查询参数</param>
        [HttpGet]
        public new async Task<IActionResult> PageQueryAsync([FromQuery] DictTypeQuery query)
        {
            return await base.PageQueryAsync(query);
        }

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="request">创建参数</param>
        [HttpPost]
        public new async Task<IActionResult> CreateAsync(DictTypeDto request)
        {
            return await base.CreateAsync(request);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="id">标识</param>
        /// <param name="request">修改参数</param>
        [HttpPut("{id?}")]
        public new async Task<IActionResult> UpdateAsync(string id, DictTypeDto request)
        {
            return await base.UpdateAsync(id, request);
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