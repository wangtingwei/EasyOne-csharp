namespace EasyOne.WebSite.Admin.Survey
{
    using Microsoft.Reporting.WebForms;
    using EasyOne.ExtendedControls;
    using EasyOne.Model.Survey;
    using EasyOne.Survey;
    using EasyOne.Web.UI;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Data;
    using System.Text;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public partial class SurveyReport : AdminPage
    {
        protected const int BarWidth = 220;
        private int rowNumber1;
        private int rowNumber2;
        private int surveyId;

        protected void Page_Load(object sender, EventArgs e)
        {
            this.surveyId = BasePage.RequestInt32("SurveyID");
            if (!base.IsPostBack)
            {
                if (this.surveyId == 0)
                {
                    AdminPage.WriteErrMsg("<li>请指定问卷ID</li>", "SurveyManage.aspx");
                }
                else if (!SurveyManager.SurveyIdOfPassedExists(this.surveyId))
                {
                    AdminPage.WriteErrMsg("<li>该问卷未启用，不能查看报表！</li>");
                }
                else
                {
                    SurveyInfo surveyById = SurveyManager.GetSurveyById(this.surveyId);
                    if (!surveyById.IsNull)
                    {
                        this.LblSurveyName.Text = surveyById.SurveyName;
                        this.LblDescription.Text = surveyById.Description;
                        this.LblTitle.Text = "[" + surveyById.SurveyName + "]问卷报表";
                        this.LblSurveyNumber.Text = SurveyRecord.GetTotalOfSurveyRecord(this.surveyId).ToString();
                        this.LblDate.Text = surveyById.CreateDate.ToString() + "/" + surveyById.EndTime.ToString();
                    }
                    else
                    {
                        AdminPage.WriteErrMsg("<li>未找到对应的问卷信息</li>");
                    }
                }
                this.BtnReport.Attributes.Add("onclick", "javascript:window.print();return false;");
            }
        }

        protected void RptSurveyQuestion_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
        }

        protected void RptSurveyQuestion_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            this.rowNumber1++;
            if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
            {
                SurveyFieldInfo dataItem = e.Item.DataItem as SurveyFieldInfo;
                Label label = e.Item.FindControl("LblQuestion") as Label;
                label.Text = "&nbsp;" + this.rowNumber1.ToString() + "、" + dataItem.QuestionContent;
                Label label2 = e.Item.FindControl("LblOptions") as Label;
                StringBuilder builder = new StringBuilder("");
                if (((dataItem.QuestionType == 2) || (dataItem.QuestionType == 3)) || (((dataItem.QuestionType == 4) || (dataItem.QuestionType == 5)) || (dataItem.QuestionType == 7)))
                {
                    for (int i = 0; i < dataItem.Settings.Count; i++)
                    {
                        builder.Append("&nbsp;&nbsp;" + dataItem.Settings[i]);
                    }
                    label2.Text = builder.ToString();
                }
                else
                {
                    switch (dataItem.QuestionType)
                    {
                        case 0:
                            label2.Text = "&nbsp;&nbsp;此题为单行填空";
                            return;

                        case 1:
                            label2.Text = "&nbsp;&nbsp;此题为多行填空";
                            return;

                        case 6:
                            label2.Text = "&nbsp;&nbsp;此题为日期时间";
                            return;

                        case 7:
                            return;

                        case 8:
                            label2.Text = "&nbsp;&nbsp;此题为数字";
                            return;

                        case 9:
                            label2.Text = "&nbsp;&nbsp;此题为Email";
                            return;
                    }
                }
            }
        }

        protected void RptSurveyVote_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            this.rowNumber2++;
            if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
            {
                SurveyFieldInfo dataItem = e.Item.DataItem as SurveyFieldInfo;
                Label label = e.Item.FindControl("LblQuestionContent") as Label;
                label.Text = "&nbsp;" + this.rowNumber2.ToString() + "、（" + SurveyField.GetQuestionType(dataItem.QuestionType) + "）";
                HyperLink link = e.Item.FindControl("LnkListAnswer") as HyperLink;
                link.NavigateUrl = "AnswerList.aspx?SurveyID=" + this.surveyId.ToString() + "&QuestionID=" + dataItem.QuestionId.ToString();
                link.Text = dataItem.QuestionContent;
                link.ToolTip = "[" + dataItem.QuestionContent + "]回答详情";
                PlaceHolder ph = e.Item.FindControl("PlhQuestion") as PlaceHolder;
                if (((dataItem.QuestionType == 0) || (dataItem.QuestionType == 1)) || (((dataItem.QuestionType == 6) || (dataItem.QuestionType == 8)) || (dataItem.QuestionType == 9)))
                {
                    Literal child = new Literal();
                    child.Text = "<div class='border tdbg' style='width:99%; padding:5px 0 5px 0;'><a href='AnswerList.aspx?SurveyID=" + this.surveyId.ToString() + "&QuestionID=" + dataItem.QuestionId.ToString() + "'>&nbsp;内容</a></div>";
                    ph.Controls.Add(child);
                }
                else
                {
                    this.ShowCountTable(dataItem.Settings, ph, dataItem.QuestionId);
                }
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
            child.LocalReport.ReportPath = (showType == "Pie") ? @"Admin\Survey\Pie.rdlc" : @"Admin\Survey\Bar.rdlc";
            child.LocalReport.DataSources.Add(new ReportDataSource("CustomDs", dataSourceValue));
            ph.Controls.Add(child);
        }

        private void ShowCountTable(Collection<string> optionNames, PlaceHolder ph, int questionId)
        {
            IList<int> questionVoteAmountList = SurveyVote.GetQuestionVoteAmountList(this.surveyId, questionId);
            if (questionVoteAmountList.Count <= 0)
            {
                ph.Visible = false;
            }
            else
            {
                switch (BasePage.RequestString("ShowType"))
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
            int num = 0;
            foreach (int num2 in optionValues)
            {
                num += num2;
            }
            for (int i = 0; i < optionNames.Count; i++)
            {
                float num4 = 0f;
                if (num != 0)
                {
                    num4 = Convert.ToSingle(optionValues[i]) / Convert.ToSingle(num);
                }
                TableRow row2 = new TableRow();
                row2.CssClass = "tdbg";
                TableCell[] cellArray2 = new TableCell[] { new TableCell(), new TableCell(), new TableCell(), new TableCell() };
                cellArray2[0].Text = optionNames[i];
                cellArray2[1].Text = optionValues[i].ToString();
                cellArray2[2].Text = string.Format("{0:p}", num4);
                cellArray2[3].Text = string.Format("<div class='StatBar' style='width:{0}%'/>", num4 * 220f);
                row2.Cells.AddRange(cellArray2);
                table.Rows.Add(row2);
            }
            return table;
        }
    }
}

