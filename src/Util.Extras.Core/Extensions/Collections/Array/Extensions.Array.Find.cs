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
        /// 查找所有
        /// </summary>
        /// <typeparam name="T">泛型类型</typeparam>
        /// <param name="array">数组</param>
        /// <param name="condition">条件</param>
        public static T[] FindAll<T>(this T[] array, Predicate<T> condition) => System.Array.FindAll(array, condition);
    }
}