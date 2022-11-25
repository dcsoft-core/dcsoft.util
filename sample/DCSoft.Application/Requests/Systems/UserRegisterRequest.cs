using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using DCSoft.Domain.Enums;

namespace DCSoft.Applications.Requests.Systems
{
    /// <summary>
    /// 用户注册对象
    /// </summary>
    public class UserRegisterRequest
    {
        /// <summary>
        /// 用户类型
        /// </summary>
        [Display(Name = "用户类型")]
        [Required(ErrorMessage = "用户类型不能为空")]
        [JsonPropertyName("userType")]
        public UserType UserType { get; set; }

        /// <summary>
        /// 登录用户名
        /// </summary>
        [Display(Name = "登录用户名")]
        [Required(ErrorMessage = "用户名不能为空")]
        [JsonPropertyName("userName")]
        public string UserName { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        [Display(Name = "昵称")]
        [Required(ErrorMessage = "昵称不能为空")]
        [JsonPropertyName("nickName")]
        public string NickName { get; set; }

        /// <summary>
        /// 登录密码
        /// </summary>
        [Display(Name = "登录密码")]
        [JsonPropertyName("password")]
        [Required(ErrorMessage = "密码不能不能为空")]
        public string Password { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        [Display(Name = "手机号")]
        [Required(ErrorMessage = "手机号不能为空")]
        [JsonPropertyName("mobile")]
        public string Mobile { get; set; }
    }
}