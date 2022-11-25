using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Util.Applications.Dtos;
using Util.Data;
using Util.Data.Queries;
using Util.Domain.Entities;
using Util.Domain.Repositories;
using Util.Helpers;

// ReSharper disable once CheckNamespace
namespace Util.Applications
{
    /// <summary>
    /// 增删改查服务
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <typeparam name="TDto">数据传输对象类型</typeparam>
    /// <typeparam name="TQuery">查询参数类型</typeparam>
    /// <typeparam name="TKey">实体标识类型</typeparam>
    public abstract class DeleteServiceBase<TEntity, TDto, TQuery, TKey>
        : QueryServiceBase<TEntity, TDto, TQuery, TKey>, IDeleteService<TDto, TQuery>
        where TEntity : class, IAggregateRoot<TEntity, TKey>, new()
        where TDto : class, IDto, new()
        where TQuery : IPage
    {

        #region 字段

        /// <summary>
        /// 仓储
        /// </summary>
        private readonly IRepository<TEntity, TKey> _repository;

        #endregion

        #region 构造方法

        /// <summary>
        /// 初始化增删改查服务
        /// </summary>
        /// <param name="serviceProvider">服务提供器</param>
        /// <param name="unitOfWork">工作单元</param>
        /// <param name="repository">仓储</param>
        protected DeleteServiceBase(IServiceProvider serviceProvider, IUnitOfWork unitOfWork, IRepository<TEntity, TKey> repository) : base(serviceProvider, repository)
        {
            UnitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            EntityDescription = Reflection.GetDisplayNameOrDescription<TEntity>();
        }

        #endregion

        #region 属性

        /// <summary>
        /// 工作单元
        /// </summary>
        protected IUnitOfWork UnitOfWork { get; }

        /// <summary>
        /// 实体描述
        /// </summary>
        protected string EntityDescription { get; }

        #endregion

        #region 提交工作单元

        /// <summary>
        /// 提交工作单元
        /// </summary>
        protected virtual async Task CommitAsync()
        {
            await UnitOfWork.CommitAsync();
        }

        #endregion

        #region DeleteAsync(删除)

        /// <inheritdoc />
        public virtual async Task DeleteAsync(string ids)
        {
            if (ids.IsEmpty())
                return;
            var entities = await _repository.FindByIdsAsync(ids);
            if (entities?.Count == 0)
                return;
            await DeleteBeforeAsync(entities);
            await _repository.RemoveAsync(entities);
            await DeleteAfterAsync(entities);
            await CommitAsync();
            await DeleteCommitAfterAsync(entities);
        }

        /// <summary>
        /// 删除前操作
        /// </summary>
        /// <param name="entities">实体列表</param>
        protected virtual Task DeleteBeforeAsync(List<TEntity> entities)
        {
            return Task.CompletedTask;
        }

        /// <summary>
        /// 删除后操作
        /// </summary>
        /// <param name="entities">实体集合</param>
        protected virtual Task DeleteAfterAsync(List<TEntity> entities)
        {
            return Task.CompletedTask;
        }

        /// <summary>
        /// 删除提交后操作
        /// </summary>
        /// <param name="entities">实体集合</param>
        protected virtual Task DeleteCommitAfterAsync(List<TEntity> entities)
        {
            return Task.CompletedTask;
        }

        #endregion
    }
}
