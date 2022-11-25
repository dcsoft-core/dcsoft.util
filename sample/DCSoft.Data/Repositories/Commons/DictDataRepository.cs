using DCSoft.Domain.Models.Commons;
using DCSoft.Domain.Repositories.Commons;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;
using Microsoft.EntityFrameworkCore;
using Util;
using Util.Data.EntityFrameworkCore.Trees;

namespace DCSoft.Data.Repositories.Commons
{
    /// <summary>
    /// 字典数据仓储
    /// </summary>
    public class DictDataRepository : TreeRepositoryBase<DictData>, IDictDataRepository
    {
        /// <summary>
        /// 初始化字典数据仓储
        /// </summary>
        /// <param name="unitOfWork">工作单元</param>
        public DictDataRepository(IDataUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        /// <summary>
        /// 生成排序号
        /// </summary>
        /// <param name="dictionaryId">应用程序标识</param>
        public async Task<int> GenerateSortIdAsync(Guid? dictionaryId)
        {
            var maxSortId = await Find(t => t.ParentId == dictionaryId).MaxAsync(t => t.SortId);
            return maxSortId.SafeValue() + 1;
        }

        /// <summary>
        /// 获取字典列表数据
        /// </summary>
        /// <param name="code">字典编码</param>
        /// <returns></returns>
        public async Task<List<DictData>> GetListByCodeAsync(string code)
        {
            return await FindAllAsync(t => t.Type.Equals(code) && !t.Code.Equals(code));
        }

        /// <summary>
        /// 获取字典树形数据
        /// </summary>
        /// <param name="code">字典编码</param>
        /// <returns></returns>
        public async Task<List<DictData>> GetTreeByCodeAsync(string code)
        {
            var result = await FindAllAsync(t => t.Type.Equals(code) && !t.Code.Equals(code));
            return result.OrderBy(t => t.Path).ToList();
        }
    }
}