using System.Collections.Generic;
using System.Threading.Tasks;
using DCSoft.Applications.Responses.Systems;
using Util.Applications;

namespace DCSoft.Applications.Services.Abstractions.Systems
{
    /// <summary>
    /// 菜单服务
    /// </summary>
    public interface IMenuService : IService
    {
        /// <summary>
        /// 获取菜单
        /// </summary>
        Task<List<MenuResponse>> GetMenusAsync();
    }
}