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

    public partial class ProductPic : BaseUserControl
    {
        private string m_ColspanControl;
        private bool m_IsAdminManage = true;
        private string m_TitleWidth;

        public void GetProductPic(ProductInfo productInfo)
        {
            productInfo.ProductPic = this.FileUploadProductPic.FilePath;
            string oldValue = base.BasePath + SiteConfig.SiteOption.UploadDir;
            string originalImagePath = productInfo.ProductPic.Replace(oldValue, "");
            if (this.ChkThumb.Checked)
            {
                if (!string.IsNullOrEmpty(productInfo.ProductPic))
                {
                    try
                    {
                        string extension = Path.GetExtension(productInfo.ProductPic);
                        string str4 = productInfo.ProductPic.Replace(extension, "_S" + extension);
                        productInfo.ProductThumb = Thumbs.GetThumbsPath(originalImagePath, str4.Replace(oldValue, ""));
                    }
                    catch (ArgumentException)
                    {
                        BaseUserControl.WriteErrMsg("<li>生成缩略图的路径中具有非法字符！</li>");
                    }
                }
            }
            else
            {
                productInfo.ProductThumb = this.FileUploadProductThumb.FilePath;
            }
            if (this.ChkProductPicWatermark.Checked && !string.IsNullOrEmpty(productInfo.ProductPic))
            {
                WaterMark.AddWaterMark(originalImagePath);
            }
            if (((this.ChkProductPicWatermark.Checked && this.ChkThumb.Checked) || this.ChkProductThumbWatermark.Checked) && !string.IsNullOrEmpty(productInfo.ProductThumb))
            {
                WaterMark.AddWaterMark(productInfo.ProductThumb.Replace(oldValue, ""));
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                this.FileUploadProductPic.IsAdminManage = this.m_IsAdminManage;
                this.FileUploadProductThumb.IsAdminManage = this.m_IsAdminManage;
                this.ChkThumb.Attributes.Add("onclick", "SetThumb()");
                if (BaseUserControl.RequestStringToLower("Action") != "modify")
                {
                    this.ChkThumb.Checked = SiteConfig.ShopConfig.IsThumb;
                    this.ChkProductPicWatermark.Checked = SiteConfig.ShopConfig.IsWatermark;
                    this.ChkProductThumbWatermark.Checked = SiteConfig.ShopConfig.IsWatermark;
                    if (SiteConfig.ShopConfig.IsThumb)
                    {
                        this.tbThumb.Style.Add("display", "none");
                    }
                }
            }
        }

        public void SetProductPic(ProductInfo productInfo)
        {
            this.FileUploadProductThumb.FilePath = productInfo.ProductThumb;
            this.FileUploadProductPic.FilePath = productInfo.ProductPic;
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

