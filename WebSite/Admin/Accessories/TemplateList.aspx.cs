namespace EasyOne.WebSite.Admin.Accessories
{
    using EasyOne.Web.UI;
    using System;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public partial class TemplateList : AdminPage
    {
        protected string FilePathInput;

        protected void Page_Load(object sender, EventArgs e)
        {
            this.FilePathInput = base.Request.QueryString["OpenerText"];
        }
    }
}

