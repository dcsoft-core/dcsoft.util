using DCSoft.Applications.Dtos.Systems;
using DCSoft.Applications.Services.Abstractions.Systems;
using DCSoft.Data;
using DCSoft.Data.Queries.Systems;
using DCSoft.Domain.Repositories.Systems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DCSoft.Applications.Requests.Systems;
using DCSoft.Domain.Models.Systems;
using Util.Applications.Trees;
using Util;
using Util.Data;
using Util.Exceptions;
using Microsoft.EntityFrameworkCore;
using Util.Domain;

namespace DCSoft.Applications.Services.Implements.Systems
{
    /// <summary>
    /// 角色服务
    /// </summary>
    public class RoleService : TreeServiceBase<Role, RoleDto, RoleQuery>, IRoleService
    {
        /// <summary>
        /// 初始化角色服务
        /// </summary>
        /// <param name="serviceProvider">服务提供器</param>
        /// <param name="unitOfWork">工作单元</param>
        /// <param name="repository">仓储</param>
        public RoleService(IServiceProvider serviceProvider, IDataUnitOfWork unitOfWork,
            IRoleRepository repository) : base(serviceProvider, unitOfWork, repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// 角色仓储
        /// </summary>
        private readonly IRoleRepository _repository;

        /// <inheritdoc />
        protected override IQueryable<Role> Filter(IQueryable<Role> queryable, RoleQuery param)
        {
            return base.Filter(queryable, param)
                .WhereIfNotEmpty(t => t.Code.Contains(param.Code))
                .WhereIfNotEmpty(t => t.Name.Contains(param.Name))
                .WhereIfNotEmpty(t => t.Type.Contains(param.Type))
                .WhereIfNotEmpty(t => t.IsAdmin == param.IsAdmin)
                .WhereIfNotEmpty(t => t.Enabled == param.Enabled);
        }

        /// <inheritdoc />
        public async Task<Guid> CreateAsync(CreateRoleRequest request)
        {
            var role = request.MapTo<Role>();

            await ValidateCreate(role);
            role.Init();
            var parent = await _repository.FindByIdAsync(role.ParentId);
            role.InitPath(parent);
            role.SortId = await _repository.GenerateSortIdAsync(role.ParentId);
            await _repository.AddAsync(role);

            return role.Id;
        }

        /// <summary>
        /// 创建角色验证
        /// </summary>
        /// <param name="role">角色</param>
        protected virtual async Task ValidateCreate(Role role)
        {
            role.CheckNull(nameof(role));
            if (await _repository.ExistsAsync(t => t.Code == role.Code))
                ThrowDuplicateCodeException(role.Code);
        }

        /// <summary>
        /// 抛出编码重复异常
        /// </summary>
        protected void ThrowDuplicateCodeException(string code)
        {
            throw new Warning($"角色编号{code}已存在");
        }

        /// <inheritdoc />
        public async Task UpdateAsync(UpdateRoleRequest request)
        {
            var role = await _repository.FindByIdAsync(request.Id.ToGuid());
            request.MapTo(role);
            role.CheckNull(nameof(role));
            await ValidateUpdate(role);
            role.InitPinYin();
            await _repository.UpdatePathAsync(role);
            await _repository.UpdateAsync(role);
        }

        /// <summary>
        /// 修改角色验证
        /// </summary>
        /// <param name="role">角色</param>
        protected async Task ValidateUpdate(Role role)
        {
            if (await _repository.ExistsAsync(t => t.Id != role.Id && t.Code == role.Code))
                ThrowDuplicateCodeException(role.Code);
        }

        /// <inheritdoc />
        public Task<List<Guid>> GetRoleIdsAsync(Guid userId)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public async Task<List<RoleDto>> GetRolesAsync(Guid userId)
        {
            var result = await _repository.GetRolesAsync(userId);
            return result.MapTo<List<RoleDto>>();
        }

        /// <inheritdoc />
        public async Task<List<RoleDto>> GetEnabledRolesAsync()
        {
            var entities = await _repository.Find(t => t.Enabled).ToListAsync();
            return entities.Select(ToDto).ToList();
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

        /// <inheritdoc />
        public async Task AddUsersToRoleAsync(UserRoleRequest request)
        {
            var roleId = request.RoleId;
            var userIds = request.UserIds.ToGuidList();
            if (roleId.IsEmpty() || userIds == null)
                return;
            var existsUserIds = await _repository.GetExistsUserIdsAsync(roleId, userIds);
            userIds = userIds.ToList().Except(existsUserIds).ToList();
            var userRoles = CreateUserRoles(roleId, userIds);
            await _repository.AddUserRolesAsync(userRoles);
        }

        /// <inheritdoc />
        public Task RemoveUsersFromRoleAsync(UserRoleRequest request)
        {
            var roleId = request.RoleId;
            var userIds = request.UserIds.ToGuidList();
            if (roleId.IsEmpty() || userIds == null)
                return Task.CompletedTask;
            var userRoles = CreateUserRoles(roleId, userIds);
            _repository.RemoveUserRoles(userRoles);
            return Task.CompletedTask;
        }

        /// <inheritdoc />
        public async Task AddRolesToUserAsync(Guid userId, List<Guid> roleIds)
        {
            if (userId.IsEmpty() || roleIds == null)
                return;
            var userRoles = CreateUserRoleList(userId, roleIds);
            await _repository.AddUserRolesAsync(userRoles);
        }

        /// <inheritdoc />
        public async Task RemoveUsersAllRoleAsync(Guid userId)
        {
            if (userId.IsEmpty())
                return;
            var roleList = await _repository.GetRolesAsync(userId);
            var roleIds = roleList.Select(t => t.Id).ToList();
            var roles = CreateUserRoleList(userId, roleIds);
            _repository.RemoveUserRoles(roles);
        }
    }
}