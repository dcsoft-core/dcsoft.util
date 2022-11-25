using System;
using System.ComponentModel;
using Util.Data.Queries;

namespace DCSoft.Data.Queries.Commons
{
    /// <summary>
    /// 公共附件查询参数
    /// </summary>
    public class AttachmentQuery : QueryParameter
    {
        /// <summary>
        /// 文件标识
        ///</summary>
        [Description("文件标识")]
        public Guid? FileId { get; set; }
        /// <summary>
        /// 关联对象标识
        ///</summary>
        [Description("关联对象标识")]
        public Guid? ObjectId { get; set; }
        /// <summary>
        /// 关联对象类型
        ///</summary>
        [Description("关联对象类型")]
        public string ObjectType { get; set; }
        /// <summary>
        /// 类型代码
        ///</summary>
        [Description("类型代码")]
        public string TypeCode { get; set; }
        /// <summary>
        /// 类型名称
        ///</summary>
        [Description("类型名称")]
        public string TypeName { get; set; }
        /// <summary>
        /// 附件名称
        ///</summary>
        [Description("附件名称")]
        public string ActualName { get; set; }
        /// <summary>
        /// 文件名称
        ///</summary>
        [Description("文件名称")]
        public string FileName { get; set; }
        /// <summary>
        /// MIME类型
        ///</summary>
        [Description("MIME类型")]
        public string MimeType { get; set; }
        /// <summary>
        /// 文件大小
        ///</summary>
        [Description("文件大小")]
        public int? FileSize { get; set; }
        /// <summary>
        /// 扩展名
        ///</summary>
        [Description("扩展名")]
        public string ExtensionName { get; set; }
        /// <summary>
        /// 附件路径
        ///</summary>
        [Description("附件路径")]
        public string FilePath { get; set; }
        /// <summary>
        /// 请求路径
        ///</summary>
        [Description("请求路径")]
        public string RequestPath { get; set; }
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