using System;
using System.ComponentModel;
using Util.Data.Queries;

namespace DCSoft.Data.Queries.Systems
{
    /// <summary>
    /// 声明查询参数
    /// </summary>
    public class ClaimQuery : QueryParameter
    {
        /// <summary>
        /// 声明标识
        ///</summary>
        [Description("声明标识")]
        public Guid? ClaimId { get; set; }

        /// <summary>
        /// 声明名称
        ///</summary>
        [Description("声明名称")]
        public string Name { get; set; }

        /// <summary>
        /// 启用
        ///</summary>
        [Description("启用")]
        public bool? Enabled { get; set; }

        /// <summary>
        /// 排序号
        ///</summary>
        [Description("排序号")]
        public int? SortId { get; set; }

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