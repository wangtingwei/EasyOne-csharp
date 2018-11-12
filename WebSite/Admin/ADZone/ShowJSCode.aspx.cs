namespace EasyOne.WebSite.Admin.AD
{
    using EasyOne.AccessManage;
    using EasyOne.AD;
    using EasyOne.Controls;
    using EasyOne.Enumerations;
    using EasyOne.Model.AD;
    using EasyOne.Web.UI;
    using System;
    using System.Web.UI.WebControls;

    public partial class ShowJSCode : AdminPage
    {

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            RolePermissions.BusinessAccessCheck(OperateCode.AdManage);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            ADZoneInfo adZoneById = ADZone.GetAdZoneById(BasePage.RequestInt32("ZoneId"));
            if ((adZoneById == null) || string.IsNullOrEmpty(adZoneById.ZoneJSName))
            {
                this.TxtZoneCode.Text = "找不到指定的版位！";
            }
            else
            {
                this.TxtZoneCode.Text = "<script  type=\"text/javascript\" src='{PE.SiteConfig.adpath/}/" + adZoneById.ZoneJSName + "'></script>";
            }
        }
    }
}

