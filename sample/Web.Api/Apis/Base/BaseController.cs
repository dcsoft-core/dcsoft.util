using Microsoft.AspNetCore.Authorization;
using Util.Applications.Controllers;

namespace DCSoft.Apis.Base
{
    /// <summary>
    /// 接口基类
    /// </summary>
    [Authorize]
    public class BaseController : WebApiControllerBase
    {
    }
}