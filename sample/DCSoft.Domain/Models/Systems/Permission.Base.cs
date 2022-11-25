using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Util.Domain;
using Util.Domain.Auditing;
using Util.Domain.Entities;

namespace DCSoft.Domain.Models.Systems
{
    /// <summary>
    /// 权限
    /// </summary>
    [Description("权限")]
    public partial class Permission : AggregateRoot<Permission>, IDelete, IAudited
    {
        /// <summary>
        /// 初始化权限
        /// </summary>
        public Permission() : this(Guid.Empty)
        {
        }

        /// <summary>
        /// 初始化权限
        /// </summary>
        /// <param name="id">权限标识</param>
        public Permission(Guid id) : base(id)
        {
        }

        /// <summary>
        /// 角色标识
        ///</summary>
        [DisplayName("角色标识")]
        [Required]
        public Guid RoleId { get; set; }
        /// <summary>
        /// 资源标识
        ///</summary>
        [DisplayName("资源标识")]
        [Required]
        public Guid ResourceId { get; set; }
        /// <summary>
        /// 拒绝
        ///</summary>
        [DisplayName("拒绝")]
        public bool IsDeny { get; set; }
        /// <summary>
        /// 签名
        ///</summary>
        [DisplayName("签名")]
        [MaxLength(256)]
        public string Sign { get; set; }
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
        protected override void AddChanges(Permission other)
        {
            AddChange(t => t.RoleId, other.RoleId);
            AddChange(t => t.ResourceId, other.ResourceId);
            AddChange(t => t.IsDeny, other.IsDeny);
            AddChange(t => t.Sign, other.Sign);
            AddChange(t => t.CreationTime, other.CreationTime);
            AddChange(t => t.CreatorId, other.CreatorId);
            AddChange(t => t.Creator, other.Creator);
            AddChange(t => t.LastModificationTime, other.LastModificationTime);
            AddChange(t => t.LastModifierId, other.LastModifierId);
            AddChange(t => t.LastModifier, other.LastModifier);
        }
    }
}