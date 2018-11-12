namespace EasyOne.WebSite.Admin.Collection
{
    using EasyOne.Collection;
    using EasyOne.Controls;
    using EasyOne.Web.UI;
    using System;
    using System.Text;
    using System.Web.UI.WebControls;

    public partial class ItemManage : AdminPage
    {

        protected void BtnBatchDelete_Click(object sender, EventArgs e)
        {
            StringBuilder selectList = new StringBuilder("");
            selectList = this.EgvItemRules.SelectList;
            if (selectList.Length > 0)
            {
                if (CollectionItem.Delete(selectList.ToString()))
                {
                    AdminPage.WriteSuccessMsg("<li>删除指定的采集项目成功！</li>", "ItemManage.aspx");
                }
                else
                {
                    AdminPage.WriteErrMsg("<li>删除失败！</li>");
                }
            }
            else
            {
                AdminPage.WriteErrMsg("<li>请选择要删除的采集项目！</li>");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            string str;
            if (!base.IsPostBack && ((str = BasePage.RequestString("Action")) != null))
            {
                if (!(str == "Delete"))
                {
                    if (!(str == "Copy"))
                    {
                        if (str == "Detection")
                        {
                            int itemId = BasePage.RequestInt32("ItemID");
                            CollectionProgress progress = new CollectionProgress();
                            progress.Detection(itemId);
                            if (!string.IsNullOrEmpty(progress.ErrorInfo))
                            {
                                CollectionItem.Disabled(itemId);
                                AdminPage.WriteErrMsg(progress.ErrorInfo);
                                return;
                            }
                            AdminPage.WriteSuccessMsg("<li>测试采集项目成功！</li>", "ItemManage.aspx");
                        }
                        return;
                    }
                }
                else
                {
                    if (CollectionItem.Delete(BasePage.RequestInt32("ItemID")))
                    {
                        AdminPage.WriteSuccessMsg("<li>删除指定的采集项目成功！</li>", "ItemManage.aspx");
                    }
                    return;
                }
                if (CollectionItem.Copy(BasePage.RequestInt32("ItemID")))
                {
                    AdminPage.WriteSuccessMsg("<li>复制指定的采集项目成功！</li>", "ItemManage.aspx");
                }
            }
        }
    }
}

