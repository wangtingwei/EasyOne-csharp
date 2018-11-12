namespace EasyOne.WebSite.Admin.Accessories
{
    using EasyOne.Accessories;
    using EasyOne.Controls;
    using EasyOne.ExtendedControls;
    using EasyOne.Model.Accessories;
    using EasyOne.Web.UI;
    using System;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public partial class InsideLinkManage : AdminPage
    {


        protected void EBtnBatchDelete_Click(object sender, EventArgs e)
        {
            if (WordReplace.Delete(this.EgvInsideLink.SelectList.ToString()))
            {
                AdminPage.WriteSuccessMsg("<li>删除记录成功！</li>", "InsideLinkManage.aspx");
            }
            else
            {
                AdminPage.WriteErrMsg("<li>删除失败！</li>");
            }
        }

        protected void EBtnBatchDisable_Click(object sender, EventArgs e)
        {
            if (WordReplace.Disabled(this.EgvInsideLink.SelectList.ToString()))
            {
                AdminPage.WriteSuccessMsg("<li>禁用成功！</li>", "InsideLinkManage.aspx");
            }
            else
            {
                AdminPage.WriteErrMsg("<li>禁用失败！</li>");
            }
        }

        protected void EBtnBatchEnable_Click(object sender, EventArgs e)
        {
            if (WordReplace.Enabled(this.EgvInsideLink.SelectList.ToString()))
            {
                AdminPage.WriteSuccessMsg("<li>启用成功！</li>", "InsideLinkManage.aspx");
            }
            else
            {
                AdminPage.WriteErrMsg("<li>启用失败！</li>");
            }
        }

        protected void EgvInsideLink_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (((WordReplaceInfo) e.Row.DataItem).IsEnabled)
                {
                    ((HtmlAnchor) e.Row.FindControl("EahInsideLinkEnabled")).Visible = false;
                }
                else
                {
                    ((HtmlAnchor) e.Row.FindControl("EahInsideLinkDisable")).Visible = false;
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                string str = BasePage.RequestString("listType");
                string str2 = BasePage.RequestString("Action");
                if (str2 != null)
                {
                    if (!(str2 == "Delete"))
                    {
                        if (str2 == "disInsideLink")
                        {
                            WordReplace.Disabled(BasePage.RequestString("ItemID"));
                        }
                        else if (str2 == "runInsideLink")
                        {
                            WordReplace.Enabled(BasePage.RequestString("ItemID"));
                        }
                    }
                    else if (WordReplace.Delete(BasePage.RequestString("ItemID")))
                    {
                        AdminPage.WriteSuccessMsg("<li>删除指定的记录成功！</li>", "InsideLinkManage.aspx");
                    }
                }
                if (!string.IsNullOrEmpty(str))
                {
                    this.HdnlistType.Value = str;
                }
            }
        }
    }
}

