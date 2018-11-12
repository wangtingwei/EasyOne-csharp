namespace EasyOne.WebSite.Admin.Contents
{
    using EasyOne.Common;
    using EasyOne.Contents;
    using EasyOne.Controls;
    using EasyOne.Enumerations;
    using EasyOne.Model.Contents;
    using EasyOne.Templates;
    using EasyOne.Web.UI;
    using System;
    using System.Collections.Generic;
    using System.Web.UI.WebControls;

    public partial class CategoryOrder : AdminPage
    {
        private string action;
        private bool m_IsRootOrder;
        private int m_NodeId;

        protected void BtnBack_Click(object sender, EventArgs e)
        {
            BasePage.ResponseRedirect("CategoryOrder.aspx?Action=" + this.action + "NodeId=" + this.m_NodeId.ToString());
        }

        protected void EBtnSetOrderId_Click(object sender, EventArgs e)
        {
            string str = this.HdnSerNodeList.Value;
            if (!string.IsNullOrEmpty(str))
            {
                List<NodeInfo> nodeList = new List<NodeInfo>();
                string[] strArray = str.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < strArray.Length; i++)
                {
                    NodeInfo cacheNodeById = Nodes.GetCacheNodeById(DataConverter.CLng(strArray[i]));
                    if (this.m_IsRootOrder)
                    {
                        cacheNodeById.RootId = i + 1;
                        cacheNodeById.OrderType = 1;
                    }
                    else
                    {
                        cacheNodeById.OrderId = i + 1;
                        cacheNodeById.OrderType = 0;
                    }
                    if (!cacheNodeById.IsNull)
                    {
                        nodeList.Add(cacheNodeById);
                    }
                }
                Nodes.OrderNode(nodeList);
                this.Repeater1.DataBind();
                IncludeFile.CreateIncludeFileByAssociateType(AssociateType.Node);
                base.Response.Write("<script type='text/javascript'>parent.frames[\"left\"].location.reload();</script>");
                AdminPage.WriteSuccessMsg("栏目排序成功！", "CategoryOrder.aspx");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.m_NodeId = BasePage.RequestInt32("NodeId");
            this.action = BasePage.RequestString("Action");
            if (!Nodes.CheckRoleNodePurview(this.m_NodeId, OperateCode.CurrentNodesManage))
            {
                AdminPage.WriteErrMsg("<li>对不起，您没有当前栏目的管理权限！</li>");
            }
            if (string.IsNullOrEmpty(this.action))
            {
                this.m_IsRootOrder = true;
            }
        }
    }
}

