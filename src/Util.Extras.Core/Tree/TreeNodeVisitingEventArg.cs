namespace Util.Extras.Tree
{
    /// <summary>
    /// treeNodeVisitingEventArg
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class TreeNodeVisitingEventArg<T> : System.EventArgs
    {
        /// <summary>
        /// init
        /// </summary>
        public TreeNodeVisitingEventArg()
        {
        }

        /// <summary>
        /// treeNodeVisitingEventArg
        /// </summary>
        /// <param name="node"></param>
        public TreeNodeVisitingEventArg(INode<T> node)
        {
            Node = node;
            CancelAction = false;
        }

        /// <summary>
        /// node
        /// </summary>
        public INode<T> Node { get; set; }

        /// <summary>
        /// cancelAction
        /// </summary>
        public bool CancelAction { get; set; }
    }
}