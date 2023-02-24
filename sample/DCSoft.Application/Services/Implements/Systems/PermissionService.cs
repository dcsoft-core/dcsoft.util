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
using Util.Applications;
using Util.Caching;
using DCSoft.Integration.Cache;
using Util;

namespace DCSoft.Applications.Services.Implements.Systems
{
    /// <summary>
    /// 权限服务
    /// </summary>
    public class PermissionService : CrudServiceBase<Permission, PermissionDto, PermissionQuery>, IPermissionService
    {
        /// <summary>
        /// 初始化权限服务
        /// </summary>
        /// <param name="serviceProvider">服务提供器</param>
        /// <param name="unitOfWork">工作单元</param>
        /// <param name="repository">仓储</param>
        /// <param name="userService">用户服务</param>
        /// <param name="cache">缓存服务</param>
        public PermissionService(IServiceProvider serviceProvider,
            IDataUnitOfWork unitOfWork,
            IPermissionRepository repository,
            IUserService userService,
            ICache cache) : base(serviceProvider, unitOfWork, repository)
        {
            _repository = repository;
            _userService = userService;
            _cache = cache;
        }

        /// <summary>
        /// 权限仓储
        /// </summary>
        private readonly IPermissionRepository _repository;

        /// <summary>
        /// 用户服务
        /// </summary>
        private readonly IUserService _userService;

        /// <summary>
        /// 缓存服务
        /// </summary>
        private readonly ICache _cache;

        /// <inheritdoc />
        protected override IQueryable<Permission> Filter(IQueryable<Permission> queryable, PermissionQuery param)
        {
            return queryable;
        }

        /// <summary>
        /// 权限验证
        /// </summary>
        /// <param name="api">接口路径</param>
        /// <param name="httpMethod">http请求方法</param>
        /// <returns></returns>
        public async Task<bool> Validate(string api, string httpMethod)
        {
            var permissions = await _userService.GetPermissionsAsync();

            var isValid = permissions.Any(m =>
                string.Equals(m.Method, httpMethod, StringComparison.OrdinalIgnoreCase) &&
                string.Equals(m.Url, api, StringComparison.OrdinalIgnoreCase));

            return isValid;
        }

        /// <summary>
        /// 获取资源标识列表
        /// </summary>
        /// <param name="query">权限参数</param>
        public async Task<IEnumerable<Guid>> GetResourceIdsAsync(PermissionQuery query)
        {
            return await _repository.GetResourceIdsAsync(query.ApplicationId.SafeValue(),
                query.RoleId.SafeValue(), query.IsDeny.SafeValue());
        }

        /// <summary>
        /// 保存权限
        /// </summary>
        /// <param name="request">参数</param>
        public async Task SaveAsync(SavePermissionRequest request)
        {
            await _repository.SaveAsync(request.ApplicationId.SafeValue(), request.RoleId.SafeValue(),
                request.ResourceIds.ToGuidList(), request.IsDeny.SafeValue());

            _cache.Remove(CacheKey.UserPermissions);
        }
    }
}