using System;

namespace Util.Extras.Tree
{
    /// <summary>
    /// visit from leaf to root
    /// </summary>
    /// <typeparam name="T">node type</typeparam>
    public class TreeNodeVisitorLeafToRoot<T> : TreeNodeVisitor<T>
    {
        /// <summary>
        /// init
        /// </summary>
        /// <param name="tree"></param>
        /// <param name="action"></param>
        /// <param name="fireEvent"></param>
        public TreeNodeVisitorLeafToRoot(ITree<T> tree, Action<INode<T>> action, bool fireEvent)
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
            foreach (var child in node.DirectChildren.Nodes)
            {
                Visit(child);
            }

            DoAction(node);
        }
    }
}