namespace EasyOne.ModelControls
{
    using EasyOne.Components;
    using System;
    using System.Text;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    [ToolboxData("<{0}:DownLoadUrlControl ID=\"Eah\" runat=\"server\"></{0}:DownLoadUrlControl>")]
    public class DownLoadUrlControl : Control, INamingContainer
    {
        private HtmlButton addUrlButton;
        private HtmlButton deleteUrlButton;
        private HtmlSelect downLoadUrlSelect;
        private string m_Height;
        private bool m_IsVisiableSoftSize;
        private bool m_IsVisibleDownLoadUrl;
        private string m_Value;
        private string m_Width;
        private HtmlButton modifyUrlButton;
        private string moduleName;
        private TextBox softSizeTextBox;
        private FileUploadControl uploadControl;

        protected override void CreateChildControls()
        {
            base.CreateChildControls();
            this.uploadControl = new FileUploadControl();
            this.uploadControl.ID = "UploadControl";
            this.uploadControl.ModuleName = this.moduleName;
            this.softSizeTextBox = new TextBox();
            this.softSizeTextBox.ID = "softSize";
            this.softSizeTextBox.ApplyStyleSheetSkin(this.Page);
            this.softSizeTextBox.Visible = this.m_IsVisiableSoftSize;
            this.downLoadUrlSelect = new HtmlSelect();
            this.downLoadUrlSelect.ID = "DownLoadUrl";
            this.downLoadUrlSelect.Size = 2;
            this.downLoadUrlSelect.Visible = this.m_IsVisibleDownLoadUrl;
            this.downLoadUrlSelect.ApplyStyleSheetSkin(this.Page);
            this.downLoadUrlSelect.Attributes.Add("style", "width: 417px; height: 90px");
            this.addUrlButton = new HtmlButton();
            this.addUrlButton.ID = "addUrl";
            this.addUrlButton.InnerText = "添加外部地址";
            this.addUrlButton.Visible = this.m_IsVisibleDownLoadUrl;
            this.addUrlButton.ApplyStyleSheetSkin(this.Page);
            this.addUrlButton.Attributes.Add("onclick", "AddUrl();");
            this.modifyUrlButton = new HtmlButton();
            this.modifyUrlButton.ID = "modifyUrl";
            this.modifyUrlButton.InnerText = "修改当前地址";
            this.modifyUrlButton.ApplyStyleSheetSkin(this.Page);
            this.modifyUrlButton.Visible = this.m_IsVisibleDownLoadUrl;
            this.modifyUrlButton.Attributes.Add("onclick", "ModifyUrl();");
            this.deleteUrlButton = new HtmlButton();
            this.deleteUrlButton.ID = "deleteUrl";
            this.deleteUrlButton.InnerText = "删除当前地址";
            this.deleteUrlButton.ApplyStyleSheetSkin(this.Page);
            this.deleteUrlButton.Visible = this.m_IsVisibleDownLoadUrl;
            this.deleteUrlButton.Attributes.Add("onclick", "DeleteUrl();");
        }

        private string InitJs()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("<script language=\"javascript\" type=\"text/javascript\">");
            builder.Append("var thisurl,name;");
            builder.Append("name=");
            builder.Append("'moduleName'");
            builder.Append(";");
            builder.Append("function AddUrl(){");
            builder.Append("var o=eval(document.getElementById(\"DownLoadUrl\"));");
            builder.Append("thisurl =name +'|http://';");
            builder.Append("var url=prompt('请输入'+name+'和链接，中间用“|”隔开：',thisurl);");
            builder.Append("if(url!=null&&url!=''){o.options[o.length]=new Option(url,url);}");
            builder.Append("GetDownLoadUrl(o);");
            builder.Append("}");
            builder.Append("function ModifyUrl(){");
            builder.Append("var o=eval(document.getElementById(\"DownLoadUrl\"));");
            builder.Append("if(o.length==0) return false;");
            builder.Append("thisurl=o.value; ");
            builder.Append("if (thisurl=='') {alert('请先选择一个'+name+'，再点修改按钮！');return false;}");
            builder.Append("var url=prompt('请输入'+name+'和链接，中间用“|”隔开：',thisurl);");
            builder.Append("if(url!=thisurl&&url!=null&&url!=''){o.options[o.selectedIndex]=new Option(url,url);}");
            builder.Append("GetDownLoadUrl(o);");
            builder.Append("}");
            builder.Append("function DeleteUrl(){");
            builder.Append("var o =eval(document.getElementById(\"DownLoadUrl\"));");
            builder.Append("if(o.length==0) return false;");
            builder.Append("var thisurl=o.value; ");
            builder.Append("if (thisurl=='') {alert('请先选择一个'+name+'，再点删除按钮！');return false;}");
            builder.Append(" o.options[o.selectedIndex]=null;");
            builder.Append("GetDownLoadUrl(o);");
            builder.Append("}");
            builder.Append("function GetDownLoadUrl(o){");
            builder.Append("var strUrl;");
            builder.Append("for(var i=0;i<o.length;i++)");
            builder.Append("{strUrl=o.options[i].value+\"$\"+strUrl;}");
            builder.Append("document.getElementById(\"");
            builder.Append(this.uploadControl.ClientID);
            builder.Append("\").value=strUrl;");
            builder.Append("}");
            builder.Append("</script>");
            return builder.ToString();
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            if (this.m_IsVisibleDownLoadUrl)
            {
                Type type = base.GetType();
                if (!this.Page.ClientScript.IsClientScriptBlockRegistered(type, "UrlScript"))
                {
                    this.Page.ClientScript.RegisterClientScriptBlock(type, "UrlScript", this.InitJs());
                }
            }
        }

        protected override void Render(HtmlTextWriter writer)
        {
            writer.RenderBeginTag(HtmlTextWriterTag.Table);
            writer.AddAttribute(HtmlTextWriterAttribute.Width, this.m_Width);
            writer.AddAttribute(HtmlTextWriterAttribute.Height, this.m_Height);
            writer.RenderBeginTag(HtmlTextWriterTag.Tr);
            writer.RenderBeginTag(HtmlTextWriterTag.Td);
            this.downLoadUrlSelect.RenderControl(writer);
            writer.RenderEndTag();
            writer.RenderEndTag();
            writer.RenderBeginTag(HtmlTextWriterTag.Tr);
            writer.RenderBeginTag(HtmlTextWriterTag.Td);
            this.addUrlButton.RenderControl(writer);
            this.modifyUrlButton.RenderControl(writer);
            this.deleteUrlButton.RenderControl(writer);
            writer.RenderEndTag();
            writer.RenderEndTag();
            writer.RenderBeginTag(HtmlTextWriterTag.Tr);
            writer.RenderBeginTag(HtmlTextWriterTag.Td);
            string str = "";
            str = this.Page.Request.ApplicationPath.Equals("/") ? string.Empty : this.Page.Request.ApplicationPath;
            str = this.Page.Request.Url.Scheme + "://" + this.Page.Request.Url.Authority + str + "/" + SiteConfig.SiteOption.ManageDir;
            writer.Write("<input type=\"hidden\" id=\"{0}\" name=\"{1}\" value=\"{2}\" />", this.ClientID, this.UniqueID, this.m_Value);
            writer.Write("<iframe id=\"{0}___Frame\" style=\"top:2px\" src=\"{1}\" width=\"{2}\" height=\"{3}\" frameborder=\"0\" scrolling=\"no\"></iframe>", new object[] { this.ClientID, str + "/Accessories/FileUpload.aspx?ModuleName=" + this.ModuleName + "&InstanceName=" + this.ClientID, this.m_Width, this.m_Height });
            writer.RenderEndTag();
            writer.RenderEndTag();
            writer.RenderBeginTag(HtmlTextWriterTag.Tr);
            writer.RenderBeginTag(HtmlTextWriterTag.Td);
            this.softSizeTextBox.RenderControl(writer);
            writer.RenderEndTag();
            writer.RenderEndTag();
            writer.RenderEndTag();
            writer.EndRender();
        }

        public string Height
        {
            get
            {
                return this.m_Height;
            }
            set
            {
                this.m_Height = value;
            }
        }

        public bool IsVisibleDownLoadUrl
        {
            get
            {
                return this.m_IsVisibleDownLoadUrl;
            }
            set
            {
                this.m_IsVisibleDownLoadUrl = value;
            }
        }

        public bool IsVisibleSoftSize
        {
            get
            {
                return this.m_IsVisiableSoftSize;
            }
            set
            {
                this.m_IsVisiableSoftSize = value;
            }
        }

        public string ModuleName
        {
            get
            {
                return this.moduleName;
            }
            set
            {
                this.moduleName = value;
            }
        }

        public string Value
        {
            get
            {
                return this.m_Value;
            }
            set
            {
                this.m_Value = value;
            }
        }

        public string Width
        {
            get
            {
                return this.m_Width;
            }
            set
            {
                this.m_Width = value;
            }
        }
    }
}

