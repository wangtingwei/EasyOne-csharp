namespace EasyOne.WebSite.User
{
    using EasyOne.Api;
    using EasyOne.Common;
    using EasyOne.Components;
    using EasyOne.Controls;
    using EasyOne.Enumerations;
    using EasyOne.Model.UserManage;
    using EasyOne.UserManage;
    using EasyOne.Web.UI;
    using System;
    using System.Diagnostics;
    using System.Web;
    using System.Web.Security;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using System.Reflection;

    public partial class UserLogin : DynamicPage
    {
        private static FileVersionInfo FvInfo = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location);

        internal static string GetRedirectUrl()
        {
            HttpContext current = HttpContext.Current;
            string str = current.Request.QueryString["ReturnUrl"];
            if (str == null)
            {
                str = current.Request.Form["ReturnUrl"];
                if ((!string.IsNullOrEmpty(str) && !str.Contains("/")) && str.Contains("%"))
                {
                    str = HttpUtility.UrlDecode(str);
                }
            }
            if ((!string.IsNullOrEmpty(str) && !FormsAuthentication.EnableCrossAppRedirects) && !IsPathOnSameServer(str, current.Request.Url))
            {
                str = null;
            }
            if (str == null)
            {
                return FormsAuthentication.DefaultUrl;
            }
            return DataSecurity.UrlEncode(str).Replace("%26", "&");
        }

        protected void IbtnEnter_Click(object sender, ImageClickEventArgs e)
        {
            string str2;
            UserPrincipal principal;
            UserInfo userInfo = new UserInfo();
            userInfo.UserName = this.TxtUserName.Text.Trim();
            userInfo.UserPassword = this.TxtPassword.Text.Trim();
            if (SiteConfig.UserConfig.EnableCheckCodeOfLogOn && (string.Compare(this.TxtValidateCode.Text.Trim(), this.VcodeLogOn.ValidateCodeValue, StringComparison.OrdinalIgnoreCase) != 0))
            {
                DynamicPage.WriteErrMsg("<li>您输入的验证码和系统产生的不一致，请重新输入。</li>");
            }
            UserStatus status = Users.ValidateUser(userInfo);
            if ((int)status >= 100)
            {
                DynamicPage.WriteErrMsg("<li>用户登录名称或用户密码不对！</li>");
            }
            if (status != UserStatus.None)
            {
                switch (status)
                {
                    case UserStatus.Locked:
                        DynamicPage.WriteErrMsg("<li>用户帐户被锁定！</li>");
                        return;

                    case UserStatus.WaitValidateByEmail:
                        DynamicPage.WriteErrMsg("<li>用户帐户等待邮件验证！</li>");
                        return;

                    case (UserStatus.WaitValidateByEmail | UserStatus.Locked):
                        goto Label_0344;

                    case UserStatus.WaitValidateByAdmin:
                        DynamicPage.WriteErrMsg("<li>用户帐户等待管理员验证！</li>");
                        return;

                    case UserStatus.WaitValidateByMobile:
                        DynamicPage.WriteErrMsg("<li>用户帐户等待手机验证！</li>");
                        return;
                }
                goto Label_0344;
            }
            bool isPersistent = false;
            DateTime now = DateTime.Now;
            DateTime expiration = DateTime.Now;
            string selectedValue = this.DropExpiration.SelectedValue;
            if (selectedValue != null)
            {
                if (!(selectedValue == "None"))
                {
                    if (selectedValue == "Day")
                    {
                        isPersistent = true;
                        expiration = now.AddDays(1.0);
                        goto Label_013F;
                    }
                    if (selectedValue == "Month")
                    {
                        isPersistent = true;
                        expiration = now.AddMonths(1);
                        goto Label_013F;
                    }
                    if (selectedValue == "Year")
                    {
                        isPersistent = true;
                        expiration = now.AddYears(1);
                        goto Label_013F;
                    }
                }
                else
                {
                    isPersistent = false;
                    expiration = now.AddDays(1.0);
                    goto Label_013F;
                }
            }
            isPersistent = false;
            expiration = now.AddMinutes(20.0);
        Label_013F:
            if (!ApiData.IsAPiEnable())
            {
                goto Label_0230;
            }
            string savecookie = "";
            string str5 = this.DropExpiration.SelectedValue;
            if (str5 != null)
            {
                if (!(str5 == "None"))
                {
                    if (str5 == "Day")
                    {
                        savecookie = "1";
                        goto Label_01C5;
                    }
                    if (str5 == "Month")
                    {
                        savecookie = "30";
                        goto Label_01C5;
                    }
                    if (str5 == "Year")
                    {
                        savecookie = "365";
                        goto Label_01C5;
                    }
                }
                else
                {
                    savecookie = "-1";
                    goto Label_01C5;
                }
            }
            savecookie = "-1";
        Label_01C5:
            str2 = ApiFunction.LogOn(this.TxtUserName.Text, this.TxtPassword.Text, savecookie);
            if (str2 != "true")
            {
                DynamicPage.WriteErrMsg(str2 + "<br><li>用户登录名称或用户密码不对或用户帐号处于非正常状态！</li>");
                return;
            }
            str2 = ApiFunction.RegLogOn(this.TxtUserName.Text, this.TxtPassword.Text, savecookie);
            base.Response.Write(str2);
        Label_0230:
            principal = new UserPrincipal();
            principal.UserName = userInfo.UserName;
            principal.LastPassword = userInfo.LastPassword;
            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, userInfo.UserName, now, expiration, isPersistent, principal.SerializeToString());
            string str3 = FormsAuthentication.Encrypt(ticket);
            HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, str3);
            if (isPersistent)
            {
                cookie.Expires = expiration;
            }
            cookie.HttpOnly = true;
            cookie.Path = FormsAuthentication.FormsCookiePath;
            cookie.Secure = FormsAuthentication.RequireSSL;
            base.Response.Cookies.Add(cookie);
            this.Session["UserName"] = userInfo.UserName;
            base.Response.Write("<script language=\"JavaScript\">window.location='" + GetRedirectUrl() + "';</script>");
            return;
        Label_0344:
            DynamicPage.WriteErrMsg("<li>用户登录名称或用户密码不对！</li>");
        }

        internal static bool IsPathOnSameServer(string absUriOrLocalPath, Uri currentRequestUri)
        {
            Uri uri;
            if (Uri.TryCreate(absUriOrLocalPath, UriKind.Absolute, out uri) && !uri.IsLoopback)
            {
                return string.Equals(currentRequestUri.Host, uri.Host, StringComparison.OrdinalIgnoreCase);
            }
            return true;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (BasePage.RequestStringToLower("responseversion") == "sitefactory")
            {
                base.Response.Write("系统版本为：SiteFactory " + SiteConfig.SiteInfo.ProductEdition + " " + FvInfo.ProductVersion);
                base.Response.End();
            }
            if (!SiteConfig.UserConfig.EnableCheckCodeOfLogOn)
            {
                this.PhValCode.Visible = false;
            }
        }
    }
}

