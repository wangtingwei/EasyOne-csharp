namespace EasyOne.WebSite.Admin.User
{
    using EasyOne.Common;
    using EasyOne.Controls;
    using EasyOne.Crm;
    using EasyOne.Enumerations;
    using EasyOne.Model.Crm;
    using EasyOne.Model.UserManage;
    using EasyOne.UserManage;
    using EasyOne.Web.UI;
    using EasyOne.WebSite.Controls;
    using System;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public partial class RegCompany : AdminPage
    {
        private int m_UserId;

        protected void BtnAppend_Click(object sender, EventArgs e)
        {
            if (this.Page.IsValid)
            {
                string userName = Convert.ToString(this.ViewState["UserName"]);
                int clientId = DataConverter.CLng(this.ViewState["ClientId"]);
                EasyOne.Model.Crm.CompanyInfo companyInfo = new EasyOne.Model.Crm.CompanyInfo();
                this.Company1.Action = "add";
                this.Company1.CompanyClientId = clientId;
                companyInfo = this.Company1.CompanyInfo;
                bool flag = false;
                if (Company.Add(companyInfo))
                {
                    flag = Users.UpdateForCompany(companyInfo.CompanyId, userName, UserType.Creator, 0);
                    if (flag && (clientId > 0))
                    {
                        flag = Client.UpdateForCompany(clientId, companyInfo.CompanyName);
                    }
                }
                if (flag)
                {
                    AdminPage.WriteSuccessMsg("成功创建了新企业：" + companyInfo.CompanyName + "<br>并将会员 " + userName + " 设为这家企业的创建人，拥有这家企业的管理权限（如审核批准其他人的申请）。", "UserShow.aspx?UserID=" + this.m_UserId);
                }
                else
                {
                    AdminPage.WriteSuccessMsg("<li>添加不成功！</li>");
                }
            }
        }

        protected void BtnRegister_Click(object sender, EventArgs e)
        {
            int compayId = DataConverter.CLng(this.HdnCompanyId.Value);
            if (compayId <= 0)
            {
                AdminPage.WriteErrMsg("<li>请指定要加入的企业！</li>");
            }
            EasyOne.Model.Crm.CompanyInfo compayById = Company.GetCompayById(compayId);
            UserType userType = (UserType) DataConverter.CLng(this.RadlUserType.SelectedValue);
            string userName = Convert.ToString(this.ViewState["UserName"]);
            if (compayById.IsNull)
            {
                AdminPage.WriteErrMsg("<li>找不到指定的企业！</li>");
            }
            else if (Users.UpdateForCompany(compayById.CompanyId, userName, userType, compayById.ClientId))
            {
                AdminPage.WriteSuccessMsg("成功将 " + userName + " 加入到企业 " + compayById.CompanyName + " 中！", "UserShow.aspx?UserID=" + this.m_UserId);
            }
            else
            {
                AdminPage.WriteErrMsg("<li>添加不成功！</li>");
            }
        }

        private void CheckUserInfo()
        {
            UserInfo userById = Users.GetUserById(this.m_UserId);
            if (userById.IsNull)
            {
                AdminPage.WriteErrMsg("<li>未找到指定的会员</li>");
            }
            if (userById.CompanyId > 0)
            {
                AdminPage.WriteErrMsg("<li>此会员已经是企业会员！</li>");
            }
            this.ViewState["ClientId"] = userById.ClientId;
            this.ViewState["UserName"] = userById.UserName;
            this.LblUserName.Text = "会员名称：" + userById.UserName;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.m_UserId = BasePage.RequestInt32("UserId");
            if (!this.Page.IsPostBack)
            {
                this.CheckUserInfo();
            }
        }
    }
}

