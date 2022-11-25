namespace DCSoft.Integration.Options.Token
{
    /// <summary>
    /// 刷新令牌设置
    /// </summary>
    public sealed class RefreshTokenSettingOptions
    {
        /// <summary>
        /// 令牌过期时间（分钟）
        /// </summary>
        public int ExpiredTime { get; set; } = 43200;
    }
}