using DCSoft.Domain.Models;
using DCSoft.Domain.Models.Systems;
using DCSoft.Domain.Repositories.Systems;
using Util.Data.EntityFrameworkCore;

namespace DCSoft.Data.Repositories.Systems
{
    /// <summary>
    /// 声明仓储
    /// </summary>
    public class ClaimRepository : RepositoryBase<Claim>, IClaimRepository
    {
        /// <summary>
        /// 初始化声明仓储
        /// </summary>
        /// <param name="unitOfWork">工作单元</param>
        public ClaimRepository(IDataUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}