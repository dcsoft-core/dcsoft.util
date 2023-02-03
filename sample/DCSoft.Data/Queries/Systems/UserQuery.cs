using System;
using System.ComponentModel;
using DCSoft.Domain.Enums;
using Util.Data.Queries;

namespace DCSoft.Data.Queries.Systems
{
    /// <summary>
    /// 用户查询参数
    /// </summary>
    public class UserQuery : QueryParameter
    {
        /// <summary>
        /// 用户标识
        ///</summary>
        [Description("用户标识")]
        public Guid? UserId { get; set; }

        /// <summary>
        /// 部门标识
        ///</summary>
        [Description("部门标识")]
        public Guid? DepartmentId { get; set; }

        /// <summary>
        /// 角色标识
        /// </summary>
        public Guid? RoleId { get; set; }

        /// <summary>
        /// 用户类型
        ///</summary>
        [Description("用户类型")]
        public UserType? UserType { get; set; }

        /// <summary>
        /// 排除的角色标识
        /// </summary>
        public Guid? ExceptRoleId { get; set; }

        /// <summary>
        /// 昵称
        ///</summary>
        [Description("昵称")]
        public string NickName { get; set; }

        /// <summary>
        /// 用户名
        ///</summary>
        [Description("用户名")]
        public string UserName { get; set; }

        /// <summary>
        /// 标准化用户名
        ///</summary>
        [Description("标准化用户名")]
        public string NormalizedUserName { get; set; }

        /// <summary>
        /// 安全邮箱
        ///</summary>
        [Description("安全邮箱")]
        public string Email { get; set; }

        /// <summary>
        /// 标准化邮箱
        ///</summary>
        [Description("标准化邮箱")]
        public string NormalizedEmail { get; set; }

        /// <summary>
        /// 邮箱已确认
        ///</summary>
        [Description("邮箱已确认")]
        public bool? EmailConfirmed { get; set; }

        /// <summary>
        /// 安全手机
        ///</summary>
        [Description("安全手机")]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// 手机已确认
        ///</summary>
        [Description("手机已确认")]
        public bool? PhoneNumberConfirmed { get; set; }

        /// <summary>
        /// 密码
        ///</summary>
        [Description("密码")]
        public string Password { get; set; }

        /// <summary>
        /// 密码散列
        ///</summary>
        [Description("密码散列")]
        public string PasswordHash { get; set; }

        /// <summary>
        /// 安全码
        ///</summary>
        [Description("安全码")]
        public string SafePassword { get; set; }

        /// <summary>
        /// 安全码散列
        ///</summary>
        [Description("安全码散列")]
        public string SafePasswordHash { get; set; }

        /// <summary>
        /// 启用两阶段认证
        ///</summary>
        [Description("启用两阶段认证")]
        public bool? TwoFactorEnabled { get; set; }

        /// <summary>
        /// 启用
        ///</summary>
        [Description("启用")]
        public bool? Enabled { get; set; }

        /// <summary>
        /// 起始冻结时间
        /// </summary>
        public DateTime? BeginDisabledTime { get; set; }

        /// <summary>
        /// 结束冻结时间
        /// </summary>
        public DateTime? EndDisabledTime { get; set; }

        /// <summary>
        /// 启用锁定
        ///</summary>
        [Description("启用锁定")]
        public bool? LockoutEnabled { get; set; }

        /// <summary>
        /// 起始锁定截止
        /// </summary>
        public DateTime? BeginLockoutEnd { get; set; }

        /// <summary>
        /// 结束锁定截止
        /// </summary>
        public DateTime? EndLockoutEnd { get; set; }

        /// <summary>
        /// 登陆失败次数
        ///</summary>
        [Description("登陆失败次数")]
        public int? AccessFailedCount { get; set; }

        /// <summary>
        /// 登陆次数
        ///</summary>
        [Description("登陆次数")]
        public int? LoginCount { get; set; }

        /// <summary>
        /// 注册Ip
        ///</summary>
        [Description("注册Ip")]
        public string RegisterIp { get; set; }

        /// <summary>
        /// 起始上次登陆时间
        /// </summary>
        public DateTime? BeginLastLoginTime { get; set; }

        /// <summary>
        /// 结束上次登陆时间
        /// </summary>
        public DateTime? EndLastLoginTime { get; set; }

        /// <summary>
        /// 上次登陆Ip
        ///</summary>
        [Description("上次登陆Ip")]
        public string LastLoginIp { get; set; }

        /// <summary>
        /// 起始本次登陆时间
        /// </summary>
        public DateTime? BeginCurrentLoginTime { get; set; }

        /// <summary>
        /// 结束本次登陆时间
        /// </summary>
        public DateTime? EndCurrentLoginTime { get; set; }

        /// <summary>
        /// 本次登陆Ip
        ///</summary>
        [Description("本次登陆Ip")]
        public string CurrentLoginIp { get; set; }

        /// <summary>
        /// 安全戳
        ///</summary>
        [Description("安全戳")]
        public string SecurityStamp { get; set; }

        /// <summary>
        /// 头像
        ///</summary>
        [Description("头像")]
        public string Avatar { get; set; }

        /// <summary>
        /// 备注
        ///</summary>
        [Description("备注")]
        public string Remark { get; set; }

        /// <summary>
        /// 起始创建时间
        /// </summary>
        public DateTime? BeginCreationTime { get; set; }

        /// <summary>
        /// 结束创建时间
        /// </summary>
        public DateTime? EndCreationTime { get; set; }

        /// <summary>
        /// 创建者标识
        ///</summary>
        [Description("创建者标识")]
        public Guid? CreatorId { get; set; }

        /// <summary>
        /// 创建者
        ///</summary>
        [Description("创建者")]
        public string Creator { get; set; }

        /// <summary>
        /// 起始最后修改时间
        /// </summary>
        public DateTime? BeginLastModificationTime { get; set; }

        /// <summary>
        /// 结束最后修改时间
        /// </summary>
        public DateTime? EndLastModificationTime { get; set; }

        /// <summary>
        /// 最后修改者标识
        ///</summary>
        [Description("最后修改者标识")]
        public Guid? LastModifierId { get; set; }

        /// <summary>
        /// 最后修改者
        ///</summary>
        [Description("最后修改者")]
        public string LastModifier { get; set; }
    }
}