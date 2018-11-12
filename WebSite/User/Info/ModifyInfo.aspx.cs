namespace EasyOne.WebSite.User.Info
{
    using EasyOne.Api;
    using EasyOne.Common;
    using EasyOne.Components;
    using EasyOne.Controls;
    using EasyOne.Crm;
    using EasyOne.Enumerations;
    using EasyOne.ExtendedControls;
    using EasyOne.Model.Crm;
    using EasyOne.Model.UserManage;
    using EasyOne.UserManage;
    using EasyOne.Web.UI;
    using EasyOne.WebSite.Controls;
    using EasyOne.WebSite.Controls.Crm;
    using System;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public partial class ModifyInfo : DynamicPage
    {
        protected CompanyControl Company1;
        protected ExtendedButton EBtnSubmit;
        protected HtmlForm form1;
        protected HiddenField HdnContacterID;
        protected Label LblUserGroup;
        protected Label LblUserName;
        protected Label LblUserType;
        protected LiaisonInformation LiaisonInformation1;
        private bool m_ShowCompanyInfo;
        protected NumberValidator NumberValidatorFaceHeight;
        protected NumberValidator NumberValidatorFaceWidth;
        protected PersonalInformation PersonalInformation1;
        protected RadioButtonList RadlPrivacySetting;
        protected ScriptManager SmgeRegion;
        protected TextBox TxtAnswer;
        protected TextBox TxtCompany;
        protected TextBox TxtCompanyAddress;
        protected TextBox TxtDepartment;
        protected TextBox TxtEmail;
        protected TextBox TxtFaceHeight;
        protected TextBox TxtFaceWidth;
        protected TextBox TxtOperation;
        protected TextBox TxtPosition;
        protected TextBox TxtQuestion;
        protected TextBox TxtSign;
        protected TextBox TxtTitle;
        protected TextBox TxtTrueName;
        protected TextBox TxtUserFace;
        protected TextBox TxtUserPassword;
        protected UserNavigation UserCenterNavigation;
        protected EasyOne.Controls.RequiredFieldValidator ValrEmail;
        protected EasyOne.Controls.RequiredFieldValidator ValrQuestion;
        protected EmailValidator Vmail;
        protected ExtendedSiteMapPath YourPosition;

        protected void EBtnSubmit_Click(object sender, EventArgs e)
        {
            UserInfo usersByUserName = Users.GetUsersByUserName(PEContext.Current.User.UserName);
            if (!string.IsNullOrEmpty(this.TxtUserPassword.Text))
            {
                usersByUserName.UserPassword = StringHelper.MD5(this.TxtUserPassword.Text.ToLower());
            }
            if (!string.IsNullOrEmpty(this.TxtAnswer.Text))
            {
                usersByUserName.Answer = StringHelper.MD5(this.TxtAnswer.Text);
            }
            usersByUserName.Question = this.TxtQuestion.Text;
            usersByUserName.Email = this.TxtEmail.Text;
            usersByUserName.UserFace = this.TxtUserFace.Text;
            usersByUserName.FaceWidth = DataConverter.CLng(this.TxtFaceWidth.Text);
            usersByUserName.FaceHeight = DataConverter.CLng(this.TxtFaceHeight.Text);
            usersByUserName.Sign = this.TxtSign.Text;
            usersByUserName.PrivacySetting = DataConverter.CLng(this.RadlPrivacySetting.SelectedValue);
            usersByUserName.UserTrueName = this.TxtTrueName.Text.Trim();
            ContacterInfo contacterInfo = new ContacterInfo();
            contacterInfo.ContacterId = DataConverter.CLng(this.HdnContacterID.Value);
            contacterInfo.UserName = usersByUserName.UserName;
            contacterInfo.TrueName = this.TxtTrueName.Text;
            contacterInfo.Title = this.TxtTitle.Text;
            this.PersonalInformation1.GetContacter(contacterInfo);
            this.LiaisonInformation1.GetContacter(contacterInfo);
            usersByUserName.Sex = this.PersonalInformation1.UserSex;
            contacterInfo.Company = this.TxtCompany.Text;
            contacterInfo.Department = this.TxtDepartment.Text;
            contacterInfo.Position = this.TxtPosition.Text;
            contacterInfo.Operation = this.TxtOperation.Text;
            contacterInfo.CompanyAddress = this.TxtCompanyAddress.Text;
            contacterInfo.ClientId = 0;
            contacterInfo.ParentId = 0;
            contacterInfo.CreateTime = DateTime.Now;
            contacterInfo.Owner = "";
            contacterInfo.UserType = ContacterType.EnterpriceMainContacter;
            contacterInfo.UpdateTime = DateTime.Now;
            if (ApiData.IsAPiEnable())
            {
                string str = ApiFunction.UpdateUser(usersByUserName.UserName, this.TxtUserPassword.Text, usersByUserName.Email, usersByUserName.Question, this.TxtAnswer.Text, usersByUserName.UserTrueName, usersByUserName.Sex.ToString(), contacterInfo.Birthday.ToString(), contacterInfo.QQ, contacterInfo.Msn, contacterInfo.Mobile, contacterInfo.OfficePhone, contacterInfo.Address, contacterInfo.ZipCode, contacterInfo.Homepage);
                if (str != "true")
                {
                    DynamicPage.WriteErrMsg("<li>" + str + "</li>");
                }
            }
            if (Users.Update(usersByUserName, contacterInfo))
            {
                if (usersByUserName.UserType == UserType.Creator)
                {
                    this.Company1.Action = "modify";
                    this.Company1.CompanyId = usersByUserName.CompanyId;
                    Company.Update(this.Company1.CompanyInfo);
                }
                DynamicPage.WriteSuccessMsg("<li>修改" + PEContext.Current.User.UserName + "会员信息成功！</li>", "../Default.aspx");
            }
            else
            {
                DynamicPage.WriteErrMsg("<li>修改" + PEContext.Current.User.UserName + "会员信息失败！</li>");
            }
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
            if (!base.IsPostBack)
            {
                UserInfo usersByUserName = Users.GetUsersByUserName(PEContext.Current.User.UserName);
                if (usersByUserName.IsNull)
                {
                    DynamicPage.WriteErrMsg("<li>没有" + PEContext.Current.User.UserName + "的用户信息，请检查此用户是否注册是否存在！</li>");
                }
                if (usersByUserName.UserType == UserType.Creator)
                {
                    this.m_ShowCompanyInfo = true;
                    this.Company1.Action = "modify";
                    this.Company1.CompanyId = usersByUserName.CompanyId;
                    this.Company1.Visible = true;
                }
                this.LblUserName.Text = usersByUserName.UserName;
                this.LblUserGroup.Text = UserGroups.GetUserGroupById(usersByUserName.GroupId).GroupName;
                this.LblUserType.Text = BasePage.EnumToHtml<UserType>(usersByUserName.UserType);
                this.TxtQuestion.Text = usersByUserName.Question.ToString();
                this.TxtEmail.Text = usersByUserName.Email.ToString();
                this.RadlPrivacySetting.Text = usersByUserName.PrivacySetting.ToString();
                this.TxtUserFace.Text = usersByUserName.UserFace.ToString();
                this.TxtFaceWidth.Text = usersByUserName.FaceWidth.ToString();
                this.TxtSign.Text = usersByUserName.Sign.ToString();
                this.TxtFaceHeight.Text = usersByUserName.FaceHeight.ToString();
                this.TxtTrueName.Text = usersByUserName.UserTrueName;
                ContacterInfo contacterInfo = new ContacterInfo();
                contacterInfo = Contacter.GetContacterByUserName(usersByUserName.UserName);
                this.PersonalInformation1.SetContacter(contacterInfo);
                this.LiaisonInformation1.SetContacter(contacterInfo);
                this.TxtTrueName.Text = contacterInfo.TrueName;
                this.TxtTitle.Text = contacterInfo.Title;
                this.HdnContacterID.Value = contacterInfo.ContacterId.ToString();
                this.TxtCompany.Text = contacterInfo.Company;
                this.TxtDepartment.Text = contacterInfo.Department;
                this.TxtPosition.Text = contacterInfo.Position;
                this.TxtOperation.Text = contacterInfo.Operation;
                this.TxtCompanyAddress.Text = contacterInfo.CompanyAddress;
            }
        }
    }
}

