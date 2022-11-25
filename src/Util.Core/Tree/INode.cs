using System;

namespace Util.Tree
{
    /// <summary>
    /// INode
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface INode<T> : IEnumerableCollectionPair<T>, IDisposable //, ICollection<T>
    {
        /// <summary>
        /// 
        /// </summary>
        T Data { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        string ToStringRecursive();

        /// <summary>
        /// 
        /// </summary>
        int Depth { get; }

        /// <summary>
        /// 
        /// </summary>
        int BranchIndex { get; }

        /// <summary>
        /// 
        /// </summary>
        int BranchCount { get; }

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
        INode<T> Parent { get; }

        /// <summary>
        /// 
        /// </summary>
        INode<T> Previous { get; }

        /// <summary>
        /// 
        /// </summary>
        INode<T> Next { get; }

        /// <summary>
        /// 
        /// </summary>
        INode<T> Child { get; }

        /// <summary>
        /// 
        /// </summary>
        ITree<T> Tree { get; }

        /// <summary>
        /// 
        /// </summary>
        INode<T> Root { get; }

        /// <summary>
        /// 
        /// </summary>
        INode<T> Top { get; }

        /// <summary>
        /// 
        /// </summary>
        INode<T> First { get; }

        /// <summary>
        /// 
        /// </summary>
        INode<T> Last { get; }

        /// <summary>
        /// 
        /// </summary>
        INode<T> LastChild { get; }

        /// <summary>
        /// 
        /// </summary>
        bool IsTree { get; }

        /// <summary>
        /// 
        /// </summary>
        bool IsRoot { get; }

        /// <summary>
        /// 
        /// </summary>
        bool IsTop { get; }

        /// <summary>
        /// 
        /// </summary>
        bool HasParent { get; }

        /// <summary>
        /// 
        /// </summary>
        bool HasPrevious { get; }

        /// <summary>
        /// 
        /// </summary>
        bool HasNext { get; }

        /// <summary>
        /// 
        /// </summary>
        bool HasChild { get; }

        /// <summary>
        /// 
        /// </summary>
        bool IsLeaf { get; }

        /// <summary>
        /// 
        /// </summary>
        bool IsFirst { get; }

        /// <summary>
        /// 
        /// </summary>
        bool IsLast { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        INode<T> this[T item] { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        bool Contains(INode<T> item);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        bool Contains(T item);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        INode<T> InsertPrevious(T o);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        INode<T> InsertNext(T o);

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
        INode<T> Add(T o);

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
        void InsertPrevious(ITree<T> tree);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tree"></param>
        void InsertNext(ITree<T> tree);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tree"></param>
        void InsertChild(ITree<T> tree);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tree"></param>
        void Add(ITree<T> tree);

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
        ITree<T> Cut();

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
        void Remove();

        /// <summary>
        /// 
        /// </summary>
        bool CanMoveToParent { get; }

        /// <summary>
        /// 
        /// </summary>
        bool CanMoveToPrevious { get; }

        /// <summary>
        /// 
        /// </summary>
        bool CanMoveToNext { get; }

        /// <summary>
        /// 
        /// </summary>
        bool CanMoveToChild { get; }

        /// <summary>
        /// 
        /// </summary>
        bool CanMoveToFirst { get; }

        /// <summary>
        /// 
        /// </summary>
        bool CanMoveToLast { get; }

        /// <summary>
        /// 
        /// </summary>
        void MoveToParent();

        /// <summary>
        /// 
        /// </summary>
        void MoveToPrevious();

        /// <summary>
        /// 
        /// </summary>
        void MoveToNext();

        /// <summary>
        /// 
        /// </summary>
        void MoveToChild();

        /// <summary>
        /// 
        /// </summary>
        void MoveToFirst();

        /// <summary>
        /// 
        /// </summary>
        void MoveToLast();

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