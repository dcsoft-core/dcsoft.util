using System;
using System.ComponentModel;
using Util.Data.Queries;

namespace DCSoft.Data.Queries.Logs
{
    /// <summary>
    /// 操作日志查询参数
    /// </summary>
    public class OperateQuery : QueryParameter
    {
        /// <summary>
        /// 日志标识
        ///</summary>
        [Description("日志标识")]
        public Guid? LogId { get; set; }
        /// <summary>
        /// 模块标题
        ///</summary>
        [Description("模块标题")]
        public string Title { get; set; }
        /// <summary>
        /// 业务类型（0其它 1新增 2修改 3删除）
        ///</summary>
        [Description("业务类型（0其它 1新增 2修改 3删除）")]
        public int? Type { get; set; }
        /// <summary>
        /// 请求方式
        ///</summary>
        [Description("请求方式")]
        public string HttpMethod { get; set; }
        /// <summary>
        /// 方法名称
        ///</summary>
        [Description("方法名称")]
        public string Method { get; set; }
        /// <summary>
        /// 请求URL
        ///</summary>
        [Description("请求URL")]
        public string Url { get; set; }
        /// <summary>
        /// 用户类型（0其它 1后台用户 2手机端用户）
        ///</summary>
        [Description("用户类型（0其它 1后台用户 2手机端用户）")]
        public int? UrlType { get; set; }
        /// <summary>
        /// 主机地址
        ///</summary>
        [Description("主机地址")]
        public string IpAddress { get; set; }
        /// <summary>
        /// 操作地点
        ///</summary>
        [Description("操作地点")]
        public string Location { get; set; }
        /// <summary>
        /// 请求参数
        ///</summary>
        [Description("请求参数")]
        public string Params { get; set; }
        /// <summary>
        /// 返回值
        ///</summary>
        [Description("返回值")]
        public string Result { get; set; }
        /// <summary>
        /// 操作状态（0正常 1异常）
        ///</summary>
        [Description("操作状态（0正常 1异常）")]
        public int? Status { get; set; }
        /// <summary>
        /// 错误信息
        ///</summary>
        [Description("错误信息")]
        public string ErrorMsg { get; set; }
        /// <summary>
        /// 操作系统
        ///</summary>
        [Description("操作系统")]
        public string OS { get; set; }
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