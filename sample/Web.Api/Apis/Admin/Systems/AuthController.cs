using DCSoft.Apis.Base;
using DCSoft.Applications.Requests.Systems;
using DCSoft.Applications.Services.Abstractions.Systems;
using DCSoft.Web.Core.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Util.Applications.Properties;
using Util.Exceptions;

namespace DCSoft.Apis.Admin.Systems
{
    /// <summary>
    /// 认证控制器
    /// </summary>
    [Router("systems", "auth")]
    public class AuthController : BaseController
    {
        #region 初始化控制器

        /// <summary>
        /// 初始化控制器
        /// </summary>
        /// <param name="securityService">安全服务</param>
        public AuthController(ISecurityService securityService)
        {
            _securityService = securityService;
        }

        #endregion

        #region 属性定义

        /// <summary>
        /// 安全服务
        /// </summary>
        private readonly ISecurityService _securityService;

        #endregion

        #region 登录

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> LoginAsync([FromBody] UserLoginRequest request)
        {
            if (request == null)
                throw new Warning(WebApiResource.RequestIsEmpty);
            var result = await _securityService.SignInAsync(request);
            return Success(result);
        }

        #endregion

        #region 刷新token

        /// <summary>
        /// 刷新令牌
        /// </summary>
        [HttpPost("refresh")]
        [Login]
        public async Task<IActionResult> RefreshTokenAsync()
        {
            var result = await _securityService.RefreshTokenAsync();
            return Success(result);
        }

        #endregion

        #region 获取验证码

        /// <summary>
        /// 获取验证码
        /// </summary>
        /// <param name="lastKey">上次验证码键</param>
        /// <returns></returns>
        [HttpGet("verifyCode")]
        [AllowAnonymous]
        public async Task<IActionResult> GetVerifyCode(string lastKey)
        {
            var result = await _securityService.GetVerifyCodeAsync(lastKey);
            return Success(result);
        }

        #endregion

        #region 获取密码密钥

        /// <summary>
        /// 获取密码密钥
        /// </summary>
        /// <returns></returns>
        [HttpGet("encryptKey")]
        [AllowAnonymous]
        public async Task<IActionResult> GetPassWordEncryptKey()
        {
            var result = await _securityService.GetEncryptKeyAsync();
            return Success(result);
        }

        #endregion

        #region 注销

        /// <summary>
        /// 注销
        /// </summary>
        /// <returns></returns>
        [HttpGet("logout")]
        [Login]
        public async Task<IActionResult> LogoutAsync()
        {
            await _securityService.SignOutAsync();
            return Success();
        }

        #endregion
    }
}
