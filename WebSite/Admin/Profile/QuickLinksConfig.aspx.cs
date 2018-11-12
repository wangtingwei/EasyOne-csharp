namespace EasyOne.WebSite.Admin
{
    using EasyOne.AccessManage;
    using EasyOne.Components;
    using EasyOne.Controls;
    using EasyOne.Enumerations;
    using EasyOne.Model.UserManage;
    using EasyOne.UserManage;
    using EasyOne.Web.UI;
    using System;
    using System.Collections.ObjectModel;
    using System.IO;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using System.Xml;

    public partial class QuickLinksConfig : AdminPage
    {
        private Collection<string> quickConfig;
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

        private Collection<string> ConvertToCollection(AdminProfileInfo adminProfileInfo)
        {
            Collection<string> collection = new Collection<string>();
            if (!adminProfileInfo.IsNull)
            {
                if (!string.IsNullOrEmpty(adminProfileInfo.QuickLinksConfig))
                {
                    foreach (string str in adminProfileInfo.QuickLinksConfig.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        collection.Add(str);
                    }
                }
                return collection;
            }
            foreach (XmlNode node in this.xmlDoc.SelectNodes("/Links//Link[@IsDefalutShow='true']"))
            {
                if (this.CheckPermission(node.Attributes["operateCode"].Value))
                {
                    collection.Add(node.Attributes["id"].Value);
                }
            }
            return collection;
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
            AdminProfileInfo adminProfile = AdminProfile.GetAdminProfile(PEContext.Current.Admin.AdminName);
            this.quickConfig = this.ConvertToCollection(adminProfile);
            this.RptQuickLinks.DataSource = this.xmlDoc.SelectNodes("/Links/Module");
            this.RptQuickLinks.DataBind();
        }

        protected void RptQuickLink_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Header)
            {
                Literal literal = e.Item.FindControl("LitMainLink") as Literal;
                RepeaterItem parent = (RepeaterItem) e.Item.Parent.Parent;
                literal.Text = ((XmlNode) parent.DataItem).Attributes["title"].Value;
            }
            if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
            {
                XmlNode dataItem = (XmlNode) e.Item.DataItem;
                e.Item.Visible = false;
                if ((dataItem != null) && this.CheckPermission(dataItem.Attributes["operateCode"].Value))
                {
                    e.Item.Visible = true;
                    string item = dataItem.Attributes["id"].Value;
                    Literal literal2 = (Literal) e.Item.FindControl("LitLink");
                    if (this.quickConfig.Contains(item))
                    {
                        literal2.Text = "<li id=\"_links_" + item + "\"><a id=\"" + item + "Link\" href=\"javascript:DeleteLink('" + item + "','" + dataItem.Attributes["leftUrl"].Value + "','" + dataItem.Attributes["rightUrl"].Value + "','" + dataItem.Attributes["title"].Value + "');\">" + dataItem.Attributes["title"].Value + "<span id=\"" + item + "LinkStatus\" style=\"color:Red\"><b>√</b></span></a></li>";
                    }
                    else
                    {
                        literal2.Text = "<li id=\"_links_" + item + "\"><a id=\"" + item + "Link\" href=\"javascript:AddLink('" + item + "','" + dataItem.Attributes["leftUrl"].Value + "','" + dataItem.Attributes["rightUrl"].Value + "','" + dataItem.Attributes["title"].Value + "');\">" + dataItem.Attributes["title"].Value + "<span id=\"" + item + "LinkStatus\" style=\"color:Red\"></span></a></li>";
                    }
                }
            }
        }

        protected void RptQuickLinks_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
            {
                XmlNode dataItem = (XmlNode) e.Item.DataItem;
                e.Item.Visible = false;
                if ((dataItem != null) && this.CheckPermission(dataItem.Attributes["operateCode"].Value))
                {
                    e.Item.Visible = true;
                    Literal literal = e.Item.FindControl("LitModuleDescription") as Literal;
                    literal.Text = dataItem.Attributes["description"].Value;
                    Repeater repeater = (Repeater) e.Item.FindControl("RptQuickMainLink");
                    repeater.ItemDataBound += new RepeaterItemEventHandler(this.RptQuickMainLink_ItemDataBound);
                    repeater.DataSource = this.xmlDoc.SelectNodes("/Links/Module[@id='" + dataItem.Attributes["id"].Value + "']/Link");
                    repeater.DataBind();
                }
            }
        }

        protected void RptQuickMainLink_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
            {
                XmlNode dataItem = (XmlNode) e.Item.DataItem;
                Repeater repeater = (Repeater) e.Item.FindControl("RptQuickLink");
                repeater.ItemDataBound += new RepeaterItemEventHandler(this.RptQuickLink_ItemDataBound);
                XmlNodeList list = dataItem.SelectNodes("/Links/Module/Link[@id='" + dataItem.Attributes["id"].Value + "']/Link");
                if (list.Count > 0)
                {
                    e.Item.Visible = false;
                    if ((dataItem != null) && this.CheckPermission(dataItem.Attributes["operateCode"].Value))
                    {
                        e.Item.Visible = true;
                        repeater.DataSource = list;
                        repeater.DataBind();
                    }
                }
                else
                {
                    PlaceHolder holder = (PlaceHolder) e.Item.FindControl("PlhQuickLink");
                    holder.Visible = true;
                    e.Item.Visible = false;
                    if ((dataItem != null) && this.CheckPermission(dataItem.Attributes["operateCode"].Value))
                    {
                        e.Item.Visible = true;
                        string item = dataItem.Attributes["id"].Value;
                        Literal literal = (Literal) e.Item.FindControl("LitQuickLink");
                        if (this.quickConfig.Contains(item))
                        {
                            literal.Text = "<li id=\"_links_" + item + "\"><a id=\"" + item + "Link\" href=\"javascript:DeleteLink('" + item + "','" + dataItem.Attributes["leftUrl"].Value + "','" + dataItem.Attributes["rightUrl"].Value + "','" + dataItem.Attributes["title"].Value + "');\">" + dataItem.Attributes["title"].Value + "<span id=\"" + item + "LinkStatus\" style=\"color:Red\"><b>√</b></span></a></li>";
                        }
                        else
                        {
                            literal.Text = "<li id=\"_links_" + item + "\"><a id=\"" + item + "Link\" href=\"javascript:AddLink('" + item + "','" + dataItem.Attributes["leftUrl"].Value + "','" + dataItem.Attributes["rightUrl"].Value + "','" + dataItem.Attributes["title"].Value + "');\">" + dataItem.Attributes["title"].Value + "<span id=\"" + item + "LinkStatus\" style=\"color:Red\"></span></a></li>";
                        }
                    }
                }
            }
        }
    }
}

