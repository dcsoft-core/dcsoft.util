using System;
using System.Collections.Generic;
using System.IO;

namespace Util.Tree
{
    /// <summary>
    /// ITree
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ITree<T> : IEnumerableCollectionPair<T>, IDisposable
    {
        /// <summary>
        /// 
        /// </summary>
        Type DataType { get; }

        /// <summary>
        /// 
        /// </summary>
        IEqualityComparer<T> DataComparer { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stream"></param>
        void XmlSerialize(Stream stream);

        /// <summary>
        /// 
        /// </summary>
        void Clear();

        /// <summary>
        /// 
        /// </summary>
        int Count { get; }

        /// <summary>
        /// 
        /// </summary>
        int DirectChildCount { get; }

        /// <summary>
        /// 
        /// </summary>
        INode<T> Root { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        INode<T> this[T o] { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        string ToStringRecursive();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        bool Contains(T item);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        bool Contains(INode<T> item);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        INode<T> InsertChild(T o);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        INode<T> AddChild(T o);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tree"></param>
        void InsertChild(ITree<T> tree);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tree"></param>
        void AddChild(ITree<T> tree);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        ITree<T> Cut(T o);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        ITree<T> Copy(T o);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        ITree<T> DeepCopy(T o);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        bool Remove(T o);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        ITree<T> Copy();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        ITree<T> DeepCopy();

        /// <summary>
        /// 
        /// </summary>
        IEnumerableCollectionPair<T> All { get; }

        /// <summary>
        /// 
        /// </summary>
        IEnumerableCollectionPair<T> AllChildren { get; }

        /// <summary>
        /// 
        /// </summary>
        IEnumerableCollectionPair<T> DirectChildren { get; }

        /// <summary>
        /// 
        /// </summary>
        IEnumerableCollectionPair<T> DirectChildrenInReverse { get; }

        /// <summary>
        /// 
        /// </summary>
        event EventHandler<NodeTreeDataEventArgs<T>> Validate;

        /// <summary>
        /// 
        /// </summary>
        event EventHandler Clearing;

        /// <summary>
        /// 
        /// </summary>
        event EventHandler Cleared;

        /// <summary>
        /// 
        /// </summary>
        event EventHandler<NodeTreeDataEventArgs<T>> Setting;

        /// <summary>
        /// 
        /// </summary>
        event EventHandler<NodeTreeDataEventArgs<T>> SetDone;

        /// <summary>
        /// 
        /// </summary>
        event EventHandler<NodeTreeInsertEventArgs<T>> Inserting;

        /// <summary>
        /// 
        /// </summary>
        event EventHandler<NodeTreeInsertEventArgs<T>> Inserted;

        /// <summary>
        /// 
        /// </summary>
        event EventHandler Cutting;

        /// <summary>
        /// 
        /// </summary>
        event EventHandler CutDone;

        /// <summary>
        /// 
        /// </summary>
        event EventHandler<NodeTreeNodeEventArgs<T>> Copying;

        /// <summary>
        /// 
        /// </summary>
        event EventHandler<NodeTreeNodeEventArgs<T>> Copied;

        /// <summary>
        /// 
        /// </summary>
        event EventHandler<NodeTreeNodeEventArgs<T>> DeepCopying;

        /// <summary>
        /// 
        /// </summary>
        event EventHandler<NodeTreeNodeEventArgs<T>> DeepCopied;
    }
}