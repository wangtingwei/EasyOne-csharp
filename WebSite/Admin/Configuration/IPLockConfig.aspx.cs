namespace EasyOne.WebSite.Admin.Accessories
{
    using EasyOne.Common;
    using EasyOne.Components;
    using EasyOne.Controls;
    using EasyOne.Web.UI;
    using EasyOne.WebSite.Controls;
    using System;
    using System.Security;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public partial class IPLockConfig : AdminPage
    {

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            if (base.IsValid)
            {
                SiteConfigInfo config = SiteConfig.ConfigReadFromFile();
                config.IPLockConfig.LockIPType = this.RdnLockIPType.SelectedValue;
                config.IPLockConfig.LockIPWhite = this.IPLockWhite.Value;
                config.IPLockConfig.LockIPBlack = this.IPLockBlack.Value;
                config.IPLockConfig.AdminLockIPType = this.RdnAdminLockIPType.SelectedValue;
                config.IPLockConfig.AdminLockIPWhite = this.IPLockAdminWhite.Value;
                config.IPLockConfig.AdminLockIPBlack = this.IPLockAdminBlack.Value;
                try
                {
                    new SiteConfig().Update(config);
                    SiteCache.Remove("EasyOneSiteConfig");
                    AdminPage.WriteSuccessMsg("IP访问限定配置！", "IPLockConfig.aspx");
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
            if (!base.IsPostBack)
            {
                SiteConfigInfo info = SiteConfig.ConfigInfo();
                this.RdnLockIPType.Items[DataConverter.CLng(info.IPLockConfig.LockIPType)].Selected = true;
                this.IPLockBlack.Value = info.IPLockConfig.LockIPBlack;
                this.IPLockWhite.Value = info.IPLockConfig.LockIPWhite;
                this.RdnAdminLockIPType.Items[DataConverter.CLng(info.IPLockConfig.AdminLockIPType)].Selected = true;
                this.IPLockAdminBlack.Value = info.IPLockConfig.AdminLockIPBlack;
                this.IPLockAdminWhite.Value = info.IPLockConfig.AdminLockIPWhite;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.Modify();
        }
    }
}

