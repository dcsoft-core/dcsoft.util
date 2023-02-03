using System;
using System.Collections;

// ReSharper disable once CheckNamespace
namespace Util.Extras.Extensions
{
    /// <summary>
    /// 数组(<see cref="Array"/>) 扩展
    /// </summary>
    public static partial class ArrayExtensions
    {
        /// <summary>
        /// 排序
        /// </summary>
        /// <param name="array">数组</param>
        public static void Sort(this System.Array array) => System.Array.Sort(array);

        /// <summary>
        /// 排序
        /// </summary>
        /// <param name="array">数组</param>
        /// <param name="items">其它项数组</param>
        public static void Sort(this System.Array array, System.Array items) => System.Array.Sort(array, items);

        /// <summary>
        /// 排序
        /// </summary>
        /// <param name="array">数组</param>
        /// <param name="index">索引</param>
        /// <param name="length">长度</param>
        public static void Sort(this System.Array array, int index, int length) =>
            System.Array.Sort(array, index, length);

        /// <summary>
        /// 排序
        /// </summary>
        /// <param name="array">数组</param>
        /// <param name="items">其它项数组</param>
        /// <param name="index">索引</param>
        /// <param name="length">长度</param>
        public static void Sort(this System.Array array, System.Array items, int index, int length) =>
            System.Array.Sort(array, items, index, length);

        /// <summary>
        /// 排序
        /// </summary>
        /// <param name="array">数组</param>
        /// <param name="comparer">比较器</param>
        public static void Sort(this System.Array array, IComparer comparer) => System.Array.Sort(array, comparer);

        /// <summary>
        /// 排序
        /// </summary>
        /// <param name="array">数组</param>
        /// <param name="items">其它项数组</param>
        /// <param name="comparer">比较器</param>
        public static void Sort(this System.Array array, System.Array items, IComparer comparer) =>
            System.Array.Sort(array, items, comparer);

        /// <summary>
        /// 排序
        /// </summary>
        /// <param name="array">数组</param>
        /// <param name="index">索引</param>
        /// <param name="length">长度</param>
        /// <param name="comparer">比较器</param>
        public static void Sort(this System.Array array, int index, int length, IComparer comparer) =>
            System.Array.Sort(array, index, length, comparer);

        /// <summary>
        /// 排序
        /// </summary>
        /// <param name="array">数组</param>
        /// <param name="items">其它项数组</param>
        /// <param name="index">索引</param>
        /// <param name="length">长度</param>
        /// <param name="comparer">比较器</param>
        public static void Sort(this System.Array array, System.Array items, int index, int length,
            IComparer comparer) =>
            System.Array.Sort(array, items, index, length, comparer);
    }
}