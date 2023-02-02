using System.Threading.Tasks;
using Util.Applications;
using Util.Applications.Dtos;
using Util.Data.Queries;

namespace Util.Extras.Applications
{
    /// <summary>
    /// 增删改查服务
    /// </summary>
    /// <typeparam name="TDto">数据传输对象类型</typeparam>
    /// <typeparam name="TQuery">查询参数类型</typeparam>
    public interface IDeleteService<TDto, in TQuery> : IQueryService<TDto, TQuery>
        where TDto : IDto, new()
        where TQuery : IPage
    {
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ids">用逗号分隔的标识列表，范例："1,2"</param>
        Task DeleteAsync(string ids);
    }
}