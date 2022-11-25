using System.Collections.Generic;
using DCSoft.Applications.Dtos.Commons;

namespace DCSoft.Applications.Responses.Commons
{
    /// <summary>
    /// 部门树形数据返回值
    /// </summary>
    public class DepartmentTreeResponse
    {
        /// <summary>
        /// 节点列表
        /// </summary>
        public IList<DepartmentTreeData> Nodes { get; set; }

        /// <summary>
        /// 展开的Key
        /// </summary>
        public string[] ExpandedKeys { get; set; }
    }

    /// <summary>
    /// 部门树形数据
    /// </summary>
    public class DepartmentTreeData
    {
        /// <summary>
        /// 初始化
        /// </summary>
        public DepartmentTreeData(DepartmentDto dto)
        {
            Id = dto.Id;
            Key = dto.Id;
            Code = dto.Code;
            Title = dto.Name;
            ParentId = dto.ParentId;
            Level = dto.Level;
            Children = new List<DepartmentTreeData>();
        }

        /// <summary>
        /// 标识
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 标识
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 文本
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 父标识
        /// </summary>
        public string ParentId { get; set; }

        /// <summary>
        /// 级别
        /// </summary>
        public int? Level { get; set; }

        /// <summary>
        /// 是否叶子
        /// </summary>
        public bool IsLeaf { get; set; }

        /// <summary>
        /// 是否展开
        /// </summary>
        public bool Expanded { get; set; }

        /// <summary>
        /// 是否选中
        /// </summary>
        public bool Checked { get; set; }

        /// <summary>
        /// 子菜单
        /// </summary>
        public List<DepartmentTreeData> Children { get; set; }
    }
}