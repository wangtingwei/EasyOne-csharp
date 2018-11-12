namespace EasyOne.WebSite.Admin.Template
{
    using EasyOne.Controls;
    using EasyOne.Templates;
    using EasyOne.Web.UI;
    using System;
    using System.Text;
    using System.Web.UI.WebControls;

    public partial class PagerManageUI : AdminPage
    {

        protected void BtnAdd_Click(object sender, EventArgs e)
        {
            BasePage.ResponseRedirect("Pager.aspx");
        }

        protected void BtnDel_Click(object sender, EventArgs e)
        {
            StringBuilder selectList = this.GdvPagerList.SelectList;
            if (selectList.Length == 0)
            {
                AdminPage.WriteErrMsg("<li>对不起，您还没选择要删除的标签！</li>");
            }
            else if (PagerManage.Delete(selectList.ToString()))
            {
                this.GdvPagerList.DataBind();
                BasePage.ResponseRedirect("PagerManage.aspx");
            }
            else
            {
                AdminPage.WriteErrMsg("删除失败！", "PagerManage.aspx");
            }
        }

        protected void GdvPagerList_RowCommand(object sender, CommandEventArgs e)
        {
            bool flag = false;
            string commandName = e.CommandName;
            if (commandName != null)
            {
                if (!(commandName == "Deleted"))
                {
                    if (commandName == "Copy")
                    {
                        flag = PagerManage.Copy(e.CommandArgument.ToString());
                        goto Label_0050;
                    }
                }
                else
                {
                    flag = PagerManage.Delete(e.CommandArgument.ToString());
                    goto Label_0050;
                }
            }
            flag = false;
        Label_0050:
            if (flag)
            {
                this.GdvPagerList.DataBind();
                BasePage.ResponseRedirect("PagerManage.aspx");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(BasePage.RequestString("SearchType")) && !string.IsNullOrEmpty(BasePage.RequestString("keyword")))
            {
                this.OdsPager.SelectParameters.Clear();
                this.OdsPager.SelectParameters.Add("searchType", TypeCode.String, BasePage.RequestInt32("SearchType").ToString());
                this.OdsPager.SelectParameters.Add("keyword", TypeCode.String, BasePage.RequestString("keyword").ToString());
                this.OdsPager.DataBind();
            }
        }
    }
}

