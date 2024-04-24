using System.Collections.Generic;

namespace Util.Extras.Applications.Dtos;

/// <summary>
/// 树形数据返回值
/// </summary>
public class TreeDataResult<TNode>
{
    /// <summary>
    /// 选中的节点
    /// </summary>
    public List<string> CheckedKeys { get; set; }

    /// <summary>
    /// 选择的节点
    /// </summary>
    public List<string> SelectedKeys { get; set; }

    /// <summary>
    /// 节点集合
    /// </summary>
    public List<TNode> Nodes { get; set; }
}