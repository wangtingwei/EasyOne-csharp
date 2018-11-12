namespace EasyOne.WebSite.Admin.AD
{
    using EasyOne.AD;
    using EasyOne.Model.AD;
    using EasyOne.Web.UI;
    using System;
    using System.Web.UI;

    public partial class ADCount : BasePage
    {
        private string srcript;

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected override void Render(HtmlTextWriter writer)
        {
            string strB = BasePage.RequestString("Action");
            AdvertisementInfo advertisementById = Advertisement.GetAdvertisementById(BasePage.RequestInt32("AdId"));
            if (!advertisementById.IsNull)
            {
                if (advertisementById.CountView)
                {
                    advertisementById.Views++;
                }
                if (string.Compare("Click", strB, StringComparison.OrdinalIgnoreCase) == 0)
                {
                    if (advertisementById.CountClick)
                    {
                        advertisementById.Clicks++;
                    }
                    if (!string.IsNullOrEmpty(advertisementById.LinkUrl))
                    {
                        this.srcript = "<script language='javascript' type='text/javascript'>window.location='" + advertisementById.LinkUrl + "';</script>";
                    }
                }
                Advertisement.Update(advertisementById);
                writer.Write(this.srcript);
            }
        }
    }
}

