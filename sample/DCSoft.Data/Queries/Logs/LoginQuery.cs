using System;
using System.ComponentModel;
using Util.Data.Queries;

namespace DCSoft.Data.Queries.Logs
{
    /// <summary>
    /// 登录日志查询参数
    /// </summary>
    public class LoginQuery : QueryParameter
    {
        /// <summary>
        /// 日志标识
        ///</summary>
        [Description("日志标识")]
        public Guid? LogId { get; set; }
        /// <summary>
        /// 登录帐号
        ///</summary>
        [Description("登录帐号")]
        public string LoginName { get; set; }
        /// <summary>
        /// 登录IP地址
        ///</summary>
        [Description("登录IP地址")]
        public string IpAddress { get; set; }
        /// <summary>
        /// 登录地点
        ///</summary>
        [Description("登录地点")]
        public string Location { get; set; }
        /// <summary>
        /// 操作系统
        ///</summary>
        [Description("操作系统")]
        public string OS { get; set; }
        /// <summary>
        /// 登录状态（0成功 1失败）
        ///</summary>
        [Description("登录状态（0成功 1失败）")]
        public int? Status { get; set; }
        /// <summary>
        /// 提示消息
        ///</summary>
        [Description("提示消息")]
        public string PromptMsg { get; set; }
        /// <summary>
        /// 浏览器类型
        ///</summary>
        [Description("浏览器类型")]
        public string Browser { get; set; }
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