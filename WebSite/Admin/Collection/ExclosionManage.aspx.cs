namespace EasyOne.WebSite.Admin.Collection
{
    using EasyOne.Collection;
    using EasyOne.Controls;
    using EasyOne.Model.Collection;
    using EasyOne.Web.UI;
    using System;
    using System.Text;
    using System.Web.UI.WebControls;

    public partial class ExclosionManage : AdminPage
    {

        protected void BtnBatchDelete_Click(object sender, EventArgs e)
        {
            StringBuilder builder = new StringBuilder("");
            if (CollectionExclosion.Delete(this.EgvExclosion.SelectList.ToString()))
            {
                AdminPage.WriteSuccessMsg("<li>删除指定的采集排除成功！</li>", "ExclosionManage.aspx");
            }
            else
            {
                AdminPage.WriteErrMsg("<li>删除失败！</li>");
            }
        }

        protected void EgvExclosion_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                CollectionExclosionInfo dataItem = (CollectionExclosionInfo) e.Row.DataItem;
                Label label = e.Row.FindControl("LblExclosionType") as Label;
                switch (dataItem.ExclosionType)
                {
                    case 1:
                        label.Text = "文本";
                        return;

                    case 2:
                        label.Text = "时间";
                        return;

                    case 3:
                        label.Text = "数字";
                        return;

                    default:
                        return;
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if ((!base.IsPostBack && (BasePage.RequestString("Action") == "Delete")) && CollectionExclosion.Delete(BasePage.RequestInt32("ExclosionID")))
            {
                AdminPage.WriteSuccessMsg("<li>删除指定的采集排除成功！</li>", "ExclosionManage.aspx");
            }
        }
    }
}

