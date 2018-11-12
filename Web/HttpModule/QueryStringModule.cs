namespace EasyOne.Web.HttpModule
{
    using EasyOne.Common;
    using EasyOne.Components;
    using EasyOne.Web;
    using EasyOne.Web.Configuration;
    using System;
    using System.Collections.Specialized;
    using System.Globalization;
    using System.IO;
    using System.Web;
    using System.Web.Configuration;

    public class QueryStringModule : IHttpModule
    {
        private static string _ErrorMessage;
        private QueryStringsSection m_QueryStringsSection;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Application_BeginRequest(object sender, EventArgs e)
        {
            HttpApplication application = (HttpApplication) sender;
            HttpRequest request = application.Request;
            //获取应用程序根的虚拟路径，并通过对应用程序根使用波形符 (~) 表示法（例如，以“~/page.aspx”的形式）使该路径成为相对路径。
            string path = request.AppRelativeCurrentExecutionFilePath.ToLower(CultureInfo.CurrentCulture);
            string extension = Path.GetExtension(path);
            string managePath = GetManagePath();
            if (!path.Contains("/" + managePath + "/") && (string.Compare(".aspx", extension, true) == 0))
            {
                PageElement pageElement = this.m_QueryStringsSection.Page[path];
                if ((pageElement != null) && (!Validate(request.QueryString, pageElement) && pageElement.AbortOnError))
                {
                    EasyOne.Web.Utility.WriteUserErrMsg(_ErrorMessage, SiteConfig.SiteInfo.VirtualPath + "Default.aspx");
                }
            }
        }

        private static bool CheckParamValue(ParamElement paramElement, string paramValue)
        {
            ParamType dataType = paramElement.DataType;
            if (!string.IsNullOrEmpty(paramValue))
            {
                if (string.Compare(paramValue, "null", true) == 0)
                {
                    return true;
                }
                if (((dataType == ParamType.String) && (paramElement.Length > 0)) && (paramValue.Length > paramElement.Length))
                {
                    return false;
                }
                if ((dataType == ParamType.Int) && !DataValidator.IsNumberSign(paramValue))
                {
                    return false;
                }
                if ((dataType == ParamType.Bool) && !(((string.Equals(paramValue, "yes", StringComparison.OrdinalIgnoreCase) | string.Equals(paramValue, "no", StringComparison.OrdinalIgnoreCase)) | string.Equals(paramValue, "true", StringComparison.OrdinalIgnoreCase)) | string.Equals(paramValue, "false", StringComparison.OrdinalIgnoreCase)))
                {
                    return false;
                }
            }
            return true;
        }

        public void Dispose()
        {
        }
        /// <summary>
        /// 获取管理员文件夹
        /// </summary>
        /// <returns></returns>
        private static string GetManagePath()
        {
            return SiteConfig.SiteOption.ManageDir.ToLower(CultureInfo.CurrentCulture);
        }

        public void Init(HttpApplication context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }
            context.BeginRequest += new EventHandler(this.Application_BeginRequest);
            this.m_QueryStringsSection = (QueryStringsSection) WebConfigurationManager.GetSection("EasyOne.web/queryStrings");
        }

        private static bool Validate(NameValueCollection postedQueryString, PageElement pageElement)
        {
            if (pageElement != null)
            {
                if (postedQueryString.Count > pageElement.Param.Count)
                {
                    _ErrorMessage = EasyOne.Web.Utility.GetGlobalErrorString("PageParamMoreThen");
                    return false;
                }
                for (int i = 0; i <= (postedQueryString.Count - 1); i++)
                {
                    string str = postedQueryString.Keys[i];
                    if (!string.IsNullOrEmpty(str))
                    {
                        str = str.ToLower(CultureInfo.CurrentCulture);
                    }
                    if (!pageElement.Param.Contains(str))
                    {
                        _ErrorMessage = EasyOne.Web.Utility.GetGlobalErrorString("PageParamNotContains");
                        return false;
                    }
                }
                for (int j = 0; j <= (pageElement.Param.Count - 1); j++)
                {
                    ParamElement paramElement = pageElement.Param[j];
                    string paramValue = postedQueryString[paramElement.Name];
                    if (paramValue == null)
                    {
                        if (!paramElement.Optional)
                        {
                            _ErrorMessage = EasyOne.Web.Utility.GetGlobalErrorString("PageParamLessThen");
                            return false;
                        }
                    }
                    else if (!CheckParamValue(paramElement, paramValue))
                    {
                        _ErrorMessage = EasyOne.Web.Utility.GetGlobalErrorString("PageParamUnequal");
                        return false;
                    }
                }
            }
            return true;
        }

        public static string ModuleName
        {
            get
            {
                return "QueryStringModule";
            }
        }
    }
}

