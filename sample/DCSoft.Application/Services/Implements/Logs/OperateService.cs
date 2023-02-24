using DCSoft.Applications.Dtos.Logs;
using DCSoft.Applications.Services.Abstractions.Logs;
using DCSoft.Data;
using DCSoft.Data.Queries.Logs;
using DCSoft.Domain.Repositories.Logs;
using System;
using System.Linq;
using DCSoft.Domain.Models.Logs;
using Util.Applications;

namespace DCSoft.Applications.Services.Implements.Logs
{
    /// <summary>
    /// 操作日志服务
    /// </summary>
    public class OperateService : CrudServiceBase<Operate, OperateDto, OperateQuery>, IOperateService
    {
        /// <summary>
        /// 初始化操作日志服务
        /// </summary>
        /// <param name="serviceProvider">服务提供器</param>
        /// <param name="unitOfWork">工作单元</param>
        /// <param name="repository">仓储</param>
        public OperateService(IServiceProvider serviceProvider, IDataUnitOfWork unitOfWork,
            IOperateRepository repository) : base(serviceProvider, unitOfWork, repository)
        {
        }

        /// <inheritdoc />
        protected override IQueryable<Operate> Filter(IQueryable<Operate> queryable, OperateQuery param)
        {
            return queryable;
        }
    }
}