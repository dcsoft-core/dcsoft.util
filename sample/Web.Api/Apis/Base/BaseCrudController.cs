using Microsoft.AspNetCore.Authorization;
using System.Diagnostics;
using System.Text.RegularExpressions;
using Util.Applications;
using Util.Applications.Controllers;
using Util.Applications.Dtos;
using Util.Data.Queries;

namespace DCSoft.Apis.Base
{
    /// <summary>
    /// CRUD接口基类
    /// </summary>
    [Authorize]
    public class BaseCrudController<TDto, TQuery> : CrudControllerBase<TDto, TQuery>
        where TDto : IDto, new()
        where TQuery : IPage
    {
        /// <summary>
        /// 会话
        /// </summary>
        protected virtual Util.Sessions.ISession Session => Util.Sessions.Session.Instance;

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
        /// 初始化Crud控制器
        /// </summary>
        /// <param name="service">Crud服务</param>
        protected BaseCrudController(ICrudService<TDto, TQuery> service)
            : base(service)
        {
        }
    }
}