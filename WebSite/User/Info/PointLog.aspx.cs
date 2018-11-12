namespace EasyOne.WebSite.User.Info
{
    using EasyOne.Components;
    using EasyOne.Controls;
    using EasyOne.Web.UI;
    using EasyOne.WebSite.Controls;
    using System;
    using System.Web.UI.HtmlControls;

    public partial class PointLog : DynamicPage
    {


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                this.SystemPointLog.SearchType = 10;
                this.SystemPointLog.Field = 1;
                this.SystemPointLog.Keyword = PEContext.Current.User.UserName;
            }
        }
    }
}

