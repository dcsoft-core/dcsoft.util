using DCSoft.Apis.Base;
using DCSoft.Applications.Extensions.Commons;
using DCSoft.Applications.Services.Abstractions.Commons;
using DCSoft.Data.Queries.Commons;
using DCSoft.Integration.Upload;
using DCSoft.Logging.Serilog;
using DCSoft.Web.Core.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Util;
using Util.Exceptions;
using Util.Extras.Sessions;
using ILogger = DCSoft.Logging.Serilog.ILogger;

namespace DCSoft.Apis.Admin.Commons
{
    /// <summary>
    /// 公共附件控制器
    /// </summary>
    [Router("commons", "attachment")]
    public class AttachmentController : BaseController
    {
        #region 初始化控制器

        /// <summary>
        /// 初始化控制器
        /// </summary>
        /// <param name="logger">日志服务</param>
        /// <param name="attachmentService">附件服务</param>
        /// <param name="uploadConfig">上传配置</param>
        /// <param name="uploadHelper">上传帮助</param>
        public AttachmentController(ILogger logger,
            IAttachmentService attachmentService,
            IOptionsMonitor<UploadOptions> uploadConfig,
            UploadHelper uploadHelper)
        {
            _logger = logger;
            _attachmentService = attachmentService;
            _uploadConfig = uploadConfig.CurrentValue;
            _uploadHelper = uploadHelper;
        }

        #endregion

        #region 属性定义

        /// <summary>
        /// 日志操作者
        /// </summary>
        private readonly ILogger _logger;

        /// <summary>
        /// 附件服务
        /// </summary>
        private readonly IAttachmentService _attachmentService;

        /// <summary>
        /// 上传配置
        /// </summary>
        private readonly UploadOptions _uploadConfig;

        /// <summary>
        /// 上传帮助
        /// </summary>
        private readonly UploadHelper _uploadHelper;

        #endregion

        #region 上传附件

        /// <summary>
        /// 上传附件
        /// </summary>
        /// <returns></returns>
        [HttpPost("upload")]
        [Login]
        public async Task<IActionResult> UploadAsync()
        {
            var objId = Util.Extras.Helpers.Web.GetParam("objId");
            var objType = Util.Extras.Helpers.Web.GetParam("objType");
            var typeCode = Util.Extras.Helpers.Web.GetParam("typeCode");
            var typeName = Util.Extras.Helpers.Web.GetParam("typeName");
            var file = Util.Extras.Helpers.Web.GetFile();
            if (file == null)
            {
                throw new Warning("上传文件有误");
            }

            var config = _uploadConfig.Document;
            var fileInfo = await _uploadHelper.UploadAsync(file, config, new { Id = $"{Session.GetUserName()}" });

            var result = fileInfo.ToUploadFileInfo(typeCode, typeName);
            var dto = result.ToAttachmentDto(objId.ToGuid(), objType);
            await _attachmentService.CreateAsync(dto);
            _logger.Operate("上传附件", BusinessType.Upload, CurrentMethodName);

            return Success(result);
        }

        #endregion

        #region 下载附件

        /// <summary>
        /// 下载附件
        /// </summary>
        /// <returns></returns>
        [HttpPost("download/{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> DownloadAsync(string id)
        {
            var dto = await _attachmentService.GetByIdAsync(id);
            await Util.Extras.Helpers.Web.DownloadFileAsync(dto.FilePath, dto.ActualName);
            return Success();
        }

        #endregion

        #region 查询附件

        /// <summary>
        /// 查询附件
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        [Login]
        public async Task<IActionResult> QueryAsync([FromQuery] AttachmentQuery query)
        {
            var list = await _attachmentService.ListQueryAsync(query);
            var result = list.ToUploadFileInfo();
            return Success(result);
        }

        #endregion
    }
}