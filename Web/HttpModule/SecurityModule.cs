namespace EasyOne.Web.HttpModule
{
    using EasyOne.Accessories;
    using EasyOne.Common;
    using EasyOne.Components;
    using EasyOne.UserManage;
    using EasyOne.Web;
    using EasyOne.Web.Configuration;
    using System;
    using System.Diagnostics;
    using System.Globalization;
    using System.Security.Cryptography;
    using System.Web;
    using System.Web.Configuration;
    using System.Web.Security;
    using System.Reflection;
    /// <summary>
    /// 安全模块
    /// </summary>
    public class SecurityModule : IHttpModule
    {
        private const string ADMIN_LOGINURL = "Login.aspx";
        private const string DEFAULT_MANAGEPATH = "admin";
        private static FileVersionInfo fvInfo = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location);
        private NoCheckAdminLogOnElement m_NoCheckAdminLogOnSection;
        private NoCheckUrlReferrerElement m_NoCheckUrlReferrerSection;

        private void Application_AuthenticateRequest(object sender, EventArgs e)
        {
            FormsAuthenticationTicket ticket = null;
            HttpApplication application = (HttpApplication) sender;
            HttpContext context = application.Context;
            if ((context.Request.Url.ToString().IndexOf("/Install/Default.aspx", StringComparison.OrdinalIgnoreCase) < 0) && (context.Request.Url.ToString().IndexOf("/Install/Upgrade.aspx", StringComparison.OrdinalIgnoreCase) < 0))
            {
                //获取用于存储 Forms 身份验证票证的 Cookie 名称
                string name = FormsAuthentication.FormsCookieName + "AdminCookie";
                ticket = ExtractTicketFromCookie(context, name);
                if (ticket != null)
                {
                    SlidingExpiration(context, ticket, name);
                    AdminPrincipal principal = AdminPrincipal.CreatePrincipal(ticket);
                    if (principal.Identity.IsAuthenticated)
                    {   
                        principal.AdministratorInfo = Administrators.GetAdministratorByAdminName(principal.AdminName);
                        //在这里获取管理员的权限列表
                        principal.Roles = RoleMembers.GetRoleIdListByAdminId(principal.AdministratorInfo.AdminId);
                        PEContext.Current.Admin = principal;
                    }
                }
            }
        }
        /// <summary>
        /// 判断网站是否部署完必
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        private void Application_BeginRequest(object source, EventArgs e)
        {
            string str = WebConfigurationManager.AppSettings["Version"];
            HttpContext context = ((HttpApplication) source).Context;
            if ((context.Request.Url.ToString().IndexOf("/Install/Default.aspx", StringComparison.OrdinalIgnoreCase) < 0) && (context.Request.Url.ToString().IndexOf("/Install/Upgrade.aspx", StringComparison.OrdinalIgnoreCase) < 0))
            {
                if (string.IsNullOrEmpty(str) && context.Request.Url.GetLeftPart(UriPartial.Path).EndsWith(".aspx", StringComparison.OrdinalIgnoreCase))
                {
                    context.Response.Redirect("~/Install/Default.aspx", true);
                }
                string productVersion = fvInfo.ProductVersion;
                string str3 = DataBaseHandle.CurrentVersion();
                if ((str3 == "99.99.99.99") && (str == "0.9.8.0"))
                {
                    str3 = "0.9.8.0";
                }
                if ((productVersion.Length == 7) && (str3.Length == 7))
                {
                    productVersion = productVersion.Remove(productVersion.Length - 2, 2);
                    str3 = str3.Remove(str3.Length - 2, 2);
                    if ((string.IsNullOrEmpty(str3) || (DataConverter.CLng(str3.Replace(".", "")) < DataConverter.CLng(productVersion.Replace(".", "")))) && (context.Request.Url.GetLeftPart(UriPartial.Path).EndsWith(".aspx", StringComparison.OrdinalIgnoreCase) && (context.Request.Url.ToString().IndexOf("Install", StringComparison.CurrentCultureIgnoreCase) < 0)))
                    {
                        context.Response.Redirect("~/Install/Upgrade.aspx", true);
                    }
                }
            }
        }

        private void Application_PostAuthenticateRequest(object sender, EventArgs e)
        {
            HttpApplication application = (HttpApplication) sender;
            HttpContext context = application.Context;
            HttpRequest request = application.Request;
            if ((context.Request.Url.ToString().IndexOf("/Install/Default.aspx", StringComparison.OrdinalIgnoreCase) < 0) && (context.Request.Url.ToString().IndexOf("/Install/Upgrade.aspx", StringComparison.OrdinalIgnoreCase) < 0))
            {
                string managePath = GetManagePath();
                string accessingurl = request.AppRelativeCurrentExecutionFilePath.ToLower(CultureInfo.CurrentCulture);
                if (managePath != "admin")
                {
                    if (EasyOne.Web.Utility.AccessingPath(accessingurl, "~/admin/"))
                    {
                        EasyOne.Web.Utility.ResponseFileNotFound();
                    }
                    if (EasyOne.Web.Utility.AccessingPath(accessingurl, "~/" + managePath + "/"))
                    {
                        accessingurl = accessingurl.Replace("~/" + managePath + "/", "~/admin/");
                    }
                }
                if (EasyOne.Web.Utility.AccessingPath(accessingurl, "~/admin/"))
                {
                    if ((this.m_NoCheckAdminLogOnSection.Mode != NoCheckType.All) && this.NeedCheckAdminLogOn(accessingurl))
                    {
                        string str3 = "~/" + managePath + "/";
                        str3 = request.AppRelativeCurrentExecutionFilePath.Substring(0, str3.Length);
                        if (!PEContext.Current.Admin.Identity.IsAuthenticated)
                        {
                            context.Response.Redirect(str3 + "Login.aspx", true);
                        }
                        if (PEContext.Current.Admin.AdministratorInfo.IsNull)
                        {
                            context.Response.Redirect(str3 + "Login.aspx", true);
                        }
                        if (!PEContext.Current.Admin.AdministratorInfo.EnableMultiLogOn && (PEContext.Current.Admin.AdministratorInfo.RndPassword != PEContext.Current.Admin.RndPassword))
                        {
                            EasyOne.Web.Utility.WriteErrMsg(EasyOne.Web.Utility.GetGlobalErrorString("MultiAdminLoginSystem"), str3 + "Login.aspx");
                        }
                    }
                    if ((this.m_NoCheckUrlReferrerSection.Mode != NoCheckType.All) && this.NeedCheckUrlReferrer(accessingurl))
                    {
                        if ((request.UrlReferrer == null) || (request.UrlReferrer.Host.Length <= 0))
                        {
                            EasyOne.Web.Utility.WriteErrMsg(EasyOne.Web.Utility.GetGlobalErrorString("UrlReferrerIsNull"), string.Empty);
                        }
                        else if (!string.Equals(request.Url.Host, request.UrlReferrer.Host, StringComparison.CurrentCultureIgnoreCase))
                        {
                            EasyOne.Web.Utility.WriteErrMsg(EasyOne.Web.Utility.GetGlobalErrorString("UrlReferrerIsOuter"), string.Empty);
                        }
                    }
                    if (managePath != "admin")
                    {
                        if (accessingurl.EndsWith("/", StringComparison.CurrentCultureIgnoreCase))
                        {
                            accessingurl = accessingurl + "Index.aspx";
                        }
                        context.RewritePath(accessingurl + request.Url.Query);
                    }
                }
            }
        }

        public void Dispose()
        {
        }
        /// <summary>
        /// 提取ticket 从cookie
        /// </summary>
        /// <param name="context"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        private static FormsAuthenticationTicket ExtractTicketFromCookie(HttpContext context, string name)
        {
            FormsAuthenticationTicket ticket = null;
            string encryptedTicket = null;
            HttpCookie cookie = context.Request.Cookies[name];
            if (cookie != null)
            {
                encryptedTicket = cookie.Value;
            }
            if ((encryptedTicket != null) && (encryptedTicket.Length > 1))
            {
                try
                {//创建一个 FormsAuthenticationTicket 对象，此对象将根据传递给该方法的加密的 Forms 身份验证票证而定
                    ticket = FormsAuthentication.Decrypt(encryptedTicket);
                }
                catch (ArgumentException exception1)
                {
                    if (exception1 != null)
                    {
                        return null;
                    }
                }
                catch (CryptographicException)
                {
                    context.Request.Cookies.Remove(name);
                }
                if (ticket != null)
                {
                    if (SiteConfig.SiteOption.TicketTime == 0)
                    {
                        return ticket;
                    }
                    //判断票证是否过期
                    if (!ticket.Expired)
                    {
                        return ticket;
                    }
                }
            }
            return null;
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
            //从当前 Web 应用程序的默认配置文件中检索指定的配置节
            SecuritySection section = (SecuritySection) WebConfigurationManager.GetSection("EasyOne.web/security");
            this.m_NoCheckUrlReferrerSection = section.NoCheckUrlReferrer;
            this.m_NoCheckAdminLogOnSection = section.NoCheckAdminLogOn;
            context.BeginRequest += new EventHandler(this.Application_BeginRequest);//将SecurityModule改为this
            //事件发出信号表示配置的身份验证机制已对当前请求进行了身份验证
            //订阅 AuthenticateRequest 事件可确保在处理附加模块或事件处理程序之前对请求进行身份验证。
            context.AuthenticateRequest += new EventHandler(this.Application_AuthenticateRequest);//将SecurityModule改为this
            //预订 PostAuthenticateRequest 事件的功能可以访问由 PostAuthenticateRequest 处理的任何数据。
            context.PostAuthenticateRequest += new EventHandler(this.Application_PostAuthenticateRequest);
        }

        private bool NeedCheckAdminLogOn(string currentPage)
        {
            return (this.m_NoCheckAdminLogOnSection.Page[currentPage] == null);
        }

        private bool NeedCheckUrlReferrer(string currentPage)
        {
            return (this.m_NoCheckUrlReferrerSection.Page[currentPage] == null);
        }

        private static void SlidingExpiration(HttpContext context, FormsAuthenticationTicket ticket, string cookieName)
        {
            FormsAuthenticationTicket ticket2 = null;
            if (FormsAuthentication.SlidingExpiration)
            {
                ticket2 = FormsAuthentication.RenewTicketIfOld(ticket);
            }
            else
            {
                ticket2 = ticket;
            }
            string str = FormsAuthentication.Encrypt(ticket2);
            HttpCookie cookie = context.Request.Cookies[cookieName];
            if (cookie == null)
            {
                cookie = new HttpCookie(cookieName, str);
                cookie.Path = ticket2.CookiePath;
            }
            if (ticket.IsPersistent)
            {
                cookie.Expires = ticket2.Expiration;
            }
            cookie.Value = str;
            cookie.Secure = FormsAuthentication.RequireSSL;
            cookie.HttpOnly = true;
            if (FormsAuthentication.CookieDomain != null)
            {
                cookie.Domain = FormsAuthentication.CookieDomain;
            }
            context.Response.Cookies.Remove(cookie.Name);
            context.Response.Cookies.Add(cookie);
        }

        public static string ModuleName
        {
            get
            {
                return "SecurityModule";
            }
        }
    }
}

