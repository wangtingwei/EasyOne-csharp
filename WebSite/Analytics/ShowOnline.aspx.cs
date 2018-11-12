namespace EasyOne.WebSite.Analytics
{
    using EasyOne.Controls;
    using EasyOne.Model.Analytics;
    using EasyOne.Web.UI;
    using System;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public partial class ShowOnline : DynamicPage
    {
        protected void EgvOnLine_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                StatOnlineInfo dataItem = e.Row.DataItem as StatOnlineInfo;
                if (dataItem != null)
                {
                    string str = string.Empty;
                    TimeSpan span = (TimeSpan) (DateTime.Now - dataItem.OnTime);
                    int num = (span.Hours * 60) + span.Minutes;
                    if (num != 0)
                    {
                        str = str + num.ToString() + "'";
                    }
                    if (span.Seconds < 10)
                    {
                        str = str + "0";
                    }
                    str = str + span.Seconds.ToString() + "\"";
                    e.Row.Cells[0].Text = e.Row.RowIndex.ToString();
                    e.Row.Cells[4].Text = str;
                    HyperLink link = e.Row.FindControl("HlnkUrl") as HyperLink;
                    if (dataItem.UserPage.Length >= 40)
                    {
                        link.Text = dataItem.UserPage.Substring(0, 40) + "...";
                    }
                    else
                    {
                        link.Text = dataItem.UserPage;
                    }
                    link.ToolTip = dataItem.UserPage;
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }
    }
}

