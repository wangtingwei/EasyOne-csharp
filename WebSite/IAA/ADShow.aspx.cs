namespace EasyOne.WebSite.AD
{
    using EasyOne.AD;
    using EasyOne.Model.AD;
    using EasyOne.Web.UI;
    using System;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public partial class ADShow : BasePage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            AdvertisementInfo advertisementById = Advertisement.GetAdvertisementById(BasePage.RequestInt32("AdId"));
            if (!advertisementById.IsNull)
            {
                this.LabAdShow.Text = Advertisement.GetAdContent(advertisementById);
                if (advertisementById.CountClick)
                {
                    advertisementById.Clicks++;
                }
                if (advertisementById.CountView)
                {
                    advertisementById.Views++;
                }
                Advertisement.Update(advertisementById);
            }
        }
    }
}

