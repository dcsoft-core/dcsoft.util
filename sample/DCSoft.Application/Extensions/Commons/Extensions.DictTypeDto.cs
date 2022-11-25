using DCSoft.Applications.Dtos.Commons;
using DCSoft.Domain.Models.Commons;
using Util;

namespace DCSoft.Applications.Extensions.Commons
{
    /// <summary>
    /// 字典类型数据传输对象扩展
    /// </summary>
    public static class DictTypeDtoExtension
    {
        /// <summary>
        /// 转换为字典类型实体
        /// </summary>
        /// <param name="dto">字典类型数据传输对象</param>
        public static DictType ToEntity(this DictTypeDto dto)
        {
            if (dto == null)
                return new DictType();
            return dto.MapTo(new DictType(dto.Id.ToGuid()));
        }

        /// <summary>
        /// 转换为字典类型数据传输对象
        /// </summary>
        /// <param name="entity">字典类型实体</param>
        public static DictTypeDto ToDto(this DictType entity)
        {
            if (entity == null)
                return new DictTypeDto();
            return entity.MapTo<DictTypeDto>();
        }
    }
}