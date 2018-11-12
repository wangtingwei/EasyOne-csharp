namespace EasyOne.WebSite.Shop
{
    using EasyOne.Accessories;
    using EasyOne.Api;
    using EasyOne.Common;
    using EasyOne.Components;
    using EasyOne.Controls;
    using EasyOne.Enumerations;
    using EasyOne.Model.Accessories;
    using EasyOne.Model.Crm;
    using EasyOne.Model.UserManage;
    using EasyOne.UserManage;
    using EasyOne.Web.UI;
    using System;
    using System.Collections.Generic;
    using System.Net.Mail;
    using System.Web;
    using System.Web.Security;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public class FastRegister : DynamicPage
    {
        protected Button BtnLogOn;
        protected Button BtnRegister;
        protected Button btnUnRegisterBuy;
        protected HtmlForm form1;
        protected Label LblRegTitle;
        protected PlaceHolder PhValCode;
        protected EasyOne.Controls.RequiredFieldValidator RegLogOnPassword;
        protected EasyOne.Controls.RequiredFieldValidator RegLogOnUserName;
        protected RegularExpressionValidator RegularExpressionValidatorPassword;
        protected EasyOne.Controls.RequiredFieldValidator ReqTxtEmail;
        protected EasyOne.Controls.RequiredFieldValidator ReqTxtPassword;
        protected EasyOne.Controls.RequiredFieldValidator ReqTxtPwdConfirm;
        protected EasyOne.Controls.RequiredFieldValidator ReqTxtUserName;
        protected HtmlTable TableRegisterMust;
        protected TextBox TxtEmail;
        protected TextBox TxtPassword;
        protected TextBox TxtPwdConfirm;
        protected TextBox TxtRegPassword;
        protected TextBox TxtRegUserName;
        protected TextBox TxtUserName;
        protected TextBox TxtValidateCode;
        protected UserConfig userSiteConfig = SiteConfig.UserConfig;
        protected CompareValidator ValCompPassword;
        protected EmailValidator ValeTxtEmail;
        protected RegularExpressionValidator ValgTextMaxLength;
        protected ValidateCode VcodeLogOn;

        protected void BtnLogOn_Click(object sender, EventArgs e)
        {
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
            switch (status)
            {
                case UserStatus.Locked:
                    DynamicPage.WriteErrMsg("<li>用户帐户被锁定！</li>");
                    return;

                case UserStatus.WaitValidateByEmail:
                    DynamicPage.WriteErrMsg("<li>用户帐户等待邮件验证！</li>");
                    return;

                case UserStatus.WaitValidateByAdmin:
                    DynamicPage.WriteErrMsg("<li>用户帐户等待管理员验证！</li>");
                    return;

                case UserStatus.WaitValidateByMobile:
                    DynamicPage.WriteErrMsg("<li>用户帐户等待手机验证！</li>");
                    return;

                case UserStatus.None:
                {
                    bool isPersistent = false;
                    DateTime now = DateTime.Now;
                    DateTime expiration = DateTime.Now;
                    isPersistent = false;
                    expiration = now.AddDays(1.0);
                    UserPrincipal principal = new UserPrincipal();
                    principal.UserName = userInfo.UserName;
                    principal.LastPassword = userInfo.LastPassword;
                    FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, userInfo.UserName, now, expiration, isPersistent, principal.SerializeToString());
                    string str = FormsAuthentication.Encrypt(ticket);
                    HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, str);
                    if (isPersistent)
                    {
                        cookie.Expires = expiration;
                    }
                    cookie.HttpOnly = true;
                    cookie.Path = FormsAuthentication.FormsCookiePath;
                    cookie.Secure = FormsAuthentication.RequireSSL;
                    base.Response.Cookies.Add(cookie);
                    this.Session["UserName"] = userInfo.UserName;
                    base.Response.Redirect("Payment.aspx");
                    return;
                }
            }
            DynamicPage.WriteErrMsg("<li>用户登录名称或用户密码不对！</li>");
        }

        protected void BtnRegister_Click(object sender, EventArgs e)
        {
            IEncourageStrategy<int> strategy2;
            if (!this.userSiteConfig.EnableUserReg)
            {
                return;
            }
            if (!this.Page.IsValid)
            {
                return;
            }
            this.CheckUserName();
            this.CheckEmail();
            UserInfo usersInfo = new UserInfo();
            usersInfo.Email = this.TxtEmail.Text;
            usersInfo.FaceWidth = 0;
            usersInfo.FaceHeight = 0;
            usersInfo.PrivacySetting = 0;
            ContacterInfo contacterInfo = new ContacterInfo();
            contacterInfo.Sex = (UserSexType) Enum.Parse(typeof(UserSexType), "0");
            contacterInfo.Marriage = (UserMarriageType) Enum.Parse(typeof(UserMarriageType), "0");
            contacterInfo.Income = -1;
            contacterInfo.Education = -1;
            contacterInfo.ClientId = 0;
            contacterInfo.ParentId = 0;
            contacterInfo.CreateTime = DateTime.Now;
            contacterInfo.Owner = "";
            contacterInfo.UserType = ContacterType.EnterpriceMainContacter;
            contacterInfo.UpdateTime = DateTime.Now;
            contacterInfo.UserName = Users.UserNamefilter(this.TxtRegUserName.Text);
            contacterInfo.Birthday = null;
            usersInfo.UserName = Users.UserNamefilter(this.TxtRegUserName.Text);
            usersInfo.UserPassword = StringHelper.MD5(this.TxtRegPassword.Text);
            usersInfo.GroupId = this.userSiteConfig.GroupId;
            usersInfo.JoinTime = DateTime.Now;
            usersInfo.RegTime = DateTime.Now;
            usersInfo.UserExp = (int) this.userSiteConfig.PresentExp;
            usersInfo.UserPoint = 0;
            usersInfo.IsInheritGroupRole = true;
            usersInfo.Status = UserStatus.None;
            string str = DataSecurity.MakeRandomString(10);
            usersInfo.LastPassword = str;
            if (this.userSiteConfig.EmailCheckReg)
            {
                usersInfo.Status = UserStatus.WaitValidateByEmail;
                usersInfo.CheckNum = DataSecurity.MakeRandomString("abcdefghijklmnopqrstuvwxyz0123456789_", 10);
            }
            if (this.userSiteConfig.AdminCheckReg)
            {
                usersInfo.Status = UserStatus.WaitValidateByAdmin;
            }
            if (this.userSiteConfig.EmailCheckReg && this.userSiteConfig.AdminCheckReg)
            {
                usersInfo.Status = UserStatus.WaitValidateByAdmin | UserStatus.WaitValidateByEmail;
            }
            usersInfo.EndTime = new DateTime?(DateTime.Now);
            usersInfo.Balance = 0M;
            string str2 = "";
            if (ApiData.IsAPiEnable())
            {
                str2 = ApiFunction.RegUser(usersInfo.UserName, this.TxtRegPassword.Text, usersInfo.Question, usersInfo.Answer, usersInfo.Email, contacterInfo.TrueName, contacterInfo.Sex.ToString(), contacterInfo.Birthday.ToString(), contacterInfo.QQ, contacterInfo.Msn, contacterInfo.Mobile, contacterInfo.OfficePhone, contacterInfo.Province, contacterInfo.City, contacterInfo.Address, contacterInfo.ZipCode, contacterInfo.Homepage);
                if (str2 != "true")
                {
                    DynamicPage.WriteErrMsg(str2 + "<br><li>注册失败！</li>");
                }
                str2 = ApiFunction.RegLogOn(usersInfo.UserName, this.TxtRegPassword.Text, "1");
            }
            if (!Users.Add(usersInfo, contacterInfo))
            {
                DynamicPage.WriteErrMsg("<li>注册失败！</li>");
                return;
            }
            if (this.userSiteConfig.PresentMoney != 0.0)
            {
                IEncourageStrategy<decimal> strategy = new UserMoney();
                strategy.IncreaseForUsers(usersInfo.UserId.ToString(), (decimal) this.userSiteConfig.PresentMoney, "注册时赠送的金钱", true, "注册时赠送的金钱");
            }
            if (this.userSiteConfig.PresentValidNum == 0)
            {
                goto Label_03EF;
            }
            int howMany = 0;
            if (this.userSiteConfig.PresentValidNum == -1)
            {
                howMany = 0x270f;
            }
            else
            {
                switch (this.userSiteConfig.PresentValidUnit)
                {
                    case 1:
                        howMany = this.userSiteConfig.PresentValidNum;
                        goto Label_03C4;

                    case 2:
                        howMany = this.userSiteConfig.PresentValidNum * 30;
                        goto Label_03C4;

                    case 3:
                        howMany = this.userSiteConfig.PresentValidNum * 0x16d;
                        goto Label_03C4;
                }
                howMany = this.userSiteConfig.PresentValidNum;
            }
        Label_03C4:
            strategy2 = new UserDate();
            strategy2.IncreaseForUsers(usersInfo.UserId.ToString(), howMany, "注册时赠送有效期", true, "注册时赠送有效期");
        Label_03EF:
            if (this.userSiteConfig.PresentPoint != 0)
            {
                IEncourageStrategy<int> strategy3 = new UserPoint();
                strategy3.IncreaseForUsers(usersInfo.UserId.ToString(), this.userSiteConfig.PresentPoint, "注册时赠送点券", true, "注册时赠送点券");
            }
            if (this.userSiteConfig.EmailCheckReg)
            {
                MailInfo mailInfo = new MailInfo();
                mailInfo.IsBodyHtml = true;
                mailInfo.FromName = SiteConfig.SiteInfo.SiteName;
                List<MailAddress> list = new List<MailAddress>();
                list.Add(new MailAddress(usersInfo.Email));
                mailInfo.MailToAddressList = list;
                mailInfo.MailBody = this.userSiteConfig.EmailOfRegCheck.Replace("{$CheckNum}", usersInfo.CheckNum).Replace("{$CheckUrl}", base.Request.Url.GetLeftPart(UriPartial.Authority) + base.BasePath + "User/RegisterCheck.aspx?UserName=" + HttpUtility.UrlEncode(usersInfo.UserName) + "&CheckNum=" + usersInfo.CheckNum);
                mailInfo.Subject = SiteConfig.SiteInfo.SiteName + "网站会员注册验证码";
                if (SendMail.Send(mailInfo) == MailState.Ok)
                {
                    DynamicPage.WriteSuccessMsg("<li>注册验证码已成功发送到你的注册邮箱，请到邮箱查收并验证！</li>" + str2, "../Default.aspx");
                }
                else
                {
                    DynamicPage.WriteSuccessMsg("<li>注册成功，但发送验证邮件失败，请检查邮件地址是否正确，或与网站管理员联系！</li>" + str2, "../Default.aspx");
                }
            }
            string str3 = "";
            if (this.userSiteConfig.EnableRegCompany)
            {
                str3 = "<li><a href='/Company/RegCompany.aspx'>继续注册企业?</a></li>";
            }
            if (usersInfo.Status == UserStatus.None)
            {
                bool isPersistent = false;
                DateTime now = DateTime.Now;
                DateTime expiration = DateTime.Now;
                isPersistent = false;
                expiration = now.AddDays(1.0);
                UserPrincipal principal = new UserPrincipal();
                principal.UserName = usersInfo.UserName;
                principal.LastPassword = usersInfo.LastPassword;
                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, usersInfo.UserName, now, expiration, isPersistent, principal.SerializeToString());
                string str4 = FormsAuthentication.Encrypt(ticket);
                HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, str4);
                if (isPersistent)
                {
                    cookie.Expires = expiration;
                }
                cookie.HttpOnly = true;
                cookie.Path = FormsAuthentication.FormsCookiePath;
                cookie.Secure = FormsAuthentication.RequireSSL;
                base.Response.Cookies.Add(cookie);
                this.Session["UserName"] = usersInfo.UserName;
            }
            if (SiteConfig.ShopConfig.IsPayPassword)
            {
                BasePage.ResponseRedirect("../User/RegisterPayPassword.aspx?Url=FastRegister");
            }
            else
            {
                DynamicPage.WriteSuccessMsg("<li>注册成功！" + str3 + "</li>" + str2, "../Shop/Payment.aspx");
            }
        }

        protected void BtnunRegisterBuy_Click(object sender, EventArgs e)
        {
            if (SiteConfig.ShopConfig.EnableGuestBuy)
            {
                BasePage.ResponseRedirect("Payment.aspx");
            }
            else
            {
                DynamicPage.WriteErrMsg("<li>不允许游客购买商品，请返回上一页注册后再购买！</li>");
            }
        }

        private void CheckEmail()
        {
            if (!this.userSiteConfig.EnableMultiRegPerEmail && !Users.GetUsersByEmail(this.TxtEmail.Text).IsNull)
            {
                DynamicPage.WriteErrMsg("<li>Email已被他人注册，请输入不同的Email！</li>");
            }
        }

        private void CheckUserName()
        {
            if (StringHelper.FoundCharInArr(this.userSiteConfig.UserNameRegDisabled, this.TxtRegUserName.Text, "|"))
            {
                DynamicPage.WriteErrMsg("<li>该用户名禁止注册，请输入不同的用户名！</li>");
            }
            if (Users.Exists(Users.UserNamefilter(this.TxtRegUserName.Text)))
            {
                DynamicPage.WriteErrMsg("<li>该用户名已被他人占用，请输入不同的用户名！</li>");
            }
            if (Users.UserNamefilter(this.TxtUserName.Text).Length != this.TxtUserName.Text.Length)
            {
                DynamicPage.WriteErrMsg("<li>注册用户名中有非法字符！</li>");
            }
            int userNameLimit = 1;
            int userNameMax = 20;
            if (this.userSiteConfig.UserNameLimit > 0)
            {
                userNameLimit = this.userSiteConfig.UserNameLimit;
            }
            if (this.userSiteConfig.UserNameMax >= userNameLimit)
            {
                userNameMax = this.userSiteConfig.UserNameMax;
            }
            if ((this.TxtRegUserName.Text.Length < userNameLimit) || (userNameLimit > userNameMax))
            {
                DynamicPage.WriteErrMsg("用户名必须大于" + userNameLimit.ToString() + "个字符并且不能超过" + userNameMax.ToString() + "个字符");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!PEContext.Current.User.Identity.IsAuthenticated && !SiteConfig.UserConfig.EnableCheckCodeOfLogOn)
            {
                this.PhValCode.Visible = false;
            }
            int userNameLimit = 1;
            int userNameMax = 20;
            if (this.userSiteConfig.UserNameLimit > 0)
            {
                userNameLimit = this.userSiteConfig.UserNameLimit;
            }
            if (this.userSiteConfig.UserNameMax >= userNameLimit)
            {
                userNameMax = this.userSiteConfig.UserNameMax;
            }
            this.ValgTextMaxLength.ValidationExpression = @"^[a-zA-Z0-9_\u4e00-\u9fa5]{" + userNameLimit.ToString() + "," + userNameMax.ToString() + "}$";
            this.ValgTextMaxLength.ErrorMessage = "用户名必须大于" + userNameLimit.ToString() + "个字符并且不能超过" + userNameMax.ToString() + "个字符";
            if (!this.Page.IsPostBack)
            {
                this.LblRegTitle.Text = "用户名必须大于" + userNameLimit.ToString() + "个字符并且不能超过" + userNameMax.ToString() + "个字符";
            }
        }
    }
}

