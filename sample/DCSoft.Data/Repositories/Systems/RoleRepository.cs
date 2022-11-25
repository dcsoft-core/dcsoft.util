using DCSoft.Domain.Models;
using DCSoft.Domain.Models.Systems;
using DCSoft.Domain.Repositories.Systems;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Util;
using Util.Data.EntityFrameworkCore.Trees;

namespace DCSoft.Data.Repositories.Systems
{
    /// <summary>
    /// 角色仓储
    /// </summary>
    public class RoleRepository : TreeRepositoryBase<Role>, IRoleRepository
    {
        /// <summary>
        /// 初始化角色仓储
        /// </summary>
        /// <param name="unitOfWork">工作单元</param>
        public RoleRepository(IDataUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        /// <summary>
        /// 通过编号获取角色
        /// </summary>
        /// <param name="roleId">角色编号</param>
        public async Task<Role> FindByIdAsync(string roleId)
        {
            return await FindByIdAsync(roleId.ToGuid());
        }

        /// <summary>
        /// 通过名称获取角色
        /// </summary>
        /// <param name="normalizedRoleName">标准化角色名称</param>
        public async Task<Role> FindByNameAsync(string normalizedRoleName)
        {
            return await SingleAsync(r => r.NormalizedName == normalizedRoleName);
        }

        /// <summary>
        /// 获取用户的角色列表
        /// </summary>
        /// <param name="userId">用户标识</param>
        public async Task<IEnumerable<Role>> GetRolesAsync(Guid userId)
        {
            return await GetRoleQueryable(userId).ToListAsync();
        }

        /// <summary>
        /// 获取角色查询对象
        /// </summary>
        private IQueryable<Role> GetRoleQueryable(Guid userId)
        {
            return from role in Set
                join userRole in UnitOfWork.Set<UserRole>() on role.Id equals userRole.RoleId
                where userRole.UserId == userId
                select role;
        }

        /// <summary>
        /// 获取用户的角色标识列表
        /// </summary>
        /// <param name="userId">用户标识</param>
        public async Task<IEnumerable<Guid>> GetRoleIdsAsync(Guid userId)
        {
            return await GetRoleQueryable(userId).Select(t => t.Id).ToListAsync();
        }

        /// <summary>
        /// 获取已添加的用户标识列表
        /// </summary>
        /// <param name="roleId">角色标识</param>
        /// <param name="userIds">用户标识列表</param>
        public async Task<IEnumerable<Guid>> GetExistsUserIdsAsync(Guid roleId, List<Guid> userIds)
        {
            return await UnitOfWork.Set<UserRole>().Where(t => t.RoleId == roleId && userIds.Contains(t.UserId))
                .Select(t => t.UserId).ToListAsync();
        }

        /// <summary>
        /// 添加用户角色列表
        /// </summary>
        /// <param name="userRoles">用户角色列表</param>
        public async Task AddUserRolesAsync(IEnumerable<UserRole> userRoles)
        {
            await UnitOfWork.Set<UserRole>().AddRangeAsync(userRoles);
        }

        /// <summary>
        /// 从角色移除用户
        /// </summary>
        /// <param name="userRoles">用户角色列表</param>
        public void RemoveUserRoles(IEnumerable<UserRole> userRoles)
        {
            UnitOfWork.Set<UserRole>().RemoveRange(userRoles);
        }

        /// <summary>
        /// 获取全部角色
        /// </summary>
        /// <returns></returns>
        public async Task<List<Role>> GetAllAsync()
        {
            return await Find().ToListAsync();
        }

        /// <summary>
        /// 创建用户角色列表
        /// </summary>
        private List<UserRole> CreateUserRoles(Guid roleId, List<Guid> userIds)
        {
            return userIds.Where(id => id.IsEmpty() == false).Select(userId => new UserRole(userId, roleId)).ToList();
        }

        /// <summary>
        /// 创建用户角色列表
        /// </summary>
        private List<UserRole> CreateUserRoleList(Guid userId, List<Guid> roleIds)
        {
            return roleIds.Where(id => id.IsEmpty() == false).Select(roleId => new UserRole(userId, roleId)).ToList();
        }

        /// <summary>
        /// 添加用户到角色
        /// </summary>
        /// <param name="roleId">角色标识</param>
        /// <param name="userIds">用户标识列表</param>
        public async Task AddUsersToRoleAsync(Guid roleId, List<Guid> userIds)
        {
            if (roleId.IsEmpty() || userIds == null)
                return;
            var existsUserIds = await GetExistsUserIdsAsync(roleId, userIds);
            userIds = userIds.ToList().Except(existsUserIds).ToList();
            var userRoles = CreateUserRoles(roleId, userIds);
            await AddUserRolesAsync(userRoles);
        }

        /// <summary>
        /// 从角色移除用户
        /// </summary>
        /// <param name="roleId">角色标识</param>
        /// <param name="userIds">用户标识列表</param>
        public Task RemoveUsersFromRoleAsync(Guid roleId, List<Guid> userIds)
        {
            if (roleId.IsEmpty() || userIds == null)
                return Task.CompletedTask;
            var userRoles = CreateUserRoles(roleId, userIds);
            RemoveUserRoles(userRoles);
            return Task.CompletedTask;
        }

        /// <summary>
        /// 添加角色到用户
        /// </summary>
        /// <param name="userId">用户标识</param>
        /// <param name="roleIds">角色标识列表</param>
        public async Task AddRolesToUserAsync(Guid userId, List<Guid> roleIds)
        {
            if (userId.IsEmpty() || roleIds == null)
                return;
            var userRoles = CreateUserRoleList(userId, roleIds);
            await AddUserRolesAsync(userRoles);
        }

        /// <summary>
        /// 移除用户所有角色
        /// </summary>
        /// <param name="userId">角色标识</param>
        public async Task<Task> RemoveUsersAllRoleAsync(Guid userId)
        {
            if (userId.IsEmpty())
                return Task.CompletedTask;
            var roleList = await GetRolesAsync(userId);
            var roleIds = roleList.Select(t => t.Id).ToList();
            var roles = CreateUserRoleList(userId, roleIds);
            RemoveUserRoles(roles);
            return Task.CompletedTask;
        }

        /// <summary>
        /// 生成排序号
        /// </summary>
        /// <param name="roleId">角色标识</param>
        public async Task<int> GenerateSortIdAsync(Guid? roleId)
        {
            var maxSortId = await Find(t => t.ParentId == roleId).MaxAsync(t => t.SortId);
            return maxSortId.SafeValue() + 1;
        }
    }
}