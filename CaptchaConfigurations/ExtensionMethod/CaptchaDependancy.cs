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
        public static void UseCaptcha(this IServiceCollection services, CaptchaOptions? options = null)
        {
            CaptchaOptions _options;
            _options = options ?? new CaptchaOptions(); 

            StaticParams.CaptchaValueSendType = _options.CaptchaValueSendType;
            StaticParams.CaptchaType = _options.CaptchaType;
            StaticParams.CaptchaCharacter = _options.CaptchaCharacter;
            StaticParams.FontSize = _options.FontSize;
            StaticParams.FontStyle = _options.FontStyle;
            StaticParams.Width = _options.Width;
            StaticParams.Height = _options.Height;
            StaticParams.MinLineThickness = _options.MinLineThickness;
            StaticParams.MaxLineThickness = _options.MaxLineThickness;
            StaticParams.DrawLines = _options.DrawLines;
            StaticParams.BackgroundColor = _options.BackgroundColor;
            StaticParams.Encoder = _options.Encoder;
            StaticParams.DrawLinesColor = _options.DrawLinesColor;
            StaticParams.EncoderType = _options.EncoderType;
            StaticParams.FontFamilies = _options.FontFamilies;
            StaticParams.NoiseRate = _options.NoiseRate;
            StaticParams.NoiseRateColor = _options.NoiseRateColor;
            StaticParams.TextColor = _options.TextColor;
            StaticParams.MaxRotationDegrees = _options.MaxRotationDegrees;  
            

            services.AddScoped<ICaptchaServices>(x => ActivatorUtilities.CreateInstance<CaptchaServices>(x, _options ));
            services.AddHttpContextAccessor();
            services.AddScoped<ValidateCaptchaAttribute>();
            services.AddScoped<CaptchaOptions>();
            services.AddMemoryCache();



        }


    }
}
