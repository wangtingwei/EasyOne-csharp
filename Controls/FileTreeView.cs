namespace EasyOne.Controls
{
    using System;
    using System.ComponentModel;
    using System.IO;
    using System.Web;
    using System.Web.UI;

    public class FileTreeView : Control, INamingContainer
    {
        protected override void CreateChildControls()
        {
            if (!string.IsNullOrEmpty(this.FileDirectory))
            {
                XLoadTree child = new XLoadTree {
                    RootText = this.RootNodeName,
                    RootAction = this.RootAction
                };
                DirectoryInfo info = new DirectoryInfo(HttpContext.Current.Request.PhysicalApplicationPath + @"\" + this.FileDirectory + @"\");
                if (info.Exists)
                {
                    child.XmlSrc = this.DirectoriesXmlUrl;
                    this.Controls.Add(child);
                }
                else
                {
                    LiteralControl control = new LiteralControl("文件目录不存在，请配置正确的文件目录");
                    this.Controls.Add(control);
                }
            }
            else
            {
                LiteralControl control2 = new LiteralControl("请配置文件目录");
                this.Controls.Add(control2);
            }
            if (!this.Page.ClientScript.IsClientScriptBlockRegistered("rightMenu"))
            {
                string script = "<script type=\"text/javascript\">\r\n                                function rightMenu(nodeId,arrModelId,arrModelName,event) {}\r\n                             </script>";
                this.Page.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "rightMenu", script);
            }
        }

        [DefaultValue(""), Description(""), Localizable(true), Category("自定义"), Bindable(true)]
        public string DirectoriesXmlUrl
        {
            get
            {
                string str = (string) this.ViewState["DirectoriesXmlUrl"];
                if (str != null)
                {
                    return str;
                }
                return string.Empty;
            }
            set
            {
                this.ViewState["DirectoriesXmlUrl"] = value;
            }
        }

        [Category("自定义"), DefaultValue(""), Localizable(true), Bindable(true), Description("文件目录")]
        public string FileDirectory
        {
            get
            {
                string str = (string) this.ViewState["FileDirectory"];
                if (str != null)
                {
                    return str;
                }
                return string.Empty;
            }
            set
            {
                this.ViewState["FileDirectory"] = value;
            }
        }

        [Localizable(true), Description(""), Bindable(true), Category("自定义"), DefaultValue("")]
        public string NodeNavigateUrl
        {
            get
            {
                string str = (string) this.ViewState["NodeNavigateUrl"];
                if (str != null)
                {
                    return str;
                }
                return string.Empty;
            }
            set
            {
                this.ViewState["NodeNavigateUrl"] = value;
            }
        }

        [DefaultValue(""), Description(""), Localizable(true), Category("自定义"), Bindable(true)]
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

        [DefaultValue(""), Bindable(true), Localizable(true), Description(""), Category("自定义")]
        public string RootNodeName
        {
            get
            {
                object obj2 = this.ViewState["RootNodeName"];
                if (obj2 != null)
                {
                    return (string) obj2;
                }
                return string.Empty;
            }
            set
            {
                this.ViewState["RootNodeName"] = value;
            }
        }
    }
}

