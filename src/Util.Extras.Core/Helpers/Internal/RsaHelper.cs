using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Util.Extras.Helpers.Internal
{
    /// <summary>
    /// RSA加解密 使用OpenSSL的公钥加密/私钥解密
    ///
    /// 公私钥请使用openssl生成  ssh-keygen -t rsa 命令生成的公钥私钥是不行的
    ///
    /// 作者：李志强
    /// 时间：2017年10月30日15:50:14
    /// QQ:501232752
    /// </summary>
    internal class RsaHelper
    {
        private readonly RSA _privateKeyRsaProvider;
        private readonly RSA _publicKeyRsaProvider;
        private readonly HashAlgorithmName _hashAlgorithmName;
        private readonly Encoding _encoding;

        /// <summary>
        /// 实例化RSAHelper
        /// </summary>
        /// <param name="rsaType">加密算法类型 RSA SHA1;RSA2 SHA256 密钥长度至少为2048</param>
        /// <param name="encoding">编码类型</param>
        /// <param name="privateKey">私钥</param>
        /// <param name="publicKey">公钥</param>
        public RsaHelper(RSAType rsaType, Encoding encoding, string privateKey = null, string publicKey = null)
        {
            _encoding = encoding;
            if (!string.IsNullOrEmpty(privateKey))
                _privateKeyRsaProvider = CreateRsaProviderFromPrivateKey(privateKey);
            if (!string.IsNullOrEmpty(publicKey))
                _publicKeyRsaProvider = CreateRsaProviderFromPublicKey(publicKey);
            _hashAlgorithmName = rsaType == RSAType.RSA ? HashAlgorithmName.SHA1 : HashAlgorithmName.SHA256;
        }

        #region 使用私钥签名

        /// <summary>
        /// 使用私钥签名
        /// </summary>
        /// <param name="data">原始数据</param>
        public string Sign(string data)
        {
            var dataBytes = _encoding.GetBytes(data);
            var signatureBytes =
                _privateKeyRsaProvider.SignData(dataBytes, _hashAlgorithmName, RSASignaturePadding.Pkcs1);
            return System.Convert.ToBase64String(signatureBytes);
        }

        #endregion

        #region 使用公钥验证签名

        /// <summary>
        /// 使用公钥验证签名
        /// </summary>
        /// <param name="data">原始数据</param>
        /// <param name="sign">签名</param>
        public bool Verify(string data, string sign)
        {
            var dataBytes = _encoding.GetBytes(data);
            var signBytes = System.Convert.FromBase64String(sign);
            var verify =
                _publicKeyRsaProvider.VerifyData(dataBytes, signBytes, _hashAlgorithmName, RSASignaturePadding.Pkcs1);
            return verify;
        }

        #endregion

        #region 解密

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="cipherText">密文</param>
        public string Decrypt(string cipherText)
        {
            if (_privateKeyRsaProvider == null)
                throw new Exception("_privateKeyRsaProvider is null");
            return Encoding.UTF8.GetString(_privateKeyRsaProvider.Decrypt(System.Convert.FromBase64String(cipherText),
                RSAEncryptionPadding.Pkcs1));
        }

        #endregion

        #region 加密

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="text">文本</param>
        public string Encrypt(string text)
        {
            if (_publicKeyRsaProvider == null)
                throw new Exception("_publicKeyRsaProvider is null");
            return System.Convert.ToBase64String(_publicKeyRsaProvider.Encrypt(Encoding.UTF8.GetBytes(text),
                RSAEncryptionPadding.Pkcs1));
        }

        #endregion

        #region 使用私钥创建RSA实例

        /// <summary>
        /// 通过私钥创建RSA实例
        /// </summary>
        /// <param name="privateKey">私钥</param>
        public RSA CreateRsaProviderFromPrivateKey(string privateKey)
        {
            var privateKeyBits = System.Convert.FromBase64String(privateKey);
            var rsa = RSA.Create();
            var rsaParameters = new RSAParameters();
            using (var binReader = new BinaryReader(new MemoryStream(privateKeyBits)))
            {
                var twoBytes = binReader.ReadUInt16();
                if (twoBytes == 0x8130)
                    binReader.ReadByte();
                else if (twoBytes == 0x8230)
                    binReader.ReadInt16();
                else
                    throw new Exception("Unexpected value read binReader.ReadUInt16()");

                twoBytes = binReader.ReadUInt16();
                if (twoBytes != 0x0102)
                    throw new Exception("Unexpected version");

                var bt = binReader.ReadByte();
                if (bt != 0x00)
                    throw new Exception("Unexpected value read binReader.ReadByte()");

                rsaParameters.Modulus = binReader.ReadBytes(GetIntegerSize(binReader));
                rsaParameters.Exponent = binReader.ReadBytes(GetIntegerSize(binReader));
                rsaParameters.D = binReader.ReadBytes(GetIntegerSize(binReader));
                rsaParameters.P = binReader.ReadBytes(GetIntegerSize(binReader));
                rsaParameters.Q = binReader.ReadBytes(GetIntegerSize(binReader));
                rsaParameters.DP = binReader.ReadBytes(GetIntegerSize(binReader));
                rsaParameters.DQ = binReader.ReadBytes(GetIntegerSize(binReader));
                rsaParameters.InverseQ = binReader.ReadBytes(GetIntegerSize(binReader));
            }

            rsa.ImportParameters(rsaParameters);
            return rsa;
        }

        #endregion

        #region 使用公钥创建RSA实例

        /// <summary>
        /// 通过公钥创建RSA实例
        /// </summary>
        /// <param name="publicKeyString">公钥</param>
        public RSA CreateRsaProviderFromPublicKey(string publicKeyString)
        {
            // encoded OID sequence for  PKCS #1 rsaEncryption szOID_RSA_RSA = "1.2.840.113549.1.1.1"
            byte[] seqOid =
                { 0x30, 0x0D, 0x06, 0x09, 0x2A, 0x86, 0x48, 0x86, 0xF7, 0x0D, 0x01, 0x01, 0x01, 0x05, 0x00 };

            var x509Key = System.Convert.FromBase64String(publicKeyString);

            // ---------  Set up stream to read the asn.1 encoded SubjectPublicKeyInfo blob  ------
            using var mem = new MemoryStream(x509Key);
            using var binReader = new BinaryReader(mem);

            var twoBytes = binReader.ReadUInt16();
            if (twoBytes == 0x8130) //data read as little endian order (actual data order for Sequence is 30 81)
                binReader.ReadByte(); //advance 1 byte
            else if (twoBytes == 0x8230)
                binReader.ReadInt16(); //advance 2 bytes
            else
                return null;

            var seq = binReader.ReadBytes(15);
            if (!CompareByteArrays(seq, seqOid)) //make sure Sequence for OID is correct
                return null;

            twoBytes = binReader.ReadUInt16();
            if (twoBytes == 0x8103) //data read as little endian order (actual data order for Bit String is 03 81)
                binReader.ReadByte(); //advance 1 byte
            else if (twoBytes == 0x8203)
                binReader.ReadInt16(); //advance 2 bytes
            else
                return null;

            var bt = binReader.ReadByte();
            if (bt != 0x00) //expect null byte next
                return null;

            twoBytes = binReader.ReadUInt16();
            if (twoBytes == 0x8130) //data read as little endian order (actual data order for Sequence is 30 81)
                binReader.ReadByte(); //advance 1 byte
            else if (twoBytes == 0x8230)
                binReader.ReadInt16(); //advance 2 bytes
            else
                return null;

            twoBytes = binReader.ReadUInt16();
            byte lowByte;
            byte highByte = 0x00;

            if (twoBytes == 0x8102) //data read as little endian order (actual data order for Integer is 02 81)
                lowByte = binReader.ReadByte(); // read next bytes which is bytes in modulus
            else if (twoBytes == 0x8202)
            {
                highByte = binReader.ReadByte(); //advance 2 bytes
                lowByte = binReader.ReadByte();
            }
            else
                return null;

            byte[] modInt =
                { lowByte, highByte, 0x00, 0x00 }; //reverse byte order since asn.1 key uses big endian order
            var modSize = BitConverter.ToInt32(modInt, 0);

            var firstByte = binReader.PeekChar();
            if (firstByte == 0x00)
            {
                //if first byte (highest order) of modulus is zero, don't include it
                binReader.ReadByte(); //skip this null byte
                modSize -= 1; //reduce modulus buffer size by 1
            }

            var modulus = binReader.ReadBytes(modSize); //read the modulus bytes

            if (binReader.ReadByte() != 0x02) //expect an Integer for the exponent data
                return null;
            var expBytes =
                (int)binReader.ReadByte(); // should only need one byte for actual exponent data (for all useful values)
            var exponent = binReader.ReadBytes(expBytes);

            // ------- create RSACryptoServiceProvider instance and initialize with public key -----
            var rsa = RSA.Create();
            var rsaKeyInfo = new RSAParameters
            {
                Modulus = modulus,
                Exponent = exponent
            };
            rsa.ImportParameters(rsaKeyInfo);

            return rsa;
        }

        #endregion

        #region 导入密钥算法

        /// <summary>
        /// 获取int大小
        /// </summary>
        /// <param name="binReader">二进制读取器</param>
        private int GetIntegerSize(BinaryReader binReader)
        {
            int count;
            var bt = binReader.ReadByte();
            if (bt != 0x02)
                return 0;
            bt = binReader.ReadByte();

            if (bt == 0x81)
                count = binReader.ReadByte();
            else if (bt == 0x82)
            {
                var highByte = binReader.ReadByte();
                var lowByte = binReader.ReadByte();
                byte[] modInt = { lowByte, highByte, 0x00, 0x00 };
                count = BitConverter.ToInt32(modInt, 0);
            }
            else
            {
                count = bt;
            }

            while (binReader.ReadByte() == 0x00)
            {
                count -= 1;
            }

            binReader.BaseStream.Seek(-1, SeekOrigin.Current);
            return count;
        }

        /// <summary>
        /// 加密byte[]
        /// </summary>
        /// <param name="a">byte[]</param>
        /// <param name="b">byte[]</param>
        private static bool CompareByteArrays(IReadOnlyCollection<byte> a, IReadOnlyList<byte> b)
        {
            if (a.Count != b.Count)
                return false;
            var i = 0;
            foreach (var c in a)
            {
                if (c != b[i])
                    return false;
                i++;
            }

            return true;
        }

        #endregion
    }

    /// <summary>
    /// RSA算法类型
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public enum RSAType
    {
        /// <summary>
        /// SHA1
        /// </summary>
        // ReSharper disable once InconsistentNaming
        RSA = 0,

        /// <summary>
        /// RSA2 密钥长度至少为2048
        /// SHA256
        /// </summary>
        // ReSharper disable once InconsistentNaming
        RSA2
    }
}