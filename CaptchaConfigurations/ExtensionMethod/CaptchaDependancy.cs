using CaptchaConfigurations.ActionFilter;
using CaptchaConfigurations.CaptchaOptionsDTO;
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
        public static void UseCaptcha(this IServiceCollection services, CaptchaOptions options)
        {

            services.AddScoped<ICaptchaServices>(x => ActivatorUtilities.CreateInstance<CaptchaServices>(x, options));
            services.AddHttpContextAccessor();
            services.AddScoped<ValidateCaptchaAttribute>();
            services.AddScoped<ValidateCaptchaAttribute>(x => ActivatorUtilities.CreateInstance<ValidateCaptchaAttribute>(x, options));
            services.AddScoped<CaptchaOptions>();


        }
    }
}
