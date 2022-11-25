namespace Util.Tree
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class TreeNodeVisitedEventArg<T> : System.EventArgs
    {
        /// <summary>
        /// 
        /// </summary>
        public TreeNodeVisitedEventArg()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        public TreeNodeVisitedEventArg(INode<T> node)
        {
            Node = node;
        }

        /// <summary>
        /// 
        /// </summary>
        public INode<T> Node { get; set; }
    }
}