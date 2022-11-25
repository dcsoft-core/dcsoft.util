using System.ComponentModel.DataAnnotations.Schema;
using Util;
using Util.Helpers;
using Util.Security;
using Util.Security.Encryptors;
using Util.Utils;

namespace DCSoft.Domain.Models.Systems
{
    /// <summary>
    /// 用户
    /// </summary>
    public partial class User
    {
        /// <summary>
        /// 加密器
        /// </summary>
        [NotMapped]
        public IEncryptor Encryptor { get; set; }

        /// <summary>
        /// 初始化
        /// </summary>
        public override void Init()
        {
            base.Init();
            InitUserName();
            AccessFailedCount = 0;
            LoginCount = 0;
            RegisterIp = Web.Ip;
        }

        /// <summary>
        /// 初始化用户名
        /// </summary>
        private void InitUserName()
        {
            NormalizedUserName = UserName.ToUpper();
            NormalizedEmail = Email?.ToUpper();
        }

        /// <summary>
        /// 设置密码
        /// </summary>
        /// <param name="password">密码</param>
        public void SetPasswordHash(string password)
        {
            SecurityStamp = Encrypt.Md5By32(Util.Helpers.Id.Create()).ToUpper();
            PasswordHash = Encrypt.Base64Encrypt(Encrypt.HmacSha256(Encrypt.AesEncrypt(password), SecurityStamp));
        }

        /// <summary>
        /// 设置密码
        /// </summary>
        /// <param name="password">密码</param>
        /// <param name="storeOriginalPassword">是否存储原始密码</param>
        public void SetPassword(string password, bool? storeOriginalPassword)
        {
            if (storeOriginalPassword.SafeValue())
            {
                Password = GetEncryptor().Encrypt(password);
                return;
            }

            Password = null;
        }

        /// <summary>
        /// 获取加密器
        /// </summary>
        protected virtual IEncryptor GetEncryptor()
        {
            return Encryptor ?? new PasswordEncryptor();
        }

        /// <summary>
        /// 设置安全码
        /// </summary>
        /// <param name="password">安全码</param>
        /// <param name="storeOriginalPassword">是否存储原始密码</param>
        public void SetSafePassword(string password, bool? storeOriginalPassword)
        {
            if (storeOriginalPassword.SafeValue())
            {
                SafePassword = GetEncryptor().Encrypt(password);
                return;
            }

            SafePassword = null;
        }

        /// <summary>
        /// 获取密码
        /// </summary>
        public string GetPassword()
        {
            return GetEncryptor().Decrypt(Password);
        }

        /// <summary>
        /// 获取安全码
        /// </summary>
        public string GetSafePassword()
        {
            return GetEncryptor().Decrypt(SafePassword);
        }
    }
}