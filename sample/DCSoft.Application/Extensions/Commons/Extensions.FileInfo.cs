using System;
using System.Collections.Generic;
using DCSoft.Applications.Dtos.Commons;
using Util.Extensions;
using Util.Files;
using Util.Helpers;

namespace DCSoft.Applications.Extensions.Commons
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
            //ContentType="image/png"
            //Extension="png"
            //FileDirectory="D:/upload/admin/avatar/admin"
            //FileName="icon.png"
            //FilePath="D:/upload/admin/ avatar/admin/1907e963b21049f2a5f5b53e4e32253a.png"
            //FileRelativePath="admin/1907e963b21049f2a5f5b53e4e32253a.png"
            //FileRequestPath="/upload/admin/avatar/admin/1907e963b21049f2a5f5b53e4e32253a.png"
            //RelativePath="admin"
            //RequestPath="/upload/admin/ avatar"
            //SaveName="1907e963b21049f2a5f5b53e4e32253a.png"
            //Size={ 18.13 KB}
            //UploadPath="D:/upload/admin/avatar"

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

        /// <summary>
        /// 转换为附件信息参数
        /// </summary>
        /// <param name="file">附件信息实体</param>
        /// <param name="objId">对象标识</param>
        /// <param name="objType">对象类型</param>
        public static AttachmentDto ToAttachmentDto(this UploadFileInfo file, Guid objId, string objType)
        {
            var result = new AttachmentDto();
            if (file == null)
                return result;
            result.Id = file.Id;
            result.ObjectId = objId;
            result.ObjectType = objType;
            result.TypeCode = file.TypeCode;
            result.TypeName = file.TypeName;
            result.ActualName = file.Name;
            result.MimeType = file.Type;
            result.FileSize = (int)file.Size;
            result.ExtensionName = file.ExtensionName;
            result.FileName = file.FileName;
            result.FilePath = file.FilePath;
            result.RequestPath = file.Url;
            return result;
        }
        /// <summary>
        /// 转换为附件信息实体
        /// </summary>
        /// <param name="list">附件信息参数</param>
        public static List<UploadFileInfo> ToUploadFileInfo(this List<AttachmentDto> list)
        {
            var result = new List<UploadFileInfo>();
            if (list == null)
                return result;
            list.ForEach(item =>
            {
                result.Add(new UploadFileInfo()
                {
                    Id = item.Id,
                    TypeCode = item.TypeCode,
                    TypeName = item.TypeName,
                    Name = item.FileName,
                    FileName = item.ActualName,
                    ExtensionName = item.ExtensionName,
                    Size = item.FileSize.ToLong(),
                    Type = item.MimeType,
                    Url = item.RequestPath,
                    FilePath = item.FilePath
                });
            });
            return result;
        }
    }
}