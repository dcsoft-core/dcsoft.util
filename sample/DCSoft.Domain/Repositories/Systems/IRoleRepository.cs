using DCSoft.Domain.Models;
using DCSoft.Domain.Models.Systems;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Util.Domain.Trees;

namespace DCSoft.Domain.Repositories.Systems
{
    /// <summary>
    /// 角色仓储
    /// </summary>
    public interface IRoleRepository : ITreeRepository<Role>
    {
        /// <summary>
        /// 获取用户的角色列表
        /// </summary>
        /// <param name="userId">用户标识</param>
        Task<IEnumerable<Role>> GetRolesAsync(Guid userId);

        /// <summary>
        /// 获取用户的角色标识列表
        /// </summary>
        /// <param name="userId">用户标识</param>
        Task<IEnumerable<Guid>> GetRoleIdsAsync(Guid userId);

        /// <summary>
        /// 获取已添加的用户标识列表
        /// </summary>
        /// <param name="roleId">角色标识</param>
        /// <param name="userIds">用户标识列表</param>
        Task<IEnumerable<Guid>> GetExistsUserIdsAsync(Guid roleId, List<Guid> userIds);

        /// <summary>
        /// 添加用户角色列表
        /// </summary>
        /// <param name="userRoles">用户角色列表</param>
        Task AddUserRolesAsync(IEnumerable<UserRole> userRoles);

        /// <summary>
        /// 从角色移除用户
        /// </summary>
        /// <param name="userRoles">用户角色列表</param>
        void RemoveUserRoles(IEnumerable<UserRole> userRoles);

        /// <summary>
        /// 获取全部角色
        /// </summary>
        /// <returns></returns>
        Task<List<Role>> GetAllAsync();

        /// <summary>
        /// 添加用户到角色
        /// </summary>
        /// <param name="roleId">角色标识</param>
        /// <param name="userIds">用户标识列表</param>
        Task AddUsersToRoleAsync(Guid roleId, List<Guid> userIds);

        /// <summary>
        /// 从角色移除用户
        /// </summary>
        /// <param name="roleId">角色标识</param>
        /// <param name="userIds">用户标识列表</param>
        Task RemoveUsersFromRoleAsync(Guid roleId, List<Guid> userIds);

        /// <summary>
        /// 添加角色到用户
        /// </summary>
        /// <param name="userId">用户标识</param>
        /// <param name="roleIds">角色标识列表</param>
        /// <returns></returns>
        Task AddRolesToUserAsync(Guid userId, List<Guid> roleIds);

        /// <summary>
        /// 移除用户所有角色
        /// </summary>
        /// <param name="userId">用户标识</param>
        Task<Task> RemoveUsersAllRoleAsync(Guid userId);

        /// <summary>
        /// 生成排序号
        /// </summary>
        /// <param name="roleId">角色标识</param>
        Task<int> GenerateSortIdAsync(Guid? roleId);
    }
}