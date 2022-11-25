using System;
using System.ComponentModel.DataAnnotations;
using Util.Applications.Trees;

namespace DCSoft.Applications.Dtos.Commons
{
    /// <summary>
    /// 字典数据参数
    /// </summary>
    public class DictDataDto : TreeDtoBase<DictDataDto>
    {
        /// <summary>
        /// 编码
        ///</summary>
        [Display(Name = "编码")]
        [Required]
        [MaxLength(128)]
        public string Code { get; set; }
        /// <summary>
        /// 名称
        ///</summary>
        [Display(Name = "名称")]
        [Required]
        [MaxLength(128)]
        public string Name { get; set; }
        /// <summary>
        /// 类型
        ///</summary>
        [Display(Name = "类型")]
        [Required]
        [MaxLength(128)]
        public string Type { get; set; }
        /// <summary>
        /// 拼音简码
        ///</summary>
        [Display(Name = "拼音简码")]
        [Required]
        [MaxLength(64)]
        public string PinYin { get; set; }
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

        /// <inheritdoc />
        public override string GetText()
        {
            return Name;
        }
    }
}