using System;
using System.ComponentModel;
using Util.Data.Trees;

namespace DCSoft.Data.Queries.Systems
{
    /// <summary>
    /// 角色查询参数
    /// </summary>
    public class RoleQuery : TreeQueryParameter
    {
        /// <summary>
        /// 角色标识
        ///</summary>
        [Description("角色标识")]
        public Guid? RoleId { get; set; }
        /// <summary>
        /// 角色编码
        ///</summary>
        [Description("角色编码")]
        public string Code { get; set; }
        /// <summary>
        /// 角色名称
        ///</summary>
        [Description("角色名称")]
        public string Name { get; set; }
        /// <summary>
        /// 标准化角色名称
        ///</summary>
        [Description("标准化角色名称")]
        public string NormalizedName { get; set; }
        /// <summary>
        /// 角色类型
        ///</summary>
        [Description("角色类型")]
        public string Type { get; set; }
        /// <summary>
        /// 管理员
        ///</summary>
        [Description("管理员")]
        public bool? IsAdmin { get; set; }
        /// <summary>
        /// 备注
        ///</summary>
        [Description("备注")]
        public string Remark { get; set; }
        /// <summary>
        /// 拼音简码
        ///</summary>
        [Description("拼音简码")]
        public string PinYin { get; set; }
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