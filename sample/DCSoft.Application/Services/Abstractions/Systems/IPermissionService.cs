using DCSoft.Applications.Requests.Systems;
using DCSoft.Data.Queries.Systems;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Util.Applications;
using DCSoft.Applications.Dtos.Systems;

namespace DCSoft.Applications.Services.Abstractions.Systems
{
    /// <summary>
    /// 权限服务
    /// </summary>
    public interface IPermissionService : ICrudService<PermissionDto, PermissionQuery>
    {
        /// <summary>
        /// 权限验证
        /// </summary>
        /// <param name="api"></param>
        /// <param name="httpMethod"></param>
        /// <returns></returns>
        Task<bool> Validate(string api, string httpMethod);

        /// <summary>
        /// 获取资源标识列表
        /// </summary>
        /// <param name="query">权限参数</param>
        Task<IEnumerable<Guid>> GetResourceIdsAsync(PermissionQuery query);

        /// <summary>
        /// 保存权限
        /// </summary>
        /// <param name="request">参数</param>
        Task SaveAsync(SavePermissionRequest request);
    }
}