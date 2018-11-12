﻿namespace EasyOne.ExtendedControls
{
    using EasyOne.Common;
    using System;
    using System.Globalization;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    [ToolboxData("<{0}:ExtendedHyperLink ID=\"ELnk\" runat=server></{0}:ExtendedHyperLink>")]
    public class ExtendedHyperLink : HyperLink
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
                if (this.ViewState["HtmlEncode"] != null)
                {
                    return Convert.ToBoolean(this.ViewState["HtmlEncode"], CultureInfo.CurrentCulture);
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

