using System;

namespace DCSoft.Applications.Responses.Systems.AuthResult
{
    /// <summary>
    /// 刷新Token信息
    /// </summary>
    public class RefreshToken
    {
        /// <summary>
        /// 客户端ID
        /// </summary>
        public string ClientId { get; set; }

        /// <summary>
        /// 标识值
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// 过期时间
        /// </summary>
        public DateTime EndUtcTime { get; set; }
    }
}