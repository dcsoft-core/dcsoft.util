using System.Collections;
using System.Collections.Generic;

namespace Util.Tree
{
    /// <summary>
    /// IEnumerableCollection
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IEnumerableCollection<T> : IEnumerable<T>, ICollection
    {
        /// <summary>
        /// Contains
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        bool Contains(T item);
    }
}