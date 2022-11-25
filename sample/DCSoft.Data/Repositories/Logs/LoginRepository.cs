using DCSoft.Domain.Models.Logs;
using DCSoft.Domain.Repositories.Logs;
using Util.Data.EntityFrameworkCore;

namespace DCSoft.Data.Repositories.Logs
{
    /// <summary>
    /// 登录日志仓储
    /// </summary>
    public class LoginRepository : RepositoryBase<Login>, ILoginRepository
    {
        /// <summary>
        /// 初始化登录日志仓储
        /// </summary>
        /// <param name="unitOfWork">工作单元</param>
        public LoginRepository(IDataUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}