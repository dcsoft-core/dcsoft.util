using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DCSoft.Domain.Enums;
using DCSoft.Domain.Models;
using DCSoft.Domain.Models.Systems;
using DCSoft.Domain.Repositories.Systems;
using Microsoft.EntityFrameworkCore;
using Util.Data.EntityFrameworkCore.Trees;

namespace DCSoft.Data.Repositories.Systems
{
    /// <summary>
    /// 资源仓储
    /// </summary>
    public class ResourceRepository : TreeRepositoryBase<Resource>, IResourceRepository
    {
        /// <summary>
        /// 初始化资源仓储
        /// </summary>
        /// <param name="unitOfWork">工作单元</param>
        /// <param name="permissionRepository">权限仓储</param>
        public ResourceRepository(IDataUnitOfWork unitOfWork, 
            IPermissionRepository permissionRepository) : base(unitOfWork)
        {
            _permissionRepository = permissionRepository;
        }
        /// <summary>
        /// 权限仓储
        /// </summary>
        private readonly IPermissionRepository _permissionRepository;

        /// <summary>
        /// 获取模块列表
        /// </summary>
        /// <param name="applicationId">应用程序标识</param>
        /// <param name="roleIds">角色标识列表</param>
        public async Task<IEnumerable<Resource>> GetModulesAsync(Guid applicationId, List<Guid> roleIds)
        {
            if (applicationId == Guid.Empty || roleIds == null || roleIds.Count == 0)
                return new List<Resource>();
            var result = await (from module in Find()
                join permission in _permissionRepository.Find() on module.Id equals permission.ResourceId
                where (module.Type == ResourceType.Module || module.Type == ResourceType.Operation) &&
                      module.ApplicationId == applicationId &&
                      module.Enabled &&
                      roleIds.Contains(permission.RoleId) &&
                      permission.IsDeny == false
                select module).ToListAsync();
            return result.Distinct().OrderBy(t => t.SortId).ToList();
        }
    }
}