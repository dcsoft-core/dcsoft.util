using System;
using System.ComponentModel.DataAnnotations;
using Util.Applications.Trees;

namespace DCSoft.Applications.Dtos.Systems
{
    /// <summary>
    /// 资源参数
    /// </summary>
    public class ResourceDto : TreeDtoBase<ResourceDto>
    {
        /// <summary>
        /// 应用程序标识
        ///</summary>
        [Display(Name = "应用程序标识")]
        public Guid? ApplicationId { get; set; }

        /// <summary>
        /// 资源名称
        ///</summary>
        [Display(Name = "资源名称")]
        [Required]
        [MaxLength(256)]
        public string Name { get; set; }

        /// <summary>
        /// 资源类型
        ///</summary>
        [Display(Name = "资源类型")]
        [Required]
        public int Type { get; set; }

        /// <summary>
        /// 资源地址
        ///</summary>
        [Display(Name = "资源地址")]
        [MaxLength(256)]
        public string Uri { get; set; }

        /// <summary>
        /// 备注
        ///</summary>
        [Display(Name = "备注")]
        [MaxLength(512)]
        public string Remark { get; set; }

        /// <summary>
        /// 拼音简码
        ///</summary>
        [Display(Name = "拼音简码")]
        [MaxLength(256)]
        public string PinYin { get; set; }

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

        /// <inheritdoc />
        public override string GetText()
        {
            return Name;
        }
    }
}