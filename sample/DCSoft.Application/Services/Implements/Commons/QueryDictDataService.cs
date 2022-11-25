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
    /// 字典数据查询服务
    /// </summary>
    public class QueryDictDataService : TreeServiceBase<DictData, DictDataDto, DictDataQuery>, IQueryDictDataService
    {
        /// <summary>
        /// 初始化资源服务
        /// </summary>
        /// <param name="serviceProvider">服务提供器</param>
        /// <param name="unitOfWork">工作单元</param>
        /// <param name="dictDataRepository">仓储</param>
        public QueryDictDataService(IServiceProvider serviceProvider,
            IDataUnitOfWork unitOfWork,
            IDictDataRepository dictDataRepository) : base(serviceProvider, unitOfWork, dictDataRepository)
        {
            _dictDataRepository = dictDataRepository;
        }

        /// <summary>
        /// 仓储
        /// </summary>
        private readonly IDictDataRepository _dictDataRepository;

        /// <summary>
        /// 过滤
        /// </summary>
        protected override IQueryable<DictData> Filter(IQueryable<DictData> queryable, DictDataQuery query)
        {
            return base.Filter(queryable, query).Include(t => t.Parent)
                .WhereIfNotEmpty(t => t.Path.Contains(query.DictId.ToString()))
                .WhereIfNotEmpty(t => t.Level == query.Level)
                .WhereIfNotEmpty(t => t.Code.StartsWith(query.Code))
                .WhereIfNotEmpty(t => t.Name.Contains(query.Name));
        }

        /// <summary>
        /// 转成数据传输对象
        /// </summary>
        protected override DictDataDto ToDto(DictData po)
        {
            return po.ToDto();
        }

        /// <summary>
        /// 删除前操作
        /// </summary>
        /// <param name="entities"></param>
        protected override Task DeleteBeforeAsync(List<DictData> entities)
        {
            var exists = entities.Any(t => t.Level == 1);
            if (exists)
            {
                throw new Warning("一级字典不能删除");
            }
            return Task.CompletedTask;
        }
    }
}