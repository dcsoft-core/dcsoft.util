using DCSoft.Applications.Dtos.Systems;
using DCSoft.Domain.Enums;
using DCSoft.Domain.Extends;
using Util;
using Util.Helpers;

namespace DCSoft.Applications.Extensions.Systems
{
    /// <summary>
    /// 应用程序参数扩展
    /// </summary>
    public static partial class Extension
    {
        /// <summary>
        /// 转成应用程序参数
        /// </summary>
        public static ApplicationDto ToDto(this Domain.Models.Systems.Application model)
        {
            if (model == null)
                return null;
            var result = model.MapTo<ApplicationDto>();
            var extend = Json.ToObject<ApplicationExtend>(model.Extend);
            if (extend == null)
                return result;
            extend.MapTo(result);
            if (extend.IsClient)
            {
                extend.Client.MapTo(result);
            }

            return result;
        }

        /// <summary>
        /// 转成应用程序实体
        /// </summary>
        public static Domain.Models.Systems.Application ToEntity(this ApplicationDto dto)
        {
            if (dto == null)
                return null;
            var result = dto.MapTo<Domain.Models.Systems.Application>();
            dto.MapTo(result.Client);
            result.IsClient = dto.ApplicationType == ApplicationType.Client;
            result.Client.AccessTokenLifetime = dto.AccessTokenLifetime;
            result.Client.AllowedScopes = dto.AllowedScopes;

            return result;
        }

        /// <summary>
        /// 转换为应用程序持久化对象
        /// </summary>
        /// <param name="entity">应用程序实体</param>
        public static Domain.Models.Systems.Application ToPo(this Domain.Models.Systems.Application entity)
        {
            if (entity == null)
                return null;
            var result = entity.MapTo<Domain.Models.Systems.Application>();
            result.Extend = Json.ToJson(CreateExtend(entity));
            return result;
        }

        /// <summary>
        /// 创建扩展
        /// </summary>
        private static ApplicationExtend CreateExtend(Domain.Models.Systems.Application entity)
        {
            return entity.MapTo<ApplicationExtend>();
        }
    }
}