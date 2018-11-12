namespace EasyOne.WebSite.Admin.AD
{
    using EasyOne.AccessManage;
    using EasyOne.AD;
    using EasyOne.Common;
    using EasyOne.Components;
    using EasyOne.Controls;
    using EasyOne.Enumerations;
    using EasyOne.Model.AD;
    using EasyOne.Web.UI;
    using System;
    using System.Web;
    using System.Web.UI.HtmlControls;

    public partial class PreviewAD : AdminPage
    {

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            RolePermissions.BusinessAccessCheck(OperateCode.AdManage);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            int zoneId = BasePage.RequestInt32("ZoneId");
            if (string.Compare(BasePage.RequestString("Type"), "Zone", StringComparison.OrdinalIgnoreCase) == 0)
            {
                if (Advertisement.GetADList(zoneId).Count == 0)
                {
                    this.ShowJS.InnerHtml = "版位中暂时还未添加广告，请添加后再进行预览！";
                }
                else
                {
                    ADZoneInfo adZoneById = ADZone.GetAdZoneById(zoneId);
                    if (adZoneById != null)
                    {
                        this.ShowJS.InnerHtml = "<script  type=\"text/javascript\" src='" + base.ResolveUrl("~/" + VirtualPathUtility.AppendTrailingSlash(SiteConfig.SiteOption.AdvertisementDir) + adZoneById.ZoneJSName) + "?temp=" + DataSecurity.RandomNum() + "'></script>";
                    }
                }
            }
            else
            {
                this.ShowAd();
            }
        }

        private void ShowAd()
        {
            AdvertisementInfo advertisementById = Advertisement.GetAdvertisementById(BasePage.RequestInt32("AdId"));
            this.ShowJS.InnerHtml = Advertisement.GetAdContent(advertisementById);
        }
    }
}

