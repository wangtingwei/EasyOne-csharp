namespace EasyOne.WebSite.Admin
{
    using EasyOne.Common;
    using EasyOne.Components;
    using EasyOne.Controls;
    using EasyOne.Enumerations;
    using EasyOne.Logging;
    using EasyOne.Model.UserManage;
    using EasyOne.UserManage;
    using EasyOne.Web.UI;
    using System;
    using System.Web;
    using System.Web.Security;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public partial class LogOn : AdminPage
    {
        protected void ChangeID()
        {
            this.TxtUserName.ID = this.Getnamestring();
            this.TxtPassword.ID = this.Getpassstring();
        }
        protected string Getnamestring()
        {
            string str = "";
            Random random = new Random();
            for (int i = 0; i < 8; i++)
            {
                char ch;
                int num = random.Next();
                if ((num % 2) == 0)
                {
                    ch = (char) (0x30 + ((ushort) (num % 10)));
                }
                else
                {
                    ch = (char) (0x61 + ((ushort) (num % 0x1a)));
                }
                str = str + ch.ToString();
            }
            this.Session["LoginName"] = str;
            return str;
        }

        protected string Getpassstring()
        {
            string str = "";
            Random random = new Random();
            random.Next();
            for (int i = 0; i < 9; i++)
            {
                char ch;
                int num = random.Next();
                if ((num % 2) == 0)
                {
                    ch = (char) (0x30 + ((ushort) (num % 10)));
                }
                else
                {
                    ch = (char) (0x61 + ((ushort) (num % 0x1a)));
                }
                str = str + ch.ToString();
            }
            this.Session["password"] = str;
            return str;
        }

        protected void IbtnEnter_Click(object sender, EventArgs e)
        {
            string str = (string) this.Session["LoginName"];
            string str2 = (string) this.Session["password"];
            if (string.IsNullOrEmpty(str) || string.IsNullOrEmpty(str2))
            {
                AdminPage.WriteErrMsg("<li>操作超时</li>", "Login.aspx");
            }
            string str4 = base.Request.Form[str];
            string str5 = base.Request.Form[str2];
            AdministratorInfo info = Administrators.AuthenticateAdmin(str4.Trim(), str5.Trim());
            if (info.IsLock)
            {
                AdminPage.WriteErrMsg("<li>此管理员已经被锁定，请联系网站管理员！</li>", "Login.aspx");
            }
            LogInfo info2 = new LogInfo();
            info2.UserName = info.AdminName;
            info2.UserIP = PEContext.Current.UserHostAddress;
            info2.ScriptName = base.Request.RawUrl;
            info2.Timestamp = DateTime.Now;
            info2.Source = "";
            ILog log = LogFactory.CreateLog();
            if (!info.IsNull && (string.Compare(info.AdminName, str4.Trim(), StringComparison.OrdinalIgnoreCase) == 0))
            {
                AdminPrincipal principal = new AdminPrincipal();
                principal.UserName = info.UserName;
                principal.AdminName = info.AdminName;
                principal.RndPassword = info.RndPassword;
                string userData = principal.SerializeToString();
                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, info.AdminName, DateTime.Now, DateTime.Now.AddMinutes((double) SiteConfig.SiteOption.TicketTime), false, userData);
                string str8 = FormsAuthentication.Encrypt(ticket);
                HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName + "AdminCookie", str8);
                cookie.HttpOnly = true;
                cookie.Path = FormsAuthentication.FormsCookiePath;
                cookie.Secure = FormsAuthentication.RequireSSL;
                base.Response.Cookies.Add(cookie);
                if (base.Request.Cookies[FormsAuthentication.FormsCookieName] == null)
                {
                    UserInfo usersByUserName = Users.GetUsersByUserName(principal.UserName);
                    if (!usersByUserName.IsNull && (usersByUserName.Status == UserStatus.None))
                    {
                        string str9 = DataSecurity.MakeRandomString(10);
                        usersByUserName.LogOnTimes++;
                        usersByUserName.LastLogOnTime = new DateTime?(DateTime.Now);
                        usersByUserName.LastLogOnIP = PEContext.Current.UserHostAddress;
                        usersByUserName.LastPassword = str9;
                        Users.Update(usersByUserName);
                        UserPrincipal principal2 = new UserPrincipal();
                        principal2.UserName = principal.UserName;
                        principal2.LastPassword = str9;
                        FormsAuthenticationTicket ticket2 = new FormsAuthenticationTicket(1, principal.UserName, DateTime.Now, DateTime.Now.AddDays(1.0), false, principal2.SerializeToString());
                        string str10 = FormsAuthentication.Encrypt(ticket2);
                        HttpCookie cookie2 = new HttpCookie(FormsAuthentication.FormsCookieName, str10);
                        cookie2.HttpOnly = true;
                        cookie2.Path = FormsAuthentication.FormsCookiePath;
                        cookie2.Secure = FormsAuthentication.RequireSSL;
                        this.Session["UserName"] = principal2.UserName;
                        base.Response.Cookies.Add(cookie2);
                    }
                }
                info2.PostString = "";
                info2.Category = LogCategory.LogOnOk;
                info2.Message = "登录成功";
                info2.Title = info.AdminName + " 登录成功";
                info2.Priority = LogPriority.Normal;
                log.Add(info2);
                BasePage.ResponseRedirect("Index.aspx", true);
            }
            else
            {
                info2.PostString = "\r\nFORM: " + HttpContext.Current.Request.Form.ToString() + "\r\nQUERYSTRING: " + HttpContext.Current.Request.QueryString.ToString();
                info2.Category = LogCategory.LogOnFailure;
                info2.Message = "登录失败";
                info2.Title = str4.Trim() + " 登录失败";
                info2.Priority = LogPriority.Highest;
                log.Add(info2);
                AdminPage.WriteErrMsg("<li>用户登录名称或用户密码不对！</li>");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                this.ChangeID();
            }
        }
    }
}

