namespace EasyOne.WebSite.Admin.Analytics
{
    using EasyOne.Analytics;
    using EasyOne.Common;
    using EasyOne.Controls;
    using EasyOne.Model.Analytics;
    using EasyOne.Web.UI;
    using System;
    using System.Web.UI.WebControls;

    public partial class StatVisitorReport : AdminPage
    {

        protected void ExtendedGridView1_RowCommand(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "ShowDetail")
            {
                base.Items["StatVisitorId"] = DataConverter.CLng(e.CommandArgument);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                StatInfoListInfo statInfoListInfo = OtherReport.GetStatInfoListInfo();
                this.HdnTimezone.Value = Convert.ToString((int) (statInfoListInfo.MasterTimeZone / 60));
            }
        }
    }
}

