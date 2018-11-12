namespace EasyOne.ModelControls
{
    using EasyOne.Common;
    using EasyOne.Components;
    using System;
    using System.Collections.Specialized;
    using System.Text;
    using System.Web;
    using System.Web.UI;

    [ToolboxData("<{0}:FileUploadControl ID=\"ExtenFileUpload\" runat=\"server\"></{0}:FileUploadControl>")]
    public class FileUploadControl : Control, IPostBackDataHandler
    {
        public bool LoadPostData(string postDataKey, NameValueCollection postCollection)
        {
            if (postCollection != null)
            {
                string str = postDataKey + "__FileId";
                if (postCollection[str] != this.FileId.ToString())
                {
                    this.FileId = DataConverter.CLng(postCollection[str]);
                    return true;
                }
                if (postCollection[postDataKey] != this.FilePath)
                {
                    this.FilePath = postCollection[postDataKey];
                    return true;
                }
            }
            return false;
        }

        protected override void OnPreRender(EventArgs e)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("<script type=\"text/javascript\">\n");
            builder.Append("   function " + this.UniqueID.Replace("$", "").Replace("_", "") + "UpdatePath(path,size,id)\n");
            builder.Append("   {\n");
            builder.Append("     document.getElementById(\"" + this.ClientID + "\").value = path;\n");
            builder.Append("     document.getElementById(\"" + this.ClientID + "__FileId\").value = id;\n");
            builder.Append("   }\n");
            builder.Append("   function " + this.UniqueID.Replace("$", "").Replace("_", "") + "UpdatePathErrMessage(errMessage)\n");
            builder.Append("   {\n");
            builder.Append("     alert(errMessage);\n");
            builder.Append("   }\n");
            builder.Append("</script>\n");
            if (!this.Page.ClientScript.IsClientScriptBlockRegistered(base.GetType(), this.UniqueID))
            {
                this.Page.ClientScript.RegisterClientScriptBlock(base.GetType(), this.UniqueID, builder.ToString());
            }
        }

        public void RaisePostDataChangedEvent()
        {
        }

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
            string str2 = str + "/Accessories/FileUpload.aspx?NodeId=" + this.NodeId + "&ModuleName=" + this.ModuleName + "&ReturnJSFunction=" + this.UniqueID.Replace("$", "").Replace("_", "") + "UpdatePath";
            string str3 = string.IsNullOrEmpty(this.CustomReturnJSFunction) ? "" : ("&CustomReturnJSFunction=" + this.CustomReturnJSFunction);
            str2 = str2 + str3;
            if (this.IsAdminManage || PEContext.Current.User.Identity.IsAuthenticated)
            {
                writer.WriteBeginTag("input");
                writer.WriteAttribute("type", "text");
                writer.WriteAttribute("style", "font-size:9pt;height:15px;width:280px;");
                writer.WriteAttribute("id", this.ClientID);
                writer.WriteAttribute("name", this.UniqueID);
                writer.WriteAttribute("value", HttpUtility.HtmlAttributeEncode(this.FilePath));
                writer.Write(" />");
                writer.Write("<br />");
                writer.WriteBeginTag("input");
                writer.WriteAttribute("type", "hidden");
                writer.WriteAttribute("id", this.ClientID + "__FileId");
                writer.WriteAttribute("name", this.UniqueID + "__FileId");
                writer.WriteAttribute("value", this.FileId.ToString());
                writer.Write(" />");
                writer.Write("<iframe id=\"{0}___Frame\" style=\"top:2px\" src=\"{1}\" width=\"{2}\" height=\"{3}\" frameborder=\"0\" marginheight=\"0\" marginwidth=\"0\" scrolling=\"no\"></iframe>", new object[] { this.ClientID, str2, this.Width, this.Height });
            }
            else
            {
                writer.WriteBeginTag("input");
                writer.WriteAttribute("type", "text");
                writer.WriteAttribute("style", "font-size:9pt;height:15px;width:280px;");
                writer.WriteAttribute("id", this.ClientID + "__FileId");
                writer.WriteAttribute("name", this.UniqueID + "__FileId");
                writer.WriteAttribute("value", HttpUtility.HtmlAttributeEncode(this.FilePath));
                writer.Write(" />");
            }
        }

        public string CustomReturnJSFunction
        {
            get
            {
                object obj2 = this.ViewState["CustomReturnJSFunction"];
                if (obj2 != null)
                {
                    return (string) obj2;
                }
                return "";
            }
            set
            {
                this.ViewState["CustomReturnJSFunction"] = value;
            }
        }

        public int FileId
        {
            get
            {
                object obj2 = this.ViewState["FileId"];
                if (obj2 != null)
                {
                    return (int) obj2;
                }
                return 0;
            }
            set
            {
                this.ViewState["FileId"] = value;
            }
        }

        public string FilePath
        {
            get
            {
                object obj2 = this.ViewState["FilePath"];
                if (obj2 != null)
                {
                    return (string) obj2;
                }
                return "";
            }
            set
            {
                this.ViewState["FilePath"] = value;
            }
        }

        public string Height
        {
            get
            {
                object obj2 = this.ViewState["Height"];
                if (obj2 != null)
                {
                    return (string) obj2;
                }
                return "50";
            }
            set
            {
                this.ViewState["Height"] = value;
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

        public string ModuleName
        {
            get
            {
                object obj2 = this.ViewState["ModuleName"];
                if (obj2 != null)
                {
                    return (string) obj2;
                }
                return "";
            }
            set
            {
                this.ViewState["ModuleName"] = value;
            }
        }

        public string NodeId
        {
            get
            {
                object obj2 = this.ViewState["NodeId"];
                if (obj2 != null)
                {
                    return (string) obj2;
                }
                return "";
            }
            set
            {
                this.ViewState["NodeId"] = value;
            }
        }

        public string Width
        {
            get
            {
                object obj2 = this.ViewState["Width"];
                if (obj2 != null)
                {
                    return (string) obj2;
                }
                return "100%";
            }
            set
            {
                this.ViewState["Width"] = value;
            }
        }
    }
}

