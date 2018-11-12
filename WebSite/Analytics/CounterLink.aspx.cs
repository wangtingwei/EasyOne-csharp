namespace EasyOne.WebSite.AnalyticsUI
{
    using EasyOne.Analytics;
    using EasyOne.Common;
    using EasyOne.Model.Analytics;
    using EasyOne.Web.UI;
    using System;
    using System.Text;
    using System.Web.UI.HtmlControls;

    public partial class CounterLink : BasePage
    {
        private const string s_Interval = "Interval";
        private const string s_IntervalNum = "IntervalNum";
        private const string s_IsCountOnline = "IsCountOnline";
        private const string s_RegFieldsFill = "RegFields_Fill";

        protected void Page_Load(object sender, EventArgs e)
        {
            int interval;
            int intervalNum;
            string str = string.Empty;
            if (((base.Application["RegFields_Fill"] == null) || (base.Application["Interval"] == null)) || (base.Application["IntervalNum"] == null))
            {
                StatInfoListInfo statInfoListInfo = OtherReport.GetStatInfoListInfo();
                base.Application.Lock();
                base.Application["Interval"] = statInfoListInfo.Interval;
                base.Application["IntervalNum"] = statInfoListInfo.IntervalNum;
                base.Application.UnLock();
                str = string.IsNullOrEmpty(statInfoListInfo.RegFieldsFill) ? string.Empty : statInfoListInfo.RegFieldsFill;
                base.Application["RegFields_Fill"] = str;
                interval = statInfoListInfo.Interval;
                intervalNum = statInfoListInfo.IntervalNum;
            }
            else
            {
                str = base.Application["RegFields_Fill"].ToString();
                interval = DataConverter.CLng(base.Application["Interval"]);
                intervalNum = DataConverter.CLng(base.Application["IntervalNum"]);
            }
            string str2 = BasePage.RequestString("Style");
            StringBuilder builder = new StringBuilder();
            builder.Append("var i = 0;");
            builder.Append("function EasyOneRef(){");
            builder.Append(" if(i <= " + intervalNum.ToString() + "){");
            builder.Append("var EasyOneImg=new Image();");
            builder.Append("EasyOneImg.src='" + base.FullBasePath + "Analytics/StatOnline.aspx';");
            builder.Append("setTimeout('EasyOneRef()'," + Convert.ToString((int) (interval * 0x3e8)) + ");}");
            builder.Append("i+=1;}");
            if (str.Contains("IsCountOnline"))
            {
                builder.Append("EasyOneRef();");
            }
            builder.Append("var referrer = escape(document.referrer);");
            builder.Append("var timezone = (new Date()).getTimezoneOffset();");
            builder.Append("var width = screen.width;");
            builder.Append("var height = screen.height;");
            builder.Append("var color = screen.colorDepth;");
            builder.Append("document.write('<'+'script type=\"text/javascript\"  src=" + base.FullBasePath + "Analytics/Counter.aspx?style=" + str2 + "&Referer='+referrer+'&Timezone='+timezone+'&Width='+width+'&Height='+height+'&Color='+color+'><'+'/script>');");
            base.Response.Write(builder.ToString());
            base.Response.End();
        }
    }
}

