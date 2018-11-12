namespace EasyOne.WebSite.Admin.Statistics
{
    using EasyOne.Controls;
    using EasyOne.Web.UI;
    using System;
    using System.Web.UI.WebControls;
    using EasyOne.ExtendedControls;

    public partial class SiteCount : AdminPage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            string str = BasePage.RequestString("Action") + "Control.ascx";
            this.PlcControl.Controls.Add(base.LoadControl("~/Controls/Statistics/" + str));
        }
    }
}

