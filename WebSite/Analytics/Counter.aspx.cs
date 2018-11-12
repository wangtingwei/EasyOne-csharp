namespace EasyOne.WebSite.AnalyticsUI
{
    using EasyOne.Analytics;
    using EasyOne.Common;
    using EasyOne.Enumerations;
    using EasyOne.Model.Analytics;
    using EasyOne.Web.UI;
    using System;
    using System.Text;
    using System.Web;
    using System.Web.UI.HtmlControls;

    public partial class Stat : AdminPage
    {
        private string m_ClientIP;
        private int m_KillRefreshTmp;
        private string m_RegFieldsFillTmp;
        private const string s_FYesterDay = "FYesterDay";
        private const string s_Interval = "Interval";
        private const string s_IntervalNum = "IntervalNum";
        private const string s_IsCountOnline = "IsCountOnline";
        private const string s_KillRefresh = "KillRefresh";
        private const string s_LastIPCache = "EasyOne_LastIP";
        private const string s_NYesterDayVisitorNum = "nYesterDayVisitorNum";
        private const string s_RegFieldsFill = "RegFields_Fill";

        private void InitApp()
        {
            if ((base.Application["RegFields_Fill"] != null) && (base.Application["KillRefresh"] != null))
            {
                this.m_RegFieldsFillTmp = base.Application["RegFields_Fill"].ToString();
                this.m_KillRefreshTmp = DataConverter.CLng(base.Application["KillRefresh"]);
            }
            else
            {
                StatInfoListInfo statInfoListInfo = OtherReport.GetStatInfoListInfo();
                this.m_RegFieldsFillTmp = string.IsNullOrEmpty(statInfoListInfo.RegFieldsFill) ? string.Empty : statInfoListInfo.RegFieldsFill;
                this.m_KillRefreshTmp = statInfoListInfo.KillRefresh;
                base.Application.Lock();
                base.Application["RegFields_Fill"] = this.m_RegFieldsFillTmp;
                base.Application["KillRefresh"] = this.m_KillRefreshTmp;
                base.Application["Interval"] = statInfoListInfo.Interval;
                base.Application["IntervalNum"] = statInfoListInfo.IntervalNum;
                base.Application.UnLock();
            }
            if (base.Application["EasyOne_LastIP"] == null)
            {
                base.Application["EasyOne_LastIP"] = "#0.0.0.0#";
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            base.Response.Expires = 0;
            this.m_ClientIP = Counter.GetIP(base.Request);
            this.InitApp();
            if (this.m_RegFieldsFillTmp.Contains("IsCountOnline"))
            {
                StatOnlineInfo onlineByIP = OtherReport.GetOnlineByIP(this.m_ClientIP);
                if (onlineByIP.IsNull)
                {
                    this.Update();
                }
                else if (onlineByIP.LastTime == onlineByIP.OnTime)
                {
                    this.Update();
                }
                else
                {
                    Counter.StatInfoListAddView();
                }
            }
            else if (base.Application["EasyOne_LastIP"].ToString().Contains(this.m_ClientIP))
            {
                Counter.StatInfoListAddView();
            }
            else
            {
                this.SaveIP(this.m_ClientIP);
                this.Update();
            }
            this.ShowInfo();
        }

        private void SaveIP(string inIP)
        {
            string str = base.Application["EasyOne_LastIP"].ToString().Trim(new char[] { '#' });
            string[] strArray = str.Split(new char[] { '#' });
            if (strArray.Length < this.m_KillRefreshTmp)
            {
                str = "#" + str + "#" + inIP + "#";
            }
            else
            {
                str = "#" + str.Replace(strArray[0], inIP) + "#";
            }
            base.Application.Lock();
            base.Application["EasyOne_LastIP"] = str;
            base.Application.UnLock();
        }

        private void ShowInfo()
        {
            string str = BasePage.RequestString("Style");
            if (!string.IsNullOrEmpty(str) || (string.Compare(str, "none", StringComparison.OrdinalIgnoreCase) != 0))
            {
                StatInfoListInfo statInfoListInfo = OtherReport.GetStatInfoListInfo();
                int num = statInfoListInfo.TotalNum + statInfoListInfo.OldTotalNum;
                int num2 = statInfoListInfo.TotalView + statInfoListInfo.OldTotalView;
                TimeSpan span = (TimeSpan) (DateTime.Today - DataConverter.CDate(statInfoListInfo.StartDate));
                int days = 0;
                if (span.Days <= 0)
                {
                    days = span.Days;
                }
                else
                {
                    days = num / span.Days;
                    if ((num % span.Days) != 0)
                    {
                        days++;
                    }
                }
                StringBuilder builder = new StringBuilder();
                string str2 = str.ToLower().ToString();
                if (str2 != null)
                {
                    if (!(str2 == "simple"))
                    {
                        if (str2 == "all")
                        {
                            builder.Append("总访问量：" + num.ToString() + "人次<br>");
                            builder.Append("总浏览量：" + num2.ToString() + "人次<br>");
                            if (this.m_RegFieldsFillTmp.Contains("IsCountOnline"))
                            {
                                builder.Append("当前在线：" + OtherReport.GetCurrentOnlineCount().ToString() + "人次<br>");
                            }
                            if (this.m_RegFieldsFillTmp.Contains("FYesterDay"))
                            {
                                int num4 = 0;
                                if (base.Application["nYesterDayVisitorNum"] != null)
                                {
                                    num4 = DataConverter.CLng(base.Application["nYesterDayVisitorNum"]);
                                }
                                else
                                {
                                    int[] list = TimeReport.GetList(StatName.Day, DateTime.Today.AddDays(-1.0).ToString("yyyy-MM-dd"));
                                    if (list != null)
                                    {
                                        foreach (int num5 in list)
                                        {
                                            num4 += num5;
                                        }
                                    }
                                    base.Application["nYesterDayVisitorNum"] = num4;
                                }
                                builder.Append("昨日访问：" + num4.ToString() + "人次<br>");
                            }
                            builder.Append("今日访问：" + statInfoListInfo.DayNum.ToString() + "人次<br>");
                            builder.Append("日均访问：" + days.ToString() + "人次<br>");
                        }
                        else if (str2 == "common")
                        {
                            builder.Append("总访问量：" + num.ToString() + "人次<br>");
                            builder.Append("总浏览量：" + num2.ToString() + "人次<br>");
                            if (this.m_RegFieldsFillTmp.Contains("IsCountOnline"))
                            {
                                builder.Append("当前在线：" + OtherReport.GetCurrentOnlineCount().ToString() + "人次<br>");
                            }
                        }
                    }
                    else
                    {
                        builder.Append("总访问量：" + num.ToString() + "人次<br>");
                        if (this.m_RegFieldsFillTmp.Contains("IsCountOnline"))
                        {
                            builder.Append("当前在线：" + OtherReport.GetCurrentOnlineCount().ToString() + "人次<br>");
                        }
                    }
                }
                base.Response.Write("document.write('" + builder.ToString() + "');");
                base.Response.End();
            }
        }

        private void Update()
        {
            int num;
            string str = base.Server.UrlDecode(BasePage.RequestString("Referer"));
            string str2 = string.IsNullOrEmpty(str) ? "" : str.Substring(0, str.IndexOf("/", 8, StringComparison.Ordinal));
            string input = BasePage.RequestString("Timezone");
            int num2 = (base.Request.Cookies["VisitNum"] == null) ? 1 : (DataConverter.CLng(base.Request.Cookies["VisitNum"].Value) + 1);
            str = string.IsNullOrEmpty(str) ? "直接输入或书签导入" : StringHelper.SubString(str, 100, "...");
            str2 = string.IsNullOrEmpty(str2) ? "直接输入或书签导入" : StringHelper.SubString(str2, 50, "...");
            int num3 = DataConverter.CLng(input);
            if (num3 == 0)
            {
                input = "其它";
                num = 0;
            }
            else
            {
                num = -1 * (num3 / 60);
                string str4 = (num3 < 0) ? "GMT+" : "GMT-";
                input = str4 + Convert.ToString(Math.Abs(num)) + "：" + Convert.ToString((int) (Math.Abs(num3) % 60));
            }
            StatUpdateInfo updateInfo = new StatUpdateInfo();
            updateInfo.Browser = string.IsNullOrEmpty(base.Request.Browser.Browser) ? "其它" : (base.Request.Browser.Browser.Replace("IE", "MSIE") + base.Request.Browser.Version);
            updateInfo.Color = Counter.GetColor(BasePage.RequestString("Color"));
            updateInfo.EncodeIP = StringHelper.EncodeIP(this.m_ClientIP);
            updateInfo.IP = this.m_ClientIP;
            updateInfo.Keyword = Counter.FindKeyword(str);
            updateInfo.Mozilla = base.Request.UserAgent;
            updateInfo.Referer = str;
            updateInfo.Screen = Counter.GetScreen(BasePage.RequestString("Width"), BasePage.RequestString("Height"));
            updateInfo.System = Counter.GetSystemType(base.Request);
            updateInfo.Timezone = input;
            updateInfo.VisitTimezone = num;
            updateInfo.VisitNum = num2;
            updateInfo.Weburl = str2;
            Counter.StatUpdate(updateInfo);
            HttpCookie cookie = new HttpCookie("VisitNum", num2.ToString());
            cookie.Expires = DateTime.MaxValue;
            base.Response.Cookies.Add(cookie);
        }
    }
}

