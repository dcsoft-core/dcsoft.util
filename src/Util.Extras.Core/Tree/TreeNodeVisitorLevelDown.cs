using System;

namespace Util.Extras.Tree
{
    /// <summary>
    /// visit level 1 to level last
    /// </summary>
    /// <typeparam name="T">node type</typeparam>
    public class TreeNodeVisitorLevelDown<T> : TreeNodeVisitor<T>
    {
        /// <summary>
        /// init
        /// </summary>
        /// <param name="tree"></param>
        /// <param name="action"></param>
        /// <param name="fireEvent"></param>
        public TreeNodeVisitorLevelDown(ITree<T> tree, Action<INode<T>> action, bool fireEvent)
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
                DoAction(node);
            }

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
                DoAction(child);
            }

            foreach (var child in node.DirectChildren.Nodes)
            {
                Visit(child);
            }
        }
    }
}