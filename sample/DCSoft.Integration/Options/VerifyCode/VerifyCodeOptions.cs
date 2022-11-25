namespace DCSoft.Integration.Options.VerifyCode
{
    /// <summary>
    /// 验证码设置
    /// </summary>
    public sealed class VerifyCodeOptions
    {
        /// <summary>
        /// 启用
        /// </summary>
        public bool Enable { get; set; } = true;

        /// <summary>
        /// 字体
        /// </summary>
        public string[] Fonts { get; set; } = { "Times New Roman", "Verdana", "Arial", "Gungsuh", "Impact" };
    }
}