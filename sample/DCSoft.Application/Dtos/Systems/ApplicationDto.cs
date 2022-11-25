using DCSoft.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Util;
using Util.Applications.Dtos;

namespace DCSoft.Applications.Dtos.Systems
{
    /// <summary>
    /// 应用程序参数
    /// </summary>
    public class ApplicationDto : DtoBase
    {
        /// <summary>
        /// 应用程序类型
        /// </summary>
        [Display(Name = "应用程序类型")]
        public ApplicationType ApplicationType { get; set; }

        /// <summary>
        /// 应用程序类型
        /// </summary>
        [Display(Name = "应用程序类型")]
        public string ApplicationTypeName => ApplicationType.Description();

        /// <summary>
        /// 应用程序编码
        ///</summary>
        [Display(Name = "应用程序编码")]
        [Required]
        [MaxLength(64)]
        public string Code { get; set; }
        /// <summary>
        /// 应用程序名称
        ///</summary>
        [Display(Name = "应用程序名称")]
        [Required]
        [MaxLength(256)]
        public string Name { get; set; }
        /// <summary>
        /// 启用
        ///</summary>
        [Display(Name = "启用")]
        public bool Enabled { get; set; }
        /// <summary>
        /// 启用注册
        ///</summary>
        [Display(Name = "启用注册")]
        public bool RegisterEnabled { get; set; }

        /// <summary>
        /// 允许的作用域
        /// </summary>
        [Display(Name = "允许的作用域")]
        public List<string> AllowedScopes { get; set; }

        /// <summary>
        /// 访问令牌生命周期
        /// </summary>
        [Display(Name = "访问令牌生命周期")]
        public int AccessTokenLifetime { get; set; }
        /// <summary>
        /// 备注
        ///</summary>
        [Display(Name = "备注")]
        [MaxLength(512)]
        public string Remark { get; set; }
        /// <summary>
        /// 扩展
        ///</summary>
        [Display(Name = "扩展")]
        public string Extend { get; set; }
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