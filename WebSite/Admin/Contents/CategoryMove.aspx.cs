namespace EasyOne.WebSite.Admin.Contents
{
    using EasyOne.Common;
    using EasyOne.Contents;
    using EasyOne.Controls;
    using EasyOne.Enumerations;
    using EasyOne.Templates;
    using EasyOne.Web.UI;
    using System;
    using System.Web.UI.WebControls;

    public partial class CategoryMove : AdminPage
    {
        protected void BtnCancel_Click(object sender, EventArgs e)
        {
            BasePage.ResponseRedirect("CategoryManage.aspx");
        }

        protected void EBtnSubmit_Click(object sender, EventArgs e)
        {
            int nodeId = DataConverter.CLng(this.LstFromNodes.SelectedValue);
            int moveToNodeId = DataConverter.CLng(this.LstToNodes.SelectedValue);
            if (nodeId == 0)
            {
                AdminPage.WriteErrMsg("<li>指定的节点不存在或者已经被删除！</li>", "CategoryMove.aspx?NodeID=" + nodeId);
            }
            if (string.IsNullOrEmpty(this.LstFromNodes.SelectedValue.ToString()))
            {
                AdminPage.WriteErrMsg("<li>请先选择要移动的目标节点！</li>", "CategoryMove.aspx?NodeID=" + nodeId);
            }
            int errorNum = Nodes.NodesMove(nodeId, moveToNodeId);
            if (errorNum > 0)
            {
                AdminPage.WriteErrMsg("<li>" + Nodes.WriteMessageByErrorNum(errorNum) + "</li>", "CategoryMove.aspx?NodeID=" + nodeId);
            }
            else
            {
                IncludeFile.CreateIncludeFileByAssociateType(AssociateType.Node);
                base.Response.Write("<script type='text/javascript'>parent.frames[\"left\"].location.reload();</script>");
                AdminPage.WriteSuccessMsg("<li>节点移动成功！</li>", "CategoryManage.aspx");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                if (!Nodes.CheckRoleNodePurview(BasePage.RequestInt32("NodeID"), OperateCode.CurrentNodesManage))
                {
                    AdminPage.WriteErrMsg("<li>对不起，您没有当前栏目的管理权限！</li>");
                }
                int num = BasePage.RequestInt32("NodeID");
                this.LstFromNodes.DataSource = Nodes.GetNodeNameForItems();
                this.LstFromNodes.DataTextField = "NodeName";
                this.LstFromNodes.DataValueField = "NodeId";
                this.LstFromNodes.DataBind();
                this.LstToNodes.DataSource = Nodes.GetNodeNameForContainerItems();
                this.LstToNodes.DataTextField = "NodeName";
                this.LstToNodes.DataValueField = "NodeId";
                this.LstToNodes.DataBind();
                this.LstFromNodes.SelectedIndex = BasePage.GetSelectedIndexByValue(this.LstFromNodes, num.ToString());
            }
        }
    }
}

