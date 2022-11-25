using DCSoft.Applications.Dtos.Commons;
using DCSoft.Domain.Models.Commons;
using Util;

namespace DCSoft.Applications.Extensions.Commons
{
    /// <summary>
    /// 系统参数参数扩展
    /// </summary>
    public static class ParametersDtoExtension
    {
        /// <summary>
        /// 转换为系统参数实体
        /// </summary>
        /// <param name="dto">系统参数参数</param>
        public static Parameters ToEntity(this ParametersDto dto)
        {
            if (dto == null)
                return new Parameters();
            return dto.MapTo(new Parameters(dto.Id.ToGuid()));
        }

        /// <summary>
        /// 转换为系统参数参数
        /// </summary>
        /// <param name="entity">系统参数实体</param>
        public static ParametersDto ToDto(this Parameters entity)
        {
            if (entity == null)
                return new ParametersDto();
            return entity.MapTo<ParametersDto>();
        }
    }
}