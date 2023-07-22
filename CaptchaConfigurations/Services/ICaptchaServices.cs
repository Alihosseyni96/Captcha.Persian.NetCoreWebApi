using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaptchaConfigurations.Services
{
    public interface ICaptchaServices
    {
         Task<string> CreateRandomStringAsync();
        Task<Bitmap> CreateImageAsync(string randomString);
        Task<string> HashString(string text);
        Task SetOrReSetCoockieAsync(string key, string value);
        Task<byte[]> ConvertImageToByteArrayAsync(Bitmap bitmap);
        Task RemoveCoockieAsync();

        //To Register in Container
        Task<FileContentResult> GetCaptcha();
        Task ValidateCaptcha(string inputString, string coockieValue);
    }
}
