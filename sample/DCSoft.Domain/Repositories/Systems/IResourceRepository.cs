using DCSoft.Domain.Models;
using DCSoft.Domain.Models.Systems;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Util.Domain.Trees;

namespace DCSoft.Domain.Repositories.Systems
{
    /// <summary>
    /// 资源仓储
    /// </summary>
    public interface IResourceRepository : ITreeRepository<Resource>
    {
        /// <summary>
        /// 获取模块列表
        /// </summary>
        /// <param name="applicationId">应用程序标识</param>
        /// <param name="roleIds">角色标识列表</param>
        Task<IEnumerable<Resource>> GetModulesAsync(Guid applicationId, List<Guid> roleIds);
    }
}