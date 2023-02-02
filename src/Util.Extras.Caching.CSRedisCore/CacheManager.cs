using System;
using System.Threading.Tasks;
using Util.Caching;

namespace Util.Extras.Caching.CSRedisCore
{
    /// <summary>
    /// RedisCaching缓存服务
    /// </summary>
    public class CacheManager : ICache
    {
        /// <summary>
        /// 是否存在指定键的缓存
        /// </summary>
        /// <param name="key">缓存键</param>
        public bool Exists(string key)
        {
            return RedisHelper.Exists(key);
        }

        /// <summary>
        /// 从缓存中获取数据，如果不存在，则执行获取数据操作并添加到缓存中
        /// </summary>
        /// <typeparam name="T">缓存数据类型</typeparam>
        /// <param name="key">缓存键</param>
        public T Get<T>(string key)
        {
            return Get<T>(key, null);
        }

        /// <summary>
        /// 从缓存中获取数据，如果不存在，则执行获取数据操作并添加到缓存中
        /// </summary>
        /// <typeparam name="T">缓存数据类型</typeparam>
        /// <param name="key">缓存键</param>
        /// <param name="func">获取数据操作</param>
        /// <param name="expiration">过期时间间隔</param>
        public T Get<T>(string key, Func<T> func, TimeSpan? expiration = null)
        {
            if (RedisHelper.Exists(key))
            {
                return RedisHelper.Get<T>(key);
            }

            var data = func.Invoke();
            RedisHelper.Set(key, data, GetExpiration(expiration));
            return data;
        }

        /// <summary>
        /// 从缓存中获取数据，如果不存在，则执行获取数据操作并添加到缓存中
        /// </summary>
        /// <typeparam name="T">缓存数据类型</typeparam>
        /// <param name="key">缓存键</param>
        /// <param name="func">获取数据操作</param>
        /// <param name="expiration">过期时间间隔</param>
        public async Task<T> GetAsync<T>(string key, Func<Task<T>> func, TimeSpan? expiration = null)
        {
            if (await RedisHelper.ExistsAsync(key))
            {
                return await RedisHelper.GetAsync<T>(key);
            }

            var data = await func.Invoke();
            await RedisHelper.SetAsync(key, data, GetExpiration(expiration));
            return data;
        }

        /// <summary>
        /// 获取过期时间间隔
        /// </summary>
        private TimeSpan GetExpiration(TimeSpan? expiration)
        {
            expiration ??= TimeSpan.FromHours(12);
            return expiration.SafeValue();
        }

        /// <summary>
        /// 当缓存数据不存在则添加，已存在不会添加，添加成功返回true
        /// </summary>
        /// <typeparam name="T">缓存数据类型</typeparam>
        /// <param name="key">缓存键</param>
        /// <param name="value">值</param>
        /// <param name="expiration">过期时间间隔</param>
        public bool TryAdd<T>(string key, T value, TimeSpan? expiration = null)
        {
            var result = false;
            if (!RedisHelper.Exists(key))
            {
                result = RedisHelper.Set(key, value, GetExpiration(expiration));
            }

            return result;
        }

        /// <summary>
        /// 移除缓存
        /// </summary>
        /// <param name="key">缓存键</param>
        public void Remove(string key)
        {
            RedisHelper.Del(key);
        }

        /// <summary>
        /// 清空缓存
        /// </summary>
        public void Clear()
        {
            var keys = RedisHelper.Keys("admin");
            RedisHelper.Del(keys);
        }
    }
}