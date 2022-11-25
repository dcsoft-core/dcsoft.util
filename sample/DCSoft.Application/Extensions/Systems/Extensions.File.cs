using Util.Files;
using Util.Helpers;

namespace DCSoft.Applications.Extensions.Systems
{
    /// <summary>
    /// 上传参数扩展
    /// </summary>
    public static class FileExtension
    {
        /// <summary>
        /// 转换为附件信息实体
        /// </summary>
        /// <param name="file">附件信息参数</param>
        /// <param name="typeCode"></param>
        /// <param name="typeName"></param>
        public static UploadFileInfo ToUploadFileInfo(this FileInfo file, string typeCode, string typeName)
        {
            var result = new UploadFileInfo();
            if (file == null)
                return result;

            result.TypeCode = typeCode;
            result.TypeName = typeName;
            result.Id = Id.CreateGuid().ToString();
            result.Name = file.FileName;
            result.FileName = file.SaveName;
            result.ExtensionName = $".{file.Extension}";
            result.Size = file.Size.Size;
            result.Type = file.ContentType;
            result.Url = file.FileRequestPath;
            result.FilePath = file.FilePath; // 实际路径
            return result;
        }
    }
}