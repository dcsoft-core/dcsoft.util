using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Regex = Util.Helpers.Regex;

namespace Util.Extras.Helpers
{
    /// <summary>
    /// 字符串操作
    /// </summary>
    public static class String
    {
        #region FullPinYin(获取汉字的全拼)

        /// <summary>
        /// 将汉字转换成拼音(全拼)
        /// </summary>
        /// <param name="text">汉字字符串</param>
        public static string FullPinYin(string text)
        {
            // 匹配中文字符
            var regex = new System.Text.RegularExpressions.Regex("^[\u4e00-\u9fa5]$");
            var pyString = "";
            var nowChar = text.ToCharArray();
            for (var j = 0; j < nowChar.Length; j++)
            {
                // 中文字符
                if (regex.IsMatch(nowChar[j].ToString()))
                {
                    var array = Encoding.Default.GetBytes(nowChar[j].ToString());
                    int i1 = array[0];
                    int i2 = array[1];
                    var chrAsc = i1 * 256 + i2 - 65536;
                    if (chrAsc is > 0 and < 160)
                    {
                        pyString += nowChar[j];
                    }
                    else
                    {
                        // 修正部分文字
                        switch (chrAsc)
                        {
                            case -9254:
                                pyString += "Zhen";
                                break;
                            case -8985:
                                pyString += "Qian";
                                break;
                            case -5463:
                                pyString += "Jia";
                                break;
                            case -8274:
                                pyString += "Ge";
                                break;
                            case -5448:
                                pyString += "Ga";
                                break;
                            case -5447:
                                pyString += "La";
                                break;
                            case -4649:
                                pyString += "Chen";
                                break;
                            case -5436:
                                pyString += "Mao";
                                break;
                            case -5213:
                                pyString += "Mao";
                                break;
                            case -3597:
                                pyString += "Die";
                                break;
                            case -5659:
                                pyString += "Tian";
                                break;
                            default:
                                for (var i = (Const.SpellCode.Length - 1); i >= 0; i--)
                                {
                                    if (Const.SpellCode[i] <= chrAsc)
                                    {
                                        //判断汉字的拼音区编码是否在指定范围内
                                        pyString += Const.SpellLetter[j]; //如果不超出范围则获取对应的拼音
                                        break;
                                    }
                                }

                                break;
                        }
                    }
                }
                else // 非中文字符
                {
                    pyString += nowChar[j].ToString();
                }
            }

            return pyString;
        }

        #endregion

        #region ToUnicode(字符串转Unicode)

        /// <summary>
        /// 字符串转Unicode
        /// </summary>
        /// <param name="value">值</param>
        public static string ToUnicode(string value)
        {
            var bytes = Encoding.Unicode.GetBytes(value);
            var sb = new StringBuilder();
            for (var i = 0; i < bytes.Length; i += 2)
                sb.AppendFormat("\\u{0}{1}", bytes[i + 1].ToString("x").PadLeft(2, '0'),
                    bytes[i].ToString("x").PadLeft(2, '0'));
            return sb.ToString();
        }

        #endregion

        #region ToUnicodeByCn(中文字符串转Unicode)

        /// <summary>
        /// 中文字符串转Unicode
        /// </summary>
        /// <param name="value">值</param>
        public static string ToUnicodeByCn(string value)
        {
            var sb = new StringBuilder();
            if (!string.IsNullOrWhiteSpace(value))
            {
                var chars = value.ToCharArray();
                for (var i = 0; i < value.Length; i++)
                {
                    // 将中文字符串转换为十进制整数，然后转为十六进制Unicode字符
                    sb.Append(Regex.IsMatch(chars[i].ToString(), "([\u4e00-\u9fa5])")
                        ? ToUnicode(chars[i].ToString())
                        : chars[i].ToString());
                }
            }

            return sb.ToString();
        }

        #endregion

        #region UnicodeToStr(Unicode转字符串)

        /// <summary>
        /// Unicode转字符串
        /// </summary>
        /// <param name="value">值</param>
        public static string UnicodeToStr(string value) =>
            new System.Text.RegularExpressions.Regex(@"\\u([0-9A-F]{4})",
                RegexOptions.IgnoreCase | RegexOptions.Compiled).Replace(value,
                x => Convert.ToChar(System.Convert.ToUInt16(x.Result("$1"), 16)).ToString());

        #endregion

        #region Empty(空字符串)

        /// <summary>
        /// 空字符串
        /// </summary>
        public static string Empty => string.Empty;

        #endregion

        #region Distinct(去除重复)

        /// <summary>
        /// 去除重复字符串
        /// </summary>
        /// <param name="value">值，范例1："5555"，返回"5"，范例2："4545"，返回"45"</param>
        public static string Distinct(string value)
        {
            var array = value.ToCharArray();
            return new string(array.Distinct().ToArray());
        }

        #endregion

        #region Truncate(截断字符串)

        /// <summary>
        /// 截断字符串
        /// </summary>
        /// <param name="text">文本</param>
        /// <param name="length">返回长度</param>
        /// <param name="endChatCount">添加结束符号的个数，默认0，不添加</param>
        /// <param name="endChar">结束符号，默认为省略号</param>
        public static string Truncate(string text, int length, int endChatCount = 0, string endChar = ".")
        {
            if (string.IsNullOrWhiteSpace(text))
                return string.Empty;
            if (text.Length < length)
                return text;
            return $"{text.Substring(0, length)}{GetEndString(endChatCount, endChar)}";
        }

        /// <summary>
        /// 获取结束字符串
        /// </summary>
        /// <param name="endCharCount">添加结束符号的个数</param>
        /// <param name="endChar">结束符号</param>
        private static string GetEndString(int endCharCount, string endChar)
        {
            var sb = new StringBuilder();
            for (var i = 0; i < endCharCount; i++)
                sb.Append(endChar);
            return sb.ToString();
        }

        #endregion

        #region GetLastProperty(获取最后一个属性)

        /// <summary>
        /// 获取最后一个属性
        /// </summary>
        /// <param name="propertyName">属性名，范例，A.B.C,返回"C"</param>
        public static string GetLastProperty(string propertyName)
        {
            if (string.IsNullOrWhiteSpace(propertyName))
                return string.Empty;
            var lastIndex = propertyName.LastIndexOf(".", StringComparison.Ordinal) + 1;
            return propertyName.Substring(lastIndex);
        }

        #endregion

        #region GetHideMobile(获取隐藏中间几位后的手机号码)

        /// <summary>
        /// 获取隐藏中间几位后的手机号码
        /// </summary>
        /// <param name="value">手机号码</param>
        public static string GetHideMobile(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return string.Empty;
            return $"{value.Substring(0, 3)}******{value.Substring(value.Length - 3)}";
        }

        #endregion

        #region GetStringLength(获取字符串的字节数)

        /// <summary>
        /// 获取字符串的字节数
        /// </summary>
        /// <param name="value">值</param>
        public static int GetStringLength(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return 0;
            int strLength = 0;
            var encoding = new ASCIIEncoding();
            // 将字符串转换为ASCII编码的字节数字
            byte[] bytes = encoding.GetBytes(value);
            for (var i = 0; i <= bytes.Length - 1; i++)
            {
                if (bytes[i] == 63)
                    strLength++;
                strLength++;
            }

            return strLength;
        }

        #endregion

        #region ToSnakeCase(将字符串转换为蛇形策略)

        /// <summary>
        /// 将字符串转换为蛇形策略
        /// </summary>
        /// <param name="str">字符串</param>
        public static string ToSnakeCase(string str)
        {
            if (string.IsNullOrEmpty(str))
                return str;

            var sb = new StringBuilder();
            var state = SnakeCaseState.Start;
            for (var i = 0; i < str.Length; i++)
            {
                if (str[i] == ' ')
                {
                    if (state != SnakeCaseState.Start)
                        state = SnakeCaseState.NewWord;
                }
                else if (char.IsUpper(str[i]))
                {
                    switch (state)
                    {
                        case SnakeCaseState.Upper:
                            bool hasNext = (i + 1 < str.Length);
                            if (i > 0 && hasNext)
                            {
                                char nextChar = str[i + 1];
                                if (!char.IsUpper(nextChar) && nextChar != '_')
                                {
                                    sb.Append('_');
                                }
                            }

                            break;

                        case SnakeCaseState.Lower:
                        case SnakeCaseState.NewWord:
                            sb.Append('_');
                            break;
                    }

                    sb.Append(char.ToLowerInvariant(str[i]));
                    state = SnakeCaseState.Upper;
                }
                else if (str[i] == '_')
                {
                    sb.Append('_');
                    state = SnakeCaseState.Start;
                }
                else
                {
                    if (state == SnakeCaseState.NewWord)
                    {
                        sb.Append('_');
                    }

                    sb.Append(str[i]);
                    state = SnakeCaseState.Lower;
                }
            }

            return sb.ToString();
        }

        #endregion

        #region ToCamelCase(将字符串转换为骆驼策略)

        /// <summary>
        /// 将字符串转换为骆驼策略
        /// </summary>
        /// <param name="str">字符串</param>
        public static string ToCamelCase(string str)
        {
            if (string.IsNullOrEmpty(str) || !char.IsUpper(str[0]))
                return str;
            char[] chars = str.ToCharArray();
            for (var i = 0; i < chars.Length; i++)
            {
                if (i == 1 && !char.IsUpper(chars[i]))
                    break;
                bool hasNext = (i + 1 < chars.Length);
                if (i > 0 && hasNext && !char.IsUpper(chars[i + 1]))
                {
                    if (char.IsSeparator(chars[i + 1]))
                        chars[i] = char.ToLowerInvariant(chars[i]);
                    break;
                }

                chars[i] = char.ToLowerInvariant(chars[i]);
            }

            return new string(chars);
        }

        #endregion

        #region GenerateNonceStr(生成随机字符串)

        /// <summary>
        /// 生成随机字符串
        /// </summary>
        public static string GenerateNonceStr() => Guid.NewGuid().ToString("N");

        #endregion

        #region SplitWordGroup(分隔词组)

        /// <summary>
        /// 分隔词组
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="separator">分隔符。默认使用"-"分隔</param>
        public static string SplitWordGroup(string value, char separator = '-')
        {
            var pattern = @"([A-Z])(?=[a-z])|(?<=[a-z])([A-Z]|[0-9]+)";
            return string.IsNullOrWhiteSpace(value)
                ? string.Empty
                : System.Text.RegularExpressions.Regex.Replace(value, pattern, $"{separator}$1$2").TrimStart(separator)
                    .ToLower();
        }

        #endregion
    }

    /// <summary>
    /// 字符串策略
    /// </summary>
    public enum StringCase
    {
        /// <summary>
        /// 蛇形策略
        /// </summary>
        Snake,

        /// <summary>
        /// 骆驼策略
        /// </summary>
        Camel,

        /// <summary>
        /// 不执行策略
        /// </summary>
        None,
    }

    /// <summary>
    /// 蛇形策略状态
    /// </summary>
    internal enum SnakeCaseState
    {
        /// <summary>
        /// 开头
        /// </summary>
        Start,

        /// <summary>
        /// 小写
        /// </summary>
        Lower,

        /// <summary>
        /// 大写
        /// </summary>
        Upper,

        /// <summary>
        /// 单词
        /// </summary>
        NewWord
    }
}