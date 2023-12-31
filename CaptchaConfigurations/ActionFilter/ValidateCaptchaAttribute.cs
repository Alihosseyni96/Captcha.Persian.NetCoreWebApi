﻿using CaptchaConfigurations.CaptchaOptionsDTO;
using CaptchaConfigurations.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection.PortableExecutable;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CaptchaConfigurations.ActionFilter
{
    public class ValidateCaptchaAttribute : ActionFilterAttribute , IExceptionFilter
    {

        public override async void OnActionExecuting(ActionExecutingContext context)
        {

            var inputText = string.Empty;

            switch (StaticParams.CaptchaValueSendType)
            {
                case CaptchaValueSendType.InHeader:
                     inputText = await InHeder(context);
                    break;
                case CaptchaValueSendType.InBody:
                    inputText = await InBody(context);
                    break;
            }

            try
            {
                var coockieValue = context.HttpContext.Request.Cookies["CaptchaKey"];
                if (string.IsNullOrEmpty(inputText) || string.IsNullOrEmpty(coockieValue))
                {
                    throw new Exception("کپچا نادرست است");
                }

                var capatchaServices = context.HttpContext.RequestServices
                    .GetService(typeof(ICaptchaServices)) as CaptchaServices;

                var inputTextAsHash = await capatchaServices.HashString(inputText);

                if (inputTextAsHash != coockieValue)
                {
                    await capatchaServices.RemoveCoockieAsync();
                    await capatchaServices.DeleteCoockieFromCache(inputText, coockieValue);
                    throw new Exception("کپچا نادرست است");

                }

                var checkCacheCoockie = await capatchaServices.CheckCoockie(inputText,coockieValue);
                if (checkCacheCoockie is false)
                {
                    await capatchaServices.DeleteCoockieFromCache(inputText, coockieValue);
                    await capatchaServices.RemoveCoockieAsync();
                    throw new Exception("کپچا نادرست است");
                }

                await capatchaServices.DeleteCoockieFromCache(inputText,coockieValue);
                await capatchaServices.RemoveCoockieAsync();


            }
            catch (Exception e)
            {
                context.Result = new ObjectResult(e) 
                {
                    StatusCode = StatusCodes.Status500InternalServerError ,
                    Value = e.Message,
                };


            }
        }



        private async Task<string> InHeder(ActionExecutingContext context)
        {
            var inputText = context.HttpContext.Request?.Headers["CaptchaValue"].ToString();
            return inputText;   

        }
        private async Task<string> InBody(ActionExecutingContext context)
        {
            var src = context.ActionArguments.First().Value;
            var inputText =  src.GetType()?.GetProperty("CaptchaValue")?.GetValue(src, null)?.ToString();
            return inputText;

        }

        public void OnException(ExceptionContext context)
        {
            throw new NotImplementedException();
        }
    }
}
