using System;
using UAParser;
using Util.Extras.Sessions;
using Util.Sessions;
using Util.Extras.Tools.IPLocation;
using Util.Helpers;
using Web = Util.Extras.Helpers.Web;

namespace DCSoft.Logging.Serilog
{
    /// <summary>
    /// 日志操作者
    /// </summary>
    public class Logger : ILogger
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="loginLog">登录日志</param>
        /// <param name="operatorLog">操作日志</param>
        public Logger(ILoginLog loginLog, IOperateLog operatorLog)
        {
            LoginLog = loginLog;
            OperateLog = operatorLog;
        }

        /// <summary>
        /// 登录日志
        /// </summary>
        private ILoginLog LoginLog { get; }

        /// <summary>
        /// 操作日志
        /// </summary>
        private IOperateLog OperateLog { get; }

        /// <summary>
        /// 登录日志
        /// </summary>
        /// <param name="userName">登录名</param>
        /// <param name="status">登录状态（0成功 1失败）</param>
        public void Login(string userName, LoginStatus status)
        {
            Login(userName, status, "");
        }

        /// <summary>
        /// 登录日志
        /// </summary>
        /// <param name="userName">登录名</param>
        /// <param name="status">登录状态（0成功 1失败）</param>
        /// <param name="promptMsg">提示消息</param>
        public void Login(string userName, LoginStatus status, string promptMsg)
        {
            var ua = Parser.GetDefault().Parse(Web.Browser ?? "");
            var logId = Id.CreateGuid();
            var loginName = userName;
            var ipAddress = Web.Ip;
            var location = IPSearch.GetLocation(ipAddress);
            var operatingSystem = ua.OS.ToString();
            var browser = Web.Browser;
            var creationTime = DateTime.Now;
            var creatorId = Session.Instance.GetUserId();
            var creator = Session.Instance.GetUserName();

            LoginLog.Write(logId, loginName, ipAddress, location, operatingSystem, status, promptMsg, browser,
                creationTime, creatorId, creator, false);
        }

        /// <summary>
        /// 操作日志
        /// </summary>
        /// <param name="title">模块标题</param>
        /// <param name="type">业务类型（0其它 1新增 2修改 3删除）</param>
        /// <param name="method">方法名称</param>
        public void Operate(string title, BusinessType type, string method)
        {
            Operate(title, type, method, string.Empty);
        }

        /// <summary>
        /// 操作日志
        /// </summary>
        /// <param name="title">模块标题</param>
        /// <param name="type">业务类型（0其它 1新增 2修改 3删除）</param>
        /// <param name="method">方法名称</param>
        /// <param name="response">返回正文</param>
        public void Operate(string title, BusinessType type, string method, string response)
        {
            Operate(title, type, method, response, OperateType.Admin);
        }

        /// <summary>
        /// 操作日志
        /// </summary>
        /// <param name="title">模块标题</param>
        /// <param name="type">业务类型（0其它 1新增 2修改 3删除）</param>
        /// <param name="method">方法名称</param>
        /// <param name="response">返回正文</param>
        /// <param name="userType">操作类别（0其它 1后台用户 2手机端用户）</param>
        public void Operate(string title, BusinessType type, string method, string response, OperateType userType)
        {
            Operate(title, type, method, response, userType, OperateStatus.Success);
        }

        /// <summary>
        /// 操作日志
        /// </summary>
        /// <param name="title">模块标题</param>
        /// <param name="type">业务类型（0其它 1新增 2修改 3删除）</param>
        /// <param name="method">方法名称</param>
        /// <param name="response">返回正文</param>
        /// <param name="userType">操作类别（0其它 1后台用户 2手机端用户）</param>
        /// <param name="status">操作状态（0正常 1异常）</param>
        public void Operate(string title, BusinessType type, string method, string response, OperateType userType, OperateStatus status)
        {
            var errorMsg = "";
            Operate(title, type, method, response, userType, status, errorMsg);
        }

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
        public void Operate(string title, BusinessType type, string method, string response, OperateType userType, OperateStatus status,
            string errorMsg)
        {
            var ua = Parser.GetDefault().Parse(Web.Browser ?? "");
            var logId = Id.CreateGuid();
            var httpMethod = Util.Helpers.Web.Request.Method;
            var url = Util.Helpers.Web.Url;
            var param = Web.Body;
            var result = response;
            var ipAddress = Web.Ip;
            var location = IPSearch.GetLocation(ipAddress);
            var operatingSystem = ua.OS.ToString();
            var browser = Web.Browser;
            var creationTime = DateTime.Now;
            var creatorId = Session.Instance.GetUserId();
            var creator = Session.Instance.GetUserName();

            OperateLog.Write(logId, title, type, httpMethod, method, url, userType, ipAddress,
                location, param, result,
                status, errorMsg, operatingSystem, browser, creationTime, creatorId, creator, false);
        }
    }
}