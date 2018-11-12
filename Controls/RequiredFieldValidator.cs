namespace EasyOne.Controls
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    [DefaultProperty("Text"), ToolboxData("<{0}:RequiredFieldValidator runat=server></{0}:RequiredFieldValidator>")]
    public class RequiredFieldValidator : System.Web.UI.WebControls.RequiredFieldValidator
    {
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

        [Localizable(true), DefaultValue("* "), Description("必填的提示文字"), Category("自定义")]
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

        [Localizable(true), DefaultValue(typeof(Color), "Red"), Category("自定义"), Description("必填的提示文字的颜色"), TypeConverter(typeof(WebColorConverter))]
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

        [Localizable(true), Bindable(true), Category("自定义"), DefaultValue(true), Description("是否显示必填的提示文字")]
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

