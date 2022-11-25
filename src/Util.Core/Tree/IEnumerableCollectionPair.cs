namespace Util.Tree
{
    /// <summary>
    /// IEnumerableCollectionPair
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IEnumerableCollectionPair<T>
    {
        /// <summary>
        /// Nodes
        /// </summary>
        IEnumerableCollection<INode<T>> Nodes { get; }

        /// <summary>
        /// Values
        /// </summary>
        IEnumerableCollection<T> Values { get; }
    }
}