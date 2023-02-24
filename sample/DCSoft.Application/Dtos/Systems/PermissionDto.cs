using System;
using System.ComponentModel.DataAnnotations;
using Util.Applications.Dtos;

namespace DCSoft.Applications.Dtos.Systems
{
    /// <summary>
    /// 权限参数
    /// </summary>
    public class PermissionDto : DtoBase
    {
        /// <summary>
        /// 角色标识
        ///</summary>
        [Display(Name = "角色标识")]
        [Required]
        public Guid RoleId { get; set; }

        /// <summary>
        /// 资源标识
        ///</summary>
        [Display(Name = "资源标识")]
        [Required]
        public Guid ResourceId { get; set; }

        /// <summary>
        /// 拒绝
        ///</summary>
        [Display(Name = "拒绝")]
        public bool IsDeny { get; set; }

        /// <summary>
        /// 签名
        ///</summary>
        [Display(Name = "签名")]
        [MaxLength(256)]
        public string Sign { get; set; }

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