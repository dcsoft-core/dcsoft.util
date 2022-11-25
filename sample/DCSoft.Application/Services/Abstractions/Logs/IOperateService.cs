using DCSoft.Applications.Dtos.Logs;
using DCSoft.Data.Queries.Logs;
using Util.Applications;

namespace DCSoft.Applications.Services.Abstractions.Logs
{
    /// <summary>
    /// 操作日志服务
    /// </summary>
    public interface IOperateService : ICrudService<OperateDto, OperateQuery>
    {
    }
}