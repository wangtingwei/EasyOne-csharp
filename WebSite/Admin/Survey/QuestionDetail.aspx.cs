namespace EasyOne.WebSite.Admin.Survey
{
    using EasyOne.Controls;
    using EasyOne.Model.Survey;
    using EasyOne.Survey;
    using EasyOne.Web.UI;
    using System;
    using System.Collections.Generic;
    using System.Web.UI.WebControls;

    public partial class QuestionDetail : AdminPage
    {

        protected void EgvQuestionDetail_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                SurveyFieldInfo dataItem = e.Row.DataItem as SurveyFieldInfo;
                e.Row.Cells[0].Text = Convert.ToString((int) (((this.EgvQuestionDetail.PageIndex * this.EgvQuestionDetail.PageCount) + e.Row.RowIndex) + 1));
                e.Row.Cells[2].Text = SurveyField.GetQuestionType(dataItem.QuestionType);
                e.Row.Cells[3].Text = BasePage.RequestString("SurveyName");
            }
        }

        private IList<SurveyFieldInfo> GetDataSource()
        {
            if (string.IsNullOrEmpty(BasePage.RequestString("SurveyID")))
            {
                return null;
            }
            if ((((IList<SurveyFieldInfo>) base.Cache["SurveyFieldInfoList"]) == null) || (this.HdnSurveyId.Value != BasePage.RequestString("SurveyID")))
            {
                base.Cache["SurveyFieldInfoList"] = SurveyField.GetFieldList(BasePage.RequestInt32("SurveyID"));
                this.HdnSurveyId.Value = BasePage.RequestString("SurveyID");
            }
            return (base.Cache["SurveyFieldInfoList"] as IList<SurveyFieldInfo>);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(BasePage.RequestString("SurveyName")))
            {
                this.LblTitle.Text = "当前问卷：" + BasePage.RequestString("SurveyName");
            }
            if (!base.IsPostBack)
            {
                this.EgvQuestionDetail.DataSource = this.GetDataSource();
                this.EgvQuestionDetail.DataBind();
                this.HdnSurveyId.Value = BasePage.RequestString("SurveyID");
            }
        }
    }
}

