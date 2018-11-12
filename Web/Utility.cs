namespace EasyOne.Web
{
    using EasyOne.Common;
    using System;
    using System.Collections.Specialized;
    using System.Globalization;
    using System.IO;
    using System.Security.Cryptography;
    using System.Text;
    using System.Web;
    using System.Web.UI.WebControls;
    //面面公用类
    internal sealed class Utility
    {
        private Utility()
        {
        }
        /// <summary>
        /// 访问路径是否正确
        /// <param name="accessingurl">访问路径URL</param>
        /// <param name="path">路径开头</param>
        /// </summary>
        public static bool AccessingPath(string accessingurl, string path)
        {
            bool flag = accessingurl.StartsWith(path, StringComparison.CurrentCultureIgnoreCase);
            bool flag2 = accessingurl.EndsWith("aspx", StringComparison.CurrentCultureIgnoreCase);
            bool flag3 = accessingurl.EndsWith("/", StringComparison.CurrentCultureIgnoreCase);
            if (!flag)
            {
                return false;
            }
            if (!flag2)
            {
                return flag3;
            }
            return true;
        }
        /// <summary>
        /// 为地址添加安全码
        /// </summary>
        /// <param name="currenturl"></param>
        /// <returns></returns>
        public static string AppendSecurityCode(string currenturl)
        {
            if (!string.IsNullOrEmpty(currenturl))
            {
                if (currenturl.IndexOf("?", StringComparison.CurrentCultureIgnoreCase) < 0)
                {
                    currenturl = currenturl + "?sid=" + GetSecurityCode(currenturl);
                    return currenturl;
                }
                if ((currenturl.IndexOf("&sid=", StringComparison.CurrentCultureIgnoreCase) < 0) && (currenturl.IndexOf("?sid=", StringComparison.CurrentCultureIgnoreCase) < 0))
                {
                    currenturl = currenturl + "&sid=" + GetSecurityCode(currenturl);
                }
            }
            return currenturl;
        }
        /// <summary>
        /// 生成完整的URL
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string CombineRawUrl(string url)
        {
            if ((url[0] != '~') && (url.IndexOf(':') < 0))
            {
                string rawUrl = HttpContext.Current.Request.RawUrl;
                if (rawUrl.IndexOf('?') > 0)
                {
                    rawUrl = rawUrl.Split(new char[] { '?' })[0];
                }
                if (url.IndexOf('?') > 0)
                {
                    string[] strArray = url.Split(new char[] { '?' });
                    url = VirtualPathUtility.Combine(rawUrl, strArray[0]) + "?" + strArray[1];
                    return url;
                }
                url = VirtualPathUtility.Combine(rawUrl, url);
            }
            return url;
        }

        public static string EnumToHtml<T>(T enumValue) where T: struct
        {
            string str = enumValue.ToString();
            string name = enumValue.GetType().Name;
            if (!str.Contains(","))
            {
                return GetGlobalEnumString(name + "_" + str);
            }
            StringBuilder builder = new StringBuilder();
            foreach (string str3 in str.Split(new string[] { "," }, StringSplitOptions.None))
            {
                string resourceKey = name + "_" + str3.Trim();
                if (builder.Length > 0)
                {
                    builder.Append(",");
                }
                builder.Append(GetGlobalEnumString(resourceKey));
            }
            return builder.ToString();
        }
        /// <summary>
        /// 获取文件执行目录 将"/" 追加到虚拟路径后面(如果路径结尾尚不存在"/")
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static string GetBasePath(HttpRequest request)
        {
            if (request == null)
            {
                return "/";
            }
            return VirtualPathUtility.AppendTrailingSlash(request.ApplicationPath);
        }

        public static string GetGlobalCacheString(string resourceKey)
        {
            return GetGlobalString("CacheResources", resourceKey);
        }

        public static string GetGlobalEnumString(string resourceKey)
        {
            return GetGlobalString("EnumResources", resourceKey);
        }

        public static string GetGlobalErrorString(string resourceKey)
        {
            return GetGlobalString("ErrorMessage", resourceKey);
        }
        /// <summary>
        /// 根据指定的 ClassKey 和 ResourceKey 属性，获取应用程序级别资源对象。
        /// </summary>
        /// <param name="classKey"></param>
        /// <param name="resourceKey"></param>
        /// <returns></returns>
        public static string GetGlobalString(string classKey, string resourceKey)
        {
            string str = (string) HttpContext.GetGlobalResourceObject(classKey, resourceKey, CultureInfo.CurrentCulture);
            if (str == null)
            {
                str = string.Empty;
            }
            return str;
        }
        /// <summary>
        /// 获取安全代码
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>

        public static string GetSecurityCode(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                return string.Empty;
            }
            string str = "";
            string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(filePath);
            int index = filePath.IndexOf("?", StringComparison.CurrentCultureIgnoreCase);
            if (index > 0)
            {///使用指定的 Encoding 将查询字符串分析成一个 NameValueCollection
                str = HttpUtility.ParseQueryString(filePath.Substring(index, filePath.Length - index))["Action"];
            }
            string s = fileNameWithoutExtension.ToLower(CultureInfo.CurrentCulture) + str.ToLower(CultureInfo.CurrentCulture) + HttpContext.Current.Session.SessionID;
            ///使用 SHA1 哈希函数计算基于哈希值的消息验证代码 (HMAC)。
            HMACSHA1 hmacsha = new HMACSHA1(Encoding.UTF8.GetBytes(s));
            ///计算指定字节数组的哈希值
            return BitConverter.ToString(hmacsha.ComputeHash(Encoding.UTF8.GetBytes(s))).Replace("-", "").ToLower(CultureInfo.CurrentCulture);
        }
        /// <summary>
        /// 根据value获取选中的索引
        /// </summary>
        /// <param name="listControl"></param>
        /// <param name="selectValue"></param>
        /// <returns></returns>
        public static int GetSelectedIndexByValue(ListControl listControl, string selectValue)
        {
            int index = -1;
            if (listControl != null)
            {
                index = listControl.Items.IndexOf(listControl.Items.FindByValue(selectValue));
            }
            return index;
        }
        /// <summary>
        /// 生成分页链接
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        public static string RebuildPageName(string filename, NameValueCollection query)
        {
            if (string.IsNullOrEmpty(filename))
            {
                return string.Empty;
            }
            string[] strArray = filename.Split(new char[] { '/' });
            if (strArray.Length > 0)
            {
                filename = strArray[strArray.Length - 1];
            }
            if (filename.IndexOf('?') > 0)
            {
                filename = filename.Substring(0, filename.IndexOf('?') - 1);
            }
            StringBuilder builder = new StringBuilder(filename);
            if (query.Count > 0)
            {
                bool flag = false;
                for (int i = 0; i < query.Count; i++)
                {
                    if (i == 0)
                    {
                        builder.Append("?");
                    }
                    else
                    {
                        builder.Append("&");
                    }
                    if (query.GetKey(i) == "page")
                    {
                        builder.Append("page={$pageid/}");
                        flag = true;
                    }
                    else
                    {
                        builder.Append(query.GetKey(i) + "=" + HttpUtility.UrlEncode(DataSecurity.FilterBadChar(query.Get(i))));
                    }
                }
                if (!flag)
                {
                    if (builder.Length > filename.Length)
                    {
                        builder.Append("&page={$pageid/}");
                    }
                    else
                    {
                        builder.Append("?page={$pageid/}");
                    }
                }
            }
            else
            {
                builder.Append("?page={$pageid/}");
            }
            return builder.ToString();
        }
        /// <summary>
        /// 将请求参数转换成Int32 如果为空或不能转换则赋值默认值
        /// </summary>
        /// <param name="queryItem">URL的请求参数</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>int整形</returns>
        public static int RequestInt32(string queryItem, int defaultValue)
        {
            return DataConverter.CLng(HttpContext.Current.Request.QueryString[queryItem], defaultValue);
        }
        /// <summary>
        /// 获取request的查询参数，如果该参数值为空，则返回默认值
        /// </summary>
        /// <param name="queryItem">QueryString参数</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>string字符串</returns>
        public static string RequestString(string queryItem, string defaultValue)
        {
            string str = HttpContext.Current.Request.QueryString[queryItem];
            if (str == null)
            {
                return defaultValue;
            }
            return str.Trim();
        }
        /// <summary>
        /// 获取request的查询参数，如果该参数值为空，则返回默认值
        /// </summary>
        /// <param name="queryItem">QueryString参数</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>string字符串</returns>
        public static string RequestStringToLower(string queryItem, string defaultValue)
        {
            string str = HttpContext.Current.Request.QueryString[queryItem];
            if (str == null)
            {
                return defaultValue.ToLower(CultureInfo.CurrentCulture);
            }
            return str.Trim().ToLower(CultureInfo.CurrentCulture);
        }
        /// <summary>
        ///打印文件没找到 
        /// </summary>
        public static void ResponseFileNotFound()
        {
            HttpContext.Current.Server.Transfer("NonexistentPage.aspx");
            HttpContext.Current.Response.End();
        }

        public static void ResponseRedirect(string redirecturl, bool endResponse)
        {
            redirecturl = CombineRawUrl(redirecturl);
            if (Encoding.Default.GetBytes(redirecturl).Length > 0x76c)
            {
                WriteErrMsg("<li>对不起，您的URL地址长度超出了浏览器的限制！</li>", "");
            }
            HttpContext.Current.Response.Redirect(redirecturl, endResponse);
        }
        /// <summary>
        /// 设置列表类型控件SelectedIndexs
        /// </summary>
        /// <param name="listControl"></param>
        /// <param name="selectValue"></param>
        public static void SetSelectedIndexByValue(ListControl listControl, string selectValue)
        {
            if (listControl != null)
            {
                listControl.SelectedIndex = listControl.Items.IndexOf(listControl.Items.FindByValue(selectValue));
            }
        }
        /// <summary>
        /// 打印错误信息
        /// </summary>
        /// <param name="errorMessage"></param>
        /// <param name="returnurl"></param>
        public static void WriteErrMsg(string errorMessage, string returnurl)
        {
            HttpContext.Current.Items["ErrorMessage"] = errorMessage;
            HttpContext.Current.Items["ReturnUrl"] = returnurl;
            HttpContext.Current.Server.Transfer("~/Admin/Prompt/ShowError.aspx");
        }
        /// <summary>
        /// 提示信息
        /// </summary>
        /// <param name="message"></param>
        /// <param name="returnurl"></param>
        /// <param name="messageTitle"></param>
        public static void WriteMessage(string message, string returnurl, string messageTitle)
        {
            HttpContext.Current.Items["Message"] = message;
            HttpContext.Current.Items["ReturnUrl"] = returnurl;
            HttpContext.Current.Items["MessageTitle"] = messageTitle;
            HttpContext.Current.Server.Transfer("~/Admin/Prompt/ShowMessage.aspx");
        }
        /// <summary>
        /// 提示成功信息
        /// </summary>
        /// <param name="errorMessage"></param>
        /// <param name="returnurl"></param>
        public static void WriteSuccessMsg(string successMessage, string returnurl)
        {
            HttpContext.Current.Items["SuccessMessage"] = successMessage;
            HttpContext.Current.Items["ReturnUrl"] = returnurl;
            HttpContext.Current.Server.Transfer("~/Admin/Prompt/ShowSuccess.aspx");
        }
        /// <summary>
        /// 提示成功信息
        /// </summary>
        /// <param name="errorMessage"></param>
        /// <param name="returnurl"></param>
        public static void WriteUserErrMsg(string errorMessage, string returnurl)
        {
            HttpContext.Current.Items["ErrorMessage"] = errorMessage;
            HttpContext.Current.Items["ReturnUrl"] = returnurl;
            HttpContext.Current.Server.Transfer("~/Prompt/ShowError.aspx");
        }
        /// <summary>
        ///提示成功信息 
        /// </summary>
        /// <param name="successMessage"></param>
        /// <param name="returnurl"></param>
        public static void WriteUserSuccessMsg(string successMessage, string returnurl)
        {
            HttpContext.Current.Items["SuccessMessage"] = successMessage;
            HttpContext.Current.Items["ReturnUrl"] = returnurl;
            HttpContext.Current.Server.Transfer("~/Prompt/ShowSuccess.aspx");
        }
    }
}

