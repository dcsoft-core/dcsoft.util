namespace Util.Extras.Helpers
{
    /// <summary>
    /// 类型转换
    /// </summary>
    public static class Convert
    {
        #region ToByte(转换为byte)

        /// <summary>
        /// 转换为8位整型
        /// </summary>
        /// <param name="input">输入值</param>
        public static byte ToByte(object input) => ToByte(input, default);

        /// <summary>
        /// 转换为8位整型
        /// </summary>
        /// <param name="input">输入值</param>
        /// <param name="defaultValue">默认值</param>
        public static byte ToByte(object input, byte defaultValue) => ToByteOrNull(input) ?? defaultValue;

        /// <summary>
        /// 转换为8位可空整型
        /// </summary>
        /// <param name="input">输入值</param>
        public static byte? ToByteOrNull(object input)
        {
            var success = byte.TryParse(input.SafeString(), out var result);
            if (success)
                return result;
            try
            {
                var temp = Util.Helpers.Convert.ToDoubleOrNull(input, 0);
                if (temp == null)
                    return null;
                return Convert.ToByte(temp);
            }
            catch
            {
                return null;
            }
        }

        #endregion

        #region ToChar(转换为char)

        /// <summary>
        /// 转换为字符
        /// </summary>
        /// <param name="input">输入值</param>
        public static char ToChar(object input) => ToChar(input, default);

        /// <summary>
        /// 转换为字符
        /// </summary>
        /// <param name="input">输入值</param>
        /// <param name="defaultValue">默认值</param>
        public static char ToChar(object input, char defaultValue) => ToCharOrNull(input) ?? defaultValue;

        /// <summary>
        /// 转换为可空字符
        /// </summary>
        /// <param name="input">输入值</param>
        public static char? ToCharOrNull(object input)
        {
            var success = char.TryParse(input.SafeString(), out var result);
            if (success)
                return result;
            return null;
        }

        #endregion
    }
}