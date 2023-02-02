using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.IdentityModel.Tokens;
using Util.Extras.Authorization;
using Util.Helpers;

// ReSharper disable InconsistentNaming
// ReSharper disable CheckNamespace
// ReSharper disable PossibleNullReferenceException
// ReSharper disable IdentifierTypo
// ReSharper disable PossibleInvalidOperationException
// ReSharper disable StringLiteralTypo

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// JWT 授权服务拓展类
/// </summary>
public static class JWTAuthorizationServiceCollectionExtensions
{
    /// <summary>
    /// 添加 JWT 授权
    /// </summary>
    /// <param name="authenticationBuilder"></param>
    /// <param name="tokenValidationParameters">token 验证参数</param>
    /// <param name="jwtBearerConfigure"></param>
    /// <param name="enableGlobalAuthorize">启动全局授权</param>
    /// <returns></returns>
    public static AuthenticationBuilder AddJwt(this AuthenticationBuilder authenticationBuilder, object tokenValidationParameters = default, Action<JwtBearerOptions> jwtBearerConfigure = null, bool enableGlobalAuthorize = false)
    {
        // 配置 JWT 选项
        ConfigureJWTOptions(authenticationBuilder.Services);

        // 添加授权
        authenticationBuilder.AddJwtBearer(options =>
        {
            // 反射获取全局配置
            var jwtSettings = Config.Get<JWTSettingsOptions>("JWTSettings");

            // 配置 JWT 验证信息
            options.TokenValidationParameters = (tokenValidationParameters as TokenValidationParameters) ?? JWTEncryption.CreateTokenValidationParameters(jwtSettings);

            // 添加自定义配置
            jwtBearerConfigure?.Invoke(options);
        });

        //启用全局授权
        if (enableGlobalAuthorize)
        {
            authenticationBuilder.Services.Configure<MvcOptions>(options =>
            {
                options.Filters.Add(new AuthorizeFilter());
            });
        }

        return authenticationBuilder;
    }

    /// <summary>
    /// 添加 JWT 授权
    /// </summary>
    /// <param name="services"></param>
    /// <param name="authenticationConfigure">授权配置</param>
    /// <param name="tokenValidationParameters">token 验证参数</param>
    /// <param name="jwtBearerConfigure"></param>
    /// <returns></returns>
    public static AuthenticationBuilder AddJwt(this IServiceCollection services, Action<AuthenticationOptions> authenticationConfigure = null, object tokenValidationParameters = default, Action<JwtBearerOptions> jwtBearerConfigure = null)
    {
        // 添加默认授权
        var authenticationBuilder = services.AddAuthentication(options =>
         {
             options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
             options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

             // 添加自定义配置
             authenticationConfigure?.Invoke(options);
         });

        AddJwt(authenticationBuilder, tokenValidationParameters, jwtBearerConfigure);

        return authenticationBuilder;
    }

    /// <summary>
    /// 添加 JWT 授权
    /// </summary>
    /// <typeparam name="TAuthorizationHandler"></typeparam>
    /// <param name="services"></param>
    /// <param name="authenticationConfigure"></param>
    /// <param name="tokenValidationParameters"></param>
    /// <param name="jwtBearerConfigure"></param>
    /// <param name="enableGlobalAuthorize"></param>
    /// <returns></returns>
    public static AuthenticationBuilder AddJwt<TAuthorizationHandler>(this IServiceCollection services, Action<AuthenticationOptions> authenticationConfigure = null, object tokenValidationParameters = default, Action<JwtBearerOptions> jwtBearerConfigure = null, bool enableGlobalAuthorize = false)
        where TAuthorizationHandler : class, IAuthorizationHandler
    {
        // 添加策略授权服务
        services.AddAppAuthorization<TAuthorizationHandler>(null, enableGlobalAuthorize);

        // 添加授权
        return services.AddJwt(authenticationConfigure, tokenValidationParameters, jwtBearerConfigure);
    }

    /// <summary>
    /// 添加 JWT 授权
    /// </summary>
    /// <param name="services"></param>
    private static void ConfigureJWTOptions(IServiceCollection services)
    {
        // 配置验证
        services.AddOptions<JWTSettingsOptions>()
                .BindConfiguration("JWTSettings")
                .ValidateDataAnnotations()
                .PostConfigure(options =>
                {
                    _ = JWTEncryption.SetDefaultJwtSettings(options);
                });
    }
}