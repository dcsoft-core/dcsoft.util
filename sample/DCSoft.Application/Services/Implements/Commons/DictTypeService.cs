using DCSoft.Applications.Dtos.Commons;
using DCSoft.Applications.Services.Abstractions.Commons;
using DCSoft.Data;
using DCSoft.Data.Queries.Commons;
using DCSoft.Domain.Repositories.Commons;
using System;
using System.Linq;
using DCSoft.Domain.Models.Commons;
using Util.Applications;
using Util.Data;
using System.Threading.Tasks;
using Util.Exceptions;

namespace DCSoft.Applications.Services.Implements.Commons
{
    /// <summary>
    /// 字典类型服务
    /// </summary>
    public class DictTypeService : CrudServiceBase<DictType, DictTypeDto, DictTypeQuery>, IDictTypeService
    {
        /// <summary>
        /// 初始化字典类型服务
        /// </summary>
        /// <param name="serviceProvider">服务提供器</param>
        /// <param name="unitOfWork">工作单元</param>
        /// <param name="repository">仓储</param>
        public DictTypeService(IServiceProvider serviceProvider, IDataUnitOfWork unitOfWork, IDictTypeRepository repository) : base(serviceProvider, unitOfWork, repository)
        {
            _dictTypeRepository = repository;
        }

        /// <summary>
        /// 字典类型仓储
        /// </summary>
        private readonly IDictTypeRepository _dictTypeRepository;

        /// <inheritdoc />
        protected override IQueryable<DictType> Filter(IQueryable<DictType> queryable, DictTypeQuery param)
        {
            return base.Filter(queryable, param)
                .WhereIfNotEmpty(t => t.Name.Contains(param.Name))
                .WhereIfNotEmpty(t => t.Code.Contains(param.Code));
        }

        /// <summary>
        /// 创建前操作
        /// </summary>
        protected override async Task CreateBeforeAsync(DictType entity)
        {
            await base.CreateBeforeAsync(entity);
            entity.Init();
            if (await _dictTypeRepository.ExistsAsync(t => t.Code == entity.Code))
                throw new Warning("字典类型已存在");
            if (await _dictTypeRepository.ExistsAsync(t => t.Name == entity.Name))
                throw new Warning("字典名称已存在");
        }

        /// <summary>
        /// 修改前操作
        /// </summary>
        protected override async Task UpdateBeforeAsync(DictType entity)
        {
            await base.UpdateBeforeAsync(entity);
            entity.Init();
            if (await _dictTypeRepository.ExistsAsync(t => t.Id != entity.Id && t.Code == entity.Code))
                throw new Warning("字典类型已存在");
            if (await _dictTypeRepository.ExistsAsync(t => t.Id != entity.Id && t.Name == entity.Name))
                throw new Warning("字典名称已存在");
        }
    }
}