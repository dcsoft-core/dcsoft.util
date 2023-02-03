using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Util.Domain;
using Util.Domain.Auditing;
using Util.Domain.Entities;

namespace DCSoft.Domain.Models.Commons
{
    /// <summary>
    /// 公共参数
    /// </summary>
    [Description("公共参数")]
    public partial class Parameters : AggregateRoot<Parameters>, IDelete, IAudited
    {
        /// <summary>
        /// 初始化公共参数
        /// </summary>
        public Parameters() : this(Guid.Empty)
        {
        }

        /// <summary>
        /// 初始化公共参数
        /// </summary>
        /// <param name="id">公共参数标识</param>
        public Parameters(Guid id) : base(id)
        {
        }

        /// <summary>
        /// 参数名称
        ///</summary>
        [DisplayName("参数名称")]
        [Required]
        [MaxLength(128)]
        public string Name { get; set; }

        /// <summary>
        /// 参数标题
        ///</summary>
        [DisplayName("参数标题")]
        [Required]
        [MaxLength(128)]
        public string Title { get; set; }

        /// <summary>
        /// 参数内容
        ///</summary>
        [DisplayName("参数内容")]
        [Required]
        [MaxLength(128)]
        public string Value { get; set; }

        /// <summary>
        /// 参数类型
        ///</summary>
        [DisplayName("参数类型")]
        public int? Type { get; set; }

        /// <summary>
        /// 排序
        ///</summary>
        [DisplayName("排序")]
        public int? SortId { get; set; }

        /// <summary>
        /// 是否可编辑
        ///</summary>
        [DisplayName("是否可编辑")]
        public bool? IsEdit { get; set; }

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
        protected override void AddChanges(Parameters other)
        {
            AddChange(t => t.Name, other.Name);
            AddChange(t => t.Title, other.Title);
            AddChange(t => t.Value, other.Value);
            AddChange(t => t.Type, other.Type);
            AddChange(t => t.SortId, other.SortId);
            AddChange(t => t.IsEdit, other.IsEdit);
            AddChange(t => t.CreationTime, other.CreationTime);
            AddChange(t => t.CreatorId, other.CreatorId);
            AddChange(t => t.Creator, other.Creator);
            AddChange(t => t.LastModificationTime, other.LastModificationTime);
            AddChange(t => t.LastModifierId, other.LastModifierId);
            AddChange(t => t.LastModifier, other.LastModifier);
        }
    }
}