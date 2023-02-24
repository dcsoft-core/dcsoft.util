using System;
using System.ComponentModel;
using Util.Data.Queries;

namespace DCSoft.Data.Queries.Systems
{
    /// <summary>
    /// 权限查询参数
    /// </summary>
    public class PermissionQuery : QueryParameter
    {
        /// <summary>
        /// 应用程序标识
        /// </summary>
        public Guid? ApplicationId { get; set; }

        /// <summary>
        /// 权限标识
        ///</summary>
        [Description("权限标识")]
        public Guid? PermissionId { get; set; }

        /// <summary>
        /// 角色标识
        ///</summary>
        [Description("角色标识")]
        public Guid? RoleId { get; set; }

        /// <summary>
        /// 资源标识
        ///</summary>
        [Description("资源标识")]
        public Guid? ResourceId { get; set; }

        /// <summary>
        /// 拒绝
        ///</summary>
        [Description("拒绝")]
        public bool? IsDeny { get; set; }

        /// <summary>
        /// 签名
        ///</summary>
        [Description("签名")]
        public string Sign { get; set; }

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