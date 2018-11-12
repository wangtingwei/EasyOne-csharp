namespace EasyOne.WebSite.Admin.Accessories
{
    using EasyOne.AccessManage;
    using EasyOne.Enumerations;
    using EasyOne.ExtendedControls;
    using EasyOne.Web.UI;
    using System;
    using EasyOne.ModelControls;
    public partial class SiteConfigGuide : AdminPage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            this.LitSiteConfig.Visible = RolePermissions.AccessCheck(OperateCode.SiteConfig);
            this.LitFrontPageTemplateConfig.Visible = RolePermissions.AccessCheck(OperateCode.DynamicPageConfig);
            this.LtrShop.Visible = BasePage.IseShop;
        }
    }
}

