using DCSoft.Applications.Dtos.Commons;
using DCSoft.Applications.Services.Abstractions.Commons;
using DCSoft.Data;
using DCSoft.Data.Queries.Commons;
using DCSoft.Domain.Repositories.Commons;
using System;
using DCSoft.Domain.Models.Commons;
using Util.Applications;
using System.Threading.Tasks;
using Util.Exceptions;

namespace DCSoft.Applications.Services.Implements.Commons
{
    /// <summary>
    /// 公共参数服务
    /// </summary>
    public class ParametersService : CrudServiceBase<Parameters, ParametersDto, ParametersQuery>, IParametersService
    {
        /// <summary>
        /// 初始化公共参数服务
        /// </summary>
        /// <param name="serviceProvider">服务提供器</param>
        /// <param name="unitOfWork">工作单元</param>
        /// <param name="repository">仓储</param>
        public ParametersService(IServiceProvider serviceProvider, IDataUnitOfWork unitOfWork,
            IParametersRepository repository) : base(serviceProvider, unitOfWork, repository)
        {
            _parametersRepository = repository;
        }

        /// <summary>
        /// 系统参数仓储
        /// </summary>
        private readonly IParametersRepository _parametersRepository;

        /// <summary>
        /// 保存参数
        /// </summary>
        /// <param name="paramName"></param>
        /// <param name="paramValue"></param>
        /// <returns></returns>
        public async Task<bool> SaveParamAsync(string paramName, string paramValue)
        {
            var param = await _parametersRepository.SingleAsync(t => t.Name == paramName);
            if (param == null)
            {
                throw new Warning("参数信息未找到");
            }

            param.Value = paramValue;
            await _parametersRepository.UpdateAsync(param);
            return true;
        }
    }
}