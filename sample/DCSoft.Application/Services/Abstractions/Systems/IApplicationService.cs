using DCSoft.Applications.Dtos.Systems;
using DCSoft.Data.Queries.Systems;
using System.Threading.Tasks;
using Util.Applications;

namespace DCSoft.Applications.Services.Abstractions.Systems
{
    /// <summary>
    /// 应用程序服务
    /// </summary>
    public interface IApplicationService : ICrudService<ApplicationDto, ApplicationQuery>
    {
        /// <summary>
        /// 通过应用程序编码查找
        /// </summary>
        /// <param name="code">应用程序编码</param>
        Task<ApplicationDto> GetByCodeAsync(string code);
    }
}