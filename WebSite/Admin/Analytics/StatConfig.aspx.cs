namespace EasyOne.WebSite.Admin.Analytics
{
    using EasyOne.Analytics;
    using EasyOne.Common;
    using EasyOne.Controls;
    using EasyOne.Model.Analytics;
    using EasyOne.Web.UI;
    using System;
    using System.Text;
    using System.Web.UI.WebControls;

    public partial class StatConfig : AdminPage
    {

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            if (base.IsValid)
            {
                StatInfoListInfo info = new StatInfoListInfo();
                info.MasterTimeZone = DataConverter.CLng(this.DropTimezone.SelectedValue);
                info.OnlineTime = DataConverter.CLng(this.TxtOnlineTime.Text);
                info.Interval = DataConverter.CLng(this.TxtInterval.Text);
                info.IntervalNum = DataConverter.CLng(this.TxtIntervalNum.Text);
                info.VisitRecord = DataConverter.CLng(this.TxtVisitRecord.Text);
                info.KillRefresh = DataConverter.CLng(this.TxtKillRefresh.Text);
                info.OldTotalNum = DataConverter.CLng(this.TxtOldTotalNum.Text);
                info.OldTotalView = DataConverter.CLng(this.TxtOldTotalView.Text);
                info.StartDate = this.DpkStartDate.Date.ToString("yyyy-MM-dd");
                StringBuilder builder = new StringBuilder("");
                foreach (ListItem item in this.ChklRegFields.Items)
                {
                    if (item.Selected)
                    {
                        builder.Append(item.Value + ",");
                    }
                }
                info.RegFieldsFill = builder.ToString().TrimEnd(new char[] { ',' });
                if (Counter.SaveConfig(info))
                {
                    base.Application.Lock();
                    if (base.Application["Interval"] != null)
                    {
                        base.Application["Interval"] = info.Interval;
                    }
                    if (base.Application["IntervalNum"] != null)
                    {
                        base.Application["IntervalNum"] = info.IntervalNum;
                    }
                    if (base.Application["RegFields_Fill"] != null)
                    {
                        base.Application["RegFields_Fill"] = info.RegFieldsFill;
                    }
                    if (base.Application["KillRefresh"] != null)
                    {
                        base.Application["KillRefresh"] = info.KillRefresh;
                    }
                    base.Application.UnLock();
                    AdminPage.WriteSuccessMsg("网站统计配置保存成功！", base.Request.UrlReferrer.ToString());
                }
            }
            else
            {
                AdminPage.WriteErrMsg("输入的不是有效的数值，请认真检查！", "StatConfig.aspx");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                StatInfoListInfo statInfoListInfo = OtherReport.GetStatInfoListInfo();
                if (statInfoListInfo != null)
                {
                    this.DropTimezone.SelectedValue = statInfoListInfo.MasterTimeZone.ToString();
                    this.TxtOnlineTime.Text = statInfoListInfo.OnlineTime.ToString();
                    this.TxtInterval.Text = statInfoListInfo.Interval.ToString();
                    this.TxtIntervalNum.Text = statInfoListInfo.IntervalNum.ToString();
                    this.TxtVisitRecord.Text = statInfoListInfo.VisitRecord.ToString();
                    this.TxtKillRefresh.Text = statInfoListInfo.KillRefresh.ToString();
                    this.TxtOldTotalNum.Text = statInfoListInfo.OldTotalNum.ToString();
                    this.TxtOldTotalView.Text = statInfoListInfo.OldTotalView.ToString();
                    this.DpkStartDate.Text = DataConverter.CDate(statInfoListInfo.StartDate).ToString("yyyy-MM-dd");
                    if (!string.IsNullOrEmpty(statInfoListInfo.RegFieldsFill))
                    {
                        foreach (string str in statInfoListInfo.RegFieldsFill.Split(new char[] { ',' }))
                        {
                            this.ChklRegFields.Items.FindByValue(str.Trim()).Selected = true;
                        }
                    }
                }
            }
        }
    }
}

