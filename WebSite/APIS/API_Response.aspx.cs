namespace EasyOne.WebSite.API
{
    using EasyOne.Accessories;
    using EasyOne.Api;
    using EasyOne.Common;
    using EasyOne.Components;
    using EasyOne.Crm;
    using EasyOne.Enumerations;
    using EasyOne.Model.Accessories;
    using EasyOne.Model.Crm;
    using EasyOne.Model.UserManage;
    using EasyOne.UserManage;
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Net.Mail;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Web;
    using System.Web.Security;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;

    [CLSCompliant(false)]
    public partial class API_Response : Page
    {
        protected ApiData API;
        protected UserInfo userInfo;
        protected UserConfig userSiteConfig;

        public bool CheckSysKey(string iName, string iSysKey)
        {
            if (((iName == string.Empty) || (iName == null)) || ((iSysKey == string.Empty) || (iSysKey == null)))
            {
                return false;
            }
            if (iSysKey.Length == 0x20)
            {
                iSysKey = iSysKey.Substring(8, 0x10);
            }
            string str = StringHelper.MD5(iName + this.API.ApiKey).Substring(8, 0x10);
            string str2 = StringHelper.MD5GB2312(iName + this.API.ApiKey).Substring(8, 0x10);
            if (!(str.ToLower() == iSysKey.ToLower()) && !(str2.ToLower() == iSysKey.ToLower()))
            {
                return false;
            }
            return true;
        }

        public void checkUser()
        {
            this.CheckUserName();
            this.CheckUserEmail();
        }

        public bool CheckUserEmail()
        {
            this.API.SpeItems[7, 1] = this.API.GetNodeText(this.API.SpeItems[7, 0]);
            if (!Regex.IsMatch(this.API.SpeItems[7, 1], @"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"))
            {
                this.API.FoundErr = true;
                this.API.ErrMsg = "请输入正确的Email";
                return false;
            }
            if (!this.userSiteConfig.EnableMultiRegPerEmail && !Users.GetUsersByEmail(this.API.SpeItems[7, 1]).IsNull)
            {
                this.API.FoundErr = true;
                this.API.ErrMsg = "Email已被他人注册，请输入不同的Email！";
                return false;
            }
            return true;
        }

        public bool CheckUserName()
        {
            if (!this.userSiteConfig.EnableUserReg)
            {
                this.API.FoundErr = true;
                this.API.ErrMsg = "系统禁止注册！";
                return false;
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
            if (!Regex.IsMatch(this.API.SpeItems[5, 1], @"^[a-zA-Z0-9_\u4e00-\u9fa5]{" + userNameLimit.ToString() + "," + userNameMax.ToString() + "}$"))
            {
                this.API.FoundErr = true;
                this.API.ErrMsg = "用户名必须大于4个字符并且不能超过20个字符";
                return false;
            }
            if (StringHelper.FoundCharInArr(this.userSiteConfig.UserNameRegDisabled, this.API.SpeItems[5, 1], "|"))
            {
                this.API.FoundErr = true;
                this.API.ErrMsg = "该用户名禁止注册，请输入不同的用户名！";
                return false;
            }
            if (Users.Exists(Users.UserNamefilter(this.API.SpeItems[5, 1])))
            {
                this.API.FoundErr = true;
                this.API.ErrMsg = "该用户名已被他人占用，请输入不同的用户名！";
                return false;
            }
            return true;
        }

        public void createUser()
        {
            IEncourageStrategy<int> strategy2;
            this.API.SpeItems[7, 1] = this.API.GetNodeText(this.API.SpeItems[7, 0]);
            if (!this.CheckUserName() || !this.CheckUserEmail())
            {
                return;
            }
            this.API.PrepareData(true);
            UserInfo usersInfo = new UserInfo();
            usersInfo.Question = this.API.SpeItems[8, 1];
            usersInfo.Answer = StringHelper.MD5(this.API.SpeItems[9, 1]);
            usersInfo.Email = this.API.SpeItems[7, 1];
            string str = DataSecurity.MakeRandomString(10);
            usersInfo.LastPassword = str;
            ContacterInfo contacterInfo = new ContacterInfo();
            contacterInfo.TrueName = this.API.SpeItems[11, 1];
            contacterInfo.Country = "";
            contacterInfo.Province = "";
            contacterInfo.City = "";
            contacterInfo.Address = this.API.SpeItems[0x12, 1];
            contacterInfo.ZipCode = this.API.SpeItems[0x13, 1];
            contacterInfo.OfficePhone = this.API.SpeItems[0x11, 1];
            contacterInfo.HomePhone = "";
            contacterInfo.Mobile = this.API.SpeItems[0x10, 1];
            contacterInfo.Fax = "";
            contacterInfo.Homepage = this.API.SpeItems[20, 1];
            contacterInfo.Email = this.API.SpeItems[7, 1];
            contacterInfo.QQ = this.API.SpeItems[14, 1];
            contacterInfo.Msn = this.API.SpeItems[15, 1];
            contacterInfo.Icq = "";
            contacterInfo.Yahoo = "";
            contacterInfo.UC = "";
            contacterInfo.Aim = "";
            contacterInfo.IdCard = "";
            if (this.API.SpeItems[12, 1] == "1")
            {
                this.API.SpeItems[12, 1] = "Female";
            }
            else if (this.API.SpeItems[12, 1] == "0")
            {
                this.API.SpeItems[12, 1] = "Male";
            }
            else
            {
                this.API.SpeItems[12, 1] = "Secrecy";
            }
            contacterInfo.Sex = (UserSexType) Enum.Parse(typeof(UserSexType), this.API.SpeItems[12, 1]);
            contacterInfo.Marriage = UserMarriageType.Secrecy;
            contacterInfo.Income = -1;
            contacterInfo.Education = -1;
            contacterInfo.Company = "";
            contacterInfo.Department = "";
            contacterInfo.ClientId = 0;
            contacterInfo.ParentId = 0;
            contacterInfo.CreateTime = DateTime.Now;
            contacterInfo.Owner = "";
            contacterInfo.UserType = ContacterType.EnterpriceMainContacter;
            contacterInfo.UpdateTime = DateTime.Now;
            contacterInfo.UserName = Users.UserNamefilter(this.API.SpeItems[5, 1]);
            contacterInfo.Phs = "";
            contacterInfo.Birthday = string.IsNullOrEmpty(this.API.SpeItems[13, 1]) ? null : new DateTime?(Convert.ToDateTime(this.API.SpeItems[13, 1]));
            contacterInfo.Position = "";
            usersInfo.UserName = Users.UserNamefilter(this.API.SpeItems[5, 1]);
            usersInfo.UserPassword = StringHelper.MD5(this.API.SpeItems[6, 1]);
            usersInfo.GroupId = this.userSiteConfig.GroupId;
            usersInfo.JoinTime = DateTime.Now;
            usersInfo.RegTime = DateTime.Now;
            usersInfo.UserExp = (int) this.userSiteConfig.PresentExp;
            usersInfo.UserPoint = 0;
            usersInfo.IsInheritGroupRole = true;
            usersInfo.Status = UserStatus.None;
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
            if (!Users.Add(usersInfo, contacterInfo))
            {
                this.API.FoundErr = true;
                this.API.ErrMsg = "注册失败！";
                return;
            }
            if (this.userSiteConfig.PresentMoney != 0.0)
            {
                IEncourageStrategy<decimal> strategy = new UserMoney();
                strategy.IncreaseForUsers(usersInfo.UserId.ToString(), (decimal) this.userSiteConfig.PresentMoney, "注册时赠送的金钱", true, "注册时赠送的金钱");
            }
            if (this.userSiteConfig.PresentValidNum == 0)
            {
                goto Label_05C6;
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
                        goto Label_059B;

                    case 2:
                        howMany = this.userSiteConfig.PresentValidNum * 30;
                        goto Label_059B;

                    case 3:
                        howMany = this.userSiteConfig.PresentValidNum * 0x16d;
                        goto Label_059B;
                }
                howMany = this.userSiteConfig.PresentValidNum;
            }
        Label_059B:
            strategy2 = new UserDate();
            strategy2.IncreaseForUsers(usersInfo.UserId.ToString(), howMany, "注册时赠送有效期", true, "注册时赠送有效期");
        Label_05C6:
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
                mailInfo.MailBody = this.userSiteConfig.EmailOfRegCheck.Replace("{$CheckNum}", usersInfo.CheckNum).Replace("{$CheckUrl}", base.Request.Url.GetLeftPart(UriPartial.Authority) + "User/RegisterCheck.aspx?UserName=" + HttpUtility.UrlEncode(usersInfo.UserName) + "&CheckNum=" + usersInfo.CheckNum);
                mailInfo.Subject = SiteConfig.SiteInfo.SiteName + "网站会员注册验证码";
                SendMail.Send(mailInfo);
            }
            if (usersInfo.Status == UserStatus.None)
            {
                UserPrincipal principal = new UserPrincipal();
                principal.UserName = usersInfo.UserName;
                principal.LastPassword = usersInfo.LastPassword;
                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, usersInfo.UserName, DateTime.Now, DateTime.Now.AddMinutes(60.0), false, principal.SerializeToString());
                string str2 = FormsAuthentication.Encrypt(ticket);
                HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, str2);
                cookie.HttpOnly = true;
                cookie.Path = FormsAuthentication.FormsCookiePath;
                cookie.Secure = FormsAuthentication.RequireSSL;
                base.Response.Cookies.Add(cookie);
            }
        }

        public void DealResponse()
        {
            try
            {
                this.API.MyXmlDoc.Load(base.Request.InputStream);
            }
            catch
            {
                this.API.FoundErr = true;
                this.API.ErrMsg = "加载xml数据错误!";
                this.API.WriteErrXml();
                return;
            }
            this.API.SpeItems[2, 1] = this.API.GetNodeText(this.API.SpeItems[2, 0]);
            this.API.SpeItems[5, 1] = this.API.GetNodeText(this.API.SpeItems[5, 0]);
            this.API.SpeItems[1, 1] = this.API.GetNodeText(this.API.SpeItems[1, 0]);
            if (((this.API.SpeItems[2, 1] == "") || (this.API.SpeItems[5, 1] == "")) || (this.API.SpeItems[1, 1] == ""))
            {
                this.API.FoundErr = true;
                this.API.ErrMsg = "未包含必须元素，数据同步被拒绝!";
            }
            if (!this.CheckSysKey(this.API.SpeItems[5, 1], this.API.SpeItems[2, 1]))
            {
                this.API.FoundErr = true;
                this.API.ErrMsg = "安全码不符，数据同步被拒绝!";
            }
            if (this.API.FoundErr)
            {
                this.API.WriteErrXml();
            }
            switch (this.API.SpeItems[1, 1])
            {
                case "checkname":
                    this.checkUser();
                    break;

                case "reguser":
                    this.createUser();
                    break;

                case "login":
                    this.loginUser();
                    break;

                case "logout":
                    this.Loginout();
                    break;

                case "update":
                    this.UpdateUser();
                    break;

                case "delete":
                    this.DeleteUser();
                    break;

                case "getinfo":
                    this.GetUserInfo();
                    break;
            }
            if (!this.API.FoundErr)
            {
                this.API.SpeItems[3, 1] = "0";
                this.API.PrepareXml(false);
                this.API.WriteXml();
            }
            else
            {
                this.API.WriteErrXml();
            }
        }

        public void DeleteUser()
        {
            if ((this.API.SpeItems[5, 1] == string.Empty) || (this.API.SpeItems[5, 1] == null))
            {
                this.API.FoundErr = true;
                this.API.ErrMsg = "未指定删除用户名!";
            }
            else
            {
                foreach (string str in this.API.SpeItems[5, 1].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    if ((str != null) || (str.Length != 0))
                    {
                        UserInfo usersByUserName = Users.GetUsersByUserName(str);
                        if (!usersByUserName.IsNull)
                        {
                            try
                            {
                                if (Users.Delete(usersByUserName.UserId))
                                {
                                    Users.DeleteUserRelation(usersByUserName.UserId, usersByUserName.UserName);
                                }
                            }
                            catch (Exception exception)
                            {
                                this.API.FoundErr = true;
                                this.API.ErrMsg = exception.ToString();
                                break;
                            }
                        }
                    }
                }
            }
        }

        public void GetUserInfo()
        {
            this.API.SpeItems[5, 1] = Users.UserNamefilter(this.API.SpeItems[5, 1]);
            if (!Users.Exists(this.API.SpeItems[5, 1]))
            {
                this.API.FoundErr = true;
                this.API.ErrMsg = "查询的用户不存在";
            }
            else
            {
                UserInfo usersByUserName = Users.GetUsersByUserName(this.API.SpeItems[5, 1]);
                this.API.SpeItems[6, 1] = usersByUserName.UserPassword;
                this.API.SpeItems[7, 1] = usersByUserName.Email;
                this.API.SpeItems[8, 1] = usersByUserName.Question;
                this.API.SpeItems[6, 1] = usersByUserName.UserPassword;
                this.API.SpeItems[9, 1] = usersByUserName.Answer;
                this.API.SpeItems[0x16, 1] = usersByUserName.JoinTime.ToString();
                this.API.SpeItems[0x15, 1] = usersByUserName.LastLogOnIP;
                this.API.SpeItems[0x1a, 1] = usersByUserName.Balance.ToString();
                this.API.SpeItems[0x17, 1] = usersByUserName.UserExp.ToString();
                this.API.SpeItems[0x19, 1] = usersByUserName.UserPoint.ToString();
                this.API.SpeItems[0x18, 1] = usersByUserName.ConsumePoint.ToString();
                this.API.SpeItems[0x1b, 1] = usersByUserName.PostItems.ToString();
                this.API.SpeItems[0x1c, 1] = usersByUserName.Status.ToString();
                ContacterInfo contacterByUserName = Contacter.GetContacterByUserName(usersByUserName.UserName);
                if (!contacterByUserName.IsNull)
                {
                    this.API.SpeItems[11, 1] = contacterByUserName.TrueName;
                    this.API.SpeItems[0x1f, 1] = contacterByUserName.Sex.ToString();
                    this.API.SpeItems[20, 1] = contacterByUserName.Homepage;
                    this.API.SpeItems[14, 1] = contacterByUserName.QQ;
                    this.API.SpeItems[15, 1] = contacterByUserName.Msn;
                    this.API.SpeItems[0x11, 1] = contacterByUserName.OfficePhone;
                    this.API.SpeItems[0x10, 1] = contacterByUserName.Mobile;
                    this.API.SpeItems[0x1d, 1] = contacterByUserName.Province;
                    this.API.SpeItems[30, 1] = contacterByUserName.City;
                    this.API.SpeItems[0x12, 1] = contacterByUserName.Address;
                    this.API.SpeItems[0x13, 1] = contacterByUserName.ZipCode;
                }
            }
        }

        private void Loginon()
        {
            UserPrincipal principal;
            if (!this.CheckSysKey(this.API.SpeItems[5, 1], this.API.SpeItems[2, 1]))
            {
                return;
            }
            this.API.SpeItems[5, 1] = HttpUtility.UrlDecode(this.API.SpeItems[5, 1], Encoding.GetEncoding("gb2312"));
            UserInfo usersByUserName = Users.GetUsersByUserName(this.API.SpeItems[5, 1]);
            if (usersByUserName.IsNull)
            {
                return;
            }
            if ((this.API.SpeItems[6, 1].Length != 0x10) && (this.API.SpeItems[6, 1].Length != 0x20))
            {
                return;
            }
            if (this.API.SpeItems[6, 1].Length == usersByUserName.UserPassword.Length)
            {
                if (string.Compare(this.API.SpeItems[6, 1], usersByUserName.UserPassword, StringComparison.Ordinal) != 0)
                {
                    return;
                }
            }
            else if (this.API.SpeItems[6, 1].Length == 0x20)
            {
                if (!StringHelper.ValidateMD5(usersByUserName.UserPassword, this.API.SpeItems[6, 1]))
                {
                    return;
                }
            }
            else if ((usersByUserName.UserPassword.Length != 0x20) || !StringHelper.ValidateMD5(this.API.SpeItems[6, 1], usersByUserName.UserPassword))
            {
                return;
            }
            string str = DataSecurity.MakeRandomString(10);
            usersByUserName.LogOnTimes++;
            usersByUserName.LastLogOnTime = new DateTime?(DateTime.Now);
            usersByUserName.LastLogOnIP = PEContext.Current.UserHostAddress;
            usersByUserName.LastPassword = str;
            Users.Update(usersByUserName);
            bool isPersistent = false;
            DateTime now = DateTime.Now;
            DateTime expiration = DateTime.Now;
            string str3 = this.API.SpeItems[10, 1];
            if (str3 != null)
            {
                if (!(str3 == "0"))
                {
                    if (str3 == "1")
                    {
                        isPersistent = true;
                        expiration = now.AddDays(1.0);
                        goto Label_026B;
                    }
                    if (str3 == "2")
                    {
                        isPersistent = true;
                        expiration = now.AddMonths(1);
                        goto Label_026B;
                    }
                    if (str3 == "3")
                    {
                        isPersistent = true;
                        expiration = now.AddYears(1);
                        goto Label_026B;
                    }
                }
                else
                {
                    isPersistent = false;
                    expiration = now.AddMinutes(20.0);
                    goto Label_026B;
                }
            }
            isPersistent = false;
            expiration = now.AddMinutes(20.0);
        Label_026B:
            principal = new UserPrincipal();
            principal.UserName = this.API.SpeItems[5, 1];
            principal.LastPassword = str;
            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, this.API.SpeItems[5, 1], now, expiration, isPersistent, principal.SerializeToString());
            string str2 = FormsAuthentication.Encrypt(ticket);
            HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, str2);
            if (isPersistent)
            {
                cookie.Expires = expiration;
            }
            cookie.HttpOnly = true;
            cookie.Path = FormsAuthentication.FormsCookiePath;
            cookie.Secure = FormsAuthentication.RequireSSL;
            base.Response.Cookies.Add(cookie);
            this.Session["UserName"] = this.userInfo.UserName;
        }

        private void Loginout()
        {
            if (PEContext.Current.User.Identity.IsAuthenticated)
            {
                FormsAuthentication.SignOut();
            }
        }

        public void loginUser()
        {
            this.API.SpeItems[6, 1] = this.API.GetNodeText(this.API.SpeItems[6, 0]);
            this.userInfo.UserName = this.API.SpeItems[5, 1];
            this.userInfo.UserPassword = this.API.SpeItems[6, 1];
            if (Users.ValidateUser(this.userInfo) != UserStatus.None)
            {
                this.API.FoundErr = true;
                this.API.ErrMsg = "用户登录名称或用户密码不对或用户帐号处于非正常状态！";
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.API = new ApiData();
            if (this.API.ApiEnable != "true")
            {
                this.API.ErrMsg = "接口没有开通";
                this.API.WriteErrXml();
            }
            this.userSiteConfig = SiteConfig.UserConfig;
            this.userInfo = new UserInfo();
            NameValueCollection values = HttpUtility.ParseQueryString(base.Request.Url.Query, Encoding.GetEncoding("GB2312"));
            this.API.SpeItems[2, 1] = base.Request.QueryString[this.API.SpeItems[2, 0]];
            this.API.SpeItems[5, 1] = values[this.API.SpeItems[5, 0]];
            this.API.SpeItems[6, 1] = base.Request.QueryString[this.API.SpeItems[6, 0]];
            this.API.SpeItems[10, 1] = base.Request.QueryString[this.API.SpeItems[10, 0]];
            if ((this.API.SpeItems[2, 1] != "") && (this.API.SpeItems[5, 1] != null))
            {
                if ((this.API.SpeItems[5, 1] != "") && (this.API.SpeItems[5, 1] != null))
                {
                    if ((this.API.SpeItems[6, 1] != "") && (this.API.SpeItems[6, 1] != null))
                    {
                        this.Loginon();
                    }
                    else
                    {
                        this.Loginout();
                    }
                }
            }
            else
            {
                this.DealResponse();
            }
        }

        public void UpdateUser()
        {
            if (!Users.Exists(this.API.SpeItems[5, 1]))
            {
                this.API.FoundErr = true;
                this.API.ErrMsg = "查询的用户不存在";
            }
            else
            {
                this.API.PrepareData(true);
                if ((this.API.SpeItems[7, 1] != string.Empty) && !Regex.IsMatch(this.API.SpeItems[7, 1], @"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"))
                {
                    this.API.FoundErr = true;
                    this.API.ErrMsg = "请输入正确的Email";
                }
                else
                {
                    UserInfo usersByUserName = Users.GetUsersByUserName(this.API.SpeItems[5, 1]);
                    if (!string.IsNullOrEmpty(this.API.SpeItems[6, 1]))
                    {
                        usersByUserName.UserPassword = StringHelper.MD5(this.API.SpeItems[6, 1].ToLower());
                    }
                    if (!string.IsNullOrEmpty(this.API.SpeItems[9, 1]))
                    {
                        usersByUserName.Answer = StringHelper.MD5(this.API.SpeItems[9, 1]);
                    }
                    if (!string.IsNullOrEmpty(this.API.SpeItems[8, 1]))
                    {
                        usersByUserName.Question = this.API.SpeItems[8, 1];
                    }
                    if (!string.IsNullOrEmpty(this.API.SpeItems[0x1c, 1]))
                    {
                        usersByUserName.Status = (UserStatus) Enum.Parse(typeof(UserStatus), ApiData.ExchangStatus(this.API.SpeItems[0x1c, 1]).ToString());
                    }
                    if (!string.IsNullOrEmpty(this.API.SpeItems[7, 1]))
                    {
                        usersByUserName.Email = this.API.SpeItems[7, 1];
                    }
                    if (!string.IsNullOrEmpty(this.API.SpeItems[11, 1]))
                    {
                        usersByUserName.UserTrueName = this.API.SpeItems[11, 1];
                    }
                    if (!string.IsNullOrEmpty(this.API.SpeItems[12, 1]))
                    {
                        usersByUserName.Sex = (UserSexType) Enum.Parse(typeof(UserSexType), ApiData.ExchangeGender(this.API.SpeItems[12, 1]).ToString());
                    }
                    ContacterInfo contacterByUserName = Contacter.GetContacterByUserName(usersByUserName.UserName);
                    if (!string.IsNullOrEmpty(this.API.SpeItems[11, 1]))
                    {
                        contacterByUserName.TrueName = usersByUserName.UserTrueName;
                    }
                    if (!string.IsNullOrEmpty(this.API.SpeItems[12, 1]))
                    {
                        contacterByUserName.Sex = usersByUserName.Sex;
                    }
                    if (!string.IsNullOrEmpty(this.API.SpeItems[7, 1]))
                    {
                        contacterByUserName.Email = usersByUserName.Email;
                    }
                    if (!string.IsNullOrEmpty(this.API.SpeItems[0x13, 1]))
                    {
                        contacterByUserName.ZipCode = this.API.SpeItems[0x13, 1];
                    }
                    if (!string.IsNullOrEmpty(this.API.SpeItems[14, 1]))
                    {
                        contacterByUserName.QQ = this.API.SpeItems[14, 1];
                    }
                    if (!string.IsNullOrEmpty(this.API.SpeItems[0x10, 1]))
                    {
                        contacterByUserName.Mobile = this.API.SpeItems[0x10, 1];
                    }
                    if (!string.IsNullOrEmpty(this.API.SpeItems[15, 1]))
                    {
                        contacterByUserName.Msn = this.API.SpeItems[15, 1];
                    }
                    if (!string.IsNullOrEmpty(this.API.SpeItems[20, 1]))
                    {
                        contacterByUserName.Homepage = this.API.SpeItems[20, 1];
                    }
                    if (!string.IsNullOrEmpty(this.API.SpeItems[13, 1]))
                    {
                        contacterByUserName.Birthday = string.IsNullOrEmpty(this.API.SpeItems[13, 1]) ? null : new DateTime?(Convert.ToDateTime(this.API.SpeItems[13, 1]));
                    }
                    if (!string.IsNullOrEmpty(this.API.SpeItems[0x11, 1]))
                    {
                        contacterByUserName.OfficePhone = this.API.SpeItems[0x11, 1];
                    }
                    if (!string.IsNullOrEmpty(this.API.SpeItems[0x12, 1]))
                    {
                        contacterByUserName.Address = this.API.SpeItems[0x12, 1];
                    }
                    if (!Users.Update(usersByUserName, contacterByUserName))
                    {
                        this.API.FoundErr = true;
                        this.API.ErrMsg = "修改会员信息失败!!";
                    }
                }
            }
        }
    }
}

