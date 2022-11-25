namespace DCSoft.Applications.Responses.Systems.AuthResult;

/// <summary>
/// 密码加密密钥返回
/// </summary>
public class AuthEncryptKeyResp
{
    /// <summary>
    /// 缓存键
    /// </summary>
    public string Key { get; set; }

    /// <summary>
    /// 密钥
    /// </summary>
    public string EncryptKey { get; set; }
}