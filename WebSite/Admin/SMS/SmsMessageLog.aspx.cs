namespace EasyOne.WebSite.Admin.Sms
{
    using EasyOne.Common;
    using EasyOne.Components;
    using EasyOne.Web.UI;
    using System;
    using System.Web.UI;

    public partial class SmsMessageLog : AdminPage
    {
        protected override void Render(HtmlTextWriter writer)
        {
            string strA = BasePage.RequestString("Action");
            string str2 = StringHelper.MD5(SiteConfig.SmsConfig.UserName + SiteConfig.SmsConfig.MD5Key);
            if (string.Compare(strA, "receive", StringComparison.OrdinalIgnoreCase) == 0)
            {
                writer.Write("<Meta http-equiv='Refresh' Content='0; Url=http://sms.EasyOne.net/MessageGate/MessageReceive.aspx?UserName=" + base.Server.UrlEncode(SiteConfig.SmsConfig.UserName) + "&MD5String=" + str2 + "'>");
                base.Render(writer);
            }
            if (string.Compare(strA, "send", StringComparison.OrdinalIgnoreCase) == 0)
            {
                writer.Write("<Meta http-equiv='Refresh' Content='0; Url=http://sms.EasyOne.net/MessageGate/MessageLog.aspx?UserName=" + base.Server.UrlEncode(SiteConfig.SmsConfig.UserName) + "&MD5String=" + str2 + "'>");
                base.Render(writer);
            }
        }
    }
}

