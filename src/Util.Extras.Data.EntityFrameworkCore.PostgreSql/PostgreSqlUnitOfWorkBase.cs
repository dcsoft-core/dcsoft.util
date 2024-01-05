using System;
using Microsoft.EntityFrameworkCore;
using Util.Data.EntityFrameworkCore;
using Util.Extras.Domain.Auditing;
using Util.Extras.Sessions;

namespace Util.Extras.Data.EntityFrameworkCore
{
    /// <summary>
    /// PostgreSql工作单元基类
    /// </summary>
    public abstract class PostgreSqlUnitOfWorkBase : UnitOfWorkBase
    {
        /// <summary>
        /// 初始化SqlServer工作单元
        /// </summary>
        /// <param name="serviceProvider">服务提供器</param>
        /// <param name="options">配置</param>
        protected PostgreSqlUnitOfWorkBase(IServiceProvider serviceProvider, DbContextOptions options)
            : base(serviceProvider, options)
        {
        }

        /// <summary>
        /// 获取用户名称
        /// </summary>
        protected virtual string GetUserName()
        {
            return Session.GetUserName();
        }

        #region SetCreationAudited(设置创建审计信息)

        /// <summary>
        /// 设置创建审计信息
        /// </summary>
        protected override void SetCreationAudited(object entity)
        {
            CreationAuditedSetter.Set(entity, GetUserId(), GetUserName());
        }

        #endregion

        #region SetModificationAudited(设置修改审计信息)

        /// <summary>
        /// 设置修改审计信息
        /// </summary>
        protected override void SetModificationAudited(object entity)
        {
            ModificationAuditedSetter.Set(entity, GetUserId(), GetUserName());
        }

        #endregion
    }
}