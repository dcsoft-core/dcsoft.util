using System;
using System.Collections.Generic;
using System.Linq;
// ReSharper disable RedundantLambdaParameterType
// ReSharper disable UnusedMember.Global
// ReSharper disable AutoPropertyCanBeMadeGetOnly.Local

namespace Util.Extras.Tree
{
    /// <inheritdoc />
    /// <summary>
    /// construct tree by path
    /// </summary>
    /// <typeparam name="TK">key type</typeparam>
    /// <typeparam name="TV">value type</typeparam>
    /// <remarks>sorted by path</remarks>
    public class TreeViewByPath<TK, TV> : TreeView<TK, TV> where TK : notnull
    {
        /// <inheritdoc />
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="keyPropName">key prop</param>
        /// <param name="pathPropName">path prop</param>
        /// <param name="sortPropName">sort prop</param>
        public TreeViewByPath(string keyPropName,
            string pathPropName,
            string sortPropName)
        {
            GetKeyDelegate = FieldFuncUtil.GetFunc<TV, TK>(keyPropName);
            GetPathDelegate = FieldFuncUtil.GetFunc<TV, string>(pathPropName);
            GetSortDelegate = FieldFuncUtil.GetFunc<TV, object>(sortPropName);
            CompareValueDelegate = (TV x, TV y) =>
            {
                var xs = GetSortDelegate(x) as IComparable;
                var ys = GetSortDelegate(y) as IComparable;
                if (xs == null)
                {
                    return ys == null ? 0 : -1;
                }

                return xs.CompareTo(ys);
            };
        }

        /// <inheritdoc />
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="getKey">key delegate</param>
        /// <param name="getPath">path delegate</param>
        /// <param name="compareValue">sort delegate</param>
        public TreeViewByPath(Func<TV, TK> getKey,
            Func<TV, string> getPath,
            Comparison<TV> compareValue)
        {
            GetKeyDelegate = getKey;
            GetPathDelegate = getPath;
            CompareValueDelegate = compareValue;
        }

        #region override method

        /// <summary>
        /// reset
        /// </summary>
        /// <param name="nodeValues"></param>
        public override void Reset(IEnumerable<TV> nodeValues)
        {
            InitCollection();
            //init parent stack
            var parentStack = new Stack<INode<TreeViewData<TV>>>();
            foreach (var value in nodeValues)
            {
                var path = GetPathDelegate(value);
                //get parent node
                while (parentStack.Count > 0 && !path.Contains(GetPathDelegate(parentStack.Peek().Data.Value)))
                {
                    parentStack.Pop();
                }

                var nodeData = new TreeViewData<TV>(value, null, false);
                var node = parentStack.Count == 0 ? InsertNode(Tree, nodeData) : InsertNode(parentStack.Peek(), nodeData);
                //push
                parentStack.Push(node);
            }
        }

        /// <summary>
        /// insertNode
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override INode<TreeViewData<TV>> InsertNode(TV value)
        {
            var path = GetPathDelegate(value);
            PathList.Add(path, null);
            var index = PathList.IndexOfKey(path);
            INode<TreeViewData<TV>> parentNode;
            if (index != 0)
            {
                var prevNode = PathList.ElementAt(index - 1).Value;
                if (path.Contains(GetPathDelegate(prevNode.Data.Value)))
                {
                    parentNode = prevNode;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }

            var childNodes = new List<INode<TreeViewData<TV>>>();
            foreach (var currChild in parentNode.DirectChildren.Nodes)
            {
                if (GetPathDelegate(currChild.Data.Value).Contains(path))
                {
                    childNodes.Add(currChild);
                }
            }

            var nodeData = new TreeViewData<TV>(value, null, false);
            //delete for sort temp value
            PathList.Remove(path);
            var node = InsertNode(parentNode, nodeData);
            //insert then change parent
            BeginInit();
            foreach (var currChild in childNodes)
            {
                var childTree = currChild.Cut();
                var current = node.Child;
                var childValue = childTree.Root.Child.Data;
                while (current != null && CompareValue(childValue.Value, current.Data.Value) >= 0)
                {
                    current = current.Next;
                }

                if (current == null)
                {
                    node.AddChild(childTree);
                }
                else
                {
                    current.InsertPrevious(childTree);
                }
            }

            EndInit();
            return node;
        }

        /// <summary>
        /// initCollection
        /// </summary>
        protected override void InitCollection()
        {
            PathList ??= new SortedList<string, INode<TreeViewData<TV>>>();

            base.InitCollection();
        }

        /// <summary>
        /// onTreeCutting
        /// </summary>
        /// <param name="node"></param>
        protected override void OnTreeCutting(INode<TreeViewData<TV>> node)
        {
            foreach (var n in node.All.Nodes)
            {
                NodeDict.Remove(GetKey(n.Data.Value));
                PathList.Remove(GetPathDelegate(n.Data.Value));
            }
        }

        /// <summary>
        /// onTreeInserted
        /// </summary>
        /// <param name="node"></param>
        protected override void OnTreeInserted(INode<TreeViewData<TV>> node)
        {
            foreach (var n in node.All.Nodes)
            {
                NodeDict.Add(GetKey(n.Data.Value), n);
                PathList.Add(GetPathDelegate(n.Data.Value), n);
            }
        }

        /// <summary>
        /// onTreeCleared
        /// </summary>
        protected override void OnTreeCleared()
        {
            NodeDict.Clear();
            PathList.Clear();
        }

        /// <summary>
        /// getKey
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        protected override TK GetKey(TV value)
        {
            return GetKeyDelegate(value);
        }

        /// <summary>
        /// compareValue
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        protected override int CompareValue(TV x, TV y)
        {
            return CompareValueDelegate(x, y);
        }

        #endregion

        #region extend props

        /// <summary>
        /// path list
        /// </summary>
        public SortedList<string, INode<TreeViewData<TV>>> PathList { get; set; }

        /// <summary>
        /// getKeyDelegate
        /// </summary>
        public Func<TV, TK> GetKeyDelegate { get; set; }

        /// <summary>
        /// getPathDelegate
        /// </summary>
        public Func<TV, string> GetPathDelegate { get; set; }

        /// <summary>
        /// compareValueDelegate
        /// </summary>
        public Comparison<TV> CompareValueDelegate { get; set; }

        /// <summary>
        /// getSortDelegate
        /// </summary>
        private Func<TV, object> GetSortDelegate { get; set; }

        #endregion
    }
}