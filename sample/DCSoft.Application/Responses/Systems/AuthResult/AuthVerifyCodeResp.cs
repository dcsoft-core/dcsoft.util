namespace DCSoft.Applications.Responses.Systems.AuthResult;

/// <summary>
/// 验证码返回
/// </summary>
public class AuthVerifyCodeResp
{
    /// <summary>
    /// 缓存键
    /// </summary>
    public string Key { get; set; }

    /// <summary>
    /// 图片
    /// </summary>
    public string Img { get; set; }
}