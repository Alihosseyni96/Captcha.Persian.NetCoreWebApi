using CaptchaConfigurations.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CaptchaConfigurations.ActionFilter
{
    public class ValidateCaptchaAttribute : ActionFilterAttribute
    {


        public override async void OnActionExecuting(ActionExecutingContext context)
        {
			try
			{
                var inputText = context.HttpContext.Request.Headers["CaptchaValue"].ToString();
                var coockieValue = context.HttpContext.Request.Cookies["CaptchaKey"];
                if (inputText == null || coockieValue == null)
                {
                    throw new Exception(" مقدار کپچا را به درستی وارد کنید");
                }

                var capatchaServices = context.HttpContext.RequestServices
                    .GetService(typeof(ICaptchaServices)) as CaptchaServices;

                var inputTextAsHash = await capatchaServices.HashString(inputText);

                if (inputTextAsHash != coockieValue)
                {
                    throw new Exception("کد کیپچا را صحیح وارد کنید");
                }

            }
            catch (Exception e)
            {
                context.Result = new BadRequestObjectResult(e.Message);
			}
        }


    }
}
