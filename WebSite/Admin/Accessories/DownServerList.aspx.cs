namespace EasyOne.WebSite.Admin.Accessories
{
    using EasyOne.Accessories;
    using EasyOne.Model.Accessories;
    using EasyOne.Web.UI;
    using System;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public partial class DownServerList : AdminPage
    {
        protected string allDownServer;
        protected string downserver;
        protected string downserverInput;
        protected int i;


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
                Label label = e.Item.FindControl("LblServerName") as Label;
                if (dataItem.ShowType == 0)
                {
                    label.Text = "<a href=\"#\" onclick=\"add('$$$" + dataItem.ServerName + "|" + dataItem.ServerId.ToString() + "')\" >" + dataItem.ServerName + "</a>";
                }
                else
                {
                    label.Text = "<a href=\"#\" onclick=\"add('$$$" + dataItem.ServerLogo + "|" + dataItem.ServerId.ToString() + "')\" >" + dataItem.ServerName + "</a>";
                }
                this.allDownServer = this.allDownServer + "$$$";
                if (dataItem.ShowType == 0)
                {
                    this.allDownServer = this.allDownServer + dataItem.ServerName;
                }
                else
                {
                    this.allDownServer = this.allDownServer + dataItem.ServerLogo;
                }
                this.allDownServer = this.allDownServer + "|" + dataItem.ServerId;
            }
        }
    }
}

