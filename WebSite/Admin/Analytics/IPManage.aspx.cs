namespace EasyOne.WebSite.Admin.Analytics
{
    using EasyOne.Analytics;
    using EasyOne.Common;
    using EasyOne.Controls;
    using EasyOne.Model.Analytics;
    using EasyOne.Web.UI;
    using System;
    using System.Web.UI.WebControls;

    public partial class IPManage : AdminPage
    {

        protected void ExtendedGridView1_DataBound(object sender, EventArgs e)
        {
            this.LblCount.Text = IPStorage.GetTotal().ToString();
        }

        protected void ExtendedGridView1_RowCommand(object sender, CommandEventArgs e)
        {
            if ((e.CommandName == "EditIP") || (e.CommandName == "DelIP"))
            {
                GridViewRow row = ((GridView) sender).Rows[Convert.ToInt32(e.CommandArgument)];
                string commandName = e.CommandName;
                if (commandName != null)
                {
                    if (!(commandName == "EditIP"))
                    {
                        if (!(commandName == "DelIP"))
                        {
                            return;
                        }
                    }
                    else
                    {
                        this.Context.Items.Add("startIP", ((Label) row.FindControl("LblStartIP")).Text);
                        this.Context.Items.Add("endIP", ((Label) row.FindControl("LblEndIP")).Text);
                        this.Context.Items.Add("address", row.Cells[2].Text);
                        base.Server.Transfer("IPAdd.aspx?Action=Edit");
                        return;
                    }
                    StatIPInfo info = new StatIPInfo();
                    info.StartIP = StringHelper.EncodeIP(((Label) row.FindControl("LblStartIP")).Text);
                    info.EndIP = StringHelper.EncodeIP(((Label) row.FindControl("LblEndIP")).Text);
                    if (IPStorage.Delete(info))
                    {
                        this.ExtendedGridView1.DataBind();
                    }
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }
    }
}

