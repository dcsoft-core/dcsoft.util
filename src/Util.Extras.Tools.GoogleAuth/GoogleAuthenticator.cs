using System;

namespace Util.Extras.Tools.GoogleAuth
{
    /// <summary>
    /// 谷歌认证
    /// </summary>
    public class GoogleAuthenticator : IGoogleAuthenticator
    {
        /// <summary>
        /// 初始化
        /// </summary>
        public GoogleAuthenticator()
        {
            _twoFactorAuthenticator = new TwoFactorAuthenticator();
        }

        /// <summary>
        /// 双因素身份验证器
        /// </summary>
        private readonly TwoFactorAuthenticator _twoFactorAuthenticator;

        /// <inheritdoc />
        public string GenerateSecretCode()
        {
            return GenerateSecretCode(Util.Extras.Helpers.Random.GenerateRandom(16));
        }

        /// <inheritdoc />
        public string GenerateSecretCode(string secret)
        {
            return _twoFactorAuthenticator.EncodeAccountSecretKey(secret);
        }

        /// <inheritdoc />
        public AuthCode GenerateAuthCode(string accountTitleNoSpaces, string accountSecretKey)
        {
            return _twoFactorAuthenticator.GenerateAuthCode(accountTitleNoSpaces, accountSecretKey);
        }

        /// <inheritdoc />
        public bool ValidateTwoFactorPin(string accountSecretKey, string twoFactorCodeFromClient)
        {
            return ValidateTwoFactorPin(accountSecretKey, twoFactorCodeFromClient, TimeSpan.FromMinutes(1));
        }

        /// <inheritdoc />
        public bool ValidateTwoFactorPin(string accountSecretKey, string twoFactorCodeFromClient,
            TimeSpan timeTolerance)
        {
            return _twoFactorAuthenticator.ValidateTwoFactorPin(accountSecretKey, twoFactorCodeFromClient,
                timeTolerance);
        }
    }
}