namespace EasyOne.WebSite.Admin.Accessories
{
    using EasyOne.Accessories;
    using EasyOne.Controls;
    using EasyOne.Model.Accessories;
    using EasyOne.Web.UI;
    using System;
    using System.Text;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public partial class WordFilterManage : AdminPage
    {

        protected void EBtnBatchDelete_Click(object sender, EventArgs e)
        {
            StringBuilder builder = new StringBuilder("");
            if (WordReplace.Delete(this.EgvWordFilter.SelectList.ToString()))
            {
                AdminPage.WriteSuccessMsg("<li>删除指定的记录成功！</li>", "WordFilterManage.aspx");
            }
            else
            {
                AdminPage.WriteErrMsg("<li>删除失败！</li>");
            }
        }

        protected void EBtnBatchDisable_Click(object sender, EventArgs e)
        {
            StringBuilder builder = new StringBuilder();
            if (WordReplace.Disabled(this.EgvWordFilter.SelectList.ToString()))
            {
                AdminPage.WriteSuccessMsg("<li>禁用操作成功！</li>", "WordFilterManage.aspx");
            }
            else
            {
                AdminPage.WriteErrMsg("<li>禁用失败！</li>");
            }
        }

        protected void EBtnBatchEnable_Click(object sender, EventArgs e)
        {
            StringBuilder builder = new StringBuilder("");
            if (WordReplace.Enabled(this.EgvWordFilter.SelectList.ToString()))
            {
                AdminPage.WriteSuccessMsg("<li>启用操作成功！</li>", "WordFilterManage.aspx");
            }
            else
            {
                AdminPage.WriteErrMsg("<li>启用失败！</li>");
            }
        }

        protected void EgvWordFilter_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (((WordReplaceInfo) e.Row.DataItem).IsEnabled)
                {
                    ((HtmlAnchor) e.Row.FindControl("EahWordFilterEnabled")).Visible = false;
                }
                else
                {
                    ((HtmlAnchor) e.Row.FindControl("EahWordFilterDisable")).Visible = false;
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                BasePage.RequestInt32("ListType");
                BasePage.RequestString("Keyword");
                string str = BasePage.RequestString("Action");
                if (str != null)
                {
                    if (!(str == "Delete"))
                    {
                        if (!(str == "disWordFilter"))
                        {
                            if (str == "runWordFilter")
                            {
                                WordReplace.Enabled(BasePage.RequestString("ItemID"));
                            }
                            return;
                        }
                    }
                    else
                    {
                        if (WordReplace.Delete(BasePage.RequestString("ItemID")))
                        {
                            AdminPage.WriteSuccessMsg("<li>删除指定的记录成功！</li>", "WordFilterManage.aspx");
                        }
                        return;
                    }
                    WordReplace.Disabled(BasePage.RequestString("ItemID"));
                }
            }
        }
    }
}

