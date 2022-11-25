using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Util;
using Util.Exceptions;
using Util.Extensions;
using Util.Helpers;
using Util.Text;

namespace DCSoft.Integration.Upload
{
    /// <summary>
    /// 文件上传帮助类
    /// </summary>
    public class UploadHelper : Util.Dependency.ISingletonDependency
    {
        /// <summary>
        /// 上传单文件
        /// </summary>
        /// <param name="file"></param>
        /// <param name="config"></param>
        /// <param name="args"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<Util.Files.FileInfo> UploadAsync(IFormFile file, FileUploadOptions config, object args, CancellationToken cancellationToken = default)
        {
            if (file == null || file.Length < 1)
            {
                throw new Warning("请上传文件！");
            }

            //格式限制
            if (!config.ContentType.Contains(file.ContentType))
            {
                throw new Warning("文件格式错误");
            }

            //大小限制
            if (!(file.Length <= config.MaxSize))
            {
                throw new Warning("文件过大");
            }

            var fileInfo = new Util.Files.FileInfo(file.FileName, file.Length)
            {
                UploadPath = config.UploadPath,
                RequestPath = config.RequestPath
            };

            var dateTimeFormat = config.DateTimeFormat.IsEmpty() ? DateTime.Now.ToString(config.DateTimeFormat) : "";
            var format = config.Format.NotEmpty() ? string.Format(config.Format, args) : "";
            fileInfo.RelativePath = Path.Combine(dateTimeFormat, format).ToPath();

            if (!Directory.Exists(fileInfo.FileDirectory))
            {
                Directory.CreateDirectory(fileInfo.FileDirectory);
            }

            fileInfo.ContentType = file.ContentType;

            fileInfo.SaveName = $"{Id.Create()}.{fileInfo.Extension}";

            await SaveAsync(file, fileInfo.FilePath, cancellationToken);

            return fileInfo;
        }

        /// <summary>
        /// 保存文件
        /// </summary>
        /// <param name="file"></param>
        /// <param name="filePath"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task SaveAsync(IFormFile file, string filePath, CancellationToken cancellationToken = default)
        {
            await using var stream = new FileStream(filePath, FileMode.Create);
            await file.CopyToAsync(stream, cancellationToken);
        }
    }
}
