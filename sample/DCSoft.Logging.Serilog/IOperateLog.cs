using System;
using Util.Dependency;

namespace DCSoft.Logging.Serilog
{
    /// <summary>
    /// 操作日志接口
    /// </summary>
    public interface IOperateLog : IScopeDependency
    {
        /// <summary>
        /// 消息日志
        /// </summary>
        /// <param name="logId">日志标识</param>
        /// <param name="title">模块标题</param>
        /// <param name="type">业务类型（0其它 1新增 2修改 3删除）</param>
        /// <param name="httpMethod">请求方式</param>
        /// <param name="method">方法名称</param>
        /// <param name="url">请求URL</param>
        /// <param name="urlType">操作类别（0其它 1后台用户 2手机端用户）</param>
        /// <param name="ipAddress">主机地址</param>
        /// <param name="location">操作地点</param>
        /// <param name="param">请求参数</param>
        /// <param name="result">返回值</param>
        /// <param name="status">操作状态（0正常 1异常）</param>
        /// <param name="errorMsg">错误信息</param>
        /// <param name="operatingSystem">操作系统</param>
        /// <param name="browser">浏览器类型</param>
        /// <param name="creationTime">创建时间</param>
        /// <param name="creatorId">创建人编号</param>
        /// <param name="creator">创建人</param>
        /// <param name="lastModificationTime">最后修改时间</param>
        /// <param name="lastModifierId">最后修改人编号</param>
        /// <param name="lastModifier">最后修改人</param>
        /// <param name="isDeleted">是否删除</param>
        /// <param name="version">版本号</param>
        void Write(Guid logId, string title, BusinessType type, string httpMethod, string method, string url,
            OperateType urlType,
            string ipAddress, string location, string param, string result, OperateStatus status, string errorMsg,
            string operatingSystem,
            string browser, DateTime creationTime, Guid creatorId, string creator, DateTime lastModificationTime,
            Guid lastModifierId,
            string lastModifier, bool isDeleted, byte[] version);
    }
}