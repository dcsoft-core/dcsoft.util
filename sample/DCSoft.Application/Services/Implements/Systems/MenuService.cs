using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DCSoft.Applications.Extensions.Systems;
using DCSoft.Applications.Responses.Systems;
using DCSoft.Applications.Services.Abstractions.Systems;
using DCSoft.Domain.Enums;
using DCSoft.Domain.Extensions;
using DCSoft.Domain.Models.Systems;
using DCSoft.Domain.Repositories.Systems;
using Util;
using Util.Applications;
using Util.Sessions;

namespace DCSoft.Applications.Services.Implements.Systems
{
    /// <summary>
    /// 菜单服务
    /// </summary>
    public class MenuService : ServiceBase, IMenuService
    {
        /// <summary>
        /// 初始化菜单服务
        /// </summary>
        /// <param name="serviceProvider">服务提供器</param>
        /// <param name="roleRepository">角色仓储</param>
        /// <param name="moduleRepository">模块仓储</param>
        public MenuService(IServiceProvider serviceProvider, IRoleRepository roleRepository, IModuleRepository moduleRepository) : base(serviceProvider)
        {
            _roleRepository = roleRepository;
            _moduleRepository = moduleRepository;
        }

        /// <summary>
        /// 角色仓储
        /// </summary>
        private readonly IRoleRepository _roleRepository;

        /// <summary>
        /// 模块仓储
        /// </summary>
        private readonly IModuleRepository _moduleRepository;

        /// <summary>
        /// 获取菜单
        /// </summary>
        public async Task<List<MenuResponse>> GetMenusAsync()
        {
            var userId = Session.UserId;
            if (userId.IsEmpty())
                return new List<MenuResponse>();
            var roleIds = await _roleRepository.GetRoleIdsAsync(userId.ToGuid());
            var modules = await _moduleRepository.GetModulesAsync(Session.GetApplicationId(), roleIds.ToList());
            modules = modules.Where(t => t.Type == ResourceType.Module).ToList();
            await AddMissingParents(modules.ToList());
            return modules.Select(t => t.ToMenuResponse()).ToList();
        }

        /// <summary>
        /// 添加缺失的父节点列表
        /// </summary>
        private async Task AddMissingParents(List<Module> modules)
        {
            var parentIds = modules.Select(t=>t.Id.ToString()).ToList();
            var parents = await _moduleRepository.FindByIdsAsync(parentIds.Select(t => t.ToGuid()));
            modules.AddRange(parents.Where(t => t.Enabled).Select(t => t.ToModule()));
        }
    }
}