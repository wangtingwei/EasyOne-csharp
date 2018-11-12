namespace EasyOne.WebSite
{
    using EasyOne.Accessories;
    using EasyOne.Api;
    using EasyOne.Components;
    using EasyOne.Contents;
    using EasyOne.Controls;
    using EasyOne.Enumerations;
    using EasyOne.ExtendedControls;
    using EasyOne.Model.UserManage;
    using EasyOne.UserManage;
    using EasyOne.Web.UI;
    using System;
    using System.Web;
    using System.Web.Security;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public partial class LoginIndex : BasePage
    {
        

        protected void BtnLogOn_Click(object sender, EventArgs e)
        {
            bool flag;
            DateTime now;
            DateTime time2;
            string str2;
            UserPrincipal principal;
            UserInfo userInfo = new UserInfo();
            userInfo.UserName = this.TxtUserName.Text.Trim();
            userInfo.UserPassword = this.TxtPassword.Text.Trim();
            if (SiteConfig.UserConfig.EnableCheckCodeOfLogOn && (string.Compare(this.TxtValidateCode.Text.Trim(), this.VcodeLogOn.ValidateCodeValue, StringComparison.OrdinalIgnoreCase) != 0))
            {
                this.PnlLogOnMessage.Visible = true;
                this.PnlLogOn.Visible = false;
                this.PnlLogOnStatus.Visible = false;
                this.LitMessage.Text = "<li>您输入的验证码和系统产生的不一致，请重新输入。</li>";
            }
            UserStatus status = Users.ValidateUser(userInfo);
            if ((int)status >= 100)
            {
                this.PnlLogOnMessage.Visible = true;
                this.PnlLogOn.Visible = false;
                this.PnlLogOnStatus.Visible = false;
                this.LitErrorMessage.Text = "<li>用户登录名称或用户密码不对！</li><br />";
                return;
            }
            switch (status)
            {
                case UserStatus.None:
                    flag = false;
                    now = DateTime.Now;
                    time2 = DateTime.Now;
                    switch (this.DropExpiration.SelectedValue)
                    {
                        case "None":
                            flag = false;
                            time2 = now.AddDays(1.0);
                            break;

                        case "Day":
                            flag = true;
                            time2 = now.AddDays(1.0);
                            break;

                        case "Month":
                            flag = true;
                            time2 = now.AddMonths(1);
                            break;

                        case "Year":
                            flag = true;
                            time2 = now.AddYears(1);
                            break;
                    }
                    flag = false;
                    time2 = now.AddMinutes(20.0);
                    break;

                case UserStatus.Locked:
                    this.PnlLogOnMessage.Visible = true;
                    this.PnlLogOn.Visible = false;
                    this.PnlLogOnStatus.Visible = false;
                    this.LitErrorMessage.Text = "<li>用户帐号被锁定！</li><br />";
                    return;

                case UserStatus.WaitValidateByEmail:
                    this.PnlLogOnMessage.Visible = true;
                    this.PnlLogOn.Visible = false;
                    this.PnlLogOnStatus.Visible = false;
                    this.LitErrorMessage.Text = "<li>用户帐号等待邮件验证！</li><br />";
                    return;

                case UserStatus.WaitValidateByAdmin:
                    this.PnlLogOnMessage.Visible = true;
                    this.PnlLogOn.Visible = false;
                    this.PnlLogOnStatus.Visible = false;
                    this.LitErrorMessage.Text = "<li>用户帐号等待管理员验证！</li><br />";
                    return;

                case UserStatus.WaitValidateByMobile:
                    this.PnlLogOnMessage.Visible = true;
                    this.PnlLogOn.Visible = false;
                    this.PnlLogOnStatus.Visible = false;
                    this.LitErrorMessage.Text = "<li>用户帐号等待手机验证！</li><br />";
                    return;

                default:
                    this.PnlLogOnMessage.Visible = true;
                    this.PnlLogOn.Visible = false;
                    this.PnlLogOnStatus.Visible = false;
                    this.LitErrorMessage.Text = "<li>用户登录名称或用户密码不对或用户帐号处于非正常状态！</li><br />";
                    return;
            }
            if (!ApiData.IsAPiEnable())
            {
                goto Label_02B0;
            }
            string savecookie = "";
            string selectedValue = this.DropExpiration.SelectedValue;
            if (selectedValue != null)
            {
                if (!(selectedValue == "None"))
                {
                    if (selectedValue == "Day")
                    {
                        savecookie = "1";
                        goto Label_0247;
                    }
                    if (selectedValue == "Month")
                    {
                        savecookie = "30";
                        goto Label_0247;
                    }
                    if (selectedValue == "Year")
                    {
                        savecookie = "365";
                        goto Label_0247;
                    }
                }
                else
                {
                    savecookie = "-1";
                    goto Label_0247;
                }
            }
            savecookie = "-1";
        Label_0247:
            str2 = ApiFunction.LogOn(this.TxtUserName.Text, this.TxtPassword.Text, savecookie);
            if (str2 != "true")
            {
                this.PnlLogOnMessage.Visible = true;
                this.PnlLogOn.Visible = false;
                this.PnlLogOnStatus.Visible = false;
                this.LitErrorMessage.Text = "<li>登陆失败</li><br>" + str2;
                return;
            }
        Label_02B0:
            principal = new UserPrincipal();
            principal.UserName = userInfo.UserName;
            principal.LastPassword = userInfo.LastPassword;
            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, userInfo.UserName, now, time2, flag, principal.SerializeToString());
            string str3 = FormsAuthentication.Encrypt(ticket);
            HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, str3);
            if (flag)
            {
                cookie.Expires = time2;
            }
            cookie.HttpOnly = true;
            cookie.Path = FormsAuthentication.FormsCookiePath;
            cookie.Secure = FormsAuthentication.RequireSSL;
            base.Response.Cookies.Add(cookie);
            this.Session["UserName"] = userInfo.UserName;
            base.Response.Redirect(base.Request.RawUrl);
        }

        protected void BtnReturn_Click(object sender, EventArgs e)
        {
            base.Response.Redirect(base.Request.RawUrl);
        }

        private void InitLogOnStatus()
        {
            UserInfo usersByUserName = Users.GetUsersByUserName(PEContext.Current.User.UserName);
            this.LitUserName.Text = usersByUserName.UserName;
            this.LitMoney.Text = "资金余额：" + usersByUserName.Balance.ToString("0.00") + "元";
            this.LitUserExp.Text = "经验积分：" + usersByUserName.UserExp.ToString() + "分 ";
            this.LitPoint.Text = "可用" + SiteConfig.UserConfig.PointName + "：" + usersByUserName.UserPoint.ToString() + SiteConfig.UserConfig.PointUnit;
            this.LitLogOnTime.Text = "登录次数：" + usersByUserName.LogOnTimes.ToString() + "次";
            this.LitSignIn.Text = "待签文章：" + SignInLog.GetNotSignInContentCountByUserName(usersByUserName.UserName).ToString() + "篇";
            this.LitMessage.Text = "待阅短信：" + Message.UnreadMessageCount(usersByUserName.UserName).ToString() + "条";
            if (!SiteConfig.SiteOption.EnablePointMoneyExp)
            {
                this.LitMoney.Visible = false;
                this.LitUserExp.Visible = false;
                this.LitPoint.Visible = false;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (PEContext.Current.User.Identity.IsAuthenticated)
            {
                this.PnlLogOnStatus.Visible = true;
                this.PnlLogOn.Visible = false;
                this.InitLogOnStatus();
            }
            else
            {
                this.PnlLogOn.Visible = true;
                this.PnlLogOnStatus.Visible = false;
                if (!SiteConfig.UserConfig.EnableCheckCodeOfLogOn)
                {
                    this.PhValCode.Visible = false;
                }
            }
        }
    }
}

