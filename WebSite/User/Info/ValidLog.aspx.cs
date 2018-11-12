namespace EasyOne.WebSite.User.Info
{
    using EasyOne.Components;
    using EasyOne.Controls;
    using EasyOne.Web.UI;
    using EasyOne.WebSite.Controls;
    using System;
    using System.Web.UI.HtmlControls;

    public partial class ValidLog : DynamicPage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                this.SystemValidLog.SearchType = 10;
                this.SystemValidLog.Field = 1;
                this.SystemValidLog.Keyword = PEContext.Current.User.UserName;
            }
        }
    }
}

