namespace EasyOne.WebSite.Admin.Collection
{
    using EasyOne.Collection;
    using EasyOne.Common;
    using EasyOne.Components;
    using EasyOne.Controls;
    using EasyOne.Web.UI;
    using System;
    using System.Data;
    using System.Text;
    using System.Web;
    using System.Web.UI.WebControls;

    public partial class CollectionMain : AdminPage
    {

        protected void BtnCollec_Click(object sender, EventArgs e)
        {
            StringBuilder selectList = new StringBuilder("");
            selectList = this.EgvItemRules.SelectList;
            bool flag = DataConverter.CBoolean(base.Request.Form["ChkIsTitle"]);
            if (selectList.Length > 0)
            {
                CollectionProgress progress = new CollectionProgress();
                progress.ItemId = selectList.ToString();
                progress.IsTitle = flag;
                progress.PhysicalApplicationPath = HttpContext.Current.Request.PhysicalApplicationPath;
                progress.UserName = PEContext.Current.Admin.UserName;
                progress.AdminName = PEContext.Current.Admin.AdminName;
                progress.CreateCollectionProc();
                string str = "false";
                if (CollectionItem.ExistsCreateHtml(selectList.ToString()))
                {
                    str = "true";
                }
                BasePage.ResponseRedirect("CollectionProc.aspx?ItemIds=" + selectList.ToString() + "&isCreateHtml=" + str);
            }
            else
            {
                AdminPage.WriteErrMsg("<li>请选择要采集的项目！</li>");
            }
        }

        protected void EgvItemRules_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView dataItem = (DataRowView) e.Row.DataItem;
                Label label = (Label) e.Row.FindControl("LblSuccessRecord");
                Label label2 = (Label) e.Row.FindControl("LblFailureRecord");
                int num = CollectionHistory.SuccessRecord(DataConverter.CLng(dataItem["ItemId"].ToString()));
                int num2 = CollectionHistory.FailureRecord(DataConverter.CLng(dataItem["ItemId"].ToString()));
                label.Text = "<span style='color:blue'>" + num.ToString() + "</span>";
                if (num2 == 0)
                {
                    label2.Text = "<span style='color:green'>" + num2.ToString() + "</span>";
                }
                else
                {
                    label2.Text = "<span style='color:red'>" + num2.ToString() + "</span>";
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            bool isPostBack = base.IsPostBack;
        }
    }
}

