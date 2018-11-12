namespace EasyOne.WebSite.Common
{
    using EasyOne.Contents;
    using EasyOne.Web.UI;
    using System;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;

    public partial class GetBrowseTimes : BasePage
    {

        protected override void Render(HtmlTextWriter writer)
        {
            if (!this.Page.IsPostBack)
            {
                int generalId = BasePage.RequestInt32("id", 0);
                base.Response.Write("document.write(" + ContentManage.UpdateBrowseTimes(generalId) + ")");
            }
        }
    }
}

