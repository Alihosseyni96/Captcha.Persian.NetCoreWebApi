using SixLabors.ImageSharp.Formats;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaptchaConfigurations.CaptchaOptionsDTO
{
    public  class CaptchaOptions
    {
        public  CaptchaType? CaptchaType { get; set; } = CaptchaOptionsDTO.CaptchaType.mixed;
        public  int? CaptchaCharacter { get; set; } = 6;
        public  CaptchaValueSendType? CaptchaValueSendType { get; set; } = CaptchaOptionsDTO.CaptchaValueSendType.InHeader;

        /// <summary>
        /// "Arial", "Verdana", "Times New Roman"
        /// </summary>
        public string[] FontFamilies { get; set; } = new string[] { "Arial", "Verdana", "Times New Roman" };
        public  SixLabors.ImageSharp.Color[] TextColor { get; set; } = new SixLabors.ImageSharp.Color[] { SixLabors.ImageSharp.Color.Blue, SixLabors.ImageSharp.Color.Black, SixLabors.ImageSharp.Color.Black, SixLabors.ImageSharp.Color.Brown, SixLabors.ImageSharp.Color.Gray, SixLabors.ImageSharp.Color.Green };
        public  SixLabors.ImageSharp.Color[] DrawLinesColor { get; set; } = new SixLabors.ImageSharp.Color[] { SixLabors.ImageSharp.Color.Blue, SixLabors.ImageSharp.Color.Black, SixLabors.ImageSharp.Color.Black, SixLabors.ImageSharp.Color.Brown, SixLabors.ImageSharp.Color.Gray, SixLabors.ImageSharp.Color.Green };
        public  float MinLineThickness { get; set; } = 0.7f;
        public  float MaxLineThickness { get; set; } = 2.0f;
        public  ushort Width { get; set; } = 180;
        public  ushort Height { get; set; } = 50;
        public  ushort NoiseRate { get; set; } = 800;
        public  SixLabors.ImageSharp.Color[] NoiseRateColor { get; set; } = new SixLabors.ImageSharp.Color[] { SixLabors.ImageSharp.Color.Gray };
        public  byte FontSize { get; set; } = 29;
        public  SixLabors.Fonts.FontStyle FontStyle { get; set; } = SixLabors.Fonts.FontStyle.Regular;
        public  EncoderTypes EncoderType { get; set; } = EncoderTypes.Png;
        public  IImageEncoder Encoder => Captcha.ExtensionMethod.Extensions.GetEncoder(EncoderType);
        public  byte DrawLines { get; set; } = 5;
        public  byte MaxRotationDegrees { get; set; } = 5;
        public  SixLabors.ImageSharp.Color[] BackgroundColor { get; set; } = new SixLabors.ImageSharp.Color[] { SixLabors.ImageSharp.Color.White };

    }




    internal static class StaticParams
    {
        public static CaptchaType? CaptchaType { get; internal set; }
        public static int? CaptchaCharacter { get; internal set; } 
        public static CaptchaValueSendType? CaptchaValueSendType { get; internal set; }
        public static string[] FontFamilies { get; set; } 
        public static SixLabors.ImageSharp.Color[] TextColor { get; set; } 
        public static SixLabors.ImageSharp.Color[] DrawLinesColor { get; set; }
        public static float MinLineThickness { get; set; } 
        public static float MaxLineThickness { get; set; }
        public static ushort Width { get; set; } 
        public static ushort Height { get; set; } 
        public static ushort NoiseRate { get; set; }
        public static SixLabors.ImageSharp.Color[] NoiseRateColor { get; set; } 
        public static byte FontSize { get; set; } 
        public static SixLabors.Fonts.FontStyle FontStyle { get; set; } 
        public static EncoderTypes EncoderType { get; set; } 
        public static IImageEncoder Encoder { get; set; }
        public static byte DrawLines { get; set; } 
        public static byte MaxRotationDegrees { get; set; } 
        public static SixLabors.ImageSharp.Color[] BackgroundColor { get; set; }


    }


    public enum CaptchaType
    {
        Letters = 1,
        Numbers = 2,
        mixed = 3
    }

    public enum CaptchaValueSendType
    {
        InHeader = 1,
        InBody = 2,
    }
    public enum EncoderTypes
    {
        Jpeg,
        Png,
    }


}
