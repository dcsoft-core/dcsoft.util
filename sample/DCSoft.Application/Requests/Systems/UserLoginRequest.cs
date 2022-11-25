using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DCSoft.Applications.Requests.Systems
{
    /// <summary>
    /// 用户登录对象
    /// </summary>
    public class UserLoginRequest
    {
        /// <summary>
        /// 登录用户名
        /// </summary>
        [Display(Name = "登录用户名")]
        [Required(ErrorMessage = "用户名不能为空")]
        [JsonPropertyName("userName")]
        public string UserName { get; set; }

        /// <summary>
        /// 登录密码Key
        /// </summary>
        [Display(Name = "登录密码Key")]
        [JsonPropertyName("passwordKey")]
        public string PasswordKey { get; set; }

        /// <summary>
        /// 登录密码
        /// </summary>
        [Display(Name = "登录密码")]
        [JsonPropertyName("password")]
        [Required(ErrorMessage = "密码不能为空")]
        public string Password { get; set; }

        /// <summary>
        /// 验证码Key
        /// </summary>
        [Display(Name = "验证码Key")]
        [JsonPropertyName("verifyCodeKey")]
        public string VerifyCodeKey { get; set; }

        /// <summary>
        /// 验证码
        /// </summary>
        [Display(Name = "验证码")]
        [JsonPropertyName("verifyCode")]
        public string VerifyCode { get; set; }

        /// <summary>
        /// 客户端编码
        /// </summary>
        public string ClientId { get; set; }
    }
}