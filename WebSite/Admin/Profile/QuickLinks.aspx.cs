namespace EasyOne.WebSite.Admin
{
    using EasyOne.AccessManage;
    using EasyOne.Components;
    using EasyOne.Enumerations;
    using EasyOne.Model.UserManage;
    using EasyOne.UserManage;
    using EasyOne.Web.UI;
    using System;
    using System.IO;
    using System.Web;
    using System.Web.UI.WebControls;
    using System.Xml;

    public partial class QuickLinks : AdminPage
    {
        private XmlDocument xmlDoc;

        private bool CheckPermission(string operateCode)
        {
            if (operateCode == "None")
            {
                return true;
            }
            if (!Enum.IsDefined(typeof(OperateCode), operateCode))
            {
                return false;
            }
            OperateCode code = (OperateCode) Enum.Parse(typeof(OperateCode), operateCode);
            return RolePermissions.AccessCheck(code);
        }

        private void InitRptMenu()
        {
            AdminProfileInfo adminProfile = AdminProfile.GetAdminProfile(PEContext.Current.Admin.AdminName);
            if (!adminProfile.IsNull && (adminProfile.QuickLinksConfig != null))
            {
                string[] strArray = adminProfile.QuickLinksConfig.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                this.RptMenu.DataSource = strArray;
                this.RptMenu.ItemDataBound += new RepeaterItemEventHandler(this.RptMenu_AdminConfigItemDataBound);
                this.RptMenu.DataBind();
            }
            else
            {
                this.RptMenu.DataSource = this.xmlDoc.SelectNodes("/Links//Link[@IsDefalutShow='true']");
                this.RptMenu.ItemDataBound += new RepeaterItemEventHandler(this.RptMenu_DefaultConfigItemDataBound);
                this.RptMenu.DataBind();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            string str;
            this.xmlDoc = new XmlDocument();
            HttpContext current = HttpContext.Current;
            if (current != null)
            {
                str = current.Server.MapPath("~/Admin/Common/QuickLinks.xml");
            }
            else
            {
                str = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Admin/Common/QuickLinks.xml");
            }
            this.xmlDoc.Load(str);
            this.InitRptMenu();
        }

        protected void RptMenu_AdminConfigItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
            {
                string dataItem = (string) e.Item.DataItem;
                e.Item.Visible = false;
                XmlNode node = this.xmlDoc.SelectSingleNode("/Links//Link[@id='" + dataItem + "']");
                if ((node != null) && this.CheckPermission(node.Attributes["operateCode"].Value))
                {
                    e.Item.Visible = true;
                    Literal literal = (Literal) e.Item.FindControl("LitLink");
                    literal.Text = "<li id=\"_links_" + dataItem + "\"><a href=\"javascript:OpenLink('" + node.Attributes["leftUrl"].Value + "','" + node.Attributes["rightUrl"].Value + "')\">" + node.Attributes["title"].Value + "</a></li>";
                }
            }
        }

        protected void RptMenu_DefaultConfigItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
            {
                e.Item.Visible = false;
                XmlNode dataItem = e.Item.DataItem as XmlNode;
                if ((dataItem != null) && this.CheckPermission(dataItem.Attributes["operateCode"].Value))
                {
                    e.Item.Visible = true;
                    Literal literal = (Literal) e.Item.FindControl("LitLink");
                    literal.Text = "<li id=\"_links_" + dataItem.Attributes["id"].Value + "\"><a href=\"javascript:OpenLink('" + dataItem.Attributes["leftUrl"].Value + "','" + dataItem.Attributes["rightUrl"].Value + "')\">" + dataItem.Attributes["title"].Value + "</a></li>";
                }
            }
        }
    }
}

