namespace EasyOne.WebSite.Admin.User
{
    using EasyOne.Accessories;
    using EasyOne.Common;
    using EasyOne.Components;
    using EasyOne.Controls;
    using EasyOne.Crm;
    using EasyOne.Enumerations;
    using EasyOne.Model.Accessories;
    using EasyOne.Model.Crm;
    using EasyOne.Model.UserManage;
    using EasyOne.ModelControls;
    using EasyOne.UserManage;
    using EasyOne.Web.UI;
    using System;
    using System.Web.UI.WebControls;

    public partial class RegClient : AdminPage
    {
        protected void BtnRegister_Click(object sender, EventArgs e)
        {
            int num = DataConverter.CLng(this.ClientSelect.DataKey);
            if (num == 0)
            {
                AdminPage.WriteErrMsg("<li>请选择要加入的企业客户</li>");
            }
            UserInfo userInfo = this.ViewState["UserInfo"] as UserInfo;
            if (string.IsNullOrEmpty(userInfo.UserTrueName))
            {
                AdminPage.WriteErrMsg("<li>请在修改用户界面中输入会员的真实姓名</li>", "User.aspx?Action=Modify&UserID=" + userInfo.UserId);
            }
            if (userInfo != null)
            {
                userInfo.ClientId = num;
                Users.Update(userInfo);
            }
            ContacterInfo contacterInfo = this.ViewState["ContacterInfo"] as ContacterInfo;
            if (contacterInfo != null)
            {
                contacterInfo.ClientId = num;
                contacterInfo.UserType = ContacterType.EnterpriceMainContacter;
                Contacter.Update(contacterInfo);
            }
            AdminPage.WriteSuccessMsg("成功将会员“" + userInfo.UserName + "”升级为企业客户联系人！", "UserShow.aspx?UserID=" + userInfo.UserId.ToString());
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            if (base.IsValid)
            {
                ClientInfo clientInfo = new ClientInfo();
                clientInfo.ClientId = Client.GetMaxId() + 1;
                clientInfo.ClientNum = this.TxtClientNum.Text.Trim();
                clientInfo.ShortedForm = this.TxtShortedForm.Text.Trim();
                clientInfo.Area = DataConverter.CLng(this.DropArea.SelectedValue);
                clientInfo.ClientField = DataConverter.CLng(this.DropClientField.SelectedValue);
                clientInfo.ValueLevel = DataConverter.CLng(this.DropValueLevel.SelectedValue);
                clientInfo.CreditLevel = DataConverter.CLng(this.DropCreditLevel.SelectedValue);
                clientInfo.Importance = DataConverter.CLng(this.DropImportance.SelectedValue);
                clientInfo.ConnectionLevel = DataConverter.CLng(this.DropConnectionLevel.SelectedValue);
                clientInfo.SourceType = DataConverter.CLng(this.DropSourceType.SelectedValue);
                clientInfo.GroupId = DataConverter.CLng(this.DropGroupID.SelectedValue);
                clientInfo.PhaseType = DataConverter.CLng(this.DropPhaseType.SelectedValue);
                if (string.IsNullOrEmpty(this.HdnClientName.Value))
                {
                    clientInfo.ClientName = clientInfo.ShortedForm;
                }
                else
                {
                    clientInfo.ClientName = this.HdnClientName.Value;
                }
                clientInfo.ClientType = DataConverter.CLng(this.HdnClientType.Value);
                clientInfo.UpdateTime = DateTime.Now;
                clientInfo.CreateTime = DateTime.Now;
                clientInfo.Owner = PEContext.Current.Admin.AdminName;
                if (Client.Add(clientInfo))
                {
                    UserInfo userInfo = this.ViewState["UserInfo"] as UserInfo;
                    if (userInfo != null)
                    {
                        userInfo.ClientId = clientInfo.ClientId;
                        Users.Update(userInfo);
                    }
                    ContacterInfo contacterInfo = this.ViewState["ContacterInfo"] as ContacterInfo;
                    if (contacterInfo != null)
                    {
                        contacterInfo.ClientId = clientInfo.ClientId;
                        Contacter.Update(contacterInfo);
                    }
                    CompanyInfo companyInfo = this.ViewState["CompanyInfo"] as CompanyInfo;
                    if (companyInfo != null)
                    {
                        companyInfo.ClientId = clientInfo.ClientId;
                        Company.Update(companyInfo);
                    }
                    AdminPage.WriteSuccessMsg("成功将会员“" + userInfo.UserName + "”升级为客户！", "UserShow.aspx?UserID=" + userInfo.UserId.ToString());
                }
                else
                {
                    AdminPage.WriteErrMsg("升级不成功！");
                }
            }
        }

        private void CheckUserInfo()
        {
            int userId = BasePage.RequestInt32("UserID");
            if (userId <= 0)
            {
                AdminPage.WriteErrMsg("请指定会员ID！", "UserManage.aspx");
            }
            else
            {
                UserInfo userById = Users.GetUserById(userId);
                if (userById.IsNull)
                {
                    AdminPage.WriteErrMsg("找不到指定的会员！", "UserManage.aspx");
                }
                else
                {
                    if (userById.ClientId > 0)
                    {
                        AdminPage.WriteErrMsg("此会员已经是客户！", "UserManage.aspx");
                    }
                    this.ViewState["UserInfo"] = userById;
                    this.LblUserName.Text = "会员名称：" + userById.UserName;
                    ContacterInfo contacterByUserName = Contacter.GetContacterByUserName(userById.UserName);
                    if (contacterByUserName.IsNull)
                    {
                        AdminPage.WriteErrMsg("此会员还没有填写详细的联系资料，不能升级为客户！", "UserManage.aspx");
                    }
                    else
                    {
                        if (userById.UserType == UserType.Persional)
                        {
                            this.HdnClientType.Value = "1";
                            this.LblClientType.Text = "个人客户";
                            this.HdnClientName.Value = contacterByUserName.TrueName;
                            contacterByUserName.UserType = ContacterType.Persional;
                        }
                        else
                        {
                            if (userById.UserType == UserType.Creator)
                            {
                                contacterByUserName.UserType = ContacterType.EnterpriceMainContacter;
                            }
                            else
                            {
                                contacterByUserName.UserType = ContacterType.EnterpriceSecondContacter;
                            }
                            this.HdnClientType.Value = "0";
                            this.LblClientType.Text = "企业客户";
                            CompanyInfo compayById = Company.GetCompayById(userById.CompanyId);
                            if (compayById.IsNull)
                            {
                                AdminPage.WriteErrMsg("找不到对应的企业信息，不能升级为企业客户！", "UserManage.aspx");
                            }
                            else
                            {
                                this.ViewState["CompanyInfo"] = compayById;
                                this.HdnClientName.Value = compayById.CompanyName;
                            }
                        }
                        this.ViewState["ContacterInfo"] = contacterByUserName;
                    }
                }
            }
        }

        private void DropDownListDataBind(string fieldName, DropDownList dropClient)
        {
            ChoicesetValueInfo item = new ChoicesetValueInfo();
            item.DataTextField = "";
            item.DataValueField = -1;
            item.IsDefault = false;
            ChoicesetValueInfoCollection dictionaryFieldValueByName = Choiceset.GetDictionaryFieldValueByName("PE_Client", fieldName);
            dictionaryFieldValueByName.Insert(0, item);
            dropClient.DataSource = dictionaryFieldValueByName;
            dropClient.DataTextField = "DataTextField";
            dropClient.DataValueField = "DataValueField";
            dropClient.DataBind();
            foreach (ChoicesetValueInfo info2 in dictionaryFieldValueByName)
            {
                if (info2.IsDefault)
                {
                    dropClient.SelectedValue = info2.DataValueField.ToString();
                    break;
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                this.CheckUserInfo();
                this.DropDownListDataBind("Area", this.DropArea);
                this.DropDownListDataBind("ClientField", this.DropClientField);
                this.DropDownListDataBind("ValueLevel", this.DropValueLevel);
                this.DropDownListDataBind("CreditLevel", this.DropCreditLevel);
                this.DropDownListDataBind("Importance", this.DropImportance);
                this.DropDownListDataBind("ConnectionLevel", this.DropConnectionLevel);
                this.DropDownListDataBind("GroupID", this.DropGroupID);
                this.DropDownListDataBind("SourceType", this.DropSourceType);
                this.DropDownListDataBind("PhaseType", this.DropPhaseType);
                this.TxtClientNum.Text = Client.GetClientNum();
            }
        }
    }
}

