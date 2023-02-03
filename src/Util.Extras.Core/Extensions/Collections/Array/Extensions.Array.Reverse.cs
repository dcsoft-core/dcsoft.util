using System;

// ReSharper disable once CheckNamespace
namespace Util.Extras.Extensions
{
    /// <summary>
    /// 数组(<see cref="Array"/>) 扩展
    /// </summary>
    public static partial class ArrayExtensions
    {
        /// <summary>
        /// 反转
        /// </summary>
        /// <param name="array">数组</param>
        public static void Reverse(this System.Array array) => System.Array.Reverse(array);

        /// <summary>
        /// 反转
        /// </summary>
        /// <param name="array">数组</param>
        /// <param name="index">索引</param>
        /// <param name="length">长度</param>
        public static void Reverse(this System.Array array, int index, int length) =>
            System.Array.Reverse(array, index, length);
    }
}