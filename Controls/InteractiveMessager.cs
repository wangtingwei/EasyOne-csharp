namespace EasyOne.Controls
{
    using System;
    using System.ComponentModel;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public class InteractiveMessager : Literal
    {
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            WebControl control = this.Parent.FindControl(this.ControlToMessage) as WebControl;
            if (control != null)
            {
                control.Attributes.Add("onfocus", "onControlFocus(" + control.ClientID + "," + this.ClientID + ");");
                control.Attributes.Add("onblur", "onControlBlur(" + control.ClientID + "," + this.ClientID + ");");
                control.Style.Add("float", "left");
                control.Style.Add("line-height", "100%");
            }
            this.Page.ClientScript.RegisterExpandoAttribute(this.ControlToMessage, "MessageId", this.ClientID);
            this.Page.ClientScript.RegisterExpandoAttribute(this.ClientID, "ValidatorOkMessage", this.ValidatorOkMessage);
            this.Page.ClientScript.RegisterExpandoAttribute(this.ClientID, "Message", base.Text);
            this.Page.ClientScript.RegisterExpandoAttribute(this.ClientID, "IsValidEmpty", this.IsValidEmpty.ToString());
            string script = "<script type=\"text/javascript\">\r\nvar currentClassName = \"\";\r\nfunction onControlFocus(obj,messager)\r\n{\r\n    currentClassName = obj.className;\r\n    obj.className = 'input_onFocus';\r\n    messager.className = 'd_on';\r\n    messager.innerHTML = messager.Message;\r\n}\r\n    \r\nfunction onControlBlur(obj,messager)\r\n{\r\n    obj.className = currentClassName;\r\n    messager.className ='';\r\n    for (i = 0; i < Page_Validators.length; i++) \r\n    {\r\n        val = Page_Validators[i];\r\n        if(val.controltovalidate == obj.id)\r\n        {\r\n            val.isvalid = true;\r\n            if ((typeof(val.enabled) == \"undefined\" || val.enabled != false)) \r\n            {\r\n                if (typeof(val.evaluationfunction) == \"function\")\r\n                {\r\n                    val.isvalid = val.evaluationfunction(val);\r\n                }\r\n             }\r\n                \r\n            if(messager.IsValidEmpty==\"True\")\r\n            {\r\n                if(val.isvalid == false)\r\n                {\r\n                    messager.innerHTML = val.errormessage;\r\n                    messager.className = 'd_err';\r\n                    break;\r\n                }\r\n                else\r\n                {\r\n                    messager.innerHTML = messager.ValidatorOkMessage;\r\n                    messager.className = 'd_ok';\r\n                }\r\n            }\r\n            else\r\n            {\r\n                if(obj.value!='')\r\n                {\r\n                    if(val.isvalid == false)\r\n                    {\r\n                        messager.innerHTML = val.errormessage;\r\n                        messager.className = 'd_err';\r\n                        break;\r\n                    }\r\n                    else\r\n                    {\r\n                        messager.innerHTML = messager.ValidatorOkMessage;\r\n                        messager.className = 'd_ok';\r\n                    }\r\n                }\r\n                else\r\n                {\r\n                     obj.className ='';\r\n                     messager.className = '';\r\n                }\r\n            }\r\n\r\n            ValidatorUpdateDisplay(val);\r\n        }\r\n    }\r\n}\r\n</script>";
            if (!this.Page.ClientScript.IsStartupScriptRegistered(this.Page.GetType(), "MessageJS"))
            {
                this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "MessageJS", script);
            }
        }

        protected override void Render(HtmlTextWriter writer)
        {
            writer.Write("<div id=\"");
            writer.Write(this.ClientID);
            writer.Write("\">");
            if (this.IsShowText)
            {
                writer.Write(base.Text);
            }
            writer.Write("</div>");
        }

        [DefaultValue("")]
        public string ControlToMessage
        {
            get
            {
                object obj2 = this.ViewState["ControlToMessage"];
                if (obj2 != null)
                {
                    return (string) obj2;
                }
                return "";
            }
            set
            {
                this.ViewState["ControlToMessage"] = value;
            }
        }

        [DefaultValue(false)]
        public bool IsShowText
        {
            get
            {
                object obj2 = this.ViewState["IsShowText"];
                return ((obj2 != null) && ((bool) obj2));
            }
            set
            {
                this.ViewState["IsShowText"] = value;
            }
        }

        [DefaultValue(true)]
        public bool IsValidEmpty
        {
            get
            {
                object obj2 = this.ViewState["IsValidEmpty"];
                if (obj2 != null)
                {
                    return (bool) obj2;
                }
                return true;
            }
            set
            {
                this.ViewState["IsValidEmpty"] = value;
            }
        }

        [DefaultValue("")]
        public string ValidatorOkMessage
        {
            get
            {
                object obj2 = this.ViewState["ValidatorOkMessage"];
                if (obj2 != null)
                {
                    return (string) obj2;
                }
                return "";
            }
            set
            {
                this.ViewState["ValidatorOkMessage"] = value;
            }
        }
    }
}

