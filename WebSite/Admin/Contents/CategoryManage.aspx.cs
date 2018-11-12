namespace EasyOne.WebSite.Admin.Contents
{
    using EasyOne.Contents;
    using EasyOne.Controls;
    using EasyOne.Enumerations;
    using EasyOne.Templates;
    using EasyOne.Web.UI;
    using System;
    using System.Web.UI.WebControls;

    public partial class CategoryManage : AdminPage
    {

        private void DeleteNode()
        {
            if (BasePage.RequestInt32("NodeID") == -2)
            {
                AdminPage.WriteErrMsg("首页节点不允许删除！", "CategoryManage.aspx");
            }
            switch (Nodes.Delete(BasePage.RequestInt32("NodeID")))
            {
                case 0:
                    AdminPage.WriteErrMsg("删除节点失败！", "CategoryManage.aspx");
                    return;

                case 1:
                    IncludeFile.CreateIncludeFileByAssociateType(AssociateType.Node);
                    base.Response.Write("<script type='text/javascript'>parent.frames[\"left\"].location.reload();</script>");
                    AdminPage.WriteSuccessMsg("删除节点成功！请记得重新生成相关节点的文件呀！", "CategoryManage.aspx");
                    return;

                case 2:
                    AdminPage.WriteErrMsg("节点不存在，或者已经被删除！", "CategoryManage.aspx");
                    return;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                if (BasePage.RequestString("Action") == "ResetChildNodes")
                {
                    this.ResetChildNodes();
                }
                if (BasePage.RequestString("Action") == "Delete")
                {
                    this.DeleteNode();
                }
                if (string.Compare("clear", BasePage.RequestString("Action"), StringComparison.OrdinalIgnoreCase) == 0)
                {
                    ContentManage.UpdateStatus(ContentManage.GetGeneralIdArrByNodeId(Nodes.GetCacheNodeById(BasePage.RequestInt32("NodeId")).ArrChildId, -4), -3);
                }
            }
        }

        private void ResetChildNodes()
        {
            int num = 0;
            num = Nodes.ResetChildNodes(BasePage.RequestInt32("NodeID"));
            if (num > 0)
            {
                if (num == 1)
                {
                    AdminPage.WriteErrMsg("<li>请选择你要复位的一级节点，一级节点以下的节点不能进行复位操作！</li>", "CategoryManage.aspx");
                }
                else
                {
                    AdminPage.WriteErrMsg("<li>复位节点失败！</li>", "CategoryManage.aspx");
                }
            }
            else
            {
                IncludeFile.CreateIncludeFileByAssociateType(AssociateType.Node);
                base.Response.Write("<script type='text/javascript'>parent.frames[\"left\"].location.reload();</script>");
                AdminPage.WriteSuccessMsg("<li>复位节点成功！</li>", "CategoryManage.aspx");
            }
        }
    }
}

