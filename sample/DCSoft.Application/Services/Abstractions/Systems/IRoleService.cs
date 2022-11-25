using DCSoft.Applications.Dtos.Systems;
using DCSoft.Applications.Requests.Systems;
using DCSoft.Data.Queries.Systems;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Util.Aop;
using Util.Applications.Trees;

namespace DCSoft.Applications.Services.Abstractions.Systems
{
    /// <summary>
    /// 角色服务
    /// </summary>
    public interface IRoleService : ITreeService<RoleDto, RoleQuery>
    {
        /// <summary>
        /// 创建角色
        /// </summary>
        /// <param name="request">创建角色参数</param>
        Task<Guid> CreateAsync([NotNull] CreateRoleRequest request);

        /// <summary>
        /// 修改角色
        /// </summary>
        /// <param name="request">修改角色参数</param>
        Task UpdateAsync([NotNull] UpdateRoleRequest request);

        /// <summary>
        /// 获取用户的角色标识列表
        /// </summary>
        /// <param name="userId">用户标识</param>
        Task<List<Guid>> GetRoleIdsAsync(Guid userId);

        /// <summary>
        /// 获取用户的角色列表
        /// </summary>
        /// <param name="userId">用户标识</param>
        Task<List<RoleDto>> GetRolesAsync(Guid userId);

        /// <summary>
        /// 获取已启用的角色列表
        /// </summary>
        Task<List<RoleDto>> GetEnabledRolesAsync();

        /// <summary>
        /// 添加用户到角色
        /// </summary>
        /// <param name="request">用户角色参数</param>
        Task AddUsersToRoleAsync(UserRoleRequest request);

        /// <summary>
        /// 从角色移除用户
        /// </summary>
        /// <param name="request">用户角色参数</param>
        Task RemoveUsersFromRoleAsync(UserRoleRequest request);

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
        Task RemoveUsersAllRoleAsync(Guid userId);
    }
}