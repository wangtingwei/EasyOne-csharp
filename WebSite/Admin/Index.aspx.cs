namespace EasyOne.WebSite.Admin
{
    using EasyOne.AccessManage;
    using EasyOne.Common;
    using EasyOne.Components;
    using EasyOne.Enumerations;
    using EasyOne.Web.UI;
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Text;
    using System.Web;
    using System.Web.UI.HtmlControls;
    using System.Xml;
    using System.Reflection;

    public partial class Index : AdminPage
    {
        //protected HtmlTitle AdminPageTitle;
        //protected HtmlGenericControl ChannelMenuItems;
        protected string CheckNewVersionJSUrl = "";
        public const string CheckNewVersionUrl = "http://update.EasyOne.net/SiteFactory/Version.aspx";
        private static FileVersionInfo fvInfo = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location);
        //protected HtmlHead Head1;
        protected string IndexLeftUrl = "Profile/QuickLinks.aspx";
        protected string IndexRightUrl = "Profile/MyWorktable.aspx";
        //protected HtmlForm myform;
        public static readonly string ProductVersion = fvInfo.ProductVersion;
        //protected HtmlGenericControl SubMenu;
        private XmlDocument xmlDoc;
        private string xmlPath = "menu";

        private bool CheckIsShowSubMenuLi(XmlNode child)
        {
            string str;
            if (child == null)
            {
                return false;
            }
            if ((child.Name == "ModifyPassword") && !PEContext.Current.Admin.AdministratorInfo.EnableModifyPassword)
            {
                return false;
            }
            if (!this.CheckPermission(this.GetAttributeValue(child, "operateCode")))
            {
                return false;
            }
            bool flag = true;
            if ((!SiteConfig.SiteOption.EnablePointMoneyExp && ((str = this.GetAttributeValue(child, "id")) != null)) && (((str == "CardsManage") || (str == "PaymentLogManage")) || (((str == "BankrollItemList") || (str == "PointLog")) || (str == "ValidLog"))))
            {
                flag = false;
            }
            if (child.Attributes["title"] != null)
            {
                child.Attributes["title"].Value = this.GetAttributeValue(child, "title").Replace("点券", SiteConfig.UserConfig.PointName);
            }
            return flag;
        }

        private bool CheckPermission(string operateCode)
        {
            if (operateCode == "None")
            {
                return true;
            }
            if (string.IsNullOrEmpty(operateCode))
            {
                return false;
            }
            return RolePermissions.AccessCheck(operateCode);
        }

        private string GetAttributeValue(XmlNode xmlNode, string attributeName)
        {
            string str = "";
            if (xmlNode != null)
            {
                XmlAttribute attribute = xmlNode.Attributes[attributeName];
                if (attribute != null)
                {
                    str = attribute.Value;
                }
            }
            return str;
        }

        private void InitDivMenu(StringBuilder sb, string nodeName, string path)
        {
            XmlNode node = this.xmlDoc.SelectSingleNode(path);
            if ((node != null) && node.HasChildNodes)
            {
                int num = 0;
                StringBuilder builder = new StringBuilder();
                builder.Append("<div id=\"PopMenu_" + nodeName + "\" onmouseover=\"show('" + nodeName + "')\" onmouseout=\"hide('" + nodeName + "')\" class=\"SubMenuDiv\" onclick=\"hide('" + nodeName + "')\">\n");
                builder.Append("<dl>\n");
                builder.Append("<dd>\n");
                foreach (XmlNode node2 in node)
                {
                    string str2;
                    if ((this.CheckPermission(this.GetAttributeValue(node2, "operateCode")) && (this.GetAttributeValue(node2, "ShowOnMenu") != "false")) && ((SiteConfig.SiteOption.EnablePointMoneyExp || ((str2 = this.GetAttributeValue(node2, "title")) == null)) || ((str2 != "银行账户管理") && (str2 != "在线支付平台管理"))))
                    {
                        if (!SiteConfig.ShopConfig.EnableCoupon)
                        {
                            switch (this.GetAttributeValue(node2, "id"))
                            {
                                case "Coupon":
                                case "CouponManage":
                                case "CouponList":
                                {
                                    continue;
                                }
                            }
                        }
                        num++;
                        builder.Append("<span><a href=\"#\" onclick=\"ShowOperatingList('" + this.GetAttributeValue(node2, "leftUrl") + "','" + this.GetAttributeValue(node2, "rightUrl") + "')\">\n");
                        builder.Append(this.GetAttributeValue(node2, "title"));
                        builder.Append("</a></span>\n");
                    }
                }
                int num2 = num * 0x19;
                builder.Append("</dd>\n");
                builder.Append("</dl>\n");
                builder.Append(string.Concat(new object[] { "<iframe id=\"iframe_", nodeName, "\" width=\"100%\" frameborder=0 height=\"", num2, "px\" style=\"position:absolute; top:0px; z-index:-1; border-style:none;\"></iframe>\n" }));
                builder.Append("</div>\n");
                if (num != 0)
                {
                    sb.Append(builder.ToString());
                }
                else
                {
                    sb.Append("<div id=\"PopMenu_" + nodeName + "\" onmouseover=\"show('" + nodeName + "')\" onmouseout=\"hide('" + nodeName + "')\"></div>");
                }
            }
        }

        private void InitMainMenu()
        {
            XmlNode node = this.xmlDoc.SelectSingleNode(this.xmlPath);
            StringBuilder builder = new StringBuilder();
            if (node == null)
            {
                AdminPage.WriteErrMsg("MainMenu.xml配置文件不存在menu根元素", "Index.aspx");
            }
            if (node.HasChildNodes)
            {
                foreach (XmlNode node2 in node)
                {
                    if (this.CheckPermission(this.GetAttributeValue(node2, "operateCode")))
                    {
                        string attributeValue = this.GetAttributeValue(node2, "id");
                        builder.Append("<li id=\"" + attributeValue + "\" onclick=\"ShowHideLayer('ChannelMenu_" + attributeValue + "')\">");
                        builder.Append("<a href=\"#\" id=\"AChannelMenu_" + attributeValue + "\">");
                        builder.Append("<span id=\"SpanChannelMenu_" + attributeValue + "\">" + this.GetAttributeValue(node2, "title") + "</span></a> </li>");
                    }
                }
            }
            this.ChannelMenuItems.InnerHtml = builder.ToString();
        }

        protected void InitMainMenuJS()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("<script type=\"text/JavaScript\">");
            builder.Append("function show(id)");
            builder.Append("{\n");
            builder.Append("var obj;\n");
            builder.Append("obj=document.getElementById('PopMenu_'+id);\tobj.style.visibility=\"visible\";\n");
            builder.Append("}\n");
            builder.Append("function hide(id)");
            builder.Append("{\n");
            builder.Append("var obj;\n");
            builder.Append("obj=document.getElementById('PopMenu_'+id);\tobj.style.visibility=\"hidden\";\n");
            builder.Append("}\n");
            builder.Append("function hideOthers(id)");
            builder.Append("{\n");
            builder.Append("var divs;\n");
            builder.Append("if(document.all)\n");
            builder.Append("{\n");
            builder.Append("  divs = document.all.tags('DIV');\n");
            builder.Append("}\n");
            builder.Append("else\n");
            builder.Append("{\n");
            builder.Append("  divs = document.getElementsByTagName(\"DIV\");\n");
            builder.Append("}\n");
            builder.Append("\n for(var i = 0 ;i < divs.length;i++)\n");
            builder.Append("{\n");
            builder.Append("\tif(divs[i].id != 'PopMenu_'+id && divs[i].id.indexOf('PopMenu_')>=0)\n");
            builder.Append("\t{");
            builder.Append("\t    divs[i].style.visibility=\"hidden\";\n");
            builder.Append("\t}");
            builder.Append("\t}");
            builder.Append("}");
            builder.Append(" </script>");
            if (!base.ClientScript.IsClientScriptBlockRegistered("ClientScript"))
            {
                base.ClientScript.RegisterClientScriptBlock(base.GetType(), "ClientScript", builder.ToString());
            }
        }

        private void InitSubMenuLi(StringBuilder sb, string channelMenuId)
        {
            string xpath = this.xmlPath + "/channelMenu[@id='" + channelMenuId + "']";
            XmlNode node = this.xmlDoc.SelectSingleNode(xpath);
            if ((node != null) && node.HasChildNodes)
            {
                foreach (XmlNode node2 in node)
                {
                    string str5;
                    string attributeValue = this.GetAttributeValue(node2, "id");
                    if (!this.CheckIsShowSubMenuLi(node2))
                    {
                        continue;
                    }
                    if (node2.Name == "mainMenu")
                    {
                        string str6;
                        bool hasChildNodes = node2.HasChildNodes;
                        if ((((str6 = this.GetAttributeValue(node2, "id")) != null) && (str6 == "NodesManage")) && !PEContext.Current.Admin.IsSuperAdmin)
                        {
                            hasChildNodes = RolePermissions.AccessCheckNodePermission(OperateCode.CurrentNodesManage, -1);
                        }
                        if (hasChildNodes)
                        {
                            sb.Append("<li onmouseover=\"show('" + attributeValue + "');hideOthers('" + attributeValue + "');\" onmouseout=\"hide('" + attributeValue + "')\">\n");
                        }
                        else
                        {
                            sb.Append("<li onmouseover=\"hideOthers('" + attributeValue + "');\">\n");
                        }
                    }
                    else
                    {
                        sb.Append("<li>\n");
                    }
                    string str3 = this.GetAttributeValue(node2, "leftUrl");
                    string str4 = this.GetAttributeValue(node2, "rightUrl");
                    if (this.GetAttributeValue(node2.ParentNode, "id") != "MenuMyDeskTop")
                    {
                        if (string.IsNullOrEmpty(str3) || string.IsNullOrEmpty(str4))
                        {
                            sb.Append("<a href=\"#\">\n");
                        }
                        else
                        {
                            sb.Append("<a href=\"#\" onclick=\"ShowMain('" + str3 + "','" + str4 + "')\">\n");
                        }
                        sb.Append(this.GetAttributeValue(node2, "title"));
                        sb.Append("</a>\n");
                    }
                    else
                    {
                        string name = node2.Name;
                        if (name == null)
                        {
                            goto Label_025C;
                        }
                        if (!(name == "SignOut"))
                        {
                            if (name == "Welcome")
                            {
                                goto Label_0235;
                            }
                            goto Label_025C;
                        }
                        sb.Append("<a id=\"LinkButton1\" href=\"Logout.aspx\">" + node2.InnerText + "</a>");
                    }
                    goto Label_02AD;
                Label_0235:
                    str5 = string.Format(node2.InnerText, PEContext.Current.Admin.AdminName);
                    sb.Append(str5);
                    goto Label_02AD;
                Label_025C:;
                    sb.Append("<a href=\"#\" onclick=\"ShowMain('" + str3 + "','" + str4 + "')\">\n");
                    sb.Append(attributeValue);
                    sb.Append("</a>\n");
                Label_02AD:
                    this.InitDivMenu(sb, attributeValue, xpath + "/mainMenu[@id='" + attributeValue + "']");
                    sb.Append("</li>\n");
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            string str;
            this.AdminPageTitle.Text = SiteConfig.SiteInfo.SiteName + "--后台管理";
            if (this.Session["IndexRightUrl"] != null)
            {
                this.IndexRightUrl = this.Session["IndexRightUrl"].ToString();
                this.Session.Remove("IndexRightUrl");
            }
            if (this.Session["IndexLeftUrl"] != null)
            {
                this.IndexLeftUrl = this.Session["IndexLeftUrl"].ToString();
                this.Session.Remove("IndexLeftUrl");
            }
            this.xmlDoc = new XmlDocument();
            HttpContext current = HttpContext.Current;
            if (current != null)
            {
                str = current.Server.MapPath("~/Admin/Common/MainMenu.xml");
            }
            else
            {
                str = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Admin/Common/MainMenu.xml");
            }
            try
            {
                this.xmlDoc.Load(str);
            }
            catch (XmlException exception)
            {
                AdminPage.WriteErrMsg("MainMenu.xml配置文件不符合XML规范，具体错误信息：" + exception.Message, "Index.aspx");
            }
            this.InitMainMenuJS();
            this.InitMainMenu();
            this.ShowSubMenu();
            this.CheckNewVersionJSUrl = "http://update.EasyOne.net/SiteFactory/Version.aspx?Trade=SiteFactory&Product=" + SiteConfig.SiteInfo.ProductEdition + "&SystemVersion=" + ProductVersion;
        }

        private void ShowSubMenu()
        {
            XmlNode node = this.xmlDoc.SelectSingleNode(this.xmlPath);
            if (node != null)
            {
                StringBuilder sb = new StringBuilder();
                if (node.HasChildNodes)
                {
                    foreach (XmlNode node2 in node)
                    {
                        string attributeValue = this.GetAttributeValue(node2, "id");
                        if (attributeValue == "MenuMyDeskTop")
                        {
                            sb.Append("<div id=\"ChannelMenu_" + attributeValue + "\" style=\"width: 100%;\">\n");
                        }
                        else
                        {
                            sb.Append("<div id=\"ChannelMenu_" + attributeValue + "\" style=\"width: 100%; display: none;\">\n");
                        }
                        sb.Append("<ul>\n");
                        this.InitSubMenuLi(sb, attributeValue);
                        sb.Append("</ul>\n");
                        sb.Append("</div>\n");
                    }
                }
                this.SubMenu.InnerHtml = sb.ToString();
            }
        }

        public string SubMenuArray()
        {
            XmlNode node = this.xmlDoc.SelectSingleNode(this.xmlPath);
            StringBuilder sb = new StringBuilder();
            if (node == null)
            {
                return null;
            }
            if (node.HasChildNodes)
            {
                foreach (XmlNode node2 in node)
                {
                    if (this.CheckPermission(this.GetAttributeValue(node2, "operateCode")))
                    {
                        StringHelper.AppendString(sb, "\"ChannelMenu_" + this.GetAttributeValue(node2, "id") + "\"");
                    }
                }
            }
            sb.Insert(0, "var arr = new Array(");
            sb.Append(");");
            return sb.ToString();
        }
    }
}

