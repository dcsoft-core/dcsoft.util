using System;
using System.Collections.Generic;
using DCSoft.Applications.Dtos.Systems;
using DCSoft.Applications.Requests.Systems;
using DCSoft.Applications.Responses.Systems;
using DCSoft.Domain.Extends;
using DCSoft.Domain.Models.Systems;
using Util;
using Util.Helpers;
using Util.Extras.Text;
using Util.Extras.Tree;

namespace DCSoft.Applications.Extensions.Systems
{
    /// <summary>
    /// 模块资源参数扩展
    /// </summary>
    public static partial class Extension
    {
        /// <summary>
        /// 转成模块参数
        /// </summary>
        public static ModuleDto ToModuleDto(this Resource po)
        {
            if (po == null)
                return null;
            var result = po.MapTo<ModuleDto>();
            result.Url = po.Uri;
            var extend = Json.ToObject<ModuleExtend>(po.Extend);
            extend.MapTo(result);
            return result;
        }

        /// <summary>
        /// 转成模块
        /// </summary> 
        public static Module ToModule(this CreateModuleRequest request)
        {
            return request?.MapTo<Module>();
        }

        /// <summary>
        /// 转换树形数据
        /// </summary>
        /// <returns></returns>
        public static IList<ModuleTreeData> ToTreeData(this IEnumerable<ModuleDto> data)
        {
            TreeView<string, ModuleDto> tree =
                new TreeViewByParentId<string, ModuleDto>("Id", "ParentId", "SortId");
            tree.Reset(data);
            // Tree构造
            IList<ModuleTreeData> result = new List<ModuleTreeData>();
            var parentDict = new Dictionary<string, ModuleTreeData>();
            ITreeNodeVisitor<TreeViewData<ModuleDto>> visitor = new TreeNodeVisitorRootToLeaf<TreeViewData<ModuleDto>>(
                tree.Tree, delegate(INode<TreeViewData<ModuleDto>> treeNode)
                {
                    if (treeNode.IsRoot) return;
                    var menu = treeNode.Data.Value;
                    var dto = new ModuleTreeData(menu)
                    {
                        IsLeaf = treeNode.IsLeaf,
                        Expanded = menu.ParentId.IsNullOrEmpty(),
                        Checked = false
                    };
                    if (treeNode.Parent.IsRoot)
                    {
                        result.Add(dto);
                    }
                    else
                    {
                        if (parentDict.ContainsKey(menu.ParentId))
                        {
                            parentDict[menu.ParentId].Children.Add(dto);
                        }
                    }

                    parentDict.Add(treeNode.Data.Value.Id, dto);
                }, false);
            visitor.Visit();
            return result;
        }

        /// <summary>
        /// 转换树形数据
        /// </summary>
        /// <returns></returns>
        public static IList<ModuleDto> ToPermissionData(this IEnumerable<ModuleDto> data, List<Guid> resIds)
        {
            TreeView<string, ModuleDto> tree =
                new TreeViewByParentId<string, ModuleDto>("Id", "ParentId", "SortId");
            tree.Reset(data);
            // Tree构造
            IList<ModuleDto> result = new List<ModuleDto>();
            var parentDict = new Dictionary<string, ModuleDto>();
            ITreeNodeVisitor<TreeViewData<ModuleDto>> visitor = new TreeNodeVisitorRootToLeaf<TreeViewData<ModuleDto>>(
                tree.Tree, delegate(INode<TreeViewData<ModuleDto>> treeNode)
                {
                    if (treeNode.IsRoot) return;
                    var menu = treeNode.Data.Value;
                    var dto = menu;
                    dto.Leaf = treeNode.IsLeaf;
                    dto.Expanded = treeNode.HasNext;
                    if (menu.Type == 1)
                    {
                        result.Add(dto);
                    }
                    else
                    {
                        if (parentDict.ContainsKey(menu.ParentId))
                        {
                            if (menu.Type == 2)
                            {
                                dto.Checked = resIds.Contains(dto.Id.ToGuid());
                                parentDict[menu.ParentId].Operators.Add(dto);
                                parentDict[menu.ParentId].Leaf = true;
                            }
                        }
                    }

                    parentDict.Add(treeNode.Data.Value.Id, dto);
                }, false);
            visitor.Visit();
            return result;
        }
    }
}