using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serilog.Sinks
{
    /// <summary>
    /// 日志类型
    /// </summary>
    public enum LogType
    {
        /// <summary>
        /// 系统
        /// </summary>
        Sys,
        /// <summary>
        /// 登录
        /// </summary>
        Login,
        /// <summary>
        /// 操作
        /// </summary>
        Operate
    }
}
