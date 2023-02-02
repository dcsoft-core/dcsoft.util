using System;
using DCSoft.Integration.Cache;
using EasyCaching.Core.Configurations;
using EasyCaching.InMemory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Util.Caching;
using Util.Helpers;

namespace DCSoft.Web.Core.Extensions
{
    /// <summary>
    /// Cache缓存扩展
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// 注册缓存操作
        /// </summary>
        /// <param name="services">服务集合</param>
        public static void AddCaches(this IServiceCollection services)
        {
            var cacheOption = Config.Get<CacheOptions>("Cache");
            switch (cacheOption.Type)
            {
                //0内存缓存，1Redis缓存
                case CacheType.Memory:
                    services.AddEasyCache(options =>
                    {
                        options.UseInMemory(config =>
                        {
                            config.DBConfig = new InMemoryCachingOptions
                            {
                                // scan time, default value is 60s
                                ExpirationScanFrequency = 60,
                                // total count of cache items, default value is 10000
                                SizeLimit = 10000
                            };
                        });
                    });
                    break;
                case CacheType.Redis:
                    services.AddRedisCache(cacheOption.Redis.ConnectionString);
                    break;
            }
        }

        /// <summary>
        /// 注册EasyCaching缓存操作
        /// </summary>
        /// <param name="services">服务集合</param>
        /// <param name="configAction">配置操作</param>
        public static void AddEasyCache(this IServiceCollection services, Action<EasyCachingOptions> configAction)
        {
            services.TryAddScoped<ICache, Util.Caching.EasyCaching.CacheManager>();
            services.AddEasyCaching(configAction);
        }

        /// <summary>
        /// 注册EasyCaching缓存操作
        /// </summary>
        /// <param name="services">服务集合</param>
        /// <param name="connectionString">连接字符串</param>
        public static void AddRedisCache(this IServiceCollection services, string connectionString)
        {
            services.AddSingleton<ICache, Util.Extras.Caching.CSRedisCore.CacheManager>();

            var csRedis = new CSRedis.CSRedisClient(connectionString);
            RedisHelper.Initialization(csRedis);
        }
    }
}