using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using DCSoft.Domain.Enums;
using Util.Domain;
using Util.Domain.Auditing;
using Util.Domain.Entities;

namespace DCSoft.Domain.Models.Systems
{
    /// <summary>
    /// 用户
    /// </summary>
    [Description("用户")]
    public partial class User : AggregateRoot<User>, IDelete, IAudited
    {
        /// <summary>
        /// 初始化用户
        /// </summary>
        public User() : this(Guid.Empty)
        {
        }

        /// <summary>
        /// 初始化用户
        /// </summary>
        /// <param name="id">用户标识</param>
        public User(Guid id) : base(id)
        {
        }

        /// <summary>
        /// 部门标识
        ///</summary>
        [DisplayName("部门标识")]
        public Guid? DepartmentId { get; set; }
        /// <summary>
        /// 用户类型
        ///</summary>
        [DisplayName("用户类型")]
        [Required]
        public UserType UserType { get; set; }
        /// <summary>
        /// 昵称
        ///</summary>
        [DisplayName("昵称")]
        [MaxLength(256)]
        public string NickName { get; set; }
        /// <summary>
        /// 用户名
        ///</summary>
        [DisplayName("用户名")]
        [MaxLength(256)]
        public string UserName { get; set; }
        /// <summary>
        /// 标准化用户名
        ///</summary>
        [DisplayName("标准化用户名")]
        [MaxLength(256)]
        public string NormalizedUserName { get; set; }
        /// <summary>
        /// 安全邮箱
        ///</summary>
        [DisplayName("安全邮箱")]
        [MaxLength(256)]
        public string Email { get; set; }
        /// <summary>
        /// 标准化邮箱
        ///</summary>
        [DisplayName("标准化邮箱")]
        [MaxLength(256)]
        public string NormalizedEmail { get; set; }
        /// <summary>
        /// 邮箱已确认
        ///</summary>
        [DisplayName("邮箱已确认")]
        public bool EmailConfirmed { get; set; }
        /// <summary>
        /// 安全手机
        ///</summary>
        [DisplayName("安全手机")]
        [MaxLength(64)]
        public string PhoneNumber { get; set; }
        /// <summary>
        /// 手机已确认
        ///</summary>
        [DisplayName("手机已确认")]
        public bool PhoneNumberConfirmed { get; set; }
        /// <summary>
        /// 密码
        ///</summary>
        [DisplayName("密码")]
        [MaxLength(256)]
        public string Password { get; set; }
        /// <summary>
        /// 密码散列
        ///</summary>
        [DisplayName("密码散列")]
        [MaxLength(1024)]
        public string PasswordHash { get; set; }
        /// <summary>
        /// 安全码
        ///</summary>
        [DisplayName("安全码")]
        [MaxLength(256)]
        public string SafePassword { get; set; }
        /// <summary>
        /// 安全码散列
        ///</summary>
        [DisplayName("安全码散列")]
        [MaxLength(1024)]
        public string SafePasswordHash { get; set; }
        /// <summary>
        /// 启用两阶段认证
        ///</summary>
        [DisplayName("启用两阶段认证")]
        public bool TwoFactorEnabled { get; set; }
        /// <summary>
        /// 启用
        ///</summary>
        [DisplayName("启用")]
        public bool Enabled { get; set; }
        /// <summary>
        /// 冻结时间
        ///</summary>
        [DisplayName("冻结时间")]
        public DateTime? DisabledTime { get; set; }
        /// <summary>
        /// 启用锁定
        ///</summary>
        [DisplayName("启用锁定")]
        public bool LockoutEnabled { get; set; }
        /// <summary>
        /// 锁定截止
        ///</summary>
        [DisplayName("锁定截止")]
        public DateTime? LockoutEnd { get; set; }
        /// <summary>
        /// 登陆失败次数
        ///</summary>
        [DisplayName("登陆失败次数")]
        public int? AccessFailedCount { get; set; }
        /// <summary>
        /// 登陆次数
        ///</summary>
        [DisplayName("登陆次数")]
        public int? LoginCount { get; set; }
        /// <summary>
        /// 注册Ip
        ///</summary>
        [DisplayName("注册Ip")]
        [MaxLength(32)]
        public string RegisterIp { get; set; }
        /// <summary>
        /// 上次登陆时间
        ///</summary>
        [DisplayName("上次登陆时间")]
        public DateTime? LastLoginTime { get; set; }
        /// <summary>
        /// 上次登陆Ip
        ///</summary>
        [DisplayName("上次登陆Ip")]
        [MaxLength(32)]
        public string LastLoginIp { get; set; }
        /// <summary>
        /// 本次登陆时间
        ///</summary>
        [DisplayName("本次登陆时间")]
        public DateTime? CurrentLoginTime { get; set; }
        /// <summary>
        /// 本次登陆Ip
        ///</summary>
        [DisplayName("本次登陆Ip")]
        [MaxLength(32)]
        public string CurrentLoginIp { get; set; }
        /// <summary>
        /// 安全戳
        ///</summary>
        [DisplayName("安全戳")]
        [MaxLength(1024)]
        public string SecurityStamp { get; set; }
        /// <summary>
        /// 头像
        ///</summary>
        [DisplayName("头像")]
        [MaxLength(512)]
        public string Avatar { get; set; }
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
        /// 角色列表
        /// </summary>
        public ICollection<Role> Roles { get; set; }

        /// <summary>
        /// 多对多中间表（用户-角色）
        /// </summary>
        public List<UserRole> UserRoles { get; set; }

        /// <summary>
        /// 添加变更列表
        /// </summary>
        protected override void AddChanges(User other)
        {
            AddChange(t => t.DepartmentId, other.DepartmentId);
            AddChange(t => t.UserType, other.UserType);
            AddChange(t => t.NickName, other.NickName);
            AddChange(t => t.UserName, other.UserName);
            AddChange(t => t.NormalizedUserName, other.NormalizedUserName);
            AddChange(t => t.Email, other.Email);
            AddChange(t => t.NormalizedEmail, other.NormalizedEmail);
            AddChange(t => t.EmailConfirmed, other.EmailConfirmed);
            AddChange(t => t.PhoneNumber, other.PhoneNumber);
            AddChange(t => t.PhoneNumberConfirmed, other.PhoneNumberConfirmed);
            AddChange(t => t.Password, other.Password);
            AddChange(t => t.PasswordHash, other.PasswordHash);
            AddChange(t => t.SafePassword, other.SafePassword);
            AddChange(t => t.SafePasswordHash, other.SafePasswordHash);
            AddChange(t => t.TwoFactorEnabled, other.TwoFactorEnabled);
            AddChange(t => t.Enabled, other.Enabled);
            AddChange(t => t.DisabledTime, other.DisabledTime);
            AddChange(t => t.LockoutEnabled, other.LockoutEnabled);
            AddChange(t => t.LockoutEnd, other.LockoutEnd);
            AddChange(t => t.AccessFailedCount, other.AccessFailedCount);
            AddChange(t => t.LoginCount, other.LoginCount);
            AddChange(t => t.RegisterIp, other.RegisterIp);
            AddChange(t => t.LastLoginTime, other.LastLoginTime);
            AddChange(t => t.LastLoginIp, other.LastLoginIp);
            AddChange(t => t.CurrentLoginTime, other.CurrentLoginTime);
            AddChange(t => t.CurrentLoginIp, other.CurrentLoginIp);
            AddChange(t => t.SecurityStamp, other.SecurityStamp);
            AddChange(t => t.Avatar, other.Avatar);
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