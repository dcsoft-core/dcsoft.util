using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using EasyCaching.Core;

namespace Util.Caching.EasyCaching
{
    /// <summary>
    /// EasyCaching缓存服务
    /// </summary>
    public class CacheManager : ICache
    {
        /// <summary>
        /// 缓存提供器
        /// </summary>
        private readonly IEasyCachingProvider _provider;

        /// <summary>
        /// 初始化缓存
        /// </summary>
        /// <param name="provider">EasyCaching缓存提供器</param>
        public CacheManager(IEasyCachingProvider provider)
        {
            _provider = provider;
        }

        /// <summary>
        /// 获取所有键
        /// </summary>
        /// <returns></returns>
        private List<string> GetAllKeys()
        {
            const BindingFlags flags = BindingFlags.Instance | BindingFlags.NonPublic;
            var entries = _provider.GetType().GetField("_entries", flags)?.GetValue(_provider);
            var cacheItems = entries as IDictionary;
            var keys = new List<string>();
            if (cacheItems == null) return keys;
            foreach (DictionaryEntry cacheItem in cacheItems)
            {
                keys.Add(cacheItem.Key.ToString());
            }

            return keys;
        }

        /// <summary>
        /// 是否存在指定键的缓存
        /// </summary>
        /// <param name="key">缓存键</param>
        public bool Exists(string key)
        {
            return _provider.Exists(key);
        }

        /// <summary>
        /// 是否存在指定键的缓存
        /// </summary>
        /// <param name="key">缓存键</param>
        public async Task<bool> ExistsAsync(string key)
        {
            return await _provider.ExistsAsync(key);
        }

        /// <summary>
        /// 从缓存中获取数据，如果不存在，则执行获取数据操作并添加到缓存中
        /// </summary>
        /// <typeparam name="T">缓存数据类型</typeparam>
        /// <param name="key">缓存键</param>
        /// <param name="expiration">过期时间间隔</param>
        public T Get<T>(string key, TimeSpan? expiration = null)
        {
            return Get<T>(key, null, null);
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
            var result = _provider.Get(key, func, GetExpiration(expiration));
            return result.Value;
        }

        /// <summary>
        /// 从缓存中获取数据，如果不存在，则执行获取数据操作并添加到缓存中
        /// </summary>
        /// <typeparam name="T">缓存数据类型</typeparam>
        /// <param name="key">缓存键</param>
        /// <param name="expiration">过期时间间隔</param>
        public async Task<T> GetAsync<T>(string key, TimeSpan? expiration = null)
        {
            return await GetAsync<T>(key, null, null);
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
            var result = await _provider.GetAsync(key, func, GetExpiration(expiration));
            return result.Value;
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
            return _provider.TrySet(key, value, GetExpiration(expiration));
        }

        /// <summary>
        /// 当缓存数据不存在则添加，已存在不会添加，添加成功返回true
        /// </summary>
        /// <typeparam name="T">缓存数据类型</typeparam>
        /// <param name="key">缓存键</param>
        /// <param name="value">值</param>
        /// <param name="expiration">过期时间间隔</param>
        public async Task<bool> TryAddAsync<T>(string key, T value, TimeSpan? expiration = null)
        {
            return await _provider.TrySetAsync(key, value, GetExpiration(expiration));
        }

        /// <inheritdoc />
        public void Remove(string key)
        {
            _provider.Remove(key);
        }

        /// <inheritdoc />
        public async Task RemoveAsync(string key)
        {
            await _provider.RemoveAsync(key);
        }

        /// <inheritdoc />
        public async Task<long> RemoveAsync(params string[] key)
        {
            foreach (var k in key)
            {
                await _provider.RemoveAsync(k);
            }
            return key.Length.ToString().ToLong();
        }

        /// <inheritdoc />
        public async Task<long> RemoveByPatternAsync(string pattern)
        {
            if (pattern.IsEmpty())
                return default;

            pattern = Regex.Replace(pattern, @"\{.*\}", "(.*)");

            var keys = GetAllKeys().Where(k => Regex.IsMatch(k, pattern));

            var enumerable = keys as string[] ?? keys.ToArray();
            if (enumerable.Any())
            {
                return await RemoveAsync(enumerable.ToArray());
            }

            return default;
        }

        /// <inheritdoc />
        public void Clear()
        {
            _provider.Flush();
        }

        /// <inheritdoc />
        public async Task ClearAsync()
        {
            await _provider.FlushAsync();
        }
    }
}
