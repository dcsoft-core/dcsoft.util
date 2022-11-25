using System;
using System.Threading.Tasks;
using DCSoft.Applications.Dtos.Commons;
using DCSoft.Applications.Requests.Commons;
using Util.Aop;
using Util.Applications;

namespace DCSoft.Applications.Services.Abstractions.Commons
{
    /// <summary>
    /// 组织机构服务
    /// </summary>
    public interface IDepartmentService : IService
    {
        /// <summary>
        /// 创建部门
        /// </summary>
        /// <param name="request">创建部门参数</param>
        Task<Guid> CreateAsync([NotNull] CreateDepartmentRequest request);

        /// <summary>
        /// 修改部门
        /// </summary>
        /// <param name="request">部门参数</param>
        Task UpdateAsync([NotNull] DepartmentDto request);
    }
}