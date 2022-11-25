using System;

namespace Util.Tree
{
    /// <summary>
    /// visit from root to leaf
    /// </summary>
    /// <typeparam name="T">node type</typeparam>
    public class TreeNodeVisitorRootToLeaf<T> : TreeNodeVisitor<T>
    {
        /// <summary>
        /// init
        /// </summary>
        /// <param name="tree"></param>
        /// <param name="action"></param>
        /// <param name="fireEvent"></param>
        public TreeNodeVisitorRootToLeaf(ITree<T> tree, Action<INode<T>> action, bool fireEvent)
            : base(tree, action, fireEvent)
        {
        }

        /// <summary>
        /// visit
        /// </summary>
        public override void Visit()
        {
            foreach (var node in Tree.DirectChildren.Nodes)
            {
                Visit(node);
            }
        }

        /// <summary>
        /// visit
        /// </summary>
        /// <param name="node"></param>
        private void Visit(INode<T> node)
        {
            DoAction(node);
            foreach (var child in node.DirectChildren.Nodes)
            {
                Visit(child);
            }
        }
    }
}