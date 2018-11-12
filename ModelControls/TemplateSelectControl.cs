namespace EasyOne.ModelControls
{
    using EasyOne.Components;
    using System;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    [ToolboxData("<{0}:TemplateSelectControl ID=\"FileC\" runat=\"server\"></{0}:TemplateSelectControl>"), Themeable(true)]
    public class TemplateSelectControl : TextBox
    {
        protected override void Render(HtmlTextWriter writer)
        {
            string str = "";
            str = this.Page.Request.ApplicationPath.Equals("/") ? string.Empty : this.Page.Request.ApplicationPath;
            str = (this.Page.Request.Url.Scheme + "://" + this.Page.Request.Url.Authority + str) + "/" + SiteConfig.SiteOption.ManageDir;
            base.Render(writer);
            writer.Write("<input type='Button' value='浏览...' style=\"cursor:pointer;\" onclick=\"window.open('" + str + "/Accessories/TemplateList.aspx?OpenerText=" + this.ClientID + "','FileList' ,'width=670,height=400,resizable=0,scrollbars=yes');\">");
        }

        public string FilePath
        {
            get
            {
                object obj2 = this.ViewState["FilePath"];
                if (obj2 != null)
                {
                    return (string) obj2;
                }
                return string.Empty;
            }
            set
            {
                this.ViewState["FilePath"] = value;
            }
        }
    }
}

