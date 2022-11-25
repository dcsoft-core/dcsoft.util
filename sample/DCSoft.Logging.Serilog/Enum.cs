namespace DCSoft.Logging.Serilog
{
    /// <summary>
    /// 业务类型（0 其它,1 新增,2 修改,3 删除）
    /// </summary>
    public enum BusinessType
    {
        /// <summary>
        /// 其它
        /// </summary>
        Other = 0,

        /// <summary>
        /// 新增
        /// </summary>
        Insert = 1,

        /// <summary>
        /// 修改
        /// </summary>
        Update = 2,

        /// <summary>
        /// 删除
        /// </summary>
        Delete = 3,

        /// <summary>
        /// 保存
        /// </summary>
        Save = 4,

        /// <summary>
        /// 批量新增
        /// </summary>
        BatchInsert = 5,

        /// <summary>
        /// 批量修改
        /// </summary>
        BatchUpdate = 6,

        /// <summary>
        /// 批量删除
        /// </summary>
        BatchDelete = 7,

        /// <summary>
        /// 批量保存
        /// </summary>
        BatchSave = 8,

        /// <summary>
        /// 导入
        /// </summary>
        Import = 9,

        /// <summary>
        /// 导出
        /// </summary>
        Export = 10,

        /// <summary>
        /// 上传
        /// </summary>
        Upload = 11,

        /// <summary>
        /// 启用
        /// </summary>
        Enable = 12,

        /// <summary>
        /// 禁用
        /// </summary>
        Disable = 13,

        /// <summary>
        /// 头像
        /// </summary>
        Avatar = 14,

        /// <summary>
        /// 重置密码
        /// </summary>
        ResetPwd = 15,

        /// <summary>
        /// 修改密码
        /// </summary>
        ChangePwd = 16,
    }

    /// <summary>
    /// 操作类别（0 其它,1 后台用户,2 手机端用户）
    /// </summary>
    public enum OperateType
    {
        /// <summary>
        /// 其它
        /// </summary>
        Other = 0,

        /// <summary>
        /// 后台用户
        /// </summary>
        Admin = 1,

        /// <summary>
        /// 手机端用户
        /// </summary>
        Mobile = 2
    }

    /// <summary>
    /// 操作状态（0 正常,1 异常）
    /// </summary>
    public enum OperateStatus
    {
        /// <summary>
        /// 正常
        /// </summary>
        Success = 0,

        /// <summary>
        /// 异常
        /// </summary>
        Failure = 1
    }

    /// <summary>
    /// 登录状态（0 成功,1 失败）
    /// </summary>
    public enum LoginStatus : int
    {
        /// <summary>
        /// 成功
        /// </summary>
        Success = 0,

        /// <summary>
        /// 失败
        /// </summary>
        Failure = 1
    }
}