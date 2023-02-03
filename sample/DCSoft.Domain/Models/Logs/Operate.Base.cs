using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Util.Domain;
using Util.Domain.Auditing;
using Util.Domain.Entities;

namespace DCSoft.Domain.Models.Logs
{
    /// <summary>
    /// 操作日志
    /// </summary>
    [Description("操作日志")]
    public partial class Operate : AggregateRoot<Operate>, IDelete, IAudited
    {
        /// <summary>
        /// 初始化日志
        /// </summary>
        public Operate() : this(Guid.Empty)
        {
        }

        /// <summary>
        /// 初始化日志
        /// </summary>
        /// <param name="id">日志标识</param>
        public Operate(Guid id) : base(id)
        {
        }

        /// <summary>
        /// 模块标题
        ///</summary>
        [DisplayName("模块标题")]
        [Required]
        [MaxLength(64)]
        public string Title { get; set; }

        /// <summary>
        /// 业务类型（0其它 1新增 2修改 3删除）
        ///</summary>
        [DisplayName("业务类型（0其它 1新增 2修改 3删除）")]
        [Required]
        public int Type { get; set; }

        /// <summary>
        /// 请求方式
        ///</summary>
        [DisplayName("请求方式")]
        [Required]
        [MaxLength(16)]
        public string HttpMethod { get; set; }

        /// <summary>
        /// 方法名称
        ///</summary>
        [DisplayName("方法名称")]
        [Required]
        [MaxLength(128)]
        public string Method { get; set; }

        /// <summary>
        /// 请求URL
        ///</summary>
        [DisplayName("请求URL")]
        [MaxLength(256)]
        public string Url { get; set; }

        /// <summary>
        /// 用户类型（0其它 1后台用户 2手机端用户）
        ///</summary>
        [DisplayName("用户类型（0其它 1后台用户 2手机端用户）")]
        public int? UrlType { get; set; }

        /// <summary>
        /// 主机地址
        ///</summary>
        [DisplayName("主机地址")]
        [MaxLength(32)]
        public string IpAddress { get; set; }

        /// <summary>
        /// 操作地点
        ///</summary>
        [DisplayName("操作地点")]
        [MaxLength(256)]
        public string Location { get; set; }

        /// <summary>
        /// 请求参数
        ///</summary>
        [DisplayName("请求参数")]
        public string Params { get; set; }

        /// <summary>
        /// 返回值
        ///</summary>
        [DisplayName("返回值")]
        public string Result { get; set; }

        /// <summary>
        /// 操作状态（0正常 1异常）
        ///</summary>
        [DisplayName("操作状态（0正常 1异常）")]
        public int? Status { get; set; }

        /// <summary>
        /// 错误信息
        ///</summary>
        [DisplayName("错误信息")]
        public string ErrorMsg { get; set; }

        /// <summary>
        /// 操作系统
        ///</summary>
        [DisplayName("操作系统")]
        [MaxLength(128)]
        public string OS { get; set; }

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
        protected override void AddChanges(Operate other)
        {
            AddChange(t => t.Title, other.Title);
            AddChange(t => t.Type, other.Type);
            AddChange(t => t.HttpMethod, other.HttpMethod);
            AddChange(t => t.Method, other.Method);
            AddChange(t => t.Url, other.Url);
            AddChange(t => t.UrlType, other.UrlType);
            AddChange(t => t.IpAddress, other.IpAddress);
            AddChange(t => t.Location, other.Location);
            AddChange(t => t.Params, other.Params);
            AddChange(t => t.Result, other.Result);
            AddChange(t => t.Status, other.Status);
            AddChange(t => t.ErrorMsg, other.ErrorMsg);
            AddChange(t => t.OS, other.OS);
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