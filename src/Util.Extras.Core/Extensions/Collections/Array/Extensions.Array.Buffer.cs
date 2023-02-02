using System;

// ReSharper disable once CheckNamespace
namespace Util.Extras.Extensions
{
    /// <summary>
    /// 数组(<see cref="Array"/>) 扩展
    /// </summary>
    public static partial class ArrayExtensions
    {
        /// <summary>
        /// 块复制
        /// </summary>
        /// <param name="src">源数组</param>
        /// <param name="srcOffset">源数组偏移量</param>
        /// <param name="dst">目标数组</param>
        /// <param name="dstOffset">目标数组偏移量</param>
        /// <param name="count">计数</param>
        public static void BlockCopy(this System.Array src, int srcOffset, System.Array dst, int dstOffset, int count) =>
            Buffer.BlockCopy(src, srcOffset, dst, dstOffset, count);

        /// <summary>
        /// 获取字节长度
        /// </summary>
        /// <param name="array">数组</param>
        public static int ByteLength(this System.Array array) => Buffer.ByteLength(array);

        /// <summary>
        /// 获取指定索引的字节
        /// </summary>
        /// <param name="array">数组</param>
        /// <param name="index">索引</param>
        public static byte GetByte(this System.Array array, int index) => Buffer.GetByte(array, index);

        /// <summary>
        /// 设置字节
        /// </summary>
        /// <param name="array">数组</param>
        /// <param name="index">索引</param>
        /// <param name="value">值</param>
        public static void SetByte(this System.Array array, int index, byte value) => Buffer.SetByte(array, index, value);
    }
}