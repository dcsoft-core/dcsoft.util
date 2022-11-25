using DCSoft.Domain.Models.Logs;
using DCSoft.Domain.Repositories.Logs;
using Util.Data.EntityFrameworkCore;

namespace DCSoft.Data.Repositories.Logs
{
    /// <summary>
    /// 操作日志仓储
    /// </summary>
    public class OperateRepository : RepositoryBase<Operate>, IOperateRepository
    {
        /// <summary>
        /// 初始化操作日志仓储
        /// </summary>
        /// <param name="unitOfWork">工作单元</param>
        public OperateRepository(IDataUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}