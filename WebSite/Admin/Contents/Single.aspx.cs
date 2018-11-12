namespace EasyOne.WebSite.Admin.Contents
{
    using EasyOne.AccessManage;
    using EasyOne.Common;
    using EasyOne.Components;
    using EasyOne.Contents;
    using EasyOne.Controls;
    using EasyOne.Enumerations;
    using EasyOne.Model.Contents;
    using EasyOne.ModelControls;
    using EasyOne.Templates;
    using EasyOne.Web.UI;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public partial class Single : AdminPage
    {
        private string m_arrCurrentNodesManage;

        private void BindDropParentNode()
        {
            if (!this.Page.IsPostBack)
            {
                IList<NodeInfo> nodeNameForContainerItems = Nodes.GetNodeNameForContainerItems();
                this.DropParentNode.DataSource = nodeNameForContainerItems;
                this.DropParentNode.DataBind();
                BasePage.SetSelectedIndexByValue(this.DropParentNode, BasePage.RequestInt32("NodeID").ToString());
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
            this.TxtDescription.Text = cacheNodeById.Description;
            this.TxtMetaDescription.Text = cacheNodeById.MetaDescription;
            this.TxtMetaKeywords.Text = cacheNodeById.MetaKeywords;
            this.RadlShowOnMenu.SelectedValue = cacheNodeById.ShowOnMenu.ToString();
            this.FileCdefaultListTmeplate.Text = cacheNodeById.DefaultTemplateFile;
            switch (cacheNodeById.OpenType)
            {
                case 0:
                    this.RadOpenType0.Checked = true;
                    break;

                case 1:
                    this.RadOpenType1.Checked = true;
                    break;
            }
            BasePage.SetSelectedIndexByValue(this.RadlIsCreate, cacheNodeById.IsCreateListPage.ToString());
            string parentDir = "";
            if (!string.IsNullOrEmpty(cacheNodeById.ParentDir))
            {
                parentDir = cacheNodeById.ParentDir;
            }
            else if (!string.IsNullOrEmpty(cacheNodeById.NodeDir))
            {
                parentDir = parentDir + cacheNodeById.NodeDir;
            }
            else
            {
                parentDir = "/";
            }
            this.LblPageHtmlDir.Text = parentDir;
            this.PagePostfix.Value = cacheNodeById.ListPagePostfix;
            this.TxtPageHtmlDir.Text = cacheNodeById.ListPageHtmlRule;
            if (nodeId == -2)
            {
                if (string.IsNullOrEmpty(cacheNodeById.ListPageHtmlRule) || (string.Compare(cacheNodeById.ListPageHtmlRule, "Index", StringComparison.OrdinalIgnoreCase) != 0))
                {
                    this.TxtPageHtmlDir.Text = "Index";
                }
                this.TxtPageHtmlDir.Enabled = false;
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

        private void DetectionPermissions(string action)
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
                    nodesInfo.NodeId = Nodes.GetMaxNodeId() + 1;
                    switch (Nodes.Add(nodesInfo))
                    {
                        case 0:
                            AdminPage.WriteErrMsg("添加单页失败！");
                            return;

                        case 1:
                            IncludeFile.CreateIncludeFileByAssociateType(AssociateType.Node);
                            base.Response.Write("<script type='text/javascript'>parent.frames[\"left\"].location.reload();</script>");
                            AdminPage.WriteSuccessMsg("添加单页成功！", "Single.aspx?Action=Modify&NodeID=" + nodesInfo.NodeId);
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
                            AdminPage.WriteErrMsg("父节点必须为容器节点！");
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
                            AdminPage.WriteErrMsg("修改单页失败！");
                            return;

                        case 1:
                            IncludeFile.CreateIncludeFileByAssociateType(AssociateType.Node);
                            base.Response.Write("<script type='text/javascript'>parent.frames[\"left\"].location.reload();</script>");
                            AdminPage.WriteSuccessMsg("修改单页成功！", "Single.aspx?Action=Modify&NodeID=" + nodesInfo.NodeId);
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
            NodeInfo nodeById = null;
            NodeSettingInfo settings = null;
            string str2 = BasePage.RequestStringToLower("Action", "add");
            if (str2 != null)
            {
                if (!(str2 == "modify"))
                {
                    if (str2 == "add")
                    {
                        nodeById = new NodeInfo();
                        settings = new NodeSettingInfo();
                        nodeById.ParentId = DataConverter.CLng(this.DropParentNode.SelectedValue);
                        nodeById.RootId = 0;
                    }
                }
                else
                {
                    nodeById = Nodes.GetNodeById(BasePage.RequestInt32("NodeID", 0));
                    if (nodeById.IsNull)
                    {
                        settings = new NodeSettingInfo();
                    }
                    else
                    {
                        settings = nodeById.Settings;
                    }
                }
            }
            nodeById.NodeName = this.TxtNodeName.Text;
            nodeById.NodeIdentifier = this.TxtNodeIdentifier.Text;
            nodeById.NodeType = NodeType.Single;
            nodeById.ListPageSavePathType = ListPagePathType.SinglePath;
            nodeById.NodePicUrl = this.TxtNodePicUrl.Text;
            nodeById.Tips = this.TxtTips.Text;
            nodeById.Description = this.TxtDescription.Text;
            nodeById.MetaDescription = this.TxtMetaDescription.Text;
            nodeById.MetaKeywords = this.TxtMetaKeywords.Text;
            nodeById.ShowOnMenu = Convert.ToBoolean(this.RadlShowOnMenu.SelectedValue);
            nodeById.DefaultTemplateFile = this.FileCdefaultListTmeplate.Text;
            if (this.RadOpenType0.Checked)
            {
                nodeById.OpenType = 0;
            }
            if (this.RadOpenType1.Checked)
            {
                nodeById.OpenType = 1;
            }
            nodeById.IsCreateListPage = DataConverter.CBoolean(this.RadlIsCreate.SelectedValue);
            nodeById.ListPageHtmlRule = this.TxtPageHtmlDir.Text;
            nodeById.ListPagePostfix = this.PagePostfix.Value;
            nodeById.Settings = settings;
            return nodeById;
        }

        private void InitialText()
        {
            int nodeId = BasePage.RequestInt32("NodeID");
            string action = BasePage.RequestStringToLower("Action", "add");
            this.ViewState["action"] = action;
            string str2 = action;
            if (str2 != null)
            {
                if (!(str2 == "add"))
                {
                    if (str2 == "modify")
                    {
                        this.LblTitle.Text = "修改单页";
                        this.SmpNavigator.CurrentNode = "修改单页设置：";
                        this.TrNodeId.Visible = true;
                        this.LitNodeId.Text = nodeId.ToString();
                        if (!this.Page.IsPostBack)
                        {
                            this.BindNodesInfo(nodeId);
                        }
                        goto Label_00DA;
                    }
                }
                else
                {
                    this.LblNodeName.Visible = false;
                    this.BindDropParentNode();
                    goto Label_00DA;
                }
            }
            this.SmpNavigator.CurrentNode = "添加单页";
            this.LblNodeName.Visible = false;
            this.BindDropParentNode();
        Label_00DA:
            this.DetectionPermissions(action);
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

