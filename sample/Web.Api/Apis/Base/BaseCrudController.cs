using Microsoft.AspNetCore.Authorization;
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
        /// 初始化Crud控制器
        /// </summary>
        /// <param name="service">Crud服务</param>
        protected BaseCrudController(ICrudService<TDto, TQuery> service)
            : base(service)
        {
        }
    }
}