using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using MyCaptcha.CaptchaDto;
using System.Drawing;
using System.Security.Cryptography;
using System.Text;

namespace MyCaptcha.CaptchaConfiguration
{
    public class CaptchaServices
    {
        private readonly Random _rnd;
        private readonly HttpContextAccessor _httpContextAccessot;
        private readonly string CookieKey = "CaptchaKey";
        public CaptchaServices(Random rnd)
        {
            _rnd = rnd;
            _httpContextAccessot = new HttpContextAccessor();
        }

        public async Task<FileContentResult> GetCaptcha()
        {

            #region CreateRandomString
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

            #endregion
            //string fileName = $"captcha{Guid.NewGuid()}.jpg";
            //string path = Path.Combine(Environment.CurrentDirectory, @"CaptchaImageFolder\", fileName);
            #region CreateImage
            Bitmap secImage = new Bitmap(width: 50, height: 50);
            Graphics graphIma = Graphics.FromImage(secImage);
            graphIma.DrawString(result, new Font("arial", 12, FontStyle.Strikeout), SystemBrushes.WindowText, new PointF());
            #endregion

            #region HashRandomString
            string resultAsHash = string.Empty;
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashValue1 = sha256.ComputeHash(Encoding.UTF8.GetBytes(result));
                resultAsHash = Convert.ToHexString(hashValue1);
            }
            #endregion

            //set HashString into coockie
            #region SetCoocki
            var cookie = _httpContextAccessot.HttpContext.Request.Cookies[CookieKey];
            if (!string.IsNullOrEmpty(cookie))
            {
                _httpContextAccessot.HttpContext.Response.Cookies.Delete(CookieKey);
            }
            _httpContextAccessot.HttpContext.Response.Cookies.Append(CookieKey, resultAsHash);

            #endregion

            ImageConverter converter = new ImageConverter();
            var imageAsByte = (byte[])converter.ConvertTo(secImage, typeof(byte[]));



            return new FileContentResult(imageAsByte, "image/png");


        }

    }
}
