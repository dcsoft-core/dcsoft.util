using DCSoft.Applications.Dtos.Systems;
using DCSoft.Applications.Services.Abstractions.Systems;
using DCSoft.Data;
using DCSoft.Data.Queries.Systems;
using DCSoft.Domain.Repositories.Systems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DCSoft.Applications.Extensions.Systems;
using DCSoft.Applications.Requests.Systems;
using DCSoft.Applications.Responses.Systems;
using DCSoft.Domain.Enums;
using DCSoft.Domain.Models.Systems;
using DCSoft.Integration.Cache;
using Microsoft.EntityFrameworkCore;
using Util;
using Util.Applications;
using Util.Caching;
using Util.Data;
using Util.Extras.Security.Encryptors;
using Util.Extras.Sessions;
using Util.Helpers;

namespace DCSoft.Applications.Services.Implements.Systems
{
    /// <summary>
    /// 用户服务
    /// </summary>
    public class UserService : CrudServiceBase<User, UserDto, UserQuery>, IUserService
    {
        /// <summary>
        /// 初始化用户服务
        /// </summary>
        /// <param name="serviceProvider">服务提供器</param>
        /// <param name="unitOfWork">工作单元</param>
        /// <param name="repository">仓储</param>
        /// <param name="moduleRepository">模块仓储</param>
        /// <param name="roleRepository">角色仓储</param>
        /// <param name="cache">缓存服务</param>
        public UserService(IServiceProvider serviceProvider, IDataUnitOfWork unitOfWork,
            IUserRepository repository,
            IModuleRepository moduleRepository,
            IRoleRepository roleRepository,
            ICache cache) : base(serviceProvider, unitOfWork, repository)
        {
            _repository = repository;
            _moduleRepository = moduleRepository;
            _roleRepository = roleRepository;
            _cache = cache;
        }

        /// <summary>
        /// 用户仓储
        /// </summary>
        private readonly IUserRepository _repository;

        /// <summary>
        /// 模块仓储
        /// </summary>
        private readonly IModuleRepository _moduleRepository;

        /// <summary>
        /// 角色服务
        /// </summary>
        private readonly IRoleRepository _roleRepository;

        /// <summary>
        /// 缓存服务
        /// </summary>
        private readonly ICache _cache;

        /// <inheritdoc />
        protected override IQueryable<User> Filter(IQueryable<User> queryable, UserQuery param)
        {
            if (param.RoleId != null)
                return _repository.FilterByRole(queryable, param.RoleId.SafeValue());
            if (param.ExceptRoleId != null)
                return _repository.FilterByRole(queryable, param.ExceptRoleId.SafeValue(), true);
            return base.Filter(queryable, param)
                .Include(t => t.Roles)
                .WhereIfNotEmpty(t => t.UserName.Contains(param.UserName))
                .WhereIfNotEmpty(t => t.PhoneNumber.Contains(param.PhoneNumber))
                .WhereIfNotEmpty(t => t.Email.Contains(param.Email))
                .WhereIfNotEmpty(t => t.NickName.Contains(param.NickName))
                .WhereIf(t => t.UserType == param.UserType, param.UserType != 0);
        }

        /// <summary>
        /// 转成数据传输对象
        /// </summary>
        protected override UserDto ToDto(User po)
        {
            var result = po.MapTo<UserDto>();
            return result;
        }

        /// <inheritdoc />
        public async Task<Guid> CreateAsync(CreateUserRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));
            var user = request.MapTo<User>();
            user.Enabled = true;

            user.Init();
            user.Validate();
            user.SetPasswordHash(user.Password);
            user.SetPassword(user.Password, true);
            await _repository.AddAsync(user);

            await _roleRepository.AddRolesToUserAsync(user.Id, request.RoleIds.ToGuidList());
            return user.Id;
        }

        /// <inheritdoc />
        public async Task<Guid> UpdateAsync(UpdateUserRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            var user = await _repository.FindByIdAsync(request.Id.ToGuid());
            user.NickName = request.NickName;
            user.Email = request.Email;
            user.PhoneNumber = request.PhoneNumber;
            user.Remark = request.Remark;

            user.Init();
            user.Validate();
            await _repository.UpdateAsync(user);

            await _roleRepository.RemoveUsersAllRoleAsync(user.Id);
            await _roleRepository.AddRolesToUserAsync(user.Id, request.RoleIds.ToGuidList());
            return user.Id;
        }

        /// <inheritdoc />
        public override async Task<UserDto> GetByIdAsync(object id)
        {
            var userId = id.ToString().ToGuid();
            var user = await _repository.FindByIdAsync(userId);
            var dto = user.ToDto();
            var role = await _roleRepository.GetRolesAsync(userId);
            dto.Roles = role.MapToList<RoleDto>();
            return dto;
        }

        /// <inheritdoc />
        public async Task UpdateSuccessLoginAsync(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));
            user.AccessFailedCount = 0;
            user.LoginCount = (user.LoginCount ?? 0) + 1;
            user.LastLoginIp = user.CurrentLoginIp;
            user.LastLoginTime = user.CurrentLoginTime;
            user.CurrentLoginIp = Ip.GetIp();
            user.CurrentLoginTime = DateTime.Now;
            await _repository.UpdateAsync(user);
        }

        /// <inheritdoc />
        public async Task UpdateFailLoginAsync(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));
            user.AccessFailedCount = (user.AccessFailedCount ?? 0) + 1;
            await _repository.UpdateAsync(user);
        }

        /// <inheritdoc />
        public async Task<Guid> UpdateBaseAsync(UpdateUserBaseRequest request)
        {
            var id = request.Id.ToGuid();
            var user = await _repository.FindByIdAsync(id);
            user.NickName = request.NickName;
            user.PhoneNumber = request.PhoneNumber;
            user.Remark = request.Remark;
            await _repository.UpdateAsync(user);
            return user.Id;
        }

        /// <summary>
        /// 删除前操作
        /// </summary>
        /// <param name="entities"></param>
        protected override async Task DeleteBeforeAsync(List<User> entities)
        {
            foreach (var user in entities)
            {
                await _roleRepository.RemoveUsersAllRoleAsync(user.Id);
            }
        }

        /// <inheritdoc />
        public async Task<bool> UpdateAvatarAsync(Guid userId, string avatar)
        {
            var user = await _repository.FindByIdAsync(userId);
            user.Avatar = avatar;
            await _repository.UpdateAsync(user);
            return true;
        }

        /// <inheritdoc />
        public async Task<Guid> RegisterAsync(UserRegisterRequest request)
        {
            var userId = Id.CreateGuid();
            var user = new User(userId)
            {
                UserType = request.UserType,
                UserName = request.UserName,
                NickName = request.NickName,
                PhoneNumber = request.Mobile,
                PasswordHash = new PasswordEncryptor().Encrypt(request.Password)
            };
            await _repository.AddAsync(user);
            return user.Id;
        }

        /// <inheritdoc />
        public async Task<Guid> EnableAsync(Guid userId, bool isEnabled)
        {
            var user = await _repository.SingleAsync(p => p.Id == userId);
            if (user == null || user.Id.IsEmpty())
            {
                return userId;
            }

            user.Enabled = isEnabled;
            await _repository.UpdateAsync(user);
            return user.Id;
        }

        /// <inheritdoc />
        public async Task<bool> ResetPasswordAsync(Guid userId)
        {
            var user = await _repository.SingleAsync(p => p.Id == userId);
            if (user == null || user.Id.IsEmpty())
                return false;
            var userPwd = "123456";
            await _repository.ResetPasswordAsync(user, userPwd);
            return true;
        }

        /// <inheritdoc />
        public async Task<bool> ChangePasswordAsync(Guid userId, string currentPassword, string newPassword)
        {
            var user = await _repository.SingleAsync(p => p.Id == userId);
            if (user == null || user.Id.IsEmpty())
                return false;

            await _repository.ChangePasswordAsync(user, currentPassword, newPassword);
            return true;
        }

        /// <inheritdoc />
        public async Task<List<UserPermissionsResponse>> GetPermissionsAsync()
        {
            var userId = Session.UserId;
            if (userId.IsEmpty())
                return new List<UserPermissionsResponse>();

            return await _cache.GetAsync(
                string.Format(CacheKey.UserPermissions, Session.GetUserName()),
                async () =>
                {
                    var roleIds = await _roleRepository.GetRoleIdsAsync(userId.ToGuid());
                    var modules = await _moduleRepository.GetModulesAsync(Session.GetApplicationId(), roleIds.ToList());
                    modules = modules.Where(t => t.Type == ResourceType.Operation).ToList();
                    return modules.MapTo<List<UserPermissionsResponse>>();
                }
            );
        }

        /// <inheritdoc />
        public async Task<User> FindByNameAsync(string userName)
        {
            return await _repository.SingleAsync(t => t.UserName == userName);
        }

        /// <inheritdoc />
        public async Task<User> FindByEmailAsync(string email)
        {
            return await _repository.SingleAsync(t => t.Email == email);
        }

        /// <inheritdoc />
        public async Task<User> FindByPhoneAsync(string phoneNumber)
        {
            return await _repository.SingleAsync(t => t.PhoneNumber == phoneNumber);
        }
    }
}