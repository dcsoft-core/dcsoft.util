using System;
using Util.Helpers;

namespace Util.Extras.Domain.Auditing
{
    /// <summary>
    /// 修改操作审计设置器
    /// </summary>
    public class ModificationAuditedSetter
    {
        /// <summary>
        /// 实体
        /// </summary>
        private readonly object _entity;

        /// <summary>
        /// 用户标识
        /// </summary>
        private readonly string _userId;

        /// <summary>
        /// 用户名称
        /// </summary>
        private readonly string _userName;

        /// <summary>
        /// 初始化修改操作审计设置器
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="userId">用户标识</param>
        /// <param name="userName">用户名称</param>
        private ModificationAuditedSetter(object entity, string userId, string userName)
        {
            _entity = entity;
            _userId = userId;
            _userName = userName;
        }

        /// <summary>
        /// 设置修改审计属性
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="userId">用户标识</param>
        /// <param name="userName">用户名称</param>
        public static void Set(object entity, string userId, string userName)
        {
            new ModificationAuditedSetter(entity, userId, userName).Init();
        }

        /// <summary>
        /// 初始化
        /// </summary>
        public void Init()
        {
            if (_entity == null)
                return;
            if (_entity is IModificationAudited<Guid> entity)
            {
                entity.LastModificationTime = Time.Now;
                entity.LastModifierId = _userId.ToGuid();
                entity.LastModifier = _userName.SafeString();
                return;
            }

            if (_entity is IModificationAudited<Guid?> entity2)
            {
                entity2.LastModificationTime = Time.Now;
                entity2.LastModifierId = _userId.ToGuidOrNull();
                entity2.LastModifier = _userName.SafeString();
                return;
            }

            if (_entity is IModificationAudited<int> entity3)
            {
                entity3.LastModificationTime = Time.Now;
                entity3.LastModifierId = _userId.ToInt();
                entity3.LastModifier = _userName.SafeString();
                return;
            }

            if (_entity is IModificationAudited<int?> entity4)
            {
                entity4.LastModificationTime = Time.Now;
                entity4.LastModifierId = _userId.ToIntOrNull();
                entity4.LastModifier = _userName.SafeString();
                return;
            }

            if (_entity is IModificationAudited<string> entity5)
            {
                entity5.LastModificationTime = Time.Now;
                entity5.LastModifierId = _userId.SafeString();
                entity5.LastModifier = _userName.SafeString();
                return;
            }

            if (_entity is IModificationAudited<long> entity6)
            {
                entity6.LastModificationTime = Time.Now;
                entity6.LastModifierId = _userId.ToLong();
                entity6.LastModifier = _userName.SafeString();
                return;
            }

            if (_entity is IModificationAudited<long?> entity7)
            {
                entity7.LastModificationTime = Time.Now;
                entity7.LastModifierId = _userId.ToLongOrNull();
                entity7.LastModifier = _userName.SafeString();
            }
        }
    }
}