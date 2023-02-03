using DCSoft.Applications.Dtos.Systems;
using DCSoft.Applications.Services.Abstractions.Systems;
using DCSoft.Data;
using DCSoft.Data.Queries.Systems;
using DCSoft.Domain.Repositories.Systems;
using System;
using System.Linq;
using DCSoft.Domain.Models.Systems;
using Util.Applications.Trees;

namespace DCSoft.Applications.Services.Implements.Systems
{
    /// <summary>
    /// 资源服务
    /// </summary>
    public class ResourceService : TreeServiceBase<Resource, ResourceDto, ResourceQuery>, IResourceService
    {
        /// <summary>
        /// 初始化资源服务
        /// </summary>
        /// <param name="serviceProvider">服务提供器</param>
        /// <param name="unitOfWork">工作单元</param>
        /// <param name="repository">仓储</param>
        public ResourceService(IServiceProvider serviceProvider, IDataUnitOfWork unitOfWork,
            IResourceRepository repository) : base(serviceProvider, unitOfWork, repository)
        {
        }

        /// <inheritdoc />
        protected override IQueryable<Resource> Filter(IQueryable<Resource> queryable, ResourceQuery param)
        {
            return queryable;
        }
    }
}