using System.ComponentModel;

namespace DCSoft.Domain.Enums
{
    /// <summary>
    /// 用户类型
    /// </summary>
    public enum UserType
    {
        /// <summary>
        /// 管理员
        /// </summary>
        [Description("管理员")] Admin = 1,

        /// <summary>
        /// 代理商
        /// </summary>
        [Description("代理商")] Agent = 2,

        /// <summary>
        /// 会员
        /// </summary>
        [Description("会员")] Member = 3,
    }
}