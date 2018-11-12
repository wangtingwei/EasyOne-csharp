namespace EasyOne.ModelControls
{
    using EasyOne.Components;
    using System;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    [Themeable(true), ToolboxData("<{0}:SourceControl runat=\"server\"></{0}:SourceControl>")]
    public class SourceControl : TextBox
    {
        protected override void Render(HtmlTextWriter writer)
        {
            base.Render(writer);
            string str = "";
            str = this.Page.Request.ApplicationPath.Equals("/") ? string.Empty : this.Page.Request.ApplicationPath;
            str = this.Page.Request.Url.Scheme + "://" + this.Page.Request.Url.Authority + str;
            if (this.IsAdminManage)
            {
                str = str + "/" + SiteConfig.SiteOption.ManageDir;
            }
            else
            {
                str = str + "/User";
            }
            writer.Write("<span style=\"color: blue\"><=");
            writer.Write("【<span onclick=\"document.getElementById('");
            writer.Write(this.ClientID);
            writer.Write("').value='本站原创'\" style=\"cursor:pointer;color: red;\">本站原创</span>】");
            if (this.IsAdminManage || PEContext.Current.User.Identity.IsAuthenticated)
            {
                writer.Write("【<span style=\"cursor:pointer;color: red;\" onclick=\"window.open('" + str + "/Accessories/SourceList.aspx?OpenerInput=" + this.ClientID + "','SourceList' ,'width=600,height=450,resizable=0,scrollbars=yes');\">更多</span>】");
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

