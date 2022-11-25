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
    /// 模块
    /// </summary>
    [NotMapped, Description("模块")]
    public partial class Module : TreeEntityBase<Module>, IDelete, IAudited
    {
        /// <summary>
        /// 初始化模块
        /// </summary>
        public Module() : this(Guid.Empty, "", 0)
        {
        }

        /// <summary>
        /// 初始化模块
        /// </summary>
        /// <param name="id">模块标识</param>
        /// <param name="path">路径</param>
        /// <param name="level">级数</param>
        public Module(Guid id, string path, int level) : base(id, path, level)
        {
        }

        /// <summary>
        /// 应用程序标识
        /// </summary>
        [Description("应用程序标识")]
        public Guid? ApplicationId { get; set; }

        /// <summary>
        /// 模块名称
        /// </summary>
        [Description("模块名称"), MaxLength(256)]
        [Required(ErrorMessage = "模块名称不能为空")]
        public string Name { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        [Description("类型")]
        public ResourceType Type { get; set; }

        /// <summary>
        /// 模块地址
        /// </summary>
        [Description("模块地址"), MaxLength(256)]
        public string Url { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        [NotMapped, Description("图标")]
        public string Icon { get; set; }

        /// <summary>
        /// 是否展开
        /// </summary>
        [NotMapped, Description("是否展开")]
        public bool? Expanded { get; set; }

        /// <summary>
        /// 方法
        /// </summary>
        [NotMapped, Description("方法")]
        public string Method { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [Description("备注"), MaxLength(512)]
        public string Remark { get; set; }

        /// <summary>
        /// 拼音简码
        /// </summary>
        [Description("拼音简码"), MaxLength(256)]
        public string PinYin { get; set; }
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
        protected override void AddChanges(DCSoft.Domain.Models.Systems.Module other)
        {
            AddChange(t => t.Id, other.Id);
            AddChange(t => t.ApplicationId, other.ApplicationId);
            AddChange(t => t.Name, other.Name);
            AddChange(t => t.Type, other.Type);
            AddChange(t => t.Url, other.Url);
            AddChange(t => t.ParentId, other.ParentId);
            AddChange(t => t.Path, other.Path);
            AddChange(t => t.Level, other.Level);
            AddChange(t => t.Remark, other.Remark);
            AddChange(t => t.PinYin, other.PinYin);
            AddChange(t => t.Enabled, other.Enabled);
            AddChange(t => t.SortId, other.SortId);
            AddChange(t => t.CreationTime, other.CreationTime);
            AddChange(t => t.CreatorId, other.CreatorId);
            AddChange(t => t.Creator, other.Creator);
            AddChange(t => t.LastModificationTime, other.LastModificationTime);
            AddChange(t => t.LastModifierId, other.LastModifierId);
            AddChange(t => t.LastModifier, other.LastModifier);
        }
    }
}