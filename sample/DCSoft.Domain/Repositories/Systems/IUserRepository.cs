using DCSoft.Domain.Models;
using DCSoft.Domain.Models.Systems;
using System.Linq;
using System.Threading.Tasks;
using System;
using Util.Domain.Repositories;

namespace DCSoft.Domain.Repositories.Systems
{
    /// <summary>
    /// 用户仓储
    /// </summary>
    public interface IUserRepository : IRepository<User>
    {
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="user">用户</param>
        /// <param name="currentPassword">当前密码</param>
        /// <param name="newPassword">新密码</param>
        Task ChangePasswordAsync(User user, string currentPassword, string newPassword);

        /// <summary>
        /// 管理员重置密码
        /// </summary>
        /// <param name="user">用户</param>
        /// <param name="newPassword">新密码</param>
        Task ResetPasswordAsync(User user, string newPassword);

        /// <summary>
        /// 过滤角色
        /// </summary>
        /// <param name="queryable">查询对象</param>
        /// <param name="roleId">角色标识</param>
        /// <param name="except">是否排除该角色的用户列表</param>
        IQueryable<User> FilterByRole(IQueryable<User> queryable, Guid roleId, bool except = false);
    }
}