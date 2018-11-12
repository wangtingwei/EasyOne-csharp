namespace EasyOne.Controls
{
    using System;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;

    public class XLoadTree : Control
    {
        protected override void OnLoad(EventArgs e)
        {
            Type type = base.GetType();
            if (!this.Page.ClientScript.IsClientScriptIncludeRegistered(type, "Js/xtree.js"))
            {
                string webResourceUrl = this.Page.ClientScript.GetWebResourceUrl(type, "Js/xtree.js");
                this.Page.ClientScript.RegisterClientScriptInclude(type, "Js/xtree.js", webResourceUrl);
            }
            if (!this.Page.ClientScript.IsClientScriptIncludeRegistered(type, "Js/xmlextras.js"))
            {
                string url = this.Page.ClientScript.GetWebResourceUrl(type, "Js/xmlextras.js");
                this.Page.ClientScript.RegisterClientScriptInclude(type, "Js/xmlextras.js", url);
            }
            if (!this.Page.ClientScript.IsClientScriptIncludeRegistered(type, "Js/xloadtree.js"))
            {
                string str3 = this.Page.ClientScript.GetWebResourceUrl(type, "Js/xloadtree.js");
                this.Page.ClientScript.RegisterClientScriptInclude(type, "Js/xloadtree.js", str3);
            }
            if (!this.Page.ClientScript.IsClientScriptIncludeRegistered(type, "Js/xmenu.js"))
            {
                string str4 = this.Page.ClientScript.GetWebResourceUrl(type, "vxmenu.js");
                this.Page.ClientScript.RegisterClientScriptInclude(type, "Js/xmenu.js", str4);
            }
            if (!this.IsApplyStyleSheetCss && !this.Page.ClientScript.IsClientScriptBlockRegistered(type, "css"))
            {
                this.Page.ClientScript.RegisterClientScriptBlock(type, "css", "");
                HtmlLink child = new HtmlLink();
                child.Attributes.Add("type", "text/css");
                child.Attributes.Add("rel", "stylesheet");
                child.Attributes.Add("href", this.Page.ClientScript.GetWebResourceUrl(type, "Css/xmenu.css"));
                this.Page.Header.Controls.Add(child);
                HtmlLink link2 = new HtmlLink();
                link2.Attributes.Add("type", "text/css");
                link2.Attributes.Add("rel", "stylesheet");
                link2.Attributes.Add("href", this.Page.ClientScript.GetWebResourceUrl(type, "Css/xtree.css"));
                this.Page.Header.Controls.Add(link2);
            }
        }

        protected override void Render(HtmlTextWriter writer)
        {
            Type type = base.GetType();
            if (!this.Page.ClientScript.IsClientScriptIncludeRegistered("rightMenujs"))
            {
                writer.Write("<div id=\"menudata\"></div>");
            }
            writer.Write("<script type=\"text/javascript\">\n");
            string webResourceUrl = string.IsNullOrEmpty(this.RootIcon) ? this.Page.ClientScript.GetWebResourceUrl(type, "Images/closefolder.gif") : this.RootIcon;
            if (this.RootIcon == "WebSite")
            {
                webResourceUrl = this.Page.ClientScript.GetWebResourceUrl(type, "Images/WebSite.gif");
            }
            string str2 = string.IsNullOrEmpty(this.OpenRootIcon) ? this.Page.ClientScript.GetWebResourceUrl(type, "Images/closefolder.gif") : this.OpenRootIcon;
            string str3 = string.IsNullOrEmpty(this.FolderIcon) ? this.Page.ClientScript.GetWebResourceUrl(type, "Images/closefolder.gif") : this.FolderIcon;
            string str4 = string.IsNullOrEmpty(this.OpenFolderIcon) ? this.Page.ClientScript.GetWebResourceUrl(type, "Images/openfolder.gif") : this.OpenFolderIcon;
            string str5 = string.IsNullOrEmpty(this.FileIcon) ? this.Page.ClientScript.GetWebResourceUrl(type, "Images/closefolder.gif") : this.FileIcon;
            writer.Write("webFXTreeConfig.rootIcon\t\t= \"" + webResourceUrl + "\";\n");
            writer.Write("webFXTreeConfig.openRootIcon\t= \"" + str2 + "\";\n");
            writer.Write("webFXTreeConfig.folderIcon\t= \"" + str3 + "\";\n");
            writer.Write("webFXTreeConfig.openFolderIcon= \"" + str4 + "\";\n");
            writer.Write("webFXTreeConfig.fileIcon\t\t= \"" + str5 + "\";\n");
            writer.Write("webFXTreeConfig.containerIcon\t= \"" + this.Page.ClientScript.GetWebResourceUrl(type, "Images/closefolder.gif") + "\";\n");
            writer.Write("webFXTreeConfig.linkIcon\t    = \"" + this.Page.ClientScript.GetWebResourceUrl(type, "Images/outlink.gif") + "\";\n");
            writer.Write("webFXTreeConfig.singleIcon\t= \"" + this.Page.ClientScript.GetWebResourceUrl(type, "Images/singlepage.gif") + "\";\n");
            writer.Write("webFXTreeConfig.forbidclosefolder\t= \"" + this.Page.ClientScript.GetWebResourceUrl(type, "Images/forbidclosefolder.gif") + "\";\n");
            writer.Write("webFXTreeConfig.forbidopenfolder\t= \"" + this.Page.ClientScript.GetWebResourceUrl(type, "Images/forbidopenfolder.gif") + "\";\n");
            writer.Write("webFXTreeConfig.lockclosefolder\t= \"" + this.Page.ClientScript.GetWebResourceUrl(type, "Images/lockclosefolder.gif") + "\";\n");
            writer.Write("webFXTreeConfig.lockopenfolder\t= \"" + this.Page.ClientScript.GetWebResourceUrl(type, "Images/lockopenfolder.gif") + "\";\n");
            writer.Write("webFXTreeConfig.purviewclosefolder\t= \"" + this.Page.ClientScript.GetWebResourceUrl(type, "Images/purviewclosefolder.gif") + "\";\n");
            writer.Write("webFXTreeConfig.purviewopenfolder\t= \"" + this.Page.ClientScript.GetWebResourceUrl(type, "Images/purviewopenfolder.gif") + "\";\n");
            writer.Write("webFXTreeConfig.halfclosefolder\t= \"" + this.Page.ClientScript.GetWebResourceUrl(type, "Images/halfcolsefolder.gif") + "\";\n");
            writer.Write("webFXTreeConfig.halfopenfolder\t= \"" + this.Page.ClientScript.GetWebResourceUrl(type, "Images/halfopenfolder.gif") + "\";\n");
            writer.Write("webFXTreeConfig.lMinusIcon\t= \"" + this.Page.ClientScript.GetWebResourceUrl(type, "Images/Lminus.png") + "\";\n");
            writer.Write("webFXTreeConfig.lPlusIcon\t\t= \"" + this.Page.ClientScript.GetWebResourceUrl(type, "Images/Lplus.png") + "\";\n");
            writer.Write("webFXTreeConfig.tMinusIcon\t= \"" + this.Page.ClientScript.GetWebResourceUrl(type, "Images/Tminus.png") + "\";\n");
            writer.Write("webFXTreeConfig.tPlusIcon\t\t= \"" + this.Page.ClientScript.GetWebResourceUrl(type, "Images/Tplus.png") + "\";\n");
            writer.Write("webFXTreeConfig.iIcon\t\t\t= \"" + this.Page.ClientScript.GetWebResourceUrl(type, "Images/I.png") + "\";\n");
            writer.Write("webFXTreeConfig.lIcon\t\t\t= \"" + this.Page.ClientScript.GetWebResourceUrl(type, "Images/L.png") + "\";\n");
            writer.Write("webFXTreeConfig.tIcon\t\t\t= \"" + this.Page.ClientScript.GetWebResourceUrl(type, "Images/T.png") + "\";\n");
            writer.Write("webFXTreeConfig.blankIcon\t\t= \"" + this.Page.ClientScript.GetWebResourceUrl(type, "Images/blank.png") + "\";\n");
            if (this.CheckBox)
            {
                writer.Write("webFXTreeConfig.checkbox = true ;");
            }
            writer.Write("var rti;\n");
            writer.Write("var tree = new WebFXLoadTree(\"" + this.RootText + "\",\"" + this.XmlSrc + "\",\"" + this.RootAction + "\",\"\",\"" + webResourceUrl + "\",\"" + webResourceUrl + "\",\"" + this.RootTarget + "\");\n");
            writer.Write("document.write(tree);\n");
            writer.Write("if (webFXTreeConfig.expanIds != \"\") {\n");
            writer.Write("    var arrId = webFXTreeConfig.expanIds.split(\",\");\n");
            writer.Write("    for (i=0; i < arrId.length;i++){\n");
            writer.Write("        webFXTreeHandler.toggle(arrId[i]);\n");
            writer.Write("    }\n");
            writer.Write("}\n");
            writer.Write("</script>\n");
        }

        public bool CheckBox
        {
            get
            {
                object obj2 = this.ViewState["CheckBox"];
                return ((obj2 != null) && ((bool) obj2));
            }
            set
            {
                this.ViewState["CheckBox"] = value;
            }
        }

        public string FileIcon
        {
            get
            {
                object obj2 = this.ViewState["FileIcon"];
                if (obj2 != null)
                {
                    return (string) obj2;
                }
                return string.Empty;
            }
            set
            {
                this.ViewState["FileIcon"] = value;
            }
        }

        public string FolderIcon
        {
            get
            {
                object obj2 = this.ViewState["FolderIcon"];
                if (obj2 != null)
                {
                    return (string) obj2;
                }
                return string.Empty;
            }
            set
            {
                this.ViewState["FolderIcon"] = value;
            }
        }

        public bool IsApplyStyleSheetCss
        {
            get
            {
                object obj2 = this.ViewState["IsApplyStyleSheetCss"];
                if (obj2 != null)
                {
                    return (bool) obj2;
                }
                return true;
            }
            set
            {
                this.ViewState["IsApplyStyleSheetCss"] = value;
            }
        }

        public string OpenFolderIcon
        {
            get
            {
                object obj2 = this.ViewState["OpenFolderIcon"];
                if (obj2 != null)
                {
                    return (string) obj2;
                }
                return string.Empty;
            }
            set
            {
                this.ViewState["OpenFolderIcon"] = value;
            }
        }

        public string OpenRootIcon
        {
            get
            {
                object obj2 = this.ViewState["OpenRootIcon"];
                if (obj2 != null)
                {
                    return (string) obj2;
                }
                return string.Empty;
            }
            set
            {
                this.ViewState["OpenRootIcon"] = value;
            }
        }

        public string RootAction
        {
            get
            {
                object obj2 = this.ViewState["RootAction"];
                if (obj2 != null)
                {
                    return (string) obj2;
                }
                return string.Empty;
            }
            set
            {
                this.ViewState["RootAction"] = value;
            }
        }

        public string RootIcon
        {
            get
            {
                object obj2 = this.ViewState["RootIcon"];
                if (obj2 != null)
                {
                    return (string) obj2;
                }
                return string.Empty;
            }
            set
            {
                this.ViewState["RootIcon"] = value;
            }
        }

        public string RootTarget
        {
            get
            {
                object obj2 = this.ViewState["RootTarget"];
                if (obj2 != null)
                {
                    return (string) obj2;
                }
                return "main_right";
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    this.ViewState["RootTarget"] = "main_right";
                }
                else
                {
                    this.ViewState["RootTarget"] = value;
                }
            }
        }

        public string RootText
        {
            get
            {
                object obj2 = this.ViewState["RootText"];
                if (obj2 != null)
                {
                    return (string) obj2;
                }
                return string.Empty;
            }
            set
            {
                this.ViewState["RootText"] = value;
            }
        }

        public string XmlSrc
        {
            get
            {
                object obj2 = this.ViewState["XmlSrc"];
                if (obj2 != null)
                {
                    return (string) obj2;
                }
                return string.Empty;
            }
            set
            {
                this.ViewState["XmlSrc"] = value;
            }
        }
    }
}

