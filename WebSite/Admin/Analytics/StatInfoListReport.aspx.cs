namespace EasyOne.WebSite.Admin.Analytics
{
    using EasyOne.Analytics;
    using EasyOne.Controls;
    using EasyOne.Enumerations;
    using EasyOne.Model.Analytics;
    using EasyOne.Web.UI;
    using System;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public partial class StatInfoListReport : AdminPage
    {

        private void init()
        {
            StatInfoListInfo statInfoListInfo = OtherReport.GetStatInfoListInfo();
            this.LblStartDate.Text = statInfoListInfo.StartDate;
            int num = 0;
            if (!string.IsNullOrEmpty(statInfoListInfo.StartDate))
            {
                TimeSpan span = (TimeSpan) (DateTime.Today - Convert.ToDateTime(statInfoListInfo.StartDate));
                num = span.Days + 1;
            }
            int num2 = 0;
            if (num <= 0)
            {
                num2 = num;
            }
            else
            {
                num2 = statInfoListInfo.TotalNum / num;
                if ((statInfoListInfo.TotalNum % num) != 0)
                {
                    num2++;
                }
            }
            this.TblcStatDayNum.InnerText = num.ToString();
            this.TdlcMonthMaxNum.InnerText = statInfoListInfo.MonthMaxNum.ToString();
            this.TdlcTotalNum.InnerText = statInfoListInfo.TotalNum.ToString();
            this.TdlcMonthMaxDate.InnerText = statInfoListInfo.MonthMaxDate;
            this.TdlcTotalView.InnerText = statInfoListInfo.TotalView.ToString();
            this.TdlcDayMaxDate.InnerText = statInfoListInfo.DayMaxDate;
            this.TdlcDayMaxNum.InnerText = statInfoListInfo.DayMaxNum.ToString();
            this.TdlcAveDayNum.InnerText = num2.ToString();
            this.TdlcHourMaxNum.InnerText = statInfoListInfo.HourMaxNum.ToString();
            this.TdlcDayNum.InnerText = statInfoListInfo.DayNum.ToString();
            this.TdlcHourMaxTime.InnerText = statInfoListInfo.HourMaxTime;
            this.TdlcPreDayNum.InnerText = Convert.ToString((int) ((statInfoListInfo.DayNum * 0x5a0) / ((DateTime.Now.Hour * 60) + DateTime.Now.Minute)));
            this.TdlcChinaNum.InnerText = statInfoListInfo.ChinaNum.ToString();
            this.TdlcOtherNum.InnerText = statInfoListInfo.OtherNum.ToString();
            this.TdlcSystem.InnerText = UserDataReport.MaxValue(StatName.UserSystem);
            this.TdlcBrowser.InnerText = UserDataReport.MaxValue(StatName.UserBrowser);
            this.TdlcMaxAreNum.InnerText = UserDataReport.MaxValue(StatName.UserAddress);
            this.TdlcMaxWebNum.InnerText = UserDataReport.MaxValue(StatName.UserWeburl);
            this.TdlcMaxScrNum.InnerText = UserDataReport.MaxValue(StatName.UserScreen);
            this.TdlcMaxColorNum.InnerText = UserDataReport.MaxValue(StatName.UserColor);
            this.TdlcCountNum.InnerText = OtherReport.GetVisitorCount().ToString();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                this.init();
            }
        }
    }
}

