using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Util.Helpers {
    /// <summary>
    /// 字符串操作
    /// </summary>
    public static class String {

        #region Join(将集合连接为带分隔符的字符串)

        /// <summary>
        /// 将集合连接为带分隔符的字符串
        /// </summary>
        /// <typeparam name="T">集合元素类型</typeparam>
        /// <param name="values">值</param>
        /// <param name="quotes">引号，默认不带引号，范例：单引号 "'"</param>
        /// <param name="separator">分隔符，默认使用逗号分隔</param>
        public static string Join<T>( IEnumerable<T> values, string quotes = "", string separator = "," ) {
            if( values == null )
                return string.Empty;
            var result = new StringBuilder();
            foreach( var each in values )
                result.AppendFormat( "{0}{1}{0}{2}", quotes, each, separator );
            return result.ToString().RemoveEnd( separator );
        }

        #endregion

        #region FirstLowerCase(首字母小写)

        /// <summary>
        /// 首字母小写
        /// </summary>
        /// <param name="value">值</param>
        public static string FirstLowerCase( string value ) {
            if( string.IsNullOrWhiteSpace( value ) )
                return string.Empty;
            var result = Rune.DecodeFromUtf16( value, out var rune, out var charsConsumed );
            if( result != OperationStatus.Done || Rune.IsLower( rune ) )
                return value;
            return Rune.ToLowerInvariant( rune ) + value[charsConsumed..];
        }

        #endregion

        #region FirstUpperCase(首字母大写)

        /// <summary>
        /// 首字母大写
        /// </summary>
        /// <param name="value">值</param>
        public static string FirstUpperCase( string value ) {
            if( string.IsNullOrWhiteSpace( value ) )
                return string.Empty;
            var result = Rune.DecodeFromUtf16( value, out var rune, out var charsConsumed );
            if( result != OperationStatus.Done || Rune.IsUpper( rune ) )
                return value;
            return Rune.ToUpperInvariant( rune ) + value[charsConsumed..];
        }

        #endregion

        #region RemoveStart(移除起始字符串)

        /// <summary>
        /// 移除起始字符串
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="start">要移除的值</param>
        public static string RemoveStart( string value, string start ) {
            if( string.IsNullOrWhiteSpace( value ) )
                return string.Empty;
            if( string.IsNullOrEmpty( start ) )
                return value;
            if ( value.StartsWith( start, StringComparison.Ordinal ) == false )
                return value;
            return value.Substring( start.Length, value.Length - start.Length );
        }

        /// <summary>
        /// 移除起始字符串
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="start">要移除的值</param>
        public static StringBuilder RemoveStart( StringBuilder value, string start ) {
            if( value == null || value.Length == 0 )
                return null;
            if( string.IsNullOrEmpty( start ) )
                return value;
            if( start.Length > value.Length )
                return value;
            var chars = start.ToCharArray();
            for( int i = 0; i < chars.Length; i++ ) {
                if( value[i] != chars[i] )
                    return value;
            }
            return value.Remove( 0, start.Length );
        }

        #endregion

        #region RemoveEnd(移除末尾字符串)

        /// <summary>
        /// 移除末尾字符串
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="end">要移除的值</param>
        public static string RemoveEnd( string value, string end ) {
            if( string.IsNullOrWhiteSpace( value ) )
                return string.Empty;
            if( string.IsNullOrEmpty( end ) )
                return value;
            if( value.EndsWith( end, StringComparison.Ordinal ) == false )
                return value;
            return value.Substring( 0, value.LastIndexOf( end, StringComparison.Ordinal ) );
        }

        /// <summary>
        /// 移除末尾字符串
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="end">要移除的值</param>
        public static StringBuilder RemoveEnd( StringBuilder value, string end ) {
            if( value == null || value.Length == 0 )
                return null;
            if( string.IsNullOrEmpty( end ) )
                return value;
            if( end.Length > value.Length )
                return value;
            var chars = end.ToCharArray();
            for( int i = chars.Length-1; i >= 0; i-- ) {
                var j = value.Length - ( chars.Length - i );
                if( value[j] != chars[i] )
                    return value;
            }
            return value.Remove( value.Length - end.Length, end.Length );
        }

        #endregion

        #region PinYin(获取汉字的拼音简码)

        /// <summary>
        /// 获取汉字的拼音简码，即首字母缩写,范例：中国,返回zg
        /// </summary>
        /// <param name="chineseText">汉字文本,范例： 中国</param>
        public static string PinYin( string chineseText ) {
            if( chineseText.IsEmpty() )
                return string.Empty;
            var result = new StringBuilder();
            foreach( char text in chineseText )
                result.Append( ResolvePinYin( text ) );
            return result.ToString().ToLower();
        }

        /// <summary>
        /// 解析单个汉字的拼音简码
        /// </summary>
        private static string ResolvePinYin( char text ) {
            byte[] charBytes = Encoding.UTF8.GetBytes( text.ToString() );
            if( charBytes[0] <= 127 )
                return text.ToString();
            var unicode = (ushort)( charBytes[0] * 256 + charBytes[1] );
            string pinYin = ResolveByCode( unicode );
            if( pinYin.IsEmpty() == false )
                return pinYin;
            return ResolveByConst( text.ToString() );
        }

        /// <summary>
        /// 使用字符编码方式获取拼音简码
        /// </summary>
        private static string ResolveByCode( ushort unicode ) {
            if( unicode >= '\uB0A1' && unicode <= '\uB0C4' )
                return "A";
            if( unicode >= '\uB0C5' && unicode <= '\uB2C0' && unicode != 45464 )
                return "B";
            if( unicode >= '\uB2C1' && unicode <= '\uB4ED' )
                return "C";
            if( unicode >= '\uB4EE' && unicode <= '\uB6E9' )
                return "D";
            if( unicode >= '\uB6EA' && unicode <= '\uB7A1' )
                return "E";
            if( unicode >= '\uB7A2' && unicode <= '\uB8C0' )
                return "F";
            if( unicode >= '\uB8C1' && unicode <= '\uB9FD' )
                return "G";
            if( unicode >= '\uB9FE' && unicode <= '\uBBF6' )
                return "H";
            if( unicode >= '\uBBF7' && unicode <= '\uBFA5' )
                return "J";
            if( unicode >= '\uBFA6' && unicode <= '\uC0AB' )
                return "K";
            if( unicode >= '\uC0AC' && unicode <= '\uC2E7' )
                return "L";
            if( unicode >= '\uC2E8' && unicode <= '\uC4C2' )
                return "M";
            if( unicode >= '\uC4C3' && unicode <= '\uC5B5' )
                return "N";
            if( unicode >= '\uC5B6' && unicode <= '\uC5BD' )
                return "O";
            if( unicode >= '\uC5BE' && unicode <= '\uC6D9' )
                return "P";
            if( unicode >= '\uC6DA' && unicode <= '\uC8BA' )
                return "Q";
            if( unicode >= '\uC8BB' && unicode <= '\uC8F5' )
                return "R";
            if( unicode >= '\uC8F6' && unicode <= '\uCBF9' )
                return "S";
            if( unicode >= '\uCBFA' && unicode <= '\uCDD9' )
                return "T";
            if( unicode >= '\uCDDA' && unicode <= '\uCEF3' )
                return "W";
            if( unicode >= '\uCEF4' && unicode <= '\uD188' )
                return "X";
            if( unicode >= '\uD1B9' && unicode <= '\uD4D0' )
                return "Y";
            if( unicode >= '\uD4D1' && unicode <= '\uD7F9' )
                return "Z";
            return string.Empty;
        }

        /// <summary>
        /// 通过拼音简码常量获取
        /// </summary>
        private static string ResolveByConst( string text ) {
            int index = Const.ChinesePinYin.IndexOf( text, StringComparison.Ordinal );
            if( index < 0 )
                return string.Empty;
            return Const.ChinesePinYin.Substring( index + 1, 1 );
        }

        #endregion

        #region FullPinYin(获取汉字的全拼)

        /// <summary>
        /// 将汉字转换成拼音(全拼)
        /// </summary>
        /// <param name="text">汉字字符串</param>
        public static string FullPinYin(string text)
        {
            // 匹配中文字符
            var regex = new System.Text.RegularExpressions.Regex("^[\u4e00-\u9fa5]$");
            var array = new byte[2];
            var pyString = "";
            var chrAsc = 0;
            var i1 = 0;
            var i2 = 0;
            var nowChar = text.ToCharArray();
            for (var j = 0; j < nowChar.Length; j++)
            {
                // 中文字符
                if (regex.IsMatch(nowChar[j].ToString()))
                {
                    array = Encoding.Default.GetBytes(nowChar[j].ToString());
                    i1 = (short)(array[0]);
                    i2 = (short)(array[1]);
                    chrAsc = i1 * 256 + i2 - 65536;
                    if (chrAsc > 0 && chrAsc < 160)
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
            new System.Text.RegularExpressions.Regex(@"\\u([0-9A-F]{4})", RegexOptions.IgnoreCase | RegexOptions.Compiled).Replace(value,
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
