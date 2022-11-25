using DCSoft.Domain.Models.Systems;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Util.Domain.Repositories;

namespace DCSoft.Domain.Repositories.Systems
{
    /// <summary>
    /// 权限仓储
    /// </summary>
    public interface IPermissionRepository : IRepository<Permission>
    {
        /// <summary>
        /// 获取资源标识列表
        /// </summary>
        /// <param name="applicationId">应用程序标识</param>
        /// <param name="roleId">角色标识</param>
        /// <param name="isDeny">是否拒绝</param>
        Task<IEnumerable<Guid>> GetResourceIdsAsync(Guid applicationId, Guid roleId, bool isDeny);

        /// <summary>
        ///获取权限标识列表
        /// </summary>
        /// <param name="roleId">角色标识</param>
        /// <param name="resourceIds">资源标识列表</param>
        Task<IEnumerable<Guid>> GetPermissionIdsAsync(Guid roleId, List<Guid> resourceIds);

        /// <summary>
        /// 移除权限
        /// </summary>
        /// <param name="roleId">角色标识</param>
        /// <param name="resourceIds">资源标识列表</param>
        Task RemoveAsync(Guid roleId, List<Guid> resourceIds);

        /// <summary>
        /// 保存权限
        /// </summary>
        /// <param name="applicationId">应用程序标识</param>
        /// <param name="roleId">角色标识</param>
        /// <param name="resourceIds">资源标识列表</param>
        /// <param name="isDeny">是否拒绝</param>
        Task SaveAsync(Guid applicationId, Guid roleId, List<Guid> resourceIds, bool isDeny);
    }
}