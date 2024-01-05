namespace Util.Extras.Tools.GoogleAuth
{
    /// <summary>
    /// 认证编码
    /// </summary>
    public class AuthCode
    {
        /// <summary>
        /// 帐户
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// 帐户密钥
        /// </summary>
        public string AccountSecretKey { get; set; }

        /// <summary>
        /// 手动输入
        /// </summary>
        public string ManualEntryKey { get; set; }

        /// <summary>
        /// 二维码
        /// </summary>
        public string QrCodeImage { get; set; }
    }
}