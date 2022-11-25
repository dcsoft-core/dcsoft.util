using DCSoft.Applications.Dtos.Systems;
using DCSoft.Applications.Services.Abstractions.Systems;
using DCSoft.Data;
using DCSoft.Data.Queries.Systems;
using DCSoft.Domain.Repositories.Systems;
using System;
using System.Linq;
using System.Threading.Tasks;
using DCSoft.Applications.Extensions.Systems;
using DCSoft.Domain.Models.Systems;
using Util.Applications;
using Util.Data;
using Util.Extensions;
using Util.Exceptions;

namespace DCSoft.Applications.Services.Implements.Systems
{
    /// <summary>
    /// 应用程序服务
    /// </summary>
    public class ApplicationService : CrudServiceBase<Application, ApplicationDto, ApplicationQuery>, IApplicationService
    {
        /// <summary>
        /// 初始化应用程序服务
        /// </summary>
        /// <param name="serviceProvider">服务提供器</param>
        /// <param name="unitOfWork">工作单元</param>
        /// <param name="repository">仓储</param>
        public ApplicationService(IServiceProvider serviceProvider, 
            IDataUnitOfWork unitOfWork, 
            IApplicationRepository repository) : base(serviceProvider, unitOfWork, repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// 应用程序仓储
        /// </summary>
        private readonly IApplicationRepository _repository;

        /// <inheritdoc />
        protected override ApplicationDto ToDto(Application entity)
        {
            return entity.ToDto();
        }

        /// <inheritdoc />
        protected override IQueryable<Application> Filter(IQueryable<Application> queryable, ApplicationQuery param)
        {
            return base.Filter(queryable, param)
                .WhereIfNotEmpty(t => t.Code.Contains(param.Code))
                .WhereIfNotEmpty(t => t.Name.Contains(param.Name))
                .WhereIfNotEmpty(t => t.Remark.Contains(param.Remark)); ;
        }

        /// <summary>
        /// 创建应用程序
        /// </summary>
        /// <param name="dto">应用程序参数</param>
        public override async Task<string> CreateAsync(ApplicationDto dto)
        {
            var entity = dto.ToEntity().ToPo();
            await ValidateCreateAsync(entity);
            entity.Init();
            await _repository.AddAsync(entity);
            return entity.Id.ToString();
        }

        /// <summary>
        /// 验证创建应用程序
        /// </summary>
        private async Task ValidateCreateAsync(Domain.Models.Systems.Application entity)
        {
            entity.CheckNull(nameof(entity));
            if (await _repository.CanCreateAsync(entity) == false)
                ThrowCodeRepeatException(entity);
        }

        /// <summary>
        /// 抛出编码重复异常
        /// </summary>
        private void ThrowCodeRepeatException(Domain.Models.Systems.Application entity)
        {
            throw new Warning($"应用程序编码{entity.Code}已存在");
        }

        /// <summary>
        /// 修改应用程序
        /// </summary>
        /// <param name="dto">应用程序参数</param>
        public override async Task UpdateAsync(ApplicationDto dto)
        {
            var entity = dto.ToEntity().ToPo();
            await ValidateUpdateAsync(entity);
            await _repository.UpdateAsync(entity);
        }

        /// <summary>
        /// 验证修改应用程序
        /// </summary>
        private async Task ValidateUpdateAsync(Domain.Models.Systems.Application entity)
        {
            entity.CheckNull(nameof(entity));
            if (await _repository.CanUpdateAsync(entity) == false)
                ThrowCodeRepeatException(entity);
        }

        /// <inheritdoc />
        public async Task<ApplicationDto> GetByCodeAsync(string code)
        {
            var application = await _repository.GetByCodeAsync(code);
            return application.ToDto();
        }
    }
}