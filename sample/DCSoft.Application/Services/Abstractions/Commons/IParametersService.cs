using DCSoft.Applications.Dtos.Commons;
using DCSoft.Data.Queries.Commons;
using System.Threading.Tasks;
using Util.Applications;

namespace DCSoft.Applications.Services.Abstractions.Commons
{
    /// <summary>
    /// 公共参数服务
    /// </summary>
    public interface IParametersService : ICrudService<ParametersDto, ParametersQuery>
    {
        /// <summary>
        /// 保存参数
        /// </summary>
        /// <param name="paramName">名称</param>
        /// <param name="paramValue">值</param>
        /// <returns></returns>
        Task<bool> SaveParamAsync(string paramName, string paramValue);
    }
}