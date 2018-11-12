namespace EasyOne.WebSite.Admin.User
{
    using EasyOne.Controls;
    using EasyOne.ModelControls;
    using EasyOne.UserManage;
    using EasyOne.Web.UI;
    using EasyOne.WebSite.Controls;
    using System;
    using System.Web.UI.WebControls;
    using EasyOne.Components;

    public partial  class PointLog : AdminPage
    {
        private string m_PointName = SiteConfig.UserConfig.PointName;

        protected void BtnDelete_Click(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            string str = "";
            switch (this.RadlDatepartType.SelectedValue)
            {
                case "0":
                    now = DateTime.Now.AddDays(-10.0);
                    str = "10天前";
                    break;

                case "1":
                    now = DateTime.Now.AddMonths(-1);
                    str = "1个月前";
                    break;

                case "2":
                    now = DateTime.Now.AddMonths(-2);
                    str = "2个月前";
                    break;

                case "3":
                    now = DateTime.Now.AddMonths(-3);
                    str = "3个月前";
                    break;

                case "4":
                    now = DateTime.Now.AddMonths(-6);
                    str = "6个月前";
                    break;

                case "5":
                    now = DateTime.Now.AddYears(-1);
                    str = "1年前";
                    break;
            }
            if (UserPointLog.Delete(now))
            {
                BasePage.ResponseRedirect("PointLog.aspx");
            }
            else
            {
                AdminPage.WriteErrMsg("<li>已经没有" + str + "的" + this.m_PointName + "记录！</li>");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                string str = BasePage.RequestString("SearchType");
                string str2 = BasePage.RequestString("Field");
                string str3 = BasePage.RequestString("KeyWord");
                if (!string.IsNullOrEmpty(str))
                {
                    switch (str)
                    {
                        case "-1":
                            this.SmpNavigator.AdditionalNode = "所有" + this.m_PointName + "明细记录";
                            return;

                        case "0":
                            this.SmpNavigator.AdditionalNode = "最近10天内的新" + this.m_PointName + "明细记录";
                            return;

                        case "1":
                            this.SmpNavigator.AdditionalNode = "最近一月内的新" + this.m_PointName + "明细记录";
                            return;

                        case "2":
                            this.SmpNavigator.AdditionalNode = "所有收入记录";
                            return;

                        case "3":
                            this.SmpNavigator.AdditionalNode = "所有支出记录";
                            return;

                        case "10":
                            if (string.IsNullOrEmpty(str2) || string.IsNullOrEmpty(str3))
                            {
                                this.SmpNavigator.AdditionalNode = "所有" + this.m_PointName + "明细记录";
                                return;
                            }
                            switch (str2)
                            {
                                case "1":
                                    this.SmpNavigator.AdditionalNode = "用户名中含有 <span style='color:#f00'>" + str3 + "</span> 的" + this.m_PointName + "明细记录";
                                    return;

                                case "2":
                                    this.SmpNavigator.AdditionalNode = "消费时间为 <span style='color:#f00'>" + str3 + "</span> 的" + this.m_PointName + "明细记录";
                                    return;
                            }
                            return;

                        case "11":
                            this.SmpNavigator.AdditionalNode = this.m_PointName + "明细复杂查询记录";
                            return;

                        default:
                            this.SmpNavigator.AdditionalNode = "所有" + this.m_PointName + "明细记录";
                            return;
                    }
                }
            }
        }
    }
}

