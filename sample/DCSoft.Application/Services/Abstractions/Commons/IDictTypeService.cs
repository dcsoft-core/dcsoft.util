using DCSoft.Applications.Dtos.Commons;
using DCSoft.Data.Queries.Commons;
using Util.Applications;

namespace DCSoft.Applications.Services.Abstractions.Commons
{
    /// <summary>
    /// 字典类型服务
    /// </summary>
    public interface IDictTypeService : ICrudService<DictTypeDto, DictTypeQuery>
    {
    }
}