using Microsoft.AspNetCore.Authorization;
using Util.Applications.Trees;
using Util.Data.Trees;
using Util.Ui.NgZorro.Controllers;

namespace DCSoft.Apis.Base
{
    /// <summary>
    /// 接口基类
    /// </summary>
    [Authorize]
    public class BaseTreeController<TDto, TQuery> : NgZorroTreeControllerBase<TDto, TQuery> where TDto : TreeDtoBase<TDto>, new() where TQuery : class, ITreeQueryParameter
    {
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="service"></param>
        public BaseTreeController(ITreeService<TDto, TQuery> service) : base(service)
        {
        }
    }
}