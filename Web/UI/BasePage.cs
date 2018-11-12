namespace EasyOne.Web.UI
{
    using EasyOne.Components;
    using EasyOne.Web;
    using System;
    using System.Globalization;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public class BasePage : Page
    {
        private static readonly object EventRefreshed = new object();
        //声明事件Refreshed
        public event EventHandler Refreshed
        {
            add
            {
                base.Events.AddHandler(EventRefreshed, value);
            }
            remove
            {
                base.Events.RemoveHandler(EventRefreshed, value);
            }
        }

        public static string CombineRawurl(string originalurl)
        {
            return Utility.CombineRawUrl(originalurl);
        }

        public static string EnumToHtml<T>(T enumValue) where T: struct
        {
            return Utility.EnumToHtml<T>(enumValue);
        }

        public static string GetGlobalCacheString(string resourceKey)
        {
            return Utility.GetGlobalCacheString(resourceKey);
        }

        public static string GetGlobalEnumString(string resourceKey)
        {
            return Utility.GetGlobalEnumString(resourceKey);
        }

        public static string GetGlobalErrorString(string resourceKey)
        {
            return Utility.GetGlobalErrorString(resourceKey);
        }

        public static string GetGlobalString(string classKey, string resourceKey)
        {
            return Utility.GetGlobalString(classKey, resourceKey);
        }

        public static int GetSelectedIndexByValue(ListControl listControl, string selectValue)
        {
            return Utility.GetSelectedIndexByValue(listControl, selectValue);
        }

        public virtual void OnRefreshed(EventArgs e)
        {
            EventHandler handler = base.Events[EventRefreshed] as EventHandler;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public static int RequestInt32(string queryItem)
        {
            return Utility.RequestInt32(queryItem, 0);
        }

        public static int RequestInt32(string queryItem, int defaultValue)
        {
            return Utility.RequestInt32(queryItem, defaultValue);
        }

        public static string RequestString(string queryItem)
        {
            return Utility.RequestString(queryItem, string.Empty);
        }

        public static string RequestString(string queryItem, string defaultValue)
        {
            return Utility.RequestString(queryItem, defaultValue);
        }

        public static string RequestStringToLower(string queryItem)
        {
            return Utility.RequestStringToLower(queryItem, string.Empty);
        }

        public static string RequestStringToLower(string queryItem, string defaultValue)
        {
            return Utility.RequestStringToLower(queryItem, defaultValue);
        }

        public static void ResponseRedirect(string redirecturl)
        {
            Utility.ResponseRedirect(redirecturl, true);
        }

        public static void ResponseRedirect(string redirecturl, bool endResponse)
        {
            Utility.ResponseRedirect(redirecturl, endResponse);
        }

        public static void SetSelectedIndexByValue(ListControl listControl, string selectValue)
        {
            Utility.SetSelectedIndexByValue(listControl, selectValue);
        }

        public static string AppRelativePageurlWithAction
        {
            get
            {
                string appRelativeCurrentExecutionFilePath = HttpContext.Current.Request.AppRelativeCurrentExecutionFilePath;
                string str2 = RequestString("Action");
                if (!string.IsNullOrEmpty(str2))
                {
                    appRelativeCurrentExecutionFilePath = appRelativeCurrentExecutionFilePath + "?Action=" + str2;
                }
                return appRelativeCurrentExecutionFilePath;
            }
        }

        public string BasePath
        {
            get
            {
                return Utility.GetBasePath(base.Request);
            }
        }

        public string FullBasePath
        {
            get
            {
                return (base.Request.Url.AbsoluteUri.Replace(base.Request.Url.PathAndQuery, string.Empty) + this.BasePath);
            }
        }

        public virtual bool IsCheckRefreshed
        {
            get
            {
                return false;
            }
        }

        public static bool IseShop
        {
            get
            {
                return (string.Compare(SiteConfig.SiteInfo.ProductEdition, "eShop", true, CultureInfo.CurrentCulture) == 0);
            }
        }

        public static bool IsRefreshed
        {
            get
            {
                object obj2 = HttpContext.Current.Items["IsRefreshed"];
                if (obj2 == null)
                {
                    return false;
                }
                return (bool) obj2;
            }
        }
    }
}

