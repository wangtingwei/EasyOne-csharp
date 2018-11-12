namespace EasyOne.WebSite.Admin.Analytics
{
    using EasyOne.Analytics;
    using EasyOne.Controls;
    using EasyOne.Web.UI;
    using System;
    using System.Web.UI.WebControls;

    public partial class StatOtherReport : AdminPage
    {
        protected const int BarWidth = 90;
        private static readonly string[] fCounter_ItemNames = new string[] { "首次", "二次", "三次", "四次", "五次", "六次", "七次", "八次", "九次", "十次以上" };
        protected float itemSum;

        private void FCounter()
        {
            int[] visitList = OtherReport.GetVisitList();
            if (visitList != null)
            {
                int totalNum = 0;
                foreach (int num2 in visitList)
                {
                    totalNum += num2;
                }
                this.SmpNavigator.AdditionalNode = "访问次数统计分析";
                this.StatTable("次数分析", fCounter_ItemNames, visitList, totalNum);
            }
            else
            {
                this.ShowNothing();
            }
        }

        private void FOnline()
        {
            if (!base.IsPostBack)
            {
                this.OdsStat.SelectMethod = "GetStatOnlineList";
                this.OdsStat.SelectCountMethod = "GetTotalStatOnline";
                this.ExtendedGridView1.Columns.Clear();
                System.Web.UI.WebControls.BoundField field = new System.Web.UI.WebControls.BoundField();
                System.Web.UI.WebControls.BoundField field2 = new System.Web.UI.WebControls.BoundField();
                System.Web.UI.WebControls.BoundField field3 = new System.Web.UI.WebControls.BoundField();
                System.Web.UI.WebControls.BoundField field4 = new System.Web.UI.WebControls.BoundField();
                System.Web.UI.WebControls.BoundField field5 = new System.Web.UI.WebControls.BoundField();
                field.DataField = "id";
                field2.DataField = "UserIP";
                field4.DataField = "OnTime";
                field5.DataField = "LastTime";
                field3.DataField = "UserPage";
                field.HeaderText = "编号";
                field2.HeaderText = "访问者IP";
                field4.HeaderText = "上站时间";
                field5.HeaderText = "最后刷新时间";
                field3.HeaderText = "用户当前浏览页";
                this.ExtendedGridView1.EmptyDataText = "当前无人在线！";
                this.ExtendedGridView1.Columns.Add(field);
                this.ExtendedGridView1.Columns.Add(field2);
                this.ExtendedGridView1.Columns.Add(field4);
                this.ExtendedGridView1.Columns.Add(field5);
                this.ExtendedGridView1.Columns.Add(field3);
                this.LblCount.Text = OtherReport.GetTotalStatOnline().ToString();
                this.ExtendedGridView1.DataSourceID = "OdsStat";
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            string str = BasePage.RequestString("Action");
            if (str != null)
            {
                if (!(str == "FVisit"))
                {
                    if (!(str == "IsCountOnline"))
                    {
                        return;
                    }
                }
                else
                {
                    this.FCounter();
                    return;
                }
                this.FOnline();
            }
        }

        private void ShowNothing()
        {
            Literal child = new Literal();
            child.Text = "<table border='0' cellpadding='0' cellspacing='0' class='border tdbg' style='height:105px; width:100%'><tr><td align='center' valign='middle'>没有任何数据！</td></tr></table>";
            this.LblCount.Text = "0";
            this.PlhStat.Controls.Add(child);
        }

        private void StatTable(string statItem, string[] itemNames, int[] items, int totalNum)
        {
            Table child = new Table();
            child.Width = Unit.Percentage(100.0);
            child.CellSpacing = 1;
            child.CellPadding = 2;
            child.CssClass = "border";
            TableRow row = new TableRow();
            row.CssClass = "title";
            TableCell[] cells = new TableCell[] { new TableCell(), new TableCell(), new TableCell(), new TableCell() };
            cells[0].Text = statItem;
            cells[1].Text = "访问人数";
            cells[2].Text = "百分比";
            cells[3].Text = "图示";
            cells[0].Width = Unit.Percentage(30.0);
            cells[1].Width = Unit.Percentage(20.0);
            cells[2].Width = Unit.Percentage(20.0);
            cells[3].Width = Unit.Percentage(30.0);
            row.Cells.AddRange(cells);
            child.Rows.Add(row);
            for (int i = 0; i < itemNames.Length; i++)
            {
                float num2 = Convert.ToSingle(items[i]) / Convert.ToSingle(totalNum);
                TableRow row2 = new TableRow();
                row2.CssClass = "tdbg";
                TableCell[] cellArray2 = new TableCell[] { new TableCell(), new TableCell(), new TableCell(), new TableCell() };
                cellArray2[0].Text = itemNames[i];
                cellArray2[1].Text = items[i].ToString();
                cellArray2[2].Text = string.Format("{0:p}", num2);
                cellArray2[3].Text = string.Format("<div class='StatBar' style='width:{0}%'/>", num2 * 90f);
                row2.Cells.AddRange(cellArray2);
                child.Rows.Add(row2);
            }
            this.LblCount.Text = totalNum.ToString();
            this.PlhStat.Controls.Add(child);
        }
    }
}

