using DCSoft.Domain.Models.Commons;
using DCSoft.Domain.Repositories.Commons;
using Util.Data.EntityFrameworkCore;

namespace DCSoft.Data.Repositories.Commons
{
    /// <summary>
    /// 公共附件仓储
    /// </summary>
    public class AttachmentRepository : RepositoryBase<Attachment>, IAttachmentRepository
    {
        /// <summary>
        /// 初始化公共附件仓储
        /// </summary>
        /// <param name="unitOfWork">工作单元</param>
        public AttachmentRepository(IDataUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}