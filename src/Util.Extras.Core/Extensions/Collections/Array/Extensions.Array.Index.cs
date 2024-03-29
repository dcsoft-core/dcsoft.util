﻿using System;

// ReSharper disable once CheckNamespace
namespace Util.Extras.Extensions
{
    /// <summary>
    /// 数组(<see cref="Array"/>) 扩展
    /// </summary>
    public static partial class ArrayExtensions
    {
        /// <summary>
        /// 获取指定值的索引
        /// </summary>
        /// <param name="array">数组</param>
        /// <param name="value">值</param>
        public static int IndexOf(this System.Array array, object value) => System.Array.IndexOf(array, value);

        /// <summary>
        /// 获取指定值的索引
        /// </summary>
        /// <param name="array">数组</param>
        /// <param name="value">值</param>
        /// <param name="startIndex">起始索引</param>
        public static int IndexOf(this System.Array array, object value, int startIndex) =>
            System.Array.IndexOf(array, value, startIndex);

        /// <summary>
        /// 获取指定值的索引
        /// </summary>
        /// <param name="array">数组</param>
        /// <param name="value">值</param>
        /// <param name="startIndex">起始索引</param>
        /// <param name="count">计数</param>
        public static int IndexOf(this System.Array array, object value, int startIndex, int count) =>
            System.Array.IndexOf(array, value, startIndex, count);

        /// <summary>
        /// 获取指定值的最后索引
        /// </summary>
        /// <param name="array">数组</param>
        /// <param name="value">值</param>
        public static int LastIndexOf(this System.Array array, object value) => System.Array.LastIndexOf(array, value);

        /// <summary>
        /// 获取指定值的最后索引
        /// </summary>
        /// <param name="array">数组</param>
        /// <param name="value">值</param>
        /// <param name="startIndex">起始索引</param>
        public static int LastIndexOf(this System.Array array, object value, int startIndex) =>
            System.Array.LastIndexOf(array, value, startIndex);

        /// <summary>
        /// 获取指定值的最后索引
        /// </summary>
        /// <param name="array">数组</param>
        /// <param name="value">值</param>
        /// <param name="startIndex">起始索引</param>
        /// <param name="count">计数</param>
        public static int LastIndexOf(this System.Array array, object value, int startIndex, int count) =>
            System.Array.LastIndexOf(array, value, startIndex, count);

        /// <summary>
        /// 是否在数组索引范围内
        /// </summary>
        /// <param name="array">数组</param>
        /// <param name="index">索引</param>
        public static bool WithInIndex(this System.Array array, int index) =>
            array != null && index >= 0 && index < array.Length;

        /// <summary>
        /// 是否在数组索引范围内
        /// </summary>
        /// <param name="array">数组</param>
        /// <param name="index">索引</param>
        /// <param name="dimension">数组维度</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static bool WithInIndex(this System.Array array, int index, int dimension)
        {
            if (dimension <= 0)
                throw new ArgumentOutOfRangeException(nameof(dimension));
            return array != null && index >= array.GetLowerBound(dimension) && index <= array.GetUpperBound(dimension);
        }
    }
}