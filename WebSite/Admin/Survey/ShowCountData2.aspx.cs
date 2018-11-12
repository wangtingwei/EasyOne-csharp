namespace EasyOne.WebSite.Admin.Survey
{
    using EasyOne.Common;
    using EasyOne.Controls;
    using EasyOne.ExtendedControls;
    using EasyOne.Model.Survey;
    using EasyOne.Survey;
    using EasyOne.Web.UI;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Text;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public partial class ShowCountData2 : AdminPage
    {
        protected string ip;
        protected int m_RecordId;
        protected int m_SurveyId;

        private void BindData()
        {
            this.RptShowCountList.DataSource = SurveyRecord.GetList((this.Pager.CurrentPageIndex - 1) * this.Pager.PageSize, this.Pager.PageSize, this.m_SurveyId, this.m_RecordId);
            this.RptShowCountList.DataBind();
            this.Pager.RecordCount = SurveyRecord.GetTotalOfSurveyRecord();
        }

        protected void BtnDelete_Click(object sender, EventArgs e)
        {
            if (SurveyRecord.Delete(this.m_RecordId.ToString(), this.m_SurveyId))
            {
                AdminPage.WriteSuccessMsg("删除记录成功", "ShowCountData3.aspx?SurveyID=" + this.m_SurveyId);
            }
            else
            {
                AdminPage.WriteErrMsg("<li>删除记录失败</li>");
            }
        }

        protected void BtnReturn_Click(object sender, EventArgs e)
        {
            BasePage.ResponseRedirect("ShowCountData3.aspx?SurveyID=" + this.m_SurveyId);
        }

        private static string GetAnswer(IList<SurveyFieldInfo> surveyFieldInfoList, int i, DataTable dataTable)
        {
            switch (surveyFieldInfoList[i].QuestionType)
            {
                case 2:
                case 4:
                    return DataSecurity.GetArrayValue(DataConverter.CLng(dataTable.Rows[0][i], -1), surveyFieldInfoList[i].Settings);

                case 3:
                case 5:
                {
                    StringBuilder sb = new StringBuilder();
                    foreach (string str2 in dataTable.Rows[0][i].ToString().Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        string arrayValue = DataSecurity.GetArrayValue(DataConverter.CLng(str2, -1), surveyFieldInfoList[i].Settings);
                        StringHelper.AppendString(sb, arrayValue, "&nbsp;&nbsp;&nbsp;&nbsp;");
                    }
                    return sb.ToString();
                }
                case 7:
                    if (DataConverter.CLng(dataTable.Rows[0][i]) != 0)
                    {
                        return "是";
                    }
                    return "否";
            }
            return dataTable.Rows[0][i].ToString();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.m_SurveyId = BasePage.RequestInt32("SurveyID");
            this.m_RecordId = BasePage.RequestInt32("RecordID");
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
                this.Pager.PageSize = 1;
                this.BindData();
                if (this.m_RecordId > 0)
                {
                    this.showPage.Visible = false;
                    this.showButton.Visible = true;
                }
                else
                {
                    this.showPage.Visible = true;
                    this.showButton.Visible = false;
                }
            }
        }

        protected void Pager_PageChanged(object src, PageChangedEventArgs e)
        {
            this.Pager.CurrentPageIndex = e.NewPageIndex;
            this.BindData();
        }

        private static void RptShowCountDataBind(Repeater rptShowCountData, IList<SurveyFieldInfo> surveyFieldInfoList)
        {
            int num = 1;
            for (int i = 0; i < surveyFieldInfoList.Count; i++)
            {
                if (((surveyFieldInfoList[i].QuestionType == 2) || (surveyFieldInfoList[i].QuestionType == 3)) && (surveyFieldInfoList[i].InputType != 0))
                {
                    surveyFieldInfoList[i].QuestionContent = num + "、" + surveyFieldInfoList[i].QuestionContent;
                    i++;
                    SurveyFieldInfo item = new SurveyFieldInfo();
                    item.QuestionContent = "填空";
                    surveyFieldInfoList.Insert(i, item);
                }
                else
                {
                    surveyFieldInfoList[i].QuestionContent = num + "、" + surveyFieldInfoList[i].QuestionContent;
                }
                num++;
            }
            rptShowCountData.DataSource = surveyFieldInfoList;
            rptShowCountData.DataBind();
        }

        protected void RptShowCountList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
            {
                Repeater rptShowCountData = (Repeater) e.Item.FindControl("RptShowCountData");
                IList<SurveyFieldInfo> surveyFieldInfoList = new List<SurveyFieldInfo>();
                surveyFieldInfoList = SurveyField.GetFieldList(this.m_SurveyId);
                if (surveyFieldInfoList != null)
                {
                    RptShowCountDataBind(rptShowCountData, surveyFieldInfoList);
                    int i = 0;
                    DataTable dataTable = new DataTable();
                    dataTable = ((SurveyRecordInfo) e.Item.DataItem).Answer;
                    foreach (RepeaterItem item in rptShowCountData.Controls)
                    {
                        if (item.ItemType == ListItemType.Header)
                        {
                            Label label = (Label) item.FindControl("LblIP");
                            Label label2 = (Label) item.FindControl("LblSubmitTime");
                            label.Text = ((SurveyRecordInfo) e.Item.DataItem).IP;
                            label2.Text = ((SurveyRecordInfo) e.Item.DataItem).SubmitTime.ToString();
                        }
                        else if ((item.ItemType == ListItemType.Item) || (item.ItemType == ListItemType.AlternatingItem))
                        {
                            Label label3 = (Label) item.FindControl("LblAnswer");
                            label3.Text = GetAnswer(surveyFieldInfoList, i, dataTable);
                            i++;
                        }
                    }
                }
            }
        }
    }
}

