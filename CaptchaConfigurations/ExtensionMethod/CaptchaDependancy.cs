using CaptchaConfigurations.ActionFilter;
using CaptchaConfigurations.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaptchaConfigurations.ExtensionMethod
{
    public static class CaptchaDependancy
    {
        public static void UseCaptcha(this IServiceCollection services)
        {
            services.AddScoped<Random>();
            services.AddScoped<ICaptchaServices, CaptchaConfigurations.Services.CaptchaServices>();
            services.AddHttpContextAccessor();
            services.AddScoped<ValidateCaptchaAttribute>();
        }
    }
}
