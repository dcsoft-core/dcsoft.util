using DCSoft.Applications.Dtos.Systems;
using DCSoft.Domain.Models.Systems;
using Util;

namespace DCSoft.Applications.Extensions.Systems
{
    /// <summary>
    /// 用户数据传输对象扩展
    /// </summary>
    public static class UserDtoExtension
    {
        /// <summary>
        /// 转换为实体
        /// </summary>
        /// <param name="dto">声明数据传输对象</param>
        public static User ToEntity(this UserDto dto)
        {
            if (dto == null)
                return new User();
            return dto.MapTo(new User(dto.Id.ToGuid()));
        }

        /// <summary>
        /// 转换为数据传输对象
        /// </summary>
        /// <param name="entity">声明实体</param>
        public static UserDto ToDto(this User entity)
        {
            if (entity == null)
                return new UserDto();
            return entity.MapTo<UserDto>();
        }
    }
}