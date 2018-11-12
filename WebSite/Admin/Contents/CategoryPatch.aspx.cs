namespace EasyOne.WebSite.Admin.Contents
{
    using EasyOne.Contents;
    using EasyOne.Controls;
    using EasyOne.Enumerations;
    using EasyOne.Templates;
    using EasyOne.Web.UI;
    using System;
    using System.Web.UI.WebControls;

    public partial class CategoryPatch : AdminPage
    {

        protected void BtnCancel_Click(object sender, EventArgs e)
        {
            BasePage.ResponseRedirect("CategoryManage.aspx");
        }

        protected void EBtnPatch_Click(object sender, EventArgs e)
        {
            Nodes.PatchNodes();
            IncludeFile.CreateIncludeFileByAssociateType(AssociateType.Node);
            AdminPage.WriteSuccessMsg("<li>节点修复成功！</li>", "CategoryManage.aspx");
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

