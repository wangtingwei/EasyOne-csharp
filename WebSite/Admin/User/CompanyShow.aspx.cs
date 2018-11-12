namespace EasyOne.WebSite.Admin.User
{
    using EasyOne.Controls;
    using EasyOne.Enumerations;
    using EasyOne.Web.UI;
    using EasyOne.WebSite.Controls;
    using System;

    public partial class CompanyShow : AdminPage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                int num = BasePage.RequestInt32("CompanyID");
                this.CompanyInfo1.CompanyId = num;
                this.CompanyMemberManage1.CompanyId = num;
                this.CompanyMemberManage1.UserType = UserType.Creator;
                this.CompanyMemberManage1.ReturnAddress = "CompanyShow.aspx?CompanyID=" + num.ToString();
            }
        }
    }
}

