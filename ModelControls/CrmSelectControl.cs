namespace EasyOne.ModelControls
{
    using EasyOne.Components;
    using System;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.Drawing.Design;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    [DefaultProperty("Text"), ToolboxData("<{0}:CrmSelectControl runat=server></{0}:CrmSelectControl>"), Themeable(true), ValidationProperty("Text")]
    public class CrmSelectControl : TextBox, INamingContainer
    {
        private HiddenField m_Value;

        protected override void AddAttributesToRender(HtmlTextWriter writer)
        {
            base.AddAttributesToRender(writer);
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "inputtext");
        }

        protected override void CreateChildControls()
        {
            this.Controls.Clear();
            base.CreateChildControls();
            this.m_Value = new HiddenField();
            this.m_Value.ID = "Value";
            this.Controls.Add(this.m_Value);
            base.ChildControlsCreated = true;
        }

        private string GetDefalutValue()
        {
            string str = string.Empty;
            if (this.IsReturnValue)
            {
                str = "+encodeURI(document.getElementById('" + this.ClientID + "').value)";
            }
            return str;
        }

        private string GetReturnJs()
        {
            if (!string.IsNullOrEmpty(this.TextChangeJS))
            {
                return ("&ReturnJs=" + this.TextChangeJS);
            }
            return string.Empty;
        }

        private string GetUrlArgs()
        {
            string str = string.Empty;
            if (!string.IsNullOrEmpty(this.UrlArgs))
            {
                str = (this.UrlArgs[0] == '&') ? this.UrlArgs : ("&" + this.UrlArgs);
            }
            if (this.IsReturnValue)
            {
                str = str + "&DefaultValue=";
            }
            return str;
        }

        protected override bool LoadPostData(string postDataKey, NameValueCollection postCollection)
        {
            string str = postCollection[postDataKey];
            if (!this.Text.Equals(str))
            {
                this.Text = str;
            }
            return base.LoadPostData(postDataKey, postCollection);
        }

        protected override void Render(HtmlTextWriter writer)
        {
            if (string.IsNullOrEmpty(this.FileUrl))
            {
                throw new HttpException("FileUrl属性不能为空！");
            }
            this.EnsureChildControls();
            string str = "";
            string str2 = "?";
            if (HttpContext.Current != null)
            {
                str = this.Page.Request.ApplicationPath.Equals("/") ? string.Empty : this.Page.Request.ApplicationPath;
                str = this.Page.Request.Url.Scheme + "://" + this.Page.Request.Url.Authority + str;
                str = str + "/" + SiteConfig.SiteOption.ManageDir + "/" + this.FileUrl.ToLower().Replace("~/", "").Replace("admin/", "");
            }
            if (str.IndexOf('?') >= 0)
            {
                str2 = "&";
            }
            this.m_Value.RenderControl(writer);
            base.Render(writer);
            writer.Write("&nbsp;");
            writer.AddAttribute(HtmlTextWriterAttribute.Value, this.ButtonText);
            writer.AddAttribute(HtmlTextWriterAttribute.Onclick, string.Format("window.open('{0}" + str2 + "OpenerText={1}&OpenerKey={2}{3}{5}'{4},'ContentSelectControl','width=670,height=400,resizable=0,scrollbars=yes')", new object[] { str, this.ClientID, this.m_Value.ClientID, this.GetUrlArgs(), this.GetDefalutValue(), this.GetReturnJs() }));
            writer.AddStyleAttribute(HtmlTextWriterStyle.Cursor, "pointer");
            writer.AddAttribute(HtmlTextWriterAttribute.Type, "button");
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "inputbutton");
            if (!this.ButtonEnabled)
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Disabled, "true");
            }
            writer.RenderBeginTag(HtmlTextWriterTag.Input);
            writer.RenderEndTag();
        }

        [Description("按钮的启用状态"), DefaultValue(true), Category("自定义")]
        public bool ButtonEnabled
        {
            get
            {
                return ((this.ViewState["ButtonEabled"] == null) || ((bool) this.ViewState["ButtonEabled"]));
            }
            set
            {
                this.ViewState["ButtonEabled"] = value;
            }
        }

        [DefaultValue("浏览..."), Bindable(true), Description("将在选择按钮上显示的文本"), Category("自定义")]
        public virtual string ButtonText
        {
            get
            {
                string str = (string) this.ViewState["ButtonText"];
                if (str != null)
                {
                    return str;
                }
                return string.Empty;
            }
            set
            {
                this.ViewState["ButtonText"] = value;
            }
        }

        [Description("弹出窗口返回的附带数据,如物品的ID"), Category("自定义"), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual string DataKey
        {
            get
            {
                this.EnsureChildControls();
                return this.m_Value.Value;
            }
            set
            {
                this.EnsureChildControls();
                this.m_Value.Value = value;
            }
        }

        [Themeable(false), UrlProperty("*.aspx"), Description("弹出窗口的相对路径"), Category("自定义"), Editor("System.Web.UI.Design.UrlEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        public virtual string FileUrl
        {
            get
            {
                string str = (string) this.ViewState["PopupFileUrl"];
                if (str != null)
                {
                    return str;
                }
                return string.Empty;
            }
            set
            {
                this.ViewState["PopupFileUrl"] = value;
            }
        }

        [Description("是否返回Text中的值到弹出窗口(代替 DefaultValue={0})"), Category("自定义")]
        public bool IsReturnValue
        {
            get
            {
                if (this.ViewState["IsReturnValue"] == null)
                {
                    return false;
                }
                return (bool) this.ViewState["IsReturnValue"];
            }
            set
            {
                this.ViewState["IsReturnValue"] = value;
            }
        }

        [Description("当文本改变时要执行的脚本，在打开的页面中也应该对此方法作处理"), Category("自定义")]
        public virtual string TextChangeJS
        {
            get
            {
                string str = (string) this.ViewState["Js"];
                if (str != null)
                {
                    return str;
                }
                return string.Empty;
            }
            set
            {
                this.ViewState["Js"] = value;
            }
        }

        [Category("自定义"), Description("转给弹出页面的Url参数"), DefaultValue("")]
        public virtual string UrlArgs
        {
            get
            {
                string str = (string) this.ViewState["UrlArgs"];
                if (str != null)
                {
                    return str;
                }
                return string.Empty;
            }
            set
            {
                this.ViewState["UrlArgs"] = value;
            }
        }
    }
}

