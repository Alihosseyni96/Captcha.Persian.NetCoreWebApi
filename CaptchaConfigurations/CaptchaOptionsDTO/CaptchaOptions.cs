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
        public  FontStyle? FontStyle { get; set; } = System.Drawing.FontStyle.Strikeout;
        public  int? CaptchaCharacter { get; set; } = 6;
        public  int? FontSize { get; set; } = 10;
        public  CaptchaValueSendType? CaptchaValueSendType { get; set; } = CaptchaOptionsDTO.CaptchaValueSendType.InHeader;
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
        ApplicationJson = 2,
        FormDataMethod = 3
    }


    internal static class StaticParams
    {
        public static CaptchaType? CaptchaType { get; internal set; }
        public static FontStyle? FontStyle { get; internal set; }
        public static int? CaptchaCharacter { get; internal set; } 
        public static int? FontSize { get; set; } 
        public static CaptchaValueSendType? CaptchaValueSendType { get; internal set; } 

    }



}
