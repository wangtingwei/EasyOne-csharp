namespace EasyOne.WebSite.Admin.User
{
    using EasyOne.Controls;
    using EasyOne.UserManage;
    using EasyOne.Web.UI;
    using System;
    using System.Collections.Generic;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using EasyOne.Model.UserManage;

    public partial class AdminList : AdminPage
    {
        private string m_AdminName;
        protected string m_JsFunctionName;
        private int m_OperateCode = -1;

        protected void BtnSearch_Click(object sender, EventArgs e)
        {
            this.m_AdminName = this.TxtKeyWord.Text;
            this.DlstAdminBind();
        }

        private void DlstAdminBind()
        {
            IList<AdministratorInfo> list = new List<AdministratorInfo>();
            if ((this.m_OperateCode != -1) && string.IsNullOrEmpty(this.m_AdminName))
            {
                list = Administrators.GetAdminListByOperateCode((this.Pager.CurrentPageIndex - 1) * this.Pager.PageSize, this.Pager.PageSize, this.m_OperateCode);
            }
            else
            {
                list = Administrators.GetAdminList((this.Pager.CurrentPageIndex - 1) * this.Pager.PageSize, this.Pager.PageSize, this.m_AdminName);
            }
            this.DlstAdmin.DataSource = list;
            this.Pager.RecordCount = Administrators.GetTotalOfAdmin();
            this.DlstAdmin.DataBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.Compare(BasePage.RequestString("OperateCode"), "OrderManage", StringComparison.OrdinalIgnoreCase) == 0)
            {
                this.m_OperateCode = 0x614719c;
            }
            this.m_JsFunctionName = BasePage.RequestString("JsFunctionName", "SelectAdmin");
            if (!this.Page.IsPostBack)
            {
                this.DlstAdminBind();
            }
        }

        protected void Pager_PageChanged(object src, PageChangedEventArgs e)
        {
            this.Pager.CurrentPageIndex = e.NewPageIndex;
            this.DlstAdminBind();
        }
    }
}

