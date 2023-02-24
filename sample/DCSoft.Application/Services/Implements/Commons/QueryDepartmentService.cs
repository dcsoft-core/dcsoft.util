using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DCSoft.Applications.Dtos.Commons;
using DCSoft.Applications.Extensions.Commons;
using DCSoft.Applications.Services.Abstractions.Commons;
using DCSoft.Data;
using DCSoft.Data.Queries.Commons;
using DCSoft.Domain.Models.Commons;
using DCSoft.Domain.Repositories.Commons;
using Microsoft.EntityFrameworkCore;
using Util.Applications.Trees;
using Util.Data;
using Util.Exceptions;

namespace DCSoft.Applications.Services.Implements.Commons
{
    /// <summary>
    /// 组织机构查询服务
    /// </summary>
    public class QueryDepartmentService : TreeServiceBase<Department, DepartmentDto, DepartmentQuery>,
        IQueryDepartmentService
    {
        /// <summary>
        /// 初始化资源服务
        /// </summary>
        /// <param name="serviceProvider">服务提供器</param>
        /// <param name="unitOfWork">工作单元</param>
        /// <param name="departmentRepository">部门仓储</param>
        public QueryDepartmentService(IServiceProvider serviceProvider,
            IDataUnitOfWork unitOfWork,
            IDepartmentRepository departmentRepository)
            : base(serviceProvider, unitOfWork, departmentRepository)
        {
            DepartmentRepository = departmentRepository;
        }

        /// <summary>
        /// 部门仓储
        /// </summary>
        public IDepartmentRepository DepartmentRepository { get; set; }

        /// <summary>
        /// 过滤
        /// </summary>
        protected override IQueryable<Department> Filter(IQueryable<Department> queryable,
            DepartmentQuery query)
        {
            return base.Filter(queryable, query).Include(t => t.Parent)
                .WhereIfNotEmpty(t => t.Code.StartsWith(query.Code))
                .WhereIfNotEmpty(t => t.Name.Contains(query.Name));
        }

        /// <summary>
        /// 转成数据传输对象
        /// </summary>
        protected override DepartmentDto ToDto(Department po)
        {
            return po.ToDepartmentDto();
        }

        /// <summary>
        /// 删除前操作
        /// </summary>
        /// <param name="entities"></param>
        protected override Task DeleteBeforeAsync(List<Department> entities)
        {
            var exists = entities.Any(t => t.Level == 1);
            if (exists)
            {
                throw new Warning("一级部门不能删除");
            }

            return Task.CompletedTask;
        }
    }
}