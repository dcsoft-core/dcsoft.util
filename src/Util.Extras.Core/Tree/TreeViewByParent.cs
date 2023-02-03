using System;
using System.Collections.Generic;

// ReSharper disable RedundantLambdaParameterType
// ReSharper disable RedundantCast
// ReSharper disable AutoPropertyCanBeMadeGetOnly.Local

namespace Util.Extras.Tree
{
    /// <inheritdoc />
    /// <summary>
    /// construct tree by parent/child
    /// </summary>
    /// <typeparam name="TK">key type</typeparam>
    /// <typeparam name="TV">value type</typeparam>
    /// <remarks>parent should previous child</remarks>
    public class TreeViewByParent<TK, TV> : TreeView<TK, TV> where TK : notnull
    {
        /// <inheritdoc />
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="keyPropName">key prop</param>
        /// <param name="parentPropName">parent prop</param>
        /// <param name="sortPropName">sort prop</param>
        public TreeViewByParent(string keyPropName,
            string parentPropName,
            string sortPropName)
        {
            GetKeyDelegate = FieldFuncUtil.GetFunc<TV, TK>(keyPropName);
            GetParentDelegate = FieldFuncUtil.GetFunc<TV, TV>(parentPropName);
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
        /// <param name="getParent">parent delegate</param>
        /// <param name="compareValue"></param>
        public TreeViewByParent(Func<TV, TK> getKey,
            Func<TV, TV> getParent,
            Comparison<TV> compareValue)
        {
            GetKeyDelegate = getKey;
            GetParentDelegate = getParent;
            CompareValueDelegate = compareValue;
        }

        #region override method

        /// <summary>
        /// reset
        /// </summary>
        /// <param name="nodeValues"></param>
        /// <exception cref="ApplicationException"></exception>
        public override void Reset(IEnumerable<TV> nodeValues)
        {
            InitCollection();
            foreach (var value in nodeValues)
            {
                var parentData = GetParentDelegate(value);
                var nodeData = new TreeViewData<TV>(value, null, false);
                if (parentData == null)
                {
                    InsertNode(Tree, nodeData);
                }
                else
                {
                    var parentKey = (TK)GetKey(parentData);
                    if (!NodeDict.ContainsKey(parentKey))
                    {
                        throw new ApplicationException($"{parentKey} parent not found");
                    }

                    InsertNode(NodeDict[parentKey], nodeData);
                }
            }
        }

        /// <summary>
        /// insertNode
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override INode<TreeViewData<TV>> InsertNode(TV value)
        {
            var parentData = GetParentDelegate(value);
            var nodeData = new TreeViewData<TV>(value, null, false);
            if (parentData == null)
            {
                return InsertNode(Tree, nodeData);
            }
            else
            {
                var parentKey = (TK)GetKey(parentData);
                return InsertNode(NodeDict[parentKey], nodeData);
            }
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
        /// getKeyDelegate
        /// </summary>
        public Func<TV, TK> GetKeyDelegate { get; set; }

        /// <summary>
        /// getParentDelegate
        /// </summary>
        public Func<TV, TV> GetParentDelegate { get; set; }

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