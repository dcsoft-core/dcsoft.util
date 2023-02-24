using System;
using System.Collections.Generic;

// ReSharper disable UnusedMember.Global
// ReSharper disable RedundantLambdaParameterType
// ReSharper disable ConditionIsAlwaysTrueOrFalse
// ReSharper disable AutoPropertyCanBeMadeGetOnly.Local

namespace Util.Extras.Tree
{
    /// <inheritdoc />
    /// <summary>
    /// construct by id/parentId
    /// </summary>
    /// <typeparam name="TK">key type</typeparam>
    /// <typeparam name="TV">value type</typeparam>
    /// <remarks>parentId previous id</remarks>
    public class TreeViewByParentId<TK, TV> : TreeView<TK, TV> where TK : notnull
    {
        /// <inheritdoc />
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="keyPropName">key prop</param>
        /// <param name="parentIdPropName">parent id prop</param>
        /// <param name="sortPropName">sort prop</param>
        public TreeViewByParentId(string keyPropName,
            string parentIdPropName,
            string sortPropName)
        {
            GetKeyDelegate = FieldFuncUtil.GetFunc<TV, TK>(keyPropName);
            GetParentIdDelegate = FieldFuncUtil.GetFunc<TV, TK>(parentIdPropName);
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
        /// <param name="getParentId">parentId delegate</param>
        /// <param name="compareValue">sort delegate</param>
        public TreeViewByParentId(Func<TV, TK> getKey,
            Func<TV, TK> getParentId,
            Comparison<TV> compareValue)
        {
            GetKeyDelegate = getKey;
            GetParentIdDelegate = getParentId;
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
            foreach (var value in nodeValues)
            {
                var parentId = GetParentIdDelegate(value);
                var nodeData = new TreeViewData<TV>(value, null, false);
                if (parentId == null || !NodeDict.ContainsKey(parentId))
                {
                    InsertNode(Tree, nodeData);
                }
                else
                {
                    InsertNode(NodeDict[parentId], nodeData);
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
            var parentId = GetParentIdDelegate(value);
            var nodeData = new TreeViewData<TV>(value, null, false);
            if (parentId == null || !NodeDict.ContainsKey(parentId))
            {
                return InsertNode(Tree, nodeData);
            }
            else
            {
                return InsertNode(NodeDict[parentId], nodeData);
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
        /// getParentIdDelegate
        /// </summary>
        public Func<TV, TK> GetParentIdDelegate { get; set; }

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