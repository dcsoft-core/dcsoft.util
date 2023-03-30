using Microsoft.AspNetCore.Authorization;
using System.Diagnostics;
using System.Text.RegularExpressions;
using Util.Applications.Trees;
using Util.Data.Trees;
using Util.Ui.NgZorro.Controllers;

namespace DCSoft.Apis.Base
{
    /// <summary>
    /// 接口基类
    /// </summary>
    [Authorize]
    public class BaseTreeController<TDto, TQuery> : NgZorroTreeControllerBase<TDto, TQuery>
        where TDto : TreeDtoBase<TDto>, new() where TQuery : class, ITreeQueryParameter
    {
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
        /// 初始化
        /// </summary>
        /// <param name="service"></param>
        public BaseTreeController(ITreeService<TDto, TQuery> service) : base(service)
        {
        }
    }
}