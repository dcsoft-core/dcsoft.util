using DCSoft.Applications.Dtos.Systems;
using DCSoft.Data.Queries.Systems;
using Util.Applications.Trees;

namespace DCSoft.Applications.Services.Abstractions.Systems
{
    /// <summary>
    /// 资源服务
    /// </summary>
    public interface IResourceService : ITreeService<ResourceDto, ResourceQuery>
    {
    }
}