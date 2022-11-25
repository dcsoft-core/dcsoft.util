using DCSoft.Domain.Enums;
using DCSoft.Domain.Models.Systems;

namespace DCSoft.Domain.Extends
{
    /// <summary>
    /// 应用程序扩展信息
    /// </summary>
    public class ApplicationExtend
    {
        /// <summary>
        /// 应用程序类型
        /// </summary>
        public ApplicationType ApplicationType { get; set; }

        /// <summary>
        /// 是否客户端
        /// </summary>
        public bool IsClient { get; set; }

        /// <summary>
        /// 客户端
        /// </summary>
        public Client Client { get; set; }
    }
}