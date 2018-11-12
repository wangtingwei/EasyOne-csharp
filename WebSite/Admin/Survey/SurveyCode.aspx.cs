namespace EasyOne.WebSite.Admin.Survey
{
    using EasyOne.Controls;
    using EasyOne.Model.Survey;
    using EasyOne.Web.UI;
    using System;
    using System.Web.UI.WebControls;

    public partial class SurveyCode : AdminPage
    {

        protected void Egv_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                SurveyInfo dataItem = (SurveyInfo) e.Row.DataItem;
                string fullBasePath = base.FullBasePath;
                if (!string.IsNullOrEmpty(dataItem.FileName))
                {
                    ((TextBox) e.Row.FindControl("TxtFrameCode")).Text = "<IFrame src='" + fullBasePath + "Survey/" + dataItem.FileName + "' frameBorder='0' scrolling='no'>" + dataItem.SurveyName + "</IFrame>";
                    ((TextBox) e.Row.FindControl("TxtLinkCode")).Text = "<a href='" + fullBasePath + "Survey/" + dataItem.FileName + "' target='_blank'>" + dataItem.SurveyName + "</a>";
                }
                else
                {
                    ((TextBox) e.Row.FindControl("TxtFrameCode")).Text = "问卷未完成！";
                    ((TextBox) e.Row.FindControl("TxtLinkCode")).Text = "问卷未完成！";
                }
                if (dataItem.IsOpen == 1)
                {
                    ((TextBox) e.Row.FindControl("TxtResultCode")).Text = string.Concat(new object[] { "<a href='", fullBasePath, "Survey/ListReport.aspx?SurveyID=", dataItem.SurveyId, "' target='_blank'>", dataItem.SurveyName, "调查结果</a>" });
                }
                else
                {
                    ((TextBox) e.Row.FindControl("TxtResultCode")).Text = "指定的问卷未启用，无统计数据！";
                }
            }
        }
    }
}

