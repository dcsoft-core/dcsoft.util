using DCSoft.Applications.Dtos.Systems;
using DCSoft.Domain.Models.Systems;
using Util;

namespace DCSoft.Applications.Extensions.Systems
{
    /// <summary>
    /// 声明数据传输对象扩展
    /// </summary>
    public static class ClaimDtoExtension
    {
        /// <summary>
        /// 转换为声明实体
        /// </summary>
        /// <param name="dto">声明数据传输对象</param>
        public static Claim ToEntity(this ClaimDto dto)
        {
            if (dto == null)
                return new Claim();
            return dto.MapTo(new Claim(dto.Id.ToGuid()));
        }

        /// <summary>
        /// 转换为声明数据传输对象
        /// </summary>
        /// <param name="entity">声明实体</param>
        public static ClaimDto ToDto(this Claim entity)
        {
            if (entity == null)
                return new ClaimDto();
            return entity.MapTo<ClaimDto>();
        }
    }
}