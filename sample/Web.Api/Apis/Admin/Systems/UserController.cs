using DCSoft.Apis.Base;
using DCSoft.Applications.Extensions.Systems;
using DCSoft.Applications.Requests.Systems;
using DCSoft.Applications.Responses.Systems;
using DCSoft.Applications.Responses.Systems.NgAlain;
using DCSoft.Applications.Services.Abstractions.Systems;
using DCSoft.Data.Queries.Systems;
using DCSoft.Integration.Upload;
using DCSoft.Logging.Serilog;
using DCSoft.Web.Core.Attributes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Util;
using Util.Exceptions;
using Util.Extras.Applications.Properties;
using Util.Extras.Sessions;
using Util.Sessions;
using ILogger = DCSoft.Logging.Serilog.ILogger;

namespace DCSoft.Apis.Admin.Systems
{
    /// <summary>
    /// 用户控制器
    /// </summary>
    [Router("systems", "user")]
    public class UserController : BaseController
    {
        #region 初始化控制器

        /// <summary>
        /// 初始化控制器
        /// </summary>
        /// <param name="logger">日志服务</param>
        /// <param name="userService">用户服务</param>
        /// <param name="menuService">安全服务</param>
        /// <param name="uploadConfig">上传配置</param>
        /// <param name="uploadHelper">上传帮助</param>
        public UserController(ILogger logger,
            IUserService userService,
            IMenuService menuService,
            IOptionsMonitor<UploadOptions> uploadConfig,
            UploadHelper uploadHelper)
        {
            _logger = logger;
            _userService = userService;
            _menuService = menuService;
            _uploadOptions = uploadConfig.CurrentValue;
            _uploadHelper = uploadHelper;
        }

        #endregion

        #region 属性定义

        /// <summary>
        /// 用户服务
        /// </summary>
        private readonly IUserService _userService;

        /// <summary>
        /// 菜单服务
        /// </summary>
        private readonly IMenuService _menuService;

        /// <summary>
        /// 日志操作者
        /// </summary>
        private readonly ILogger _logger;

        /// <summary>
        /// 上传配置
        /// </summary>
        private readonly UploadOptions _uploadOptions;

        /// <summary>
        /// 上传帮助
        /// </summary>
        private readonly UploadHelper _uploadHelper;

        #endregion

        #region 获取应用程序数据

        /// <summary>
        /// 获取应用程序数据
        /// </summary>
        [HttpGet("app-data")]
        [Login]
        public async Task<IActionResult> GetAppDataAsync()
        {
            var menus = await _menuService.GetMenusAsync();
            var user = await _userService.GetByIdAsync(Session.UserId);
            var data = new AppData
            {
                App = { Name = Session.GetApplicationName(), Description = Session.GetApplicationName() },
                User =
                {
                    UserId = Session.UserId,
                    Name = user.UserName,
                    NickName = user.NickName,
                    Avatar = user.Avatar.IsEmpty() ? "/assets/img/avatar.jpg" : user.Avatar,
                    Email = user.Email,
                },
                Menu = menus.ToNgAlainMenus()
            };
            return Success(data);
        }

        /// <summary>
        /// 获取应用程序菜单
        /// </summary>
        [HttpGet("app-menu")]
        [Login]
        public async Task<IActionResult> GetAppMenuAsync()
        {
            var menus = await _menuService.GetMenusAsync();
            //Menu = menus.ToNgAlainMenus();
            return Success(menus);
        }

        #endregion

        #region 创建

        /// <summary>
        /// 创建
        /// </summary>
        /// <remarks>
        /// 调用范例:
        /// POST
        /// /api/customer
        /// </remarks>
        /// <param name="request">创建参数</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CreateUserRequest request)
        {
            if (request == null)
                throw new Warning(AppRes.CreateRequestIsEmpty);
            var id = await _userService.CreateAsync(request);
            _logger.Operate("用户", BusinessType.Insert, CurrentMethodName);
            return Success(id);
        }

        #endregion

        #region 详情

        /// <summary>
        /// 详情
        /// </summary>
        /// <param name="id">标识</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(string id)
        {
            if (id.IsEmpty())
                throw new Warning(AppRes.IdIsEmpty);
            var result = await _userService.GetByIdAsync(id);
            return Success(result);
        }

        #endregion

        #region 基本信息

        /// <summary>
        /// 基本信息
        /// </summary>
        /// <returns></returns>
        [HttpGet("base")]
        [Login]
        public async Task<IActionResult> GetBaseByIdAsync()
        {
            var userDto = await _userService.GetByIdAsync(Session.UserId);
            var result = userDto.MapTo<UserBaseResponse>();
            return Success(result);
        }

        #endregion

        #region 修改基本信息

        /// <summary>
        /// 修改基本信息
        /// </summary>
        /// <param name="request">修改参数</param>
        /// <returns></returns>
        [HttpPut("base")]
        [Login]
        public async Task<IActionResult> UpdateBaseAsync([FromBody] UpdateUserBaseRequest request)
        {
            if (request == null)
                throw new Warning(AppRes.UpdateRequestIsEmpty);
            var result = await _userService.UpdateBaseAsync(request);
            _logger.Operate("用户", BusinessType.Update, CurrentMethodName);
            return Success(result);
        }

        #endregion

        #region 修改头像

        /// <summary>
        /// 修改头像
        /// </summary>
        /// <returns></returns>
        [HttpPost("avatar")]
        [Login]
        public async Task<IActionResult> UpdateAvatarAsync()
        {
            var file = Util.Helpers.Web.GetFile();
            if (file == null)
            {
                throw new Warning("上传文件有误");
            }

            var config = _uploadOptions.Avatar;
            var fileInfo = await _uploadHelper.UploadAsync(file, config, new { Id = $"{Session.GetUserName()}" });

            var result = fileInfo.ToUploadFileInfo("01", "头像");
            await _userService.UpdateAvatarAsync(Session.UserId.ToGuid(), result.Url);
            _logger.Operate("用户", BusinessType.Avatar, CurrentMethodName);
            return Success(result);
        }

        #endregion

        #region 修改

        /// <summary>
        /// 修改
        /// </summary>
        /// <remarks>
        /// 调用范例:
        /// PUT
        /// /api/customer/1
        /// </remarks>
        /// <param name="request">修改参数</param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] UpdateUserRequest request)
        {
            if (request == null)
                throw new Warning(AppRes.UpdateRequestIsEmpty);
            await _userService.UpdateAsync(request);
            _logger.Operate("用户", BusinessType.Update, CurrentMethodName);
            return Success();
        }

        #endregion

        #region 删除

        /// <summary>
        /// 删除，注意：该方法用于删除单个实体，批量删除请使用POST提交，否则可能失败
        /// </summary>
        /// <remarks>
        /// 调用范例:
        /// DELETE
        /// /api/customer/1
        /// </remarks>
        /// <param name="id">标识</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            if (id.IsEmpty())
                throw new Warning(AppRes.IdIsEmpty);
            await _userService.DeleteAsync(id);
            _logger.Operate("用户", BusinessType.Delete, CurrentMethodName);
            return Success();
        }

        #endregion

        #region 启用

        /// <summary>
        /// 启用
        /// </summary>
        /// <remarks>
        /// 调用范例:
        /// PUT
        /// /api/user/1
        /// </remarks>
        /// <param name="id">标识</param>
        /// <returns></returns>
        [HttpPut("enable/{id}")]
        public async Task<IActionResult> EnableAsync(string id)
        {
            if (id.IsEmpty())
                throw new Warning(AppRes.IdIsEmpty);
            var userId = await _userService.EnableAsync(id.ToGuid(), true);
            _logger.Operate("用户", BusinessType.Enable, CurrentMethodName);
            return Success(userId);
        }

        /// <summary>
        /// 锁定
        /// </summary>
        /// <remarks>
        /// 调用范例:
        /// PUT
        /// /api/user/1
        /// </remarks>
        /// <param name="id">标识</param>
        /// <returns></returns>
        [HttpPut("disable/{id}")]
        public async Task<IActionResult> DisabledAsync(string id)
        {
            if (id.IsEmpty())
                throw new Warning(AppRes.IdIsEmpty);
            var userId = await _userService.EnableAsync(id.ToGuid(), false);
            _logger.Operate("用户", BusinessType.Disable, CurrentMethodName);
            return Success(userId);
        }

        #endregion

        #region 重置密码

        /// <summary>
        /// 重置密码
        /// </summary>
        /// <remarks>
        /// 调用范例:
        /// PUT
        /// /api/user/1
        /// </remarks>
        /// <param name="id">标识</param>
        /// <returns></returns>
        [HttpPut("pwd/{id}")]
        public async Task<IActionResult> ResetPwdAsync(string id)
        {
            if (id.IsEmpty())
                throw new Warning(AppRes.IdIsEmpty);
            var ret = await _userService.ResetPasswordAsync(id.ToGuid());
            _logger.Operate("用户", BusinessType.ResetPwd, CurrentMethodName);
            return Success(ret);
        }

        #endregion

        #region 修改密码

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <remarks>
        /// 调用范例:
        /// PUT
        /// /api/user/1
        /// </remarks>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("pwd")]
        public async Task<IActionResult> ChangePwdAsync([FromBody] ChangePwdRequest request)
        {
            if (request == null)
                throw new Warning(AppRes.RequestIsEmpty);
            var ret = await _userService.ChangePasswordAsync(Session.GetUserId(), request.OldPassword,
                request.NewPassword);
            _logger.Operate("用户", BusinessType.ChangePwd, CurrentMethodName);
            return Success(ret);
        }

        #endregion

        #region 批量删除

        /// <summary>
        /// 批量删除，注意：body参数需要添加引号，"'1,2,3'"而不是"1,2,3"
        /// </summary>
        /// <remarks>
        /// 调用范例:
        /// POST   
        /// /api/customer/delete
        /// body: "'1,2,3'"
        /// </remarks>
        /// <param name="ids">标识列表，多个Id用逗号分隔，范例：1,2,3</param>
        [HttpPost("delete")]
        public async Task<IActionResult> BatchDeleteAsync([FromBody] string ids)
        {
            if (ids.IsEmpty())
                throw new Warning(AppRes.IdIsEmpty);
            await _userService.DeleteAsync(ids);
            _logger.Operate("用户", BusinessType.BatchDelete, CurrentMethodName);
            return Success(true);
        }

        #endregion

        #region 分页查询

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <remarks> 
        /// 调用范例: 
        /// GET
        /// /api/customer?name=a
        /// </remarks>
        /// <param name="query">查询参数</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> PagerQueryAsync([FromQuery] UserQuery query)
        {
            var result = await _userService.PageQueryAsync(query);
            return Success(result);
        }

        #endregion
    }
}