namespace EasyOne.WebSite.User.Accessories
{
    using EasyOne.Accessories;
    using EasyOne.ExtendedControls;
    using EasyOne.Model.Accessories;
    using EasyOne.Web.UI;
    using System;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public partial class DownServerList : DynamicPage
    {
        protected string allDownServer;
        protected Button BtnSearch;
        protected string downserver;
        protected string downserverInput;
        protected HtmlForm form1;
        protected HtmlHead Head1;
        protected int i;
        protected Repeater RepDownServers;
        protected TextBox TxtDownServer;

        private void BindData()
        {
            this.RepDownServers.DataSource = DownServer.GetDownServerList();
            this.RepDownServers.DataBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.BindData();
        }

        protected void RepDownServers_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
            {
                DownServerInfo dataItem = (DownServerInfo) e.Item.DataItem;
                ExtendedLabel label = e.Item.FindControl("LblServerName") as ExtendedLabel;
                if (dataItem.ShowType == 0)
                {
                    label.Text = "<a href=\"#\" onclick=\"add('$$$" + dataItem.ServerName + "|" + dataItem.ServerId.ToString() + "')\" >" + dataItem.ServerName + "</a>";
                }
                else
                {
                    label.Text = string.Concat(new object[] { "<a href=\"#\" onclick=\"add('$$$", dataItem.ServerUrl, "|", dataItem.ServerId.ToString(), "')\" >", dataItem.ServerName, "</a>" });
                }
                this.allDownServer = this.allDownServer + "$$$";
                if (dataItem.ShowType == 0)
                {
                    this.allDownServer = this.allDownServer + dataItem.ServerName;
                }
                else
                {
                    this.allDownServer = this.allDownServer + dataItem.ServerUrl.ToString();
                }
                this.allDownServer = this.allDownServer + "|" + dataItem.ServerId;
            }
        }
    }
}

