namespace EasyOne.ModelControls
{
    using System;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    [ControlBuilder(typeof(PlaceHolderControlBuilder))]
    public class LinkImage : Control
    {
        private Image m_Image = new Image();

        public LinkImage()
        {
            this.m_Image.ImageAlign = ImageAlign.AbsMiddle;
        }

        protected override void Render(HtmlTextWriter writer)
        {
            string str = "";
            str = this.Page.Request.ApplicationPath.Equals("/") ? string.Empty : this.Page.Request.ApplicationPath;
            str = this.Page.Request.Url.Scheme + "://" + this.Page.Request.Url.Authority + str + "/Images/ModelIcon/";
            this.m_Image.ImageUrl = str + this.Icon;
            writer.Write("<div class=\"linkType\">");
            if (this.IsShowLink)
            {
                writer.Write("<div class=\"link\"></div>");
            }
            this.m_Image.RenderControl(writer);
            base.Render(writer);
            writer.Write("</div>");
        }

        public string Icon
        {
            get
            {
                object obj2 = this.ViewState["Icon"];
                if (obj2 != null)
                {
                    return (string) obj2;
                }
                return string.Empty;
            }
            set
            {
                this.ViewState["Icon"] = value;
            }
        }

        public bool IsShowLink
        {
            get
            {
                object obj2 = this.ViewState["IsShowLink"];
                return ((obj2 != null) && ((bool) obj2));
            }
            set
            {
                this.ViewState["IsShowLink"] = value;
            }
        }
    }
}

