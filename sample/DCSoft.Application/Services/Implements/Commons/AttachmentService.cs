using DCSoft.Applications.Dtos.Commons;
using DCSoft.Applications.Services.Abstractions.Commons;
using DCSoft.Data;
using DCSoft.Data.Queries.Commons;
using DCSoft.Domain.Repositories.Commons;
using System;
using System.Linq;
using DCSoft.Domain.Models.Commons;
using Util.Applications;
using Util.Data;
using System.Collections.Generic;
using System.Threading.Tasks;
using Util;

namespace DCSoft.Applications.Services.Implements.Commons
{
    /// <summary>
    /// 公共附件服务
    /// </summary>
    public class AttachmentService : CrudServiceBase<Attachment, AttachmentDto, AttachmentQuery>, IAttachmentService
    {
        /// <summary>
        /// 初始化公共附件服务
        /// </summary>
        /// <param name="serviceProvider">服务提供器</param>
        /// <param name="unitOfWork">工作单元</param>
        /// <param name="repository">仓储</param>
        public AttachmentService(IServiceProvider serviceProvider, 
            IDataUnitOfWork unitOfWork, 
            IAttachmentRepository repository) : base(serviceProvider, unitOfWork, repository)
        {
            _attachmentRepository = repository;
        }

        /// <summary>
        /// 附件信息仓储
        /// </summary>
        private readonly IAttachmentRepository _attachmentRepository;

        /// <inheritdoc />
        protected override IQueryable<Attachment> Filter(IQueryable<Attachment> queryable, AttachmentQuery param)
        {
            return base.Filter(queryable, param)
                .WhereIfNotEmpty(t => t.ActualName.Contains(param.ActualName))
                .WhereIfNotEmpty(t => t.ObjectType == param.ObjectType);
        }

        /// <summary>
        /// 按照ObjectId删除附件
        /// </summary>
        /// <param name="objectId">对象标识</param>
        /// <returns></returns>
        public async Task<bool> DeleteAsync(Guid objectId)
        {
            var result = await _attachmentRepository.FindAllAsync(t => t.ObjectId == objectId);
            await _attachmentRepository.RemoveAsync(result);
            return true;
        }

        /// <summary>
        /// 查询附件列表
        /// </summary>
        /// <param name="query">查询对象</param>
        /// <returns></returns>
        public async Task<List<AttachmentDto>> ListQueryAsync(AttachmentQuery query)
        {
            var list = await _attachmentRepository.FindAllAsync(t => t.ObjectId == query.ObjectId);
            var result = list.MapToList<AttachmentDto>().OrderBy(t => t.TypeCode).ToList();
            return result;
        }
    }
}