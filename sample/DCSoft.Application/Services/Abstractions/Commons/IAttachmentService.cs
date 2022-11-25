using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DCSoft.Applications.Dtos.Commons;
using DCSoft.Data.Queries.Commons;
using Util.Applications;

namespace DCSoft.Applications.Services.Abstractions.Commons
{
    /// <summary>
    /// 公共附件服务
    /// </summary>
    public interface IAttachmentService : ICrudService<AttachmentDto, AttachmentQuery>
    {
        /// <summary>
        /// 按照ObjectId删除附件
        /// </summary>
        /// <param name="objectId">对象标识</param>
        /// <returns></returns>
        Task<bool> DeleteAsync(Guid objectId);

        /// <summary>
        /// 查询附件列表
        /// </summary>
        /// <param name="query">查询对象</param>
        /// <returns></returns>
        Task<List<AttachmentDto>> ListQueryAsync(AttachmentQuery query);
    }
}