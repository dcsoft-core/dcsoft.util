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
    /// 字典数据数据传输对象扩展
    /// </summary>
    public static class DictDataDtoExtension
    {
        /// <summary>
        /// 转换为字典数据实体
        /// </summary>
        /// <param name="dto">字典数据数据传输对象</param>
        public static DictData ToEntity(this DictDataDto dto)
        {
            if (dto == null)
                return new DictData();
            return dto.MapTo(new DictData(dto.Id.ToGuid(), dto.Path, dto.Level is { } i ? i : 0));
        }

        /// <summary>
        /// 转成字典
        /// </summary> 
        public static DictData ToEntity(this CreateDictDataRequest request)
        {
            return request.MapTo<DictData>();
        }

        /// <summary>
        /// 转换为字典数据数据传输对象
        /// </summary>
        /// <param name="entity">字典数据实体</param>
        public static DictDataDto ToDto(this DictData entity)
        {
            if (entity == null)
                return new DictDataDto();
            return entity.MapTo<DictDataDto>();
        }

        /// <summary>
        /// 转成SelectItem对象
        /// </summary>
        public static List<DictDataResponse> ToSelectItem(this IEnumerable<DictData> data)
        {
            var result = new List<DictDataResponse>();
            foreach (var item in data)
            {
                result.Add(new DictDataResponse
                {
                    Id = item.Id.ToString(),
                    Text = item.Name,
                    Value = item.Code,
                    Disabled = !item.Enabled
                });
            }

            return result;
        }

        /// <summary>
        /// 转换树形数据
        /// </summary>
        /// <returns></returns>
        public static IList<DictDataTreeResponse> ToTreeData(this IEnumerable<DictDataDto> data)
        {
            TreeView<string, DictDataDto>
                tree = new TreeViewByParentId<string, DictDataDto>("Id", "ParentId", "SortId");
            tree.Reset(data);
            // Tree构造
            IList<DictDataTreeResponse> result = new List<DictDataTreeResponse>();
            var parentDict = new Dictionary<string, DictDataTreeResponse>();
            ITreeNodeVisitor<TreeViewData<DictDataDto>> visitor =
                new TreeNodeVisitorRootToLeaf<TreeViewData<DictDataDto>>(
                    tree.Tree, delegate(INode<TreeViewData<DictDataDto>> treeNode)
                    {
                        if (treeNode.IsRoot) return;
                        var menu = treeNode.Data.Value;
                        var dto = new DictDataTreeResponse(menu)
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