using System.IO;
using System.Text;
using SixLabors.Fonts;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using Util.Extras.Extensions;
using Util.Helpers;
using Random = System.Random;

namespace Util.Extras.Tools.Captcha
{
    /// <summary>
    /// 验证码帮助类
    /// </summary>
    public class VerifyCodeHelper
    {
        /// <summary>
        /// 初始化
        /// </summary>
        public VerifyCodeHelper()
        {
        }

        /// <summary>
        /// 生成随机数
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        private string GenerateRandom(int length)
        {
            var chars = new StringBuilder();
            //验证码的字符集，去掉了一些容易混淆的字符 
            char[] character =
            {
                '2', '3', '4', '5', '6', '8', '9', 'a', 'b', 'd', 'e', 'f', 'h', 'k', 'm', 'n', 'r', 'x', 'y', 'A', 'B',
                'C', 'D', 'E', 'F', 'G', 'H', 'J', 'K', 'L', 'M', 'N', 'P', 'R', 'S', 'T', 'W', 'X', 'Y'
            };
            var rnd = new Random();
            //生成验证码字符串 
            for (var i = 0; i < length; i++)
            {
                chars.Append(character[rnd.Next(character.Length)]);
            }

            return chars.ToString();
        }

        /// <summary>
        /// 画图
        /// </summary>
        /// <param name="code"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        private byte[] Draw(out string code, int length = 4)
        {
            const int codeW = 110;
            const int codeH = 36;
            const int fontSize = 22;

            //颜色列表，用于验证码、噪线、噪点 
            Color[] color =
            {
                Color.Black, Color.Red, Color.Blue, Color.Green, Color.Orange, Color.Brown, Color.Brown, Color.DarkBlue
            };
            //字体列表，用于验证码 

            var option = Config.Get<VerifyCodeOptions>("VerifyCode");
            var fonts = option.Fonts;
            var collection = new FontCollection();
            collection.AddSystemFonts();

            code = GenerateRandom(length);

            //创建画布
            using var img = new Image<Rgba32>(codeW, codeH);
            using var ms = new MemoryStream();
            var rnd = new Random();
            //画噪线 
            for (var i = 0; i < 1; i++)
            {
                var x1 = new PointF(rnd.Next(codeW), rnd.Next(codeH));
                var y1 = new PointF(rnd.Next(codeW), rnd.Next(codeH));
                var x2 = new PointF(rnd.Next(codeW), rnd.Next(codeH));
                var y2 = new PointF(rnd.Next(codeW), rnd.Next(codeH));
                var clr = color[rnd.Next(color.Length)];
                IPen pen = Pens.DashDot(clr, 1);
                img.Mutate(o => o.DrawLines(pen, x1, y1, x2, y2));
            }

            //画验证码字符串 
            {
                for (var i = 0; i < code.Length; i++)
                {
                    var fnt = fonts[rnd.Next(fonts.Length)];
                    var family = collection.Get(fnt);
                    var ft = new Font(family, fontSize);
                    var clr = color[rnd.Next(color.Length)];
                    var text = code;
                    img.Mutate(ctx => ctx.DrawText(text[i].ToString(), ft, clr, new PointF((float)i * 24 + 2, 0)));
                }
            }

            //将验证码图片写入内存流，并将其以 "image/Png" 格式输出
            img.Save(ms, PngFormat.Instance);
            return ms.ToArray();
        }

        /// <summary>
        /// 获取Base64图片
        /// </summary>
        /// <param name="code"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public string GetBase64String(out string code, int length = 4)
        {
            return Draw(out code, length).ToBase64();
        }
    }
}