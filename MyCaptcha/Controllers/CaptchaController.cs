using CaptchaConfigurations.ActionFilter;
using CaptchaConfigurations.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyCaptcha.CaptchaDto;

namespace MyCaptcha.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CaptchaController : ControllerBase
    {
        private readonly ICaptchaServices _captchaServices;

        public CaptchaController(ICaptchaServices captchaServices)
        {
            _captchaServices = captchaServices;
        }



        [HttpGet]
        public async Task<FileContentResult> GetCaptchaNew()
        {
            return await _captchaServices.GetCaptcha();
        }

        [HttpPost]
        [ValidateCaptcha]
        public async Task<string> SigninApplicationJson([FromBody] SigninDto signin)
        {
            return "Hello World";
        }




    }
}
