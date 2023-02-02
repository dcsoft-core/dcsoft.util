using System;
using DCSoft.Integration.Helpers;
using DCSoft.Integration.Upload;
using DCSoft.Web.Core.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Util.Infrastructure;

namespace DCSoft.Web.Core
{
    /// <summary>
    /// 启动配置
    /// </summary>
    public class Startup : IServiceRegistrar
    {
        /// <inheritdoc />
        public int OrderId => 1000;

        /// <inheritdoc />
        public bool Enabled => true;

        /// <inheritdoc />
        public Action Register(ServiceContext context)
        {
            context.HostBuilder.ConfigureServices((hostContext, services) =>
            {
                services.AddCaches();
                services.AddSingleton<UploadHelper>();
                services.AddSingleton<VerifyCodeHelper>();
            });
            return null!;
        }
    }
}
