namespace EasyOne.WebSite.Admin.Survey
{
    using EasyOne.Common;
    using EasyOne.Controls;
    using EasyOne.Model.Survey;
    using EasyOne.Survey;
    using EasyOne.Web.UI;
    using System;
    using System.Text;
    using System.Web.UI.WebControls;

    public partial class AnswerList : AdminPage
    {
        protected SurveyFieldInfo fieldInfo;
        protected bool IsThree;
        protected int questionId;
        protected int surveyId;

        private void BindData()
        {
            this.RptAnswerList.DataSource = SurveyRecord.GetList((this.Pager.CurrentPageIndex - 1) * this.Pager.PageSize, this.Pager.PageSize, this.surveyId, 0);
            this.RptAnswerList.DataBind();
            this.Pager.RecordCount = SurveyRecord.GetTotalOfSurveyRecord();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.surveyId = BasePage.RequestInt32("SurveyID");
            this.questionId = BasePage.RequestInt32("QuestionID");
            if ((this.surveyId != 0) && (this.questionId != 0))
            {
                this.fieldInfo = SurveyField.GetFieldInfoById(this.surveyId, this.questionId);
                this.LblTitle.Text = "当前问题：" + this.fieldInfo.QuestionContent;
                this.SmpNavigator.CurrentNode = "当前问卷：" + SurveyManager.GetSurveyById(this.surveyId).SurveyName;
                if ((this.fieldInfo.QuestionType == 2) || (this.fieldInfo.QuestionType == 3))
                {
                    this.IsThree = true;
                }
                this.BindData();
            }
            if (!base.IsPostBack)
            {
                this.Pager.PageSize = 10;
            }
        }

        protected void Pager_PageChanged(object sender, PageChangedEventArgs e)
        {
            this.Pager.CurrentPageIndex = e.NewPageIndex;
            this.BindData();
        }

        protected void RptAnswerList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if ((e.Item.ItemType != ListItemType.AlternatingItem) && (e.Item.ItemType != ListItemType.Item))
            {
                return;
            }
            StringBuilder builder = new StringBuilder(0x80);
            Literal literal = e.Item.FindControl("LtrAnswerList") as Literal;
            SurveyRecordInfo dataItem = e.Item.DataItem as SurveyRecordInfo;
            builder.Append("<tr class='tdbg'>");
            builder.Append("<td>" + dataItem.IP + "</td>");
            string str = "Q" + this.questionId.ToString();
            string name = str + "Input";
            switch (this.fieldInfo.QuestionType)
            {
                case 0:
                case 1:
                case 6:
                case 8:
                case 9:
                    builder.Append("<td>" + dataItem.Answer.Rows[e.Item.ItemIndex][str].ToString() + "</td>");
                    goto Label_0438;

                case 2:
                {
                    int index = DataConverter.CLng(dataItem.Answer.Rows[e.Item.ItemIndex][str].ToString());
                    if (index <= this.fieldInfo.Settings.Count)
                    {
                        builder.Append("<td>" + DataSecurity.GetArrayValue(index, this.fieldInfo.Settings) + "</td>");
                        break;
                    }
                    builder.Append("<td>其他</td>");
                    break;
                }
                case 3:
                    builder.Append("<td>");
                    foreach (string str3 in dataItem.Answer.Rows[e.Item.ItemIndex][str].ToString().Split(new char[] { ',' }))
                    {
                        builder.Append(DataSecurity.GetArrayValue(DataConverter.CLng(str3), this.fieldInfo.Settings) + " ");
                    }
                    builder.Append("</td>");
                    if (dataItem.Answer.Columns.Contains(name))
                    {
                        builder.Append("<td>" + dataItem.Answer.Rows[e.Item.ItemIndex][name].ToString() + "</td>");
                    }
                    else
                    {
                        builder.Append("<td></td>");
                    }
                    goto Label_0438;

                case 4:
                    builder.Append("<td>" + this.fieldInfo.Settings[DataConverter.CLng(dataItem.Answer.Rows[e.Item.ItemIndex][str].ToString())] + "</td>");
                    goto Label_0438;

                case 5:
                    builder.Append("<td>");
                    foreach (string str4 in dataItem.Answer.Rows[e.Item.ItemIndex][str].ToString().Split(new char[] { ',' }))
                    {
                        builder.Append(DataSecurity.GetArrayValue(DataConverter.CLng(str4), this.fieldInfo.Settings) + " ");
                    }
                    builder.Append("</td>");
                    goto Label_0438;

                case 7:
                    if (DataConverter.CLng(dataItem.Answer.Rows[e.Item.ItemIndex][str].ToString()) != 0)
                    {
                        builder.Append("<td>是</td>");
                    }
                    else
                    {
                        builder.Append("<td>否</td>");
                    }
                    goto Label_0438;

                default:
                    goto Label_0438;
            }
            if (dataItem.Answer.Columns.Contains(name))
            {
                builder.Append("<td>" + dataItem.Answer.Rows[e.Item.ItemIndex][name].ToString() + "</td>");
            }
            else
            {
                builder.Append("<td></td>");
            }
        Label_0438:
            builder.Append("</tr>");
            literal.Text = builder.ToString();
        }
    }
}

