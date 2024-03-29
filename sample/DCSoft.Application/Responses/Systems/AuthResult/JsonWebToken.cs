﻿using System;
using Util.Extras.Timing;
using Convert = Util.Helpers.Convert;

namespace DCSoft.Applications.Responses.Systems.AuthResult
{
    /// <summary>
    /// JwtToken
    /// </summary>
    public class JsonWebToken
    {
        /// <summary>
        /// 访问令牌。用于业务身份认证的令牌
        /// </summary>
        public string AccessToken { get; set; }

        /// <summary>
        /// 访问令牌有效期。UTC标准
        /// </summary>
        public long AccessTokenUtcExpires { get; set; }

        /// <summary>
        /// 刷新令牌。用于刷新AccessToken的令牌
        /// </summary>
        public string RefreshToken { get; set; }

        /// <summary>
        /// 刷新令牌有效期。UTC标准
        /// </summary>
        public long RefreshUtcExpires { get; set; }

        /// <summary>
        /// 令牌类型
        /// </summary>
        public string TokenType { get; set; }

        /// <summary>
        /// 是否已过期
        /// </summary>
        public bool IsExpired() => Convert.To<long>(DateTime.UtcNow.ToJsGetTime()) > AccessTokenUtcExpires;
    }
}