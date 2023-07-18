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
        //private readonly CaptchaServices _captchaServices;
        private readonly ICaptchaServices _captchaServices;

        public CaptchaController(ICaptchaServices captchaServices)
        {
            _captchaServices = captchaServices;
        }

        //[HttpGet]
        //public async Task<FileContentResult> GetCaptcha()
        //{
        //    return await _captchaServices.GetCaptcha();
        //}


        [HttpGet]
        public async Task<FileContentResult> GetCaptchaNew()
        {
            return await _captchaServices.GetCaptcha();
        }
        //To Do Write a MiddleWare AndWrite A Service To Validate Captcha Input Text And CaptchaToken In Coockie

        [HttpPost]
        [ValidateCaptcha]
        public async Task<string> Signin([FromBody] SigninDto signin)
        {
            return "Hello World";
        }
    }
}
