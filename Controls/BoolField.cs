namespace EasyOne.Controls
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    [ToolboxData("<{0}:BoolField runat=server></{0}:BoolField>"), DefaultProperty("Text")]
    public class BoolField : EasyOne.Controls.BoundField
    {
        protected override string FormatDataValue(object dataValue, bool encode)
        {
            if (!(dataValue is bool))
            {
                return base.FormatDataValue(dataValue, encode);
            }
            if ((bool) dataValue)
            {
                if (this.TrueTextColor == Color.Empty)
                {
                    return this.TrueText;
                }
                return ("<span style=\"color:#" + this.TrueTextColor.ToArgb().ToString("x").Substring(2) + "\">" + this.TrueText + "</span>");
            }
            if (this.FalseTextColor == Color.Empty)
            {
                return this.FalseText;
            }
            return ("<span style=\"color:#" + this.FalseTextColor.ToArgb().ToString("x").Substring(2) + "\">" + this.FalseText + "</span>");
        }

        [DefaultValue("\x00d7"), Description("绑定值为 false 时显示的文本"), Localizable(true), Category("自定义"), Bindable(true)]
        public string FalseText
        {
            get
            {
                string str = (string) base.ViewState["FalseText"];
                if (str != null)
                {
                    return str;
                }
                return "\x00d7";
            }
            set
            {
                base.ViewState["FalseText"] = value;
            }
        }

        [Localizable(true), Category("自定义"), DefaultValue(typeof(Color), ""), TypeConverter(typeof(WebColorConverter)), Description("绑定值为 false 时显示文本的颜色"), Bindable(true)]
        public virtual Color FalseTextColor
        {
            get
            {
                object obj2 = base.ViewState["FalseTextColor"];
                if (obj2 != null)
                {
                    return (Color) obj2;
                }
                return Color.Empty;
            }
            set
            {
                base.ViewState["FalseTextColor"] = value;
            }
        }

        [Description(" 绑定值为 true 时显示的文本"), Category("自定义"), Localizable(true), DefaultValue("√"), Bindable(true)]
        public string TrueText
        {
            get
            {
                string str = (string) base.ViewState["TrueText"];
                if (str != null)
                {
                    return str;
                }
                return "√";
            }
            set
            {
                base.ViewState["TrueText"] = value;
            }
        }

        [Bindable(true), TypeConverter(typeof(WebColorConverter)), Description("绑定值为 true 时显示文本的颜色"), Localizable(true), Category("自定义"), DefaultValue(typeof(Color), "")]
        public virtual Color TrueTextColor
        {
            get
            {
                object obj2 = base.ViewState["TrueTextColor"];
                if (obj2 != null)
                {
                    return (Color) obj2;
                }
                return Color.Empty;
            }
            set
            {
                base.ViewState["TrueTextColor"] = value;
            }
        }
    }
}

