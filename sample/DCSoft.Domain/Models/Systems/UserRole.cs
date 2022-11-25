using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DCSoft.Domain.Models.Systems
{
    /// <summary>
    /// 用户角色
    /// </summary>
    public class UserRole
    {
        /// <summary>
        /// 初始化用户角色
        /// </summary>
        public UserRole()
        {
        }

        /// <summary>
        /// 初始化用户角色
        /// </summary>
        /// <param name="userId">用户标识</param>
        /// <param name="roleId">角色标识</param>
        public UserRole(Guid userId, Guid roleId)
        {
            UserId = userId;
            RoleId = roleId;
        }

        /// <summary>
        /// 用户标识
        /// </summary>
        [Description("用户标识")]
        public Guid UserId { get; set; }

        /// <summary>
        /// 一对一引用（系统用户）
        /// </summary>
        public User User { get; set; }

        /// <summary>
        /// 角色标识
        /// </summary>
        [Description("角色标识")]
        public Guid RoleId { get; set; }

        /// <summary>
        /// 一对一引用（系统角色）
        /// </summary>
        public Role Role { get; set; }
    }
}