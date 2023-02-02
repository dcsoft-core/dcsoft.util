using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Util.Extras.Text;

// ReSharper disable once CheckNamespace
namespace Util.Extras.Extensions
{
    /// <summary>
    /// 对象(<see cref="object"/>) 扩展
    /// </summary>
    public static partial class Extensions
    {
        /// <summary>
        /// 转整型
        /// </summary>
        /// <param name="thisValue"></param>
        /// <returns></returns>
        public static int ToInt(this object thisValue)
        {
            var reveal = 0;
            if (thisValue == null) return 0;
            if (thisValue != DBNull.Value && int.TryParse(thisValue.ToString(), out reveal))
            {
                return reveal;
            }
            return reveal;
        }

        /// <summary>
        /// 转整型
        /// </summary>
        /// <param name="thisValue"></param>
        /// <param name="errorValue"></param>
        /// <returns></returns>
        public static int ToInt(this object thisValue, int errorValue)
        {
            if (thisValue != null && thisValue != DBNull.Value && int.TryParse(thisValue.ToString(), out var reveal))
            {
                return reveal;
            }
            return errorValue;
        }

        /// <summary>
        /// 转长整型
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static long ToLong(this object s)
        {
            if (s == null || s == DBNull.Value)
                return 0L;

            long.TryParse(s.ToString(), out var result);
            return result;
        }

        /// <summary>
        /// 转货币
        /// </summary>
        /// <param name="thisValue"></param>
        /// <returns></returns>
        public static double ToMoney(this object thisValue)
        {
            if (thisValue != null && thisValue != DBNull.Value && double.TryParse(thisValue.ToString(), out var reveal))
            {
                return reveal;
            }
            return 0;
        }

        /// <summary>
        /// 转货币
        /// </summary>
        /// <param name="thisValue"></param>
        /// <param name="errorValue"></param>
        /// <returns></returns>
        public static double ToMoney(this object thisValue, double errorValue)
        {
            if (thisValue != null && thisValue != DBNull.Value && double.TryParse(thisValue.ToString(), out var reveal))
            {
                return reveal;
            }
            return errorValue;
        }

        /// <summary>
        /// 转字符串
        /// </summary>
        /// <param name="thisValue"></param>
        /// <returns></returns>
        public static string ToString(this object thisValue)
        {
            if (thisValue != null) return thisValue.ToString()?.Trim();
            return "";
        }

        /// <summary>
        /// 转字符串
        /// </summary>
        /// <param name="thisValue"></param>
        /// <param name="errorValue"></param>
        /// <returns></returns>
        public static string ToString(this object thisValue, string errorValue)
        {
            if (thisValue != null) return thisValue.ToString()?.Trim();
            return errorValue;
        }

        /// <summary>
        /// 转换成Double/Single
        /// </summary>
        /// <param name="s"></param>
        /// <param name="digits">小数位数</param>
        /// <returns></returns>
        public static double ToDouble(this object s, int? digits = null)
        {
            if (s == null || s == DBNull.Value)
                return 0d;

            double.TryParse(s.ToString(), out var result);

            if (digits == null)
                return result;

            return Math.Round(result, digits.Value);
        }

        /// <summary>
        /// 转数字
        /// </summary>
        /// <param name="thisValue"></param>
        /// <returns></returns>
        public static decimal ToDecimal(this object thisValue)
        {
            if (thisValue != null && thisValue != DBNull.Value && decimal.TryParse(thisValue.ToString(), out var reveal))
            {
                return reveal;
            }
            return 0;
        }

        /// <summary>
        /// 转数字
        /// </summary>
        /// <param name="thisValue"></param>
        /// <param name="errorValue"></param>
        /// <returns></returns>
        public static decimal ToDecimal(this object thisValue, decimal errorValue)
        {
            if (thisValue != null && thisValue != DBNull.Value && decimal.TryParse(thisValue.ToString(), out var reveal))
            {
                return reveal;
            }
            return errorValue;
        }

        /// <summary>
        /// 转日期
        /// </summary>
        /// <param name="thisValue"></param>
        /// <returns></returns>
        public static DateTime ToDate(this object thisValue)
        {
            var reveal = DateTime.MinValue;
            if (thisValue != null && thisValue != DBNull.Value && DateTime.TryParse(thisValue.ToString(), out reveal))
            {
                reveal = Convert.ToDateTime(thisValue);
            }
            return reveal;
        }

        /// <summary>
        /// 转日期
        /// </summary>
        /// <param name="thisValue"></param>
        /// <param name="errorValue"></param>
        /// <returns></returns>
        public static DateTime ToDate(this object thisValue, DateTime errorValue)
        {
            if (thisValue != null && thisValue != DBNull.Value && DateTime.TryParse(thisValue.ToString(), out var reveal))
            {
                return reveal;
            }
            return errorValue;
        }

        /// <summary>
        /// 转布尔
        /// </summary>
        /// <param name="thisValue"></param>
        /// <returns></returns>
        public static bool ToBool(this object thisValue)
        {
            var reveal = false;
            if (thisValue != null && thisValue != DBNull.Value && bool.TryParse(thisValue.ToString(), out reveal))
            {
                return reveal;
            }
            return reveal;
        }

        /// <summary>
        /// 转字节
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static byte ToByte(this object s)
        {
            if (s == null || s == DBNull.Value)
                return 0;

            byte.TryParse(s.ToString(), out var result);
            return result;
        }

        /// <summary>
        /// 转换为16进制
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="lowerCase">是否小写</param>
        /// <returns></returns>
        public static string ToHex(this byte[] bytes, bool lowerCase = true)
        {
            if (bytes == null)
                return null;

            var result = new StringBuilder();
            var format = lowerCase ? "x2" : "X2";
            for (var i = 0; i < bytes.Length; i++)
            {
                result.Append(bytes[i].ToString(format));
            }

            return result.ToString();
        }

        /// <summary>
        /// 16进制转字节数组
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static byte[] HexToBytes(this string s)
        {
            if (s.IsNull())
                return null;
            var bytes = new byte[s.Length / 2];

            for (var x = 0; x < s.Length / 2; x++)
            {
                var i = (Convert.ToInt32(s.Substring(x * 2, 2), 16));
                bytes[x] = (byte)i;
            }

            return bytes;
        }

        /// <summary>
        /// 转换为Base64
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static string ToBase64(this byte[] bytes)
        {
            if (bytes == null)
                return null;

            return Convert.ToBase64String(bytes);
        }

        #region As(强制转换)

        /// <summary>
        /// 强制转换
        /// </summary>
        /// <typeparam name="T">目标类型</typeparam>
        /// <param name="this">对象</param>
        public static T As<T>(this object @this) => (T)@this;

        /// <summary>
        /// 强制转换。如果转换失败，则返回默认值
        /// </summary>
        /// <typeparam name="T">目标类型</typeparam>
        /// <param name="this">对象</param>
        public static T AsOrDefault<T>(this object @this)
        {
            try
            {
                return (T)@this;
            }
            catch (Exception)
            {
                return default;
            }
        }

        /// <summary>
        /// 强制转换。如果转换失败，则返回默认值
        /// </summary>
        /// <typeparam name="T">目标类型</typeparam>
        /// <param name="this">对象</param>
        /// <param name="defaultVal">默认值</param>
        public static T AsOrDefault<T>(this object @this, T defaultVal)
        {
            try
            {
                return (T)@this;
            }
            catch (Exception)
            {
                return defaultVal;
            }
        }

        /// <summary>
        /// 强制转换。如果转换失败，则返回默认值
        /// </summary>
        /// <typeparam name="T">目标类型</typeparam>
        /// <param name="this">对象</param>
        /// <param name="defaultValueFactory">默认值</param>
        public static T AsOrDefault<T>(this object @this, Func<T> defaultValueFactory)
        {
            try
            {
                return (T)@this;
            }
            catch (Exception)
            {
                return defaultValueFactory();
            }
        }

        /// <summary>
        /// 强制转换。如果转换失败，则返回默认值
        /// </summary>
        /// <typeparam name="T">目标类型</typeparam>
        /// <param name="this">对象</param>
        /// <param name="defaultValueFactory">默认值</param>
        public static T AsOrDefault<T>(this object @this, Func<object, T> defaultValueFactory)
        {
            try
            {
                return (T)@this;
            }
            catch (Exception)
            {
                return defaultValueFactory(@this);
            }
        }

        #endregion

        #region TryAs(尝试强制转换)

        /// <summary>
        /// 尝试强制转换
        /// </summary>
        /// <typeparam name="T">目标类型</typeparam>
        /// <param name="this">对象</param>
        /// <param name="value">值</param>
        public static bool TryAs<T>(this object @this, out T value)
        {
            try
            {
                value = @this.As<T>();
                return true;
            }
            catch
            {
                value = default;
                return false;
            }
        }

        /// <summary>
        /// 尝试强制转换。如果转换失败，则返回默认值
        /// </summary>
        /// <typeparam name="T">目标类型</typeparam>
        /// <param name="this">对象</param>
        /// <param name="defaultValue">默认值</param>
        /// <param name="value">值</param>
        public static bool TryAsOrDefault<T>(this object @this, T defaultValue, out T value)
        {
            try
            {
                value = @this.As<T>();
                return true;
            }
            catch
            {
                value = defaultValue;
                return false;
            }
        }

        /// <summary>
        /// 尝试强制转换。如果转换失败，则返回默认值
        /// </summary>
        /// <typeparam name="T">目标类型</typeparam>
        /// <param name="this">对象</param>
        /// <param name="defaultValueFactory">默认值</param>
        /// <param name="value">值</param>
        public static bool TryAsOrDefault<T>(this object @this, Func<T> defaultValueFactory, out T value)
        {
            try
            {
                value = @this.As<T>();
                return true;
            }
            catch
            {
                value = defaultValueFactory();
                return false;
            }
        }

        /// <summary>
        /// 尝试强制转换。如果转换失败，则返回默认值
        /// </summary>
        /// <typeparam name="T">目标类型</typeparam>
        /// <param name="this">对象</param>
        /// <param name="defaultValueFactory">默认值</param>
        /// <param name="value">值</param>
        public static bool TryAsOrDefault<T>(this object @this, Func<object, T> defaultValueFactory, out T value)
        {
            try
            {
                value = @this.As<T>();
                return true;
            }
            catch
            {
                value = defaultValueFactory(@this);
                return false;
            }
        }

        #endregion

        #region IsOn(是否在指定列表内)

        /// <summary>
        /// 是否在指定列表内
        /// </summary>
        /// <param name="source">数据源</param>
        /// <param name="list">列表</param>
        public static bool IsOn(this byte source, params byte[] list) => IsOn<byte>(source, list);

        /// <summary>
        /// 是否在指定列表内
        /// </summary>
        /// <param name="source">数据源</param>
        /// <param name="list">列表</param>
        public static bool IsOn(this short source, params short[] list) => IsOn<short>(source, list);

        /// <summary>
        /// 是否在指定列表内
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="source">数据源</param>
        /// <param name="list">列表</param>
        public static bool IsOn<T>(this T source, params T[] list) where T : IComparable =>
            list.Any(t => t.CompareTo(source) == 0);

        /// <summary>
        /// 是否在指定列表内
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="source">数据源</param>
        /// <param name="list">列表</param>
        public static bool IsOn<T>(this T source, IEnumerable<T> list) where T : IComparable =>
            list.Any(item => item.CompareTo(source) == 0);

        /// <summary>
        /// 是否在指定列表内
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="source">数据源</param>
        /// <param name="list">列表</param>
        public static bool IsOn<T>(this T source, HashSet<T> list) where T : IComparable => list.Contains(source);

        /// <summary>
        /// 是否在指定列表内
        /// </summary>
        /// <param name="source">数据源</param>
        /// <param name="list">列表</param>
        public static bool IsOnIgnoreCase(this string source, params string[] list) =>
            list.Any(source.EqualsIgnoreCase);

        #endregion
    }
}