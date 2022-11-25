using DCSoft.Applications.Dtos.Systems;
using DCSoft.Data.Queries.Systems;
using System.Collections.Generic;
using System.Threading.Tasks;
using Util.Applications;

namespace DCSoft.Applications.Services.Abstractions.Systems
{
    /// <summary>
    /// 声明服务
    /// </summary>
    public interface IClaimService : ICrudService<ClaimDto, ClaimQuery>
    {
        /// <summary>
        /// 获取已启用的声明列表
        /// </summary>
        Task<List<ClaimDto>> GetEnabledClaimsAsync();
    }
}