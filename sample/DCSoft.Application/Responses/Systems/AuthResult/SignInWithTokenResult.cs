namespace DCSoft.Applications.Responses.Systems.AuthResult
{
    /// <summary>
    /// 登录结果
    /// </summary>
    public class SignInWithTokenResult : SignInResult
    {
        /// <summary>
        /// 令牌
        /// </summary>
        public JsonWebToken Token { get; set; }
    }
}