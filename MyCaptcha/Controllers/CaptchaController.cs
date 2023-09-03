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
            var res =  await _captchaServices.GetCaptcha();
            var error = HttpContext.Response.Body;
            return res;
        }

        [HttpPost]
        [ValidateCaptcha]
        public async Task<string> SigninApplicationJson([FromBody] SigninDto signin)
        {
            return "Hello World";
        }

        [HttpPost]
        [ValidateCaptcha]
        public async Task<string> SignInPostman([FromBody] CaptchaInputDto input)
        {
            return "Hello Postman";
        }




    }
}
