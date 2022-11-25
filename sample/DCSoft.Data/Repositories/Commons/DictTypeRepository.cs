using DCSoft.Domain.Models.Commons;
using DCSoft.Domain.Repositories.Commons;
using Util.Data.EntityFrameworkCore;

namespace DCSoft.Data.Repositories.Commons
{
    /// <summary>
    /// 字典类型仓储
    /// </summary>
    public class DictTypeRepository : RepositoryBase<DictType>, IDictTypeRepository
    {
        /// <summary>
        /// 初始化字典类型仓储
        /// </summary>
        /// <param name="unitOfWork">工作单元</param>
        public DictTypeRepository(IDataUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}