using System;

namespace Util.Extras.Tree
{
    /// <summary>
    /// ITreeNodeVisitor
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ITreeNodeVisitor<T>
    {
        /// <summary>
        /// 访问method
        /// </summary>
        void Visit();

        /// <summary>
        /// 访问前事件
        /// </summary>
        event EventHandler<TreeNodeVisitingEventArg<T>> Visiting;

        /// <summary>
        /// 访问后事件
        /// </summary>
        event EventHandler<TreeNodeVisitedEventArg<T>> Visited;
    }
}