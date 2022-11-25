using DCSoft.Domain.Models;
using DCSoft.Domain.Models.Systems;
using DCSoft.Domain.Repositories.Systems;
using System.Threading.Tasks;
using Util.Data.EntityFrameworkCore;

namespace DCSoft.Data.Repositories.Systems
{
    /// <summary>
    /// 应用程序仓储
    /// </summary>
    public class ApplicationRepository : RepositoryBase<Application>, IApplicationRepository
    {
        /// <summary>
        /// 初始化应用程序仓储
        /// </summary>
        /// <param name="unitOfWork">工作单元</param>
        public ApplicationRepository(IDataUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        /// <summary>
        /// 通过应用程序编码查找
        /// </summary>
        /// <param name="code">应用程序编码</param>
        public async Task<Application> GetByCodeAsync(string code)
        {
            return await SingleAsync(t => t.Code.Equals(code));
        }

        /// <summary>
        /// 是否允许跨域访问
        /// </summary>
        /// <param name="origin">来源</param>
        public async Task<bool> IsOriginAllowedAsync(string origin)
        {
            return await ExistsAsync(t => t.Extend.Contains(origin));
        }

        /// <summary>
        /// 是否允许创建应用程序
        /// </summary>
        /// <param name="entity">应用程序</param>
        public async Task<bool> CanCreateAsync(Application entity)
        {
            var exists = await ExistsAsync(t => t.Code == entity.Code);
            return exists == false;
        }

        /// <summary>
        /// 是否允许修改应用程序
        /// </summary>
        /// <param name="entity">应用程序</param>
        public async Task<bool> CanUpdateAsync(Application entity)
        {
            var exists = await ExistsAsync(t => t.Id != entity.Id && t.Code == entity.Code);
            return exists == false;
        }
    }
}