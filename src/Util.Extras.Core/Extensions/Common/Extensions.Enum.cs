using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

// ReSharper disable once CheckNamespace
namespace Util.Extras.Extensions
{
    public static partial class Extensions
    {
        /// <summary>
        /// 转描述
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public static string ToDescription(this Enum item)
        {
            string name = item.ToString();
            var desc = item.GetType().GetField(name)?.GetCustomAttribute<DescriptionAttribute>();
            return desc?.Description ?? name;
        }

        /// <summary>
        /// 转64位整型
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public static long ToInt64(this Enum item)
        {
            return Convert.ToInt64(item);
        }

        /// <summary>
        /// 转列表
        /// </summary>
        /// <param name="value"></param>
        /// <param name="ignoreUnKnown"></param>
        /// <returns></returns>
        public static List<Item> ToList(this Enum value, bool ignoreUnKnown = false)
        {
            var enumType = value.GetType();

            if (!enumType.IsEnum)
                return null;

            return Enum.GetValues(enumType).Cast<Enum>()
                .Where(m => !ignoreUnKnown || !m.ToString().Equals("UnKnown")).Select(x => new Item
                (
                    x.ToDescription(), x
                )).ToList();
        }

        /// <summary>
        /// 转列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ignoreUnKnown"></param>
        /// <returns></returns>
        public static List<Item> ToList<T>(bool ignoreUnKnown = false)
        {
            var enumType = typeof(T);

            if (!enumType.IsEnum)
                return null;

            return Enum.GetValues(enumType).Cast<Enum>()
                .Where(m => !ignoreUnKnown || !m.ToString().Equals("UnKnown")).Select(x => new Item
                (
                    x.ToDescription(), x
                )).ToList();
        }
    }
}