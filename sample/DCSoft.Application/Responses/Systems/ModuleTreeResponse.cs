using System.Collections.Generic;
using DCSoft.Applications.Dtos.Systems;

namespace DCSoft.Applications.Responses.Systems
{
    /// <summary>
    /// 模块树形数据返回值
    /// </summary>
    public class ModuleTreeResponse
    {
        /// <summary>
        /// 节点列表
        /// </summary>
        public List<ModuleTreeData> Nodes { get; set; }

        /// <summary>
        /// 展开的Key
        /// </summary>
        public string[] ExpandedKeys { get; set; }
    }

    /// <summary>
    /// 模块树形数据
    /// </summary>
    public class ModuleTreeData
    {
        /// <summary>
        /// 初始化
        /// </summary>
        public ModuleTreeData(ModuleDto dto)
        {
            Id = dto.Id;
            Key = dto.Id;
            Title = dto.Name;
            ParentId = dto.ParentId;
            Level = dto.Level;
            Children = new List<ModuleTreeData>();
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
        public List<ModuleTreeData> Children { get; set; }
    }
}