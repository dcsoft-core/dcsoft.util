using DCSoft.Domain.Models.Commons;
using DCSoft.Domain.Repositories.Commons;
using Util.Data.EntityFrameworkCore;

namespace DCSoft.Data.Repositories.Commons
{
    /// <summary>
    /// 公共参数仓储
    /// </summary>
    public class ParametersRepository : RepositoryBase<Parameters>, IParametersRepository
    {
        /// <summary>
        /// 初始化公共参数仓储
        /// </summary>
        /// <param name="unitOfWork">工作单元</param>
        public ParametersRepository(IDataUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}