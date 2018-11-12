namespace EasyOne.WebSite.Controls.Shop
{
    using EasyOne.Components;
    using EasyOne.Model.Shop;
    using EasyOne.ModelControls;
    using EasyOne.Web.UI;
    using System;
    using System.IO;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public partial class PresentPic : BaseUserControl
    {
        private string m_ColspanControl;
        private bool m_IsAdminManage = true;
        private string m_TitleWidth;

        public void GetPresentPic(PresentInfo presentInfo)
        {
            presentInfo.PresentPic = this.FileUploadPresentPic.FilePath;
            string oldValue = base.BasePath + SiteConfig.SiteOption.UploadDir;
            string originalImagePath = presentInfo.PresentPic.Replace(oldValue, "");
            if (this.ChkThumb.Checked)
            {
                if (!string.IsNullOrEmpty(presentInfo.PresentPic))
                {
                    try
                    {
                        string extension = Path.GetExtension(presentInfo.PresentPic);
                        string str4 = presentInfo.PresentPic.Replace(extension, "_S" + extension);
                        presentInfo.PresentThumb = Thumbs.GetThumbsPath(originalImagePath, str4.Replace(oldValue, ""));
                    }
                    catch (ArgumentException)
                    {
                        BaseUserControl.WriteErrMsg("<li>生成缩略图的路径中具有非法字符！</li>");
                    }
                }
            }
            else
            {
                presentInfo.PresentThumb = this.FileUploadPresentThumb.FilePath;
            }
            if (this.ChkPresentPicWatermark.Checked && !string.IsNullOrEmpty(presentInfo.PresentPic))
            {
                WaterMark.AddWaterMark(originalImagePath);
            }
            if (((this.ChkPresentPicWatermark.Checked && this.ChkThumb.Checked) || this.ChkPresentThumbWatermark.Checked) && !string.IsNullOrEmpty(presentInfo.PresentThumb))
            {
                WaterMark.AddWaterMark(presentInfo.PresentThumb.Replace(oldValue, ""));
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                this.FileUploadPresentPic.IsAdminManage = this.m_IsAdminManage;
                this.FileUploadPresentThumb.IsAdminManage = this.m_IsAdminManage;
                this.ChkThumb.Attributes.Add("onclick", "SetThumb()");
                if (BaseUserControl.RequestStringToLower("Action") != "modify")
                {
                    this.ChkThumb.Checked = SiteConfig.ShopConfig.IsThumb;
                    this.ChkPresentPicWatermark.Checked = SiteConfig.ShopConfig.IsWatermark;
                    this.ChkPresentThumbWatermark.Checked = SiteConfig.ShopConfig.IsWatermark;
                    if (SiteConfig.ShopConfig.IsThumb)
                    {
                        this.tbThumb.Style.Add("display", "none");
                    }
                }
            }
        }

        public void SetPresentPic(PresentInfo present)
        {
            this.FileUploadPresentThumb.FilePath = present.PresentThumb;
            this.FileUploadPresentPic.FilePath = present.PresentPic;
        }

        public string ColspanControl
        {
            get
            {
                if (!string.IsNullOrEmpty(this.m_ColspanControl))
                {
                    return ("colspan=\"" + this.m_ColspanControl + "\"");
                }
                return string.Empty;
            }
            set
            {
                this.m_ColspanControl = value;
            }
        }

        public bool IsAdminManage
        {
            get
            {
                return this.m_IsAdminManage;
            }
            set
            {
                this.m_IsAdminManage = value;
            }
        }

        public string TitleWidth
        {
            get
            {
                if (!string.IsNullOrEmpty(this.m_TitleWidth))
                {
                    return ("style=\"width:" + this.m_TitleWidth + "\"");
                }
                return string.Empty;
            }
            set
            {
                this.m_TitleWidth = value;
            }
        }
    }
}

