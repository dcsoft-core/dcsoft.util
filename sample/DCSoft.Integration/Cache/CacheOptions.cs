namespace DCSoft.Integration.Cache
{
    /// <summary>
    /// 缓存配置
    /// </summary>
    public class CacheOptions
    {
        /// <summary>
        /// 缓存类型
        /// </summary>
        public CacheType Type { get; set; }

        /// <summary>
        /// Memory配置
        /// </summary>
        public MemoryConfig Memory { get; set; }

        /// <summary>
        /// Redis配置
        /// </summary>
        public RedisConfig Redis { get; set; }

        /// <summary>
        /// Memory配置实例
        /// </summary>
        public class MemoryConfig
        {
        }

        /// <summary>
        /// Redis配置实例
        /// </summary>
        public class RedisConfig
        {
            /// <summary>
            /// Redis配置
            /// </summary>
            public string ConnectionString { get; set; }
        }
    }
}