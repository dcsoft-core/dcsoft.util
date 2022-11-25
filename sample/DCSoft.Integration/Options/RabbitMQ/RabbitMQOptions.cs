// ReSharper disable UnusedMember.Global
// ReSharper disable InconsistentNaming

namespace DCSoft.Integration.Options.RabbitMQ
{
    /// <summary>
    /// 消息队列配置
    /// </summary>
    public class RabbitMQOptions
    {
        /// <summary>
        /// 主机
        /// </summary>
        public string HostName { get; set; }

        /// <summary>
        /// 端口
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 交换名称
        /// </summary>
        public string ExchangeName { get; set; }

        /// <summary>
        /// 成功数据保存有效期
        /// </summary>
        public int ExpiredAt { get; set; }

        /// <summary>
        /// 失败重试次数
        /// </summary>
        public int FailedRetryCount { get; set; }
    }
}