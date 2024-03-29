﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using DCSoft.Applications.Requests.Systems;
using DCSoft.Applications.Responses.Systems.AuthResult;
using DCSoft.Applications.Services.Abstractions.Systems;
using DCSoft.Integration.Options.Token;
using DCSoft.Logging.Serilog;
using Microsoft.AspNetCore.Http;
using Util.Caching;
using Util.Exceptions;
using Util.Extras.Authorization;
using Util.Extras.Extensions;
using Util.Extras.Sessions;
using Util.Extras.Security.Claims;
using Util.Extras.Tools.Captcha;
using Util.Helpers;
using CacheKey = DCSoft.Integration.Cache.CacheKey;
using CacheOptions = Util.Caching.CacheOptions;
using Encrypt = Util.Extras.Helpers.Encrypt;
using Random = Util.Extras.Helpers.Random;

namespace DCSoft.Applications.Services.Implements.Systems
{
    /// <summary>
    /// 安全服务
    /// </summary>
    public class SecurityService : ISecurityService
    {
        /// <summary>
        /// 初始化安全服务
        /// </summary>
        /// <param name="logger">日志记录</param>
        /// <param name="applicationService">应用程序服务</param>
        /// <param name="httpContextAccessor">Http上下文访问器</param>
        /// <param name="session">当前会话</param>
        /// <param name="cache">缓存服务</param>
        /// <param name="captcha">验证码类</param>
        /// <param name="userService">用户服务</param>
        public SecurityService(ILogger logger,
            IApplicationService applicationService,
            IHttpContextAccessor httpContextAccessor,
            Util.Sessions.ISession session,
            IRedisCache cache,
            IImageSharpCaptcha captcha,
            IUserService userService)
        {
            _logger = logger;
            _applicationService = applicationService;
            _httpContextAccessor = httpContextAccessor;
            _session = session;
            _cache = cache;
            _captcha = captcha;
            _userService = userService;
        }

        /// <summary>
        /// 日志操作者
        /// </summary>
        private readonly ILogger _logger;

        /// <summary>
        /// 应用程序服务
        /// </summary>
        private readonly IApplicationService _applicationService;

        /// <summary>
        /// Http上下文访问器
        /// </summary>
        private readonly IHttpContextAccessor _httpContextAccessor;

        /// <summary>
        /// 当前会话
        /// </summary>
        private readonly Util.Sessions.ISession _session;

        /// <summary>
        /// 缓存服务
        /// </summary>
        private readonly IRedisCache _cache;

        /// <summary>
        /// 验证码类
        /// </summary>
        private readonly IImageSharpCaptcha _captcha;

        /// <summary>
        /// 用户仓储
        /// </summary>
        private readonly IUserService _userService;

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="request">登录参数</param>
        public async Task<JsonWebToken> SignInAsync([Required] UserLoginRequest request)
        {
            _logger.Login(request.UserName, LoginStatus.Failure, "用户名或密码不正确");

            #region 校验验证码

            var verifyCodeKey = string.Format(CacheKey.VerifyCodeKey, request.VerifyCodeKey);
            if (await _cache.ExistsAsync(verifyCodeKey))
            {
                var verifyCode = await _cache.GetAsync<string>(verifyCodeKey, null);
                if (string.IsNullOrEmpty(verifyCode))
                {
                    throw new Warning("验证码已过期！");
                }

                if (!string.Equals(verifyCode, request.VerifyCode, StringComparison.CurrentCultureIgnoreCase))
                {
                    throw new Warning("验证码输入有误！");
                }

                await _cache.RemoveAsync(verifyCodeKey);
            }
            else
            {
                throw new Warning("验证码已过期！");
            }

            #endregion

            #region 校验应用程序编码

            var application = await _applicationService.GetByCodeAsync(request.ClientId);
            if (application == null)
            {
                _logger.Login(request.UserName, LoginStatus.Failure, "应用程序不存在");
                throw new Warning("应用程序不存在");
            }

            #endregion

            #region 校验用户名

            var user = await _userService.FindByNameAsync(request.UserName);
            if (user == null)
            {
                _logger.Login(request.UserName, LoginStatus.Failure, "用户名不存在");
                throw new Warning("用户名不存在");
            }

            #endregion

            #region 解密密码

            if (request.PasswordKey.NotEmpty())
            {
                var passwordEncryptKey = string.Format(CacheKey.PassWordEncryptKey, request.PasswordKey);
                if (await _cache.ExistsAsync(passwordEncryptKey))
                {
                    var secretKey = await _cache.GetAsync<string>(passwordEncryptKey, null);
                    if (secretKey.IsNull())
                    {
                        throw new Warning("解析密码失败,密钥Key不存在.");
                    }

                    request.Password = Encrypt.DesDecrypt(request.Password, secretKey);
                    await _cache.RemoveAsync(passwordEncryptKey);
                }
                else
                {
                    throw new Warning("解析密码失败,密钥Key不存在.");
                }
            }

            #endregion


            var encryptPassword =
                Encrypt.Base64Encrypt(Encrypt.HmacSha256(Encrypt.AesEncrypt(request.Password), user.SecurityStamp));

            if (!encryptPassword.Equals(user.PasswordHash))
            {
                await _userService.UpdateFailLoginAsync(user);
                _logger.Login(request.UserName, LoginStatus.Failure, "用户名或密码不正确");
                throw new Warning("用户名或密码不正确");
            }

            if (!user.Enabled)
            {
                _logger.Login(request.UserName, LoginStatus.Failure, "此用户未启用，请联系管理员");
                throw new Warning("此用户未启用，请联系管理员");
            }

            if (user.LockoutEnabled && DateTime.Now <= user.LockoutEnd)
            {
                _logger.Login(request.UserName, LoginStatus.Failure, "此用户当前被锁定，请联系管理员");
                throw new Warning("此用户当前被锁定，请联系管理员");
            }

            await _userService.UpdateSuccessLoginAsync(user);

            var dicClaim = new Dictionary<string, object>()
            {
                { ClaimTypes.UserId, user.Id },
                { ClaimTypes.UserName, user.UserName },
                { ClaimTypes.Email, user.Email },
                { ClaimTypes.Mobile, user.PhoneNumber },
                { ClaimTypes.NickName, user.NickName },
                { ClaimTypes.UserType, user.UserType },
                { ClaimTypes.ApplicationId, application.Id },
                { ClaimTypes.ApplicationCode, application.Code },
                { ClaimTypes.ApplicationName, application.Name },
            };

            var accessToken = JWTEncryption.Encrypt(dicClaim, application.AccessTokenLifetime / 60);

            // 设置Swagger自动登录
            _httpContextAccessor.HttpContext.SigninToSwagger(accessToken);

            // 生成刷新Token令牌
            var refreshTokenExpire = Config.Get<RefreshTokenSettingOptions>("RefreshTokenSetting").ExpiredTime;
            var refreshToken =
                JWTEncryption.GenerateRefreshToken(accessToken, refreshTokenExpire);


            var result = new JsonWebToken()
            {
                AccessToken = accessToken,
                AccessTokenUtcExpires = application.AccessTokenLifetime,
                RefreshToken = refreshToken,
                RefreshUtcExpires = refreshTokenExpire,
                TokenType = "Bearer"
            };
            _logger.Login(request.UserName, LoginStatus.Success, "登录成功");
            return result;
        }

        /// <summary>
        /// 刷新Token
        /// </summary>
        /// <returns></returns>
        public async Task<JsonWebToken> RefreshTokenAsync()
        {
            var result = new JsonWebToken();
            var refreshToken = Util.Extras.Helpers.Web.Header("refresh_token");
            var validate = JWTEncryption.Validate(refreshToken);
            if (validate.IsValid)
            {
                var dicClaim = new Dictionary<string, object>()
                {
                    { ClaimTypes.UserId, _session.UserId },
                    { ClaimTypes.UserName, _session.GetUserName() },
                    { ClaimTypes.Email, _session.GetEmail() },
                    { ClaimTypes.Mobile, _session.GetMobile() },
                    { ClaimTypes.NickName, _session.GetNickName() },
                    { ClaimTypes.UserType, _session.GetUserType() },
                    { ClaimTypes.ApplicationId, _session.GetApplicationId() },
                    { ClaimTypes.ApplicationCode, _session.GetApplicationCode() },
                    { ClaimTypes.ApplicationName, _session.GetApplicationName() },
                };

                var application = await _applicationService.GetByCodeAsync(_session.GetApplicationCode());
                var accessToken = JWTEncryption.Encrypt(dicClaim, application.AccessTokenLifetime / 60);

                // 设置Swagger自动登录
                _httpContextAccessor.HttpContext.SigninToSwagger(accessToken);

                // 生成刷新Token令牌
                var refreshTokenExpire = Config.Get<RefreshTokenSettingOptions>("RefreshTokenSetting").ExpiredTime;
                var newRefreshToken = JWTEncryption.GenerateRefreshToken(accessToken, refreshTokenExpire);

                result.AccessToken = accessToken;
                result.AccessTokenUtcExpires = application.AccessTokenLifetime;
                result.RefreshToken = newRefreshToken;
                result.RefreshUtcExpires = refreshTokenExpire;
                result.TokenType = "Bearer";
            }

            return result;
        }

        /// <summary>
        /// 获取验证码
        /// </summary>
        /// <param name="lastKey"></param>
        /// <returns></returns>
        public Task<AuthVerifyCodeResp> GetVerifyCodeAsync(string lastKey)
        {
            var code = _captcha.GetRandomEnDigitalText(4);
            var img = _captcha.GetEnDigitalCodeByte(code).ToBase64();

            //删除上次缓存的验证码
            if (!lastKey.IsNull())
            {
                _cache.Remove(lastKey);
            }

            //写入Redis
            var guid = Id.Create();
            var key = string.Format(CacheKey.VerifyCodeKey, guid);
            _cache.TrySet(key, code, new CacheOptions() { Expiration = TimeSpan.FromMinutes(5) });

            var result = new AuthVerifyCodeResp { Key = guid, Img = img };
            return Task.FromResult(result);
        }

        /// <summary>
        /// 获取密码密钥
        /// </summary>
        /// <returns></returns>
        public Task<AuthEncryptKeyResp> GetEncryptKeyAsync()
        {
            //写入Redis
            var guid = Id.Create();
            var key = string.Format(CacheKey.PassWordEncryptKey, guid);
            var encryptKey = Random.GenerateRandom(8);
            _cache.TrySet(key, encryptKey, new CacheOptions() { Expiration = TimeSpan.FromMinutes(5) });
            var data = new AuthEncryptKeyResp { Key = guid, EncryptKey = encryptKey };

            return Task.FromResult(data);
        }

        /// <summary>
        /// 退出登录
        /// </summary>
        public Task SignOutAsync()
        {
            _httpContextAccessor.HttpContext.SignOutToSwagger();
            return Task.CompletedTask;
        }
    }
}