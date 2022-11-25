namespace DCSoft.Applications.Responses.Systems.NgAlain
{
    /// <summary>
    /// NgAlain用户信息
    /// </summary>
    public class UserInfo
    {
        /// <summary>
        /// 用户标识
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        public string Avatar { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// 电子邮件
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 关系标识
        /// </summary>
        public string RelatedId { get; set; }

        /// <summary>
        /// 关系标识名称
        /// </summary>
        public string RelatedName { get; set; }
    }
}