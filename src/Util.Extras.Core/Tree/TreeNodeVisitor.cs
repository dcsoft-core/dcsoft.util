using System;

namespace Util.Extras.Tree
{
    /// <summary>
    /// treeNodeVisitor
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class TreeNodeVisitor<T> : ITreeNodeVisitor<T>
    {
        /// <summary>
        /// init
        /// </summary>
        /// <param name="tree"></param>
        /// <param name="action"></param>
        /// <param name="fireEvent"></param>
        protected TreeNodeVisitor(ITree<T> tree, Action<INode<T>> action, bool fireEvent)
        {
            Tree = tree;
            Action = action;
            FireEvent = fireEvent;
        }

        /// <summary>
        /// tree
        /// </summary>
        public ITree<T> Tree { get; set; }

        /// <summary>
        /// action
        /// </summary>
        public Action<INode<T>> Action { get; set; }

        /// <summary>
        /// fireEvent
        /// </summary>
        public bool FireEvent { get; set; }

        /// <summary>
        /// onVisiting
        /// </summary>
        /// <param name="node"></param>
        protected void OnVisiting(INode<T> node)
        {
            if (FireEvent)
            {
                Visiting?.Invoke(this, new TreeNodeVisitingEventArg<T>(node));
            }
        }

        /// <summary>
        /// onVisited
        /// </summary>
        /// <param name="node"></param>
        protected void OnVisited(INode<T> node)
        {
            if (FireEvent)
            {
                Visited?.Invoke(this, new TreeNodeVisitedEventArg<T>(node));
            }
        }

        /// <summary>
        /// doAction
        /// </summary>
        /// <param name="node"></param>
        protected void DoAction(INode<T> node)
        {
            var cancelAction = false;
            if (FireEvent && Visiting != null)
            {
                var arg = new TreeNodeVisitingEventArg<T>(node);
                Visiting(this, arg);
                cancelAction = arg.CancelAction;
            }

            if (!cancelAction)
            {
                Action(node);
            }

            if (FireEvent)
            {
                Visited?.Invoke(this, new TreeNodeVisitedEventArg<T>(node));
            }
        }

        #region ITreeNodeVisitor<T> member

        /// <summary>
        /// visit
        /// </summary>
        public abstract void Visit();

        /// <summary>
        /// visiting
        /// </summary>
        public event EventHandler<TreeNodeVisitingEventArg<T>> Visiting;

        /// <summary>
        /// visited
        /// </summary>
        public event EventHandler<TreeNodeVisitedEventArg<T>> Visited;

        #endregion
    }
}