using System.Threading.Tasks;
using DCSoft.Applications.Requests.Systems;
using DCSoft.Applications.Responses.Systems.AuthResult;
using Util.Aop;
using Util.Applications;

namespace DCSoft.Applications.Services.Abstractions.Systems
{
    /// <summary>
    /// 安全服务
    /// </summary>
    public interface ISecurityService : IService
    {
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="request">登录参数</param>
        Task<JsonWebToken> SignInAsync([NotNull] UserLoginRequest request);

        /// <summary>
        /// 刷新令牌
        /// </summary>
        Task<JsonWebToken> RefreshTokenAsync();

        /// <summary>
        /// 获取验证码
        /// </summary>
        /// <param name="lastKey"></param>
        /// <returns></returns>
        Task<AuthVerifyCodeResp> GetVerifyCodeAsync(string lastKey);

        /// <summary>
        /// 获取密码密钥
        /// </summary>
        /// <returns></returns>
        Task<AuthEncryptKeyResp> GetEncryptKeyAsync();

        /// <summary>
        /// 退出登录
        /// </summary>
        Task SignOutAsync();
    }
}