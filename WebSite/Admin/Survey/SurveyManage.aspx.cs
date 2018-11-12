namespace EasyOne.WebSite.Admin.Survey
{
    using EasyOne.Common;
    using EasyOne.Controls;
    using EasyOne.ExtendedControls;
    using EasyOne.Model.Survey;
    using EasyOne.Survey;
    using EasyOne.Web.UI;
    using System;
    using System.Web.UI.WebControls;

    public partial class SurveyManage : AdminPage
    {

        protected void BtnDelete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.EgvSurvey.SelectList.ToString()))
            {
                AdminPage.WriteErrMsg("请指定要删除的问卷ID！", "SurveyManage.aspx");
            }
            else if (SurveyManager.Delete(this.EgvSurvey.SelectList.ToString()))
            {
                AdminPage.WriteSuccessMsg("删除问卷成功！", "Surveymanage.aspx");
            }
        }

        protected void EgvSurvey_RowCommand(object sender, CommandEventArgs e)
        {
            int surveyId = DataConverter.CLng(e.CommandArgument);
            string commandName = e.CommandName;
            if (commandName != null)
            {
                if (!(commandName == "SetState1"))
                {
                    if (!(commandName == "SetState2"))
                    {
                        if (!(commandName == "SetState0"))
                        {
                            if (commandName == "Del")
                            {
                                if (SurveyManager.Delete(surveyId.ToString()))
                                {
                                    AdminPage.WriteSuccessMsg("删除问卷成功！", "Surveymanage.aspx");
                                    return;
                                }
                                AdminPage.WriteErrMsg("<li>删除问卷失败！</li>");
                            }
                            return;
                        }
                        if (SurveyManager.SetPassed(surveyId))
                        {
                            AdminPage.WriteSuccessMsg("启用问题操作成功！", "SurveyManage.aspx");
                            return;
                        }
                        AdminPage.WriteErrMsg("<li>启用问题操作失败！</li>", "SurveyManage.aspx");
                        return;
                    }
                }
                else
                {
                    if (SurveyManager.SetForbid(surveyId))
                    {
                        AdminPage.WriteSuccessMsg("禁用问题操作成功！", "SurveyManage.aspx");
                        return;
                    }
                    AdminPage.WriteErrMsg("<li>禁用问题操作失败！</li>", "SurveyManage.aspx");
                    return;
                }
                if (SurveyManager.SetPassedOfForbid(surveyId))
                {
                    AdminPage.WriteSuccessMsg("启用问题操作成功！", "SurveyManage.aspx");
                }
                else
                {
                    AdminPage.WriteErrMsg("<li>启用问题操作失败！</li>", "SurveyManage.aspx");
                }
            }
        }

        protected void EgvSurvey_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                SurveyInfo dataItem = e.Row.DataItem as SurveyInfo;
                e.Row.Cells[2].Text = dataItem.CreateDate.ToString("yyyy-MM-dd");
                DateTime endTime = dataItem.EndTime;
                DateTime now = DateTime.Now;
                if (endTime>= now)
                {
                    TableCell cell1 = e.Row.Cells[2];
                    cell1.Text = cell1.Text + "/" + dataItem.EndTime.ToString("yyyy-MM-dd");
                }
                else
                {
                    TableCell cell2 = e.Row.Cells[2];
                    cell2.Text = cell2.Text + "/<span style='Color:#F00'>" + string.Format("{0:yyyy-MM-dd}", dataItem.EndTime) + "</span>";
                }
                e.Row.Cells[3].Text = SurveyManager.GetStateName(dataItem.IsOpen);
                if (dataItem.IsOpen == 1)
                {
                    e.Row.Cells[0].Enabled = false;
                }
            }
        }
    }
}

