namespace EasyOne.WebSite.Admin.Accessories
{
    using EasyOne.Common;
    using EasyOne.Components;
    using EasyOne.Controls;
    using EasyOne.Web.UI;
    using System;
    using System.Security;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public partial class ThumbConfig : AdminPage
    {

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            if (base.IsValid)
            {
                SiteConfigInfo config = SiteConfig.ConfigReadFromFile();
                config.ThumbsConfig.ThumbsWidth = DataConverter.CLng(this.TxtThumbsWidth.Text);
                config.ThumbsConfig.ThumbsHeight = DataConverter.CLng(this.TxtThumbsHeight.Text);
                config.ThumbsConfig.ThumbsMode = DataConverter.CLng(this.RadThumbsMode.SelectedValue);
                config.ThumbsConfig.AddBackColor = this.TxtBgColor.Text.Trim();
                if (this.RadText.Checked)
                {
                    config.WaterMarkConfig.WaterMarkType = 0;
                }
                else
                {
                    config.WaterMarkConfig.WaterMarkType = 1;
                }
                config.WaterMarkConfig.WaterMarkTextInfo.Text = this.TxtWaterMarkText.Text.Trim();
                config.WaterMarkConfig.WaterMarkTextInfo.FoneType = this.DropWaterMarkTextFoneType.SelectedValue;
                config.WaterMarkConfig.WaterMarkTextInfo.FoneSize = DataConverter.CLng(this.TxtWaterMarkTextFoneSize.Text);
                config.WaterMarkConfig.WaterMarkTextInfo.FoneColor = this.TxtWaterMarkTextFoneColor.Text.Trim();
                config.WaterMarkConfig.WaterMarkTextInfo.FoneStyle = this.DropWaterMarkTextFoneStyle.SelectedValue.Trim();
                config.WaterMarkConfig.WaterMarkTextInfo.FoneBorderColor = this.TxtWaterMarkTextFoneBorderColor.Text.Trim();
                config.WaterMarkConfig.WaterMarkTextInfo.FoneBorder = DataConverter.CLng(this.TxtWaterMarkTextFoneBorder.Text);
                config.WaterMarkConfig.WaterMarkTextInfo.WaterMarkPosition = this.DropWaterMarkTextPosition.SelectedValue;
                config.WaterMarkConfig.WaterMarkTextInfo.WaterMarkPositionX = DataConverter.CLng(this.TxtWaterMarketTextPositionX.Text);
                config.WaterMarkConfig.WaterMarkTextInfo.WaterMarkPositionY = DataConverter.CLng(this.TxtWaterMarketTextPositionY.Text);
                config.WaterMarkConfig.WaterMarkImageInfo.ImagePath = this.TxtWaterMarkImagePath.Text.Trim();
                config.WaterMarkConfig.WaterMarkImageInfo.WaterMarkPosition = this.DropWaterMarkImagePosition.SelectedValue;
                config.WaterMarkConfig.WaterMarkImageInfo.WaterMarkPositionX = DataConverter.CLng(this.TxtWaterMarkImagePositionX.Text);
                config.WaterMarkConfig.WaterMarkImageInfo.WaterMarkPositionY = DataConverter.CLng(this.TxtWaterMarkImagePositionY.Text);
                try
                {
                    new SiteConfig().Update(config);
                    SiteCache.Remove("EasyOneSiteConfig");
                    AdminPage.WriteSuccessMsg("缩略图配置保存成功！", "ThumbConfig.aspx");
                }
                catch (SecurityException exception)
                {
                    AdminPage.WriteErrMsg(exception.Message);
                }
                catch (UnauthorizedAccessException exception2)
                {
                    AdminPage.WriteErrMsg(exception2.Message);
                }
            }
        }

        private void Modify()
        {
            SiteConfigInfo info = SiteConfig.ConfigInfo();
            this.TxtThumbsWidth.Text = info.ThumbsConfig.ThumbsWidth.ToString();
            this.TxtThumbsHeight.Text = info.ThumbsConfig.ThumbsHeight.ToString();
            this.RadThumbsMode.Items[info.ThumbsConfig.ThumbsMode].Selected = true;
            this.TxtBgColor.Text = info.ThumbsConfig.AddBackColor.ToString();
            if (info.WaterMarkConfig.WaterMarkType == 0)
            {
                this.RadText.Checked = true;
                this.TextWaterMark.Style.Add("display", "");
                this.PicWaterMark.Style.Add("display", "none");
            }
            else
            {
                this.RadImage.Checked = true;
                this.TextWaterMark.Style.Add("display", "none");
                this.PicWaterMark.Style.Add("display", "");
            }
            this.TxtWaterMarkText.Text = info.WaterMarkConfig.WaterMarkTextInfo.Text;
            this.DropWaterMarkTextFoneType.SelectedValue = info.WaterMarkConfig.WaterMarkTextInfo.FoneType;
            this.TxtWaterMarkTextFoneSize.Text = info.WaterMarkConfig.WaterMarkTextInfo.FoneSize.ToString();
            this.TxtWaterMarkTextFoneColor.Text = info.WaterMarkConfig.WaterMarkTextInfo.FoneColor;
            this.DropWaterMarkTextFoneStyle.SelectedValue = info.WaterMarkConfig.WaterMarkTextInfo.FoneStyle;
            this.TxtWaterMarkTextFoneBorderColor.Text = info.WaterMarkConfig.WaterMarkTextInfo.FoneBorderColor;
            this.TxtWaterMarkTextFoneBorder.Text = info.WaterMarkConfig.WaterMarkTextInfo.FoneBorder.ToString();
            this.DropWaterMarkTextPosition.SelectedValue = info.WaterMarkConfig.WaterMarkTextInfo.WaterMarkPosition;
            this.TxtWaterMarketTextPositionX.Text = info.WaterMarkConfig.WaterMarkTextInfo.WaterMarkPositionX.ToString();
            this.TxtWaterMarketTextPositionY.Text = info.WaterMarkConfig.WaterMarkTextInfo.WaterMarkPositionY.ToString();
            this.TxtWaterMarkImagePath.Text = info.WaterMarkConfig.WaterMarkImageInfo.ImagePath;
            this.DropWaterMarkImagePosition.SelectedValue = info.WaterMarkConfig.WaterMarkImageInfo.WaterMarkPosition;
            this.TxtWaterMarkImagePositionX.Text = info.WaterMarkConfig.WaterMarkImageInfo.WaterMarkPositionX.ToString();
            this.TxtWaterMarkImagePositionY.Text = info.WaterMarkConfig.WaterMarkImageInfo.WaterMarkPositionY.ToString();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                this.Modify();
            }
        }
    }
}

