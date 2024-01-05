using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Convert = System.Convert;

namespace Util.Extras.Tools.GoogleAuth
{
    /// <summary>
    /// 双因素身份验证器
    /// </summary>
    public class TwoFactorAuthenticator
    {
        /// <summary>
        /// 时代
        /// </summary>
        public static DateTime Epoch = new(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        /// <summary>
        /// 默认时钟漂移容差
        /// </summary>
        public TimeSpan DefaultClockDriftTolerance { get; set; }

        /// <summary>
        /// 使用托管Sha1算法
        /// </summary>
        public bool UseManagedSha1Algorithm { get; set; }

        /// <summary>
        /// 尝试失败时的非托管算法
        /// </summary>
        public bool TryUnmanagedAlgorithmOnFailure { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        public TwoFactorAuthenticator() : this(true, true)
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="useManagedSha1"></param>
        /// <param name="useUnmanagedOnFail"></param>
        public TwoFactorAuthenticator(bool useManagedSha1, bool useUnmanagedOnFail)
        {
            DefaultClockDriftTolerance = TimeSpan.FromMinutes(5.0);
            UseManagedSha1Algorithm = useManagedSha1;
            TryUnmanagedAlgorithmOnFailure = useUnmanagedOnFail;
        }

        /// <summary>
        /// 生成代码
        /// </summary>
        /// <param name="accountTitleNoSpaces"></param>
        /// <param name="accountSecretKey"></param>
        /// <returns></returns>
        public AuthCode GenerateAuthCode(string accountTitleNoSpaces, string accountSecretKey)
        {
            return GenerateAuthCode(null, accountTitleNoSpaces, accountSecretKey);
        }

        /// <summary>
        /// 生成代码
        /// </summary>
        /// <param name="issuer"></param>
        /// <param name="accountTitleNoSpaces"></param>
        /// <param name="accountSecretKey"></param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        public AuthCode GenerateAuthCode(string issuer, string accountTitleNoSpaces, string accountSecretKey)
        {
            if (accountTitleNoSpaces == null)
            {
                throw new NullReferenceException("Account Title is null");
            }

            accountTitleNoSpaces = accountTitleNoSpaces.Replace(" ", "");
            var authCode = new AuthCode
            {
                Account = accountTitleNoSpaces,
                AccountSecretKey = EncodeAccountSecretKey(accountSecretKey)
            };
            var arg = authCode.ManualEntryKey = EncodeAccountSecretKey(accountSecretKey);
            var otpAuthCode = !string.IsNullOrEmpty(issuer)
                ? $"otpauth://totp/{accountTitleNoSpaces}?secret={arg}&issuer={issuer}"
                : $"otpauth://totp/{accountTitleNoSpaces}?secret={arg}";

            authCode.QrCodeImage = otpAuthCode;
            return authCode;
        }

        /// <summary>
        /// Url编码
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private string UrlEncode(string value)
        {
            var stringBuilder = new StringBuilder();
            var text = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-_.~";
            foreach (var c in value)
            {
                if (text.IndexOf(c) != -1)
                {
                    stringBuilder.Append(c);
                }
                else
                {
                    stringBuilder.Append("%" + $"{(int)c:X2}");
                }
            }

            return stringBuilder.ToString().Replace(" ", "%20");
        }

        /// <summary>
        /// 编码帐户密钥
        /// </summary>
        /// <param name="accountSecretKey"></param>
        /// <returns></returns>
        public string EncodeAccountSecretKey(string accountSecretKey)
        {
            return Base32String.Instance.Encode(Encoding.Default.GetBytes(accountSecretKey));
            // return Base32Encode(Encoding.Default.GetBytes(accountSecretKey));
        }

        /// <summary>
        /// Base32编码
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private string Base32Encode(byte[] data)
        {
            var num = 8;
            var num2 = 5;
            var array = "ABCDEFGHIJKLMNOPQRSTUVWXYZ234567".ToCharArray();
            var num3 = 0;
            var num4 = 0;
            var stringBuilder = new StringBuilder((data.Length + 7) * num / num2);
            while (num3 < data.Length)
            {
                var num6 = (data[num3] >= 0) ? data[num3] : (data[num3] + 256);
                int num5;
                if (num4 > num - num2)
                {
                    var num7 = (num3 + 1 < data.Length)
                        ? ((data[num3 + 1] >= 0) ? data[num3 + 1] : (data[num3 + 1] + 256))
                        : 0;
                    num5 = (num6 & (255 >> num4));
                    num4 = (num4 + num2) % num;
                    num5 <<= num4;
                    num5 |= num7 >> num - num4;
                    num3++;
                }
                else
                {
                    num5 = ((num6 >> num - (num4 + num2)) & 0x1F);
                    num4 = (num4 + num2) % num;
                    if (num4 == 0)
                    {
                        num3++;
                    }
                }

                stringBuilder.Append(array[num5]);
            }

            return stringBuilder.ToString();
        }

        /// <summary>
        /// 生成PIN间隔
        /// </summary>
        /// <param name="accountSecretKey"></param>
        /// <param name="counter"></param>
        /// <param name="digits"></param>
        /// <returns></returns>
        public string GeneratePinAtInterval(string accountSecretKey, long counter, int digits = 6)
        {
            return GenerateHashedCode(accountSecretKey, counter, digits);
        }

        /// <summary>
        /// 生成哈希代码
        /// </summary>
        /// <param name="secret"></param>
        /// <param name="iterationNumber"></param>
        /// <param name="digits"></param>
        /// <returns></returns>
        public string GenerateHashedCode(string secret, long iterationNumber, int digits = 6)
        {
            var bytes = Encoding.ASCII.GetBytes(secret);
            return GenerateHashedCode(bytes, iterationNumber, digits);
        }

        /// <summary>
        /// 生成哈希代码
        /// </summary>
        /// <param name="key"></param>
        /// <param name="iterationNumber"></param>
        /// <param name="digits"></param>
        /// <returns></returns>
        public string GenerateHashedCode(byte[] key, long iterationNumber, int digits = 6)
        {
            var bytes = BitConverter.GetBytes(iterationNumber);
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(bytes);
            }

            var hMacSha1Algorithm = GetHmacSha1Algorithm(key);
            var array = hMacSha1Algorithm.ComputeHash(bytes);
            var num = array[^1] & 0xF;
            var num2 = ((array[num] & 0x7F) << 24) | (array[num + 1] << 16) | (array[num + 2] << 8) | array[num + 3];
            return (num2 % (int)Math.Pow(10.0, (double)digits)).ToString(new string('0', digits));
        }

        /// <summary>
        /// 获取当前计数器
        /// </summary>
        /// <returns></returns>
        private long GetCurrentCounter()
        {
            return GetCurrentCounter(DateTime.UtcNow, Epoch, 30);
        }

        /// <summary>
        /// 获取当前计数器
        /// </summary>
        /// <param name="now"></param>
        /// <param name="epoch"></param>
        /// <param name="timeStep"></param>
        /// <returns></returns>
        private long GetCurrentCounter(DateTime now, DateTime epoch, int timeStep)
        {
            return (long)(now - epoch).TotalSeconds / timeStep;
        }

        /// <summary>
        /// 获取 HMAC Sha1 算法
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private HMACSHA1 GetHmacSha1Algorithm(byte[] key)
        {
            try
            {
#pragma warning disable SYSLIB0030
                return new HMACSHA1(key, UseManagedSha1Algorithm);
#pragma warning restore SYSLIB0030
            }
            catch (InvalidOperationException ex2)
            {
                if (UseManagedSha1Algorithm && TryUnmanagedAlgorithmOnFailure)
                {
                    try
                    {
#pragma warning disable SYSLIB0030
                        return new HMACSHA1(key, false);
#pragma warning restore SYSLIB0030
                    }
                    catch (InvalidOperationException ex)
                    {
                        throw ex;
                    }
                }

                throw ex2;
            }
        }

        /// <summary>
        /// 验证双因素PIN
        /// </summary>
        /// <param name="accountSecretKey"></param>
        /// <param name="twoFactorCodeFromClient"></param>
        /// <returns></returns>
        public bool ValidateTwoFactorPin(string accountSecretKey, string twoFactorCodeFromClient)
        {
            return ValidateTwoFactorPin(accountSecretKey, twoFactorCodeFromClient, DefaultClockDriftTolerance);
        }

        /// <summary>
        /// 验证双因素PIN
        /// </summary>
        /// <param name="accountSecretKey"></param>
        /// <param name="twoFactorCodeFromClient"></param>
        /// <param name="timeTolerance"></param>
        /// <returns></returns>
        public bool ValidateTwoFactorPin(string accountSecretKey, string twoFactorCodeFromClient,
            TimeSpan timeTolerance)
        {
            accountSecretKey = Encoding.Default.GetString(Base32String.Instance.Decode(accountSecretKey));
            var currentPins = GetCurrentPins(accountSecretKey, timeTolerance);
            return currentPins.Any((string c) => c == twoFactorCodeFromClient);
        }

        /// <summary>
        /// 获取当前PIN
        /// </summary>
        /// <param name="accountSecretKey"></param>
        /// <returns></returns>
        public string GetCurrentPin(string accountSecretKey)
        {
            return GeneratePinAtInterval(accountSecretKey, GetCurrentCounter(), 6);
        }

        /// <summary>
        /// 获取当前PIN
        /// </summary>
        /// <param name="accountSecretKey"></param>
        /// <param name="now"></param>
        /// <returns></returns>
        public string GetCurrentPin(string accountSecretKey, DateTime now)
        {
            return GeneratePinAtInterval(accountSecretKey, GetCurrentCounter(now, Epoch, 30), 6);
        }

        /// <summary>
        /// 获取当前PINs
        /// </summary>
        /// <param name="accountSecretKey"></param>
        /// <returns></returns>
        public string[] GetCurrentPins(string accountSecretKey)
        {
            return GetCurrentPins(accountSecretKey, DefaultClockDriftTolerance);
        }

        /// <summary>
        /// 获取当前PINs
        /// </summary>
        /// <param name="accountSecretKey"></param>
        /// <param name="timeTolerance"></param>
        /// <returns></returns>
        public string[] GetCurrentPins(string accountSecretKey, TimeSpan timeTolerance)
        {
            var list = new List<string>();
            var currentCounter = GetCurrentCounter();
            var num = 0;
            if (timeTolerance.TotalSeconds > 30.0)
            {
                num = Convert.ToInt32(timeTolerance.TotalSeconds / 30.0);
            }

            var num2 = currentCounter - num;
            var num3 = currentCounter + num;
            for (var num4 = num2; num4 <= num3; num4++)
            {
                list.Add(GeneratePinAtInterval(accountSecretKey, num4));
            }

            return list.ToArray();
        }
    }
}