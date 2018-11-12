namespace EasyOne.WebSite.Admin.Contents
{
    using EasyOne.AccessManage;
    using EasyOne.Common;
    using EasyOne.Components;
    using EasyOne.Contents;
    using EasyOne.Controls;
    using EasyOne.Enumerations;
    using EasyOne.Model.Contents;
    using EasyOne.Templates;
    using EasyOne.Web.UI;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Web.UI.WebControls;

    public partial class OutLink : AdminPage
    {
        private string m_arrCurrentNodesManage;

        private void BindDropParentNode()
        {
            if (!this.Page.IsPostBack)
            {
                IList<NodeInfo> nodeNameForContainerItems = Nodes.GetNodeNameForContainerItems();
                this.DropParentNode.DataSource = nodeNameForContainerItems;
                this.DropParentNode.DataTextField = "NodeName";
                this.DropParentNode.DataValueField = "NodeId";
                this.DropParentNode.DataBind();
            }
        }

        private void BindNodesInfo(int nodeId)
        {
            NodeInfo cacheNodeById = Nodes.GetCacheNodeById(nodeId);
            if (cacheNodeById.IsNull)
            {
                AdminPage.WriteErrMsg("发生错误，请传入正确的NodeID！");
            }
            if (cacheNodeById.Settings == null)
            {
                AdminPage.WriteErrMsg("发生错误！");
            }
            this.SmpNavigator.CurrentNode = "修改单页设置：<span style=\"color: Red\">" + cacheNodeById.NodeName + "</span>";
            this.DropParentNode.Visible = false;
            this.LblNodeName.Text = Nodes.ShowParentNodesNavigation(nodeId);
            this.TxtNodeName.Text = cacheNodeById.NodeName;
            this.TxtNodeIdentifier.Text = cacheNodeById.NodeIdentifier;
            this.TxtNodePicUrl.Text = cacheNodeById.NodePicUrl;
            this.TxtTips.Text = cacheNodeById.Tips;
            this.RadlShowOnPath.SelectedValue = cacheNodeById.ShowOnMenu.ToString();
            BasePage.SetSelectedIndexByValue(this.RadlOpenType, cacheNodeById.OpenType.ToString());
            this.TxtLinkUrl.Text = cacheNodeById.LinkUrl;
        }

        private void CheckPermissions(string action)
        {
            if (!PEContext.Current.Admin.IsSuperAdmin)
            {
                string roleNodeId = RolePermissions.GetRoleNodeId(PEContext.Current.Admin.Roles, OperateCode.ChildNodesManage);
                if (roleNodeId.IndexOf("-1", StringComparison.Ordinal) >= 0)
                {
                    this.m_arrCurrentNodesManage = "-1";
                }
                else
                {
                    if (roleNodeId.IndexOf(',') > 0)
                    {
                        string[] strArray = roleNodeId.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                        StringBuilder builder = new StringBuilder();
                        foreach (string str2 in strArray)
                        {
                            if (builder.Length == 0)
                            {
                                builder.Append(CountRoleNodePermissionsId(str2));
                            }
                            else
                            {
                                builder.Append("," + CountRoleNodePermissionsId(str2));
                            }
                        }
                        roleNodeId = builder.ToString();
                    }
                    else
                    {
                        roleNodeId = CountRoleNodePermissionsId(roleNodeId);
                    }
                    this.m_arrCurrentNodesManage = roleNodeId;
                    foreach (ListItem item in this.DropParentNode.Items)
                    {
                        if (!StringHelper.FoundCharInArr(this.m_arrCurrentNodesManage, item.Value))
                        {
                            item.Attributes.CssStyle.Add("background", "red");
                        }
                    }
                }
                if (action == "add")
                {
                    this.LblNodePermissions.Text = "注意：<span style='color:red;'>红色</span>的栏目表示没有权限";
                }
            }
        }

        private static string CountRoleNodePermissionsId(string iChildNodesManage)
        {
            NodeInfo nodeById = Nodes.GetNodeById(DataConverter.CLng(iChildNodesManage));
            if (nodeById.IsNull)
            {
                return "";
            }
            string arrChildId = nodeById.NodeId.ToString();
            if (nodeById.ArrChildId.IndexOf(",", StringComparison.Ordinal) > 0)
            {
                arrChildId = nodeById.ArrChildId;
            }
            return arrChildId;
        }

        protected void EBtnSubmit_Click(object sender, EventArgs e)
        {
            if (this.Page.IsValid)
            {
                if ((!PEContext.Current.Admin.IsSuperAdmin && (this.m_arrCurrentNodesManage != "-1")) && !StringHelper.FoundCharInArr(this.m_arrCurrentNodesManage, this.DropParentNode.SelectedValue))
                {
                    AdminPage.WriteErrMsg("<li>对不起，您没有保存指定栏目的权限！</li>");
                }
                NodeInfo nodesInfo = this.GetNodesInfo();
                nodesInfo.NodeId = BasePage.RequestInt32("NodeID");
                if (BasePage.RequestStringToLower("Action", "add") == "add")
                {
                    nodesInfo.ParentId = DataConverter.CLng(this.DropParentNode.SelectedValue);
                    nodesInfo.NodeId = Nodes.GetMaxNodeId() + 1;
                    switch (Nodes.Add(nodesInfo))
                    {
                        case 0:
                            AdminPage.WriteErrMsg("添加外部链接失败！");
                            return;

                        case 1:
                            IncludeFile.CreateIncludeFileByAssociateType(AssociateType.Node);
                            base.Response.Write("<script type='text/javascript'>parent.frames[\"left\"].location.reload();</script>");
                            AdminPage.WriteSuccessMsg("添加外部链接成功！", "OutLink.aspx?Action=Modify&NodeID=" + nodesInfo.NodeId);
                            return;

                        case 2:
                            AdminPage.WriteErrMsg("节点名称已经存在！");
                            return;

                        case 3:
                            AdminPage.WriteErrMsg("节点标识符已经存在！");
                            return;

                        case 4:
                            AdminPage.WriteErrMsg("节点目录名已经存在！");
                            return;

                        case 5:
                            AdminPage.WriteErrMsg("父节点必须为栏目节点！");
                            return;

                        case 6:
                            AdminPage.WriteErrMsg("父节点不存在！");
                            return;
                    }
                    AdminPage.WriteErrMsg("未知错误！");
                }
                else
                {
                    switch (Nodes.Update(nodesInfo))
                    {
                        case 0:
                            AdminPage.WriteErrMsg("修改外部链接失败！");
                            return;

                        case 1:
                            IncludeFile.CreateIncludeFileByAssociateType(AssociateType.Node);
                            base.Response.Write("<script type='text/javascript'>parent.frames[\"left\"].location.reload();</script>");
                            AdminPage.WriteSuccessMsg("修改外部链接成功！", "OutLink.aspx?Action=Modify&NodeID=" + nodesInfo.NodeId);
                            return;

                        case 2:
                            AdminPage.WriteErrMsg("节点名称已经存在！");
                            return;

                        case 3:
                            AdminPage.WriteErrMsg("节点标识符已经存在！");
                            return;
                    }
                    AdminPage.WriteErrMsg("未知错误！");
                }
            }
        }

        private NodeInfo GetNodesInfo()
        {
            NodeInfo nodeById = Nodes.GetNodeById(BasePage.RequestInt32("NodeId", 0));
            NodeSettingInfo info2 = new NodeSettingInfo();
            nodeById.NodeName = this.TxtNodeName.Text;
            nodeById.NodeIdentifier = this.TxtNodeIdentifier.Text;
            nodeById.NodeType = NodeType.Link;
            nodeById.NodePicUrl = this.TxtNodePicUrl.Text;
            nodeById.Tips = this.TxtTips.Text;
            nodeById.ShowOnMenu = DataConverter.CBoolean(this.RadlShowOnPath.SelectedValue);
            nodeById.OpenType = DataConverter.CLng(this.RadlOpenType.SelectedValue);
            nodeById.LinkUrl = this.TxtLinkUrl.Text;
            nodeById.Settings = info2;
            nodeById.RootId = 0;
            return nodeById;
        }

        private void InitialText()
        {
            string str2;
            int nodeId = BasePage.RequestInt32("NodeID");
            string action = BasePage.RequestStringToLower("Action", "add");
            if (((str2 = action) != null) && (str2 == "modify"))
            {
                this.LblTitle.Text = "修改外部链接";
                this.SmpNavigator.CurrentNode = "修改外部链接设置：";
                if (!this.Page.IsPostBack)
                {
                    this.BindNodesInfo(nodeId);
                }
            }
            else
            {
                this.SmpNavigator.CurrentNode = "添加外部链接";
                this.LblNodeName.Visible = false;
                this.BindDropParentNode();
            }
            this.CheckPermissions(action);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Nodes.CheckRoleNodePurview(BasePage.RequestInt32("NodeID"), OperateCode.CurrentNodesManage))
            {
                AdminPage.WriteErrMsg("<li>对不起，您没有当前栏目的管理权限！</li>");
            }
            this.InitialText();
        }
    }
}

