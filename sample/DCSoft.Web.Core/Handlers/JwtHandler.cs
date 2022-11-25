using DCSoft.Integration.Options.Token;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using System.Threading.Tasks;
using DCSoft.Applications.Services.Abstractions.Systems;
using Util.Authorization;
using Util.DataEncryption;
using Util.Extensions;
using Util.Helpers;
using Util.Sessions;
using ISession = Util.Sessions.ISession;
using LoginAttribute = DCSoft.Web.Core.Attributes.LoginAttribute;

namespace DCSoft.Web.Core.Handlers
{
    /// <summary>
    /// Jwt认证处理
    /// </summary>
    public class JwtHandler : AppAuthorizeHandler
    {
        /// <summary>
        /// 重写 Handler 添加自动刷新
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task HandleAsync(AuthorizationHandlerContext context)
        {
            var jwtSetting = Config.Get<JWTSettingsOptions>("JWTSettings");
            var refreshTokenSetting = Config.Get<RefreshTokenSettingOptions>("RefreshTokenSetting");

            // 自动刷新Token
            if (JWTEncryption.AutoRefreshToken(context, context.GetCurrentHttpContext(),
                    jwtSetting.ExpiredTime,
                    refreshTokenSetting.ExpiredTime))
            {
                await AuthorizeHandleAsync(context);
            }
            else
            {
                context.Fail(); // 授权失败
                DefaultHttpContext currentHttpContext = context.GetCurrentHttpContext();
                if (currentHttpContext == null)
                    return;
                currentHttpContext.SignoutToSwagger();
            }
        }

        /// <summary>
        /// 授权判断逻辑，授权通过返回 true，否则返回 false
        /// </summary>
        /// <param name="context"></param>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public override async Task<bool> PipelineAsync(AuthorizationHandlerContext context, DefaultHttpContext httpContext)
        {
            if (context.Resource!.GetType() != typeof(DefaultHttpContext))
            {
                var filterContext = (AuthorizationFilterContext)context.Resource;
                //排除匿名访问接口
                if (filterContext.ActionDescriptor.EndpointMetadata.Any(m => m.GetType() == typeof(AllowAnonymousAttribute)))
                    return true;

                //排除登录即可访问的接口
                if (filterContext.ActionDescriptor.EndpointMetadata.Any(m => m.GetType() == typeof(LoginAttribute)))
                    return true;

                // 此处已经自动验证 Jwt Token的有效性了，无需手动验证
                return await CheckAccreditedAsync(filterContext, httpContext);
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// 检查权限
        /// </summary>
        /// <param name="filterContext"></param>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        private static async Task<bool> CheckAccreditedAsync(AuthorizationFilterContext filterContext, DefaultHttpContext httpContext)
        {
            var session = Ioc.Create<ISession>();

            // 登录验证
            if (session.UserId.IsNull())
            {
                return false;
            }

            if (session.GetUserName().Equals("admin")) return true;

            //权限验证
            var httpMethod = filterContext.HttpContext.Request.Method;
            var api = filterContext.ActionDescriptor.AttributeRouteInfo!.Template!.StartsWith("/")
                ? filterContext.ActionDescriptor.AttributeRouteInfo.Template
                : "/" + filterContext.ActionDescriptor.AttributeRouteInfo.Template;
            var permissionService = Ioc.Create<IPermissionService>();
            var isValid = await permissionService.Validate(api, httpMethod);

            return await Task.Run(() => isValid);
        }
    }
}