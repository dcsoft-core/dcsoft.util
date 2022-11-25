using System.ComponentModel.DataAnnotations;

namespace DCSoft.Applications.Requests.Systems
{
    /// <summary>
    /// 修改用户基本信息参数
    /// </summary>
    public class UpdateUserBaseRequest
    {
        /// <summary>
        /// 用户标识
        /// </summary>
        [StringLength(36)]
        [Display(Name = "用户标识")]
        public string Id { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        [StringLength(512)]
        [Display(Name = "头像")]
        public string Avatar { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        [Display(Name = "昵称")]
        [StringLength(256)]
        public string NickName { get; set; }

        /// <summary>
        /// 安全邮箱
        /// </summary>
        [StringLength(256)]
        [EmailAddress]
        [Display(Name = "邮箱")]
        public string Email { get; set; }

        /// <summary>
        /// 安全手机
        /// </summary>
        [StringLength(64)]
        [Phone]
        [Display(Name = "手机号")]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [Display(Name = "备注")]
        [StringLength(512)]
        public string Remark { get; set; }

        /// <summary>
        /// 版本号
        /// </summary>
        [Display(Name = "版本号")]
        public byte[] Version { get; set; }
    }
}