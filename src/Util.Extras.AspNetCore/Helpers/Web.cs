using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Util.Helpers;

namespace Util.Extras.Helpers
{
    /// <summary>
    /// Web操作
    /// </summary>
    public static class Web
    {
        #region AccessToken(获取访问令牌)

        /// <summary>
        /// 获取访问令牌
        /// </summary>
        public static string AccessToken
        {
            get
            {
                var authorization = Util.Helpers.Web.Request?.Headers["Authorization"].SafeString();
                if (string.IsNullOrWhiteSpace(authorization))
                    return null;
                var list = authorization.Split(' ');
                if (list.Length == 2)
                    return list[1];
                return null;
            }
        }

        #endregion

        #region Body(请求正文)

        /// <summary>
        /// 请求正文
        /// </summary>
        public static string Body
        {
            get
            {
                string body = "";
                // 获取请求参数
                Util.Helpers.Web.Request.EnableBuffering();
                var stream = Util.Helpers.Web.Request.Body;
                long? length = Util.Helpers.Web.Request.ContentLength;
                Util.Helpers.Web.Request.Body.Position = 0;
                if (length is > 0)
                {
                    // 使用这个方式读取，并且使用异步
                    var streamReader = new StreamReader(stream, Encoding.UTF8);
                    body = streamReader.ReadToEndAsync().GetAwaiter().GetResult();
                }

                Util.Helpers.Web.Request.Body.Position = 0;
                return body;
            }
        }

        #endregion

        #region RespBody(返回正文)

        /// <summary>
        /// 返回正文
        /// </summary>
        public static string RespBody
        {
            get
            {
                using var reader = new StreamReader(Util.Helpers.Web.Response.Body);
                Util.Helpers.Web.Response.Body.Seek(0, SeekOrigin.Begin);
                var body = reader.ReadToEnd();

                return body;
            }
        }

        #endregion

        #region GetBodyAsync(获取请求正文)

        /// <summary>
        /// 获取请求正文
        /// </summary>
        public static async Task<string> GetBodyAsync()
        {
            Util.Helpers.Web.Request.EnableBuffering();
            return await File.ToStringAsync(Util.Helpers.Web.Request.Body, isCloseStream: false);
        }

        #endregion

        #region Ip(客户端Ip地址)

        /// <summary>
        /// Ip地址
        /// </summary>
        private static string _ip;

        /// <summary>
        /// 设置Ip地址
        /// </summary>
        /// <param name="ip">Ip地址</param>
        public static void SetIp(string ip)
        {
            _ip = ip;
        }

        /// <summary>
        /// 重置Ip地址
        /// </summary>
        public static void ResetIp()
        {
            _ip = null;
        }

        /// <summary>
        /// 客户端Ip地址
        /// </summary>
        public static string Ip
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_ip) == false)
                    return _ip;
                var list = new[] { "127.0.0.1", "::1" };
                var result = GetRemoteIpAddress();
                if (string.IsNullOrWhiteSpace(result) || list.Contains(result))
                    result = Common.IsWindows ? GetLanIp() : GetLanIp(NetworkInterfaceType.Ethernet);

                result = result.Replace("::ffff:", "");
                return result;
            }
        }

        /// <summary>
        /// 获取远程用户IP
        /// </summary>
        /// <returns></returns>
        private static string GetRemoteIpAddress()
        {
            var ip = Util.Helpers.Web.HttpContext.Request.Headers["X-Real-IP"].FirstOrDefault();
            if (!ip.IsEmpty()) return ip;
            ip = Util.Helpers.Web.HttpContext.Request.Headers["X-Forwarded-For"].FirstOrDefault();
            if (ip.IsEmpty())
            {
                ip = Util.Helpers.Web.HttpContext?.Connection.RemoteIpAddress.SafeString();
            }

            return ip.SafeString();
        }

        /// <summary>
        /// 获取局域网IP
        /// </summary>
        private static string GetLanIp()
        {
            foreach (var hostAddress in Dns.GetHostAddresses(Dns.GetHostName()))
            {
                if (hostAddress.AddressFamily == AddressFamily.InterNetwork)
                    return hostAddress.ToString();
            }

            return string.Empty;
        }

        /// <summary>
        /// 获取局域网IP
        /// </summary>
        /// <param name="type">网络接口类型</param>
        private static string GetLanIp(NetworkInterfaceType type)
        {
            try
            {
                foreach (var item in NetworkInterface.GetAllNetworkInterfaces())
                {
                    if (item.NetworkInterfaceType != type || item.OperationalStatus != OperationalStatus.Up)
                        continue;
                    var ipProperties = item.GetIPProperties();
                    if (ipProperties.GatewayAddresses.FirstOrDefault() == null)
                        continue;
                    foreach (var ip in ipProperties.UnicastAddresses)
                    {
                        if (ip.Address.AddressFamily == AddressFamily.InterNetwork)
                        {
                            var ipAddr = ip.Address.ToString();
                            ipAddr = ipAddr.Replace("::ffff:", "");
                            return ipAddr;
                        }
                    }
                }
            }
            catch
            {
                return string.Empty;
            }

            return string.Empty;
        }

        #endregion

        #region Host(主机)

        /// <summary>
        /// 主机
        /// </summary>
        public static string Host => Util.Helpers.Web.HttpContext == null ? Dns.GetHostName() : GetClientHostName();

        /// <summary>
        /// 获取Web客户端主机名
        /// </summary>
        private static string GetClientHostName()
        {
            var address = GetRemoteAddress();
            if (string.IsNullOrWhiteSpace(address))
                return Dns.GetHostName();
            var result = Dns.GetHostEntry(IPAddress.Parse(address)).HostName;
            if (result == "localhost.localdomain")
                result = Dns.GetHostName();
            return result;
        }

        /// <summary>
        /// 获取远程地址
        /// </summary>
        private static string GetRemoteAddress()
        {
            return Util.Helpers.Web.Request?.Headers["HTTP_X_FORWARDED_FOR"] ??
                   Util.Helpers.Web.Request?.Headers["REMOTE_ADDR"];
        }

        #endregion

        #region Browser(浏览器)

        /// <summary>
        /// 浏览器
        /// </summary>
        public static string Browser => Util.Helpers.Web.Request?.Headers["User-Agent"];

        #endregion

        #region 获取头部信息

        /// <summary>
        /// 获取头部信息
        /// </summary>
        public static string Header(string key) => Util.Helpers.Web.Request?.Headers[key];

        #endregion

        #region RootPath(根路径)

        /// <summary>
        /// 根路径
        /// </summary>
        public static string RootPath => Util.Helpers.Web.Environment?.ContentRootPath;

        #endregion

        #region WebRootPath(Web根路径)

        /// <summary>
        /// Web根路径，即wwwroot
        /// </summary>
        public static string WebRootPath => Util.Helpers.Web.Environment?.WebRootPath;

        #endregion

        #region 获取wwwroot路径

        /// <summary>
        /// 获取wwwroot路径
        /// </summary>
        /// <param name="relativePath">相对路径</param>
        public static string GetWebRootPath(string relativePath)
        {
            if (string.IsNullOrWhiteSpace(relativePath))
                return string.Empty;
            var rootPath = WebRootPath;
            if (string.IsNullOrWhiteSpace(rootPath))
                return Path.GetFullPath(relativePath);
            return Common.IsWindows
                ? $"{WebRootPath}\\{relativePath.Replace("/", "\\").TrimStart('\\')}"
                : $"{WebRootPath}/{relativePath.Replace("\\", "/").TrimStart('/')}";
        }

        #endregion
    }
}