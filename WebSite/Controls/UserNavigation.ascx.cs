namespace EasyOne.WebSite.Controls
{
    using EasyOne.Common;
    using EasyOne.Components;
    using EasyOne.Enumerations;
    using EasyOne.Model.UserManage;
    using EasyOne.UserManage;
    using EasyOne.Web.UI;
    using System;
    using System.IO;
    using System.Text;
    using System.Web;
    using System.Web.UI.WebControls;
    using System.Xml;

    public partial class UserNavigation : BaseUserControl
    {
        protected string jumptomainrightjs = "";
        protected bool m_IsAgent;
        protected string m_PointName = SiteConfig.UserConfig.PointName;
        protected UserPurviewInfo PurviewInfo;
        protected int totalTab;
        private XmlDocument xmlDoc;

        protected string ChargeTabDisplay()
        {
            string str = "display: ;";
            if (!SiteConfig.SiteOption.EnablePointMoneyExp)
            {
                str = "display: none;";
            }
            return str;
        }

        protected string GetTabStyleDisplay(int tabIndex)
        {
            string[] strArray = new string[] { "content", "shop", "message", "friend", "charge", "user" };
            string str = "display: none;";
            if (string.Compare(this.Tab, strArray[tabIndex], StringComparison.OrdinalIgnoreCase) == 0)
            {
                str = "display: ;";
            }
            if (!SiteConfig.SiteOption.EnablePointMoneyExp && (string.Compare(this.Tab, "charge", StringComparison.OrdinalIgnoreCase) == 0))
            {
                str = "display: none;";
            }
            return str;
        }

        protected string GetTabTitleCss(int tabIndex)
        {
            string[] strArray = new string[] { "content", "shop", "message", "friend", "charge", "user" };
            string str = "U_tabtitle";
            if (string.Compare(this.Tab, strArray[tabIndex], StringComparison.OrdinalIgnoreCase) == 0)
            {
                str = "U_titlemouseover";
            }
            return str;
        }

        private static string GetXmlNodeAttributeVaule(XmlNode xmlNode, string name)
        {
            if ((xmlNode != null) && (xmlNode.Attributes[name] != null))
            {
                return xmlNode.Attributes[name].Value;
            }
            return string.Empty;
        }

        protected void InitJavaScript()
        {
            if (this.IsContentTab())
            {
                StringBuilder builder = new StringBuilder();
                builder.Append("<script type='text/javascript'>\n");
                builder.Append("function JumpToMainRight(leftUrl,rightUrl) {");
                builder.Append(" if (rightUrl != \"\") { parent.frames[\"main_right\"].location = rightUrl;}");
                builder.Append("}");
                if (!string.IsNullOrEmpty(BaseUserControl.RequestString("js")))
                {
                    builder.Append(" window.onload=" + BaseUserControl.RequestString("js") + ";");
                }
                builder.Append("</script>");
                this.jumptomainrightjs = builder.ToString();
            }
        }

        private bool IsContentTab()
        {
            return (string.Compare(this.Tab, "content", StringComparison.OrdinalIgnoreCase) == 0);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            string str;
            this.InitJavaScript();
            UserInfo userInfo = PEContext.Current.User.UserInfo;
            this.PurviewInfo = PEContext.Current.User.PurviewInfo;
            UserGroupsInfo userGroupById = UserGroups.GetUserGroupById(userInfo.GroupId);
            this.m_IsAgent = userGroupById.GroupType == GroupType.Agent;
            if (this.PurviewInfo == null)
            {
                this.PurviewInfo = new UserPurviewInfo(true);
            }
            this.xmlDoc = new XmlDocument();
            HttpContext current = HttpContext.Current;
            if (current != null)
            {
                str = current.Server.MapPath("~/User/Common/MainMenu.xml");
            }
            else
            {
                str = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "User/Common/MainMenu.xml");
            }
            this.xmlDoc.Load(str);
            this.RptTopMenu.ItemDataBound += new RepeaterItemEventHandler(this.RptTopMenu_ItemDataBound);
            this.RptTopMenu.DataSource = this.xmlDoc.SelectNodes("/menu/TopMenu");
            this.RptTopMenu.DataBind();
            this.RptMenu.ItemDataBound += new RepeaterItemEventHandler(this.RptMenu_ItemDataBound);
            this.RptMenu.DataSource = this.xmlDoc.SelectNodes("/menu/TopMenu");
            this.RptMenu.DataBind();
        }

        private void RptMenu_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
            {
                XmlNode dataItem = (XmlNode) e.Item.DataItem;
                Literal literal = (Literal) e.Item.FindControl("LitDivBegin");
                Literal literal2 = (Literal) e.Item.FindControl("LitDivEnd");
                int num = DataConverter.CLng(GetXmlNodeAttributeVaule(dataItem, "id"));
                if ((GetXmlNodeAttributeVaule(dataItem, "Type") != "Recharge") || SiteConfig.SiteOption.EnablePointMoneyExp)
                {
                    if (num > 0)
                    {
                        literal.Text = "<div id=\"content_" + num + "\" style=\"display: none;\"><ul>";
                    }
                    else
                    {
                        literal.Text = "<div id=\"content_" + num + "\"><ul>";
                    }
                    literal2.Text = "</ul></div>";
                    Repeater repeater = (Repeater) e.Item.FindControl("RptSubMenu");
                    repeater.ItemDataBound += new RepeaterItemEventHandler(this.rptSubMenu_ItemDataBound);
                    repeater.DataSource = dataItem.SelectNodes("SubMenu");
                    repeater.DataBind();
                }
            }
        }

        private void rptSubMenu_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
            {
                XmlNode dataItem = (XmlNode) e.Item.DataItem;
                Literal literal = (Literal) e.Item.FindControl("LitMenu");
                string xmlNodeAttributeVaule = GetXmlNodeAttributeVaule(dataItem, "type");
                string str2 = GetXmlNodeAttributeVaule(dataItem, "text");
                bool enablePointMoneyExp = true;
                switch (xmlNodeAttributeVaule)
                {
                    case "Point":
                        str2 = str2.Replace("点券", SiteConfig.UserConfig.PointName);
                        enablePointMoneyExp = SiteConfig.SiteOption.EnablePointMoneyExp;
                        break;

                    case "Agent":
                        if (!this.m_IsAgent)
                        {
                            enablePointMoneyExp = false;
                        }
                        break;

                    case "DonatePoint":
                        str2 = str2.Replace("点券", SiteConfig.UserConfig.PointName);
                        if (this.PurviewInfo.IsNull || !this.PurviewInfo.EnableGivePointToOthers)
                        {
                            enablePointMoneyExp = false;
                        }
                        break;

                    case "BuyPoint":
                        str2 = str2.Replace("点券", SiteConfig.UserConfig.PointName);
                        if (this.PurviewInfo.IsNull || !this.PurviewInfo.EnableBuyPoint)
                        {
                            enablePointMoneyExp = false;
                        }
                        break;

                    case "AddMessage":
                        if (this.PurviewInfo.IsNull || (this.PurviewInfo.MaxSendToUsers == 0))
                        {
                            enablePointMoneyExp = false;
                        }
                        break;

                    case "Wholesale":
                        if (this.PurviewInfo.IsNull || !this.PurviewInfo.Enablepm)
                        {
                            enablePointMoneyExp = false;
                        }
                        break;

                    case "Coupon":
                        if (!SiteConfig.ShopConfig.EnableCoupon)
                        {
                            enablePointMoneyExp = false;
                        }
                        break;

                    case "PayPassword":
                        if (!SiteConfig.ShopConfig.IsPayPassword)
                        {
                            enablePointMoneyExp = false;
                        }
                        break;
                }
                if (enablePointMoneyExp)
                {
                    string str3 = GetXmlNodeAttributeVaule(dataItem, "href");
                    if (!str3.StartsWith("javascript:", StringComparison.OrdinalIgnoreCase))
                    {
                        str3 = base.BasePath + str3;
                    }
                    else if ((xmlNodeAttributeVaule == "content") && !this.IsContentTab())
                    {
                        if (e.Item.ItemIndex == 0)
                        {
                            str3 = base.BasePath + "User/Contents/Index.aspx?action=add";
                        }
                        else
                        {
                            str3 = base.BasePath + "User/Contents/Index.aspx?Js=" + str3.Replace("javascript:", "");
                        }
                    }
                    literal.Text = "<li><a href=\"" + str3 + "\"><img align=\"absmiddle\" src=\"" + base.BasePath + "User/Images/" + GetXmlNodeAttributeVaule(dataItem, "img") + "\" style=\"border: 0\" alt=\"" + str2 + "\" />&nbsp;&nbsp;" + str2 + "</a></li>";
                }
            }
        }

        private void RptTopMenu_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
            {
                XmlNode dataItem = (XmlNode) e.Item.DataItem;
                Literal literal = (Literal) e.Item.FindControl("LitMenu");
                int num = DataConverter.CLng(GetXmlNodeAttributeVaule(dataItem, "id"));
                if ((GetXmlNodeAttributeVaule(dataItem, "Type") != "Recharge") || SiteConfig.SiteOption.EnablePointMoneyExp)
                {
                    this.totalTab++;
                    literal.Text = string.Concat(new object[] { "<li id=\"menu_", num, "\" onmouseover=\"Show_Menu(this)\"><a href=#>", GetXmlNodeAttributeVaule(dataItem, "text"), "</a></li>" });
                }
            }
        }

        public string Tab
        {
            get
            {
                object obj2 = this.ViewState["Tab"];
                if (obj2 != null)
                {
                    return (string) obj2;
                }
                return "content";
            }
            set
            {
                this.ViewState["Tab"] = value;
            }
        }
    }
}

