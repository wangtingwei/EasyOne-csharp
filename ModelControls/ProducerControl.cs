namespace EasyOne.ModelControls
{
    using EasyOne.Components;
    using System;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    [ToolboxData("<{0}:ProducerControl runat=\"server\"></{0}:ProducerControl>"), Themeable(true)]
    public class ProducerControl : TextBox
    {
        protected override void Render(HtmlTextWriter writer)
        {
            base.Render(writer);
            string str = "";
            str = this.Page.Request.ApplicationPath.Equals("/") ? string.Empty : this.Page.Request.ApplicationPath;
            str = this.Page.Request.Url.Scheme + "://" + this.Page.Request.Url.Authority + str;
            if (this.IsAdminManage)
            {
                str = str + "/" + SiteConfig.SiteOption.ManageDir + "/Shop/";
            }
            else
            {
                str = str + "/User/Accessories/";
            }
            if (this.IsAdminManage || PEContext.Current.User.Identity.IsAuthenticated)
            {
                writer.Write("<span style=\"color: blue\"><=");
                writer.Write("【<span style=\"cursor:pointer;color: green;\" onclick=\"window.open('" + str + "ProducerList.aspx?OpenerText=" + this.ClientID + "','ProducerList' ,'width=600,height=450,resizable=0,scrollbars=yes');\">列表</span>】");
                writer.Write("</span>");
            }
        }

        public bool IsAdminManage
        {
            get
            {
                object obj2 = this.ViewState["IsAdminManage"];
                if (obj2 != null)
                {
                    return (bool) obj2;
                }
                return true;
            }
            set
            {
                this.ViewState["IsAdminManage"] = value;
            }
        }
    }
}

