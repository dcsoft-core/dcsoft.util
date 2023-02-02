using System;
using System.Collections.Generic;
using System.Linq;
using Util.Extras.Extensions;

namespace Util.Extras.Helpers
{
    /// <summary>
    /// 反射操作
    /// </summary>
    public static class Reflection
    {
        #region GetPublicProperties(获取公共属性列表)

        /// <summary>
        /// 获取公共属性列表
        /// </summary>
        /// <param name="instance">实例</param>
        public static List<Item> GetPublicProperties(object instance)
        {
            var properties = instance.GetType().GetProperties();
            return properties.ToList().Select(t => new Item(t.Name, t.GetValue(instance))).ToList();
        }

        #endregion

        #region IsDeriveClassFrom(判断当前类型是否可由指定类型派生)

        /// <summary>
        /// 判断当前类型是否可由指定类型派生
        /// </summary>
        /// <typeparam name="TBaseType">基类型</typeparam>
        /// <param name="type">当前类型</param>
        /// <param name="canAbstract">能否是抽象类</param>
        public static bool IsDeriveClassFrom<TBaseType>(Type type, bool canAbstract = false) =>
            IsDeriveClassFrom(type, typeof(TBaseType), canAbstract);

        /// <summary>
        /// 判断当前类型是否可由指定类型派生
        /// </summary>
        /// <param name="type">当前类型</param>
        /// <param name="baseType">基类型</param>
        /// <param name="canAbstract">能否是抽象类</param>
        public static bool IsDeriveClassFrom(Type type, Type baseType, bool canAbstract = false)
        {
            Check.NotNull(type, nameof(type));
            Check.NotNull(baseType, nameof(baseType));
            return type.IsClass && (!canAbstract && !type.IsAbstract) && type.IsBaseOn(baseType);
        }

        #endregion

        #region IsBaseOn(返回当前类型是否是指定基类的派生类)

        /// <summary>
        /// 返回当前类型是否是指定基类的派生类
        /// </summary>
        /// <typeparam name="TBaseType">基类型</typeparam>
        /// <param name="type">类型</param>
        public static bool IsBaseOn<TBaseType>(Type type) => IsBaseOn(type, typeof(TBaseType));

        /// <summary>
        /// 返回当前类型是否是指定基类的派生类
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="baseType">基类类型</param>
        public static bool IsBaseOn(Type type, Type baseType) => baseType.IsGenericTypeDefinition
            ? baseType.IsGenericAssignableFrom(type)
            : baseType.IsAssignableFrom(type);

        #endregion

        #region IsGenericAssignableFrom(判断当前泛型类型是否可由指定类型的实例填充)

        /// <summary>
        /// 判断当前泛型类型是否可由指定类型的实例填充
        /// </summary>
        /// <param name="genericType">泛型类型</param>
        /// <param name="type">指定类型</param>
        public static bool IsGenericAssignableFrom(Type genericType, Type type)
        {
            Check.NotNull(genericType, nameof(genericType));
            Check.NotNull(type, nameof(type));
            if (!genericType.IsGenericType)
                throw new ArgumentException("该功能只支持泛型类型的调用，非泛型类型可使用 IsAssignableFrom 方法。");
            var allOthers = new List<Type>() { type };
            if (genericType.IsInterface) allOthers.AddRange(type.GetInterfaces());

            foreach (var other in allOthers)
            {
                var cur = other;
                while (cur != null)
                {
                    if (cur.IsGenericType)
                        cur = cur.GetGenericTypeDefinition();
                    if (cur.IsSubclassOf(genericType) || cur == genericType)
                        return true;
                    cur = cur.BaseType;
                }
            }

            return false;
        }

        #endregion
    }
}
