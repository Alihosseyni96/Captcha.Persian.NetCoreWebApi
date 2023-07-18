using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
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

        public CaptchaServices(Random random)
        {
            _rnd = random;
            _httpContextAccessot = new HttpContextAccessor();
        }

        public async Task<byte[]> ConvertImageToByteArrayAsync(Bitmap bitmap)
        {
            ImageConverter converter = new ImageConverter();
            var imageAsByte = (byte[])converter.ConvertTo(bitmap, typeof(byte[]));
            return imageAsByte;
        }

        public Task<Bitmap> CreateImageAsync(string randomString)
        {
            Bitmap secImage = new Bitmap(width: 60, height: 60);
            Graphics graphIma = Graphics.FromImage(secImage);
            graphIma.DrawString(randomString, new Font("arial", 10, FontStyle.Strikeout), SystemBrushes.WindowText, new PointF());
            return Task.FromResult(secImage);   
        }

        public Task<string> CreateRandomStringAsync()
        {
            string ABC = "QWERTYUIOPASDFGHJKLZXCVBNM0123456789";
            var ABC1 = _rnd.Next(1, 36);
            var ABC2 = _rnd.Next(1, 36);
            var ABC3 = _rnd.Next(1, 36);
            var ABC4 = _rnd.Next(1, 36);
            var ABC5 = _rnd.Next(1, 36);
            var ABC6 = _rnd.Next(1, 36);

            string strABC1 = ABC.Substring(ABC1, 1);
            string strABC2 = ABC.Substring(ABC2, 1);
            string strABC3 = ABC.Substring(ABC3, 1);
            string strABC4 = ABC.Substring(ABC4, 1);
            string strABC5 = ABC.Substring(ABC5, 1);
            string strABC6 = ABC.Substring(ABC6, 1);

            string result = strABC1 + strABC2 + strABC3 + strABC4 + strABC5 + strABC6;
            return Task.FromResult(result);
        }


        public Task<string> HashString(string text)
        {
            string resultAsHash = string.Empty;
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashValue1 = sha256.ComputeHash(Encoding.UTF8.GetBytes(text));
                resultAsHash = Convert.ToHexString(hashValue1);
            }
            return Task.FromResult(resultAsHash);   
        }

        public async  Task SetOrReSetCoockieAsync(string key, string value)
        {
            string? cookie = _httpContextAccessot.HttpContext.Request.Cookies[key] ;
            if (!string.IsNullOrEmpty(cookie))
            {
                _httpContextAccessot.HttpContext.Response.Cookies.Delete(key);
            }
            _httpContextAccessot.HttpContext.Response.Cookies.Append(key, value);
            
            
        }

        //To Register In Cotainer
        public async Task<FileContentResult> GetCaptcha()
        {
            var randomString = await CreateRandomStringAsync();
            var bitmapImage = await CreateImageAsync(randomString);
            var hashString = await HashString(randomString);
            await SetOrReSetCoockieAsync(CookieKey, hashString);
            var imageAsbyteArray = await ConvertImageToByteArrayAsync(bitmapImage);
            
            return new FileContentResult(imageAsbyteArray , "image/png");
        }

        public async Task ValidateCaptcha(string inputString, string coockieValue)
        {
            var inputTextAsHash = await HashString(inputString);
            if (inputTextAsHash != coockieValue)
            {
                throw new Exception("کپچا را صحیح وارد کنید");
            }
        }
    }
}
