using DCSoft.Domain.Enums;
using DCSoft.Domain.Extends;
using DCSoft.Domain.Models.Systems;
using Util;
using Util.Helpers;

namespace DCSoft.Domain.Extensions
{
    /// <summary>
    /// 资源对象扩展
    /// </summary>
    public static partial class Extension
    {
        /// <summary>
        /// 转换为模块
        /// </summary>
        public static Module ToModule(this Resource po)
        {
            if (po == null)
                return null;
            if (po.Type != ResourceType.Module && po.Type != ResourceType.Operation)
                return null;
            var result = po.MapTo(new Module(po.Id, po.Path, po.Level));
            result.Url = po.Uri;
            var extend = Json.ToObject<ModuleExtend>(po.Extend);
            extend.MapTo(result);
            return result;
        }

        /// <summary>
        /// 转换为资源对象
        /// </summary>
        public static Resource ToResource(this Module entity)
        {
            if (entity == null)
                return null;
            var result = entity.MapTo<Resource>();
            result.Uri = entity.Url;
            result.Extend = Json.ToJson(CreateExtend(entity));
            return result;
        }

        /// <summary>
        /// 创建模块扩展对象
        /// </summary>
        private static ModuleExtend CreateExtend(Module entity)
        {
            return new ModuleExtend
            {
                Icon = entity.Icon,
                Expanded = entity.Expanded,
                Method = entity.Method
            };
        }
    }
}