namespace EasyOne.WebSite.Admin.Survey
{
    using EasyOne.Controls;
    using EasyOne.Model.Survey;
    using EasyOne.Web.UI;
    using System;
    using System.Web.UI.WebControls;

    public partial class SurveyResult : AdminPage
    {

        protected void EgvSurvey_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                SurveyInfo dataItem = e.Row.DataItem as SurveyInfo;
                e.Row.Cells[1].Text = dataItem.CreateDate.ToString("yyyy-MM-dd");
                DateTime endTime = dataItem.EndTime;
                DateTime now = DateTime.Now;
                if (endTime>= now)
                {
                    TableCell cell1 = e.Row.Cells[1];
                    cell1.Text = cell1.Text + "/" + dataItem.EndTime.ToString("yyyy-MM-dd");
                }
                else
                {
                    TableCell cell2 = e.Row.Cells[1];
                    cell2.Text = cell2.Text + "/<span style='Color:#F00'>" + string.Format("{0:yyyy-MM-dd}", dataItem.EndTime) + "</span>";
                }
            }
        }
    }
}

