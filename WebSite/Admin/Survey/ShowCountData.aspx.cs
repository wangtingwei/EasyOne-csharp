namespace EasyOne.WebSite.Admin.Survey
{
    using Microsoft.Reporting.WebForms;
    using EasyOne.Common;
    using EasyOne.Controls;
    using EasyOne.Model.Survey;
    using EasyOne.Survey;
    using EasyOne.Web.UI;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Data;
    using System.Web.UI.WebControls;

    public partial class ShowCountData : AdminPage
    {
        protected const int BarWidth = 90;
        private int m_QuestionType;
        private int rowCount;

        protected void Page_Load(object sender, EventArgs e)
        {
            this.LblSurveyName.Text = string.IsNullOrEmpty(BasePage.RequestString("SurveyName")) ? "" : ("当前问卷：" + BasePage.RequestString("SurveyName"));
            this.HdnSurveyId.Value = BasePage.RequestString("SurveyID");
            this.HdnShowType.Value = BasePage.RequestString("ShowType");
        }

        protected void RptShowCountData_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            this.rowCount++;
            if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
            {
                SurveyFieldInfo dataItem = e.Item.DataItem as SurveyFieldInfo;
                Label label = e.Item.FindControl("LblQuestionContent") as Label;
                HyperLink link = e.Item.FindControl("LnkListAnswer") as HyperLink;
                PlaceHolder ph = e.Item.FindControl("PlhQuestion") as PlaceHolder;
                this.m_QuestionType = dataItem.QuestionType;
                label.Text = "&nbsp;" + this.rowCount.ToString() + "、（" + SurveyField.GetQuestionType(dataItem.QuestionType) + "）";
                link.NavigateUrl = "AnswerList.aspx?SurveyID=" + this.HdnSurveyId.Value + "&QuestionID=" + dataItem.QuestionId.ToString();
                link.Text = dataItem.QuestionContent;
                link.ToolTip = "[" + dataItem.QuestionContent + "]回答详情";
                if (((dataItem.QuestionType == 0) || (dataItem.QuestionType == 1)) || (((dataItem.QuestionType == 6) || (dataItem.QuestionType == 8)) || (dataItem.QuestionType == 9)))
                {
                    Literal child = new Literal();
                    child.Text = "<div class='border tdbg' style='width:99%; padding:5px 0 5px 0;'><a href='AnswerList.aspx?SurveyID=" + this.HdnSurveyId.Value + "&QuestionID=" + dataItem.QuestionId.ToString() + "'>&nbsp;内容</a></div>";
                    ph.Controls.Add(child);
                }
                else
                {
                    this.Show(dataItem.Settings, ph, dataItem.QuestionId);
                }
            }
        }

        private void Show(Collection<string> optionNames, PlaceHolder ph, int questionId)
        {
            IList<int> questionVoteAmountList = SurveyVote.GetQuestionVoteAmountList(DataConverter.CLng(this.HdnSurveyId.Value), questionId);
            if (questionVoteAmountList.Count <= 0)
            {
                ph.Visible = false;
            }
            else
            {
                switch (this.HdnShowType.Value)
                {
                    case "Pie":
                        this.ShowChart("Pie", optionNames, questionVoteAmountList, ph);
                        return;

                    case "Bar":
                        this.ShowChart("Bar", optionNames, questionVoteAmountList, ph);
                        return;
                }
                ph.Controls.Add(this.VoteTable(optionNames, questionVoteAmountList));
            }
        }

        private void ShowChart(string showType, Collection<string> optionNames, IList<int> optionValues, PlaceHolder ph)
        {
            DataTable dataSourceValue = new DataTable();
            dataSourceValue.Columns.Add("optionName", typeof(string));
            dataSourceValue.Columns.Add("optionValue", typeof(int));
            for (int i = 0; i < optionNames.Count; i++)
            {
                DataRow row = dataSourceValue.NewRow();
                if (this.m_QuestionType == 7)
                {
                    row["optionName"] = optionNames[i].Contains("0") ? "否" : "是";
                }
                else
                {
                    row["optionName"] = optionNames[i];
                }
                row["optionValue"] = optionValues[i];
                dataSourceValue.Rows.Add(row);
            }
            dataSourceValue.AcceptChanges();
            ReportViewer child = new ReportViewer();
            child.Width = Unit.Percentage(99.0);
            child.Height = Unit.Point(130);
            child.BorderWidth = Unit.Empty;
            child.InternalBorderStyle = BorderStyle.NotSet;
            child.ShowToolBar = false;
            child.LocalReport.ReportPath = (showType == "Pie") ? @"Admin\Survey\Pie.rdlc" : @"Admin\Survey\Bar.rdlc";
            child.LocalReport.DataSources.Add(new ReportDataSource("CustomDs", dataSourceValue));
            ph.Controls.Add(child);
        }

        private Table VoteTable(Collection<string> optionNames, IList<int> optionValues)
        {
            Table table = new Table();
            table.Width = Unit.Percentage(100.0);
            table.CellSpacing = 1;
            table.CellPadding = 2;
            table.CssClass = "border";
            TableRow row = new TableRow();
            row.CssClass = "title";
            TableCell[] cells = new TableCell[] { new TableCell(), new TableCell(), new TableCell(), new TableCell() };
            cells[0].Text = "选项";
            cells[1].Text = "票数";
            cells[2].Text = "百分比";
            cells[3].Text = "图示";
            cells[0].Width = Unit.Percentage(45.0);
            cells[1].Width = Unit.Percentage(10.0);
            cells[2].Width = Unit.Percentage(15.0);
            cells[3].Width = Unit.Percentage(30.0);
            row.Cells.AddRange(cells);
            table.Rows.Add(row);
            int input = 0;
            foreach (int num2 in optionValues)
            {
                input += num2;
            }
            for (int i = 0; i < optionNames.Count; i++)
            {
                double num4 = 0.0;
                if (input != 0)
                {
                    num4 = DataConverter.CDouble(optionValues[i]) / DataConverter.CDouble(input);
                }
                TableRow row2 = new TableRow();
                row2.CssClass = "tdbg";
                TableCell[] cellArray2 = new TableCell[] { new TableCell(), new TableCell(), new TableCell(), new TableCell() };
                if (this.m_QuestionType == 7)
                {
                    cellArray2[0].Text = optionNames[i].Contains("0") ? "否" : "是";
                }
                else
                {
                    cellArray2[0].Text = optionNames[i];
                }
                cellArray2[1].Text = optionValues[i].ToString();
                cellArray2[2].Text = string.Format("{0:p}", num4);
                cellArray2[3].Text = "<img src='../Images/bar.gif' height='10'width='" + Convert.ToString((double) (num4 * 90.0)) + "'</img>";
                cellArray2[3].Text = string.Format("<div class='StatBar' style='width:{0}%'/>", num4 * 90.0);
                row2.Cells.AddRange(cellArray2);
                table.Rows.Add(row2);
            }
            return table;
        }
    }
}

