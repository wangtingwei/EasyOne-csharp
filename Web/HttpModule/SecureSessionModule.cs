namespace EasyOne.Web.HttpModule
{
    using EasyOne.Components;
    using System;
    using System.Configuration;
    using System.Security.Cryptography;
    using System.Text;
    using System.Web;

    public class SecureSessionModule : IHttpModule
    {
        private static string _ValidationKey;

        private void Application_BeginRequest(object sender, EventArgs e)
        {
            HttpRequest request = ((HttpApplication) sender).Request;
            HttpCookie cookie = GetCookie(request, "ASP.NET_SessionId");
            if (cookie != null)
            {
                if (cookie.Value.Length <= 0x18)
                {
                    throw new InvalidSessionException();
                }
                string id = cookie.Value.Substring(0, 0x18);
                string strA = cookie.Value.Substring(0x18);
                string strB = GetSessionIDMac(id, PEContext.Current.UserHostAddress, request.UserAgent, _ValidationKey);
                if (string.CompareOrdinal(strA, strB) != 0)
                {
                    throw new InvalidSessionException();
                }
                cookie.Value = id;
            }
        }

        private void Application_EndRequest(object sender, EventArgs e)
        {
            HttpApplication application = (HttpApplication) sender;
            HttpRequest request = application.Request;
            HttpCookie cookie = GetCookie(application.Response, "ASP.NET_SessionId");
            if (cookie != null)
            {
                cookie.Value = cookie.Value + GetSessionIDMac(cookie.Value, request.UserHostAddress, request.UserAgent, _ValidationKey);
            }
        }

        public void Dispose()
        {
        }

        private static HttpCookie FindCookie(HttpCookieCollection cookies, string name)
        {
            int count = cookies.Count;
            for (int i = 0; i < count; i++)
            {
                if (string.Compare(cookies[i].Name, name, StringComparison.CurrentCultureIgnoreCase) == 0)
                {
                    return cookies[i];
                }
            }
            return null;
        }

        private static HttpCookie GetCookie(HttpRequest request, string name)
        {
            return FindCookie(request.Cookies, name);
        }

        private static HttpCookie GetCookie(HttpResponse response, string name)
        {
            return FindCookie(response.Cookies, name);
        }

        private static string GetSessionIDMac(string id, string ip, string agent, string key)
        {
            StringBuilder builder = new StringBuilder(id, 0x200);
            builder.Append(ip.Substring(0, ip.IndexOf('.', ip.IndexOf('.') + 1)));
            builder.Append(agent);
            using (HMACSHA1 hmacsha = new HMACSHA1(Encoding.UTF8.GetBytes(key)))
            {
                return Convert.ToBase64String(hmacsha.ComputeHash(Encoding.UTF8.GetBytes(builder.ToString())));
            }
        }

        private static string GetValidationKey()
        {
            string str = ConfigurationManager.AppSettings["SessionValidationKey"];
            if (string.IsNullOrEmpty(str))
            {
                throw new InvalidSessionException("SessionValidationKey missing");
            }
            return str;
        }

        public void Init(HttpApplication context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }
            if (_ValidationKey == null)
            {
                _ValidationKey = GetValidationKey();
            }
            context.BeginRequest += new EventHandler(this.Application_BeginRequest);
            context.EndRequest += new EventHandler(this.Application_EndRequest);
        }

        public static string ModuleName
        {
            get
            {
                return "SecureSessionModule";
            }
        }
    }
}

