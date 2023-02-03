using System;
using System.Collections.Generic;

// ReSharper disable VirtualMemberCallInConstructor
// ReSharper disable UnusedParameter.Local
// ReSharper disable IdentifierTypo
// ReSharper disable RedundantLambdaParameterType
// ReSharper disable UnusedMember.Global
// ReSharper disable UnusedVariable

namespace Util.Extras.Tree
{
    /// <inheritdoc />
    /// <summary>
    /// treeView and dictionary hybrid
    /// </summary>
    /// <typeparam name="TK">key type</typeparam>
    /// <typeparam name="TV">value type</typeparam>
    public abstract class TreeView<TK, TV> : System.ComponentModel.ISupportInitialize where TK : notnull
    {
        /// <summary>
        /// construct
        /// </summary>
        protected TreeView()
        {
            InitCollection();
            //hook event
            Tree.Cutting += (sender, e) =>
            {
                if (_initilizing)
                {
                    return;
                }

                OnTreeCutting(sender as INode<TreeViewData<TV>>);
            };
            Tree.Inserted += (sender, e) =>
            {
                if (_initilizing)
                {
                    return;
                }

                OnTreeInserted(e.Node);
            };
            Tree.Cleared += (sender, e) =>
            {
                if (_initilizing)
                {
                    return;
                }

                OnTreeCleared();
            };
        }

        #region ISupportInitialize member

        private bool _initilizing;

        /// <summary>
        /// beginInit
        /// </summary>
        public void BeginInit()
        {
            _initilizing = true;
        }

        /// <summary>
        /// endInit
        /// </summary>
        public void EndInit()
        {
            _initilizing = false;
        }

        #endregion

        #region object method

        /// <summary>
        /// to string
        /// </summary>
        /// <param name="leftWidth">left width</param>
        /// <param name="split">split</param>
        /// <returns>string</returns>
        public string ToString(int leftWidth, string split)
        {
            var strList = new List<string>();
            var v = new TreeNodeVisitorRootToLeaf<TreeViewData<TV>>(
                Tree, (INode<TreeViewData<TV>> node) =>
                {
                    if (leftWidth == 0)
                    {
                        strList.Add(Convert.ToString(node.Data.Value));
                    }
                    else
                    {
                        strList.Add(new string(' ', leftWidth * node.Depth) + Convert.ToString(node.Data.Value));
                    }
                }, false);
            v.Visit();
            return string.Join(split, strList.ToArray());
        }

        /// <summary>
        /// toString
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return ToString(0, "|");
        }

        #endregion

        #region event handler

        /// <summary>
        /// onTreeCutting
        /// </summary>
        /// <param name="node"></param>
        protected virtual void OnTreeCutting(INode<TreeViewData<TV>> node)
        {
            foreach (var n in node.All.Nodes)
            {
                NodeDict.Remove(GetKey(n.Data.Value));
            }
        }

        /// <summary>
        /// onTreeInserted
        /// </summary>
        /// <param name="node"></param>
        protected virtual void OnTreeInserted(INode<TreeViewData<TV>> node)
        {
            foreach (var n in node.All.Nodes)
            {
                NodeDict.Add(GetKey(n.Data.Value), n);
            }
        }

        /// <summary>
        /// onTreeCleared
        /// </summary>
        protected virtual void OnTreeCleared()
        {
            NodeDict.Clear();
        }

        #endregion

        #region init method

        /// <summary>
        /// initCollection
        /// </summary>
        protected virtual void InitCollection()
        {
            try
            {
                NodeDict ??= new Dictionary<TK, INode<TreeViewData<TV>>>();

                Tree ??= NodeTree<TreeViewData<TV>>.NewTree();

                Tree.Clear();
            }
            finally
            {
                OnTreeCleared();
            }
        }

        /// <summary>
        /// reset treeView
        /// </summary>
        /// <param name="nodeValues">value list</param>
        public abstract void Reset(IEnumerable<TV> nodeValues);

        /// <summary>
        /// reset treeView
        /// </summary>
        /// <param name="nodeValues">value list</param>
        /// <param name="checkKeys">checked value key</param>
        /// <param name="check">check state</param>
        public virtual void Reset(IEnumerable<TV> nodeValues, ICollection<TK> checkKeys, bool check)
        {
            Reset(nodeValues);
            SetCheckKeys(checkKeys, check, true);
        }

        #endregion

        #region Check

        /// <summary>
        /// set node check state
        /// </summary>
        /// <param name="isCheck">check delegate</param>
        /// <param name="check">check state</param>
        /// <param name="clear">clear flag</param>
        public void SetCheck(Predicate<INode<TreeViewData<TV>>> isCheck, bool check, bool clear)
        {
            foreach (var node in NodeDict.Values)
            {
                if (isCheck(node))
                {
                    node.Data.Checked = check;
                }
                else
                {
                    node.Data.Checked = (node.Data.Checked & !clear);
                }
            }
        }

        /// <summary>
        /// check all node
        /// </summary>
        /// <param name="check">check state</param>
        public void SetCheckAll(bool check)
        {
            SetCheck((node) => true, check, true);
        }

        /// <summary>
        /// check all leaf node
        /// </summary>
        /// <param name="check">check state</param>
        /// <param name="clear">clear flag</param>
        public void SetCheckAllLeaf(bool check, bool clear)
        {
            SetCheck((node) => node.IsLeaf, check, clear);
        }

        /// <summary>
        /// check level to level
        /// </summary>
        /// <param name="startLevel">start level</param>
        /// <param name="endLevel">end level</param>
        /// <param name="check">check state</param>
        /// <param name="clear">clear</param>
        public void SetCheckLevel(int startLevel, int endLevel, bool check, bool clear)
        {
            SetCheck((node) =>
            {
                var level = node.Depth;
                return level >= startLevel && level <= endLevel;
            }, check, clear);
        }

        /// <summary>
        /// check key
        /// </summary>
        /// <param name="keys">keys</param>
        /// <param name="check">check state</param>
        /// <param name="clear">clear</param>
        public void SetCheckKeys(ICollection<TK> keys, bool check, bool clear)
        {
            if (keys != null)
            {
                var keySet = new HashSet<TK>(keys);
                SetCheck((node) => keySet.Contains(GetKey(node.Data.Value)), check, clear);
            }
        }

        /// <summary>
        /// reverse check
        /// </summary>
        public void SetCheckReverse()
        {
            foreach (var node in NodeDict.Values)
            {
                node.Data.Checked = !node.Data.Checked;
            }
        }

        /// <summary>
        /// get all checked data
        /// </summary>
        /// <param name="check">check state</param>
        /// <returns>values</returns>
        public IEnumerable<TV> GetCheckedData(bool check)
        {
            var tlist = new List<TV>();
            foreach (var node in NodeDict.Values)
            {
                if ((node.Data.Checked == check))
                {
                    yield return node.Data.Value;
                }
            }
        }

        /// <summary>
        /// get check node keys
        /// </summary>
        /// <param name="check">check state</param>
        /// <returns>keys</returns>
        public IEnumerable<TK> GetCheckedKey(bool check)
        {
            foreach (var node in NodeDict.Values)
            {
                if (node.Data.Checked == check)
                {
                    yield return GetKey(node.Data.Value);
                }
            }
        }

        /// <summary>
        /// get max level checked
        /// </summary>
        /// <param name="check">check state</param>
        public int GetCheckedMaxLevel(bool check)
        {
            var maxLevelForChecked = 0;
            foreach (var node in NodeDict.Values)
            {
                if (node.IsLeaf && (node.Data.Checked == check))
                {
                    if (node.Depth > maxLevelForChecked)
                    {
                        maxLevelForChecked = node.Depth;
                    }
                }
            }

            return maxLevelForChecked;
        }

        #endregion

        #region level method

        /// <summary>
        /// max level
        /// </summary>
        public int MaxLevel
        {
            get
            {
                var maxLevel = 0;
                foreach (var node in NodeDict.Values)
                {
                    if (!node.IsTop)
                    {
                        if (node.Depth > maxLevel)
                        {
                            maxLevel = node.Depth;
                        }
                    }
                }

                return maxLevel;
            }
        }

        #endregion

        #region tree and node method

        /// <summary>
        /// get key
        /// </summary>
        /// <param name="value">value</param>
        /// <returns>key</returns>
        protected abstract TK GetKey(TV value);

        /// <summary>
        /// compare value
        /// </summary>
        /// <param name="x">x value</param>
        /// <param name="y">y value</param>
        /// <returns>compare result</returns>
        protected abstract int CompareValue(TV x, TV y);

        /// <summary>
        /// insert node
        /// </summary>
        /// <param name="tree">target tree</param>
        /// <param name="value">node value</param>
        /// <returns>new node</returns>
        protected virtual INode<TreeViewData<TV>> InsertNode(ITree<TreeViewData<TV>> tree, TreeViewData<TV> value)
        {
            return InsertNode(tree.Root, value);
        }

        /// <summary>
        /// insert value
        /// </summary>
        /// <param name="node">target node</param>
        /// <param name="value">value</param>
        /// <returns>new node</returns>
        protected virtual INode<TreeViewData<TV>> InsertNode(INode<TreeViewData<TV>> node, TreeViewData<TV> value)
        {
            if (!node.HasChild)
            {
                return node.AddChild(value);
            }

            var current = node.Child;
            while (CompareValue(value.Value, current.Data.Value) >= 0)
            {
                current = current.Next;
                if (current == null)
                {
                    break;
                }
            }

            if (current == null)
            {
                return node.AddChild(value);
            }
            else
            {
                return current.InsertPrevious(value);
            }
        }

        /// <summary>
        /// insert node
        /// </summary>
        /// <param name="value">node value</param>
        /// <returns>new node</returns>
        public abstract INode<TreeViewData<TV>> InsertNode(TV value);

        /// <summary>
        /// remove node
        /// </summary>
        /// <param name="value">node value</param>
        /// <returns>cut node</returns>
        public virtual ITree<TreeViewData<TV>> CutNode(TV value)
        {
            var key = GetKey(value);
            if (!NodeDict.ContainsKey(key))
            {
                throw new ArgumentException($"cannot exists：{value}");
            }

            return NodeDict[key].Cut();
        }

        #endregion

        #region internal collections

        /// <summary>
        /// node dictionary
        /// </summary>
        public IDictionary<TK, INode<TreeViewData<TV>>> NodeDict { get; private set; }

        /// <summary>
        /// treeView object
        /// </summary>
        public ITree<TreeViewData<TV>> Tree { get; private set; }

        #endregion
    }
}