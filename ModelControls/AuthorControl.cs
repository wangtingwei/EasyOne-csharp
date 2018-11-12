namespace EasyOne.ModelControls
{
    using EasyOne.Components;
    using System;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    [Themeable(true), ToolboxData("<{0}:AuthorControl ID=\"AthC\" runat=\"server\"></{0}:AuthorControl>")]
    public class AuthorControl : TextBox
    {
        protected override void Render(HtmlTextWriter writer)
        {
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
            base.Render(writer);
            writer.Write("<span style=\"color: blue\"><=");
            writer.Write("【<span onclick=\"document.getElementById('");
            writer.Write(this.ClientID);
            writer.Write("').value='未知'\" style=\"cursor:pointer;color: red;\">未知</span>】");
            writer.Write("【<span onclick=\"document.getElementById('");
            writer.Write(this.ClientID);
            writer.Write("').value='佚名'\" style=\"cursor:pointer;color: red;\">佚名</span>】");
            if (this.IsAdminManage)
            {
                writer.Write("【<span onclick=\"document.getElementById('");
                writer.Write(this.ClientID);
                writer.Write("').value='");
                writer.Write(PEContext.Current.Admin.UserName);
                writer.Write("'\" style=\"cursor:pointer;color: red;\">");
                writer.Write(PEContext.Current.Admin.UserName);
                writer.Write("</span>】");
                writer.Write("【<span style=\"cursor:pointer;color: red;\" onclick=\"window.open('" + str + "/Accessories/AuthorList.aspx?OpenerText=" + this.ClientID + "','AuthorList' ,'width=600,height=450,resizable=0,scrollbars=yes');\">更多</span>】");
                writer.Write("</span>");
            }
            else if (PEContext.Current.User.Identity.IsAuthenticated)
            {
                writer.Write("【<span onclick=\"document.getElementById('");
                writer.Write(this.ClientID);
                writer.Write("').value='");
                writer.Write(PEContext.Current.User.UserName);
                writer.Write("'\" style=\"cursor:pointer;color: red;\">");
                writer.Write(PEContext.Current.User.UserName);
                writer.Write("</span>】");
                writer.Write("【<span style=\"cursor:pointer;color: red;\" onclick=\"window.open('" + str + "/Accessories/AuthorList.aspx?OpenerText=" + this.ClientID + "','AuthorList' ,'width=600,height=450,resizable=0,scrollbars=yes');\">更多</span>】");
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

        public string UserName
        {
            get
            {
                object obj2 = this.ViewState["UserName"];
                if (obj2 != null)
                {
                    return (string) obj2;
                }
                return string.Empty;
            }
            set
            {
                this.ViewState["UserName"] = value;
            }
        }
    }
}

