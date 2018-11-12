namespace EasyOne.WebSite.Survey
{
    using EasyOne.Model.Survey;
    using EasyOne.Survey;
    using EasyOne.Web.UI;
    using System;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public partial class AnswerList : DynamicPage
    {
        protected SurveyFieldInfo fieldInfo;
        private int surveyId;
        private int questionId;


        private void BindData()
        {
            this.RptAnswerList.DataSource = SurveyRecord.GetQuestionAnswer(this.surveyId, "Q" + this.questionId.ToString());
            this.RptAnswerList.DataBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.surveyId = BasePage.RequestInt32("SurveyID");
            this.questionId = BasePage.RequestInt32("QuestionID");
            if ((this.surveyId != 0) && (this.questionId != 0))
            {
                this.fieldInfo = SurveyField.GetFieldInfoById(this.surveyId, this.questionId);
                this.LblTitle.Text = "当前问卷：" + SurveyManager.GetSurveyById(this.surveyId).SurveyName + " / 当前问题：" + this.fieldInfo.QuestionContent;
                this.BindData();
            }
        }

        protected void RptAnswerList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if ((e.Item.ItemType == ListItemType.AlternatingItem) || (e.Item.ItemType == ListItemType.Item))
            {
                Literal literal = e.Item.FindControl("LtrAnswerList") as Literal;
                string str = Convert.ToString(e.Item.DataItem);
                if (!string.IsNullOrEmpty(str))
                {
                    literal.Text = "<td style='height:20px;'>" + str + "</td>";
                }
            }
        }
    }
}

