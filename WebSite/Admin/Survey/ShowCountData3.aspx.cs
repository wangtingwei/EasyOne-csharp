namespace EasyOne.WebSite.Admin.Survey
{
    using EasyOne.Controls;
    using EasyOne.Survey;
    using EasyOne.Web.UI;
    using System;
    using System.Web.UI.WebControls;

    public partial class ShowCountData3 : AdminPage
    {
        protected int i;
        protected int m_SurveyId;

        private void BindData()
        {
            this.DlstSurveyRecord.DataSource = SurveyRecord.GetList((this.Pager.CurrentPageIndex - 1) * this.Pager.PageSize, this.Pager.PageSize, this.m_SurveyId, 0);
            this.Pager.RecordCount = SurveyRecord.GetTotalOfSurveyRecord();
            this.DlstSurveyRecord.DataBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.m_SurveyId = BasePage.RequestInt32("SurveyID");
            if (this.m_SurveyId == 0)
            {
                AdminPage.WriteErrMsg("<li>请指定问卷ID</li>", "SurveyManage.aspx");
            }
            else if (!SurveyManager.SurveyIdOfPassedExists(this.m_SurveyId))
            {
                AdminPage.WriteErrMsg("<li>该问卷未启用，不能查看调查结果！</li>");
            }
            else
            {
                this.Pager.PageSize = 40;
                this.BindData();
            }
        }

        protected void Pager_PageChanged(object src, PageChangedEventArgs e)
        {
            this.Pager.CurrentPageIndex = e.NewPageIndex;
            this.BindData();
        }
    }
}

