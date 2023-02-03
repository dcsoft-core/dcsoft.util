using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Util.Domain;
using Util.Domain.Auditing;
using Util.Domain.Trees;

namespace DCSoft.Domain.Models.Systems
{
    /// <summary>
    /// 角色
    /// </summary>
    [Description("角色")]
    public partial class Role : TreeEntityBase<Role>, IDelete, IAudited
    {
        /// <summary>
        /// 初始化角色
        /// </summary>
        public Role() : this(Guid.Empty, "", 0)
        {
        }

        /// <summary>
        /// 初始化角色
        /// </summary>
        /// <param name="id">角色标识</param>
        /// <param name="path">路径</param>
        /// <param name="level">层级</param>
        public Role(Guid id, string path, int level) : base(id, path, level)
        {
        }

        /// <summary>
        /// 角色编码
        ///</summary>
        [DisplayName("角色编码")]
        [Required]
        [MaxLength(256)]
        public string Code { get; set; }

        /// <summary>
        /// 角色名称
        ///</summary>
        [DisplayName("角色名称")]
        [Required]
        [MaxLength(256)]
        public string Name { get; set; }

        /// <summary>
        /// 标准化角色名称
        ///</summary>
        [DisplayName("标准化角色名称")]
        [Required]
        [MaxLength(256)]
        public string NormalizedName { get; set; }

        /// <summary>
        /// 角色类型
        ///</summary>
        [DisplayName("角色类型")]
        [Required]
        [MaxLength(128)]
        public string Type { get; set; }

        /// <summary>
        /// 管理员
        ///</summary>
        [DisplayName("管理员")]
        public bool IsAdmin { get; set; }

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
        /// 用户列表
        /// </summary>
        public ICollection<User> Users { get; set; }

        /// <summary>
        /// 多对多中间表（用户角色）
        /// </summary>
        public List<UserRole> UserRoles { get; set; }

        /// <summary>
        /// 添加变更列表
        /// </summary>
        protected override void AddChanges(Role other)
        {
            AddChange(t => t.Code, other.Code);
            AddChange(t => t.Name, other.Name);
            AddChange(t => t.NormalizedName, other.NormalizedName);
            AddChange(t => t.Type, other.Type);
            AddChange(t => t.IsAdmin, other.IsAdmin);
            AddChange(t => t.ParentId, other.ParentId);
            AddChange(t => t.Path, other.Path);
            AddChange(t => t.Level, other.Level);
            AddChange(t => t.SortId, other.SortId);
            AddChange(t => t.Enabled, other.Enabled);
            AddChange(t => t.Remark, other.Remark);
            AddChange(t => t.PinYin, other.PinYin);
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