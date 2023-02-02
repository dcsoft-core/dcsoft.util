using Microsoft.EntityFrameworkCore;
using System;
using Util.Extras.Data.EntityFrameworkCore;

namespace DCSoft.Data.MySql
{
    /// <summary>
    /// 工作单元
    /// </summary>
    public class DataUnitOfWork : MySqlUnitOfWorkBase, IDataUnitOfWork
    {
        /// <summary>
        /// 初始化工作单元
        /// </summary>
        /// <param name="serviceProvider">服务提供器</param>
        /// <param name="options">配置项</param>
        public DataUnitOfWork(IServiceProvider serviceProvider, DbContextOptions<DataUnitOfWork> options) : base(serviceProvider, options)
        {
        }
    }
}