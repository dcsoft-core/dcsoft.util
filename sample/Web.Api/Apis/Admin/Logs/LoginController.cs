using DCSoft.Apis.Base;
using DCSoft.Applications.Dtos.Logs;
using DCSoft.Applications.Services.Abstractions.Logs;
using DCSoft.Data.Queries.Logs;
using DCSoft.Web.Core.Attributes;
using Microsoft.AspNetCore.Mvc;
using Util.Applications.Controllers;
using Util.Applications.Models;
using Util.Data;

namespace DCSoft.Apis.Admin.Logs
{
    /// <summary>
    /// 登录日志控制器
    /// </summary>
    [Router("logs", "login-log")]
    public class LoginController : BaseController
    {
        #region 初始化控制器

        /// <summary>
        /// 初始化控制器
        /// </summary>
        /// <param name="loginService">登录日志服务</param>
        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        #endregion

        #region 属性定义

        /// <summary>
        /// 登录日志服务
        /// </summary>
        private readonly ILoginService _loginService;

        #endregion

        #region 分页查询

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="query">查询参数</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> PagerQueryAsync([FromQuery] LoginQuery query)
        {
            var result = await _loginService.PageQueryAsync(query);
            return Success(result);
        }

        #endregion
    }
}