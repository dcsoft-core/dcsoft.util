using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DCSoft.Applications.Dtos.Systems;
using DCSoft.Applications.Extensions.Systems;
using DCSoft.Applications.Requests.Systems;
using DCSoft.Applications.Services.Abstractions.Systems;
using DCSoft.Data;
using DCSoft.Data.Queries.Systems;
using DCSoft.Domain.Enums;
using DCSoft.Domain.Extensions;
using DCSoft.Domain.Models.Systems;
using DCSoft.Domain.Repositories.Systems;
using Microsoft.EntityFrameworkCore;
using Util;
using Util.Applications.Trees;
using Util.Data;
using Util.Domain;
using Util.Exceptions;

namespace DCSoft.Applications.Services.Implements.Systems
{
    /// <summary>
    /// 模块服务
    /// </summary>
    public class ModuleService : TreeServiceBase<Resource, ModuleDto, ResourceQuery>, IModuleService
    {
        /// <summary>
        /// 初始化模块服务
        /// </summary>
        /// <param name="serviceProvider">服务提供器</param>
        /// <param name="unitOfWork">工作单元</param>
        /// <param name="moduleRepository">模块仓储</param>
        /// <param name="resourceRepository">资源仓储</param>
        public ModuleService(IServiceProvider serviceProvider, IDataUnitOfWork unitOfWork,
            IModuleRepository moduleRepository,
            IResourceRepository resourceRepository) : base(serviceProvider, unitOfWork, resourceRepository)
        {
            _moduleRepository = moduleRepository;
            _resourceRepository = resourceRepository;
        }

        /// <summary>
        /// 工作单元
        /// </summary>
        private readonly IResourceRepository _resourceRepository;

        /// <summary>
        /// 模块仓储
        /// </summary>
        private readonly IModuleRepository _moduleRepository;

        /// <summary>
        /// 过滤
        /// </summary>
        protected override IQueryable<Resource> Filter(IQueryable<Resource> queryable, ResourceQuery query)
        {
            return base.Filter(queryable, query).Include(t => t.Application)
                .Include(t => t.Parent)
                .Where(t => t.Type == ResourceType.Module || t.Type == ResourceType.Operation)
                .Where(t => t.ApplicationId == query.ApplicationId)
                .WhereIfNotEmpty(t => t.Name.Contains(query.Name))
                .WhereIfNotEmpty(t => t.Uri.Contains(query.Uri));
        }

        /// <summary>
        /// 转成数据传输对象
        /// </summary>
        protected override ModuleDto ToDto(Resource po)
        {
            return po.ToModuleDto();
        }

        /// <summary>
        /// 删除前操作
        /// </summary>
        /// <param name="entities"></param>
        protected override async Task DeleteBeforeAsync(List<Resource> entities)
        {
            var exists = entities.Any(t => t.Level == 1);
            if (exists)
            {
                throw new Warning("一级主菜单不能删除");
            }

            foreach (var resource in entities)
            {
                var list = _resourceRepository.Find(t => t.ParentId.Equals(resource.Id));
                await _resourceRepository.RemoveAsync(list);
            }
        }

        /// <summary>
        /// 创建模块
        /// </summary>
        /// <param name="request">创建模块参数</param>
        public async Task<Guid> CreateAsync(CreateModuleRequest request)
        {
            var module = request.ToModule();
            module.CheckNull(nameof(module));
            module.Init();
            var parent = await _resourceRepository.FindByIdAsync(module.ParentId);
            module.InitPath(parent.ToModule());
            module.SortId =
                await _moduleRepository.GenerateSortIdAsync(module.ApplicationId.SafeValue(), module.ParentId);
            await _resourceRepository.AddAsync(module.ToResource());
            return module.Id;
        }

        /// <summary>
        /// 修改模块
        /// </summary>
        /// <param name="request">模块参数</param>
        public override async Task UpdateAsync(ModuleDto request)
        {
            var module = await _moduleRepository.FindByIdAsync(request.Id.ToGuid());
            request.MapTo(module);
            module.Uri = request.Url;
            module.InitPinYin();
            await _moduleRepository.UpdatePathAsync(module);
            await _resourceRepository.UpdateAsync(module);
        }
    }
}