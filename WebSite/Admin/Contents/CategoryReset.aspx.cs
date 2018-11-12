namespace EasyOne.WebSite.Admin.Contents
{
    using EasyOne.Contents;
    using EasyOne.Controls;
    using EasyOne.Enumerations;
    using EasyOne.Templates;
    using EasyOne.Web.UI;
    using System;
    using System.Web.UI.WebControls;

    public partial class CategoryReset : AdminPage
    {

        protected void BtnCancel_Click(object sender, EventArgs e)
        {
            BasePage.ResponseRedirect("CategoryManage.aspx");
        }

        protected void EBtnReset_Click(object sender, EventArgs e)
        {
            Nodes.ResetNodes();
            IncludeFile.CreateIncludeFileByAssociateType(AssociateType.Node);
            base.Response.Write("<script type='text/javascript'>parent.frames[\"left\"].location.reload();</script>");
            AdminPage.WriteSuccessMsg("<li>复位节点成功！</li>", "CategoryManage.aspx");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Nodes.CheckRoleNodePurview(BasePage.RequestInt32("NodeID"), OperateCode.CurrentNodesManage))
            {
                AdminPage.WriteErrMsg("<li>对不起，您没有当前栏目的管理权限！</li>");
            }
        }
    }
}

