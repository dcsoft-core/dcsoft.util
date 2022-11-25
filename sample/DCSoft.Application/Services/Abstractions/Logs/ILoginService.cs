using DCSoft.Applications.Dtos.Logs;
using DCSoft.Data.Queries.Logs;
using Util.Applications;

namespace DCSoft.Applications.Services.Abstractions.Logs
{
    /// <summary>
    /// 登录日志服务
    /// </summary>
    public interface ILoginService : ICrudService<LoginDto, LoginQuery>
    {
    }
}