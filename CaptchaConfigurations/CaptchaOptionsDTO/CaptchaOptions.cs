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
        public  System.Drawing.FontStyle? FontStyle { get; set; } = System.Drawing.FontStyle.Strikeout;
        public  int? CaptchaCharacter { get; set; } = 6;
        public  int? FontSize { get; set; } = 10;
        public  CaptchaValueSendType? CaptchaValueSendType { get; set; } = CaptchaOptionsDTO.CaptchaValueSendType.InHeader;


        ////
        ///
        public  string[] FontFamilies { get; set; } = new string[] { "Arial", "Verdana", "Times New Roman" };
        public  SixLabors.ImageSharp.Color[] TextColor { get; set; } = new SixLabors.ImageSharp.Color[] { SixLabors.ImageSharp.Color.Blue, SixLabors.ImageSharp.Color.Black, SixLabors.ImageSharp.Color.Black, SixLabors.ImageSharp.Color.Brown, SixLabors.ImageSharp.Color.Gray, SixLabors.ImageSharp.Color.Green };
        public  SixLabors.ImageSharp.Color[] DrawLinesColor { get; set; } = new SixLabors.ImageSharp.Color[] { SixLabors.ImageSharp.Color.Blue, SixLabors.ImageSharp.Color.Black, SixLabors.ImageSharp.Color.Black, SixLabors.ImageSharp.Color.Brown, SixLabors.ImageSharp.Color.Gray, SixLabors.ImageSharp.Color.Green };
        public  float MinLineThickness { get; set; } = 0.7f;
        public  float MaxLineThickness { get; set; } = 2.0f;
        public  ushort Width { get; set; } = 180;
        public  ushort Height { get; set; } = 50;
        public  ushort NoiseRate { get; set; } = 800;
        public  SixLabors.ImageSharp.Color[] NoiseRateColor { get; set; } = new SixLabors.ImageSharp.Color[] { SixLabors.ImageSharp.Color.Gray };
        public  byte FontSizee { get; set; } = 29;
        public  SixLabors.Fonts.FontStyle FontStylee { get; set; } = SixLabors.Fonts.FontStyle.Regular;
        public  EncoderTypes EncoderType { get; set; } = EncoderTypes.Png;
        public  IImageEncoder Encoder => Captcha.ExtensionMethod.Extensions.GetEncoder(EncoderType);
        public  byte DrawLines { get; set; } = 5;
        public  byte MaxRotationDegrees { get; set; } = 5;
        public  SixLabors.ImageSharp.Color[] BackgroundColor { get; set; } = new SixLabors.ImageSharp.Color[] { SixLabors.ImageSharp.Color.White };

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



    internal static class StaticParams
    {
        public static CaptchaType? CaptchaType { get; internal set; }
        public static System.Drawing.FontStyle?  FontStyle { get; internal set; }
        public static int? CaptchaCharacter { get; internal set; } 
        public static int? FontSize { get; set; } 
        public static CaptchaValueSendType? CaptchaValueSendType { get; internal set; }


        //////
        ///
        public static string[] FontFamilies { get; set; } = new string[] { "Arial", "Verdana", "Times New Roman" };
        public static SixLabors.ImageSharp.Color[] TextColor { get; set; } = new SixLabors.ImageSharp.Color[] { SixLabors.ImageSharp.Color.Blue, SixLabors.ImageSharp.Color.Black, SixLabors.ImageSharp.Color.Black, SixLabors.ImageSharp.Color.Brown, SixLabors.ImageSharp.Color.Gray, SixLabors.ImageSharp.Color.Green };
        public static SixLabors.ImageSharp.Color[] DrawLinesColor { get; set; } = new   SixLabors.ImageSharp.Color[] { SixLabors.ImageSharp.Color.Blue, SixLabors.ImageSharp.Color.Black, SixLabors.ImageSharp.Color.Black, SixLabors.ImageSharp.Color.Brown, SixLabors.ImageSharp.Color.Gray, SixLabors.ImageSharp.Color.Green };
        public static float MinLineThickness { get; set; } = 0.7f;
        public static float MaxLineThickness { get; set; } = 2.0f;
        public static ushort Width { get; set; } = 180;
        public static ushort Height { get; set; } = 50;
        public static ushort NoiseRate { get; set; } = 800;
        public static SixLabors.ImageSharp.Color[] NoiseRateColor { get; set; } = new SixLabors.ImageSharp.Color[] { SixLabors.ImageSharp.Color.Gray };
        public static byte FontSizee { get; set; } = 29;
        public static SixLabors.Fonts.FontStyle FontStylee { get; set; } = SixLabors.Fonts.FontStyle.Regular;
        public static EncoderTypes EncoderType { get; set; } = EncoderTypes.Png;
        public static IImageEncoder Encoder => Captcha.ExtensionMethod.Extensions.GetEncoder(EncoderType);
        public static byte DrawLines { get; set; } = 5;
        public static byte MaxRotationDegrees { get; set; } = 5;
        public static SixLabors.ImageSharp.Color[] BackgroundColor { get; set; } = new SixLabors.ImageSharp.Color[] { SixLabors.ImageSharp.Color.White };


    }

    //public enum FontStyle
    //{
    //    /// <summary>
    //    /// Regular
    //    /// </summary>
    //    Regular = 0,

    //    /// <summary>
    //    /// Bold
    //    /// </summary>
    //    Bold = 1,

    //    /// <summary>
    //    /// Italic
    //    /// </summary>
    //    Italic = 2,

    //    /// <summary>
    //    /// Bold and Italic
    //    /// </summary>
    //    BoldItalic = 3,

    //    // TODO: Not yet supported
    //    // Underline = 4,
    //    // Strikeout = 8
    //}




}
