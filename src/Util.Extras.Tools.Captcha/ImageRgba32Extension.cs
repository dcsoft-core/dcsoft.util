using System.IO;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Gif;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.PixelFormats;

namespace Util.Extras.Tools.Captcha
{
    /// <summary>
    /// ImageRgba32扩展类
    /// </summary>
    public static class ImageRgba32Extension
    {
        /// <summary>
        /// 转换PNG图自主
        /// </summary>
        /// <param name="img"></param>
        /// <typeparam name="TPixel"></typeparam>
        /// <returns></returns>
        public static byte[] ToPngArray<TPixel>(this Image<TPixel> img) where TPixel : unmanaged, IPixel<TPixel>
        {
            using var ms = new MemoryStream();
            img.Save(ms, PngFormat.Instance);
            return ms.ToArray();
        }

        /// <summary>
        /// 转换GIF图片
        /// </summary>
        /// <param name="img"></param>
        /// <typeparam name="TPixel"></typeparam>
        /// <returns></returns>
        public static byte[] ToGifArray<TPixel>(this Image<TPixel> img) where TPixel : unmanaged, IPixel<TPixel>
        {
            using var ms = new MemoryStream();
            img.Save(ms, new GifEncoder());
            return ms.ToArray();
        }
    }
}