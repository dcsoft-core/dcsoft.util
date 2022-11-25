using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace Util.Applications.Middles;

/// <summary>
/// 状态码中间件
/// </summary>
public class UnifyResultStatusCodesMiddleware
{
    /// <summary>
    /// 请求委托
    /// </summary>
    private readonly RequestDelegate _next;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="next"></param>
    public UnifyResultStatusCodesMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    /// <summary>
    /// 中间件执行方法
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public async Task InvokeAsync(HttpContext context)
    {
        await _next(context);

        // 只有请求错误（短路状态码）才支持规范化处理
        if (context.Response.StatusCode < 400 || context.Response.StatusCode == 404) return;

        // 解决刷新 Token 时间和 Token 时间相近问题
        if (context.Response.StatusCode == StatusCodes.Status401Unauthorized
            && context.Response.Headers.ContainsKey("access-token")
            && context.Response.Headers.ContainsKey("x-access-token"))
        {
            context.Response.StatusCode = StatusCodes.Status403Forbidden;
        }

        // 如果 Response 已经完成输出，则禁止写入
        if (context.Response.HasStarted) return;
        await OnResponseStatusCodes(context, context.Response.StatusCode);

    }

    /// <summary>
    /// 特定状态码返回值
    /// </summary>
    /// <param name="context"></param>
    /// <param name="statusCode"></param>
    /// <returns></returns>
    public async Task OnResponseStatusCodes(HttpContext context, int statusCode)
    {
        switch (statusCode)
        {
            // 处理 401 状态码
            case StatusCodes.Status401Unauthorized:
                await context.Response.WriteAsJsonAsync(RestfulResult(StateCode.Fail, "401 Unauthorized", null, statusCode));
                break;
            // 处理 403 状态码
            case StatusCodes.Status403Forbidden:
                await context.Response.WriteAsJsonAsync(RestfulResult(StateCode.Fail, "403 Forbidden", null, statusCode));
                break;

            default: break;
        }
    }

    /// <summary>
    /// 返回 RESTful 风格结果集
    /// </summary>
    /// <param name="code"></param>
    /// <param name="message"></param>
    /// <param name="data"></param>
    /// <param name="httpStatusCode"></param>
    /// <returns></returns>
    public static object RestfulResult(string code, string message, object data = default, int httpStatusCode = default)
    {
        return new { code = code, message = message, data = data };
    }
}

/// <summary>
/// 扩展方法
/// </summary>
public static class EnableUnifyResultStatusCodes
{
    /// <summary>
    /// 启用UnifyResult
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    public static IApplicationBuilder UseUnifyResultStatusCodes(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<UnifyResultStatusCodesMiddleware>();
    }
}