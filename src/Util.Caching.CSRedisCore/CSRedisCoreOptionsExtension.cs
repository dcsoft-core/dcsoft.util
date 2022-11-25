using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Util.Configs;

namespace Util.Caching.CSRedisCore {
    /// <summary>
    /// CSRedisCore缓存操作配置扩展
    /// </summary>
    public class CSRedisCoreOptionsExtension : OptionsExtensionBase {
        /// <summary>
        /// CSRedisCore缓存配置操作
        /// </summary>
        private readonly string _connectionString;

        /// <summary>
        /// 初始化EasyCaching缓存操作配置扩展
        /// </summary>
        /// <param name="connectionString">EasyCaching缓存配置操作</param>
        public CSRedisCoreOptionsExtension( string connectionString ) {
            _connectionString = connectionString;
        }

        /// <inheritdoc />
        public override void ConfigureServices( HostBuilderContext context, IServiceCollection services ) {
            services.TryAddScoped<ICache, CacheManager>();
            var csRedis = new CSRedis.CSRedisClient(_connectionString);
            RedisHelper.Initialization(csRedis);
        }
    }
}
