using System;
using System.Collections.Generic;
using System.Linq;
using SixLabors.Fonts;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

// ReSharper disable IdentifierTypo

namespace Util.Extras.Tools.Captcha
{
    /// <summary>
    /// ImageSharp扩展类
    /// </summary>
    public static class ImageSharpExtension
    {
        /// <summary>
        /// 绘制中文字符（可以绘制字母数字，但样式可能需要改）
        /// </summary>
        /// <param name="processingContext"></param>
        /// <param name="containerWidth"></param>
        /// <param name="containerHeight"></param>
        /// <param name="text"></param>
        /// <param name="color"></param>
        /// <param name="font"></param>
        /// <returns></returns>
        public static IImageProcessingContext DrawingCnText(this IImageProcessingContext processingContext,
            int containerWidth, int containerHeight, string text, Rgba32 color, Font font)
        {
            if (string.IsNullOrEmpty(text)) return processingContext;
            var random = new Random();
            var textWidth = (containerWidth / text.Length);
            var img2Size = Math.Min(textWidth, containerHeight);
            var fontMiniSize = (int)(img2Size * 0.6);
            var fontMaxSize = (int)(img2Size * 0.95);

            for (var i = 0; i < text.Length; i++)
            {
                using var img = new Image<Rgba32>(img2Size, img2Size);
                var scaledFont = new Font(font, random.Next(fontMiniSize, fontMaxSize));
                var point = new Point(i * textWidth, (containerHeight - img.Height) / 2);
                var textGraphicsOptions = new DrawingOptions
                {
                };

                img.Mutate(ctx => ctx
                    .DrawText(textGraphicsOptions, text[i].ToString(), scaledFont, color, new Point(0, 0))
                    .Rotate(random.Next(-45, 45))
                );
                processingContext.DrawImage(img, point, 1);
            }

            return processingContext;
        }

        /// <summary>
        /// 绘制英文字符
        /// </summary>
        /// <param name="processingContext"></param>
        /// <param name="containerWidth"></param>
        /// <param name="containerHeight"></param>
        /// <param name="text"></param>
        /// <param name="colorHexArr"></param>
        /// <param name="fonts"></param>
        /// <returns></returns>
        public static IImageProcessingContext DrawingEnText(this IImageProcessingContext processingContext,
            int containerWidth, int containerHeight, string text, string[] colorHexArr, Font[] fonts)
        {
            if (string.IsNullOrEmpty(text)) return processingContext;

            var random = new Random();
            var textWidth = (containerWidth / text.Length);
            var img2Size = Math.Min(textWidth, containerHeight);
            var fontMiniSize = (int)(img2Size * 0.9);
            var fontMaxSize = (int)(img2Size * 1.37);
            var fontStyleArr = Enum.GetValues(typeof(FontStyle));

            for (var i = 0; i < text.Length; i++)
            {
                using var img = new Image<Rgba32>(img2Size, img2Size);
                var scaledFont = new Font(fonts[random.Next(0, fonts.Length)],
                    random.Next(fontMiniSize, fontMaxSize),
                    (FontStyle)fontStyleArr.GetValue(random.Next(fontStyleArr.Length))!);
                var point = new Point(i * textWidth, (containerHeight - img.Height) / 2);
                var colorHex = colorHexArr[random.Next(0, colorHexArr.Length)];
                var textGraphicsOptions = new DrawingOptions()
                {
                };

                img.Mutate(ctx => ctx
                    .DrawText(textGraphicsOptions, text[i].ToString(), scaledFont, Rgba32.ParseHex(colorHex),
                        new Point(0, 0))
                    .DrawingGrid(containerWidth, containerHeight, Rgba32.ParseHex(colorHex), 6, 1)
                    .Rotate(random.Next(-45, 45))
                );
                processingContext.DrawImage(img, point, 1);
            }

            return processingContext;
        }

        /// <summary>
        /// 绘制圆圈（泡泡）
        /// </summary>
        /// <param name="processingContext"></param>
        /// <param name="containerWidth"></param>
        /// <param name="containerHeight"></param>
        /// <param name="count"></param>
        /// <param name="miniR"></param>
        /// <param name="maxR"></param>
        /// <param name="color"></param>
        /// <param name="canOverlap"></param>
        /// <returns></returns>
        public static IImageProcessingContext DrawingCircles(this IImageProcessingContext processingContext,
            int containerWidth, int containerHeight, int count, int miniR, int maxR, Rgba32 color,
            bool canOverlap = false)
        {
            EllipsePolygon ep;
            var random = new Random();
            var points = new List<PointF>();

            if (count <= 0) return processingContext;
            using var img = new Image<Rgba32>(containerWidth, containerHeight);
            for (var i = 0; i < count; i++)
            {
                PointF tempPoint;
                tempPoint = canOverlap
                    ? new PointF(random.Next(0, containerWidth), random.Next(0, containerHeight))
                    : GetCirclePointF(containerWidth, containerHeight, (miniR + maxR), ref points);

                ep = new EllipsePolygon(tempPoint, random.Next(miniR, maxR));

                img.Mutate(ctx => ctx
                    .Draw(color, (float)(random.Next(94, 145) / 100.0), ep.Clip())
                );
            }

            processingContext.DrawImage(img, new Point(0,0), 1);
            return processingContext;
        }

        /// <summary>
        /// 绘制杂线
        /// </summary>
        /// <param name="processingContext"></param>
        /// <param name="containerWidth"></param>
        /// <param name="containerHeight"></param>
        /// <param name="color"></param>
        /// <param name="count"></param>
        /// <param name="thickness"></param>
        /// <returns></returns>
        public static IImageProcessingContext DrawingGrid(this IImageProcessingContext processingContext,
            int containerWidth, int containerHeight, Rgba32 color, int count, float thickness)
        {
            var points = new List<PointF> { new(0, 0) };
            for (var i = 0; i < count; i++)
            {
                GetCirclePointF(containerWidth, containerHeight, 9, ref points);
            }

            using var img = new Image<Rgba32>(containerWidth, containerHeight);
            
            points.Add(new PointF(containerWidth, containerHeight));
            img.Mutate(ctx => ctx
                .DrawLines(color, thickness, points.ToArray())
            );
            processingContext.DrawImage(img, new Point(0,0), 1);

            return processingContext;
        }

        /// <summary>
        /// 散 随机点
        /// </summary>
        /// <param name="containerWidth"></param>
        /// <param name="containerHeight"></param>
        /// <param name="lapR"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        private static PointF GetCirclePointF(int containerWidth, int containerHeight, double lapR,
            ref List<PointF> list)
        {
            var random = new Random();
            var newPoint = new PointF();
            var retryTimes = 10;

            do
            {
                newPoint.X = random.Next(0, containerWidth);
                newPoint.Y = random.Next(0, containerHeight);
                var tooClose = list
                    .Select(p => Math.Sqrt(Math.Pow((p.X - newPoint.X), 2) + Math.Pow((p.Y - newPoint.Y), 2)))
                    .Any(tempDistance => tempDistance < lapR);

                if (tooClose) continue;
                list.Add(newPoint);
                break;
            } while (retryTimes-- > 0);

            if (retryTimes <= 0)
            {
                list.Add(newPoint);
            }

            return newPoint;
        }
    }
}