using DCSoft.Applications.Dtos.Commons;
using DCSoft.Applications.Services.Abstractions.Commons;
using DCSoft.Data;
using DCSoft.Data.Queries.Commons;
using DCSoft.Domain.Repositories.Commons;
using System;
using System.Linq;
using DCSoft.Domain.Models.Commons;
using Util.Applications;
using DCSoft.Applications.Extensions.Commons;
using DCSoft.Applications.Requests.Commons;
using DCSoft.Applications.Responses.Commons;
using System.Collections.Generic;
using System.Threading.Tasks;
using Util.Domain;
using Util.Helpers;
using Util;
using Util.Exceptions;

namespace DCSoft.Applications.Services.Implements.Commons
{
    /// <summary>
    /// 字典数据服务
    /// </summary>
    public class DictDataService : ServiceBase, IDictDataService
    {
        /// <summary>
        /// 初始化字典数据服务
        /// </summary>
        /// <param name="serviceProvider">服务提供器</param>
        /// <param name="unitOfWork">工作单元</param>
        /// <param name="dictTypeRepository">字曲类型仓储</param>
        /// <param name="dictDataRepository">字典数据仓储</param>
        public DictDataService(IServiceProvider serviceProvider, 
            IDataUnitOfWork unitOfWork,
            IDictTypeRepository dictTypeRepository,
            IDictDataRepository dictDataRepository) : base(serviceProvider)
        {
            _dictTypeRepository = dictTypeRepository;
            _dictDataRepository = dictDataRepository;
        }

        /// <summary>
        /// 字典类型仓储
        /// </summary>
        private readonly IDictTypeRepository _dictTypeRepository;

        /// <summary>
        /// 字典数据仓储
        /// </summary>
        private readonly IDictDataRepository _dictDataRepository;

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="request">创建参数</param>
        public async Task<Guid> CreateAsync(CreateDictDataRequest request)
        {
            var dictionary = request.ToEntity();
            dictionary.CheckNull(nameof(dictionary));
            dictionary.Init();
            if (await _dictDataRepository.ExistsAsync(t => t.Code == request.Code && t.Type == request.Type))
                throw new Warning("字典编码已存在");
            if (await _dictDataRepository.ExistsAsync(t => t.Name == request.Name && t.Type == request.Type))
                throw new Warning("字典名称已存在");
            var parent = await _dictDataRepository.SingleAsync(t => t.Id.Equals(dictionary.ParentId));
            if (parent == null)
            {
                var parentType = await _dictTypeRepository.SingleAsync(t => t.Id.Equals(dictionary.ParentId));
                parent = new DictData(Id.CreateGuid(), parentType.Id.ToString() + ",", 0);
            }
            dictionary.InitPath(parent);
            dictionary.SortId = await _dictDataRepository.GenerateSortIdAsync(dictionary.ParentId);
            await _dictDataRepository.AddAsync(dictionary);
            return dictionary.Id;
        }


        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="request">参数</param>
        public async Task UpdateAsync(DictDataDto request)
        {
            var dictionary = await _dictDataRepository.FindByIdAsync(request.Id.ToGuid());
            request.MapTo(dictionary);
            if (await _dictDataRepository.ExistsAsync(t => t.Id != request.Id.ToGuid() && t.Code == request.Code && t.Type == request.Type))
                throw new Warning("字典编码已存在");
            dictionary.InitPinYin();
            await _dictDataRepository.UpdatePathAsync(dictionary);
            await _dictDataRepository.UpdateAsync(dictionary);
        }

        /// <summary>
        /// 获取字典列表数据
        /// </summary>
        /// <param name="code">字典编码</param>
        /// <param name="query">查询参数</param>
        /// <returns></returns>
        public async Task<List<DictDataResponse>> GetListByCodeAsync(string code, DictDataQuery query)
        {
            var result = await _dictDataRepository.GetListByCodeAsync(code);
            return result.OrderBy(t => t.SortId).ToSelectItem();
        }

        /// <summary>
        /// 获取字典树形数据
        /// </summary>
        /// <param name="code">字典编码</param>
        /// <param name="query">查询参数</param>
        /// <returns></returns>
        public async Task<List<DictDataTreeResponse>> GetTreeByCodeAsync(string code, DictDataQuery query)
        {
            var list = await _dictDataRepository.GetTreeByCodeAsync(code);
            var result = list.ToList();
            return result.MapToList<DictDataDto>().ToTreeData().ToList();
        }
    }
}