using CaptchaConfigurations.CaptchaOptionsDTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CaptchaConfigurations.Services
{
    public class CaptchaServices : ICaptchaServices
    {
        private readonly Random _rnd;
        private readonly HttpContextAccessor _httpContextAccessot;
        private readonly string CookieKey = "CaptchaKey";
        private readonly CaptchaOptions _options;

        public CaptchaServices(CaptchaOptions options)
        {
            _rnd = new Random();
            _httpContextAccessot = new HttpContextAccessor();
            _options = options;

        }


        private async Task<byte[]> ConvertImageToByteArrayAsync(Bitmap bitmap)
        {
            ImageConverter converter = new ImageConverter();
            var imageAsByte = (byte[])converter.ConvertTo(bitmap, typeof(byte[]));
            return imageAsByte;
        }

        private Task<Bitmap> CreateImageAsync(string randomString)
        {
            Bitmap secImage = new Bitmap(width: 60, height: 60);
            Graphics graphIma = Graphics.FromImage(secImage);
            graphIma.DrawString(randomString, new Font("arial", StaticParams.FontSize.Value, StaticParams.FontStyle.Value), SystemBrushes.WindowText, new PointF());
            return Task.FromResult(secImage);
        }

        private Task<string> CreateRandomStringAsync()
        {
            string captchaFormat = string.Empty;
            int captchaFormatCount = 36;
            string res = "";


            switch (StaticParams.CaptchaType)
            {
                case CaptchaType.mixed:
                    captchaFormat = "QWERTYUIOPASDFGHJKLZXCVBNM0123456789"; // 36
                    captchaFormatCount = 36;
                    break;

                case CaptchaType.Numbers:
                    captchaFormat = "0123456789"; // 10
                    captchaFormatCount = 10;
                    break;

                case CaptchaType.Letters:
                    captchaFormat = "QWERTYUIOPASDFGHJKLZXCVBNM"; //26
                    captchaFormatCount = 26;
                    break;
            }

            for (int i = 1; i <= StaticParams.CaptchaCharacter; i++)
            {
                var ABCi = _rnd.Next(1, captchaFormatCount);
                string strABCi = captchaFormat.Substring(ABCi, 1);
                res += strABCi;
            }
            return Task.FromResult(res);

        }


        internal Task<string> HashString(string text)
        {
            string resultAsHash = string.Empty;
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashValue1 = sha256.ComputeHash(Encoding.UTF8.GetBytes(text));
                resultAsHash = Convert.ToHexString(hashValue1);
            }
            return Task.FromResult(resultAsHash);
        }

        internal async Task SetOrReSetCoockieAsync(string key, string value)
        {
            string? cookie = _httpContextAccessot.HttpContext.Request.Cookies[key];
            if (!string.IsNullOrEmpty(cookie))
            {
                _httpContextAccessot.HttpContext.Response.Cookies.Delete(key);
            }
            _httpContextAccessot.HttpContext.Response.Cookies.Append(key, value);
        }

        internal Task RemoveCoockieAsync()
        {
            string? cookie = _httpContextAccessot.HttpContext.Request.Cookies[CookieKey];
            if (!string.IsNullOrEmpty(cookie))
            {
                _httpContextAccessot.HttpContext.Response.Cookies.Delete(CookieKey);
            }
            return Task.CompletedTask;
        }


        //To Use By Developer in Web Project
        public async Task<FileContentResult> GetCaptcha()
        {
            var randomString = await CreateRandomStringAsync();
            var bitmapImage = await CreateImageAsync(randomString);
            var hashString = await HashString(randomString);
            await SetOrReSetCoockieAsync(CookieKey, hashString);
            var imageAsbyteArray = await ConvertImageToByteArrayAsync(bitmapImage);

            return new FileContentResult(imageAsbyteArray, "image/png");
        }

        public async Task ValidateCaptcha(string inputString, string coockieValue)
        {
            var inputTextAsHash = await HashString(inputString);
            if (inputTextAsHash != coockieValue)
            {
                throw new Exception("کپچا را صحیح وارد کنید");
            }
        }
        //
    }
}
