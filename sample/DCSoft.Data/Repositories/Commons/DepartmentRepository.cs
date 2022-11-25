using DCSoft.Domain.Models.Commons;
using DCSoft.Domain.Repositories.Commons;
using System.Threading.Tasks;
using System;
using Microsoft.EntityFrameworkCore;
using Util;
using Util.Data.EntityFrameworkCore.Trees;

namespace DCSoft.Data.Repositories.Commons
{
    /// <summary>
    /// 组织机构仓储
    /// </summary>
    public class DepartmentRepository : TreeRepositoryBase<Department>, IDepartmentRepository
    {
        /// <summary>
        /// 初始化组织机构仓储
        /// </summary>
        /// <param name="unitOfWork">工作单元</param>
        public DepartmentRepository(IDataUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        /// <summary>
        /// 生成排序号
        /// </summary>
        /// <param name="departmentId">应用程序标识</param>
        public async Task<int> GenerateSortIdAsync(Guid? departmentId)
        {
            var maxSortId = await Find(t => t.ParentId == departmentId).MaxAsync(t => t.SortId);
            return maxSortId.SafeValue() + 1;
        }

        /// <summary>
        /// 生成部门编码
        /// </summary>
        /// <param name="departmentId">部门标识</param>
        /// <returns></returns>
        public async Task<string> GenerateCodeAsync(Guid? departmentId)
        {
            var parentDepartment = departmentId.IsEmpty()
                ? await SingleAsync(t => t.ParentId.IsEmpty() && t.Level == 1)
                : await SingleAsync(t => t.Id == departmentId);
            var childCount = await Find(t => t.ParentId == parentDepartment.Id).CountAsync();
            return $"{parentDepartment.Code}{(childCount + 1).ToString().PadLeft(2, '0')}";
        }

        /// <summary>
        /// 生成部门编码
        /// </summary>
        /// <param name="newParentId"></param>
        /// <param name="departmentId"></param>
        /// <returns></returns>
        public async Task<string> GenerateNewCodeAsync(Guid? newParentId, Guid? departmentId)
        {
            var oldDepartment = await SingleAsync(t => t.Id == departmentId);
            if (oldDepartment?.ParentId == newParentId)
            {
                return oldDepartment?.Code;
            }
            return await GenerateCodeAsync(newParentId);
        }
    }
}