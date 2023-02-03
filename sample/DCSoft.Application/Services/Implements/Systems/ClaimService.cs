using DCSoft.Applications.Dtos.Systems;
using DCSoft.Applications.Services.Abstractions.Systems;
using DCSoft.Data;
using DCSoft.Data.Queries.Systems;
using DCSoft.Domain.Repositories.Systems;
using System;
using System.Linq;
using DCSoft.Domain.Models.Systems;
using Util.Applications;
using Util.Data;
using System.Collections.Generic;
using System.Threading.Tasks;
using DCSoft.Applications.Extensions.Systems;
using Microsoft.EntityFrameworkCore;
using Util;

namespace DCSoft.Applications.Services.Implements.Systems
{
    /// <summary>
    /// 声明服务
    /// </summary>
    public class ClaimService : CrudServiceBase<Claim, ClaimDto, ClaimQuery>, IClaimService
    {
        /// <summary>
        /// 初始化声明服务
        /// </summary>
        /// <param name="serviceProvider">服务提供器</param>
        /// <param name="unitOfWork">工作单元</param>
        /// <param name="repository">仓储</param>
        public ClaimService(IServiceProvider serviceProvider,
            IDataUnitOfWork unitOfWork,
            IClaimRepository repository) : base(serviceProvider, unitOfWork, repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// 声明仓储
        /// </summary>
        private readonly IClaimRepository _repository;

        /// <inheritdoc />
        protected override IQueryable<Claim> Filter(IQueryable<Claim> queryable, ClaimQuery param)
        {
            return base.Filter(queryable, param).WhereIfNotEmpty(t => t.Name.Contains(param.Name))
                .WhereIfNotEmpty(t => t.Remark.Contains(param.Remark));
        }

        /// <summary>
        /// 获取已启用的声明列表
        /// </summary>
        public async Task<List<ClaimDto>> GetEnabledClaimsAsync()
        {
            var entities = await _repository.Find(t => t.Enabled).ToListAsync();
            return entities.Select(ToDto).ToList();
        }

        /// <summary>
        /// 得到详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public override async Task<ClaimDto> GetByIdAsync(object id)
        {
            var claimId = id.ToString().ToGuid();
            var result = await _repository.SingleAsync(t => t.Id == claimId);
            return result.ToDto();
        }
    }
}