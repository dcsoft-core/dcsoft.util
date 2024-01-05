using System;
using Util.Dependency;

namespace Util.Extras.Tools.GoogleAuth
{
    /// <summary>
    /// 谷歌认证
    /// </summary>
    public interface IGoogleAuthenticator : IScopeDependency
    {
        /// <summary>
        /// 生成安全密钥
        /// </summary>
        /// <returns></returns>
        string GenerateSecretCode();

        /// <summary>
        /// 生成安全密钥
        /// </summary>
        /// <param name="secret">密码</param>
        /// <returns></returns>
        string GenerateSecretCode(string secret);

        /// <summary>
        /// 生成安全认证信息
        /// </summary>
        /// <param name="accountTitleNoSpaces">帐号标题</param>
        /// <param name="accountSecretKey">帐号密钥</param>
        /// <returns></returns>
        AuthCode GenerateAuthCode(string accountTitleNoSpaces, string accountSecretKey);

        /// <summary>
        /// 验证双因子PIN
        /// </summary>
        /// <param name="accountSecretKey">帐号密钥</param>
        /// <param name="twoFactorCodeFromClient">双因子客户端值</param>
        /// <returns></returns>
        bool ValidateTwoFactorPin(string accountSecretKey, string twoFactorCodeFromClient);

        /// <summary>
        /// 验证双因子PIN
        /// </summary>
        /// <param name="accountSecretKey">帐号密钥</param>
        /// <param name="twoFactorCodeFromClient">双因子客户端值</param>
        /// <param name="timeTolerance">时间容差</param>
        /// <returns></returns>
        bool ValidateTwoFactorPin(string accountSecretKey, string twoFactorCodeFromClient, TimeSpan timeTolerance);
    }
}