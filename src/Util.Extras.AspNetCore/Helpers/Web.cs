using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Web;
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
                if (length != null && length > 0)
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
            return Util.Helpers.Web.Request?.Headers["HTTP_X_FORWARDED_FOR"] ?? Util.Helpers.Web.Request?.Headers["REMOTE_ADDR"];
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

        #region GetFiles(获取客户端文件集合)

        /// <summary>
        /// 获取客户端文件集合
        /// </summary>
        public static List<IFormFile> GetFiles()
        {
            var result = new List<IFormFile>();
            var files = Util.Helpers.Web.Request.Form.Files;
            if (files.Count == 0)
                return result;
            result.AddRange(files.Where(file => file?.Length > 0));
            return result;
        }

        #endregion

        #region GetFile(获取客户端文件)

        /// <summary>
        /// 获取客户端文件
        /// </summary>
        public static IFormFile GetFile()
        {
            var files = GetFiles();
            return files.Count == 0 ? null : files[0];
        }

        #endregion

        #region GetParam(获取请求参数)

        /// <summary>
        /// 获取请求参数，搜索路径：查询参数->表单参数->请求头
        /// </summary>
        /// <param name="name">参数名</param>
        public static string GetParam(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return string.Empty;
            if (Util.Helpers.Web.Request == null)
                return string.Empty;
            string result = Util.Helpers.Web.Request.Query[name];
            if (string.IsNullOrWhiteSpace(result) == false)
                return result;
            result = Util.Helpers.Web.Request.Form[name];
            if (string.IsNullOrWhiteSpace(result) == false)
                return result;
            result = Util.Helpers.Web.Request.Headers[name];
            return result;
        }

        #endregion

        #region UrlEncode(Url编码)

        /// <summary>
        /// Url编码
        /// </summary>
        /// <param name="url">url</param>
        /// <param name="isUpper">编码字符是否转成大写,范例,"http://"转成"http%3A%2F%2F"</param>
        public static string UrlEncode(string url, bool isUpper = false)
        {
            return UrlEncode(url, Encoding.UTF8, isUpper);
        }

        /// <summary>
        /// Url编码
        /// </summary>
        /// <param name="url">url</param>
        /// <param name="encoding">字符编码</param>
        /// <param name="isUpper">编码字符是否转成大写,范例,"http://"转成"http%3A%2F%2F"</param>
        public static string UrlEncode(string url, string encoding, bool isUpper = false)
        {
            encoding = string.IsNullOrWhiteSpace(encoding) ? "UTF-8" : encoding;
            return UrlEncode(url, Encoding.GetEncoding(encoding), isUpper);
        }

        /// <summary>
        /// Url编码
        /// </summary>
        /// <param name="url">url</param>
        /// <param name="encoding">字符编码</param>
        /// <param name="isUpper">编码字符是否转成大写,范例,"http://"转成"http%3A%2F%2F"</param>
        public static string UrlEncode(string url, Encoding encoding, bool isUpper = false)
        {
            var result = HttpUtility.UrlEncode(url, encoding);
            if (isUpper == false)
                return result;
            return GetUpperEncode(result);
        }

        /// <summary>
        /// 获取大写编码字符串
        /// </summary>
        private static string GetUpperEncode(string encode)
        {
            var result = new StringBuilder();
            int index = int.MinValue;
            for (int i = 0; i < encode.Length; i++)
            {
                string character = encode[i].ToString();
                if (character == "%")
                    index = i;
                if (i - index == 1 || i - index == 2)
                    character = character.ToUpper();
                result.Append(character);
            }

            return result.ToString();
        }

        #endregion

        #region UrlDecode(Url解码)

        /// <summary>
        /// Url解码
        /// </summary>
        /// <param name="url">url</param>
        public static string UrlDecode(string url)
        {
            return HttpUtility.UrlDecode(url);
        }

        /// <summary>
        /// Url解码
        /// </summary>
        /// <param name="url">url</param>
        /// <param name="encoding">字符编码</param>
        public static string UrlDecode(string url, Encoding encoding)
        {
            return HttpUtility.UrlDecode(url, encoding);
        }

        #endregion

        #region DownloadAsync(下载)

        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="filePath">文件绝对路径</param>
        /// <param name="fileName">文件名,包含扩展名</param>
        public static async Task DownloadFileAsync(string filePath, string fileName)
        {
            await DownloadFileAsync(filePath, fileName, Encoding.UTF8);
        }

        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="filePath">文件绝对路径</param>
        /// <param name="fileName">文件名,包含扩展名</param>
        /// <param name="encoding">字符编码</param>
        public static async Task DownloadFileAsync(string filePath, string fileName, Encoding encoding)
        {
            var bytes = File.Read(filePath);
            await DownloadAsync(bytes, fileName, encoding);
        }

        /// <summary>
        /// 下载
        /// </summary>
        /// <param name="stream">流</param>
        /// <param name="fileName">文件名,包含扩展名</param>
        public static async Task DownloadAsync(Stream stream, string fileName)
        {
            await DownloadAsync(stream, fileName, Encoding.UTF8);
        }

        /// <summary>
        /// 下载
        /// </summary>
        /// <param name="stream">流</param>
        /// <param name="fileName">文件名,包含扩展名</param>
        /// <param name="encoding">字符编码</param>
        public static async Task DownloadAsync(Stream stream, string fileName, Encoding encoding)
        {
            await DownloadAsync(await File.ToBytesAsync(stream), fileName, encoding);
        }

        /// <summary>
        /// 下载
        /// </summary>
        /// <param name="bytes">字节流</param>
        /// <param name="fileName">文件名,包含扩展名</param>
        public static async Task DownloadAsync(byte[] bytes, string fileName)
        {
            await DownloadAsync(bytes, fileName, Encoding.UTF8);
        }

        /// <summary>
        /// 下载
        /// </summary>
        /// <param name="bytes">字节流</param>
        /// <param name="fileName">文件名,包含扩展名</param>
        /// <param name="encoding">字符编码</param>
        public static async Task DownloadAsync(byte[] bytes, string fileName, Encoding encoding)
        {
            if (bytes == null || bytes.Length == 0)
                return;
            fileName = fileName.Replace(" ", "");
            fileName = UrlEncode(fileName, encoding);
            Util.Helpers.Web.Response.ContentType = "application/octet-stream";
            Util.Helpers.Web.Response.Headers.Add("Content-Disposition", $"attachment; filename={fileName}");
            Util.Helpers.Web.Response.Headers.Add("Content-Length", bytes.Length.ToString());
            await Util.Helpers.Web.Response.Body.WriteAsync(bytes, 0, bytes.Length);
        }

        #endregion
    }
}
