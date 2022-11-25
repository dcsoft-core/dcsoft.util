using DCSoft.Applications.Dtos.Commons;
using DCSoft.Data.Queries.Commons;
using Util.Applications.Trees;

namespace DCSoft.Applications.Services.Abstractions.Commons
{
    /// <summary>
    /// 字典数据查询服务
    /// </summary>
    public interface IQueryDictDataService : ITreeService<DictDataDto, DictDataQuery>
    {
    }
}