namespace EasyOne.Controls.ExtendedUploadFile
{
    using EasyOne.Common;
    using System;
    using System.Web;

    public sealed class Utils
    {
        private Utils()
        {
        }

        public static HttpContext Context()
        {
            HttpContext current = HttpContext.Current;
            if (current == null)
            {
                throw new HttpException("HttpContext not found");
            }
            return current;
        }

        public static string GetFormatString(double size)
        {
            if (size >= 1048576.0)
            {
                return (Math.Round((double) (size / 1048576.0), 2) + " m");
            }
            if (size >= 1024.0)
            {
                return (Math.Round((double) (size / 1024.0), 2) + " k");
            }
            return (size + " bytes");
        }

        public static string GetFormatString(TimeSpan span)
        {
            string str = string.Empty;
            if ((span.Days > 0) || (span.Hours > 0))
            {
                int num = (0x18 * span.Days) + span.Hours;
                str = str + num + "&nbsp;小时&nbsp;";
            }
            if (span.Minutes > 0)
            {
                str = str + span.Minutes + "&nbsp;分&nbsp;";
            }
            if (span.Seconds > 0)
            {
                str = str + span.Seconds + "&nbsp;秒&nbsp;";
            }
            return str;
        }

        public static bool IsAccordantBrowser()
        {
            HttpBrowserCapabilities browser = Context().Request.Browser;
            return (!(browser.Browser != "IE") && (float.Parse(browser.Version) >= 5.5));
        }

        public static double UpLoadFileLength()
        {
            int num = 0x3e8;
            return (double) (num * 0x400);
        }

        public static string UploadFolder()
        {
            return FileSystemObject.CreateFileFolder("Temp");
        }
    }
}

