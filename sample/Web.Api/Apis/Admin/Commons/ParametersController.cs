using DCSoft.Apis.Base;
using DCSoft.Applications.Services.Abstractions.Commons;
using DCSoft.Web.Core.Attributes;

namespace DCSoft.Apis.Admin.Commons
{
    /// <summary>
    /// 公共参数控制器
    /// </summary>
    [Router("commons", "param")]
    public class ParametersController : BaseController
    {
        #region 初始化控制器

        /// <summary>
        /// 初始化控制器
        /// </summary>
        /// <param name="logger">日志服务</param>
        /// <param name="parametersService">系统参数服务</param>
        public ParametersController(ILogger logger,
            IParametersService parametersService)
        {
            _logger = logger;
            _parametersService = parametersService;
        }

        #endregion

        #region 属性定义

        /// <summary>
        /// 日志操作者
        /// </summary>
        private readonly ILogger _logger;

        /// <summary>
        /// 系统参数服务
        /// </summary>
        private readonly IParametersService _parametersService;

        #endregion

        #region 查询信息

        ///// <summary>
        ///// 查询信息
        ///// </summary>
        ///// <returns></returns>
        //[HttpGet()]
        //[ProducesResponseType(typeof(SiteSettingDto), 200)]
        //public async Task<IActionResult> GetSiteSettingAsync()
        //{
        //    var result = await SiteSettingService.LoadAsync();
        //    return Success(result);
        //}

        #endregion

        #region 保存参数设置信息

        ///// <summary>
        ///// 保存参数设置信息
        ///// </summary>
        ///// <returns></returns>
        //[HttpPut()]
        //public async Task<IActionResult> SaveSiteSettingAsync([FromBody] SaveParamRequest request)
        //{
        //    if (request == null)
        //        return Fail(WebResource.RequestIsEmpty);
        //    var result = await SysParamService.SaveParamAsync(request.Name, request.Value);
        //    _logger.Operate("系统参数", BusinessType.Save, CurrentMethodName);
        //    return Success(result);
        //}

        #endregion
    }
}