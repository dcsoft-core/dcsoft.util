using System.Collections.Generic;
using System.Linq;
using Util.Helpers;

namespace Util.Extras.Tools.IPLocation
{
    // ReSharper disable once InconsistentNaming
    /// <summary>
    /// IP归属地查询类
    /// </summary>
    public static class IPSearch
    {
        /// <summary>
        /// 获取归属地信息
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public static string GetLocation(string ip)
        {
            if (ip.IsEmpty())
                return string.Empty;

            ip = ip.Replace("::ffff:", "");
            // 某些IP库包含有广告信息，显示的时候会过滤以下的广告信息
            var filterCountry = new Dictionary<int, string>
            {
                { 1, "CZ88.NET" },
                //{ 2, "未获得IP"},
                //{ 3, "同一个局域网内"},
                //{ 4, "局域网"}
            };

            //当地区信息包含以下内容时，被忽略
            var filterArea = new Dictionary<int, string>
            {
                { 1, "电信" },
                { 2, "移动" },
                { 3, "联通" },
                { 4, "宽带" },
                { 5, "有线通" },
                { 6, "铁通" }
            };

            // TODO 设置IP数据库路径
            var ipfilePath = Web.GetPhysicalPath("App_Data/qqwry.dat");
            var qqWry = new IPScanner(ipfilePath);

            var ipLocation = qqWry.Query(ip);
            var country = ipLocation.Country;
            var local = ipLocation.Local;
            country = filterCountry.Aggregate(country, (current, item) => current.Replace(item.Value, string.Empty));
            local = filterArea.Aggregate(local, (current, item) => current.Replace(item.Value, string.Empty));
            var address = $"{country}{local}";
            if (address.Length > 128)
            {
                address = address.Substring(0, 127);
            }

            return address;
        }
    }
}