namespace Util.Extras.Tools.IPLocation
{
    // ReSharper disable once InconsistentNaming
    /// <summary>
    /// IP归属地信息
    /// </summary>
    public class IPLocation
    {
        // ReSharper disable once InconsistentNaming
        /// <summary>
        /// IP地址
        /// </summary>
        public string IP { get; set; }

        /// <summary>
        /// 国家
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// 地区
        /// </summary>
        public string Local { get; set; }
    }
}