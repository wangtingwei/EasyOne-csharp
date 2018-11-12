namespace EasyOne.ExtendedControls
{
    using EasyOne.Common;
    using EasyOne.Components;
    using System;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    [ToolboxData("<{0}:ExtendedImage ID=\"ECImage\" runat=\"server\"></{0}:ExtendedCompressionImage>")]
    public class ExtendedImage : WebControl
    {
        private int m_height;
        private string m_src;
        private int m_width;

        protected override void Render(HtmlTextWriter writer)
        {
            this.Controls.Clear();
            HtmlImage child = new HtmlImage();
            child.Src = Utility.ConvertAbsolutePath(SiteConfig.SiteInfo.VirtualPath + VirtualPathUtility.AppendTrailingSlash(SiteConfig.SiteOption.UploadDir), this.Src);
            if (this.ImageWidth > 0)
            {
                child.Width = this.ImageWidth;
            }
            else if (this.ImageHeight > 0)
            {
                child.Height = this.ImageHeight;
            }
            else
            {
                child.Height = 0x2d;
            }
            this.Controls.Add(child);
            if (this.HasControls())
            {
                foreach (Control control in this.Controls)
                {
                    control.RenderControl(writer);
                }
            }
        }

        public int ImageHeight
        {
            get
            {
                return this.m_height;
            }
            set
            {
                this.m_height = value;
            }
        }

        public int ImageWidth
        {
            get
            {
                return this.m_width;
            }
            set
            {
                this.m_width = value;
            }
        }

        public string Src
        {
            get
            {
                return this.m_src;
            }
            set
            {
                this.m_src = value;
            }
        }
    }
}

