namespace EasyOne.Controls
{
    using EasyOne.Common;
    using System;
    using System.ComponentModel;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    [ToolboxData("<{0}:DatePicker ID=\"Dpk\" runat=\"server\"></{0}:DatePicker>"), Themeable(true)]
    public class DatePicker : TextBox
    {
        private const string DatePicker_JS = "EasyOne.Controls.DatePicker.DatePicker.js";
        private Image m_Image = new Image();

        protected override void CreateChildControls()
        {
            base.CreateChildControls();
            this.m_Image.ApplyStyleSheetSkin(this.Page);
            this.Controls.Add(this.m_Image);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            Type type = base.GetType();
            string webResourceUrl = this.Page.ClientScript.GetWebResourceUrl(type, "EasyOne.Controls.DatePicker.DatePicker.js");
            if (!this.Page.ClientScript.IsClientScriptIncludeRegistered(type, "EasyOne.Controls.DatePicker.DatePicker.js"))
            {
                this.Page.ClientScript.RegisterClientScriptInclude(type, "EasyOne.Controls.DatePicker.DatePicker.js", webResourceUrl);
            }
        }

        protected override void Render(HtmlTextWriter writer)
        {
            string format = "yyyy-MM-dd HH:mm:ss";
            if (!string.IsNullOrEmpty(this.DateFormat))
            {
                format = this.DateFormat;
                this.m_Image.Attributes.Add("onclick", "return showCalendar('" + this.ClientID + "', '" + this.DateFormat + "');");
            }
            else if (this.IsLongDate)
            {
                format = "yyyy-MM-dd HH:mm:ss";
                this.m_Image.Attributes.Add("onclick", "return showCalendar('" + this.ClientID + "', 'yyyy-MM-dd HH:mm:ss');");
            }
            else
            {
                format = "yyyy-MM-dd";
                this.m_Image.Attributes.Add("onclick", "return showCalendar('" + this.ClientID + "', 'yyyy-MM-dd');");
            }
            this.m_Image.Style.Add("cursor", "pointer");
            if (!string.IsNullOrEmpty(this.Text))
            {
                this.Text = DataConverter.CDate(this.Text).ToString(format);
            }
            base.Render(writer);
            this.m_Image.RenderControl(writer);
        }

        [Description("日期输出")]
        public DateTime Date
        {
            get
            {
                DateTime now;
                if (!DateTime.TryParse(this.Text, out now))
                {
                    now = DateTime.Now;
                }
                return now;
            }
        }

        [DefaultValue("yyyy-MM-dd")]
        public string DateFormat
        {
            get
            {
                object obj2 = this.ViewState["DateFormat"];
                if (obj2 != null)
                {
                    return (string) obj2;
                }
                return "";
            }
            set
            {
                this.ViewState["DateFormat"] = value;
            }
        }

        [DefaultValue(""), Bindable(true), Category("自定义"), UrlProperty, Description("控件中显示图片的路径"), Localizable(true)]
        public string DateImage
        {
            get
            {
                this.EnsureChildControls();
                return this.m_Image.ImageUrl;
            }
            set
            {
                this.EnsureChildControls();
                this.m_Image.ImageUrl = value;
            }
        }

        public CssStyleCollection ImageStyle
        {
            get
            {
                return this.m_Image.Style;
            }
        }

        [DefaultValue("false")]
        public bool IsLongDate
        {
            get
            {
                object obj2 = this.ViewState["IsLongDate"];
                return ((obj2 != null) && ((bool) obj2));
            }
            set
            {
                this.ViewState["IsLongDate"] = value;
            }
        }
    }
}

