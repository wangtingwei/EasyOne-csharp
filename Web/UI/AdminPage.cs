namespace EasyOne.Web.UI
{
    using EasyOne.AccessManage;
    using EasyOne.Components;
    using EasyOne.Enumerations;
    using EasyOne.Model.UserManage;
    using EasyOne.UserManage;
    using EasyOne.Web;
    using EasyOne.Web.Configuration;
    using System;
    using System.Globalization;
    using System.IO;
    using System.Web;
    using System.Web.Caching;
    using System.Web.Configuration;
    using System.Xml;
    /// <summary>
    /// 管理员基类主要用来处理后台管理共性业务
    /// </summary>
    public class AdminPage : BasePage
    {
        private static CheckSecurityCodeElement _CheckSecurityCodeElement;
        private const string StyleSheetThemeSessionName = "AdminPage_StyleSheetTheme";
        private const string ThemesDirectoryName = "App_Themes";

        public AdminPage()
        {
            base.Refreshed += new EventHandler(this.AdminPage_Refreshed);
        }

        private void AdminPage_Refreshed(object sender, EventArgs e)
        {
            throw new CustomException(PEExceptionType.RefreshedError);
        }

        public static string AppendSecurityCode(string currenturl)
        {
            return Utility.AppendSecurityCode(currenturl);
        }
        /// <summary>
        /// 用来判断用户对该页面是否有访问权限
        /// </summary>
        private void CheckPagePermission()
        {
            bool flag = false;
            string str = base.Request.AppRelativeCurrentExecutionFilePath.ToLower(CultureInfo.CurrentCulture).Replace("~/admin/", "");
            string strA = "";
            XmlDocument document = SiteCache.Get("CK_System_XmlDocument_FilePermissionConfig") as XmlDocument;//从缓存获取文件权限 
            if (document == null)
            {
                //缓存对象所依赖的文件或目录的路径。当该资源更改时，缓存的对象将过时，并从缓存中移除
                string str3;
                document = new XmlDocument();
                HttpContext current = HttpContext.Current;
                if (current != null)
                {
                    str3 = current.Server.MapPath("~/Config/Security.config");
                }
                else
                {
                    str3 = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config/Security.config");
                }
                try
                {
                    document.Load(str3);
                }
                catch (XmlException exception)
                {
                    WriteErrMsg("Security.config配置文件不符合XML规范，具体错误信息：" + exception.Message);
                }
                // CacheDpendency(string filename)初始化 CacheDependency 类的新实例，它监视文件或目录的更改情况
                SiteCache.Insert("CK_System_XmlDocument_FilePermissionConfig", document, new CacheDependency(str3));
            }
            XmlNode xmlNode = document.SelectSingleNode("security/checkPermissions");
            if (xmlNode == null)
            {
                WriteErrMsg("Security.config配置文件不存在checkPermissions根元素");
            }
            string attributeValue = GetAttributeValue(xmlNode, "mode");
            XmlNodeList list = document.SelectNodes("//*[@url='" + str + "']");
            if ((string.Compare(attributeValue, "All", StringComparison.CurrentCultureIgnoreCase) == 0) && (list.Count <= 0))
            {
                WriteErrMsg("<li>对不起，您没有当前页面的访问权限！</li>");
            }
            if (list.Count > 0)
            {
                foreach (XmlNode node2 in list)
                {
                    string[] strArray4;
                    strA = GetAttributeValue(node2, "operateCode");
                    if (string.Compare(strA, "None", StringComparison.CurrentCultureIgnoreCase) == 0)
                    {
                        flag = true;
                        break;
                    }
                    if (!strA.Contains(","))
                    {
                        goto Label_022D;
                    }
                    string[] strArray = strA.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                    string str5 = GetAttributeValue(node2, "checkType");
                    if (string.IsNullOrEmpty(str5))
                    {
                        str5 = "or";
                    }
                    string str8 = str5;
                    if (str8 != null)
                    {
                        if (!(str8 == "or"))
                        {
                            if (str8 == "and")
                            {
                                goto Label_0200;
                            }
                        }
                        else
                        {
                            foreach (string str6 in strArray)
                            {
                                if (RolePermissions.AccessCheck(str6))
                                {
                                    flag = true;//为真时返回
                                    break;
                                }
                            }
                        }
                    }
                    goto Label_0237;
                Label_0200:
                    strArray4 = strArray;
                    for (int i = 0; i < strArray4.Length; i++)
                    {
                        string operateCode = strArray4[i];
                        if (!RolePermissions.AccessCheck(operateCode))
                        {
                            flag = false;//为假时返回
                            break;
                        }
                    }
                    goto Label_0237;
                Label_022D:
                    if (RolePermissions.AccessCheck(strA))
                    {
                        flag = true;
                    }
                Label_0237:
                    if (flag)
                    {
                        break;
                    }
                }
                if (!flag)
                {
                    WriteErrMsg("<li>对不起，您没有当前页面的访问权限！</li>");
                }
            }
        }
        /// <summary>
        /// 根据xmlNode的属性名获取其值
        /// </summary>
        /// <param name="xmlNode"></param>
        /// <param name="attributeName"></param>
        /// <returns></returns>
        private static string GetAttributeValue(XmlNode xmlNode, string attributeName)
        {
            string str = "";
            if (xmlNode != null)
            {
                XmlAttribute attribute = xmlNode.Attributes[attributeName];
                if (attribute != null)
                {
                    str = attribute.Value;
                }
            }
            return str;
        }

        private static void InitCheckSecurityCodeElement()
        {
            if (_CheckSecurityCodeElement == null)
            {
                SecuritySection section = (SecuritySection) WebConfigurationManager.GetSection("EasyOne.web/security");
                _CheckSecurityCodeElement = section.CheckSecurityCode;
            }
        }

        protected virtual void InitializeSiteMapPath()
        {
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            if (HttpContext.Current.Session != null)
            {
                base.ViewStateUserKey = this.Session.SessionID;
                if (!IsValidSecurityCode)
                {
                    WriteErrMsg("页面安全码校验失败！");
                }
            }
            this.CheckPagePermission();
        }

        protected override void OnLoadComplete(EventArgs e)
        {
            base.OnLoadComplete(e);
            this.InitializeSiteMapPath();
        }

        public static void WriteErrMsg(string errorMessage)
        {
            WriteErrMsg(errorMessage, string.Empty);
        }

        public static void WriteErrMsg(string errorMessage, string returnurl)
        {
            Utility.WriteErrMsg(errorMessage, returnurl);
        }

        public static void WriteSuccessMsg(string successMessage)
        {
            WriteSuccessMsg(successMessage, string.Empty);
        }

        public static void WriteSuccessMsg(string successMessage, string returnurl)
        {
            Utility.WriteSuccessMsg(successMessage, returnurl);
        }

        private static bool IsValidSecurityCode
        {
            get
            {
                InitCheckSecurityCodeElement();
                bool flag = true;
                string filePath = BasePage.AppRelativePageurlWithAction.ToLower(CultureInfo.CurrentCulture);
                if ((_CheckSecurityCodeElement.Page[filePath] != null) && (Utility.GetSecurityCode(filePath) != BasePage.RequestString("sid")))
                {
                    flag = false;
                }
                return flag;
            }
        }

        public string StyleSheetPath
        {
            get
            {
                return (base.BasePath + "App_Themes/" + this.StyleSheetTheme + "/");
            }
        }

        public override string StyleSheetTheme
        {
            get
            {
                if (HttpContext.Current.Session == null)
                {
                    return base.StyleSheetTheme;
                }
                if (this.Session["AdminPage_StyleSheetTheme"] == null)
                {
                    AdminProfileInfo adminProfile = AdminProfile.GetAdminProfile(PEContext.Current.Admin.AdminName);
                    if (adminProfile.IsNull || string.IsNullOrEmpty(adminProfile.Theme))
                    {
                        PagesSection section = (PagesSection) WebConfigurationManager.GetSection("system.web/pages");
                        if (!string.IsNullOrEmpty(section.StyleSheetTheme) && !section.StyleSheetTheme.StartsWith("User", StringComparison.CurrentCultureIgnoreCase))
                        {
                            return section.StyleSheetTheme;
                        }
                        return "AdminDefaultTheme";
                    }
                    this.Session.Add("AdminPage_StyleSheetTheme", adminProfile.Theme);
                }
                return (string) this.Session["AdminPage_StyleSheetTheme"];
            }
        }
    }
}

