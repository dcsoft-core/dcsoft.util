using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace Util.Extras.Tools.IPLocation
{
    // ReSharper disable once InconsistentNaming
    /// <summary>
    /// 查询qqwry.dat
    /// </summary>
    public class IPScanner
    {
        private readonly byte[] _data;

        private readonly Regex _regex =
            new Regex(@"(((\d{1,2})|(1\d{2})|(2[0-4]\d)|(25[0-5]))\.){3}((\d{1,2})|(1\d{2})|(2[0-4]\d)|(25[0-5]))");

        private readonly long _firstStartIpOffset;

        /// <summary>
        /// 数量
        /// </summary>
        public long Count { get; }

        /// <summary>
        /// 查询文件
        /// </summary>
        /// <param name="dataPath"></param>
        public IPScanner(string dataPath)
        {
            using (var fs = new FileStream(dataPath, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                _data = new byte[fs.Length];
                _ = fs.Read(_data, 0, _data.Length);
            }

            var buffer = new byte[8];
            Array.Copy(_data, 0, buffer, 0, 8);
            _firstStartIpOffset = buffer[0] + buffer[1] * 0x100 + buffer[2] * 0x100 * 0x100 +
                                  buffer[3] * 0x100 * 0x100 * 0x100;
            long lastStartIpOffset = buffer[4] + buffer[5] * 0x100 + buffer[6] * 0x100 * 0x100 +
                                     buffer[7] * 0x100 * 0x100 * 0x100;
            Count = Convert.ToInt64((lastStartIpOffset - _firstStartIpOffset) / 7.0);

            if (Count <= 1L)
            {
                throw new ArgumentException("ip FileDataError");
            }
        }

        private static long IpToInt(string ip)
        {
            var separator = new[] { '.' };
            if (ip.Split(separator).Length == 3)
            {
                ip = ip + ".0";
            }

            var strArray = ip.Split(separator);
            var num2 = long.Parse(strArray[0]) * 0x100L * 0x100L * 0x100L;
            var num3 = long.Parse(strArray[1]) * 0x100L * 0x100L;
            var num4 = long.Parse(strArray[2]) * 0x100L;
            var num5 = long.Parse(strArray[3]);
            return num2 + num3 + num4 + num5;
        }

        // ReSharper disable once UnusedMember.Local
        // ReSharper disable once InconsistentNaming
        private static string IntToIP(long ipInt)
        {
            var num = (ipInt & 0xff000000L) >> 0x18;
            if (num < 0L)
            {
                num += 0x100L;
            }

            var num2 = (ipInt & 0xff0000L) >> 0x10;
            if (num2 < 0L)
            {
                num2 += 0x100L;
            }

            var num3 = (ipInt & 0xff00L) >> 8;
            if (num3 < 0L)
            {
                num3 += 0x100L;
            }

            var num4 = ipInt & 0xffL;
            if (num4 < 0L)
            {
                num4 += 0x100L;
            }

            return num + "." + num2 + "." + num3 + "." + num4;
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public IPLocation Query(string ip)
        {
            ip = ip.Equals("::1") ? "127.0.0.1" : ip;
            if (!_regex.Match(ip).Success)
            {
                ip = "300.300.300.300";
            }

            var ipLocation = new IPLocation() { IP = ip };
            // ReSharper disable once InconsistentNaming
            var intIP = IpToInt(ip);
            if (intIP >= IpToInt("127.0.0.1") && intIP <= IpToInt("127.255.255.255"))
            {
                ipLocation.Country = "本机内部环回地址";
                ipLocation.Local = "";
            }
            else if (intIP >= IpToInt("0.0.0.0") && intIP <= IpToInt("2.255.255.255") ||
                     intIP >= IpToInt("10.0.0.0") && intIP <= IpToInt("10.255.255.255") ||
                     intIP >= IpToInt("172.16.0.0") && intIP <= IpToInt("172.31.255.255") ||
                     intIP >= IpToInt("192.168.0.0") && intIP <= IpToInt("192.168.255.255"))
            {
                ipLocation.Country = "网络保留地址";
                ipLocation.Local = "";
            }
            else
            {
                var right = Count;
                var left = 0L;
                long startIp;
                long endIpOff;
                while (left < right - 1L)
                {
                    var middle = (right + left) / 2L;
                    startIp = GetStartIp(middle, out endIpOff);
                    if (intIP == startIp)
                    {
                        left = middle;
                        break;
                    }

                    if (intIP > startIp)
                    {
                        left = middle;
                    }
                    else
                    {
                        right = middle;
                    }
                }

                startIp = GetStartIp(left, out endIpOff);
                var endIp = GetEndIp(endIpOff, out var countryFlag);
                if (startIp <= intIP && endIp >= intIP)
                {
                    ipLocation.Country = GetCountry(endIpOff, countryFlag, out var local);
                    ipLocation.Local = local;
                }
                else
                {
                    ipLocation.Country = "未知的IP地址";
                    ipLocation.Local = "";
                }
            }

            return ipLocation;
        }

        private long GetStartIp(long left, out long endIpOff)
        {
            var leftOffset = _firstStartIpOffset + left * 7L;
            var buffer = new byte[7];
            Array.Copy(_data, leftOffset, buffer, 0, 7);
            endIpOff = Convert.ToInt64(buffer[4].ToString()) + Convert.ToInt64(buffer[5].ToString()) * 0x100L +
                       Convert.ToInt64(buffer[6].ToString()) * 0x100L * 0x100L;
            return Convert.ToInt64(buffer[0].ToString()) + Convert.ToInt64(buffer[1].ToString()) * 0x100L +
                   Convert.ToInt64(buffer[2].ToString()) * 0x100L * 0x100L +
                   Convert.ToInt64(buffer[3].ToString()) * 0x100L * 0x100L * 0x100L;
        }

        private long GetEndIp(long endIpOff, out int countryFlag)
        {
            var buffer = new byte[5];
            Array.Copy(_data, endIpOff, buffer, 0, 5);
            countryFlag = buffer[4];
            return Convert.ToInt64(buffer[0].ToString()) + Convert.ToInt64(buffer[1].ToString()) * 0x100L +
                   Convert.ToInt64(buffer[2].ToString()) * 0x100L * 0x100L +
                   Convert.ToInt64(buffer[3].ToString()) * 0x100L * 0x100L * 0x100L;
        }

        /// <summary>
        /// Gets the country.
        /// </summary>
        /// <param name="endIpOff">The end ip off.</param>
        /// <param name="countryFlag">The country flag.</param>
        /// <param name="local">The local.</param>
        /// <returns>country</returns>
        private string GetCountry(long endIpOff, int countryFlag, out string local)
        {
            string country;
            var offset = endIpOff + 4L;
            switch (countryFlag)
            {
                case 1:
                case 2:
                    country = GetFlagStr(ref offset, ref countryFlag, ref endIpOff);
                    offset = endIpOff + 8L;
                    local = 1 == countryFlag ? "" : GetFlagStr(ref offset, ref countryFlag, ref endIpOff);
                    break;
                default:
                    country = GetFlagStr(ref offset, ref countryFlag, ref endIpOff);
                    local = GetFlagStr(ref offset, ref countryFlag, ref endIpOff);
                    break;
            }

            return country;
        }

        private string GetFlagStr(ref long offset, ref int countryFlag, ref long endIpOff)
        {
            var buffer = new byte[3];

            while (true)
            {
                //用于向前累加偏移量
                var forwardOffset = offset;
                int flag = _data[forwardOffset++];
                //没有重定向
                if (flag != 1 && flag != 2)
                {
                    break;
                }

                Array.Copy(_data, forwardOffset, buffer, 0, 3);
                if (flag == 2)
                {
                    countryFlag = 2;
                    endIpOff = offset - 4L;
                }

                offset = Convert.ToInt64(buffer[0].ToString()) + Convert.ToInt64(buffer[1].ToString()) * 0x100L +
                         Convert.ToInt64(buffer[2].ToString()) * 0x100L * 0x100L;
            }

            if (offset < 12L)
            {
                return "";
            }

            return GetStr(ref offset);
        }

        private string GetStr(ref long offset)
        {
            var stringBuilder = new StringBuilder();
            var bytes = new byte[2];
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            var encoding = Encoding.GetEncoding("GB2312");
            while (true)
            {
                var lowByte = _data[offset++];
                if (lowByte == 0)
                {
                    return stringBuilder.ToString();
                }

                if (lowByte > 0x7f)
                {
                    var highByte = _data[offset++];
                    bytes[0] = lowByte;
                    bytes[1] = highByte;
                    if (highByte == 0)
                    {
                        return stringBuilder.ToString();
                    }

                    stringBuilder.Append(encoding.GetString(bytes));
                }
                else
                {
                    stringBuilder.Append((char)lowByte);
                }
            }
        }
    }
}