namespace DCSoft.Applications.Requests.Commons
{
    /// <summary>
    /// 保存系统参数
    /// </summary>
    public class SaveParamRequest
    {
        /// <summary>
        /// 参数名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 参数值
        /// </summary>
        public string Value { get; set; }
    }
}