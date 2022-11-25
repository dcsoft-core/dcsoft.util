using DCSoft.Apis.Base;
using DCSoft.Web.Core.Attributes;
using Microsoft.AspNetCore.Mvc;
using Util.Helpers;

namespace DCSoft.Apis.Admin.Commons
{
    /// <summary>
    /// 公共控制器
    /// </summary>
    [Router("commons", "common")]
    public class CommonController : BaseController
    {
        #region 初始化控制器

        /// <summary>
        /// 初始化控制器
        /// </summary>
        /// <param name="logger">日志服务</param>
        public CommonController(ILogger<CommonController> logger)
        {
            _logger = logger;
        }

        #endregion

        #region 属性定义

        /// <summary>
        /// 日志操作者
        /// </summary>
        private readonly ILogger<CommonController> _logger;

        #endregion

        #region 查询服务器时间

        /// <summary>
        /// 查询服务器时间
        /// </summary>
        /// <returns></returns>
        [HttpGet("timestamp")]
        [Login]
        public IActionResult GetTimestampAsync()
        {
            var result = Time.GetUnixTimestamp();
            return Success(result);
        }

        #endregion
    }
}