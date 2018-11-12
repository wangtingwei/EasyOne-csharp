namespace EasyOne.Controls
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Text;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public class FckEditorValidator : BaseValidator
    {
        protected override void AddAttributesToRender(HtmlTextWriter writer)
        {
            base.AddAttributesToRender(writer);
            if (base.RenderUplevel)
            {
                string clientID = this.ClientID;
                this.Page.ClientScript.RegisterExpandoAttribute(clientID, "evaluationfunction", "FckEditorRequiredFieldValidatorEvaluateIsValid", false);
                this.Page.ClientScript.RegisterExpandoAttribute(clientID, "initialvalue", this.InitialValue);
            }
        }

        protected override bool EvaluateIsValid()
        {
            string controlValidationValue = base.GetControlValidationValue(base.ControlToValidate);
            if (controlValidationValue != null)
            {
                return !controlValidationValue.Trim().Equals(this.InitialValue.Trim());
            }
            return true;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            StringBuilder builder = new StringBuilder();
            builder.Append("<script type=\"text/javascript\">");
            builder.Append(" function FckEditorValidatorGetValue(controltovalidate)");
            builder.Append(" {  ");
            builder.Append("   var editorInstance = FCKeditorAPI.GetInstance(controltovalidate);");
            builder.Append("   return editorInstance.GetXHTML(true);");
            builder.Append(" }");
            builder.Append(" function FckEditorRequiredFieldValidatorEvaluateIsValid(val)");
            builder.Append(" {  ");
            builder.Append("   return (ValidatorTrim(FckEditorValidatorGetValue(val.controltovalidate)) != ValidatorTrim(val.initialvalue));");
            builder.Append(" }");
            builder.Append("</script>");
            if (!this.Page.ClientScript.IsClientScriptBlockRegistered("FckEditorCustomValidate"))
            {
                this.Page.ClientScript.RegisterClientScriptBlock(base.GetType(), "FckEditorCustomValidate", builder.ToString());
            }
        }

        protected override void Render(HtmlTextWriter writer)
        {
            if ((base.Display != ValidatorDisplay.None) && this.ShowRequiredText)
            {
                writer.AddStyleAttribute(HtmlTextWriterStyle.Color, TypeDescriptor.GetConverter(this.RequiredTextColor).ConvertToString(this.RequiredTextColor));
                writer.RenderBeginTag(HtmlTextWriterTag.Span);
                writer.Write(this.RequiredText);
                writer.RenderEndTag();
            }
            base.Render(writer);
        }

        [DefaultValue(""), Themeable(false), Description("RequiredFieldValidator_InitialValue"), Category("Behavior")]
        public string InitialValue
        {
            get
            {
                object obj2 = this.ViewState["InitialValue"];
                if (obj2 != null)
                {
                    return (string) obj2;
                }
                return string.Empty;
            }
            set
            {
                this.ViewState["InitialValue"] = value;
            }
        }

        [DefaultValue("* "), Localizable(true), Category("自定义"), Description("必填的提示文字")]
        public string RequiredText
        {
            get
            {
                string str = (string) this.ViewState["RequiredText"];
                if (str != null)
                {
                    return str;
                }
                return "* ";
            }
            set
            {
                this.ViewState["RequiredText"] = value;
            }
        }

        [DefaultValue(typeof(Color), "Red"), Description("必填的提示文字的颜色"), Category("自定义"), Localizable(true), TypeConverter(typeof(WebColorConverter))]
        public Color RequiredTextColor
        {
            get
            {
                object obj2 = this.ViewState["RequiredTextColor"];
                if (obj2 != null)
                {
                    return (Color) obj2;
                }
                return Color.Red;
            }
            set
            {
                this.ViewState["RequiredTextColor"] = value;
            }
        }

        [Category("自定义"), Bindable(true), Localizable(true), DefaultValue(true), Description("是否显示必填的提示文字")]
        public bool ShowRequiredText
        {
            get
            {
                object obj2 = this.ViewState["ShowRequiredText"];
                if (obj2 != null)
                {
                    return (bool) obj2;
                }
                return true;
            }
            set
            {
                this.ViewState["ShowRequiredText"] = value;
            }
        }
    }
}

