using DCSoft.Applications.Dtos.Commons;
using DCSoft.Applications.Services.Abstractions.Commons;
using DCSoft.Data;
using DCSoft.Domain.Repositories.Commons;
using System;
using Util.Applications;
using DCSoft.Applications.Requests.Commons;
using System.Threading.Tasks;
using DCSoft.Applications.Extensions.Commons;
using Util.Domain;
using Util;

namespace DCSoft.Applications.Services.Implements.Commons
{
    /// <summary>
    /// 组织机构服务
    /// </summary>
    public class DepartmentService : ServiceBase, IDepartmentService
    {
        /// <summary>
        /// 初始化组织机构服务
        /// </summary>
        /// <param name="serviceProvider">服务提供器</param>
        /// <param name="unitOfWork">工作单元</param>
        /// <param name="repository">仓储</param>
        public DepartmentService(IServiceProvider serviceProvider, IDataUnitOfWork unitOfWork, IDepartmentRepository repository) : base(serviceProvider)
        {
            _departmentRepository = repository;
        }

        /// <summary>
        /// 部门仓储
        /// </summary>
        private readonly IDepartmentRepository _departmentRepository;

        /// <summary>
        /// 创建部门
        /// </summary>
        /// <param name="request">创建部门参数</param>
        public async Task<Guid> CreateAsync(CreateDepartmentRequest request)
        {
            var dept = request.ToDepartment();
            dept.CheckNull(nameof(dept));
            dept.Init();
            var parent = await _departmentRepository.FindByIdAsync(dept.ParentId);
            dept.InitPath(parent);
            dept.SortId = await _departmentRepository.GenerateSortIdAsync(dept.ParentId);
            dept.Code = await _departmentRepository.GenerateCodeAsync(dept.ParentId);
            await _departmentRepository.AddAsync(dept);
            return dept.Id;
        }


        /// <summary>
        /// 修改部门
        /// </summary>
        /// <param name="request">部门参数</param>
        public async Task UpdateAsync(DepartmentDto request)
        {
            var dept = await _departmentRepository.FindByIdAsync(request.Id.ToGuid());
            request.MapTo(dept);
            dept.InitPinYin();
            dept.Code = await _departmentRepository.GenerateNewCodeAsync(dept.ParentId, dept.Id);
            await _departmentRepository.UpdatePathAsync(dept);
            await _departmentRepository.UpdateAsync(dept);
        }
    }
}