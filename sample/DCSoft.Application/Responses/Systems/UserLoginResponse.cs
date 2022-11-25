using System.Text.Json.Serialization;

namespace DCSoft.Applications.Responses.Systems
{
    /// <summary>
    /// 用户登录响应
    /// </summary>
    public class UserLoginResponse
    {
        /// <summary>
        /// 授权Token
        /// </summary>
        [JsonPropertyName("access_token")]
        public string AccessToken { get; set; }

        /// <summary>
        /// 刷新Token
        /// </summary>
        [JsonPropertyName("refresh_token")]
        public string RefreshToken { get; set; }

        /// <summary>
        /// 有效期
        /// </summary>
        [JsonPropertyName("expires_in")]
        public string ExpiresIn { get; set; }

        /// <summary>
        /// Token类型
        /// </summary>
        [JsonPropertyName("token_type")]
        public string TokenType { get; set; }

        /// <summary>
        /// 会话范围
        /// </summary>
        [JsonPropertyName("scope")]
        public string Scope { get; set; }

        /// <summary>
        /// 错误
        /// </summary>
        [JsonPropertyName("error")]
        public string Error { get; set; }

        /// <summary>
        /// 错误描述
        /// </summary>
        [JsonPropertyName("error_description")]
        public string ErrorDescription { get; set; }
    }
}