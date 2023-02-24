using System;
using System.ComponentModel;
using Util.Data.Queries;

namespace DCSoft.Data.Queries.Commons
{
    /// <summary>
    /// 字典类型查询参数
    /// </summary>
    public class DictTypeQuery : QueryParameter
    {
        /// <summary>
        /// 字典类型标识
        ///</summary>
        [Description("字典类型标识")]
        public Guid? DictId { get; set; }

        /// <summary>
        /// 编码
        ///</summary>
        [Description("编码")]
        public string Code { get; set; }

        /// <summary>
        /// 名称
        ///</summary>
        [Description("名称")]
        public string Name { get; set; }

        /// <summary>
        /// 是否启用
        ///</summary>
        [Description("是否启用")]
        public bool? Enabled { get; set; }

        /// <summary>
        /// 拼音简码
        ///</summary>
        [Description("拼音简码")]
        public string PinYin { get; set; }

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