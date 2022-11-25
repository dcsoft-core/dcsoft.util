using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DCSoft.Domain.Enums;
using Util.Domain;
using Util.Domain.Auditing;
using Util.Domain.Trees;

namespace DCSoft.Domain.Models.Systems
{
    /// <summary>
    /// 资源
    /// </summary>
    [Description("资源")]
    public partial class Resource : TreeEntityBase<Resource>, IDelete, IAudited
    {
        /// <summary>
        /// 初始化资源
        /// </summary>
        public Resource() : this(Guid.Empty, "", 0)
        {
        }

        /// <summary>
        /// 初始化资源
        /// </summary>
        /// <param name="id">资源标识</param>
        /// <param name="path">路径</param>
        /// <param name="level">层级</param>
        public Resource(Guid id, string path, int level) : base(id, path, level)
        {
        }

        /// <summary>
        /// 应用程序标识
        ///</summary>
        [DisplayName("应用程序标识")]
        public Guid? ApplicationId { get; set; }
        /// <summary>
        /// 资源名称
        ///</summary>
        [DisplayName("资源名称")]
        [Required]
        [MaxLength(256)]
        public string Name { get; set; }
        /// <summary>
        /// 资源类型
        ///</summary>
        [DisplayName("资源类型")]
        [Required]
        public ResourceType Type { get; set; }
        /// <summary>
        /// 资源地址
        ///</summary>
        [DisplayName("资源地址")]
        [MaxLength(256)]
        public string Uri { get; set; }
        /// <summary>
        /// 备注
        ///</summary>
        [DisplayName("备注")]
        [MaxLength(512)]
        public string Remark { get; set; }
        /// <summary>
        /// 拼音简码
        ///</summary>
        [DisplayName("拼音简码")]
        [MaxLength(256)]
        public string PinYin { get; set; }
        /// <summary>
        /// 扩展
        ///</summary>
        [DisplayName("扩展")]
        public string Extend { get; set; }
        /// <summary>
        /// 应用程序
        /// </summary>
        [ForeignKey("ApplicationId")]
        public Application Application { get; set; }
        /// <summary>
        /// 父资源
        /// </summary>
        [ForeignKey("ParentId")]
        public Resource Parent { get; set; }
        /// <summary>
        /// 权限
        /// </summary>
        public Permission Permission { get; set; }
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
        protected override void AddChanges(Resource other)
        {
            AddChange(t => t.ApplicationId, other.ApplicationId);
            AddChange(t => t.Name, other.Name);
            AddChange(t => t.Type, other.Type);
            AddChange(t => t.Uri, other.Uri);
            AddChange(t => t.ParentId, other.ParentId);
            AddChange(t => t.Path, other.Path);
            AddChange(t => t.Level, other.Level);
            AddChange(t => t.SortId, other.SortId);
            AddChange(t => t.Enabled, other.Enabled);
            AddChange(t => t.Remark, other.Remark);
            AddChange(t => t.PinYin, other.PinYin);
            AddChange(t => t.Extend, other.Extend);
            AddChange(t => t.CreationTime, other.CreationTime);
            AddChange(t => t.CreatorId, other.CreatorId);
            AddChange(t => t.Creator, other.Creator);
            AddChange(t => t.LastModificationTime, other.LastModificationTime);
            AddChange(t => t.LastModifierId, other.LastModifierId);
            AddChange(t => t.LastModifier, other.LastModifier);
        }
    }
}