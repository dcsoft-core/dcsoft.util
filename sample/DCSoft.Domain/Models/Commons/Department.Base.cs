using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Util.Domain;
using Util.Domain.Auditing;
using Util.Domain.Trees;

namespace DCSoft.Domain.Models.Commons
{
    /// <summary>
    /// 组织机构
    /// </summary>
    [Description("组织机构")]
    public partial class Department : TreeEntityBase<Department>, IDelete, IAudited
    {
        /// <summary>
        /// 初始化组织机构
        /// </summary>
        public Department() : this(Guid.Empty, "", 0)
        {
        }

        /// <summary>
        /// 初始化组织机构
        /// </summary>
        /// <param name="id">组织机构标识</param>
        /// <param name="path">路径</param>
        /// <param name="level">层级</param>
        public Department(Guid id, string path, int level) : base(id, path, level)
        {
        }

        /// <summary>
        /// 部门编码
        ///</summary>
        [DisplayName("部门编码")]
        [Required]
        [MaxLength(128)]
        public string Code { get; set; }

        /// <summary>
        /// 部门名称
        ///</summary>
        [DisplayName("部门名称")]
        [Required]
        [MaxLength(256)]
        public string Name { get; set; }

        /// <summary>
        /// 拼音简码
        ///</summary>
        [DisplayName("拼音简码")]
        [MaxLength(256)]
        public string PinYin { get; set; }

        /// <summary>
        /// 备注
        ///</summary>
        [DisplayName("备注")]
        [MaxLength(512)]
        public string Remark { get; set; }

        /// <summary>
        /// 扩展
        ///</summary>
        [DisplayName("扩展")]
        public string Extend { get; set; }

        /// <summary>
        /// 父资源
        /// </summary>
        public Department Parent { get; set; }

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
        protected override void AddChanges(Department other)
        {
            AddChange(t => t.Code, other.Code);
            AddChange(t => t.Name, other.Name);
            AddChange(t => t.PinYin, other.PinYin);
            AddChange(t => t.Remark, other.Remark);
            AddChange(t => t.Extend, other.Extend);
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