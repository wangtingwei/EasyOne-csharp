namespace EasyOne.WebSite.Admin.Accessories
{
    using EasyOne.Components;
    using EasyOne.Controls;
    using EasyOne.Web.UI;
    using System;
    using System.Security;
    using System.Web.UI.WebControls;

    public partial class SiteInfo : AdminPage
    {

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            if (base.IsValid)
            {
                SiteConfigInfo config = SiteConfig.ConfigReadFromFile();
                config.SiteInfo.SiteName = this.TxtSiteName.Text.Trim();
                config.SiteInfo.SiteTitle = this.TxtSiteTitle.Text.Trim();
                config.SiteInfo.SiteUrl = this.TxtSiteUrl.Text.Trim();
                config.SiteInfo.LogoUrl = this.TxtLogoUrl.Text.Trim();
                config.SiteInfo.BannerUrl = this.TxtBannerUrl.Text.Trim();
                config.SiteInfo.Webmaster = this.TxtWebmaster.Text.Trim();
                config.SiteInfo.WebmasterEmail = this.TxtWebmasterEmail.Text.Trim();
                config.SiteInfo.Copyright = this.TxtCopyright.Text.Trim();
                config.SiteInfo.MetaDescription = this.TxtMeta_Description.Text.Trim();
                config.SiteInfo.MetaKeywords = this.TxtMeta_Keywords.Text.Trim();
                try
                {
                    new SiteConfig().Update(config);
                    SiteCache.Remove("EasyOneSiteConfig");
                    AdminPage.WriteSuccessMsg("网站信息配置保存成功！", "SiteInfo.aspx");
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

        private void ModifySiteInfo()
        {
            EasyOne.Components.SiteInfo siteInfo = SiteConfig.ConfigInfo().SiteInfo;
            this.TxtSiteName.Text = siteInfo.SiteName;
            this.TxtSiteTitle.Text = siteInfo.SiteTitle;
            this.TxtSiteUrl.Text = siteInfo.SiteUrl;
            this.TxtLogoUrl.Text = siteInfo.LogoUrl;
            this.TxtBannerUrl.Text = siteInfo.BannerUrl;
            this.TxtWebmaster.Text = siteInfo.Webmaster;
            this.TxtWebmasterEmail.Text = siteInfo.WebmasterEmail;
            this.TxtCopyright.Text = siteInfo.Copyright;
            this.TxtMeta_Description.Text = siteInfo.MetaDescription;
            this.TxtMeta_Keywords.Text = siteInfo.MetaKeywords;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                this.ModifySiteInfo();
            }
        }
    }
}

