using System.Collections.Generic;
using DCSoft.Applications.Dtos.Commons;
using DCSoft.Applications.Requests.Commons;
using DCSoft.Applications.Responses.Commons;
using DCSoft.Domain.Models.Commons;
using Util;
using Util.Extras.Text;
using Util.Extras.Tree;

namespace DCSoft.Applications.Extensions.Commons
{
    /// <summary>
    /// 部门参数扩展
    /// </summary>
    public static class DepartmentDtoExtension
    {
        /// <summary>
        /// 转成部门参数
        /// </summary>
        public static DepartmentDto ToDepartmentDto(this Department po)
        {
            if (po == null)
                return null;
            var result = po.MapTo<DepartmentDto>();
            return result;
        }

        /// <summary>
        /// 转成部门
        /// </summary> 
        public static Department ToDepartment(this CreateDepartmentRequest request)
        {
            return request.MapTo<Department>();
        }

        /// <summary>
        /// 转换树形数据
        /// </summary>
        /// <returns></returns>
        public static IList<DepartmentTreeData> ToTreeData(this IEnumerable<DepartmentDto> data)
        {
            TreeView<string, DepartmentDto> tree =
                new TreeViewByParentId<string, DepartmentDto>("Id", "ParentId", "SortId");
            tree.Reset(data);
            // Tree构造
            IList<DepartmentTreeData> result = new List<DepartmentTreeData>();
            var parentDict = new Dictionary<string, DepartmentTreeData>();
            ITreeNodeVisitor<TreeViewData<DepartmentDto>> visitor =
                new TreeNodeVisitorRootToLeaf<TreeViewData<DepartmentDto>>(
                    tree.Tree, delegate (INode<TreeViewData<DepartmentDto>> treeNode)
                    {
                        if (treeNode.IsRoot) return;
                        var menu = treeNode.Data.Value;
                        var dto = new DepartmentTreeData(menu)
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
    }
}