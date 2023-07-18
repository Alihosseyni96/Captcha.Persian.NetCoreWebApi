using Microsoft.AspNetCore.Mvc;

namespace MyCaptcha.CaptchaDto
{
    public class GetCaptchaDto
    {
        public FileContentResult FileContextResult { get; set; }
        public string TextHash { get; set; }
    }
}
