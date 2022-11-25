using System;
using Convert = Util.Helpers.Convert;

// ReSharper disable once CheckNamespace
namespace Util.Extensions
{
    /// <summary>
    /// 系统扩展 - 类型转换扩展
    /// </summary>
    public static partial class Extensions
    {
        #region ToBool(转换为bool)

        /// <summary>
        /// 转换为bool
        /// </summary>
        /// <param name="obj">数据</param>
        public static bool ToBool(this string obj) => Convert.ToBool(obj);

        /// <summary>
        /// 转换为可空bool
        /// </summary>
        /// <param name="obj">数据</param>
        public static bool? ToBoolOrNull(this string obj) => Convert.ToBoolOrNull(obj);

        #endregion

        #region ToInt(转换为int)

        /// <summary>
        /// 转换为int
        /// </summary>
        /// <param name="obj">数据</param>
        public static int ToInt(this string obj) => Convert.ToInt(obj);

        /// <summary>
        /// 转换为uint
        /// </summary>
        /// <param name="obj">数据</param>
        public static uint ToUInt(this string obj) => Convert.To<uint>(obj);

        /// <summary>
        /// 转换为可空int
        /// </summary>
        /// <param name="obj">数据</param>
        public static int? ToIntOrNull(this string obj) => Convert.ToIntOrNull(obj);

        #endregion

        #region ToLong(转换为long)

        /// <summary>
        /// 转换为long
        /// </summary>
        /// <param name="obj">数据</param>
        public static long ToLong(this string obj) => Convert.ToLong(obj);

        /// <summary>
        /// 转换为可空long
        /// </summary>
        /// <param name="obj">数据</param>
        public static long? ToLongOrNull(this string obj) => Convert.ToLongOrNull(obj);

        #endregion

        #region ToDouble(转换为double)

        /// <summary>
        /// 转换为double
        /// </summary>
        /// <param name="obj">数据</param>
        public static double ToDouble(this string obj) => Convert.ToDouble(obj);

        /// <summary>
        /// 转换为可空double
        /// </summary>
        /// <param name="obj">数据</param>
        public static double? ToDoubleOrNull(this string obj) => Convert.ToDoubleOrNull(obj);

        #endregion

        #region ToDecimal(转换为decimal)

        /// <summary>
        /// 转换为decimal
        /// </summary>
        /// <param name="obj">数据</param>
        public static decimal ToDecimal(this string obj) => Convert.ToDecimal(obj);

        /// <summary>
        /// 转换为可空decimal
        /// </summary>
        /// <param name="obj">数据</param>
        public static decimal? ToDecimalOrNull(this string obj) => Convert.ToDecimalOrNull(obj);

        #endregion

        #region ToDate(转换为日期)

        /// <summary>
        /// 转换为日期
        /// </summary>
        /// <param name="obj">数据</param>
        public static DateTime ToDate(this string obj) => Convert.ToDateTime(obj);

        /// <summary>
        /// 转换为可空日期
        /// </summary>
        /// <param name="obj">数据</param>
        public static DateTime? ToDateOrNull(this string obj) => Convert.ToDateTimeOrNull(obj);

        #endregion

        #region ToSnakeCase(将字符串转换为蛇形策略)

        /// <summary>
        /// 将字符串转换为蛇形策略
        /// </summary>
        /// <param name="str">字符串</param>
        public static string ToSnakeCase(this string str) => Helpers.String.ToSnakeCase(str);

        #endregion

        #region ToCamelCase(将字符串转换为骆驼策略)

        /// <summary>
        /// 将字符串转换为骆驼策略
        /// </summary>
        /// <param name="str">字符串</param>
        public static string ToCamelCase(this string str) => Helpers.String.ToCamelCase(str);

        #endregion
    }
}