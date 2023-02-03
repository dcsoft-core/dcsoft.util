using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using Util.Applications.Dtos;

namespace DCSoft.Applications.Dtos.Systems
{
    /// <summary>
    /// 用户参数
    /// </summary>
    public class UserDto : DtoBase
    {
        /// <summary>
        /// 部门标识
        ///</summary>
        [Display(Name = "部门标识")]
        public Guid? DepartmentId { get; set; }

        /// <summary>
        /// 用户类型
        ///</summary>
        [Display(Name = "用户类型")]
        [Required]
        public int UserType { get; set; }

        /// <summary>
        /// 角色
        /// </summary>
        [Display(Name = "角色")]
        [JsonIgnore]
        public List<RoleDto> Roles { get; set; }

        /// <summary>
        /// 角色标识数组
        /// </summary>
        [Display(Name = "角色标识数组")]
        public string[] RoleIds
        {
            get { return Roles.Select(t => t.Id).ToArray(); }
        }

        /// <summary>
        /// 角色名称数组
        /// </summary>
        [Display(Name = "角色名称数组")]
        public string[] RoleNames
        {
            get { return Roles.Select(t => t.Name).ToArray(); }
        }

        /// <summary>
        /// 昵称
        ///</summary>
        [Display(Name = "昵称")]
        [MaxLength(256)]
        public string NickName { get; set; }

        /// <summary>
        /// 用户名
        ///</summary>
        [Display(Name = "用户名")]
        [MaxLength(256)]
        public string UserName { get; set; }

        /// <summary>
        /// 标准化用户名
        ///</summary>
        [Display(Name = "标准化用户名")]
        [MaxLength(256)]
        public string NormalizedUserName { get; set; }

        /// <summary>
        /// 安全邮箱
        ///</summary>
        [Display(Name = "安全邮箱")]
        [MaxLength(256)]
        public string Email { get; set; }

        /// <summary>
        /// 标准化邮箱
        ///</summary>
        [Display(Name = "标准化邮箱")]
        [MaxLength(256)]
        public string NormalizedEmail { get; set; }

        /// <summary>
        /// 邮箱已确认
        ///</summary>
        [Display(Name = "邮箱已确认")]
        public bool EmailConfirmed { get; set; }

        /// <summary>
        /// 安全手机
        ///</summary>
        [Display(Name = "安全手机")]
        [MaxLength(64)]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// 手机已确认
        ///</summary>
        [Display(Name = "手机已确认")]
        public bool PhoneNumberConfirmed { get; set; }

        /// <summary>
        /// 密码
        ///</summary>
        [Display(Name = "密码")]
        [MaxLength(256)]
        public string Password { get; set; }

        /// <summary>
        /// 密码散列
        ///</summary>
        [Display(Name = "密码散列")]
        [MaxLength(1024)]
        public string PasswordHash { get; set; }

        /// <summary>
        /// 安全码
        ///</summary>
        [Display(Name = "安全码")]
        [MaxLength(256)]
        public string SafePassword { get; set; }

        /// <summary>
        /// 安全码散列
        ///</summary>
        [Display(Name = "安全码散列")]
        [MaxLength(1024)]
        public string SafePasswordHash { get; set; }

        /// <summary>
        /// 启用两阶段认证
        ///</summary>
        [Display(Name = "启用两阶段认证")]
        public bool TwoFactorEnabled { get; set; }

        /// <summary>
        /// 启用
        ///</summary>
        [Display(Name = "启用")]
        public bool Enabled { get; set; }

        /// <summary>
        /// 冻结时间
        ///</summary>
        [Display(Name = "冻结时间")]
        public DateTime? DisabledTime { get; set; }

        /// <summary>
        /// 启用锁定
        ///</summary>
        [Display(Name = "启用锁定")]
        public bool LockoutEnabled { get; set; }

        /// <summary>
        /// 锁定截止
        ///</summary>
        [Display(Name = "锁定截止")]
        public DateTime? LockoutEnd { get; set; }

        /// <summary>
        /// 登陆失败次数
        ///</summary>
        [Display(Name = "登陆失败次数")]
        public int? AccessFailedCount { get; set; }

        /// <summary>
        /// 登陆次数
        ///</summary>
        [Display(Name = "登陆次数")]
        public int? LoginCount { get; set; }

        /// <summary>
        /// 注册Ip
        ///</summary>
        [Display(Name = "注册Ip")]
        [MaxLength(32)]
        public string RegisterIp { get; set; }

        /// <summary>
        /// 上次登陆时间
        ///</summary>
        [Display(Name = "上次登陆时间")]
        public DateTime? LastLoginTime { get; set; }

        /// <summary>
        /// 上次登陆Ip
        ///</summary>
        [Display(Name = "上次登陆Ip")]
        [MaxLength(32)]
        public string LastLoginIp { get; set; }

        /// <summary>
        /// 本次登陆时间
        ///</summary>
        [Display(Name = "本次登陆时间")]
        public DateTime? CurrentLoginTime { get; set; }

        /// <summary>
        /// 本次登陆Ip
        ///</summary>
        [Display(Name = "本次登陆Ip")]
        [MaxLength(32)]
        public string CurrentLoginIp { get; set; }

        /// <summary>
        /// 安全戳
        ///</summary>
        [Display(Name = "安全戳")]
        [MaxLength(1024)]
        public string SecurityStamp { get; set; }

        /// <summary>
        /// 头像
        ///</summary>
        [Display(Name = "头像")]
        [MaxLength(512)]
        public string Avatar { get; set; }

        /// <summary>
        /// 备注
        ///</summary>
        [Display(Name = "备注")]
        [MaxLength(512)]
        public string Remark { get; set; }

        /// <summary>
        /// 创建时间
        ///</summary>
        [Display(Name = "创建时间")]
        public DateTime? CreationTime { get; set; }

        /// <summary>
        /// 创建者标识
        ///</summary>
        [Display(Name = "创建者标识")]
        public Guid? CreatorId { get; set; }

        /// <summary>
        /// 创建者
        ///</summary>
        [Display(Name = "创建者")]
        [MaxLength(256)]
        public string Creator { get; set; }

        /// <summary>
        /// 最后修改时间
        ///</summary>
        [Display(Name = "最后修改时间")]
        public DateTime? LastModificationTime { get; set; }

        /// <summary>
        /// 最后修改者标识
        ///</summary>
        [Display(Name = "最后修改者标识")]
        public Guid? LastModifierId { get; set; }

        /// <summary>
        /// 最后修改者
        ///</summary>
        [Display(Name = "最后修改者")]
        [MaxLength(256)]
        public string LastModifier { get; set; }

        /// <summary>
        /// 版本号
        ///</summary>
        [Display(Name = "版本号")]
        public byte[] Version { get; set; }
    }
}