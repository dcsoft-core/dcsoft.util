using System;

namespace Util.Tree
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class NodeTreeDataEventArgs<T> : EventArgs
    {
        /// <summary>
        /// 
        /// </summary>
        public T Data { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        public NodeTreeDataEventArgs(T data)
        {
            Data = data;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class NodeTreeNodeEventArgs<T> : EventArgs
    {
        /// <summary>
        /// 
        /// </summary>
        public INode<T> Node { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        public NodeTreeNodeEventArgs(INode<T> node)
        {
            Node = node;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public enum NodeTreeInsertOperation
    {
        /// <summary>
        /// 
        /// </summary>
        Previous,

        /// <summary>
        /// 
        /// </summary>
        Next,

        /// <summary>
        /// 
        /// </summary>
        Child,

        /// <summary>
        /// 
        /// </summary>
        Tree
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class NodeTreeInsertEventArgs<T> : EventArgs
    {
        /// <summary>
        /// 
        /// </summary>
        public NodeTreeInsertOperation Operation { get; }

        /// <summary>
        /// 
        /// </summary>
        public INode<T> Node { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="operation"></param>
        /// <param name="node"></param>
        public NodeTreeInsertEventArgs(NodeTreeInsertOperation operation, INode<T> node)
        {
            Operation = operation;
            Node = node;
        }
    }
}