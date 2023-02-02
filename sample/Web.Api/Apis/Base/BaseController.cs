using Microsoft.AspNetCore.Authorization;
using System.Diagnostics;
using System.Text.RegularExpressions;
using Util.Applications.Controllers;
using ISession = Util.Sessions.ISession;

namespace DCSoft.Apis.Base
{
    /// <summary>
    /// 接口基类
    /// </summary>
    [Authorize]
    public class BaseController : WebApiControllerBase
    {
        /// <summary>
        /// 会话
        /// </summary>
        protected virtual ISession Session => Util.Sessions.Session.Instance;

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
    }
}