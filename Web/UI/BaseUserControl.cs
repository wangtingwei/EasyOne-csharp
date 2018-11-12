namespace EasyOne.Web.UI
{
    using EasyOne.Components;
    using EasyOne.Web;
    using System;
    using System.Globalization;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public class BaseUserControl : UserControl
    {
        public static string AppendSecurityCode(string currenturl)
        {
            return Utility.AppendSecurityCode(currenturl);
        }

        public static string EnumToHtml<T>(T enumValue) where T: struct
        {
            return Utility.EnumToHtml<T>(enumValue);
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

        public static void WriteErrMsg(string errorMessage)
        {
            Utility.WriteErrMsg(errorMessage, string.Empty);
        }

        public static void WriteErrMsg(string errorMessage, string returnurl)
        {
            Utility.WriteErrMsg(errorMessage, returnurl);
        }

        public static void WriteSuccessMsg(string successMessage)
        {
            Utility.WriteSuccessMsg(successMessage, string.Empty);
        }

        public static void WriteSuccessMsg(string successMessage, string returnurl)
        {
            Utility.WriteSuccessMsg(successMessage, returnurl);
        }

        public string BasePath
        {
            get
            {
                return Utility.GetBasePath(base.Request);
            }
        }

        public static bool IseShop
        {
            get
            {
                return (string.Compare(SiteConfig.SiteInfo.ProductEdition, "eShop", true, CultureInfo.CurrentCulture) == 0);
            }
        }
    }
}

