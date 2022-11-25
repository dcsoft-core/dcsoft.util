using Util.Dependency;

namespace DCSoft.Logging.Serilog
{
    /// <summary>
    /// 日志操作者
    /// </summary>
    public interface ILogger : IScopeDependency
    {
        /// <summary>
        /// 登录日志
        /// </summary>
        /// <param name="userName">登录名</param>
        /// <param name="status">登录状态（0成功 1失败）</param>
        void Login(string userName, LoginStatus status);

        /// <summary>
        /// 登录日志
        /// </summary>
        /// <param name="userName">登录名</param>
        /// <param name="status">登录状态（0成功 1失败）</param>
        /// <param name="promptMsg">提示消息</param>
        void Login(string userName, LoginStatus status, string promptMsg);

        /// <summary>
        /// 操作日志
        /// </summary>
        /// <param name="title">模块标题</param>
        /// <param name="type">业务类型（0其它 1新增 2修改 3删除）</param>
        /// <param name="method">方法名称</param>
        void Operate(string title, BusinessType type, string method);

        /// <summary>
        /// 操作日志
        /// </summary>
        /// <param name="title">模块标题</param>
        /// <param name="type">业务类型（0其它 1新增 2修改 3删除）</param>
        /// <param name="method">方法名称</param>
        /// <param name="response">返回正文</param>
        void Operate(string title, BusinessType type, string method, string response);

        /// <summary>
        /// 操作日志
        /// </summary>
        /// <param name="title">模块标题</param>
        /// <param name="type">业务类型（0其它 1新增 2修改 3删除）</param>
        /// <param name="method">方法名称</param>
        /// <param name="response">返回正文</param>
        /// <param name="userType">操作类别（0其它 1后台用户 2手机端用户）</param>
        void Operate(string title, BusinessType type, string method, string response, OperateType userType);

        /// <summary>
        /// 操作日志
        /// </summary>
        /// <param name="title">模块标题</param>
        /// <param name="type">业务类型（0其它 1新增 2修改 3删除）</param>
        /// <param name="method">方法名称</param>
        /// <param name="response">返回正文</param>
        /// <param name="userType">操作类别（0其它 1后台用户 2手机端用户）</param>
        /// <param name="status">操作状态（0正常 1异常）</param>
        void Operate(string title, BusinessType type, string method, string response, OperateType userType,
            OperateStatus status);

        /// <summary>
        /// 操作日志
        /// </summary>
        /// <param name="title">模块标题</param>
        /// <param name="type">业务类型（0其它 1新增 2修改 3删除）</param>
        /// <param name="method">方法名称</param>
        /// <param name="response">返回正文</param>
        /// <param name="userType">操作类别（0其它 1后台用户 2手机端用户）</param>
        /// <param name="status">操作状态（0正常 1异常）</param>
        /// <param name="errorMsg">错误信息</param>
        void Operate(string title, BusinessType type, string method, string response, OperateType userType,
            OperateStatus status, string errorMsg);
    }
}