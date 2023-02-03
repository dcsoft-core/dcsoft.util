using System;
using System.ComponentModel.DataAnnotations;
using Util.Applications.Dtos;

namespace DCSoft.Applications.Dtos.Logs
{
    /// <summary>
    /// 登录日志参数
    /// </summary>
    public class LoginDto : DtoBase
    {
        /// <summary>
        /// 登录帐号
        ///</summary>
        [Display(Name = "登录帐号")]
        [Required]
        [MaxLength(64)]
        public string LoginName { get; set; }

        /// <summary>
        /// 登录IP地址
        ///</summary>
        [Display(Name = "登录IP地址")]
        [Required]
        [MaxLength(32)]
        public string IpAddress { get; set; }

        /// <summary>
        /// 登录地点
        ///</summary>
        [Display(Name = "登录地点")]
        [Required]
        [MaxLength(256)]
        public string Location { get; set; }

        /// <summary>
        /// 操作系统
        ///</summary>
        [Display(Name = "操作系统")]
        [MaxLength(64)]
        public string OS { get; set; }

        /// <summary>
        /// 登录状态（0成功 1失败）
        ///</summary>
        [Display(Name = "登录状态（0成功 1失败）")]
        public int? Status { get; set; }

        /// <summary>
        /// 提示消息
        ///</summary>
        [Display(Name = "提示消息")]
        [MaxLength(256)]
        public string PromptMsg { get; set; }

        /// <summary>
        /// 浏览器类型
        ///</summary>
        [Display(Name = "浏览器类型")]
        [MaxLength(1024)]
        public string Browser { get; set; }

        /// <summary>
        /// 创建时间
        ///</summary>
        [Display(Name = "创建时间")]
        public DateTime? CreationTime { get; set; }

        /// <summary>
        /// 创建者标识
        ///</summary>
        [Display(Name = "创建者标识")]
        public Guid? CreatorId { get; set; }

        /// <summary>
        /// 创建者
        ///</summary>
        [Display(Name = "创建者")]
        [MaxLength(256)]
        public string Creator { get; set; }

        /// <summary>
        /// 最后修改时间
        ///</summary>
        [Display(Name = "最后修改时间")]
        public DateTime? LastModificationTime { get; set; }

        /// <summary>
        /// 最后修改者标识
        ///</summary>
        [Display(Name = "最后修改者标识")]
        public Guid? LastModifierId { get; set; }

        /// <summary>
        /// 最后修改者
        ///</summary>
        [Display(Name = "最后修改者")]
        [MaxLength(256)]
        public string LastModifier { get; set; }

        /// <summary>
        /// 版本号
        ///</summary>
        [Display(Name = "版本号")]
        public byte[] Version { get; set; }
    }
}