﻿using System.Text;
using Util.Extras.Extensions;

// ReSharper disable once CheckNamespace
namespace Util.Extras.Text
{
    /// <summary>
    /// <see cref="StringBuilder"/> 扩展
    /// </summary>
    public static partial class StringBuilderExtensions
    {
        /// <summary>
        /// 反转字符串
        /// </summary>
        /// <param name="builder">StringBuilder</param>
        public static void Reverse(this StringBuilder builder)
        {
            if (builder == null || builder.Length == 0)
                return;
            var destination = new char[builder.Length];
            builder.CopyTo(0, destination, 0, builder.Length);
            destination.Reverse(0, destination.Length);

            builder.Clear();
            builder.Append(destination);
        }

        /// <summary>
        /// 反转 <see cref="StringBuilder"/>
        /// </summary>
        /// <param name="builder">StringBuilder</param>
        public static StringBuilder ToReverseBuilder(this StringBuilder builder)
        {
            if (builder is null || builder.Length == 0)
                return builder;
            var destination = new char[builder.Length];
            builder.CopyTo(0, destination, 0, builder.Length);
            destination.Reverse(0, destination.Length);
            return new StringBuilder().Append(destination);
        }

        /// <summary>
        /// 反转字符串
        /// </summary>
        /// <param name="builder">StringBuilder</param>
        public static string ToReverseString(this StringBuilder builder)
        {
            if (builder is null || builder.Length == 0)
                return string.Empty;
            return builder.ToReverseBuilder().ToString();
        }
    }
}