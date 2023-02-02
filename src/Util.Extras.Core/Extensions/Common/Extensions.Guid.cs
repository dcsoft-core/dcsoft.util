using System;
using Util.Extras.Judgments;

// ReSharper disable once CheckNamespace
namespace Util.Extras.Extensions
{
    /// <summary>
    /// <see cref="Guid"/> 扩展
    /// </summary>
    public static partial class Extensions
    {
        /// <summary>
        /// 是否为空
        /// </summary>
        /// <param name="guid">值</param>
        public static bool IsNullOrEmpty(this Guid? guid) => GuidJudgment.IsNullOrEmpty(guid);

        /// <summary>
        /// 是否为空
        /// </summary>
        /// <param name="guid">值</param>
        public static bool IsNullOrEmpty(this Guid guid) => GuidJudgment.IsNullOrEmpty(guid);
    }
}