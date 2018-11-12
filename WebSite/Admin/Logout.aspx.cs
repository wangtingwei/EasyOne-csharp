namespace EasyOne.WebSite.Admin
{
    using EasyOne.Components;
    using EasyOne.Logging;
    using EasyOne.Model.UserManage;
    using EasyOne.UserManage;
    using EasyOne.Web.UI;
    using System;
    using System.Web;
    using System.Web.Security;
    using System.Web.UI.HtmlControls;

    public partial class LogOff : AdminPage
    {
        

        protected void Page_Load(object sender, EventArgs e)
        {
            if (PEContext.Current.Admin.Identity.IsAuthenticated)
            {
                LogInfo info = new LogInfo();
                info.UserName = PEContext.Current.Admin.AdminName;
                info.UserIP = PEContext.Current.UserHostAddress;
                info.ScriptName = base.Request.RawUrl;
                info.Timestamp = DateTime.Now;
                info.PostString = "\r\nFORM: " + HttpContext.Current.Request.Form.ToString() + "\r\nQUERYSTRING: " + HttpContext.Current.Request.QueryString.ToString();
                info.Source = "";
                string name = FormsAuthentication.FormsCookieName + "AdminCookie";
                string str2 = string.Empty;
                if (HttpContext.Current.Request.Browser["supportsEmptyStringInCookieValue"] == "false")
                {
                    str2 = "NoCookie";
                }
                HttpCookie cookie = new HttpCookie(name, str2);
                cookie.HttpOnly = true;
                cookie.Path = FormsAuthentication.FormsCookiePath;
                cookie.Expires = new DateTime(0x7cf, 10, 12);
                cookie.Secure = FormsAuthentication.RequireSSL;
                if (FormsAuthentication.CookieDomain != null)
                {
                    cookie.Domain = FormsAuthentication.CookieDomain;
                }
                HttpContext.Current.Response.Cookies.Remove(name);
                HttpContext.Current.Response.Cookies.Add(cookie);
                FormsAuthentication.SignOut();
                AdministratorInfo administratorByAdminName = Administrators.GetAdministratorByAdminName(PEContext.Current.Admin.AdministratorInfo.AdminName);
                administratorByAdminName.LastLogOffTime = new DateTime?(DateTime.Now);
                Administrators.Update(administratorByAdminName);
                info.Category = LogCategory.LogOff;
                info.Message = "退出成功";
                info.Title = info.UserName + " 退出成功";
                info.Priority = LogPriority.Normal;
                LogFactory.CreateLog().Add(info);
            }
            BasePage.ResponseRedirect("index.aspx");
        }
    }
}

