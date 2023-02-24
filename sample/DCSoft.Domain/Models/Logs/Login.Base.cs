using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Util.Domain;
using Util.Domain.Auditing;
using Util.Domain.Entities;

namespace DCSoft.Domain.Models.Logs
{
    /// <summary>
    /// 登录日志
    /// </summary>
    [Description("登录日志")]
    public partial class Login : AggregateRoot<Login>, IDelete, IAudited
    {
        /// <summary>
        /// 初始化日志
        /// </summary>
        public Login() : this(Guid.Empty)
        {
        }

        /// <summary>
        /// 初始化日志
        /// </summary>
        /// <param name="id">日志标识</param>
        public Login(Guid id) : base(id)
        {
        }

        /// <summary>
        /// 登录帐号
        ///</summary>
        [DisplayName("登录帐号")]
        [Required]
        [MaxLength(64)]
        public string LoginName { get; set; }

        /// <summary>
        /// 登录IP地址
        ///</summary>
        [DisplayName("登录IP地址")]
        [Required]
        [MaxLength(32)]
        public string IpAddress { get; set; }

        /// <summary>
        /// 登录地点
        ///</summary>
        [DisplayName("登录地点")]
        [Required]
        [MaxLength(256)]
        public string Location { get; set; }

        /// <summary>
        /// 操作系统
        ///</summary>
        [DisplayName("操作系统")]
        [MaxLength(64)]
        public string OS { get; set; }

        /// <summary>
        /// 登录状态（0成功 1失败）
        ///</summary>
        [DisplayName("登录状态（0成功 1失败）")]
        public int? Status { get; set; }

        /// <summary>
        /// 提示消息
        ///</summary>
        [DisplayName("提示消息")]
        [MaxLength(256)]
        public string PromptMsg { get; set; }

        /// <summary>
        /// 浏览器类型
        ///</summary>
        [DisplayName("浏览器类型")]
        [MaxLength(1024)]
        public string Browser { get; set; }

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
        protected override void AddChanges(Login other)
        {
            AddChange(t => t.LoginName, other.LoginName);
            AddChange(t => t.IpAddress, other.IpAddress);
            AddChange(t => t.Location, other.Location);
            AddChange(t => t.OS, other.OS);
            AddChange(t => t.Status, other.Status);
            AddChange(t => t.PromptMsg, other.PromptMsg);
            AddChange(t => t.Browser, other.Browser);
            AddChange(t => t.CreationTime, other.CreationTime);
            AddChange(t => t.CreatorId, other.CreatorId);
            AddChange(t => t.Creator, other.Creator);
            AddChange(t => t.LastModificationTime, other.LastModificationTime);
            AddChange(t => t.LastModifierId, other.LastModifierId);
            AddChange(t => t.LastModifier, other.LastModifier);
        }
    }
}