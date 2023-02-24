using System.Collections.Generic;

namespace DCSoft.Domain.Models.Systems
{
    /// <summary>
    /// 客户端
    /// </summary>
    public class Client
    {
        /// <summary>
        /// 初始化客户端
        /// </summary>
        public Client()
        {
            AllowedCorsOrigins = new List<string>();
            AllowedScopes = new List<string>();
        }

        /// <summary>
        /// 允许的跨域来源
        /// </summary>
        public List<string> AllowedCorsOrigins { get; set; }

        /// <summary>
        /// 允许的作用域
        /// </summary>
        public List<string> AllowedScopes { get; set; }

        /// <summary>
        /// 访问令牌生命周期
        /// </summary>
        public int AccessTokenLifetime { get; set; }
    }
}