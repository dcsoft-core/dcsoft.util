using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DCSoft.Domain.Enums;
using Util.Domain;
using Util.Domain.Auditing;
using Util.Domain.Entities;

namespace DCSoft.Domain.Models.Systems
{
    /// <summary>
    /// 应用程序
    /// </summary>
    [Description("应用程序")]
    public partial class Application : AggregateRoot<Application>, IDelete, IAudited
    {
        /// <summary>
        /// 初始化应用程序
        /// </summary>
        public Application() : this(Guid.Empty)
        {
        }

        /// <summary>
        /// 初始化应用程序
        /// </summary>
        /// <param name="id">应用程序标识</param>
        public Application(Guid id) : base(id)
        {
        }

        /// <summary>
        /// 应用程序编码
        ///</summary>
        [DisplayName("应用程序编码")]
        [Required]
        [MaxLength(64)]
        public string Code { get; set; }
        /// <summary>
        /// 应用程序名称
        ///</summary>
        [DisplayName("应用程序名称")]
        [Required]
        [MaxLength(256)]
        public string Name { get; set; }
        /// <summary>
        /// 启用
        ///</summary>
        [DisplayName("启用")]
        public bool Enabled { get; set; }
        /// <summary>
        /// 启用注册
        ///</summary>
        [DisplayName("启用注册")]
        public bool RegisterEnabled { get; set; }
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
        /// 应用程序类型
        /// </summary>
        [NotMapped]
        public ApplicationType ApplicationType { get; set; }
        /// <summary>
        /// 是否客户端
        /// </summary>
        [NotMapped]
        public bool IsClient { get; set; }
        /// <summary>
        /// 客户端
        /// </summary>
        [NotMapped]
        public Client Client { get; set; }
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
        protected override void AddChanges(Application other)
        {
            AddChange(t => t.Code, other.Code);
            AddChange(t => t.Name, other.Name);
            AddChange(t => t.Enabled, other.Enabled);
            AddChange(t => t.RegisterEnabled, other.RegisterEnabled);
            AddChange(t => t.Remark, other.Remark);
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