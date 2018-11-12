namespace EasyOne.WebSite.Admin.Sms
{
    using EasyOne.Components;
    using EasyOne.ExtendedControls;
    using EasyOne.Web.UI;
    using System;

    public partial class SmsGuid : AdminPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (SiteConfig.SiteInfo.ProductEdition.CompareTo("eShop") == 0)
            {
                this.Literal1.Text = "<li><a href=\"SmsMessageToConsignee.aspx\" target=\"main_right\">发送给订单中的收货人</a></li>";
            }
        }
    }
}

