using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using System;

namespace DCSoft.Web.Core.Attributes
{
    /// <summary>
    /// 自定义路由 /api/[area]/[controler]/[action]
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class RouterAttribute : RouteAttribute, IApiDescriptionGroupNameProvider
    {
        /// <summary>
        /// 分组名称,是来实现接口 IApiDescriptionGroupNameProvider
        /// </summary>
        public string GroupName { get; set; }

        /// <summary>
        /// 自定义路由构造函数，继承基类路由
        /// </summary>
        /// <param name="actionName"></param>
        public RouterAttribute(string actionName = "[action]") :
            base($"/api/[area]/[controller]")
        {
        }

        /// <summary>
        /// 自定义路由构造函数，继承基类路由
        /// </summary>
        /// <param name="area"></param>
        /// <param name="controller"></param>
        public RouterAttribute(string area = "[area]", string controller = "[controller]") :
            base($"/api/{area}/{controller}")
        {
        }
    }
}