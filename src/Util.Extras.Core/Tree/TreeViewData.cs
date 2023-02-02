using System;

namespace Util.Extras.Tree
{
    /// <summary>
    /// TreeView node data
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [Serializable]
    public class TreeViewData<T>
    {
        /// <summary>
        /// init
        /// </summary>
        public TreeViewData()
        {
        }

        /// <summary>
        /// init
        /// </summary>
        /// <param name="value"></param>
        /// <param name="tag"></param>
        /// <param name="check"></param>
        public TreeViewData(T value, object tag, bool check)
        {
            this.Value = value;
            this.Tag = tag;
            this.Checked = check;
        }

        /// <summary>
        /// data
        /// </summary>
        public T Value { get; set; }

        /// <summary>
        /// Tag
        /// </summary>
        public object Tag { get; set; }

        /// <summary>
        /// checked
        /// </summary>
        public bool Checked { get; set; }

        /// <summary>
        /// toString
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"{this.Value};check:{this.Checked}";
        }
    }
}