namespace EasyOne.WebSite.User
{
    using EasyOne.Accessories;
    using EasyOne.Components;
    using EasyOne.Contents;
    using EasyOne.Controls;
    using EasyOne.Crm;
    using EasyOne.Enumerations;
    using EasyOne.ExtendedControls;
    using EasyOne.Model.Crm;
    using EasyOne.Model.UserManage;
    using EasyOne.ModelControls;
    using EasyOne.UserManage;
    using EasyOne.Web.UI;
    using EasyOne.WebSite.Controls;
    using System;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public partial class Default : DynamicPage
    {
        
        private bool m_ShowCompanyInfo;
        protected UserNavigation UserCenterNavigation;

        protected void BtnBankroll_Click(object sender, EventArgs e)
        {
            BasePage.ResponseRedirect("Info/Bankroll.aspx?ShowType=6");
        }

        protected void BtnConsumeLog_Click(object sender, EventArgs e)
        {
            BasePage.ResponseRedirect("Info/PointLog.aspx");
        }

        protected void BtnDelCompany_Click(object sender, EventArgs e)
        {
            UserInfo usersByUserName = Users.GetUsersByUserName(this.LblUserName.Text);
            if (!usersByUserName.IsNull)
            {
                if (usersByUserName.UserType != UserType.Creator)
                {
                    DynamicPage.WriteErrMsg("<li>你不是企业创建者，不能注销企业！</li>");
                }
                if (usersByUserName.ClientId > 0)
                {
                    DynamicPage.WriteErrMsg("<li>你已经是客户，不能注销企业！</li>");
                }
                if (Users.DeleteCompany(usersByUserName.CompanyId))
                {
                    DynamicPage.WriteSuccessMsg("您已经成功注销企业！", "Default.aspx");
                }
                else
                {
                    DynamicPage.WriteErrMsg("<li>注销企业失败！</li>");
                }
            }
            else
            {
                DynamicPage.WriteErrMsg("<li>未找到您的用户信息！</li>");
            }
        }

        protected void BtnExchangePoint_Click(object sender, EventArgs e)
        {
            BasePage.ResponseRedirect("Info/ExchangePoint.aspx");
        }

        protected void BtnExchangeValidDate_Click(object sender, EventArgs e)
        {
            BasePage.ResponseRedirect("Info/ExchangeValidDate.aspx");
        }

        protected void BtnExitCompany_Click(object sender, EventArgs e)
        {
            Users.RemoveFromCompany(this.LblUserName.Text);
        }

        protected void BtnFavorite_Click(object sender, EventArgs e)
        {
            BasePage.ResponseRedirect("Contents/Favorite.aspx");
        }

        protected void BtnMessage_Click(object sender, EventArgs e)
        {
            BasePage.ResponseRedirect("Message/MessageManager.aspx?ManageType=0");
        }

        protected void BtnModifyPassword_Click(object sender, EventArgs e)
        {
            BasePage.ResponseRedirect("Info/Password.aspx");
        }

        protected void BtnModifyUser_Click(object sender, EventArgs e)
        {
            BasePage.ResponseRedirect("Info/ModifyInfo.aspx");
        }

        protected void BtnOrder_Click(object sender, EventArgs e)
        {
            BasePage.ResponseRedirect("Shop/OrderList.aspx?OrderType=0");
        }

        protected void BtnPayment_Click(object sender, EventArgs e)
        {
            BasePage.ResponseRedirect("Info/Paymentlog.aspx");
        }

        protected void BtnPayOnline_Click(object sneder, EventArgs e)
        {
            BasePage.ResponseRedirect("../PayOnline/SelectPayPlatform.aspx");
        }

        protected void BtnReceive_Click(object sender, EventArgs e)
        {
            BasePage.ResponseRedirect("Contents/Signin.aspx");
        }

        protected void BtnRecharge_Click(object sender, EventArgs e)
        {
            BasePage.ResponseRedirect("Info/Recharge.aspx");
        }

        protected void BtnRechargeLog_Click(object sender, EventArgs e)
        {
            BasePage.ResponseRedirect("Info/ValidLog.aspx");
        }

        protected void BtnRegCompany_Click(object sender, EventArgs e)
        {
            BasePage.ResponseRedirect("company/regcompany.aspx");
        }

        protected void BtnRemitValidate_Click(object sender, EventArgs e)
        {
            BasePage.ResponseRedirect("Shop/ConfirmRemittance.aspx");
        }

        protected void BtnShoppingCart_Click(object sender, EventArgs e)
        {
            BasePage.ResponseRedirect("../Shop/ShoppingCart.aspx");
        }

        private void InitPrivew(UserInfo usersInfo)
        {
            UserPurviewInfo userPurview = usersInfo.UserPurview;
            if (!userPurview.EnableExchangePoint)
            {
                this.BtnExchangePoint.Visible = false;
            }
            if (!userPurview.EnableExchangeValidDate)
            {
                this.BtnExchangeValidDate.Visible = false;
            }
            this.BtnRecharge.Visible = true;
            this.BtnOrder.Visible = true;
            this.BtnReceive.Visible = true;
        }

        protected string IsShow()
        {
            string str = "";
            if (!this.m_ShowCompanyInfo)
            {
                str = "display:none";
            }
            return str;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.BtnExchangePoint.Text = "兑换" + SiteConfig.UserConfig.PointName;
            this.BtnConsumeLog.Text = SiteConfig.UserConfig.PointName + "明细";
            if (string.Compare(SiteConfig.SiteInfo.ProductEdition, "cms", true) == 0)
            {
                this.BtnShoppingCart.Visible = false;
                this.BtnRemitValidate.Visible = false;
            }
            if (!base.IsPostBack)
            {
                UserInfo usersByUserName = Users.GetUsersByUserName(PEContext.Current.User.UserName);
                if (usersByUserName.IsNull)
                {
                    DynamicPage.WriteErrMsg("<li>没有" + PEContext.Current.User.UserName + "的用户信息，请检查此用户是否注册是否存在！</li>");
                }
                this.InitPrivew(usersByUserName);
                this.LblUserName.Text = usersByUserName.UserName;
                this.LblEmail.Text = usersByUserName.Email;
                this.LblGroupName.Text = UserGroups.GetGroupName(usersByUserName.GroupId);
                this.LblUserType.Text = BasePage.EnumToHtml<UserType>(usersByUserName.UserType);
                this.LblBalance.Text = usersByUserName.Balance.ToString("0.00");
                this.LblUserPoint.Text = usersByUserName.UserPoint.ToString();
                this.LblUserExp.Text = usersByUserName.UserExp.ToString();
                this.m_ShowCompanyInfo = false;
                if (SiteConfig.ConfigInfo().UserConfig.EnableRegCompany)
                {
                    switch (usersByUserName.UserType)
                    {
                        case UserType.Persional:
                            this.BtnRegCompany.Visible = true;
                            this.BtnDelCompany.Visible = false;
                            this.BtnExitCompany.Visible = false;
                            break;

                        case UserType.Creator:
                            this.m_ShowCompanyInfo = true;
                            this.LblAuditingCompanyMemberCountTitle.Visible = true;
                            this.LblAuditingCompanyMemberCount.Visible = true;
                            this.LblAuditingCompanyMemberCount.Text = Users.GetAuditingCompanyMemberCount(usersByUserName.CompanyId).ToString() + " 名";
                            if (usersByUserName.ClientId == 0)
                            {
                                this.BtnDelCompany.Visible = true;
                            }
                            break;

                        case UserType.Administrators:
                            this.m_ShowCompanyInfo = true;
                            this.BtnExitCompany.Visible = true;
                            break;

                        case UserType.CommonLeaguer:
                            this.m_ShowCompanyInfo = true;
                            this.BtnExitCompany.Visible = true;
                            break;

                        case UserType.AuditingLeaguer:
                            this.BtnExitCompany.Visible = true;
                            break;
                    }
                }
                if (this.m_ShowCompanyInfo)
                {
                    this.CompanyInfo1.CompanyId = usersByUserName.CompanyId;
                    this.CompanyMemberManage1.CompanyId = usersByUserName.CompanyId;
                    this.CompanyMemberManage1.UserType = usersByUserName.UserType;
                    this.CompanyMemberManage1.UserId = usersByUserName.UserId;
                    this.CompanyMemberManage1.ReturnAddress = "Default.aspx";
                }
                this.LblValidNum.Text = Users.GetValidNum(usersByUserName.EndTime);
                this.LblUnsignedItems.Text = SignInLog.GetNotSignInContentCountByUserName(PEContext.Current.User.UserName).ToString();
                this.LblUnreadMsg.Text = EasyOne.Accessories.Message.UnreadMessageCount(PEContext.Current.User.UserName).ToString();
                if (usersByUserName.IsInheritGroupRole)
                {
                    this.LblSpecialPermission.Text = "继承会员组权限";
                }
                else
                {
                    this.LblSpecialPermission.Text = "单独权限设置";
                }
                this.LblRegTime.Text = usersByUserName.RegTime.ToString("yyyy年MM月dd日");
                this.LblJoinTime.Text = usersByUserName.JoinTime.ToString("yyyy年MM月dd日");
                if (!string.IsNullOrEmpty(usersByUserName.LastLogOnTime.ToString()))
                {
                    this.LblLastLoginTime.Text = usersByUserName.LastLogOnTime.Value.ToString("yyyy年MM月dd日 HH时mm分ss秒");
                }
                this.LblLastLoginIP.Text = usersByUserName.LastLogOnIP;
                ContacterInfo contacterByUserName = new ContacterInfo();
                contacterByUserName = Contacter.GetContacterByUserName(usersByUserName.UserName);
                if (contacterByUserName != null)
                {
                    this.LblTrueName.Text = contacterByUserName.TrueName;
                    this.LblTitle.Text = contacterByUserName.Title;
                    this.LblCountry.Text = contacterByUserName.Country;
                    this.LblProvince.Text = contacterByUserName.Province;
                    this.LblCity.Text = contacterByUserName.City;
                    this.LblZipCode.Text = contacterByUserName.ZipCode;
                    this.LblAddress.Text = contacterByUserName.Address;
                    this.LblOfficePhone.Text = contacterByUserName.OfficePhone;
                    this.LblHomephone.Text = contacterByUserName.HomePhone;
                    this.LblMobile.Text = contacterByUserName.Mobile;
                    this.LblFax.Text = contacterByUserName.Fax;
                    this.LblPHS.Text = contacterByUserName.Phs;
                    this.LblHomePage.Text = contacterByUserName.Homepage;
                    this.LbllEmail.Text = contacterByUserName.Email;
                    this.LblQQ.Text = contacterByUserName.QQ;
                    this.LblMSN.Text = contacterByUserName.Msn;
                    this.LblICQ.Text = contacterByUserName.Icq;
                    this.LblYahoo.Text = contacterByUserName.Yahoo;
                    this.LblUC.Text = contacterByUserName.UC;
                    this.LblAim.Text = contacterByUserName.Aim;
                    if (contacterByUserName.Birthday.HasValue)
                    {
                        this.LblBirthday.Text = contacterByUserName.Birthday.Value.ToString("yyyy年MM月dd日");
                    }
                    this.LblIDCard.Text = contacterByUserName.IdCard;
                    this.LblNativePlace.Text = contacterByUserName.NativePlace;
                    this.LblNation.Text = contacterByUserName.Nation;
                    this.LblSex.Text = BasePage.EnumToHtml<UserSexType>(contacterByUserName.Sex);
                    this.LblMarriage.Text = BasePage.EnumToHtml<UserMarriageType>(contacterByUserName.Marriage);
                    this.LblEducation.Text = Choiceset.GetDictionaryFieldValueByName("PE_Contacter", "Education")[contacterByUserName.Education].DataTextField;
                    this.LblGraduateFrom.Text = contacterByUserName.GraduateFrom;
                    this.LblInterestsOfLife.Text = contacterByUserName.InterestsOfLife;
                    this.LblInterestsOfCulture.Text = contacterByUserName.InterestsOfCulture;
                    this.LblInterestsOfAmusement.Text = contacterByUserName.InterestsOfAmusement;
                    this.LblInterestsOfSport.Text = contacterByUserName.InterestsOfSport;
                    this.LblInterestsOfOther.Text = contacterByUserName.InterestsOfOther;
                    this.LblIncome.Text = Choiceset.GetDictionaryFieldValueByName("PE_Contacter", "Income")[contacterByUserName.Income].DataTextField;
                    this.LblCompany.Text = contacterByUserName.Company;
                    this.LblDepartment.Text = contacterByUserName.Department;
                    this.LblPosition.Text = contacterByUserName.Position;
                    this.LblOperation.Text = contacterByUserName.Operation;
                    this.LblCompanyAddress.Text = contacterByUserName.CompanyAddress;
                }
                if (!SiteConfig.SiteOption.EnablePointMoneyExp)
                {
                    this.BalancePoint.Style.Add("display", "none");
                    this.ExpValid.Style.Add("display", "none");
                    this.BtnExchangePoint.Visible = false;
                    this.BtnExchangeValidDate.Visible = false;
                    this.BtnRecharge.Visible = false;
                    this.BtnRechargeLog.Visible = false;
                    this.BtnConsumeLog.Visible = false;
                    this.BtnBankroll.Visible = false;
                }
                this.BtnPayOnline.Attributes.Add("onclick", "this.form.target='_newName'");
                if (SiteConfig.SiteInfo.ProductEdition.ToLower() != "eshop")
                {
                    this.BtnOrder.Visible = false;
                }
            }
            if (EasyOne.Accessories.Message.UnreadMessageCount(PEContext.Current.User.UserName) > 0)
            {
                string script = "<script language='javascript'>window.open('../User/Message/PopupMessageRead.aspx?Unread=1&MessageID=" + EasyOne.Accessories.Message.GetUnreadMessageFirstId(PEContext.Current.User.UserName).ToString() + "', 'newmessage', 'width=600,height=400,scrollbars=yes,resizable=yes')</script>";
                if (!this.Page.ClientScript.IsClientScriptBlockRegistered(typeof(string), "OpenWindow"))
                {
                    this.Page.ClientScript.RegisterClientScriptBlock(typeof(string), "OpenWindow", script);
                }
            }
        }
    }
}

