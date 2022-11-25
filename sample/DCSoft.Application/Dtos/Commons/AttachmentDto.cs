using System;
using System.ComponentModel.DataAnnotations;
using Util.Applications.Dtos;

namespace DCSoft.Applications.Dtos.Commons
{
    /// <summary>
    /// 公共附件参数
    /// </summary>
    public class AttachmentDto : DtoBase
    {
        /// <summary>
        /// 关联对象标识
        ///</summary>
        [Display(Name = "关联对象标识")]
        public Guid? ObjectId { get; set; }
        /// <summary>
        /// 关联对象类型
        ///</summary>
        [Display(Name = "关联对象类型")]
        [Required]
        [MaxLength(64)]
        public string ObjectType { get; set; }
        /// <summary>
        /// 类型代码
        ///</summary>
        [Display(Name = "类型代码")]
        [MaxLength(128)]
        public string TypeCode { get; set; }
        /// <summary>
        /// 类型名称
        ///</summary>
        [Display(Name = "类型名称")]
        [MaxLength(128)]
        public string TypeName { get; set; }
        /// <summary>
        /// 附件名称
        ///</summary>
        [Display(Name = "附件名称")]
        [MaxLength(128)]
        public string ActualName { get; set; }
        /// <summary>
        /// 文件名称
        ///</summary>
        [Display(Name = "文件名称")]
        [MaxLength(128)]
        public string FileName { get; set; }
        /// <summary>
        /// MIME类型
        ///</summary>
        [Display(Name = "MIME类型")]
        [MaxLength(256)]
        public string MimeType { get; set; }
        /// <summary>
        /// 文件大小
        ///</summary>
        [Display(Name = "文件大小")]
        public int? FileSize { get; set; }
        /// <summary>
        /// 扩展名
        ///</summary>
        [Display(Name = "扩展名")]
        [MaxLength(32)]
        public string ExtensionName { get; set; }
        /// <summary>
        /// 附件路径
        ///</summary>
        [Display(Name = "附件路径")]
        [Required]
        [MaxLength(512)]
        public string FilePath { get; set; }
        /// <summary>
        /// 请求路径
        ///</summary>
        [Display(Name = "请求路径")]
        [Required]
        [MaxLength(512)]
        public string RequestPath { get; set; }
        /// <summary>
        /// 备注
        ///</summary>
        [Display(Name = "备注")]
        [MaxLength(512)]
        public string Remark { get; set; }
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