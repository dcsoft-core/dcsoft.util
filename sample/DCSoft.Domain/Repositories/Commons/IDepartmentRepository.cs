using DCSoft.Domain.Models;
using DCSoft.Domain.Models.Commons;
using System.Threading.Tasks;
using System;
using Util.Domain.Trees;

namespace DCSoft.Domain.Repositories.Commons
{
    /// <summary>
    /// 组织机构仓储
    /// </summary>
    public interface IDepartmentRepository : ITreeRepository<Department>
    {
        /// <summary>
        /// 生成排序号
        /// </summary>
        /// <param name="departmentId">部门标识</param>
        Task<int> GenerateSortIdAsync(Guid? departmentId);

        /// <summary>
        /// 生成部门编码
        /// </summary>
        /// <param name="departmentId"></param>
        /// <returns></returns>
        Task<string> GenerateCodeAsync(Guid? departmentId);

        /// <summary>
        /// 生成部门编码
        /// </summary>
        /// <param name="newParentId"></param>
        /// <param name="departmentId"></param>
        /// <returns></returns>
        Task<string> GenerateNewCodeAsync(Guid? newParentId, Guid? departmentId);
    }
}