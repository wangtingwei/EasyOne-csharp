namespace EasyOne.WebSite.Admin.Analytics
{
    using EasyOne.Analytics;
    using EasyOne.Controls;
    using EasyOne.Enumerations;
    using EasyOne.Web.UI;
    using System;
    using System.Web.UI.WebControls;

    public partial class StatTimeResport : AdminPage
    {
        protected const int BarWidth = 90;
        protected float m_ItemSum;
        private string m_Search;
        private StatName m_StatName;
        private static readonly string[] s_StatDay_ItemNames = new string[] { 
            "00:00-01:00", "01:00-02:00", "02:00-03:00", "03:00-04:00", "04:00-05:00", "05:00-06:00", "06:00-07:00", "07:00-08:00", "08:00-09:00", "09:00-10:00", "10:00-11:00", "11:00-12:00", "12:00-13:00", "13:00-14:00", "14:00-15:00", "15:00-16:00", 
            "16:00-17:00", "17:00-18:00", "18:00-19:00", "20:00-21:00", "21:00-22:00", "22:00-23:00", "23:00-24:00"
         };
        private static readonly string[] s_StatWeek_ItemNames = new string[] { "星期日", "星期一", "星期二", "星期三", "星期四", "星期五", "星期六" };

        private TableCell AddTd(string cellName, double percent)
        {
            TableCell cell = new TableCell();
            cell.Text = cellName;
            if (percent > 0.0)
            {
                cell.Width = Unit.Percentage(percent);
            }
            return cell;
        }

        private int[] GetItemValues()
        {
            if (BasePage.RequestString("Type") == "All")
            {
                return TimeReport.GetAllList(this.m_StatName);
            }
            return TimeReport.GetList(this.m_StatName, this.m_Search);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            string str = BasePage.RequestString("Action");
            if (Enum.IsDefined(typeof(StatName), str))
            {
                this.m_StatName = (StatName) Enum.Parse(typeof(StatName), str);
            }
            else
            {
                this.m_StatName = StatName.None;
            }
            this.m_Search = BasePage.RequestString("Search");
            this.ShowInfo();
        }

        private void SetSmpNavigator(string nodeName, string value)
        {
            if (BasePage.RequestString("Type") == "All")
            {
                this.SmpNavigator.AdditionalNode = "全部" + nodeName + "访问统计分析";
            }
            else if (!string.IsNullOrEmpty(BasePage.RequestString("Search")))
            {
                this.SmpNavigator.AdditionalNode = "查询结果：" + value + nodeName + "访问统计分析";
            }
            else
            {
                this.SmpNavigator.AdditionalNode = value + nodeName + "访问统计分析";
            }
        }

        private void ShowInfo()
        {
            string[] strArray;
            int[] itemValues = this.GetItemValues();
            string nodeName = string.Empty;
            string search = string.Empty;
            string tableHeadName = string.Empty;
            int totalNum = 0;
            switch (this.m_StatName)
            {
                case StatName.Year:
                {
                    nodeName = "年";
                    tableHeadName = "月份";
                    search = string.IsNullOrEmpty(this.m_Search) ? DateTime.Today.Year.ToString() : this.m_Search;
                    strArray = new string[itemValues.Length];
                    string str4 = string.IsNullOrEmpty(BasePage.RequestString("Type")) ? (search + "年") : string.Empty;
                    for (int j = 0; j < itemValues.Length; j++)
                    {
                        strArray[j] = str4 + ((j + 1)).ToString("0月");
                        totalNum += itemValues[j];
                    }
                    this.StatTable(tableHeadName, strArray, itemValues, totalNum);
                    goto Label_02B2;
                }
                case StatName.Month:
                    nodeName = "月";
                    tableHeadName = "日期";
                    if (!string.IsNullOrEmpty(this.m_Search))
                    {
                        search = this.m_Search;
                        break;
                    }
                    search = DateTime.Today.ToString("yyyy-MM");
                    break;

                case StatName.Week:
                    nodeName = "周";
                    tableHeadName = "星期";
                    search = string.IsNullOrEmpty(BasePage.RequestString("Type")) ? "本" : string.Empty;
                    foreach (int num4 in itemValues)
                    {
                        totalNum += num4;
                    }
                    this.StatTable(tableHeadName, s_StatWeek_ItemNames, itemValues, totalNum);
                    goto Label_02B2;

                case StatName.Day:
                    nodeName = "日";
                    tableHeadName = "小时";
                    if (!string.IsNullOrEmpty(this.m_Search))
                    {
                        search = this.m_Search;
                    }
                    else
                    {
                        search = DateTime.Today.ToString("yyyy-MM-dd");
                    }
                    foreach (int num5 in itemValues)
                    {
                        totalNum += num5;
                    }
                    this.StatTable(tableHeadName, s_StatDay_ItemNames, itemValues, totalNum);
                    goto Label_02B2;

                default:
                    this.ShowNothing();
                    goto Label_02B2;
            }
            strArray = new string[itemValues.Length];
            string str5 = string.Empty;
            if (string.IsNullOrEmpty(this.m_Search))
            {
                str5 = string.IsNullOrEmpty(BasePage.RequestString("Type")) ? DateTime.Today.ToString("yyyy年MM月") : string.Empty;
            }
            else
            {
                str5 = this.m_Search.Replace("-", "年") + "月";
            }
            for (int i = 0; i < itemValues.Length; i++)
            {
                strArray[i] = str5 + ((i + 1)).ToString("0日");
                totalNum += itemValues[i];
            }
            this.StatTable(tableHeadName, strArray, itemValues, totalNum);
        Label_02B2:
            this.SetSmpNavigator(nodeName, search);
        }

        private void ShowNothing()
        {
            Literal child = new Literal();
            child.Text = "<table border='0' cellpadding='0' cellspacing='0' class='border tdbg' style='height:105px; width:100%'><tr><td align='center' valign='middle'>没有任何数据！</td></tr></table>";
            this.LblCount.Text = "0";
            this.PlhStat.Controls.Add(child);
        }

        private void StatTable(string tableHeadName, string[] tableItemKeys, int[] tableItemValues, int totalNum)
        {
            Table child = new Table();
            child.Width = Unit.Percentage(100.0);
            child.CellSpacing = 1;
            child.CellPadding = 2;
            child.CssClass = "border";
            TableRow row = new TableRow();
            row.CssClass = "title";
            TableCell[] cells = new TableCell[] { this.AddTd(tableHeadName, 30.0), this.AddTd("访问人数", 20.0), this.AddTd("百分比", 20.0), this.AddTd("图示", 30.0) };
            row.Cells.AddRange(cells);
            child.Rows.Add(row);
            for (int i = 0; i < tableItemKeys.Length; i++)
            {
                float num2 = 0f;
                if (totalNum != 0)
                {
                    num2 = Convert.ToSingle(tableItemValues[i]) / Convert.ToSingle(totalNum);
                }
                TableRow row2 = new TableRow();
                row2.CssClass = "tdbg";
                TableCell[] cellArray2 = new TableCell[] { this.AddTd(tableItemKeys[i], -1.0), this.AddTd(tableItemValues[i].ToString(), -1.0), this.AddTd(num2.ToString("p"), -1.0), this.AddTd(string.Format("<div class='StatBar' style='width:{0}%'/>", num2 * 90f), -1.0) };
                row2.Cells.AddRange(cellArray2);
                child.Rows.Add(row2);
            }
            this.LblCount.Text = totalNum.ToString();
            this.PlhStat.Controls.Add(child);
        }
    }
}

