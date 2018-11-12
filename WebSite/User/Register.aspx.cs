namespace EasyOne.WebSite
{
    using EasyOne.Accessories;
    using EasyOne.Api;
    using EasyOne.Common;
    using EasyOne.Components;
    using EasyOne.Controls;
    using EasyOne.Crm;
    using EasyOne.Enumerations;
    using EasyOne.ExtendedControls;
    using EasyOne.Model.Accessories;
    using EasyOne.Model.Crm;
    using EasyOne.Model.UserManage;
    using EasyOne.UserManage;
    using EasyOne.Web.UI;
    using EasyOne.WebSite.Controls;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net.Mail;
    using System.Web;
    using System.Web.Security;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public partial class Register : DynamicPage, ICallbackEventHandler
    {

        protected string callBackReference;
        private string result;
        protected UserConfig userSiteConfig = new UserConfig();

        protected void BtnRegStep1_Click(object sender, EventArgs e)
        {
            if (this.userSiteConfig.EnableUserReg)
            {
                this.PnlRegStep1.Visible = false;
                this.PnlRegStep2.Visible = true;
                this.ReqTxtAddress.Visible = this.GetEnableValid("Address");
                this.InteractiveMessagerTxtAddress.IsValidEmpty = this.GetEnableValid("Address");
                this.ReqTxtAim.Visible = this.GetEnableValid("Aim");
                this.InteractiveMessagerTxtAim.IsValidEmpty = this.GetEnableValid("Aim");
                this.ReqTxtBirthday.Visible = this.GetEnableValid("Birthday");
                this.InteractiveMessagerTxtBirthday.IsValidEmpty = this.GetEnableValid("Birthday");
                this.ReqTxtCompany.Visible = this.GetEnableValid("Company");
                this.InteractiveMessagerTxtCompany.IsValidEmpty = this.GetEnableValid("Company");
                this.ReqTxtDepartment.Visible = this.GetEnableValid("Department");
                this.InteractiveMessagerTxtDepartment.IsValidEmpty = this.GetEnableValid("Department");
                this.ReqTxtFaceHeight.Visible = this.GetEnableValid("FaceHeight");
                this.InteractiveMessagerTxtFaceHeight.IsValidEmpty = this.GetEnableValid("FaceHeight");
                this.ReqTxtFaceWidth.Visible = this.GetEnableValid("FaceWidth");
                this.InteractiveMessagerTxtFaceWidth.IsValidEmpty = this.GetEnableValid("FaceWidth");
                this.ReqTxtFax.Visible = this.GetEnableValid("Fax");
                this.InteractiveMessagerTxtFax.IsValidEmpty = this.GetEnableValid("Fax");
                this.ReqTxtHomepage.Visible = this.GetEnableValid("Homepage");
                this.InteractiveMessagerTxtHomepage.IsValidEmpty = this.GetEnableValid("Homepage");
                this.ReqTxtIDCard.Visible = this.GetEnableValid("IDCard");
                this.InteractiveMessagerTxtIDCard.IsValidEmpty = this.GetEnableValid("IDCard");
                this.ReqTxtHomePhone.Visible = this.GetEnableValid("HomePhone");
                this.InteractiveMessagerTxtHomePhone.IsValidEmpty = this.GetEnableValid("HomePhone");
                this.ReqTxtICQ.Visible = this.GetEnableValid("ICQ");
                this.InteractiveMessagerTxtICQ.IsValidEmpty = this.GetEnableValid("ICQ");
                this.ReqTxtMobile.Visible = this.GetEnableValid("Mobile");
                this.InteractiveMessagerTxtMobile.IsValidEmpty = this.GetEnableValid("Mobile");
                this.ReqTxtMSN.Visible = this.GetEnableValid("MSN");
                this.InteractiveMessagerTxtMSN.IsValidEmpty = this.GetEnableValid("MSN");
                this.ReqTxtPosTitle.Visible = this.GetEnableValid("PosTitle");
                this.InteractiveMessagerTxtPosTitle.IsValidEmpty = this.GetEnableValid("PosTitle");
                this.ReqTxtQQ.Visible = this.GetEnableValid("QQ");
                this.InteractiveMessagerTxtQQ.IsValidEmpty = this.GetEnableValid("QQ");
                this.ReqTxtSign.Visible = this.GetEnableValid("Sign");
                this.InteractiveMessagerTxtSign.IsValidEmpty = this.GetEnableValid("Sign");
                this.ReqTxtSpareEmail.Visible = this.GetEnableValid("SpareEmail");
                this.InteractiveMessagerTxtSpareEmail.IsValidEmpty = this.GetEnableValid("SpareEmail");
                this.ReqTxtTrueName.Visible = this.GetEnableValid("TrueName");
                this.InteractiveMessagerTxtTrueName.IsValidEmpty = this.GetEnableValid("TrueName");
                this.ReqTxtUC.Visible = this.GetEnableValid("UC");
                this.InteractiveMessagerTxtUC.IsValidEmpty = this.GetEnableValid("UC");
                this.ReqTxtUserFace.Visible = this.GetEnableValid("UserFace");
                this.InteractiveMessagerTxtUserFace.IsValidEmpty = this.GetEnableValid("UserFace");
                this.ReqTxtVocation.Visible = this.GetEnableValid("Vocation");
                this.InteractiveMessagerTxtVocation.IsValidEmpty = this.GetEnableValid("Vocation");
                this.ReqTxtYahoo.Visible = this.GetEnableValid("Yahoo");
                this.InteractiveMessagerTxtYahoo.IsValidEmpty = this.GetEnableValid("Yahoo");
                this.ReqTxtZipCode.Visible = this.GetEnableValid("ZipCode");
                this.InteractiveMessagerTxtZipCode.IsValidEmpty = this.GetEnableValid("ZipCode");
                this.ReqTxtOfficePhone.Visible = this.GetEnableValid("OfficePhone");
                this.InteractiveMessagerTxtOfficePhone.IsValidEmpty = this.GetEnableValid("OfficePhone");
                this.ReqTxtPHS.Visible = this.GetEnableValid("PHS");
                this.InteractiveMessagerTxtPHS.IsValidEmpty = this.GetEnableValid("PHS");
                this.ReqDropMarriage.Visible = this.GetEnableValid("Marriage");
                this.InteractiveMessagerDropMarriage.IsValidEmpty = this.GetEnableValid("Marriage");
                if (this.userSiteConfig.EnableQAofReg)
                {
                    if (!string.IsNullOrEmpty(this.userSiteConfig.RegQuestion1))
                    {
                        this.LitRegQuestion1.Text = this.userSiteConfig.RegQuestion1;
                        this.TrRegQuestion1.Visible = true;
                    }
                    if (!string.IsNullOrEmpty(this.userSiteConfig.RegQuestion2))
                    {
                        this.LitRegQuestion2.Text = this.userSiteConfig.RegQuestion2;
                        this.TrRegQuestion2.Visible = true;
                    }
                    if (!string.IsNullOrEmpty(this.userSiteConfig.RegQuestion3))
                    {
                        this.LitRegQuestion3.Text = this.userSiteConfig.RegQuestion3;
                        this.TrRegQuestion3.Visible = true;
                    }
                }
                if (this.userSiteConfig.EnableCheckCodeOfReg)
                {
                    this.TrVcodeRegister.Visible = true;
                }
            }
        }

        protected void BtnRegStep1NotApprove_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("~/Index.aspx");
        }

        protected void BtnRegStep2_Click(object sender, EventArgs e)
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
            this.CheckCode();
            this.CheckQAofReg();
            UserInfo usersInfo = new UserInfo();
            usersInfo.Question = this.TxtQuestion.Text;
            usersInfo.Answer = StringHelper.MD5(this.TxtAnswer.Text);
            usersInfo.Email = this.TxtEmail.Text;
            usersInfo.UserFace = this.TxtUserFace.Text;
            usersInfo.FaceWidth = DataConverter.CLng(this.TxtFaceWidth.Text);
            usersInfo.FaceHeight = DataConverter.CLng(this.TxtFaceHeight.Text);
            usersInfo.Sign = this.TxtSign.Text;
            usersInfo.PrivacySetting = DataConverter.CLng(this.DropPrivacy.SelectedValue);
            string str = DataSecurity.MakeRandomString(10);
            usersInfo.LastPassword = str;
            ContacterInfo contacterInfo = new ContacterInfo();
            contacterInfo.TrueName = this.TxtTrueName.Text;
            contacterInfo.Country = this.Region.Country;
            contacterInfo.Province = this.Region.Province;
            contacterInfo.City = this.Region.City;
            contacterInfo.Address = this.TxtAddress.Text;
            contacterInfo.ZipCode = this.TxtZipCode.Text;
            contacterInfo.OfficePhone = this.TxtOfficePhone.Text;
            contacterInfo.HomePhone = this.TxtHomePhone.Text;
            contacterInfo.Mobile = this.TxtMobile.Text;
            contacterInfo.Fax = this.TxtFax.Text;
            contacterInfo.Homepage = this.TxtHomepage.Text;
            contacterInfo.Email = this.TxtEmail.Text;
            contacterInfo.QQ = this.TxtQQ.Text;
            contacterInfo.Msn = this.TxtMSN.Text;
            contacterInfo.Icq = this.TxtICQ.Text;
            contacterInfo.Yahoo = this.TxtYahoo.Text;
            contacterInfo.UC = this.TxtUC.Text;
            contacterInfo.Aim = this.TxtAim.Text;
            contacterInfo.IdCard = this.TxtIDCard.Text;
            contacterInfo.Sex = (UserSexType) Enum.Parse(typeof(UserSexType), this.DropSex.SelectedValue);
            contacterInfo.Marriage = (UserMarriageType) Enum.Parse(typeof(UserMarriageType), DataConverter.CLng(this.DropMarriage.SelectedValue).ToString());
            if (this.GetDisplayStyle("Income") != "none")
            {
                contacterInfo.Income = DataConverter.CLng(this.DropIncome.SelectedValue);
            }
            else
            {
                contacterInfo.Income = -1;
            }
            contacterInfo.Education = -1;
            contacterInfo.Company = this.TxtCompany.Text;
            contacterInfo.Department = this.TxtDepartment.Text;
            contacterInfo.ClientId = 0;
            contacterInfo.ParentId = 0;
            contacterInfo.CreateTime = DateTime.Now;
            contacterInfo.Owner = "";
            contacterInfo.UserType = ContacterType.EnterpriceMainContacter;
            contacterInfo.UpdateTime = DateTime.Now;
            contacterInfo.UserName = Users.UserNamefilter(this.TxtUserName.Text);
            contacterInfo.Phs = this.TxtPHS.Text;
            contacterInfo.Birthday = string.IsNullOrEmpty(this.TxtBirthday.Text) ? null : new DateTime?(this.TxtBirthday.Date);
            contacterInfo.Position = this.TxtPosTitle.Text;
            usersInfo.UserName = Users.UserNamefilter(this.TxtUserName.Text);
            usersInfo.UserPassword = StringHelper.MD5(this.TxtPassword.Text);
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
            string str2 = "";
            if (ApiData.IsAPiEnable())
            {
                str2 = ApiFunction.RegUser(usersInfo.UserName, this.TxtPassword.Text, usersInfo.Question, usersInfo.Answer, usersInfo.Email, contacterInfo.TrueName, contacterInfo.Sex.ToString(), contacterInfo.Birthday.ToString(), contacterInfo.QQ, contacterInfo.Msn, contacterInfo.Mobile, contacterInfo.OfficePhone, contacterInfo.Province, contacterInfo.City, contacterInfo.Address, contacterInfo.ZipCode, contacterInfo.Homepage);
                if (str2 != "true")
                {
                    DynamicPage.WriteErrMsg(str2 + "<br><li>注册失败！</li>");
                }
                str2 = ApiFunction.RegLogOn(usersInfo.UserName, this.TxtPassword.Text, "1");
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
                goto Label_0665;
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
                        goto Label_063A;

                    case 2:
                        howMany = this.userSiteConfig.PresentValidNum * 30;
                        goto Label_063A;

                    case 3:
                        howMany = this.userSiteConfig.PresentValidNum * 0x16d;
                        goto Label_063A;
                }
                howMany = this.userSiteConfig.PresentValidNum;
            }
        Label_063A:
            strategy2 = new UserDate();
            strategy2.IncreaseForUsers(usersInfo.UserId.ToString(), howMany, "注册时赠送有效期", true, "注册时赠送有效期");
        Label_0665:
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
                str3 = "<li><a href='Company/RegCompany.aspx'>继续注册企业?</a></li>";
            }
            if (usersInfo.Status == UserStatus.None)
            {
                UserPrincipal principal = new UserPrincipal();
                principal.UserName = usersInfo.UserName;
                principal.LastPassword = usersInfo.LastPassword;
                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, usersInfo.UserName, DateTime.Now, DateTime.Now.AddMinutes(60.0), false, principal.SerializeToString());
                string str4 = FormsAuthentication.Encrypt(ticket);
                HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, str4);
                cookie.HttpOnly = true;
                cookie.Path = FormsAuthentication.FormsCookiePath;
                cookie.Secure = FormsAuthentication.RequireSSL;
                base.Response.Cookies.Add(cookie);
            }
            if (SiteConfig.ShopConfig.IsPayPassword)
            {
                BasePage.ResponseRedirect("RegisterPayPassword.aspx?Url=Register");
            }
            else
            {
                DynamicPage.WriteSuccessMsg("<li>注册成功！" + str3 + "</li>" + str2, "Default.aspx");
            }
        }

        private void CheckCode()
        {
            if (this.userSiteConfig.EnableCheckCodeOfReg && (string.Compare(this.TxtValidateCode.Text.Trim(), this.VcodeRegister.ValidateCodeValue, StringComparison.OrdinalIgnoreCase) != 0))
            {
                DynamicPage.WriteErrMsg("<li>您输入的验证码和系统产生的不一致，请重新输入。</li>");
            }
        }

        private void CheckEmail()
        {
            if (!this.userSiteConfig.EnableMultiRegPerEmail && !Users.GetUsersByEmail(this.TxtEmail.Text).IsNull)
            {
                DynamicPage.WriteErrMsg("<li>Email已被他人注册，请输入不同的Email！</li>");
            }
        }

        private void CheckQAofReg()
        {
            if (this.userSiteConfig.EnableQAofReg)
            {
                if (!string.IsNullOrEmpty(this.userSiteConfig.RegQuestion1) && (this.TxtRegAnswer1.Text != this.userSiteConfig.RegAnswer1))
                {
                    DynamicPage.WriteErrMsg("<li>您回答网站注册问题一不正确，请重新输入。</li>");
                }
                if (!string.IsNullOrEmpty(this.userSiteConfig.RegQuestion2) && (this.TxtRegAnswer2.Text != this.userSiteConfig.RegAnswer2))
                {
                    DynamicPage.WriteErrMsg("<li>您回答网站注册问题二不正确，请重新输入。</li>");
                }
                if (!string.IsNullOrEmpty(this.userSiteConfig.RegQuestion3) && (this.TxtRegAnswer3.Text != this.userSiteConfig.RegAnswer3))
                {
                    DynamicPage.WriteErrMsg("<li>您回答网站注册问题三不正确，请重新输入。</li>");
                }
            }
        }

        private void CheckUserName()
        {
            if (StringHelper.FoundCharInArr(this.userSiteConfig.UserNameRegDisabled, this.TxtUserName.Text, "|"))
            {
                DynamicPage.WriteErrMsg("<li>该用户名禁止注册，请输入不同的用户名！</li>");
            }
            if (Users.UserNamefilter(this.TxtUserName.Text).Length != this.TxtUserName.Text.Length)
            {
                DynamicPage.WriteErrMsg("<li>注册用户名中有非法字符！</li>");
            }
            if (Users.Exists(Users.UserNamefilter(this.TxtUserName.Text)))
            {
                DynamicPage.WriteErrMsg("<li>该用户名已被他人占用，请输入不同的用户名！</li>");
            }
        }

        public string GetCallbackResult()
        {
            return this.result;
        }

        protected string GetDisplayStyle(string field)
        {
            if (StringHelper.FoundCharInArr(this.userSiteConfig.RegFieldsMustFill, field, ","))
            {
                return "";
            }
            if (StringHelper.FoundCharInArr(this.userSiteConfig.RegFieldsSelectFill, field, ","))
            {
                return "";
            }
            return "none";
        }

        protected bool GetEnableValid(string field)
        {
            return StringHelper.FoundCharInArr(this.userSiteConfig.RegFieldsMustFill, field, ",");
        }

        private void InitProtocol()
        {
            try
            {
                this.LitProtocol.Text = FileSystemObject.ReadFile(base.Request.MapPath("~/User/Protocol.txt"));
            }
            catch (FileNotFoundException)
            {
                DynamicPage.WriteErrMsg("<li>Protocol.txt文件未找到</li>", "../Default.aspx");
            }
            catch (UnauthorizedAccessException)
            {
                DynamicPage.WriteErrMsg("<li>检查您的服务器是否给文件或文件夹写入权限。</li>", "../Default.aspx");
            }
        }

        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);
            this.userSiteConfig = SiteConfig.UserConfig;
            if (this.Page.IsPostBack)
            {
                string str = base.Request.Form["ControlTreeInfo"];
                if (!string.IsNullOrEmpty(str))
                {
                    string[] strArray = str.Split(new string[] { "$$$$$" }, StringSplitOptions.None);
                    if ((strArray[0] != this.userSiteConfig.RegFieldsMustFill) || (strArray[1] != this.userSiteConfig.RegFieldsSelectFill))
                    {
                        base.Response.Redirect(base.Request.RawUrl);
                    }
                }
            }
            string[] strArray2 = string.IsNullOrEmpty(this.userSiteConfig.RegFieldsMustFill) ? new string[0] : this.userSiteConfig.RegFieldsMustFill.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            string[] strArray3 = string.IsNullOrEmpty(this.userSiteConfig.RegFieldsSelectFill) ? new string[0] : this.userSiteConfig.RegFieldsSelectFill.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string str2 in strArray2)
            {
                HtmlTableRow row = this.FindControl("TR" + str2) as HtmlTableRow;
                if (row != null)
                {
                    this.TableRegister.Rows.Remove(row);
                    this.TableRegisterMust.Rows.Add(row);
                }
            }
            foreach (string str3 in strArray3)
            {
                HtmlTableRow row2 = this.FindControl("TR" + str3) as HtmlTableRow;
                if (row2 != null)
                {
                    this.TableRegister.Rows.Remove(row2);
                    this.TableRegisterSelect.Rows.Add(row2);
                }
            }
            if (this.userSiteConfig.EnableQAofReg)
            {
                if (!string.IsNullOrEmpty(this.userSiteConfig.RegQuestion1))
                {
                    this.TableRegister.Rows.Remove(this.TrRegQuestion1);
                    this.TableRegisterMust.Rows.Add(this.TrRegQuestion1);
                }
                if (!string.IsNullOrEmpty(this.userSiteConfig.RegQuestion2))
                {
                    this.TableRegister.Rows.Remove(this.TrRegQuestion2);
                    this.TableRegisterMust.Rows.Add(this.TrRegQuestion2);
                }
                if (!string.IsNullOrEmpty(this.userSiteConfig.RegQuestion3))
                {
                    this.TableRegister.Rows.Remove(this.TrRegQuestion3);
                    this.TableRegisterMust.Rows.Add(this.TrRegQuestion3);
                }
            }
            if (this.userSiteConfig.EnableCheckCodeOfReg)
            {
                this.TableRegister.Rows.Remove(this.TrVcodeRegister);
                this.TableRegisterMust.Rows.Add(this.TrVcodeRegister);
            }
            if (strArray3.Length > 0)
            {
                this.TableRegister.Rows.Remove(this.TRSwicthSelectFill);
                this.TableRegisterMust.Rows.Add(this.TRSwicthSelectFill);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.TxtBirthday.ImageStyle.Add("float", "left");
            this.TxtBirthday.Style.Add("float", "left");
            if (!this.userSiteConfig.EnableUserReg)
            {
                this.PnlRegStep0.Visible = true;
                this.PnlRegStep1.Visible = false;
                this.PnlRegStep2.Visible = false;
            }
            else
            {
                this.callBackReference = this.Page.ClientScript.GetCallbackEventReference(this, "arg", "ReceiveServerData", "context");
                string str = this.Page.ClientScript.GetCallbackEventReference(this, "arg", "ReceiveServerDataByCheckSameData", "context");
                string script = "function CallBackByCheckSameData(arg,context){" + str + ";}";
                this.Page.ClientScript.RegisterClientScriptBlock(base.GetType(), "CallBackByCheckSameData", script, true);
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
                this.InterMessageUserName.Text = "请输入用户名。不能少于" + userNameLimit.ToString() + "个字符，不能超过" + userNameMax.ToString() + "个字符";
                this.ValgTextMaxLength.ErrorMessage = "用户名必须大于" + userNameLimit.ToString() + "个字符并且不能超过" + userNameMax.ToString() + "个字符";
                if (!base.IsPostBack)
                {
                    this.LitControlTreeInfo.BeginTag = "<input name='ControlTreeInfo' type='hidden' value='";
                    this.LitControlTreeInfo.Text = this.userSiteConfig.RegFieldsMustFill + "$$$$$" + this.userSiteConfig.RegFieldsSelectFill;
                    this.LitControlTreeInfo.EndTag = "'/>";
                    this.InitProtocol();
                    if (this.GetDisplayStyle("Income") != "none")
                    {
                        Choiceset.DropDownListDataBind("PE_Contacter", "Income", this.DropIncome);
                    }
                    this.PnlRegStep1.Visible = true;
                }
            }
        }

        public void RaiseCallbackEvent(string eventArgument)
        {
            if (string.IsNullOrEmpty(eventArgument))
            {
                this.result = "empty";
            }
            else if (eventArgument.StartsWith("$QQ"))
            {
                bool flag = Contacter.CheckExistsQQ(eventArgument.Substring(3));
                this.result = "{name:'QQ',value:" + flag.ToString().ToLower() + "}";
            }
            else if (eventArgument.StartsWith("$Msn"))
            {
                bool flag2 = Contacter.CheckExistsMsn(eventArgument.Substring(4));
                this.result = "{name:'Msn',value:" + flag2.ToString().ToLower() + "}";
            }
            else if (eventArgument.StartsWith("$Homepage"))
            {
                string homepage = eventArgument.Substring(9);
                bool flag3 = Contacter.CheckExistsHomepage(homepage) || Company.CheckExistsHomepage(homepage);
                this.result = "{name:'Homepage',value:" + flag3.ToString().ToLower() + "}";
            }
            else if (eventArgument.StartsWith("$OP"))
            {
                string phone = eventArgument.Substring(3);
                bool flag4 = Contacter.CheckExistsPhone(phone) || Company.CheckExistsPhone(phone);
                this.result = "{name:'OP',value:" + flag4.ToString().ToLower() + "}";
            }
            else if (eventArgument.StartsWith("$HP"))
            {
                string str5 = eventArgument.Substring(3);
                bool flag5 = Contacter.CheckExistsPhone(str5) || Company.CheckExistsPhone(str5);
                this.result = "{name:'HP',value:" + flag5.ToString().ToLower() + "}";
            }
            else if (eventArgument.StartsWith("$MO"))
            {
                string str6 = eventArgument.Substring(3);
                bool flag6 = Contacter.CheckExistsPhone(str6) || Company.CheckExistsPhone(str6);
                this.result = "{name:'MO',value:" + flag6.ToString().ToLower() + "}";
            }
            else if (StringHelper.FoundCharInArr(this.userSiteConfig.UserNameRegDisabled, eventArgument, "|"))
            {
                this.result = "disabled";
            }
            else if (Users.Exists(Users.UserNamefilter(eventArgument)))
            {
                this.result = "true";
            }
            else
            {
                this.result = "false";
            }
        }
    }
}

