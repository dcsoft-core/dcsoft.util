using System;
using System.ComponentModel;
using Util.Data.Queries;

namespace DCSoft.Data.Queries.Systems
{
    /// <summary>
    /// 应用程序查询参数
    /// </summary>
    public class ApplicationQuery : QueryParameter
    {
        /// <summary>
        /// 应用程序标识
        ///</summary>
        [Description("应用程序标识")]
        public Guid? ApplicationId { get; set; }
        /// <summary>
        /// 应用程序编码
        ///</summary>
        [Description("应用程序编码")]
        public string Code { get; set; }
        /// <summary>
        /// 应用程序名称
        ///</summary>
        [Description("应用程序名称")]
        public string Name { get; set; }
        /// <summary>
        /// 启用
        ///</summary>
        [Description("启用")]
        public bool? Enabled { get; set; }
        /// <summary>
        /// 启用注册
        ///</summary>
        [Description("启用注册")]
        public bool? RegisterEnabled { get; set; }
        /// <summary>
        /// 备注
        ///</summary>
        [Description("备注")]
        public string Remark { get; set; }
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