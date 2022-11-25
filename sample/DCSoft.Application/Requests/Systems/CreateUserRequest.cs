using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DCSoft.Applications.Requests.Systems
{
    /// <summary>
    /// 创建用户参数
    /// </summary>
    public class CreateUserRequest
    {
        /// <summary>
        /// 部门标识
        /// </summary>
        [StringLength(36)]
        [Display(Name = "部门标识")]
        public string DepartmentId { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        [StringLength(256)]
        [Display(Name = "用户名")]
        public string UserName { get; set; }

        /// <summary>
        /// 安全邮箱
        /// </summary>
        [StringLength(256)]
        [EmailAddress]
        [Display(Name = "邮箱")]
        public string Email { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        [Display(Name = "昵称")]
        [StringLength(256)]
        public string NickName { get; set; }

        /// <summary>
        /// 用户类型
        /// </summary>
        [Display(Name = "用户类型")]
        public int UserType { get; set; }

        /// <summary>
        /// 角色
        /// </summary>
        [Display(Name = "角色")]
        public List<string> RoleIds { get; set; }

        /// <summary>
        /// 安全手机
        /// </summary>
        [StringLength(64)]
        [Phone]
        [Display(Name = "手机号")]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [Required(ErrorMessage = "密码不能为空")]
        [StringLength(256)]
        [DataType(DataType.Password)]
        [Display(Name = "密码")]
        public string Password { get; set; }

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