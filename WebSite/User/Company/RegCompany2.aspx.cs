namespace EasyOne.WebSite.User
{
    using EasyOne.Common;
    using EasyOne.Components;
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
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public partial class RegCompany2 : DynamicPage
    {

        private int m_ClickId;
        private string m_UserName;


        protected void BtnAppend_Click(object sender, EventArgs e)
        {
            this.m_ClickId = DataConverter.CLng(this.ViewState["ClientId"]);
            EasyOne.Model.Crm.CompanyInfo companyInfo = new EasyOne.Model.Crm.CompanyInfo();
            this.Company1.Action = "add";
            this.Company1.CompanyClientId = this.m_ClickId;
            companyInfo = this.Company1.CompanyInfo;
            bool flag = false;
            if (Company.Add(companyInfo))
            {
                flag = Users.UpdateForCompany(companyInfo.CompanyId, this.m_UserName, UserType.Creator, 0);
                if (flag && (this.m_ClickId > 0))
                {
                    flag = Client.UpdateForCompany(this.m_ClickId, companyInfo.CompanyName);
                }
            }
            if (flag)
            {
                DynamicPage.WriteSuccessMsg("已经成功注册企业：" + companyInfo.CompanyName + "<br>从现在起，您是这家企业的创建人，拥有这家企业的管理权限（如审核批准其他人的申请）。同时您成为了我们的企业会员，可以享受更多服务！", "../Default.aspx");
            }
            else
            {
                DynamicPage.WriteSuccessMsg("<li>添加不成功！</li>");
            }
        }

        protected void BtnRegister_Click(object sender, EventArgs e)
        {
            EasyOne.Model.Crm.CompanyInfo byCompanyName = Company.GetByCompanyName(this.LblCompanyName.Text);
            if (byCompanyName.IsNull)
            {
                DynamicPage.WriteErrMsg("<li>您要加入的企业不存在！</li>");
            }
            else if (Users.UpdateForCompany(byCompanyName.CompanyId, this.m_UserName, UserType.AuditingLeaguer, 0))
            {
                DynamicPage.WriteSuccessMsg("已经向" + byCompanyName.CompanyName + "的企业创建人发送了加入请求！请等待他（她）的审核批准。", "../Default.aspx");
            }
            else
            {
                DynamicPage.WriteErrMsg("<li>添加不成功！</li>");
            }
        }

        private void CheckUserInfo()
        {
            UserInfo usersByUserName = Users.GetUsersByUserName(this.m_UserName);
            if (usersByUserName.IsNull)
            {
                DynamicPage.WriteErrMsg("<li>未找到对应会员信息</li>");
            }
            if (usersByUserName.CompanyId > 0)
            {
                DynamicPage.WriteErrMsg("<li>您已经是企业会员，不允许重复注册！</li>");
            }
            this.ViewState["ClientId"] = usersByUserName.ClientId;
            if (Contacter.GetContacterByUserName(this.m_UserName).IsNull)
            {
                DynamicPage.WriteErrMsg("<li>您必须先填写好详细的联系信息后才能注册企业！</li>");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.m_UserName = PEContext.Current.User.UserName;
            if (!this.Page.IsPostBack && (base.PreviousPage != null))
            {
                if (!SiteConfig.UserConfig.EnableRegCompany)
                {
                    DynamicPage.WriteErrMsg("系统禁用了企业注册功能，不能注册！");
                }
                RegCompany previousPage = base.PreviousPage as RegCompany;
                string companyName = previousPage.CompanyName;
                if (string.IsNullOrEmpty(companyName))
                {
                    DynamicPage.WriteErrMsg("<li>请输入企业名称！</li>");
                }
                this.CheckUserInfo();
                EasyOne.Model.Crm.CompanyInfo byCompanyName = Company.GetByCompanyName(companyName);
                if (byCompanyName.IsNull)
                {
                    this.PnlDifferent.Visible = true;
                    this.Company1.CompanyName = companyName;
                }
                else
                {
                    this.PnlSame.Visible = true;
                    this.LblName.Text = companyName;
                    this.LblCompanyName.Text = byCompanyName.CompanyName;
                    this.LblAddress.Text = byCompanyName.Address;
                    this.LblCountry.Text = byCompanyName.Country;
                    this.LblProvince.Text = byCompanyName.Province;
                    this.LblCity.Text = byCompanyName.City;
                }
            }
        }
    }
}

