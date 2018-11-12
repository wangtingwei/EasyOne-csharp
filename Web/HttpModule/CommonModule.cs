namespace EasyOne.Web.HttpModule
{
    using EasyOne.Components;
    using EasyOne.Enumerations;
    using EasyOne.Model.UserManage;
    using EasyOne.StaticHtml;
    using EasyOne.UserManage;
    using EasyOne.Web;
    using System;
    using System.Globalization;
    using System.Security.Cryptography;
    using System.Security.Principal;
    using System.Web;
    using System.Web.Configuration;
    using System.Web.Security;

    public class CommonModule : IHttpModule
    {
        private static void Application_AuthenticateRequest(object sender, EventArgs e)
        {
            HttpApplication application = (HttpApplication) sender;
            HttpContext context = application.Context;
            if ((context.Request.Url.ToString().IndexOf("/Install/Default.aspx", StringComparison.OrdinalIgnoreCase) < 0) && (context.Request.Url.ToString().IndexOf("/Install/Upgrade.aspx", StringComparison.OrdinalIgnoreCase) < 0))
            {
                FormsAuthenticationTicket ticket = null;
                //获取用于FORM身份验证票证
                string formsCookieName = FormsAuthentication.FormsCookieName;
                HttpCookie cookie = context.Request.Cookies[formsCookieName];
                if (cookie == null)
                {
                    //使用新的验证标识创建用户基本功能对象
                    UserPrincipal principal = new UserPrincipal(new AnonymousAuthenticateIdentity());
                    principal.UserInfo = new UserInfo(true);
                    principal.UserInfo.GroupId = -2;
                    principal.UserInfo.IsInheritGroupRole = true;
                    PEContext.Current.User = principal;
                }
                else
                {
                    try
                    {
                        //根据票证传递的参数而获取票证
                        ticket = FormsAuthentication.Decrypt(cookie.Value);
                    }
                    catch (ArgumentException)
                    {
                        return;
                    }
                    catch (CryptographicException)
                    {
                        //移除Forms验证Cookies
                        context.Request.Cookies.Remove(formsCookieName);
                    }
                    if (ticket != null)
                    {
                        UserPrincipal principal2 = UserPrincipal.CreatePrincipal(ticket);
                        if (principal2.Identity.IsAuthenticated)
                        {
                            principal2.UserInfo = Users.GetUsersByUserName(principal2.UserName);
                            UserPurviewInfo userPurview = principal2.UserInfo.UserPurview;
                            principal2.PurviewInfo = userPurview;
                            PEContext.Current.User = principal2;
                            FormsIdentity identity = new FormsIdentity(ticket);
                            GenericPrincipal principal3 = new GenericPrincipal(identity, new string[] { principal2.RoleId.ToString(CultureInfo.CurrentCulture) });
                            context.User = principal3;
                        }
                        else
                        {
                            GenericPrincipal principal4 = new GenericPrincipal(new NoAuthenticateIdentity(), null);
                            context.User = principal4;
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 验证用户是否通过登录验证
        /// </summary>
        /// <param name="context"></param>
        private static void CheckUserLogin(HttpContext context)
        {
            bool flag = true;
            if (!context.Request.Url.GetLeftPart(UriPartial.Path).EndsWith("ajax.aspx", StringComparison.OrdinalIgnoreCase) && !context.Request.Url.GetLeftPart(UriPartial.Path).EndsWith("login.aspx", StringComparison.OrdinalIgnoreCase))
            {
                //配置WEB应用程序授权
                AuthorizationSection section = (AuthorizationSection) context.GetSection("system.web/authorization");
                if (((section.Rules.Count > 0) && (section.Rules[0].Action == AuthorizationRuleAction.Allow)) && section.Rules[0].Users.Contains("*"))
                {
                    flag = false;
                }
            }
            if (flag && context.Request.Url.GetLeftPart(UriPartial.Path).EndsWith(".aspx", StringComparison.OrdinalIgnoreCase))
            {
                //如果用户的验证代号通过
                if (PEContext.Current.User.Identity.IsAuthenticated)
                {
                    bool flag2 = false;
                    UserInfo userInfo = PEContext.Current.User.UserInfo;
                    if (userInfo.Status != UserStatus.None)
                    {
                        Utility.WriteUserErrMsg(Utility.GetGlobalErrorString("UserIsNotApprove"), "~/Default.aspx");
                    }
                    if (!SiteConfig.UserConfig.EnableMultiLogOn && (PEContext.Current.User.LastPassword != userInfo.LastPassword))
                    {
                        if (context.Request.Url.GetLeftPart(UriPartial.Path).EndsWith("ajax.aspx", StringComparison.OrdinalIgnoreCase))
                        {
                            context.Items["err"] = "err";
                            context.Server.Transfer("~/ajax.aspx");
                        }
                        else
                        {
                            Utility.WriteUserErrMsg(Utility.GetGlobalErrorString("MultiUserLoginSystem"), "");
                        }
                    }
                    if (SiteConfig.UserConfig.PresentExpPerLogOn > 0.0)
                    {
                        bool flag3 = false;
                        if (!userInfo.LastPresentTime.HasValue)
                        {
                            flag3 = true;
                        }
                        else
                        {
                            TimeSpan span = (TimeSpan) (DateTime.Now - userInfo.LastPresentTime.Value);
                            if (span.TotalDays >= 1.0)
                            {
                                flag3 = true;
                            }
                        }
                        if (flag3)
                        {
                            userInfo.UserExp += (int) SiteConfig.UserConfig.PresentExpPerLogOn;
                            userInfo.LastPresentTime = new DateTime?(DateTime.Now);
                            flag2 = true;
                        }
                    }
                    if ((context.Session != null) && (context.Session["UserName"] == null))
                    {
                        userInfo.LogOnTimes++;
                        userInfo.LastLogOnTime = new DateTime?(DateTime.Now);
                        userInfo.LastLogOnIP = PEContext.Current.UserHostAddress;
                        flag2 = true;
                        context.Session.Add("UserName", PEContext.Current.User.UserName);
                    }
                    if (!userInfo.LastLogOnTime.HasValue)
                    {
                        userInfo.LastLogOnTime = new DateTime?(DateTime.Now);
                    }
                    if (flag2)
                    {
                        Users.Update(userInfo);
                    }
                }
            }
            else if (PEContext.Current.User.Identity.IsAuthenticated && (PEContext.Current.User.UserInfo.Status != UserStatus.None))
            {
                UserPrincipal principal = new UserPrincipal(new AnonymousAuthenticateIdentity());
                principal.UserInfo = new UserInfo(true);
                principal.UserInfo.GroupId = -2;
                principal.UserInfo.IsInheritGroupRole = true;
                PEContext.Current.User = principal;
                GenericPrincipal principal2 = new GenericPrincipal(new NoAuthenticateIdentity(), null);
                context.User = principal2;
                FormsAuthentication.SignOut();
            }
        }

        private static void context_PreRequestHandlerExecute(object sender, EventArgs e)
        {
            HttpApplication application = (HttpApplication) sender;
            HttpContext context = application.Context;
            if ((context.Request.Url.ToString().IndexOf("/Install/Default.aspx", StringComparison.OrdinalIgnoreCase) < 0) && (context.Request.Url.ToString().IndexOf("/Install/Upgrade.aspx", StringComparison.OrdinalIgnoreCase) < 0))
            {
                CheckUserLogin(context);
            }
        }

        public void Dispose()
        {
            Jobs.Instance().Stop();
        }

        public void Init(HttpApplication context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }
            //配置Web应用程序的身份验证，无法继承此类
            AuthenticationSection section = (AuthenticationSection) WebConfigurationManager.GetSection("system.web/authentication");
            //获取或设置身份验证模式，此处用于判断身份验证模式是否为Forms
            if (section.Mode == AuthenticationMode.Forms)
            {
                //当安全模块已建立用户标识时发生
                context.AuthenticateRequest += new EventHandler(CommonModule.Application_AuthenticateRequest);
                //恰好在ASP.NET开始执行事件处理程序时发生
                context.PreRequestHandlerExecute += new EventHandler(CommonModule.context_PreRequestHandlerExecute);
            }
            SiteConfigInfo config = SiteConfig.ConfigInfo();
            string virtualPath = config.SiteInfo.VirtualPath;
            if (string.IsNullOrEmpty(virtualPath) || (string.Compare(virtualPath, VirtualPathUtility.AppendTrailingSlash(HttpContext.Current.Request.ApplicationPath), true, CultureInfo.CurrentCulture) != 0))
            {
                config.SiteInfo.VirtualPath = HttpContext.Current.Request.ApplicationPath;
                new SiteConfig().Update(config);
            }
            Jobs.Instance().Start();
        }

        public static string ModuleName
        {
            get
            {
                return "CommonModule";
            }
        }
    }
}

