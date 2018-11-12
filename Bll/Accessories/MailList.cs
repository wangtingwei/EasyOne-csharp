namespace EasyOne.Accessories
{
    using EasyOne.Common;
    using System;
    using System.Web;

    public sealed class MailList
    {
        private MailList()
        {
        }

        public static string GetExportFileName(string fileName)
        {
            return (HttpContext.Current.Server.MapPath(@"~\Temp\") + DataSecurity.FilterBadChar(fileName));
        }
    }
}

