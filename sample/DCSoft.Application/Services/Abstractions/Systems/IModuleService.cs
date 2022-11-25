using System;
using System.Threading.Tasks;
using DCSoft.Applications.Dtos.Systems;
using DCSoft.Applications.Requests.Systems;
using DCSoft.Data.Queries.Systems;
using Util.Aop;
using Util.Applications.Trees;

namespace DCSoft.Applications.Services.Abstractions.Systems
{
    /// <summary>
    /// 模块服务
    /// </summary>
    public interface IModuleService : ITreeService<ModuleDto, ResourceQuery>
    {
        /// <summary>
        /// 创建模块
        /// </summary>
        /// <param name="request">创建模块参数</param>
        Task<Guid> CreateAsync([NotNull] CreateModuleRequest request);

        /// <summary>
        /// 修改模块
        /// </summary>
        /// <param name="request">模块参数</param>
        new Task UpdateAsync([NotNull] ModuleDto request);
    }
}