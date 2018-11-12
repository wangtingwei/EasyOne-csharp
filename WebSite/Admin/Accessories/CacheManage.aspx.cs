namespace EasyOne.WebSite.Admin.Accessories
{
    using EasyOne.Components;
    using EasyOne.Controls;
    using EasyOne.Web.UI;
    using System;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public partial class CacheManage : AdminPage
    {

        protected void BtnDelete_Click(object sender, EventArgs e)
        {
            SiteCache.Clear();
            AdminPage.WriteSuccessMsg("清除所有缓存成功！", "CacheManage.aspx");
        }

        protected void BtnDeleteLabel_Click(object sender, EventArgs e)
        {
            SiteCache.RemoveByPattern(@"CK_Label_\S*");
            AdminPage.WriteSuccessMsg("清除模板标签缓存成功！", "CacheManage.aspx");
        }

        protected void BtnDeleteModel_Click(object sender, EventArgs e)
        {
            SiteCache.RemoveByPattern(@"CK_CommonModel_\S*");
            AdminPage.WriteSuccessMsg("清除模型缓存成功！", "CacheManage.aspx");
        }

        protected void BtnDeleteNode_Click(object sender, EventArgs e)
        {
            SiteCache.RemoveByPattern(@"CK_Content_NodeInfo_\S*");
            AdminPage.WriteSuccessMsg("清除栏目缓存成功！", "CacheManage.aspx");
        }

        protected void BtnDeletePageCategory_Click(object sender, EventArgs e)
        {
            SiteCache.RemoveByPattern(@"CK_Page_Category_\S*");
            AdminPage.WriteSuccessMsg("清除节点页缓存成功！", "CacheManage.aspx");
        }

        protected void EgvCache_RowCommand(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "Del")
            {
                string str = e.CommandArgument.ToString();
                if (!string.IsNullOrEmpty(str))
                {
                    SiteCache.Remove(str);
                    AdminPage.WriteSuccessMsg("清除缓存成功！", "CacheManage.aspx");
                }
            }
        }

        protected void EgvCache_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label label = e.Row.FindControl("LblCacheKey") as Label;
                string resourceKey = DataBinder.Eval(e.Row.DataItem, "CacheName").ToString();
                label.Text = (resourceKey.Length <= 30) ? resourceKey : (resourceKey.Substring(0, 30) + "...");
                label.ToolTip = resourceKey;
                e.Row.Cells[2].Text = BasePage.GetGlobalCacheString(resourceKey);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }
    }
}

