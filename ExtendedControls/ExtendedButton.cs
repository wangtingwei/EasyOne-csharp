namespace EasyOne.ExtendedControls
{
    using EasyOne.AccessManage;
    using EasyOne.Enumerations;
    using System;
    using System.ComponentModel;
    using System.Globalization;
    using System.Text;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    [ToolboxData("<{0}:ExtendedButton ID=\"EBtn\" runat=\"server\"></{0}:ExtendedButton>")]
    public class ExtendedButton : Button
    {
        private bool m_IsChecked;
        private bool m_IsVisible;
        private EasyOne.Enumerations.OperateCode m_Operatecode;

        protected override void AddAttributesToRender(HtmlTextWriter writer)
        {
            if (this.IsShowTabs)
            {
                if (base.ControlStyleCreated && !base.ControlStyle.IsEmpty)
                {
                    base.ControlStyle.AddAttributesToRender(writer, this);
                }
                if (this.Page != null)
                {
                    this.Page.VerifyRenderingInServerForm(this);
                }
                if (this.UseSubmitBehavior)
                {
                    writer.AddAttribute(HtmlTextWriterAttribute.Type, "submit");
                }
                else
                {
                    writer.AddAttribute(HtmlTextWriterAttribute.Type, "button");
                }
                PostBackOptions postBackOptions = this.GetPostBackOptions();
                string uniqueID = this.UniqueID;
                if ((uniqueID != null) && ((postBackOptions == null) || (postBackOptions.TargetControl == this)))
                {
                    writer.AddAttribute(HtmlTextWriterAttribute.Name, uniqueID);
                }
                writer.AddAttribute(HtmlTextWriterAttribute.Value, base.Text);
                if (this.Page != null)
                {
                    this.Page.ClientScript.RegisterForEventValidation(postBackOptions);
                }
                string str2 = this.UniqueID.Replace("$", "").Replace("_", "");
                bool isEnabled = base.IsEnabled;
                string firstScript = string.Empty;
                if (isEnabled)
                {
                    firstScript = EnsureEndWithSemiColon(this.OnClientClick);
                    if (base.HasAttributes)
                    {
                        string str4 = base.Attributes["onclick"];
                        if (str4 != null)
                        {
                            firstScript = firstScript + EnsureEndWithSemiColon(str4);
                            base.Attributes.Remove("onclick");
                        }
                    }
                    if (this.Page != null)
                    {
                        this.InitJavaScript();
                        string secondScript = str2 + "PostFunction()";
                        if (secondScript != null)
                        {
                            firstScript = MergeScript(firstScript, secondScript);
                        }
                    }
                }
                if (firstScript.Length > 0)
                {
                    writer.AddAttribute(HtmlTextWriterAttribute.Onclick, firstScript);
                }
                if (this.Enabled && !isEnabled)
                {
                    writer.AddAttribute(HtmlTextWriterAttribute.Disabled, "disabled");
                }
            }
            else
            {
                base.AddAttributesToRender(writer);
            }
        }

        internal static string EnsureEndWithSemiColon(string value)
        {
            if (value != null)
            {
                int length = value.Length;
                if ((length > 0) && (value[length - 1] != ';'))
                {
                    return (value + ";");
                }
            }
            return value;
        }

        protected void InitJavaScript()
        {
            PostBackOptions postBackOptions = this.GetPostBackOptions();
            string postBackEventReference = this.Page.ClientScript.GetPostBackEventReference(postBackOptions, false);
            string key = this.UniqueID.Replace("$", "").Replace("_", "");
            StringBuilder builder = new StringBuilder();
            builder.Append("<script type=\"text/javascript\">\r\n");
            builder.Append("function " + key + "PostFunction()\r\n");
            builder.Append("{\r\n");
            builder.Append("  if(!" + postBackEventReference + ")\r\n");
            builder.Append("  { \r\n");
            if (string.IsNullOrEmpty(this.CustomValProcessFunction))
            {
                builder.Append("      for (i = 0; i < Page_Validators.length; i++) \r\n");
                builder.Append("      {\r\n");
                builder.Append("          val = Page_Validators[i];\r\n");
                builder.Append("          if (val.isvalid == false) \r\n");
                builder.Append("          {\r\n");
                builder.Append("              var id = val.id;\r\n");
                builder.Append("              var controltovalidate = document.getElementById(val.controltovalidate);\r\n");
                builder.Append("              var tempobj = controltovalidate;\r\n");
                builder.Append("              var tabIndex;\r\n");
                builder.Append("              while (tempobj)\r\n");
                builder.Append("              {\r\n");
                builder.Append("                  if(tempobj.id.indexOf(\"" + this.TabPrefix + "\")>=0)\r\n");
                builder.Append("                  {\r\n");
                builder.Append("                      tabIndex = tempobj.id.substring(" + this.TabPrefix.Length.ToString(CultureInfo.CurrentCulture) + ", tempobj.id.length);\r\n");
                builder.Append("                      break;\r\n");
                builder.Append("                  }\r\n");
                builder.Append("                  else\r\n");
                builder.Append("                  {\r\n");
                builder.Append("                      tempobj = tempobj.parentNode;\r\n");
                builder.Append("                  }\r\n");
                builder.Append("              }\r\n");
                builder.Append("              ShowTabs(tabIndex);\r\n");
                builder.Append("              if (typeof(controltovalidate.focus) != \"undefined\" && controltovalidate.focus != null)\r\n");
                builder.Append("              {\r\n");
                builder.Append("                  controltovalidate.focus();\r\n");
                builder.Append("              }\r\n");
                builder.Append("              break;\r\n");
                builder.Append("            }\r\n");
                builder.Append("       }\r\n");
            }
            else
            {
                builder.Append("     " + this.CustomValProcessFunction + "();\r\n");
            }
            builder.Append("  }\r\n");
            builder.Append("}\r\n");
            builder.Append("</script>\r\n");
            if (!this.Page.ClientScript.IsStartupScriptRegistered(base.GetType(), key))
            {
                this.Page.ClientScript.RegisterStartupScript(base.GetType(), key, builder.ToString());
            }
        }

        internal static string MergeScript(string firstScript, string secondScript)
        {
            if (!string.IsNullOrEmpty(firstScript))
            {
                return (firstScript + secondScript);
            }
            if (secondScript.TrimStart(new char[0]).StartsWith("javascript:", StringComparison.Ordinal))
            {
                return secondScript;
            }
            return ("javascript:" + secondScript);
        }

        protected override void OnInit(EventArgs e)
        {
            bool flag = RolePermissions.AccessCheck(this.m_Operatecode);
            if (this.IsChecked && !flag)
            {
                this.Enabled = false;
            }
            if (this.IsVisible && !flag)
            {
                this.Visible = true;
            }
            base.OnInit(e);
        }

        [DefaultValue(""), Category("自定义"), Description("执行验证后执行自定义JS函数名称"), Localizable(true), Bindable(true)]
        public string CustomValProcessFunction
        {
            get
            {
                string str = (string) this.ViewState["CustomValProcessFunction"];
                if (str != null)
                {
                    return str;
                }
                return "";
            }
            set
            {
                this.ViewState["CustomValProcessFunction"] = value;
            }
        }

        [Localizable(true), Bindable(true), DefaultValue(false), Description("是否启用检查"), Category("自定义")]
        public bool IsChecked
        {
            get
            {
                return this.m_IsChecked;
            }
            set
            {
                this.m_IsChecked = value;
            }
        }

        [DefaultValue(false), Category("自定义"), Bindable(true), Localizable(true), Description("验证时是否标签页跳转")]
        public bool IsShowTabs
        {
            get
            {
                object obj2 = this.ViewState["IsShowTabs"];
                return ((obj2 != null) && ((bool) obj2));
            }
            set
            {
                this.ViewState["IsShowTabs"] = value;
            }
        }

        [DefaultValue(true), Description("没有权限时是否可见"), Localizable(true), Bindable(true), Category("自定义")]
        public bool IsVisible
        {
            get
            {
                return this.m_IsVisible;
            }
            set
            {
                this.m_IsVisible = value;
            }
        }

        [Description("操作资源码"), Category("自定义"), Bindable(true), Localizable(true), DefaultValue("")]
        public EasyOne.Enumerations.OperateCode OperateCode
        {
            get
            {
                return this.m_Operatecode;
            }
            set
            {
                this.m_Operatecode = value;
            }
        }

        [Localizable(true), Category("自定义"), Description("标签页前缀"), Bindable(true), DefaultValue("Tabs")]
        public string TabPrefix
        {
            get
            {
                string str = (string) this.ViewState["TabPrefix"];
                if (str != null)
                {
                    return str;
                }
                return "Tabs";
            }
            set
            {
                this.ViewState["TabPrefix"] = value;
            }
        }
    }
}

