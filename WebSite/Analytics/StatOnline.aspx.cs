namespace EasyOne.WebSite.AnalyticsUI
{
    using EasyOne.Analytics;
    using EasyOne.Components;
    using EasyOne.Model.Analytics;
    using EasyOne.Web.UI;
    using System;
    using System.Web.UI.HtmlControls;

    public partial class StatOnline : AdminPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            base.Response.Expires = 0;
            StatOnlineInfo info = new StatOnlineInfo();
            info.UserIP = PEContext.Current.UserHostAddress;
            info.UserAgent = base.Request.UserAgent.Replace("'", "");
            info.UserPage = (base.Request.UrlReferrer == null) ? string.Empty : base.Request.UrlReferrer.ToString().Replace("'", "");
            Counter.StatOnlineAdd(info);
        }
    }
}

