using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Util.Domain;
using Util.Domain.Auditing;
using Util.Domain.Trees;

namespace DCSoft.Domain.Models.Commons
{
    /// <summary>
    /// 字典数据
    /// </summary>
    [Description("字典数据")]
    public partial class DictData : TreeEntityBase<DictData>, IDelete, IAudited
    {
        /// <summary>
        /// 初始化字典数据
        /// </summary>
        public DictData() : this(Guid.Empty, "", 0)
        {
        }

        /// <summary>
        /// 初始化字典数据
        /// </summary>
        /// <param name="id">字典数据标识</param>
        /// <param name="path">路径</param>
        /// <param name="level">层级</param>
        public DictData(Guid id, string path, int level) : base(id, path, level)
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
        /// 类型
        ///</summary>
        [DisplayName("类型")]
        [Required]
        [MaxLength(128)]
        public string Type { get; set; }

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
        /// 父资源
        /// </summary>
        public DictData Parent { get; set; }

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
        protected override void AddChanges(DictData other)
        {
            AddChange(t => t.Code, other.Code);
            AddChange(t => t.Name, other.Name);
            AddChange(t => t.Type, other.Type);
            AddChange(t => t.PinYin, other.PinYin);
            AddChange(t => t.Remark, other.Remark);
            AddChange(t => t.ParentId, other.ParentId);
            AddChange(t => t.Path, other.Path);
            AddChange(t => t.Level, other.Level);
            AddChange(t => t.SortId, other.SortId);
            AddChange(t => t.Enabled, other.Enabled);
            AddChange(t => t.CreationTime, other.CreationTime);
            AddChange(t => t.CreatorId, other.CreatorId);
            AddChange(t => t.Creator, other.Creator);
            AddChange(t => t.LastModificationTime, other.LastModificationTime);
            AddChange(t => t.LastModifierId, other.LastModifierId);
            AddChange(t => t.LastModifier, other.LastModifier);
        }
    }
}