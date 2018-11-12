namespace EasyOne.ExtendedControls
{
    using EasyOne.Common;
    using System;
    using System.ComponentModel;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    [DefaultProperty("Text"), ToolboxData("<{0}:ExtendedLabel ID=\"ELbl\" runat=server></{0}:ExtendedLabel>")]
    public class ExtendedLabel : Label
    {
        protected override void RenderContents(HtmlTextWriter writer)
        {
            string text = this.Text;
            if (this.HtmlEncode)
            {
                text = DataSecurity.HtmlEncode(text);
            }
            this.Text = this.BeginTag + text + this.EndTag;
            base.RenderContents(writer);
        }

        public string BeginTag
        {
            get
            {
                object obj2 = this.ViewState["BeginTag"];
                if (obj2 != null)
                {
                    return (string) obj2;
                }
                return string.Empty;
            }
            set
            {
                this.ViewState["BeginTag"] = value;
            }
        }

        public string EndTag
        {
            get
            {
                object obj2 = this.ViewState["EndTag"];
                if (obj2 != null)
                {
                    return (string) obj2;
                }
                return string.Empty;
            }
            set
            {
                this.ViewState["EndTag"] = value;
            }
        }

        public bool HtmlEncode
        {
            get
            {
                object obj2 = this.ViewState["HtmlEncode"];
                if (obj2 != null)
                {
                    return (bool) obj2;
                }
                return true;
            }
            set
            {
                this.ViewState["HtmlEncode"] = value;
            }
        }
    }
}

