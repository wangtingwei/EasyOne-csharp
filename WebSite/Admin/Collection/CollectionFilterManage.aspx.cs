namespace EasyOne.WebSite.Admin.Collection
{
    using EasyOne.Collection;
    using EasyOne.Controls;
    using EasyOne.Web.UI;
    using System;
    using System.Text;
    using System.Web.UI.WebControls;

    public partial class CollectionFilterManage : AdminPage
    {

        protected void BtnBatchDelete_Click(object sender, EventArgs e)
        {
            StringBuilder builder = new StringBuilder("");
            if (CollectionFilterRules.Delete(this.EgvFilterRules.SelectList.ToString()))
            {
                AdminPage.WriteSuccessMsg("<li>删除指定的采集过滤成功！</li>", "CollectionFilterManage.aspx");
            }
            else
            {
                AdminPage.WriteErrMsg("<li>删除失败！</li>");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if ((!base.IsPostBack && (BasePage.RequestString("Action") == "Delete")) && CollectionFilterRules.Delete(BasePage.RequestInt32("FilterRuleID")))
            {
                AdminPage.WriteSuccessMsg("<li>删除指定的采集过滤成功！</li>", "CollectionFilterManage.aspx");
            }
        }
    }
}

