using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Util.Domain;
using Util.Domain.Auditing;
using Util.Domain.Entities;

namespace DCSoft.Domain.Models.Commons
{
    /// <summary>
    /// 公共附件
    /// </summary>
    [Description("公共附件")]
    public partial class Attachment : AggregateRoot<Attachment>, IDelete, IAudited
    {
        /// <summary>
        /// 初始化公共附件
        /// </summary>
        public Attachment() : this(Guid.Empty)
        {
        }

        /// <summary>
        /// 初始化公共附件
        /// </summary>
        /// <param name="id">公共附件标识</param>
        public Attachment(Guid id) : base(id)
        {
        }

        /// <summary>
        /// 关联对象标识
        ///</summary>
        [DisplayName("关联对象标识")]
        public Guid? ObjectId { get; set; }

        /// <summary>
        /// 关联对象类型
        ///</summary>
        [DisplayName("关联对象类型")]
        [Required]
        [MaxLength(64)]
        public string ObjectType { get; set; }

        /// <summary>
        /// 类型代码
        ///</summary>
        [DisplayName("类型代码")]
        [MaxLength(128)]
        public string TypeCode { get; set; }

        /// <summary>
        /// 类型名称
        ///</summary>
        [DisplayName("类型名称")]
        [MaxLength(128)]
        public string TypeName { get; set; }

        /// <summary>
        /// 附件名称
        ///</summary>
        [DisplayName("附件名称")]
        [MaxLength(128)]
        public string ActualName { get; set; }

        /// <summary>
        /// 文件名称
        ///</summary>
        [DisplayName("文件名称")]
        [MaxLength(128)]
        public string FileName { get; set; }

        /// <summary>
        /// MIME类型
        ///</summary>
        [DisplayName("MIME类型")]
        [MaxLength(256)]
        public string MimeType { get; set; }

        /// <summary>
        /// 文件大小
        ///</summary>
        [DisplayName("文件大小")]
        public int? FileSize { get; set; }

        /// <summary>
        /// 扩展名
        ///</summary>
        [DisplayName("扩展名")]
        [MaxLength(32)]
        public string ExtensionName { get; set; }

        /// <summary>
        /// 附件路径
        ///</summary>
        [DisplayName("附件路径")]
        [Required]
        [MaxLength(512)]
        public string FilePath { get; set; }

        /// <summary>
        /// 请求路径
        ///</summary>
        [DisplayName("请求路径")]
        [Required]
        [MaxLength(512)]
        public string RequestPath { get; set; }

        /// <summary>
        /// 备注
        ///</summary>
        [DisplayName("备注")]
        [MaxLength(512)]
        public string Remark { get; set; }

        /// <summary>
        /// 创建时间
        ///</summary>
        [DisplayName("创建时间")]
        public DateTime? CreationTime { get; set; }

        /// <summary>
        /// 创建者标识
        ///</summary>
        [DisplayName("创建者标识")]
        public Guid? CreatorId { get; set; }

        /// <summary>
        /// 创建者
        ///</summary>
        [DisplayName("创建者")]
        [MaxLength(256)]
        public string Creator { get; set; }

        /// <summary>
        /// 最后修改时间
        ///</summary>
        [DisplayName("最后修改时间")]
        public DateTime? LastModificationTime { get; set; }

        /// <summary>
        /// 最后修改者标识
        ///</summary>
        [DisplayName("最后修改者标识")]
        public Guid? LastModifierId { get; set; }

        /// <summary>
        /// 最后修改者
        ///</summary>
        [DisplayName("最后修改者")]
        [MaxLength(256)]
        public string LastModifier { get; set; }

        /// <summary>
        /// 是否删除
        ///</summary>
        [DisplayName("是否删除")]
        public bool IsDeleted { get; set; }

        /// <summary>
        /// 添加变更列表
        /// </summary>
        protected override void AddChanges(Attachment other)
        {
            AddChange(t => t.ObjectId, other.ObjectId);
            AddChange(t => t.ObjectType, other.ObjectType);
            AddChange(t => t.TypeCode, other.TypeCode);
            AddChange(t => t.TypeName, other.TypeName);
            AddChange(t => t.ActualName, other.ActualName);
            AddChange(t => t.FileName, other.FileName);
            AddChange(t => t.MimeType, other.MimeType);
            AddChange(t => t.FileSize, other.FileSize);
            AddChange(t => t.ExtensionName, other.ExtensionName);
            AddChange(t => t.FilePath, other.FilePath);
            AddChange(t => t.RequestPath, other.RequestPath);
            AddChange(t => t.Remark, other.Remark);
            AddChange(t => t.CreationTime, other.CreationTime);
            AddChange(t => t.CreatorId, other.CreatorId);
            AddChange(t => t.Creator, other.Creator);
            AddChange(t => t.LastModificationTime, other.LastModificationTime);
            AddChange(t => t.LastModifierId, other.LastModifierId);
            AddChange(t => t.LastModifier, other.LastModifier);
        }
    }
}