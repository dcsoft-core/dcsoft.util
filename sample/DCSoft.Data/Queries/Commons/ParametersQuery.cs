using System;
using System.ComponentModel;
using Util.Data.Queries;

namespace DCSoft.Data.Queries.Commons
{
    /// <summary>
    /// 公共参数查询参数
    /// </summary>
    public class ParametersQuery : QueryParameter
    {
        /// <summary>
        /// 参数标识
        ///</summary>
        [Description("参数标识")]
        public Guid? ParamId { get; set; }

        /// <summary>
        /// 参数名称
        ///</summary>
        [Description("参数名称")]
        public string Name { get; set; }

        /// <summary>
        /// 参数标题
        ///</summary>
        [Description("参数标题")]
        public string Title { get; set; }

        /// <summary>
        /// 参数内容
        ///</summary>
        [Description("参数内容")]
        public string Value { get; set; }

        /// <summary>
        /// 参数类型
        ///</summary>
        [Description("参数类型")]
        public int? Type { get; set; }

        /// <summary>
        /// 排序
        ///</summary>
        [Description("排序")]
        public int? SortId { get; set; }

        /// <summary>
        /// 是否可编辑
        ///</summary>
        [Description("是否可编辑")]
        public bool? IsEdit { get; set; }

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