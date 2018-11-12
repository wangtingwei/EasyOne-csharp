namespace EasyOne.WebSite.Admin.Analytics
{
    using EasyOne.Analytics;
    using EasyOne.Common;
    using EasyOne.Controls;
    using EasyOne.Model.Analytics;
    using EasyOne.Web.UI;
    using System;
    using System.Web.UI.WebControls;

    public partial class ShowClientDetail : AdminPage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if ((!base.IsPostBack && (base.PreviousPage != null)) && (base.PreviousPage.Items["StatVisitorId"] != null))
            {
                StatVisitorInfo statVisitorById = OtherReport.GetStatVisitorById(DataConverter.CLng(base.PreviousPage.Items["StatVisitorId"]));
                if (!statVisitorById.IsNull)
                {
                    int masterTimeZone = OtherReport.GetStatInfoListInfo().MasterTimeZone;
                    DateTime time = statVisitorById.VTime.AddHours((double) (statVisitorById.Timezone - (masterTimeZone / 60)));
                    this.LblVTime.Text = statVisitorById.VTime.ToString();
                    this.LblIP.Text = statVisitorById.IP;
                    this.LblTimezone.Text = "GMT" + statVisitorById.Timezone.ToString();
                    this.LblAddress.Text = statVisitorById.Address;
                    this.LblClientTime.Text = time.ToString();
                    this.LblReferer.Text = statVisitorById.Referer;
                    this.LblSystem.Text = statVisitorById.System;
                    this.LblBrowser.Text = statVisitorById.Browser;
                    this.LblScreen.Text = statVisitorById.Screen;
                    this.LblColor.Text = statVisitorById.Color;
                }
            }
        }
    }
}

