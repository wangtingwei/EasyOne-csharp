namespace EasyOne.Web.UI
{
    using System;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;

    public class HtmlTemplateTitle : HtmlTitle
    {
        private string m_Template;

        protected override void Render(HtmlTextWriter writer)
        {
            writer.RenderBeginTag(HtmlTextWriterTag.Title);
            if (this.HasControls())
            {
                this.RenderChildren(writer);
            }
            else if (this.Template != null)
            {
                writer.Write(this.Template.Replace("{PE.Control.PageTitle/}", this.Text));
            }
            writer.RenderEndTag();
        }

        public string Template
        {
            get
            {
                return this.m_Template;
            }
            set
            {
                this.m_Template = value;
            }
        }
    }
}

