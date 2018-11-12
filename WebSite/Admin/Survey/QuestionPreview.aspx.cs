namespace EasyOne.WebSite.Admin.Survey
{
    using EasyOne.Common;
    using EasyOne.Controls;
    using EasyOne.Model.Survey;
    using EasyOne.Survey;
    using EasyOne.Web.UI;
    using System;
    using System.Text;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public partial class QuestionPreview : AdminPage
    {
        protected StringBuilder questionHTML;
        protected int questionId;
        private SurveyFieldInfo questionInfo;
        protected int questionType;
        protected int surveyId;

        protected void BtnModify_Click(object sender, EventArgs e)
        {
            BasePage.ResponseRedirect(string.Concat(new object[] { "Question.aspx?Action=Modify&SurveyID=", this.surveyId, "&QuestionID=", this.questionId }));
        }

        protected void BtnReturn_Click(object sender, EventArgs e)
        {
            BasePage.ResponseRedirect("QuestionManage.aspx?SurveyID=" + this.surveyId);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.questionType = BasePage.RequestInt32("QuestionType");
            this.questionId = BasePage.RequestInt32("QuestionID");
            this.surveyId = BasePage.RequestInt32("SurveyID");
            this.questionInfo = SurveyField.GetFieldInfoById(this.surveyId, this.questionId);
            this.ShowQuestionContent();
            if (DataConverter.CLng(base.Request.QueryString["IsOpen"]) == 1)
            {
                this.DivModify.Visible = false;
            }
        }

        private void ShowQuestionContent()
        {
            this.questionHTML = new StringBuilder();
            switch (this.questionType)
            {
                case 0:
                case 6:
                case 8:
                case 9:
                    this.questionHTML.Append(this.questionInfo.QuestionContent + "<input type='text' />");
                    return;

                case 1:
                    this.questionHTML.Append(this.questionInfo.QuestionContent + "<textarea></textarea>");
                    return;

                case 2:
                    this.questionHTML.Append(this.questionInfo.QuestionContent + "<br/>");
                    for (int i = 0; i < this.questionInfo.Settings.Count; i++)
                    {
                        this.questionHTML.Append("<input name=radio1 type='radio'>" + this.questionInfo.Settings[i] + "<br />");
                    }
                    switch (this.questionInfo.InputType)
                    {
                        case 1:
                            this.questionHTML.Append("<input type='text' name=text1>");
                            return;

                        case 2:
                            this.questionHTML.Append("<textarea></textarea>");
                            return;
                    }
                    return;

                case 3:
                    this.questionHTML.Append(this.questionInfo.QuestionContent + "<br/>");
                    for (int j = 0; j < this.questionInfo.Settings.Count; j++)
                    {
                        this.questionHTML.Append("<input name=checkbox1 type='checkbox'>" + this.questionInfo.Settings[j] + "<br />");
                    }
                    switch (this.questionInfo.InputType)
                    {
                        case 1:
                            this.questionHTML.Append("<input type='text' name=text1>");
                            return;

                        case 2:
                            this.questionHTML.Append("<textarea></textarea>");
                            return;
                    }
                    return;

                case 4:
                    this.questionHTML.Append(this.questionInfo.QuestionContent + "<br/>");
                    this.questionHTML.Append("<select>");
                    for (int k = 0; k < this.questionInfo.Settings.Count; k++)
                    {
                        this.questionHTML.Append("<option>" + this.questionInfo.Settings[k] + "</option><br />");
                    }
                    this.questionHTML.Append("</select>");
                    return;

                case 5:
                    this.questionHTML.Append(this.questionInfo.QuestionContent + "<br/>");
                    this.questionHTML.Append("<select multiple=multiple style='height:160px;width:140px;'>");
                    for (int m = 0; m < this.questionInfo.Settings.Count; m++)
                    {
                        this.questionHTML.Append("<option>" + this.questionInfo.Settings[m] + "</option><br />");
                    }
                    this.questionHTML.Append("</select>");
                    return;

                case 7:
                    this.questionHTML.Append(this.questionInfo.QuestionContent + "<input type='radio' />是 <input type='radio'>否");
                    return;
            }
        }
    }
}

