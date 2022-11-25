using DCSoft.Applications.Dtos.Commons;
using DCSoft.Data.Queries.Commons;
using Util.Applications.Trees;

namespace DCSoft.Applications.Services.Abstractions.Commons
{
    /// <summary>
    /// 组织机构查询服务
    /// </summary>
    public interface IQueryDepartmentService : ITreeService<DepartmentDto, DepartmentQuery>
    {
    }
}