using System.ComponentModel.DataAnnotations;

namespace DCSoft.Applications.Requests.Systems
{
    /// <summary>
    /// 修改密码
    /// </summary>
    public class ChangePwdRequest
    {
        /// <summary>
        /// 旧密码
        /// </summary>
        [Display(Name = "旧密码")]
        [Required(ErrorMessage = "旧密码不能为空")]
        public string OldPassword { get; set; }

        /// <summary>
        /// 新密码
        /// </summary>
        [Display(Name = "新密码")]
        [Required(ErrorMessage = "新密码不能为空")]
        public string NewPassword { get; set; }
    }
}