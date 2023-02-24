using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using DCSoft.Domain.Enums;
using DCSoft.Domain.Extensions;
using DCSoft.Domain.Models.Systems;
using DCSoft.Domain.Repositories.Systems;
using Microsoft.EntityFrameworkCore;
using Util;
using Util.Data.EntityFrameworkCore.Trees;

namespace DCSoft.Data.Repositories.Systems
{
    /// <summary>
    /// 模块仓储
    /// </summary>
    public class ModuleRepository : TreeRepositoryBase<Resource>, IModuleRepository
    {
        /// <summary>
        /// 资源仓储
        /// </summary>
        private readonly IResourceRepository _resourceRepository;

        /// <summary>
        /// 权限仓储
        /// </summary>
        private readonly IPermissionRepository _permissionRepository;

        /// <summary>
        /// 初始化模块仓储
        /// </summary>
        /// <param name="unitOfWork">工作单元</param>
        /// <param name="resourceRepository">资源仓储</param>
        /// <param name="permissionRepository">权限仓储</param>
        public ModuleRepository(IDataUnitOfWork unitOfWork,
            IResourceRepository resourceRepository,
            IPermissionRepository permissionRepository) : base(unitOfWork)
        {
            _resourceRepository = resourceRepository;
            _permissionRepository = permissionRepository;
        }

        /// <summary>
        /// 转成实体
        /// </summary>
        /// <param name="po">持久化对象</param>
        protected Module ToEntity(Resource po)
        {
            return po.ToModule();
        }

        /// <summary>
        /// 转成持久化对象
        /// </summary>
        /// <param name="entity">实体</param>
        protected Resource ToPo(Module entity)
        {
            return entity.ToResource();
        }

        /// <summary>
        /// 生成排序号
        /// </summary>
        /// <param name="applicationId">应用程序标识</param>
        /// <param name="parentId">父标识</param>
        public async Task<int> GenerateSortIdAsync(Guid applicationId, Guid? parentId)
        {
            var maxSortId = await _resourceRepository
                .Find(t => t.ApplicationId == applicationId && t.ParentId == parentId)
                .MaxAsync(t => t.SortId);
            return maxSortId.SafeValue() + 1;
        }

        /// <summary>
        /// 获取模块列表
        /// </summary>
        /// <param name="applicationId">应用程序标识</param>
        /// <param name="roleIds">角色标识列表</param>
        public async Task<IEnumerable<Module>> GetModulesAsync(Guid applicationId, List<Guid> roleIds)
        {
            if (applicationId == Guid.Empty || roleIds == null || roleIds.Count == 0)
                return new List<Module>();
            var pos = await (from module in _resourceRepository.Find()
                join permission in _permissionRepository.Find() on module.Id equals permission.ResourceId
                where (module.Type == ResourceType.Module || module.Type == ResourceType.Operation) &&
                      module.ApplicationId == applicationId &&
                      module.Enabled &&
                      roleIds.Contains(permission.RoleId) &&
                      permission.IsDeny == false
                select module).ToListAsync();
            return pos.Distinct().OrderBy(t => t.SortId).Select(ToEntity).ToList();
        }
    }
}