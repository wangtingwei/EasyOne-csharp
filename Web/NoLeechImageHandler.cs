namespace EasyOne.Web
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Web;

    public class NoLeechImageHandler : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }
            HttpRequest request = context.Request;
            string physicalPath = request.PhysicalPath;
            if (((request.UrlReferrer != null) && (request.UrlReferrer.Host.Length > 0)) && (CultureInfo.InvariantCulture.CompareInfo.Compare(request.Url.Host, request.UrlReferrer.Host, CompareOptions.IgnoreCase) != 0))
            {
                physicalPath = context.Server.MapPath("~/images/nopic.gif");
            }
            string str2 = null;
            string str4 = Path.GetExtension(physicalPath).ToLower(CultureInfo.CurrentCulture).ToString();
            if (str4 != null)
            {
                if (!(str4 == ".gif"))
                {
                    if (str4 == ".jpg")
                    {
                        str2 = "image/jpeg";
                    }
                    else if (str4 == ".png")
                    {
                        str2 = "image/png";
                    }
                }
                else
                {
                    str2 = "image/gif";
                }
            }
            if (!File.Exists(physicalPath))
            {
                context.Response.StatusCode = 0x194;
            }
            else
            {
                context.Response.StatusCode = 200;
                context.Response.ContentType = str2;
                context.Response.WriteFile(physicalPath);
            }
        }

        public bool IsReusable
        {
            get
            {
                return true;
            }
        }
    }
}

