namespace DCSoft.Applications.Responses.Commons
{
    /// <summary>
    /// 字典数据参数
    /// </summary>
    public class DictDataResponse
    {
        /// <summary>
        /// 标识
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 标识
        /// </summary>
        public string Key => Id;

        /// <summary>
        /// 文本
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// 文本
        /// </summary>
        public string Title => Text;

        /// <summary>
        /// 值
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// 启用
        /// </summary>
        public bool Disabled { get; set; }
    }
}