namespace EasyOne.ExtendedControls
{
    using EasyOne.Common;
    using System;
    using System.ComponentModel;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    [DefaultProperty("Text"), ToolboxData("<{0}:ExtendedLiteral ID=\"ELtr\" runat=server></{0}:ExtendedLiteral>")]
    public class ExtendedLiteral : Literal
    {
        protected override void Render(HtmlTextWriter writer)
        {
            string text = base.Text;
            if (this.HtmlEncode)
            {
                text = DataSecurity.HtmlEncode(text);
            }
            base.Text = this.BeginTag + text + this.EndTag;
            base.Render(writer);
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

