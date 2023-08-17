using System.Text.Json.Serialization;

namespace DCSoft.Applications.Dtos.Commons
{
    /// <summary>
    /// 上传文件信息
    /// </summary>
    public class UploadFileInfo
    {
        /// <summary>
        /// 标识
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 文件名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 文件名
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// 文件大小
        /// </summary>
        public long Size { get; set; }

        /// <summary>
        /// 文件类型
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 文件路径
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 扩展名
        /// </summary>
        public string ExtensionName { get; set; }

        /// <summary>
        /// 类型编号
        /// </summary>
        public string TypeCode { get; set; }

        /// <summary>
        /// 类型名称
        /// </summary>
        public string TypeName { get; set; }

        /// <summary>
        /// 文件路径
        /// </summary>
        [JsonIgnore]
        public string FilePath { get; set; }
    }
}