namespace EasyOne.WebSite.Admin.User
{
    using EasyOne.Components;
    using EasyOne.ExtendedControls;
    using EasyOne.UserManage;
    using EasyOne.Web.UI;
    using System;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public partial class UserGuide : AdminPage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack && !SiteConfig.SiteOption.EnablePointMoneyExp)
            {
                this.ListType6.Style.Add("display", "none");
                this.ListType7.Style.Add("display", "none");
                this.ListType8.Style.Add("display", "none");
                this.ListType9.Style.Add("display", "none");
                this.ListType16.Style.Add("display", "none");
                this.ListType17.Style.Add("display", "none");
                this.ListType18.Style.Add("display", "none");
                this.ListType19.Style.Add("display", "none");
            }
            this.RptGroups.DataSource = UserGroups.GetUserGroupList(0, 0);
            this.RptGroups.DataBind();
        }
    }
}

