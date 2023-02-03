using System;
using Microsoft.Extensions.DependencyInjection;

namespace Util.Extras.Tools.Captcha
{
    /// <summary>
    /// 扩展类
    /// </summary>
    public static class CaptchaExtension
    {
        /// <summary>
        /// 启用Captcha
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddCaptcha(this IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services.AddScoped<SecurityCodeHelper>();
            services.AddScoped<VerifyCodeHelper>();
            return services;
        }
    }
}