namespace EasyOne.WebSite.Survey
{
    using Microsoft.Reporting.WebForms;
    using EasyOne.Common;
    using EasyOne.Model.Survey;
    using EasyOne.Survey;
    using EasyOne.Web.UI;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Data;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public partial class ListReport : DynamicPage
    {
        protected const int BarWidth = 90;
        private int rowCount;
        private int showType;
        private int surveyId;

        protected void Page_Load(object sender, EventArgs e)
        {
            this.surveyId = BasePage.RequestInt32("SurveyID");
            this.showType = BasePage.RequestInt32("ShowType");
            this.BtnReturn.Visible = !string.IsNullOrEmpty(base.Request.ServerVariables["HTTP_REFERER"]);
            if (this.surveyId == 0)
            {
                DynamicPage.WriteErrMsg("<li>请指定问卷ID</li>");
            }
            else if (!SurveyManager.SurveyIdOfPassedExists(this.surveyId))
            {
                DynamicPage.WriteErrMsg("<li>该问卷未启用，不能查看调查结果！</li>");
            }
            else
            {
                this.LblSurveyName.Text = "《" + SurveyManager.GetSurveyById(this.surveyId).SurveyName + "》问卷调查结果";
            }
        }

        protected void RptShowCountData_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            this.rowCount++;
            if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
            {
                SurveyFieldInfo dataItem = e.Item.DataItem as SurveyFieldInfo;
                Label label = e.Item.FindControl("LblQuestionContent") as Label;
                PlaceHolder ph = e.Item.FindControl("PlhQuestion") as PlaceHolder;
                label.Text = "&nbsp;" + this.rowCount.ToString() + "、" + dataItem.QuestionContent + "（" + SurveyField.GetQuestionType(dataItem.QuestionType) + "）";
                if (((dataItem.QuestionType == 0) || (dataItem.QuestionType == 1)) || (((dataItem.QuestionType == 6) || (dataItem.QuestionType == 8)) || (dataItem.QuestionType == 9)))
                {
                    Literal child = new Literal();
                    child.Text = "<div class='border tdbg' style='width:99%; padding:5px 0 5px 0;'><a href='AnswerList.aspx?SurveyID=" + this.surveyId.ToString() + "&QuestionID=" + dataItem.QuestionId.ToString() + "'>+ 详细内容...</a></div>";
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
            IList<int> questionVoteAmountList = SurveyVote.GetQuestionVoteAmountList(this.surveyId, questionId);
            if (questionVoteAmountList.Count > 0)
            {
                switch (this.showType)
                {
                    case 1:
                        this.ShowChart("Pie", optionNames, questionVoteAmountList, ph);
                        return;

                    case 2:
                        this.ShowChart("Bar", optionNames, questionVoteAmountList, ph);
                        return;
                }
                ph.Controls.Add(this.VoteTable(optionNames, questionVoteAmountList));
            }
            else
            {
                ph.Visible = false;
            }
        }

        private void ShowChart(string type, Collection<string> optionNames, IList<int> optionValues, PlaceHolder ph)
        {
            DataTable dataSourceValue = new DataTable();
            dataSourceValue.Columns.Add("optionName", typeof(string));
            dataSourceValue.Columns.Add("optionValue", typeof(int));
            for (int i = 0; i < optionNames.Count; i++)
            {
                DataRow row = dataSourceValue.NewRow();
                row["optionName"] = optionNames[i];
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
            child.LocalReport.ReportPath = (type == "Pie") ? @"Admin\Survey\Pie.rdlc" : @"Admin\Survey\Bar.rdlc";
            child.LocalReport.DataSources.Clear();
            child.LocalReport.DataSources.Add(new ReportDataSource("CustomDs", dataSourceValue));
            child.LocalReport.Refresh();
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
                cellArray2[0].Text = optionNames[i];
                cellArray2[1].Text = optionValues[i].ToString();
                cellArray2[2].Text = string.Format("{0:p}", num4);
                cellArray2[3].Text = string.Format("<div class='StatBar' style='width:{0}%'/>", num4 * 90.0);
                row2.Cells.AddRange(cellArray2);
                table.Rows.Add(row2);
            }
            return table;
        }
    }
}

