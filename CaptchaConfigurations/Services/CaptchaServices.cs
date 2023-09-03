using CaptchaConfigurations.CaptchaOptionsDTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using SixLabors.Fonts;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Numerics;
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
        private readonly IMemoryCache _cacheService;
        int tryFindDont = 0;


        public CaptchaServices(CaptchaOptions options, IMemoryCache? cacheService = null)
        {
            _rnd = new Random();
            _httpContextAccessot = new HttpContextAccessor();
            _options = options;
            _cacheService = cacheService;
        }


        public async Task<byte[]> ConvertImageToByteArrayAsync(Bitmap bitmap)
        {
            ImageConverter converter = new ImageConverter();
            var imageAsByte = (byte[])converter.ConvertTo(bitmap, typeof(byte[]));
            return imageAsByte;
        }

        public Task<Bitmap> CreateImageAsync(string randomString)
        {
            //Bitmap secImage = new Bitmap(width: 60, height: 60);
            //Graphics graphIma = Graphics.FromImage(secImage);
            //graphIma.DrawString(randomString, new Font("arial", StaticParams.FontSize.Value, StaticParams.FontStyle), SystemBrushes.WindowText, new System.Drawing.PointF());
            //return Task.FromResult(secImage);
            return null;
        }

        public Task<string> CreateRandomStringAsync()
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

        public async Task SetOrReSetCoockieAsync(string key, string value)
        {
            string? cookie = _httpContextAccessot.HttpContext.Request.Cookies[key];
            if (!string.IsNullOrEmpty(cookie))
            {
                _httpContextAccessot.HttpContext.Response.Cookies.Delete(key);
            }
            _httpContextAccessot.HttpContext.Response.Cookies.Append(key, value);
        }

        public Task RemoveCoockieAsync()
        {
            string? cookie = _httpContextAccessot.HttpContext.Request.Cookies[CookieKey];
            if (!string.IsNullOrEmpty(cookie))
            {
                _httpContextAccessot.HttpContext.Response.Cookies.Delete(CookieKey);
            }
            return Task.CompletedTask;
        }


        public async Task<byte[]> GenerateImage(string randonString)
        {
            var t1 = _options;


            byte[] result;

            using (var imgText = new Image<Rgba32>(StaticParams.Width, StaticParams.Height))
            {
                float position = 0;
                Random random = new Random();
                byte startWith = (byte)random.Next(5, 10);
                imgText.Mutate(ctx => ctx.BackgroundColor(SixLabors.ImageSharp.Color.White));



                string fontName = StaticParams.FontFamilies[tryFindDont];
                SixLabors.Fonts.Font font;
                try
                {
                    tryFindDont++;
                    font = SixLabors.Fonts.SystemFonts.CreateFont(fontName, StaticParams.FontSize, StaticParams.FontStyle);
                }
                catch (Exception  )
                {
                    if (tryFindDont > StaticParams.FontFamilies.Length)
                    {
                        throw new Exception(message: "fonts are not exists" );
                    }
                    return await GenerateImage(randonString);
                }


                foreach (char c in randonString)
                {
                    var location = new SixLabors.ImageSharp.PointF(startWith + position, random.Next(6, 13));
                    imgText.Mutate(ctx => ctx.DrawText(c.ToString(), font, StaticParams.TextColor[random.Next(0, StaticParams.TextColor.Length)], location));
                    position += TextMeasurer.Measure(c.ToString(), new TextOptions(font)).Width;
                }

                //add rotation
                AffineTransformBuilder rotation = getRotation();
                imgText.Mutate(ctx => ctx.Transform(rotation));

                // add the dynamic image to original image
                ushort size = (ushort)TextMeasurer.Measure(randonString, new TextOptions(font)).Width;
                var img = new Image<Rgba32>(size + 10 + 5, StaticParams.Height);
                img.Mutate(ctx => ctx.BackgroundColor(StaticParams.BackgroundColor[random.Next(0, StaticParams.BackgroundColor.Length)]));


                Parallel.For(0, StaticParams.DrawLines, i =>
                {
                    int x0 = random.Next(0, random.Next(0, 30));
                    int y0 = random.Next(10, img.Height);
                    int x1 = random.Next(img.Width - random.Next(0, ((int)(img.Width * 0.25))), img.Width);
                    int y1 = random.Next(0, img.Height);
                    img.Mutate(ctx =>
                            ctx.DrawLines(StaticParams.DrawLinesColor[random.Next(0, StaticParams.DrawLinesColor.Length)],
                                          Captcha.ExtensionMethod.Extensions.GenerateNextFloat(StaticParams.MinLineThickness, StaticParams.MaxLineThickness),
                                          new SixLabors.ImageSharp.PointF[] { new SixLabors.ImageSharp.PointF(x0, y0), new SixLabors.ImageSharp.PointF(x1, y1) })
                            );
                });

                img.Mutate(ctx => ctx.DrawImage(imgText, 0.80f));

                Parallel.For(0, StaticParams.NoiseRate, i =>
                {
                    int x0 = random.Next(0, img.Width);
                    int y0 = random.Next(0, img.Height);
                    img.Mutate(
                                ctx => ctx
                                    .DrawLines(StaticParams.NoiseRateColor[random.Next(0, StaticParams.NoiseRateColor.Length)],
                                    Captcha.ExtensionMethod.Extensions.GenerateNextFloat(0.5, 1.5), new SixLabors.ImageSharp.PointF[] { new Vector2(x0, y0), new Vector2(x0, y0) })
                            );
                });

                img.Mutate(x =>
                {
                    x.Resize(StaticParams.Width, StaticParams.Height);
                });

                using (var ms = new MemoryStream())
                {
                    img.Save(ms, StaticParams.Encoder);
                    result = ms.ToArray();
                }
            }

            return result;

        }

        private AffineTransformBuilder getRotation()
        {
            Random random = new Random();
            var builder = new AffineTransformBuilder();
            var width = random.Next(10, StaticParams.Width);
            var height = random.Next(10, StaticParams.Height);
            var pointF = new SixLabors.ImageSharp.PointF(width, height);
            var rotationDegrees = random.Next(0, StaticParams.MaxRotationDegrees);
            var result = builder.PrependRotationDegrees(rotationDegrees, pointF);
            return result;
        }
        public Task<bool> SetCoockieInCache(string rndText, string coockie)
        {
            var key = $"Key{rndText}";
            _cacheService.Set(key, coockie);
            return Task.FromResult(true);
        }


        public Task<bool> DeleteCoockieFromCache(string rndText, string coockie)
        {
            var key = $"Key{rndText}";
            var cacheData = _cacheService.Get(key);
            if (cacheData is null)
            {
                return Task.FromResult(false);

            }
            if (cacheData.ToString() != coockie)
            {
                return Task.FromResult(false);
            }
            _cacheService.Remove(key);
            return Task.FromResult(true);

        }

        public Task<bool> CheckCoockie(string rndText, string coockie)
        {
            var key = $"Key{rndText}";
            var res = _cacheService.Get(key);
            if (res is null)
            {
                return Task.FromResult(false);
            }
            if (res.ToString() != coockie)
            {
                return Task.FromResult(false);
            }
            return Task.FromResult(true);
        }




        //To Use By Developer in Web Project
        public async Task<FileContentResult> GetCaptcha()
        {
            var randomString = await CreateRandomStringAsync();
            var hashString = await HashString(randomString);
            await SetOrReSetCoockieAsync(CookieKey, hashString);
            var imageAsbyteArray = await GenerateImage(randomString);
            await SetCoockieInCache(randomString, hashString);

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

        //public async Task<bool> SetCoockieInCache(string coockie)
        //{
        //    var key = Guid.NewGuid().ToString("N");
        //     _cacheService.Set(key, coockie);
        //}

    }
}
