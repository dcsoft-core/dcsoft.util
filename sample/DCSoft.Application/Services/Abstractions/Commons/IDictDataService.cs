using DCSoft.Applications.Dtos.Commons;
using DCSoft.Applications.Requests.Commons;
using DCSoft.Applications.Responses.Commons;
using DCSoft.Data.Queries.Commons;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Util.Aop;
using Util.Applications;

namespace DCSoft.Applications.Services.Abstractions.Commons
{
    /// <summary>
    /// 字典数据服务
    /// </summary>
    public interface IDictDataService : IService
    {
        /// <summary>
        /// 创建字典
        /// </summary>
        /// <param name="request">创建字典参数</param>
        Task<Guid> CreateAsync([NotNull] CreateDictDataRequest request);

        /// <summary>
        /// 修改字典
        /// </summary>
        /// <param name="request">字典参数</param>
        Task UpdateAsync([NotNull] DictDataDto request);

        /// <summary>
        /// 获取字典列表数据
        /// </summary>
        /// <param name="code">字典编码</param>
        /// <param name="query">查询参数</param>
        /// <returns></returns>
        Task<List<DictDataResponse>> GetListByCodeAsync(string code, DictDataQuery query);

        /// <summary>
        /// 获取字典树形数据
        /// </summary>
        /// <param name="code">字典编码</param>
        /// <param name="query">查询参数</param>
        /// <returns></returns>
        Task<List<DictDataTreeResponse>> GetTreeByCodeAsync(string code, DictDataQuery query);
    }
}