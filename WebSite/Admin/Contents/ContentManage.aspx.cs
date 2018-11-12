namespace EasyOne.WebSite.Admin.Contents
{
    using EasyOne.AccessManage;
    using EasyOne.Common;
    using EasyOne.CommonModel;
    using EasyOne.Components;
    using EasyOne.Contents;
    using EasyOne.Controls;
    using EasyOne.Enumerations;
    using EasyOne.ExtendedControls;
    using EasyOne.Model.CommonModel;
    using EasyOne.Model.Contents;
    using EasyOne.Model.WorkFlow;
    using EasyOne.ModelControls;
    using EasyOne.Web.UI;
    using EasyOne.WebSite.Controls;
    using EasyOne.WorkFlows;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public partial class ContentManageUI : AdminPage
    {
        private bool m_Administrator;
        private string m_arrNodeIdCheck = "";
        private string m_arrNodeIdManage = "";
        private string m_arrNodeIdShow = "";
        private bool m_isCheck;
        private bool m_isManage;
        protected Dictionary<int, string> m_ModelPreviewDictionary = new Dictionary<int, string>();
        private int m_NodeId;
        protected Dictionary<int, NodeInfo> m_NodeInfoDictionary = new Dictionary<int, NodeInfo>();
        protected Dictionary<int, string> m_StatusDictionary = new Dictionary<int, string>();

        protected void BatchNodeSet_Click(object sender, EventArgs e)
        {
            this.PermissionDetection(this.m_isManage);
            StringBuilder selectList = this.EgvContent.SelectList;
            if (selectList.Length == 0)
            {
                AdminPage.WriteErrMsg("<li>对不起，您还没选择要添加的项目！</li>");
            }
            else
            {
                BasePage.ResponseRedirect("AddContentToNode.aspx?Action=Content&Id=" + selectList.ToString());
            }
        }

        protected void BtnArchiving_Click(object sender, EventArgs e)
        {
            this.PermissionDetection(this.m_isManage);
            StringBuilder selectList = this.EgvContent.SelectList;
            if (selectList.Length == 0)
            {
                AdminPage.WriteErrMsg("<li>对不起，您还没选择要归档的项目！</li>");
            }
            else if (ContentManage.UpdateStatus(selectList.ToString(), 100))
            {
                AdminPage.WriteSuccessMsg("<li>归档内容成功！</li>", "ContentManage.aspx?NodeID=" + BasePage.RequestInt32("NodeID"));
            }
            else
            {
                AdminPage.WriteErrMsg("<li>归档内容失败！</li>", "ContentManage.aspx?NodeID=" + BasePage.RequestInt32("NodeID"));
            }
        }

        protected void BtnModify_Click(object sender, EventArgs e)
        {
            BasePage.ResponseRedirect("ContentBatchModfiy.aspx");
        }

        protected void BtnSearch_Click(object sender, EventArgs e)
        {
            this.HdnSearchType.Value = this.DrpSearchType.SelectedValue;
            this.HdnSearchKeyword.Value = this.TxtSearchKeyword.Text;
            this.EgvContent.DataBind();
        }

        private static StringBuilder CompareState(string itemIDList, GridView gv, int state)
        {
            StringBuilder sb = new StringBuilder();
            string[] strArray = itemIDList.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            IList<StatusInfo> statusList = Status.GetStatusList();
            foreach (string str in strArray)
            {
                for (int i = 0; i < gv.Rows.Count; i++)
                {
                    if (str == gv.Rows[i].Cells[1].Text)
                    {
                        Label label = (Label) gv.Rows[i].FindControl("lStatus");
                        foreach (StatusInfo info in statusList)
                        {
                            if (info.StatusName.Contains(label.Text) && (state != info.StatusCode))
                            {
                                StringHelper.AppendString(sb, gv.Rows[i].Cells[1].Text);
                                break;
                            }
                        }
                        break;
                    }
                }
            }
            return sb;
        }

        protected void DropSelectedIndex_Changed(object sender, EventArgs e)
        {
            this.HdnListType.Value = this.DropRescentQuery.SelectedValue;
            this.EgvContent.PageIndex = 0;
        }

        protected void EBtnBatchDelete_Click(object sender, EventArgs e)
        {
            this.PermissionDetection(this.m_isManage);
            StringBuilder selectList = this.EgvContent.SelectList;
            if (selectList.Length == 0)
            {
                AdminPage.WriteErrMsg("<li>对不起，您还没选择要删除的项目！</li>", "ContentManage.aspx?NodeID=" + BasePage.RequestInt32("NodeID"));
            }
            else if (ContentManage.UpdateStatus(selectList.ToString(), -3))
            {
                PermissionContent.Delete(selectList.ToString());
                EasyOne.Contents.ContentCharge.Delete(selectList.ToString());
                SiteCache.Remove("CK_Page_Category_" + this.m_NodeId.ToString());
                AdminPage.WriteSuccessMsg("<li>删除成功！</li>", "ContentManage.aspx?NodeID=" + BasePage.RequestInt32("NodeID"));
            }
            else
            {
                AdminPage.WriteErrMsg("<li>删除项目失败！</li>", "ContentManage.aspx?NodeID=" + BasePage.RequestInt32("NodeID"));
            }
        }

        protected void EBtnBatchMove_Click(object sender, EventArgs e)
        {
            this.PermissionDetection(this.m_isManage);
            StringBuilder selectList = this.EgvContent.SelectList;
            if (selectList.Length == 0)
            {
                AdminPage.WriteErrMsg("<li>对不起，您还没选择要添加的项目！</li>");
            }
            else
            {
                BasePage.ResponseRedirect("ContentBatchMove.aspx?Id=" + selectList.ToString());
            }
        }

        protected void EBtnBatchSet_Click(object sender, EventArgs e)
        {
            this.PermissionDetection(this.m_isManage);
            BasePage.ResponseRedirect("ContentBatchSet.aspx?Action=Content&GeneralID=" + this.EgvContent.SelectList.ToString());
        }

        protected void EBtnBatchSpecialSet_Click(object sender, EventArgs e)
        {
            this.PermissionDetection(this.m_isManage);
            StringBuilder selectList = this.EgvContent.SelectList;
            if (selectList.Length == 0)
            {
                AdminPage.WriteErrMsg("<li>对不起，您还没选择要添加的项目！</li>");
            }
            else
            {
                BasePage.ResponseRedirect("AddContentToSpecial.aspx?Action=Content&Id=" + selectList.ToString());
            }
        }

        protected void EBtnCancelPass_Click(object sender, EventArgs e)
        {
            this.PermissionDetection(this.m_isCheck);
            StringBuilder selectList = this.EgvContent.SelectList;
            if (selectList.Length == 0)
            {
                AdminPage.WriteErrMsg("<li>对不起，您还没选择要取消审核的项目！</li>");
            }
            else if (ContentManage.UpdateStatus(selectList.ToString(), 0))
            {
                if (!SiteConfig.SmsConfig.IsAutoSendStateMessage || !this.m_Administrator)
                {
                    AdminPage.WriteSuccessMsg("<li>取消审核成功！</li>", "ContentManage.aspx?NodeID=" + BasePage.RequestInt32("NodeID"));
                }
                else
                {
                    selectList = CompareState(selectList.ToString(), this.EgvContent, 0);
                    if (selectList.Length > 0)
                    {
                        AdminPage.WriteSuccessMsg("<li>取消审核成功！</li>", string.Concat(new object[] { "ContentMessage.aspx?NodeID=", BasePage.RequestInt32("NodeID"), "&ItemIDList=", selectList.ToString(), "&State=0" }));
                    }
                }
            }
            else
            {
                AdminPage.WriteErrMsg("<li>取消审核失败！</li>", "ContentManage.aspx?NodeID=" + BasePage.RequestInt32("NodeID"));
            }
        }

        protected void EBtnPass_Click(object sender, EventArgs e)
        {
            this.PermissionDetection(this.m_isCheck);
            StringBuilder selectList = this.EgvContent.SelectList;
            if (selectList.Length == 0)
            {
                AdminPage.WriteErrMsg("<li>对不起，您还没选择要审核的项目！</li>", "ContentManage.aspx?NodeID=" + BasePage.RequestInt32("NodeID"));
            }
            else
            {
                int nodeWorkFlowId = Nodes.GetNodeWorkFlowId(this.m_NodeId);
                RolePermissions.BusinessAccessCheck(OperateCode.NodeContentCheck, this.m_NodeId);
                if (this.m_Administrator)
                {
                    UpdateStatus(selectList.ToString(), 0x63, SiteConfig.SmsConfig.IsAutoSendStateMessage, this.EgvContent, this.m_Administrator);
                }
                else if (Nodes.GetNodeById(BasePage.RequestInt32("NodeID")).Child == 0)
                {
                    UpdateStatus(selectList.ToString(), UserPass(nodeWorkFlowId), SiteConfig.SmsConfig.IsAutoSendStateMessage, this.EgvContent, this.m_Administrator);
                }
                else
                {
                    CommonModelInfo info2 = new CommonModelInfo();
                    if (selectList.ToString().IndexOf(",", StringComparison.Ordinal) > 0)
                    {
                        foreach (string str in selectList.ToString().Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries))
                        {
                            nodeWorkFlowId = Nodes.GetNodeWorkFlowId(Nodes.GetNodeById(ContentManage.GetCommonModelInfoById(DataConverter.CLng(str)).NodeId).WorkFlowId);
                            ContentManage.UpdateStatus(str, UserPass(nodeWorkFlowId));
                        }
                        AdminPage.WriteSuccessMsg("<li>审核通过！</li>", "ContentManage.aspx?NodeID=" + BasePage.RequestInt32("NodeID"));
                    }
                    else
                    {
                        nodeWorkFlowId = Nodes.GetNodeWorkFlowId(Nodes.GetNodeById(ContentManage.GetCommonModelInfoById(DataConverter.CLng(selectList.ToString())).NodeId).WorkFlowId);
                        UpdateStatus(selectList.ToString(), UserPass(nodeWorkFlowId), SiteConfig.SmsConfig.IsAutoSendStateMessage, this.EgvContent, this.m_Administrator);
                    }
                }
            }
        }

        protected void EgvContent_RowCommand(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "DeleteContent")
            {
                int generalId = DataConverter.CLng(e.CommandArgument.ToString());
                if (ContentManage.UpdateStatus(generalId, -3))
                {
                    PermissionContent.Delete(generalId);
                    EasyOne.Contents.ContentCharge.Delete(generalId);
                    SiteCache.Remove("CK_Page_Category_" + this.m_NodeId.ToString());
                    AdminPage.WriteSuccessMsg("<li>删除成功！</li>", "ContentManage.aspx?NodeID=" + BasePage.RequestInt32("NodeID"));
                }
                else
                {
                    AdminPage.WriteErrMsg("<li>删除失败！</li>", "ContentManage.aspx?NodeID=" + BasePage.RequestInt32("NodeID"));
                }
            }
        }

        protected void EgvContent_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                CommonModelInfo dataItem = (CommonModelInfo) e.Row.DataItem;
                int nodeId = dataItem.NodeId;
                string s = "";
                int length = 0;
                if (this.m_NodeInfoDictionary.ContainsKey(dataItem.NodeId))
                {
                    s = this.m_NodeInfoDictionary[dataItem.NodeId].NodeName;
                }
                else
                {
                    NodeInfo cacheNodeById = Nodes.GetCacheNodeById(dataItem.NodeId);
                    if (!cacheNodeById.IsNull)
                    {
                        s = cacheNodeById.NodeName;
                        this.m_NodeInfoDictionary.Add(dataItem.NodeId, cacheNodeById);
                    }
                }
                if (dataItem.NodeId != this.m_NodeId)
                {
                    ExtendedHyperLink link = e.Row.FindControl("LnkNodeLink") as ExtendedHyperLink;
                    link.BeginTag = "<strong>[";
                    link.Text = s;
                    link.EndTag = "]</strong>";
                    link.NavigateUrl = "ContentManage.aspx?NodeID=" + dataItem.NodeId.ToString() + "&NodeName=" + base.Server.UrlEncode(s);
                    length = StringHelper.SubStringLength(s) + 2;
                }
                HyperLink link2 = e.Row.FindControl("HypTitle") as HyperLink;
                LinkImage image = e.Row.FindControl("LinkImageModel") as LinkImage;
                string itemIcon = ModelManager.GetCacheModelById(dataItem.ModelId).ItemIcon;
                if (string.IsNullOrEmpty(itemIcon))
                {
                    itemIcon = "Default.gif";
                }
                image.Icon = itemIcon;
                if (dataItem.LinkType != 0)
                {
                    image.IsShowLink = true;
                }
                if (this.m_ModelPreviewDictionary.ContainsKey(dataItem.ModelId))
                {
                    link2.NavigateUrl = this.m_ModelPreviewDictionary[dataItem.ModelId] + "?GeneralID=" + dataItem.GeneralId;
                }
                else
                {
                    ModelInfo modelInfoById = ModelManager.GetModelInfoById(dataItem.ModelId);
                    link2.NavigateUrl = modelInfoById.PreviewInfoFilePath + "?GeneralID=" + dataItem.GeneralId;
                    this.m_ModelPreviewDictionary.Add(dataItem.ModelId, modelInfoById.PreviewInfoFilePath);
                }
                dataItem.Title = dataItem.Title;
                length = 0x25 - length;
                link2.Text = StringHelper.SubString(dataItem.Title, length, "...");
                link2.ToolTip = "标    题：" + dataItem.Title + "\r\n录 入 者：" + dataItem.Inputer + "\r\n更新时间：" + dataItem.UpdateTime.ToString();
                Label label = e.Row.FindControl("LblIsCreateHtml") as Label;
                if (!dataItem.CreateTime.HasValue || (dataItem.CreateTime.Value <= dataItem.UpdateTime))
                {
                    label.Text = "<span style=\"color:Red\"><b>\x00d7</b></span>";
                }
                else
                {
                    label.Text = "<b>√</b>";
                }
                HyperLink link3 = (HyperLink) e.Row.FindControl("ContentModify");
                LinkButton button = (LinkButton) e.Row.FindControl("ContentDelete");
                link3.Text = "修改";
                link3.NavigateUrl = string.Concat(new object[] { "Content.aspx?Action=Modify&NodeID=", nodeId.ToString(), "&GeneralID=", dataItem.GeneralId, "&ModelID=", dataItem.ModelId.ToString(), "&LinkType=", dataItem.LinkType.ToString() });
                button.Text = "删除";
                button.CommandName = "DeleteContent";
                button.CommandArgument = dataItem.GeneralId.ToString();
                button.OnClientClick = "if(!this.disabled) return confirm('确实要删除此信息吗？删除后你还可以从回收站中还原！');";
                if (ModelManager.GetCacheModelById(dataItem.ModelId).Disabled)
                {
                    button.Enabled = false;
                    link3.Enabled = false;
                }
                if (!this.m_Administrator)
                {
                    string checkStr = nodeId.ToString();
                    NodeInfo info4 = new NodeInfo();
                    if (this.m_NodeInfoDictionary.ContainsKey(dataItem.NodeId))
                    {
                        info4 = this.m_NodeInfoDictionary[dataItem.NodeId];
                    }
                    else
                    {
                        info4 = Nodes.GetCacheNodeById(nodeId);
                    }
                    if (info4.ParentId > 0)
                    {
                        checkStr = checkStr + "," + info4.ParentPath;
                    }
                    if (!StringHelper.FoundCharInArr(checkStr, this.m_arrNodeIdManage))
                    {
                        link3.Enabled = false;
                        button.Enabled = false;
                    }
                    if (!StringHelper.FoundCharInArr(this.m_arrNodeIdShow, checkStr))
                    {
                        link2.NavigateUrl = "#";
                    }
                }
            }
        }

        protected string GetStatusShow(string status)
        {
            int key = DataConverter.CLng(status);
            if (this.m_StatusDictionary.ContainsKey(key))
            {
                return this.m_StatusDictionary[key];
            }
            return "";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.m_Administrator = PEContext.Current.Admin.IsSuperAdmin;
            this.m_NodeId = BasePage.RequestInt32("NodeID");
            foreach (StatusInfo info in Status.GetStatusList())
            {
                if (!this.m_StatusDictionary.ContainsKey(info.StatusCode))
                {
                    this.m_StatusDictionary.Add(info.StatusCode, info.StatusName);
                }
            }
            if (!base.IsPostBack)
            {
                int nodeWorkFlowId = Nodes.GetNodeWorkFlowId(this.m_NodeId);
                if (this.m_Administrator)
                {
                    this.EBtnPass.Text = "终审通过";
                }
                else
                {
                    string[] strArray = PEContext.Current.Admin.Roles.Split(new char[] { ',' });
                    FlowProcessInfo info2 = new FlowProcessInfo(true);
                    foreach (string str in strArray)
                    {
                        FlowProcessInfo flowProcessByRoles = FlowProcess.GetFlowProcessByRoles(nodeWorkFlowId, DataConverter.CLng(str));
                        if (flowProcessByRoles.PassActionStatus > info2.PassActionStatus)
                        {
                            info2 = flowProcessByRoles;
                        }
                        bool isNull = info2.IsNull;
                    }
                    if (info2.IsNull)
                    {
                        this.EBtnPass.Visible = false;
                        this.EBtnCancelPass.Visible = false;
                    }
                    else
                    {
                        this.EBtnPass.Text = info2.PassActionName;
                    }
                }
                this.DropRescentQuery.SelectedValue = BasePage.RequestStringToLower("ListType");
                this.HdnListType.Value = BasePage.RequestStringToLower("ListType");
                this.RadlContent.SelectedValue = BasePage.RequestStringToLower("status", "100");
                this.HdnStatus.Value = BasePage.RequestStringToLower("status", "100");
                if (this.m_NodeId > 0)
                {
                    this.SmpNavigator.CurrentNode = "<a href=\"ContentManage.aspx\">栏目信息管理</a>";
                    this.SmpNavigator.AdditionalNode = Nodes.ShowNodeNavigation(this.m_NodeId);
                    if (!string.IsNullOrEmpty(this.SmpNavigator.AdditionalNode))
                    {
                        this.SmpNavigator.AdditionalNode = this.SmpNavigator.AdditionalNode.Replace("根节点 >>", "");
                    }
                    IList<FieldInfo> nodeFieldList = ModelManager.GetNodeFieldList(this.m_NodeId);
                    if (nodeFieldList != null)
                    {
                        foreach (FieldInfo info4 in nodeFieldList)
                        {
                            if (info4.EnableShowOnSearchForm && (info4.FieldName != "Title"))
                            {
                                this.DrpSearchType.Items.Add(new ListItem(info4.FieldAlias, info4.FieldName));
                            }
                        }
                    }
                }
            }
            if (!this.m_Administrator)
            {
                this.m_arrNodeIdShow = RolePermissions.GetRoleNodeId(PEContext.Current.Admin.Roles, OperateCode.NodeContentPreview);
                this.m_arrNodeIdCheck = RolePermissions.GetRoleNodeId(PEContext.Current.Admin.Roles, OperateCode.NodeContentCheck);
                this.m_arrNodeIdManage = RolePermissions.GetRoleNodeId(PEContext.Current.Admin.Roles, OperateCode.NodeContentManage);
                if (this.m_NodeId > 0)
                {
                    string findStr = this.m_NodeId.ToString();
                    NodeInfo cacheNodeById = Nodes.GetCacheNodeById(this.m_NodeId);
                    if (cacheNodeById.IsNull)
                    {
                        AdminPage.WriteErrMsg("当前栏目不存在，可能被删除了请返回！");
                    }
                    if (cacheNodeById.ParentId > 0)
                    {
                        findStr = findStr + "," + cacheNodeById.ParentPath;
                    }
                    this.m_isCheck = StringHelper.FoundCharInArr(this.m_arrNodeIdCheck, findStr);
                    this.m_isManage = StringHelper.FoundCharInArr(this.m_arrNodeIdManage, findStr);
                }
                else
                {
                    this.m_isCheck = RolePermissions.AccessCheckNodePermission(OperateCode.NodeContentCheck, -1);
                    this.m_isManage = RolePermissions.AccessCheckNodePermission(OperateCode.NodeContentManage, -1);
                }
                if (!this.m_isManage)
                {
                    this.EBtnBatchDelete.Enabled = false;
                    this.EBtnBatchSet.Enabled = false;
                    this.BatchSpecialSet.Enabled = false;
                    this.BatchNodeSet.Enabled = false;
                    this.EBtnBatchMove.Enabled = false;
                    this.BtnArchiving.Enabled = false;
                }
                if (!this.m_isCheck)
                {
                    this.EBtnPass.Enabled = false;
                    this.EBtnCancelPass.Enabled = false;
                }
            }
            else
            {
                this.LblContentAdvancedSearch.Visible = true;
            }
        }

        private void PermissionDetection(bool permissionType)
        {
            if (!this.m_Administrator && !permissionType)
            {
                AdminPage.WriteErrMsg("<li>对不起，您没有当前项目的管理权限！</li>");
            }
        }

        protected void RadlContent_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.HdnStatus.Value = this.RadlContent.SelectedValue;
            this.EgvContent.PageIndex = 0;
        }

        private static void UpdateStatus(string itemIDList, int status, bool IsSendMessage, GridView gv, bool Administrator)
        {
            if (ContentManage.UpdateStatus(itemIDList, status))
            {
                if (IsSendMessage && Administrator)
                {
                    itemIDList = CompareState(itemIDList, gv, status).ToString();
                    if (!string.IsNullOrEmpty(itemIDList))
                    {
                        AdminPage.WriteSuccessMsg("<li>审核通过！</li>", string.Concat(new object[] { "ContentMessage.aspx?NodeID=", BasePage.RequestInt32("NodeID"), "&ItemIDList=", itemIDList, "&State=", status }));
                    }
                    else
                    {
                        AdminPage.WriteSuccessMsg("<li>审核通过！</li>", "ContentManage.aspx?NodeID=" + BasePage.RequestInt32("NodeID"));
                    }
                }
                else
                {
                    AdminPage.WriteSuccessMsg("<li>审核通过！</li>", "ContentManage.aspx?NodeID=" + BasePage.RequestInt32("NodeID"));
                }
            }
            else
            {
                AdminPage.WriteErrMsg("<li>审核通过失败！</li>", "ContentManage.aspx?NodeID=" + BasePage.RequestInt32("NodeID"));
            }
        }

        private static int UserPass(int workFlowId)
        {
            string[] strArray = PEContext.Current.Admin.Roles.Split(new char[] { ',' });
            FlowProcessInfo info = new FlowProcessInfo(true);
            foreach (string str in strArray)
            {
                FlowProcessInfo flowProcessByRoles = FlowProcess.GetFlowProcessByRoles(workFlowId, DataConverter.CLng(str));
                if (flowProcessByRoles.PassActionStatus > info.PassActionStatus)
                {
                    info = flowProcessByRoles;
                }
            }
            return info.PassActionStatus;
        }
    }
}

