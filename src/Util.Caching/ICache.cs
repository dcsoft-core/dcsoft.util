using System;
using System.Threading.Tasks;

namespace Util.Caching {
    /// <summary>
    /// 缓存
    /// </summary>
    public interface ICache {
        /// <summary>
        /// 是否存在指定键的缓存
        /// </summary>
        /// <param name="key">缓存键</param>
        bool Exists(string key);
        /// <summary>
        /// 是否存在指定键的缓存
        /// </summary>
        /// <param name="key">缓存键</param>
        Task<bool> ExistsAsync(string key);
        /// <summary>
        /// 从缓存中获取数据，如果不存在，则执行获取数据操作并添加到缓存中
        /// </summary>
        /// <typeparam name="T">缓存数据类型</typeparam>
        /// <param name="key">缓存键</param>
        /// <param name="expiration">过期时间间隔</param>
        T Get<T>(string key, TimeSpan? expiration = null);
        /// <summary>
        /// 从缓存中获取数据，如果不存在，则执行获取数据操作并添加到缓存中
        /// </summary>
        /// <typeparam name="T">缓存数据类型</typeparam>
        /// <param name="key">缓存键</param>
        /// <param name="func">获取数据操作</param>
        /// <param name="expiration">过期时间间隔</param>
        T Get<T>(string key, Func<T> func, TimeSpan? expiration = null);
        /// <summary>
        /// 从缓存中获取数据，如果不存在，则执行获取数据操作并添加到缓存中
        /// </summary>
        /// <typeparam name="T">缓存数据类型</typeparam>
        /// <param name="key">缓存键</param>
        /// <param name="expiration">过期时间间隔</param>
        Task<T> GetAsync<T>(string key, TimeSpan? expiration = null);
        /// <summary>
        /// 从缓存中获取数据，如果不存在，则执行获取数据操作并添加到缓存中
        /// </summary>
        /// <typeparam name="T">缓存数据类型</typeparam>
        /// <param name="key">缓存键</param>
        /// <param name="func">获取数据操作</param>
        /// <param name="expiration">过期时间间隔</param>
        Task<T> GetAsync<T>(string key, Func<Task<T>> func, TimeSpan? expiration = null);
        /// <summary>
        /// 当缓存数据不存在则添加，已存在不会添加，添加成功返回true
        /// </summary>
        /// <typeparam name="T">缓存数据类型</typeparam>
        /// <param name="key">缓存键</param>
        /// <param name="value">值</param>
        /// <param name="expiration">过期时间间隔</param>
        bool TryAdd<T>(string key, T value, TimeSpan? expiration = null);
        /// <summary>
        /// 当缓存数据不存在则添加，已存在不会添加，添加成功返回true
        /// </summary>
        /// <typeparam name="T">缓存数据类型</typeparam>
        /// <param name="key">缓存键</param>
        /// <param name="value">值</param>
        /// <param name="expiration">过期时间间隔</param>
        Task<bool> TryAddAsync<T>(string key, T value, TimeSpan? expiration = null);
        /// <summary>
        /// 移除缓存
        /// </summary>
        /// <param name="key">缓存键</param>
        void Remove(string key);
        /// <summary>
        /// 移除缓存
        /// </summary>
        /// <param name="key">缓存键</param>
        Task RemoveAsync(string key);
        /// <summary>
        /// 移除缓存
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<long> RemoveAsync(params string[] key);
        /// <summary>
        /// 移除缓存，根据匹配
        /// </summary>
        /// <param name="pattern"></param>
        /// <returns></returns>
        Task<long> RemoveByPatternAsync(string pattern);
        /// <summary>
        /// 清空缓存
        /// </summary>
        void Clear();
        /// <summary>
        /// 清空缓存
        /// </summary>
        Task ClearAsync();
    }
}
