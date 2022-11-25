using System;
using Util.Helpers;

namespace Util.Domain.Auditing {
    /// <summary>
    /// 创建操作审计设置器
    /// </summary>
    public class CreationAuditedSetter {
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
        /// 初始化创建操作审计设置器
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="userId">用户标识</param>
        /// <param name="userName">用户名称</param>
        private CreationAuditedSetter( object entity, string userId, string userName ) {
            _entity = entity;
            _userId = userId;
            _userName = userName;
        }

        /// <summary>
        /// 设置创建审计属性
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="userId">用户标识</param>
        /// <param name="userName">用户名称</param>
        public static void Set( object entity, string userId, string userName ) {
            new CreationAuditedSetter( entity, userId, userName ).Init();
        }

        /// <summary>
        /// 初始化
        /// </summary>
        public void Init() {
            if ( _entity == null )
                return;
            if( _entity is ICreationAudited<Guid> entity ) {
                entity.CreationTime = Time.Now;
                entity.CreatorId = _userId.ToGuid();
                entity.Creator = _userName.SafeString();
                return;
            }
            if( _entity is ICreationAudited<Guid?> entity2 ) {
                entity2.CreationTime = Time.Now;
                entity2.CreatorId = _userId.ToGuidOrNull();
                entity2.Creator = _userName.SafeString();
                return;
            }
            if( _entity is ICreationAudited<int> entity3 ) {
                entity3.CreationTime = Time.Now;
                entity3.CreatorId = _userId.ToInt();
                entity3.Creator = _userName.SafeString();
                return;
            }
            if( _entity is ICreationAudited<int?> entity4 ) {
                entity4.CreationTime = Time.Now;
                entity4.CreatorId = _userId.ToIntOrNull();
                entity4.Creator = _userName.SafeString();
                return;
            }
            if( _entity is ICreationAudited<string> entity5 ) {
                entity5.CreationTime = Time.Now;
                entity5.CreatorId = _userId.SafeString();
                entity5.Creator = _userName.SafeString();
                return;
            }
            if( _entity is ICreationAudited<long> entity6 ) {
                entity6.CreationTime = Time.Now;
                entity6.CreatorId = _userId.ToLong();
                entity6.Creator = _userName.SafeString();
                return;
            }
            if( _entity is ICreationAudited<long?> entity7 ) {
                entity7.CreationTime = Time.Now;
                entity7.CreatorId = _userId.ToLongOrNull();
                entity7.Creator = _userName.SafeString();
                return;
            }
        }
    }
}
