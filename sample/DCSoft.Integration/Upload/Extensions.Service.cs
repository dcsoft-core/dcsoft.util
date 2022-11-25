using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;
using System.IO;
using Util.Helpers;

namespace DCSoft.Integration.Upload
{
    /// <summary>
    /// 上传配置扩展服务
    /// </summary>
    public static class UploadConfigExtensions
    {
        /// <summary>
        /// 注册上传配置文件操作
        /// </summary>
        /// <param name="services"></param>
        public static void AddUploadConfig(this IServiceCollection services)
        {
            var uploadConfig = Config.GetSection("Upload");
            services.Configure<UploadOptions>(uploadConfig);
        }

        /// <summary>
        /// 上传配置
        /// </summary>
        /// <param name="app"></param>
        /// <param name="config"></param>
        private static void UseFileUploadConfig(IApplicationBuilder app, FileUploadOptions config)
        {
            if (!Directory.Exists(config.UploadPath))
            {
                Directory.CreateDirectory(config.UploadPath);
            }

            app.UseStaticFiles(new StaticFileOptions()
            {
                RequestPath = config.RequestPath,
                FileProvider = new PhysicalFileProvider(config.UploadPath)
            });
        }

        /// <summary>
        /// 上传配置
        /// </summary>
        /// <param name="app"></param>
        public static void UseUploadConfig(this IApplicationBuilder app)
        {
            var uploadConfig = app.ApplicationServices.GetRequiredService<IOptions<UploadOptions>>();
            UseFileUploadConfig(app, uploadConfig.Value.Avatar);
            UseFileUploadConfig(app, uploadConfig.Value.Document);
        }
    }

}
