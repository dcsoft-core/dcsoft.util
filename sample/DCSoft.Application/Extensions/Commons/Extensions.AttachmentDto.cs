using DCSoft.Applications.Dtos.Commons;
using DCSoft.Domain.Models.Commons;
using Util;

namespace DCSoft.Applications.Extensions.Commons
{
    /// <summary>
    /// 附件信息参数扩展
    /// </summary>
    public static class AttachmentDtoExtension
    {
        /// <summary>
        /// 转换为附件信息实体
        /// </summary>
        /// <param name="dto">附件信息参数</param>
        public static Attachment ToEntity(this AttachmentDto dto)
        {
            if (dto == null)
                return new Attachment();
            return dto.MapTo(new Attachment(dto.Id.ToGuid()));
        }

        /// <summary>
        /// 转换为附件信息参数
        /// </summary>
        /// <param name="entity">附件信息实体</param>
        public static AttachmentDto ToDto(this Attachment entity)
        {
            if (entity == null)
                return new AttachmentDto();
            return entity.MapTo<AttachmentDto>();
        }
    }
}