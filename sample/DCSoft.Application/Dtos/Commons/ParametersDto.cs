using System;
using System.ComponentModel.DataAnnotations;
using Util.Applications.Dtos;

namespace DCSoft.Applications.Dtos.Commons
{
    /// <summary>
    /// 公共参数参数
    /// </summary>
    public class ParametersDto : DtoBase
    {
        /// <summary>
        /// 参数名称
        ///</summary>
        [Display(Name = "参数名称")]
        [Required]
        [MaxLength(128)]
        public string Name { get; set; }

        /// <summary>
        /// 参数标题
        ///</summary>
        [Display(Name = "参数标题")]
        [Required]
        [MaxLength(128)]
        public string Title { get; set; }

        /// <summary>
        /// 参数内容
        ///</summary>
        [Display(Name = "参数内容")]
        [Required]
        [MaxLength(128)]
        public string Value { get; set; }

        /// <summary>
        /// 参数类型
        ///</summary>
        [Display(Name = "参数类型")]
        public int? Type { get; set; }

        /// <summary>
        /// 排序
        ///</summary>
        [Display(Name = "排序")]
        public int? SortId { get; set; }

        /// <summary>
        /// 是否可编辑
        ///</summary>
        [Display(Name = "是否可编辑")]
        public bool? IsEdit { get; set; }

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