using DCSoft.Applications.Dtos.Systems;
using DCSoft.Applications.Requests.Systems;
using DCSoft.Applications.Responses.Systems;
using DCSoft.Data.Queries.Systems;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Util.Aop;
using Util.Applications;
using DCSoft.Domain.Models.Systems;

namespace DCSoft.Applications.Services.Abstractions.Systems
{
    /// <summary>
    /// 用户服务
    /// </summary>
    public interface IUserService : ICrudService<UserDto, UserQuery>
    {
        /// <summary>
        /// 创建用户
        /// </summary>
        /// <param name="request">创建用户参数</param>
        Task<Guid> CreateAsync([NotNull] CreateUserRequest request);

        /// <summary>
        /// 修改用户
        /// </summary>
        /// <param name="request">修改用户参数</param>
        Task<Guid> UpdateAsync([NotNull] UpdateUserRequest request);

        /// <summary>
        /// 更新用户登录成功信息
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task UpdateSuccessLoginAsync(User user);

        /// <summary>
        /// 更新用户登录失败信息
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task UpdateFailLoginAsync(User user);

        /// <summary>
        /// 修改基本信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<Guid> UpdateBaseAsync([NotNull] UpdateUserBaseRequest request);

        /// <summary>
        /// 修改头像
        /// </summary>
        /// <param name="userId">用户标识</param>
        /// <param name="avatar">头像</param>
        /// <returns></returns>
        Task<bool> UpdateAvatarAsync(Guid userId, string avatar);

        /// <summary>
        /// 用户注册
        /// </summary>
        /// <param name="request">注册参数</param>
        /// <returns></returns>
        Task<Guid> RegisterAsync(UserRegisterRequest request);

        /// <summary>
        /// 启用/锁定
        /// </summary>
        /// <param name="userId">用户标识</param>
        /// <param name="isEnabled">启用/锁定</param>
        Task<Guid> EnableAsync(Guid userId, bool isEnabled);

        /// <summary>
        /// 管理员重置密码
        /// </summary>
        /// <returns></returns>
        Task<bool> ResetPasswordAsync(Guid userId);

        /// <summary>
        /// 用户修改密码
        /// </summary>
        /// <returns></returns>
        Task<bool> ChangePasswordAsync(Guid userId, string currentPassword, string newPassword);

        /// <summary>
        /// 获取操作权限
        /// </summary>
        Task<List<UserPermissionsResponse>> GetPermissionsAsync();
        /// <summary>
        /// 通过用户名查找
        /// </summary>
        /// <param name="userName">用户名</param>
        Task<User> FindByNameAsync(string userName);

        /// <summary>
        /// 通过电子邮件查找
        /// </summary>
        /// <param name="email">电子邮件</param>
        Task<User> FindByEmailAsync(string email);

        /// <summary>
        /// 通过手机号查找
        /// </summary>
        /// <param name="phoneNumber">手机号</param>
        Task<User> FindByPhoneAsync(string phoneNumber);
    }
}