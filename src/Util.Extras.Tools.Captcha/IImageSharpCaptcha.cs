using Util.Dependency;

namespace Util.Extras.Tools.Captcha
{
    /// <summary>
    /// 验证码配置和绘制逻辑
    /// </summary>
    public interface IImageSharpCaptcha
    {
        /// <summary>
        /// 生成随机中文字符串
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        string GetRandomCnText(int length);

        /// <summary>
        /// 生成随机英文字母/数字组合字符串
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        string GetRandomEnDigitalText(int length);

        /// <summary>
        /// 获取泡泡样式验证码
        /// </summary>
        /// <param name="text">2-3个文字，中文效果较好</param>
        /// <returns>验证码图片字节数组</returns>
        byte[] GetBubbleCodeByte(string text);

        /// <summary>
        /// 获取动态(gif)泡泡验证码
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        byte[] GetGifBubbleCodeByte(string text);

        /// <summary>
        /// 英文字母+数字组合验证码
        /// </summary>
        /// <param name="text"></param>
        /// <returns>验证码图片字节数组</returns>
        byte[] GetEnDigitalCodeByte(string text);

        /// <summary>
        /// 动态(gif)数字字母组合验证码
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        byte[] GetGifEnDigitalCodeByte(string text);
    }
}