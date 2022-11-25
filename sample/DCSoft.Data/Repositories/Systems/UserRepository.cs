using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DCSoft.Domain.Models.Systems;
using DCSoft.Domain.Repositories.Systems;
using Util;
using Util.Data.EntityFrameworkCore;
using Util.Exceptions;
using Util.Helpers;

namespace DCSoft.Data.Repositories.Systems
{
    /// <summary>
    /// 用户仓储
    /// </summary>
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        /// <summary>
        /// 初始化用户仓储
        /// </summary>
        /// <param name="unitOfWork">工作单元</param>
        public UserRepository(IDataUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        /// <summary>
        /// 根据标识查找
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <param name="cancellationToken">取消令牌</param>
        public Task<User> FindByIdAsync(string userId, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            return FindByIdAsync(userId.ToGuid(), cancellationToken);
        }

        /// <summary>
        /// 通过电子邮件查找
        /// </summary>
        /// <param name="normalizedEmail">标准化电子邮件</param>
        public async Task<User> FindByEmailAsync(string normalizedEmail)
        {
            return await SingleAsync(u => u.NormalizedEmail == normalizedEmail);
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="user"></param>
        /// <param name="currentPassword"></param>
        /// <param name="newPassword"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task ChangePasswordAsync(User user, string currentPassword, string newPassword)
        {
            var oldPassword = Encrypt.Base64Encrypt(Encrypt.HmacSha256(Encrypt.AesEncrypt(currentPassword), user.SecurityStamp));
            if (!oldPassword.Equals(user.PasswordHash))
            {
                throw new Warning("旧密码不对");
            }
            user.SetPasswordHash(newPassword);
            user.SetPassword(newPassword, true);
            await UpdateAsync(user);
        }

        /// <summary>
        /// 重置密码
        /// </summary>
        /// <param name="user"></param>
        /// <param name="newPassword"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task ResetPasswordAsync(User user, string newPassword)
        {
            user.SetPasswordHash(newPassword);
            user.SetPassword(newPassword, true);
            await UpdateAsync(user);
        }

        /// <summary>
        /// 过滤角色
        /// </summary>
        /// <param name="queryable">查询对象</param>
        /// <param name="roleId">角色标识</param>
        /// <param name="except">是否排除该角色的用户列表</param>
        public IQueryable<User> FilterByRole(IQueryable<User> queryable, Guid roleId, bool except = false)
        {
            if (roleId.IsEmpty())
                return queryable;
            var selectedUsers = from user in queryable
                                join userRole in UnitOfWork.Set<UserRole>() on user.Id equals userRole.UserId
                                where userRole.RoleId == roleId
                                select user;
            if (except)
                return queryable.Except(selectedUsers);
            return selectedUsers;
        }
    }
}