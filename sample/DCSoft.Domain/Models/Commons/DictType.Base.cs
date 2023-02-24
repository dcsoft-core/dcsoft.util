using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Util.Domain;
using Util.Domain.Auditing;
using Util.Domain.Entities;

namespace DCSoft.Domain.Models.Commons
{
    /// <summary>
    /// 字典类型
    /// </summary>
    [Description("字典类型")]
    public partial class DictType : AggregateRoot<DictType>, IDelete, IAudited
    {
        /// <summary>
        /// 初始化字典类型
        /// </summary>
        public DictType() : this(Guid.Empty)
        {
        }

        /// <summary>
        /// 初始化字典类型
        /// </summary>
        /// <param name="id">字典类型标识</param>
        public DictType(Guid id) : base(id)
        {
        }

        /// <summary>
        /// 编码
        ///</summary>
        [DisplayName("编码")]
        [Required]
        [MaxLength(128)]
        public string Code { get; set; }

        /// <summary>
        /// 名称
        ///</summary>
        [DisplayName("名称")]
        [Required]
        [MaxLength(128)]
        public string Name { get; set; }

        /// <summary>
        /// 是否启用
        ///</summary>
        [DisplayName("是否启用")]
        public bool Enabled { get; set; }

        /// <summary>
        /// 拼音简码
        ///</summary>
        [DisplayName("拼音简码")]
        [Required]
        [MaxLength(64)]
        public string PinYin { get; set; }

        /// <summary>
        /// 备注
        ///</summary>
        [DisplayName("备注")]
        [MaxLength(512)]
        public string Remark { get; set; }

        /// <summary>
        /// 创建时间
        ///</summary>
        [DisplayName("创建时间")]
        public DateTime? CreationTime { get; set; }

        /// <summary>
        /// 创建者标识
        ///</summary>
        [DisplayName("创建者标识")]
        public Guid? CreatorId { get; set; }

        /// <summary>
        /// 创建者
        ///</summary>
        [DisplayName("创建者")]
        [MaxLength(256)]
        public string Creator { get; set; }

        /// <summary>
        /// 最后修改时间
        ///</summary>
        [DisplayName("最后修改时间")]
        public DateTime? LastModificationTime { get; set; }

        /// <summary>
        /// 最后修改者标识
        ///</summary>
        [DisplayName("最后修改者标识")]
        public Guid? LastModifierId { get; set; }

        /// <summary>
        /// 最后修改者
        ///</summary>
        [DisplayName("最后修改者")]
        [MaxLength(256)]
        public string LastModifier { get; set; }

        /// <summary>
        /// 是否删除
        ///</summary>
        [DisplayName("是否删除")]
        public bool IsDeleted { get; set; }

        /// <summary>
        /// 添加变更列表
        /// </summary>
        protected override void AddChanges(DictType other)
        {
            AddChange(t => t.Code, other.Code);
            AddChange(t => t.Name, other.Name);
            AddChange(t => t.Enabled, other.Enabled);
            AddChange(t => t.PinYin, other.PinYin);
            AddChange(t => t.Remark, other.Remark);
            AddChange(t => t.CreationTime, other.CreationTime);
            AddChange(t => t.CreatorId, other.CreatorId);
            AddChange(t => t.Creator, other.Creator);
            AddChange(t => t.LastModificationTime, other.LastModificationTime);
            AddChange(t => t.LastModifierId, other.LastModifierId);
            AddChange(t => t.LastModifier, other.LastModifier);
        }
    }
}