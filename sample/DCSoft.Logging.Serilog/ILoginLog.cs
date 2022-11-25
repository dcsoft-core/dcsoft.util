using System;
using Util.Dependency;

namespace DCSoft.Logging.Serilog
{
    /// <summary>
    /// 登录日志接口
    /// </summary>
    public interface ILoginLog : IScopeDependency
    {
        /// <summary>
        /// 消息日志
        /// </summary>
        /// <param name="logId">日志标识</param>
        /// <param name="loginName">登录账号</param>
        /// <param name="ipAddress">登录IP地址</param>
        /// <param name="location">登录地点</param>
        /// <param name="operatingSystem">操作系统</param>
        /// <param name="status">登录状态（0成功 1失败）</param>
        /// <param name="promptMsg">提示消息</param>
        /// <param name="browser">浏览器类型</param>
        /// <param name="creationTime">创建时间</param>
        /// <param name="creatorId">创建人编号</param>
        /// <param name="creator">创建人</param>
        /// <param name="lastModificationTime">最后修改时间</param>
        /// <param name="lastModifierId">最后修改人编号</param>
        /// <param name="lastModifier">最后修改人</param>
        /// <param name="isDeleted">是否删除</param>
        /// <param name="version">版本号</param>
        void Write(Guid logId, string loginName, string ipAddress, string location, string operatingSystem,
            LoginStatus status, string promptMsg, string browser, DateTime creationTime, Guid creatorId, string creator,
            DateTime lastModificationTime, Guid lastModifierId, string lastModifier, bool isDeleted, byte[] version);
    }
}