using System;
using System.Collections.Generic;

namespace Util.Extras.Tree
{
    /// <summary>
    /// 根据给出的属性名从类型中构造获取值的委托
    /// </summary>
    public static class FieldFuncUtil
    {
        /// <summary>
        /// 从属性或字段构造获取值的委托
        /// </summary>
        /// <typeparam name="TV">对象类型</typeparam>
        /// <typeparam name="TK">返回类型</typeparam>
        /// <param name="propName">属性或字段名称</param>
        /// <returns>构造的委托</returns>
        public static Func<TV, TK> GetFunc<TV, TK>(string propName)
        {
            if (string.IsNullOrEmpty(propName))
            {
                throw new ArgumentNullException($"属性名:{nameof(propName)}");
            }

            var t = typeof(TV);
            var pi = t.GetProperty(propName);
            if (pi != null)
            {
                if (pi.CanRead)
                {
                    return v => (TK)pi.GetValue(v, null);
                }

                throw new ArgumentException($"参数错误：类型“{t.Name}”的属性“{propName}”为只写。");
            }

            var fi = t.GetField(propName);
            if (fi != null)
            {
                return v => (TK)fi.GetValue(v);
            }

            throw new ArgumentException($"参数错误：类型“{t.Name}”不存在名为“{propName}”的属性或者字段。");
        }

        /// <summary>
        /// 从属性或字段构造设置值的委托
        /// </summary>
        /// <typeparam name="TV">对象类型</typeparam>
        /// <typeparam name="TK">设置值的类型</typeparam>
        /// <param name="propName">属性或字段名称</param>
        /// <returns>构造的委托</returns>
        public static Action<TV, TK> SetFunc<TV, TK>(string propName)
        {
            if (string.IsNullOrEmpty(propName))
            {
                throw new ArgumentNullException($"属性名:{nameof(propName)}");
            }

            var t = typeof(TV);
            var pi = t.GetProperty(propName);
            if (pi != null)
            {
                if (pi.CanWrite)
                {
                    return (v, k) => { pi.SetValue(v, k, null); };
                }

                throw new ArgumentException($"参数错误：类型“{t.Name}”的属性“{propName}”为只读。");
            }

            var fi = t.GetField(propName);
            if (fi != null)
            {
                return (v, k) => { fi.SetValue(v, k); };
            }

            throw new ArgumentException($"参数错误：类型“{t.Name}”不存在名为“{propName}”的属性或者字段。");
        }

        /// <summary>
        /// 获取多个获取值委托的集合
        /// </summary>
        /// <param name="objType">对象类型</param>
        /// <param name="propNames">属性名称集合</param>
        /// <returns>获取的集合</returns>
        public static SortedList<int, Func<object, object>> GetFuncs(Type objType, params string[] propNames)
        {
            var funcs = new SortedList<int, Func<object, object>>();
            for (var col = 0; col < propNames.Length; col++)
            {
                var propName = propNames[col];
                if (string.IsNullOrEmpty(propName))
                {
                    continue;
                }

                var pi = objType.GetProperty(propName);
                if (pi != null)
                {
                    if (pi.CanRead)
                    {
                        funcs.Add(col, v => v == null ? null : pi.GetValue(v, null));
                    }

                    continue;
                }

                var fi = objType.GetField(propName);
                if (fi != null)
                {
                    funcs.Add(col, v => v == null ? null : fi.GetValue(v));
                }
            }

            return funcs;
        }

        /// <summary>
        /// 获取多个设置值委托的集合
        /// </summary>
        /// <param name="objType">对象类型</param>
        /// <param name="propNames">属性名称集合</param>
        /// <returns>获取的集合</returns>
        public static SortedList<int, Action<object, object>> SetFuncs(Type objType, params string[] propNames)
        {
            var funcs = new SortedList<int, Action<object, object>>();
            for (var col = 0; col < propNames.Length; col++)
            {
                var propName = propNames[col];
                if (string.IsNullOrEmpty(propName))
                {
                    continue;
                }

                var pi = objType.GetProperty(propName);
                if (pi != null)
                {
                    if (pi.CanWrite)
                    {
                        funcs.Add(col, (v, k) => { pi.SetValue(v, k, null); });
                    }

                    continue;
                }

                var fi = objType.GetField(propName);
                if (fi == null) continue;
                {
                    funcs.Add(col, (v, k) => { fi.SetValue(v, k); });
                }
            }

            return funcs;
        }
    }
}