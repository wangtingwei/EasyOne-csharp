namespace EasyOne.WebSite.Admin.User
{
    using AjaxControlToolkit;
    using EasyOne.Api;
    using EasyOne.Common;
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
    using System.Collections.Generic;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public partial class UserUI : AdminPage
    {
        private bool m_ShowCompanyInfo;

        protected void BtnCancel_Click(object sender, EventArgs e)
        {
            if (DataConverter.CBoolean(BasePage.RequestString("Administrator")))
            {
                BasePage.ResponseRedirect("Administrator.aspx");
            }
            else
            {
                BasePage.ResponseRedirect("UserManage.aspx");
            }
        }

        protected void EBtnSubmit_Click(object sender, EventArgs e)
        {
            UserInfo info;
            bool flag = false;
            int num = DataConverter.CLng(this.DropGroupId.SelectedValue);
            if (this.ViewState["UserInfo"] != null)
            {
                info = this.ViewState["UserInfo"] as UserInfo;
            }
            else
            {
                info = new UserInfo();
            }
            if (!string.IsNullOrEmpty(this.TxtQuestion.Text))
            {
                info.Question = this.TxtQuestion.Text;
            }
            if (!string.IsNullOrEmpty(this.TxtAnswer.Text))
            {
                info.Answer = StringHelper.MD5(this.TxtAnswer.Text);
            }
            info.GroupId = num;
            info.Email = this.TxtEmail.Text;
            info.UserFace = this.TxtUserFace.Text;
            info.FaceWidth = DataConverter.CLng(this.TxtFaceWidth.Text);
            info.FaceHeight = DataConverter.CLng(this.TxtFaceHeight.Text);
            info.Sign = this.TxtSign.Text;
            info.PrivacySetting = DataConverter.CLng(this.RadlPrivacySetting.SelectedValue);
            info.UserTrueName = this.TxtTrueName.Text.Trim();
            ContacterInfo contacterInfo = new ContacterInfo();
            contacterInfo.ContacterId = DataConverter.CLng(this.HdnContacterID.Value);
            contacterInfo.UserName = info.UserName;
            contacterInfo.TrueName = this.TxtTrueName.Text;
            contacterInfo.Title = this.TxtTitle.Text;
            this.PersonalInformation1.GetContacter(contacterInfo);
            this.LiaisonInformation1.GetContacter(contacterInfo);
            info.Sex = this.PersonalInformation1.UserSex;
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
            if (string.Compare(this.ViewState["action"].ToString(), "Add", StringComparison.OrdinalIgnoreCase) == 0)
            {
                info.UserName = Users.UserNamefilter(this.TxtUserName.Text);
                info.UserPassword = StringHelper.MD5(this.TxtUserPassword.Text.ToLower());
                info.JoinTime = DateTime.Now;
                info.RegTime = DateTime.Now;
                info.Status = UserStatus.None;
                info.IsInheritGroupRole = true;
                contacterInfo.UserName = info.UserName;
                if (Users.Exists(info.UserName))
                {
                    AdminPage.WriteSuccessMsg("<li>该用户名已被他人占用，请输入不同的用户名！</li>");
                }
                else
                {
                    if (ApiData.IsAPiEnable())
                    {
                        string str = ApiFunction.RegUser(info.UserName, this.TxtUserPassword.Text.ToLower(), info.Question, info.Answer, info.Email, contacterInfo.TrueName, contacterInfo.Sex.ToString(), contacterInfo.Birthday.ToString(), contacterInfo.QQ, contacterInfo.Msn, contacterInfo.Mobile, contacterInfo.OfficePhone, contacterInfo.Province, contacterInfo.City, contacterInfo.Address, contacterInfo.ZipCode, contacterInfo.Homepage);
                        if (str != "true")
                        {
                            AdminPage.WriteErrMsg(str + "<br><li>增加失败！</li>");
                        }
                    }
                    flag = Users.Add(info, contacterInfo);
                }
            }
            else
            {
                if (this.TxtUserPassword.Text != info.UserPassword)
                {
                    info.UserPassword = StringHelper.MD5(this.TxtUserPassword.Text.ToLower());
                }
                if (ApiData.IsAPiEnable())
                {
                    string str2 = ApiFunction.UpdateUser(info.UserName, this.TxtUserPassword.Text.ToLower(), info.Email, info.Question, this.TxtAnswer.Text, info.UserTrueName, info.Sex.ToString(), contacterInfo.Birthday.ToString(), contacterInfo.QQ, contacterInfo.Msn, contacterInfo.Mobile, contacterInfo.OfficePhone, contacterInfo.Address, contacterInfo.ZipCode, contacterInfo.Homepage);
                    if (str2 != "true")
                    {
                        AdminPage.WriteErrMsg("<li>" + str2 + "</li>");
                    }
                }
                flag = Users.Update(info, contacterInfo);
                if (info.UserType == UserType.Creator)
                {
                    this.Company1.Action = this.ViewState["action"].ToString();
                    this.Company1.CompanyId = info.CompanyId;
                    Company.Update(this.Company1.CompanyInfo);
                }
            }
            string returnurl = "UserShow.aspx?UserID=" + info.UserId.ToString();
            if (DataConverter.CBoolean(BasePage.RequestString("Administrator")))
            {
                returnurl = "Administrator.aspx?UserName=" + base.Server.UrlEncode(info.UserName.ToString());
            }
            if (flag)
            {
                AdminPage.WriteSuccessMsg("<li>保存会员信息成功！</li>", returnurl);
            }
            else
            {
                AdminPage.WriteErrMsg("<li>保存会员信息失败！</li>");
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

        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                string str = BasePage.RequestString("Action", "Add");
                this.ViewState["action"] = str;
                this.UserGroupList();
                this.TxtUserPassword.Attributes.Add("onkeyup", @"value=value.replace(/[\u4E00-\u9FA5\uFF00-\uFFFF]/g,'')");
                this.TxtUserPassword.Attributes.Add("onbeforepaste", @"clipboardData.setData('text',clipboardData.getData('text').replace(/[\u4E00-\u9FA5\uFF00-\uFFFF]/g,''))");
                if (str == "Modify")
                {
                    UserInfo userById = Users.GetUserById(BasePage.RequestInt32("UserID"));
                    if (!userById.IsNull)
                    {
                        this.ValrQuestion.Visible = false;
                        this.ValrAnswer.Visible = false;
                        this.PhAnswer.Visible = true;
                        this.ViewState["UserInfo"] = userById;
                        if (userById.UserType == UserType.Creator)
                        {
                            this.m_ShowCompanyInfo = true;
                            this.Company1.Action = this.ViewState["action"].ToString();
                            this.Company1.CompanyId = userById.CompanyId;
                            this.Company1.Visible = true;
                        }
                        this.DropGroupId.SelectedValue = userById.GroupId.ToString();
                        this.TxtUserName.Text = userById.UserName.ToString();
                        this.TxtUserName.Enabled = false;
                        this.TxtUserPassword.Attributes.Add("value", userById.UserPassword.ToString());
                        this.TxtQuestion.Text = userById.Question.ToString();
                        this.TxtEmail.Text = userById.Email.ToString();
                        this.RadlPrivacySetting.Text = userById.PrivacySetting.ToString();
                        this.TxtUserFace.Text = userById.UserFace.ToString();
                        this.TxtFaceWidth.Text = userById.FaceWidth.ToString();
                        this.TxtSign.Text = userById.Sign.ToString();
                        this.TxtFaceHeight.Text = userById.FaceHeight.ToString();
                        ContacterInfo contacterInfo = new ContacterInfo();
                        contacterInfo = Contacter.GetContacterByUserName(userById.UserName);
                        contacterInfo.Sex = userById.Sex;
                        this.TxtTrueName.Text = contacterInfo.TrueName;
                        this.TxtTitle.Text = contacterInfo.Title;
                        this.HdnContacterID.Value = contacterInfo.ContacterId.ToString();
                        this.TxtCompany.Text = contacterInfo.Company;
                        this.TxtDepartment.Text = contacterInfo.Department;
                        this.TxtPosition.Text = contacterInfo.Position;
                        this.TxtOperation.Text = contacterInfo.Operation;
                        this.TxtCompanyAddress.Text = contacterInfo.CompanyAddress;
                        this.PersonalInformation1.SetContacter(contacterInfo);
                        this.LiaisonInformation1.SetContacter(contacterInfo);
                    }
                }
            }
        }

        protected void UserGroupList()
        {
            IList<UserGroupsInfo> userGroupList = UserGroups.GetUserGroupList(0, 0);
            foreach (UserGroupsInfo info in userGroupList)
            {
                if (info.GroupId == -2)
                {
                    userGroupList.Remove(info);
                    break;
                }
            }
            this.DropGroupId.DataSource = userGroupList;
            this.DropGroupId.DataTextField = "GroupName";
            this.DropGroupId.DataValueField = "GroupId";
            this.DropGroupId.DataBind();
        }
    }
}

