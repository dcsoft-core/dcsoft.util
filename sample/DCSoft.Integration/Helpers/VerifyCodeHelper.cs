using DCSoft.Integration.Options.VerifyCode;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using Util.Dependency;
using Util.Extras.Extensions;
using Util.Helpers;
using Random = System.Random;

namespace DCSoft.Integration.Helpers
{
    /// <summary>
    /// 验证码帮助类
    /// </summary>
    public class VerifyCodeHelper : ISingletonDependency
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
            char[] character = { '2', '3', '4', '5', '6', '8', '9', 'a', 'b', 'd', 'e', 'f', 'h', 'k', 'm', 'n', 'r', 'x', 'y', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'J', 'K', 'L', 'M', 'N', 'P', 'R', 'S', 'T', 'W', 'X', 'Y' };
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
        public byte[] Draw(out string code, int length = 4)
        {
            const int codeW = 110;
            const int codeH = 36;
            const int fontSize = 22;

            //颜色列表，用于验证码、噪线、噪点 
            Color[] color = { Color.Black, Color.Red, Color.Blue, Color.Green, Color.Orange, Color.Brown, Color.Brown, Color.DarkBlue };
            //字体列表，用于验证码 

            var option = Config.Get<VerifyCodeOptions>("VerifyCode");
            var fonts = option.Fonts;

            code = GenerateRandom(length);

            //创建画布
            using (var bmp = new Bitmap(codeW, codeH))
            using (var g = Graphics.FromImage(bmp))
            using (var ms = new MemoryStream())
            {
                g.Clear(Color.White);
                var rnd = new Random();
                //画噪线 
                for (var i = 0; i < 1; i++)
                {
                    var x1 = rnd.Next(codeW);
                    var y1 = rnd.Next(codeH);
                    var x2 = rnd.Next(codeW);
                    var y2 = rnd.Next(codeH);
                    var clr = color[rnd.Next(color.Length)];
                    g.DrawLine(new Pen(clr), x1, y1, x2, y2);
                }

                //画验证码字符串 
                {
                    for (var i = 0; i < code.Length; i++)
                    {
                        var fnt = fonts[rnd.Next(fonts.Length)];
                        var ft = new Font(fnt, fontSize);
                        var clr = color[rnd.Next(color.Length)];
                        g.DrawString(code[i].ToString(), ft, new SolidBrush(clr), (float)i * 24 + 2, 0);
                    }
                }

                //将验证码图片写入内存流，并将其以 "image/Png" 格式输出
                bmp.Save(ms, ImageFormat.Png);
                return ms.ToArray();
            }
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
