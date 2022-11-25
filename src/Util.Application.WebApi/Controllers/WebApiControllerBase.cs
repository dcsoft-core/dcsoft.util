using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;
using System.Text.RegularExpressions;
using Util.Applications.Filters;
using Util.Properties;
using Util.Sessions;

namespace Util.Applications.Controllers {
    /// <summary>
    /// WebApi控制器基类
    /// </summary>
    [ApiController]
    [Route( "api/[controller]" )]
    [ExceptionHandler]
    [ErroLogFilter]
    public abstract class WebApiControllerBase : ControllerBase
    {
        /// <summary>
        /// 会话
        /// </summary>
        protected virtual ISession Session => Sessions.Session.Instance;

        /// <summary>
        /// 当前方法名
        /// </summary>
        /// <returns></returns>
        protected string CurrentMethodName
        {
            get
            {
                //var typeName = GetType().Name; //类名
                var stackTrace = new StackTrace(true);
                var method = stackTrace.GetFrame(1)?.GetMethod(); //方法名
                var result = $"{method?.DeclaringType?.Name}";
                var rx = new Regex(@"(?<=\<)[^}]*(?=\>)", RegexOptions.Compiled | RegexOptions.IgnoreCase);
                var matches = rx.Matches(result);
                if (matches.Count > 0)
                    result = matches[0].Value;
                return result;
            }
        }
        /// <summary>
        /// 返回成功消息
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="message">消息</param>
        /// <param name="statusCode">Http状态码</param>
        protected virtual IActionResult Success( dynamic data = null, string message = null, int? statusCode = 200 ) {
            message ??= R.Success;
            return GetResult( StateCode.Ok, message, data, statusCode );
        }

        /// <summary>
        /// 获取结果
        /// </summary>
        private IActionResult GetResult( string code, string message, dynamic data, int? httpStatusCode ) {
            var resultFactory = HttpContext.RequestServices.GetService<IResultFactory>();
            if ( resultFactory == null )
                return new Result( code, message, data, httpStatusCode );
            return resultFactory.CreateResult( code, message, data, httpStatusCode );
        }

        /// <summary>
        /// 返回失败消息
        /// </summary>
        /// <param name="message">消息</param>
        /// <param name="statusCode">Http状态码</param>
        protected virtual IActionResult Fail( string message, int? statusCode = 200 ) {
            return GetResult( StateCode.Fail, message, null, statusCode );
        }
    }
}
