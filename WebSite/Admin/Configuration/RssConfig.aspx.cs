namespace EasyOne.WebSite.Admin.Configuration
{
    using EasyOne.Common;
    using EasyOne.Components;
    using EasyOne.Controls;
    using EasyOne.Web.UI;
    using System;
    using System.Configuration;
    using System.IO;
    using System.Web.UI.WebControls;

    public partial class RssConfig : AdminPage
    {
        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            if (base.IsValid)
            {
                SiteConfigInfo config = SiteConfig.ConfigReadFromFile();
                config.SiteOption.RssEnable = DataConverter.CBoolean(this.RssEnable.SelectedValue);
                config.SiteOption.WapEnable = DataConverter.CBoolean(this.WapEnable.SelectedValue);
                try
                {
                    new SiteConfig().Update(config);
                    SiteCache.Remove("EasyOneSiteConfig");
                    AdminPage.WriteSuccessMsg("RSS/WAP配置保存成功！", "RssConfig.aspx");
                }
                catch (FileNotFoundException)
                {
                    AdminPage.WriteErrMsg("<li>文件未找到</li>", "RssConfig.aspx");
                }
                catch (UnauthorizedAccessException)
                {
                    AdminPage.WriteErrMsg("<li>检查您的服务器是否给配置文件或文件夹写入权限。</li>", "RssConfig.aspx");
                }
                catch (ConfigurationErrorsException)
                {
                    AdminPage.WriteErrMsg("<li>检查您的服务器是否给配置文件或文件夹写入权限。</li>", "RssConfig.aspx");
                }
            }
        }

        private void ModifyRssOption()
        {
            SiteOption siteOption = SiteConfig.ConfigInfo().SiteOption;
            this.RssEnable.SelectedValue = this.SelectValue(siteOption.RssEnable);
            this.WapEnable.SelectedValue = this.SelectValue(siteOption.WapEnable);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                this.ModifyRssOption();
            }
        }

        private string SelectValue(bool selected)
        {
            if (selected)
            {
                return "true";
            }
            return "false";
        }
    }
}

