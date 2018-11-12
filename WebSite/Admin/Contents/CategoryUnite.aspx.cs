namespace EasyOne.WebSite.Admin.Contents
{
    using EasyOne.Common;
    using EasyOne.Contents;
    using EasyOne.Controls;
    using EasyOne.Enumerations;
    using EasyOne.Templates;
    using EasyOne.Web.UI;
    using System;
    using System.Collections.Generic;
    using System.Web.UI.WebControls;
    using EasyOne.Model.Contents;

    public partial class CategoryUnite : AdminPage
    {

        protected void BtnCancel_Click(object sender, EventArgs e)
        {
            BasePage.ResponseRedirect("CategoryManage.aspx");
        }

        protected void EBtnUnite_Click(object sender, EventArgs e)
        {
            int nodeId = DataConverter.CLng(this.DropFromNode.SelectedValue);
            int targetNodeId = DataConverter.CLng(this.DropToNode.SelectedValue);
            if (nodeId == 0)
            {
                AdminPage.WriteErrMsg("<li>指定要合并的节点不存在或者已经被删除！</li>", "CategoryUnite.aspx");
            }
            if (targetNodeId == 0)
            {
                AdminPage.WriteErrMsg("<li>指定的目标节点不存在或者已经被删除！</li>", "CategoryUnite.aspx");
            }
            int errorType = Nodes.NodesUnite(nodeId, targetNodeId);
            if (errorType > 0)
            {
                AdminPage.WriteErrMsg("<li>" + Nodes.WriteNodesUniteMessage(errorType) + "</li>", "CategoryManage.aspx");
            }
            else
            {
                IncludeFile.CreateIncludeFileByAssociateType(AssociateType.Node);
                base.Response.Write("<script type='text/javascript'>parent.frames[\"left\"].location.reload();</script>");
                AdminPage.WriteSuccessMsg("<li>节点合并成功！已经将被合并节点及其下属子节点的所有数据转入目标节点中。</li><li>同时删除了被合并的节点及其子节点。</li><li>请重新生成目标节点的所有内容！。</li>", "CategoryManage.aspx");
            }
        }

        private void Initial()
        {
            if (!this.Page.IsPostBack)
            {
                IList<NodeInfo> nodeNameForContainerItems = Nodes.GetNodeNameForContainerItems();
                this.DropFromNode.DataSource = nodeNameForContainerItems;
                this.DropFromNode.DataBind();
                this.DropToNode.DataSource = nodeNameForContainerItems;
                this.DropToNode.DataBind();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Nodes.CheckRoleNodePurview(BasePage.RequestInt32("NodeID"), OperateCode.CurrentNodesManage))
            {
                AdminPage.WriteErrMsg("<li>对不起，您没有当前栏目的管理权限！</li>");
            }
            this.Initial();
        }
    }
}

