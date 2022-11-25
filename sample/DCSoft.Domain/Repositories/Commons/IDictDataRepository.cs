using DCSoft.Domain.Models;
using DCSoft.Domain.Models.Commons;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Util.Domain.Trees;

namespace DCSoft.Domain.Repositories.Commons
{
    /// <summary>
    /// 字典数据仓储
    /// </summary>
    public interface IDictDataRepository : ITreeRepository<DictData>
    {
        /// <summary>
        /// 生成排序号
        /// </summary>
        /// <param name="dictionaryId">字典标识</param>
        Task<int> GenerateSortIdAsync(Guid? dictionaryId);

        /// <summary>
        /// 获取字典列表数据
        /// </summary>
        /// <param name="code">字典编码</param>
        /// <returns></returns>
        Task<List<DictData>> GetListByCodeAsync(string code);

        /// <summary>
        /// 获取字典树形数据
        /// </summary>
        /// <param name="code">字典编码</param>
        /// <returns></returns>
        Task<List<DictData>> GetTreeByCodeAsync(string code);
    }
}