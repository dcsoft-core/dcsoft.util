using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DCSoft.Domain.Models.Systems;
using DCSoft.Domain.Repositories.Systems;
using Microsoft.EntityFrameworkCore;
using Util.Data.EntityFrameworkCore;
using Util.Domain;

namespace DCSoft.Data.Repositories.Systems
{
    /// <summary>
    /// 权限仓储
    /// </summary>
    public class PermissionRepository : RepositoryBase<Permission>, IPermissionRepository
    {
        /// <summary>
        /// 初始化权限仓储
        /// </summary>
        /// <param name="unitOfWork">工作单元</param>
        public PermissionRepository(IDataUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        /// <summary>
        /// 获取资源标识列表
        /// </summary>
        /// <param name="applicationId">应用程序标识</param>
        /// <param name="roleId">角色标识</param>
        /// <param name="isDeny">是否拒绝</param>
        public async Task<IEnumerable<Guid>> GetResourceIdsAsync(Guid applicationId, Guid roleId, bool isDeny)
        {
            var queryable = from permission in Find()
                            join resource in UnitOfWork.Set<Resource>() on permission.ResourceId equals resource.Id
                            where resource.ApplicationId == applicationId && permission.RoleId == roleId &&
                                  permission.IsDeny == isDeny
                            select resource.Id;
            return await queryable.ToListAsync();
        }

        /// <summary>
        ///获取权限标识列表
        /// </summary>
        /// <param name="roleId">角色标识</param>
        /// <param name="resourceIds">资源标识列表</param>
        public async Task<IEnumerable<Guid>> GetPermissionIdsAsync(Guid roleId, List<Guid> resourceIds)
        {
            return await Find().Where(t => t.RoleId == roleId && resourceIds.Contains(t.ResourceId)).Select(t => t.Id)
                .ToListAsync();
        }

        /// <summary>
        /// 移除权限
        /// </summary>
        /// <param name="roleId">角色标识</param>
        /// <param name="resourceIds">资源标识列表</param>
        public async Task RemoveAsync(Guid roleId, List<Guid> resourceIds)
        {
            var permissionIds = await GetPermissionIdsAsync(roleId, resourceIds);
            var entities = Find().Where(t => permissionIds.Contains(t.Id)).ToList();
            await RemoveAsync(entities);
        }

        /// <summary>
        /// 保存角色权限
        /// </summary>
        /// <param name="applicationId"></param>
        /// <param name="roleId"></param>
        /// <param name="resourceIds"></param>
        /// <param name="isDeny"></param>
        /// <returns></returns>
        public async Task SaveAsync(Guid applicationId, Guid roleId, List<Guid> resourceIds, bool isDeny)
        {
            if (resourceIds == null)
                return;
            var oldResourceIds = await GetResourceIdsAsync(applicationId, roleId, isDeny);
            var result = resourceIds.Compare(oldResourceIds);
            await AddAsync(ToPermissions(roleId, result.CreateList, isDeny));
            await RemoveAsync(roleId, result.DeleteList);
        }

        /// <summary>
        /// 转换为权限实体列表
        /// </summary>
        private List<Permission> ToPermissions(Guid roleId, List<Guid> resourceIds, bool isDeny)
        {
            return resourceIds.Select(resourceId => ToPermission(roleId, resourceId, isDeny)).ToList();
        }

        /// <summary>
        /// 转换为权限实体
        /// </summary>
        private Permission ToPermission(Guid roleId, Guid resourceId, bool isDeny)
        {
            var result = new Permission
            {
                RoleId = roleId,
                ResourceId = resourceId,
                IsDeny = isDeny
            };
            result.Init();
            return result;
        }
    }
}