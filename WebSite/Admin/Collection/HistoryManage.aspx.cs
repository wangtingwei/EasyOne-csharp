namespace EasyOne.WebSite.Admin.Collection
{
    using EasyOne.Collection;
    using EasyOne.Common;
    using EasyOne.Controls;
    using EasyOne.Web.UI;
    using System;
    using System.Data;
    using System.Text;
    using System.Web.UI.WebControls;

    public partial class HistoryManage : AdminPage
    {

        protected void BtnBatchDelete_Click(object sender, EventArgs e)
        {
            StringBuilder selectList = new StringBuilder("");
            selectList = this.EgvHistory.SelectList;
            if (selectList.Length > 0)
            {
                if (CollectionHistory.Delete(selectList.ToString()))
                {
                    AdminPage.WriteSuccessMsg("<li>删除指定的采集历史记录成功！</li>", "HistoryManage.aspx");
                }
                else
                {
                    AdminPage.WriteErrMsg("<li>删除失败！</li>");
                }
            }
            else
            {
                AdminPage.WriteErrMsg("<li>请选择要删除的采集历史记录！</li>");
            }
        }

        protected void BtnDeleteAll_Click(object sender, EventArgs e)
        {
            CollectionHistory.Delete();
            AdminPage.WriteSuccessMsg("<li>清空采集历史成功！</li>", "HistoryManage.aspx");
        }

        protected void BtnDeleteErr_Click(object sender, EventArgs e)
        {
            CollectionHistory.DeleteErr();
            AdminPage.WriteSuccessMsg("<li>删除全部失败采集历史记录成功！</li>", "HistoryManage.aspx");
        }

        protected void BtnDeleteSuccess_Click(object sender, EventArgs e)
        {
            CollectionHistory.DeleteSuccess();
            AdminPage.WriteSuccessMsg("<li>删除全部成功采集历史记录成功！</li>", "HistoryManage.aspx");
        }

        protected void EgvHistory_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label label = (Label) e.Row.FindControl("LblResult");
                HyperLink link = (HyperLink) e.Row.FindControl("LnkTitle");
                DataRowView dataItem = (DataRowView) e.Row.DataItem;
                if (DataConverter.CBoolean(dataItem["Result"].ToString()))
                {
                    link.Text = dataItem["Title"].ToString();
                    link.NavigateUrl = "../Contents/ContentView.aspx?Action=Modify&GeneralID=" + dataItem["GeneralID"].ToString() + "&NodeID=" + dataItem["NodeID"].ToString() + "&ModelID=" + dataItem["ModelID"].ToString();
                    link.Target = "_blank";
                    label.Text = "<span style='color:blue'>成功</span>";
                }
                else
                {
                    link.Text = dataItem["Title"].ToString();
                    label.Text = "<span style='color:red'>失败</span>";
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if ((!base.IsPostBack && (BasePage.RequestString("Action") == "Delete")) && CollectionHistory.Delete(BasePage.RequestString("HistoryID")))
            {
                AdminPage.WriteSuccessMsg("<li>删除指定的采集历史记录成功！</li>", "HistoryManage.aspx");
            }
        }
    }
}

