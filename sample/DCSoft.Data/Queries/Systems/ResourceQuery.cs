using System;
using System.ComponentModel;
using Util.Data.Trees;

namespace DCSoft.Data.Queries.Systems
{
    /// <summary>
    /// 资源查询参数
    /// </summary>
    public class ResourceQuery : TreeQueryParameter
    {
        /// <summary>
        /// 资源标识
        ///</summary>
        [Description("资源标识")]
        public Guid? ResourceId { get; set; }
        /// <summary>
        /// 应用程序标识
        ///</summary>
        [Description("应用程序标识")]
        public Guid? ApplicationId { get; set; }
        /// <summary>
        /// 角色标识
        /// </summary>
        public Guid? RoleId { get; set; }
        /// <summary>
        /// 资源名称
        ///</summary>
        [Description("资源名称")]
        public string Name { get; set; }
        /// <summary>
        /// 资源类型
        ///</summary>
        [Description("资源类型")]
        public int? Type { get; set; }
        /// <summary>
        /// 资源地址
        ///</summary>
        [Description("资源地址")]
        public string Uri { get; set; }
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
        /// 扩展
        ///</summary>
        [Description("扩展")]
        public string Extend { get; set; }
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