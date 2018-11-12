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
    using EasyOne.Model.AccessManage;
    using EasyOne.Model.CommonModel;
    using EasyOne.Model.Contents;
    using EasyOne.Model.UserManage;
    using EasyOne.ModelControls;
    using EasyOne.Templates;
    using EasyOne.UserManage;
    using EasyOne.Web.UI;
    using EasyOne.WebSite.Controls;
    using EasyOne.WorkFlows;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using EasyOne.Model.WorkFlow;

    public partial class Category : AdminPage
    {
        protected string m_arrCurrentNodesManage;
        protected DataTable initDataTable;
        private void AddNode(NodeInfo nodeInfo)
        {
            switch (EasyOne.Contents.Nodes.Add(nodeInfo, this.GetDataFromRepeater()))
            {
                case 0:
                    AdminPage.WriteErrMsg("<li>添加栏目失败！</li>");
                    return;

                case 1:
                    this.InputPermissions(nodeInfo.NodeId);
                    if (PEContext.Current.Admin.IsSuperAdmin)
                    {
                        this.InputRolePermission(nodeInfo.NodeId);
                    }
                    UpdatePurviewType(nodeInfo.NodeId, "Add");
                    IncludeFile.CreateIncludeFileByAssociateType(AssociateType.Node);
                    base.Response.Write("<script type='text/javascript'>parent.frames[\"left\"].location.reload();</script>");
                    AdminPage.WriteSuccessMsg("添加栏目成功！</li>", "Category.aspx?Action=Modify&NodeID=" + nodeInfo.NodeId);
                    return;

                case 2:
                    AdminPage.WriteErrMsg("<li>栏目名称已经存在！</li>");
                    return;

                case 3:
                    AdminPage.WriteErrMsg("<li>栏目标识符已经存在！</li>");
                    return;

                case 4:
                    AdminPage.WriteErrMsg("<li>栏目目录名已经存在！</li>");
                    return;

                case 5:
                    AdminPage.WriteErrMsg("<li>父栏目必须为容器栏目！</li>");
                    return;

                case 6:
                    AdminPage.WriteErrMsg("<li>父栏目不存在！</li>");
                    return;
            }
            AdminPage.WriteErrMsg("<li>未知错误！</li>");
        }

        private void AddPermissions(string roleIdList, OperateCode operateCode, int nodeId, string errorMessage)
        {
            if (!UserPermissions.AddNodePermissions(roleIdList, operateCode, nodeId, 1))
            {
                AdminPage.WriteErrMsg("<li>" + errorMessage + "</li>");
            }
        }

        private static void AppendSelectId(bool isChecked, StringBuilder roleIdList, string selectId)
        {
            if (isChecked)
            {
                StringHelper.AppendString(roleIdList, selectId);
            }
        }

        private bool BatchAddNode(NodeInfo nodeInfo, StringBuilder information)
        {
            bool flag = false;
            bool flag2 = true;
            if (IsDir(nodeInfo.NodeDir))
            {
                information.Append("<li>" + nodeInfo.NodeName + "目录名只能是字母、数字、下划线组成，首字符不能是数字！</li>");
                flag2 = false;
            }
            if (flag2)
            {
                switch (EasyOne.Contents.Nodes.Add(nodeInfo, this.GetDataFromRepeater()))
                {
                    case 0:
                        information.Append("<li>添加" + nodeInfo.NodeName + "栏目失败！</li>");
                        return flag;

                    case 1:
                        this.InputPermissions(nodeInfo.NodeId);
                        if (PEContext.Current.Admin.IsSuperAdmin)
                        {
                            this.InputRolePermission(nodeInfo.NodeId);
                        }
                        UpdatePurviewType(nodeInfo.NodeId, "Add");
                        IncludeFile.CreateIncludeFileByAssociateType(AssociateType.Node);
                        return true;

                    case 2:
                        information.Append("<li>" + nodeInfo.NodeName + "栏目名称已经存在！</li>");
                        return flag;

                    case 3:
                        information.Append("<li>" + nodeInfo.NodeName + "栏目标识符已经存在！</li>");
                        return flag;

                    case 4:
                        information.Append("<li>" + nodeInfo.NodeName + "栏目目录名已经存在！</li>");
                        return flag;

                    case 5:
                        information.Append("<li>" + nodeInfo.NodeName + "父栏目必须为容器栏目！</li>");
                        return flag;

                    case 6:
                        information.Append("<li>" + nodeInfo.NodeName + "父栏目不存在！</li>");
                        return flag;
                }
                information.Append("<li>添加" + nodeInfo.NodeName + "未知错误！</li>");
            }
            return flag;
        }

        private void BindNodesInfo(int nodeId)
        {
            NodeInfo cacheNodeById = EasyOne.Contents.Nodes.GetCacheNodeById(nodeId);
            if (cacheNodeById.IsNull)
            {
                AdminPage.WriteErrMsg("发生错误，请传入正确的NodeID！");
            }
            if (cacheNodeById.Settings == null)
            {
                AdminPage.WriteErrMsg("发生错误！");
            }
            this.SmpNavigator.CurrentNode = "修改栏目设置：<span style=\"color: Red\">" + cacheNodeById.NodeName + "</span>";
            this.DropParentNode.SelectedValue = cacheNodeById.ParentId.ToString();
            this.DropParentNode.Visible = false;
            this.LblNodeName.Text = EasyOne.Contents.Nodes.ShowParentNodesNavigation(nodeId);
            this.TxtNodeName.Text = cacheNodeById.NodeName;
            this.TxtNodeIdentifier.Text = cacheNodeById.NodeIdentifier;
            this.TxtNodeDir.Enabled = false;
            this.TxtNodeDir.Text = cacheNodeById.NodeDir;
            this.TxtNodePicUrl.Text = cacheNodeById.NodePicUrl;
            this.TxtTips.Text = cacheNodeById.Tips;
            this.TxtDescription.Text = cacheNodeById.Description;
            this.TxtMetaDescription.Text = cacheNodeById.MetaDescription;
            this.TxtMetaKeywords.Text = cacheNodeById.MetaKeywords;
            switch (cacheNodeById.OpenType)
            {
                case 0:
                    this.RadOpenType0.Checked = true;
                    break;

                case 1:
                    this.RadOpenType1.Checked = true;
                    break;
            }
            this.SetPurviewType(cacheNodeById.ParentId, cacheNodeById.PurviewType);
            this.ChkEnableComment.Checked = cacheNodeById.Settings.EnableComment;
            this.ChkEnableTouristsComment.Checked = cacheNodeById.Settings.EnableTouristsComment;
            this.ChkCommentNeedCheck.Checked = cacheNodeById.Settings.CommentNeedCheck;
            this.DropWorkFlow.SelectedValue = cacheNodeById.WorkFlowId.ToString();
            this.RadlEnableProtect.SelectedValue = cacheNodeById.Settings.EnableProtect.ToString();
            this.RadlEnableAddWhenHasChild.SelectedValue = cacheNodeById.Settings.EnableAddWhenHasChild.ToString();
            this.TxtHitsOfHot.Text = cacheNodeById.HitsOfHot.ToString();
            if (cacheNodeById.Settings.IsSetCache)
            {
                this.RadNeedCache1.Checked = true;
                this.RadNeedCache0.Checked = false;
                this.TxtCacheTime.Text = cacheNodeById.Settings.CacheTime.ToString();
            }
            else
            {
                this.RadNeedCache1.Checked = false;
                this.RadNeedCache0.Checked = true;
            }
            this.TxtPresentExp.Text = cacheNodeById.Settings.PresentExp.ToString();
            this.TxtDefaultItemPoint.Text = cacheNodeById.Settings.DefaultItemPoint.ToString();
            this.ShowChargeType.ChargeType = cacheNodeById.Settings.DefaultItemChargeType;
            this.ShowChargeType.PitchTime = cacheNodeById.Settings.DefaultItemPitchTime;
            this.ShowChargeType.ReadTimes = cacheNodeById.Settings.DefaultItemReadTimes;
            this.TxtDefaultItemDividePercent.Text = cacheNodeById.Settings.DefaultItemDividePercent.ToString();
            this.RadlShowOnMenu.SelectedValue = cacheNodeById.ShowOnMenu.ToString();
            this.RadlShowOnPath.SelectedValue = cacheNodeById.ShowOnPath.ToString();
            this.RadlShowOnMap.SelectedValue = cacheNodeById.ShowOnMap.ToString();
            this.RadlShowOnListIndex.SelectedValue = cacheNodeById.ShowOnListIndex.ToString();
            this.RadlShowOnListParent.SelectedValue = cacheNodeById.ShowOnListParent.ToString();
            BasePage.SetSelectedIndexByValue(this.DrpItemListOrderType, cacheNodeById.ItemListOrderType.ToString());
            BasePage.SetSelectedIndexByValue(this.DrpItemOpenType, cacheNodeById.ItemOpenType.ToString());
            this.CombItemPageSize.Value = cacheNodeById.ItemPageSize.ToString();
            this.FileCdefaultListTmeplate.Text = cacheNodeById.DefaultTemplateFile;
            this.FileContainChildTemplate.Text = cacheNodeById.ContainChildTemplateFile;
            this.RadlIsContentPageCreate.SelectedIndex = this.RadlIsContentPageCreate.Items.IndexOf(this.RadlIsContentPageCreate.Items.FindByValue(cacheNodeById.IsCreateContentPage.ToString().ToLower()));
            this.RadlIsListPageCreate.SelectedIndex = this.RadlIsListPageCreate.Items.IndexOf(this.RadlIsListPageCreate.Items.FindByValue(cacheNodeById.IsCreateListPage.ToString().ToLower()));
            BasePage.SetSelectedIndexByValue(this.RadlAutoCreateHtmlType, cacheNodeById.AutoCreateHtmlType.ToString());
            if (cacheNodeById.AutoCreateHtmlType == AutoCreateHtmlType.ContentAndRelatedNode)
            {
                this.SelectRelation.Attributes.Add("style", "display:");
            }
            this.ListBoxSetValue(this.LstRelationNodes, cacheNodeById.RelateNode);
            this.ListBoxSetValue(this.LstRelationSpecial, cacheNodeById.RelateSpecial);
            BasePage.SetSelectedIndexByValue(this.RadlListPageHtmlDirType, cacheNodeById.ListPageSavePathType.ToString());
            this.PagePostfix.Value = cacheNodeById.ListPagePostfix;
            if (!string.IsNullOrEmpty(cacheNodeById.ContentPageHtmlRule))
            {
                string contentPageHtmlRule = cacheNodeById.ContentPageHtmlRule;
                this.TxtContentHtmlDir.Value = contentPageHtmlRule.Substring(0, contentPageHtmlRule.LastIndexOf("/", StringComparison.Ordinal));
                string str2 = contentPageHtmlRule.Substring(contentPageHtmlRule.LastIndexOf("/", StringComparison.Ordinal) + 1, (contentPageHtmlRule.Length - contentPageHtmlRule.LastIndexOf("/", StringComparison.Ordinal)) - 1);
                int startIndex = contentPageHtmlRule.LastIndexOf(".", StringComparison.Ordinal);
                if (startIndex > 0)
                {
                    this.TxtContentHtmlExt.Value = contentPageHtmlRule.Substring(startIndex, contentPageHtmlRule.Length - contentPageHtmlRule.LastIndexOf(".", StringComparison.Ordinal)).Replace(".", "");
                    this.TxtContentHtmlFile.Value = str2.Substring(0, str2.LastIndexOf(".", StringComparison.Ordinal));
                }
            }
            int num2 = 1;
            if (!string.IsNullOrEmpty(cacheNodeById.CustomContent))
            {
                string[] strArray = cacheNodeById.CustomContent.Split(new string[] { "{#$$$#}" }, StringSplitOptions.None);
                int num3 = 1;
                foreach (string str3 in strArray)
                {
                    num2 = num3;
                    ((TextBox) this.PnlCustomFileds.FindControl("Custom_Content" + num3)).Text = str3;
                    ((HtmlTableRow) this.PnlCustomFileds.FindControl("objFiles" + num3)).Attributes.Add("style", "display: ''");
                    num3++;
                }
            }
            ((DropDownList) this.PnlCustomFileds.FindControl("DropCustomNum")).SelectedValue = num2.ToString();
        }

        private static string CountRoleNodePermissionsId(string iChildNodesManage)
        {
            NodeInfo nodeById = EasyOne.Contents.Nodes.GetNodeById(DataConverter.CLng(iChildNodesManage));
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

        protected void DropParentNode_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.SetPurviewType(DataConverter.CLng(this.DropParentNode.SelectedValue));
        }

        protected void EBtnAdd_Click(object sender, EventArgs e)
        {
            if (this.Page.IsValid)
            {
                if ((!PEContext.Current.Admin.IsSuperAdmin && (this.m_arrCurrentNodesManage != "-1")) && !StringHelper.FoundCharInArr(this.m_arrCurrentNodesManage, this.DropParentNode.SelectedValue))
                {
                    AdminPage.WriteErrMsg("<li>对不起，您没有保存指定栏目的权限！</li>");
                }
                NodeInfo nodesInfo = this.GetNodesInfo();
                if (BasePage.RequestString("AddType").CompareTo("BatchCategory") != 0)
                {
                    this.AddNode(nodesInfo);
                }
                else
                {
                    string text = this.TxtNodeNames.Text;
                    string str2 = this.TxtNodeIdentifiers.Text;
                    string str3 = this.TxtNodeDirs.Text;
                    if (text.IndexOf("\r\n") > 0)
                    {
                        string[] strArray = text.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                        if ((str2.IndexOf("\r\n") <= 0) || (str3.IndexOf("\r\n") <= 0))
                        {
                            AdminPage.WriteErrMsg("<li>栏目标识符或栏目的目录名不能为空！</li>");
                        }
                        else
                        {
                            string[] strArray2 = str2.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                            string[] strArray3 = str3.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                            if ((strArray2.Length != strArray.Length) || (strArray3.Length != strArray.Length))
                            {
                                AdminPage.WriteErrMsg("<li>栏目标识符或栏目的目录名与栏目名称数目不相同！</li>");
                            }
                            StringBuilder information = new StringBuilder();
                            bool flag = true;
                            for (int i = 0; i < strArray.Length; i++)
                            {
                                nodesInfo.NodeName = strArray[i];
                                nodesInfo.NodeIdentifier = strArray2[i];
                                nodesInfo.NodeDir = strArray3[i];
                                if (!this.BatchAddNode(nodesInfo, information))
                                {
                                    flag = false;
                                }
                            }
                            base.Response.Write("<script type='text/javascript'>parent.frames[\"left\"].location.reload();</script>");
                            if (flag)
                            {
                                AdminPage.WriteSuccessMsg("<li>批量添加栏目成功！</li>", "Category.aspx?Action=Modify&NodeID=" + nodesInfo.NodeId);
                            }
                            else
                            {
                                AdminPage.WriteErrMsg(information.ToString());
                            }
                        }
                    }
                    else
                    {
                        nodesInfo.NodeName = text;
                        nodesInfo.NodeIdentifier = str2;
                        nodesInfo.NodeDir = str3;
                        this.AddNode(nodesInfo);
                    }
                }
            }
        }

        protected void EBtnDelete_Click(object sender, EventArgs e)
        {
            if ((!PEContext.Current.Admin.IsSuperAdmin && (this.m_arrCurrentNodesManage != "-1")) && !StringHelper.FoundCharInArr(this.m_arrCurrentNodesManage, BasePage.RequestString("NodeID")))
            {
                AdminPage.WriteErrMsg("<li>对不起，您没有保存指定栏目的权限！</li>");
            }
            switch (EasyOne.Contents.Nodes.Delete(BasePage.RequestInt32("NodeID")))
            {
                case 0:
                    AdminPage.WriteErrMsg("删除栏目失败！");
                    return;

                case 1:
                    IncludeFile.CreateIncludeFileByAssociateType(AssociateType.Node);
                    base.Response.Write("<script type='text/javascript'>parent.frames[\"left\"].location.reload();</script>");
                    AdminPage.WriteSuccessMsg("删除栏目成功！请记得重新生成相关栏目的文件呀！");
                    return;

                case 2:
                    AdminPage.WriteErrMsg("栏目不存在，或者已经被删除！");
                    return;

                case 3:
                    AdminPage.WriteErrMsg("首页节点，禁止删除！");
                    return;
            }
        }

        protected void EBtnModify_Click(object sender, EventArgs e)
        {
            if (this.Page.IsValid)
            {
                if ((!PEContext.Current.Admin.IsSuperAdmin && (this.m_arrCurrentNodesManage != "-1")) && !StringHelper.FoundCharInArr(this.m_arrCurrentNodesManage, BasePage.RequestString("NodeID")))
                {
                    AdminPage.WriteErrMsg("<li>对不起，您没有保存指定栏目的权限！</li>");
                }
                NodeInfo nodesInfo = this.GetNodesInfo();
                nodesInfo.NeedCreateHtml = true;
                switch (EasyOne.Contents.Nodes.Update(nodesInfo, this.GetDataFromRepeater()))
                {
                    case 0:
                        AdminPage.WriteErrMsg("修改栏目失败！", "Category.aspx");
                        return;

                    case 1:
                        this.InputPermissions(nodesInfo.NodeId);
                        if (PEContext.Current.Admin.IsSuperAdmin)
                        {
                            this.InputRolePermission(nodesInfo.NodeId);
                        }
                        IncludeFile.CreateIncludeFileByAssociateType(AssociateType.Node);
                        UpdatePurviewType(nodesInfo.NodeId, "Modify");
                        base.Response.Write("<script type='text/javascript'>parent.frames[\"left\"].location.reload()</script>");
                        AdminPage.WriteSuccessMsg("修改栏目成功！", "Category.aspx?Action=Modify&NodeID=" + nodesInfo.NodeId);
                        return;

                    case 2:
                        AdminPage.WriteErrMsg("栏目名称已经存在！");
                        return;

                    case 3:
                        AdminPage.WriteErrMsg("栏目标识符已经存在！");
                        return;
                }
                AdminPage.WriteErrMsg("未知错误！");
            }
        }

        protected void EgvPermissions_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                UserGroupsInfo dataItem = (UserGroupsInfo) e.Row.DataItem;
                CheckBox box = (CheckBox) e.Row.FindControl("ChkNodeSkim");
                CheckBox box2 = (CheckBox) e.Row.FindControl("ChkNodeShow");
                CheckBox box3 = (CheckBox) e.Row.FindControl("ChkNodeInput");
                foreach (RoleNodePermissionsInfo info2 in UserPermissions.GetAllNodePermissionsById(dataItem.GroupId, BasePage.RequestInt32("NodeId"), 1))
                {
                    bool flag = info2.NodeId != -1;
                    if (info2.OperateCode == OperateCode.NodeContentSkim)
                    {
                        box.Checked = true;
                        if (!flag)
                        {
                            box.Enabled = flag;
                        }
                    }
                    if (info2.OperateCode == OperateCode.NodeContentPreview)
                    {
                        box2.Checked = true;
                        if (!flag)
                        {
                            box2.Enabled = flag;
                        }
                    }
                    if (info2.OperateCode == OperateCode.NodeContentInput)
                    {
                        box3.Checked = true;
                        if (!flag)
                        {
                            box3.Checked = flag;
                        }
                    }
                }
                if (this.RadPurviewType0.Checked)
                {
                    box.Checked = true;
                    box.Enabled = false;
                    box2.Checked = true;
                    box2.Enabled = false;
                }
                if (this.RadPurviewType1.Checked)
                {
                    box.Checked = true;
                    box.Enabled = false;
                }
            }
        }

        protected void EgvRoleView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                CheckBox box = (CheckBox) e.Row.FindControl("ChkNodePreview");
                CheckBox box2 = (CheckBox) e.Row.FindControl("ChkNodeInput");
                CheckBox box3 = (CheckBox) e.Row.FindControl("ChkNodeCheck");
                CheckBox box4 = (CheckBox) e.Row.FindControl("ChkContentManage");
                CheckBox box5 = (CheckBox) e.Row.FindControl("ChkNodeManage");
                CheckBox box6 = (CheckBox) e.Row.FindControl("ChkCommentManage");
                RoleInfo dataItem = (RoleInfo) e.Row.DataItem;
                foreach (RoleNodePermissionsInfo info2 in RolePermissions.GetAllNodePermissionsById(dataItem.RoleId, BasePage.RequestInt32("NodeId")))
                {
                    bool flag = info2.NodeId != -1;
                    if (info2.OperateCode == OperateCode.NodeContentPreview)
                    {
                        box.Checked = true;
                        if (!flag)
                        {
                            box.Enabled = flag;
                        }
                    }
                    if (info2.OperateCode == OperateCode.NodeContentInput)
                    {
                        box2.Checked = true;
                        if (!flag)
                        {
                            box2.Enabled = flag;
                        }
                    }
                    if (info2.OperateCode == OperateCode.NodeContentCheck)
                    {
                        box3.Checked = true;
                        if (!flag)
                        {
                            box3.Enabled = flag;
                        }
                    }
                    if (info2.OperateCode == OperateCode.CurrentNodesManage)
                    {
                        box5.Checked = true;
                        if (!flag)
                        {
                            box5.Enabled = flag;
                        }
                    }
                    if (info2.OperateCode == OperateCode.NodeContentManage)
                    {
                        box4.Checked = true;
                        if (!flag)
                        {
                            box4.Enabled = flag;
                        }
                    }
                    if (info2.OperateCode == OperateCode.NodeCommentManage)
                    {
                        box6.Checked = true;
                        if (!flag)
                        {
                            box6.Enabled = flag;
                        }
                    }
                }
            }
        }

        private IList<NodesModelTemplateRelationShipInfo> GetDataFromRepeater()
        {
            IList<NodesModelTemplateRelationShipInfo> list = new List<NodesModelTemplateRelationShipInfo>();
            foreach (RepeaterItem item in this.RepContentModelTemplate.Items)
            {
                if ((item.ItemType == ListItemType.Item) || (item.ItemType == ListItemType.AlternatingItem))
                {
                    string text = ((TextBox) item.FindControl("FileCTemplate")).Text;
                    if (((CheckBox) item.FindControl("ChkModel")).Checked && !string.IsNullOrEmpty(text))
                    {
                        NodesModelTemplateRelationShipInfo info = new NodesModelTemplateRelationShipInfo();
                        info.ModelId = DataConverter.CLng(((HiddenField) item.FindControl("HdnModelId")).Value);
                        info.DefaultTemplateFile = text;
                        list.Add(info);
                    }
                }
            }
            foreach (RepeaterItem item2 in this.RepShopModelTemplate.Items)
            {
                if ((item2.ItemType == ListItemType.Item) || (item2.ItemType == ListItemType.AlternatingItem))
                {
                    string str2 = ((TextBox) item2.FindControl("FileCTemplate")).Text;
                    if (((CheckBox) item2.FindControl("ChkModel")).Checked && !string.IsNullOrEmpty(str2))
                    {
                        NodesModelTemplateRelationShipInfo info2 = new NodesModelTemplateRelationShipInfo();
                        info2.ModelId = DataConverter.CLng(((HiddenField) item2.FindControl("HdnModelId")).Value);
                        info2.DefaultTemplateFile = str2;
                        list.Add(info2);
                    }
                }
            }
            return list;
        }

        private NodeInfo GetNodesInfo()
        {
            NodeInfo nodeById = EasyOne.Contents.Nodes.GetNodeById(BasePage.RequestInt32("NodeId", 0));
            NodeSettingInfo info2 = new NodeSettingInfo();
            nodeById.ParentId = DataConverter.CLng(this.DropParentNode.SelectedValue);
            if (BasePage.RequestString("AddType").CompareTo("BatchCategory") != 0)
            {
                nodeById.NodeName = this.TxtNodeName.Text;
                nodeById.NodeIdentifier = this.TxtNodeIdentifier.Text;
                nodeById.NodeDir = this.TxtNodeDir.Text;
            }
            nodeById.NodePicUrl = this.TxtNodePicUrl.Text;
            nodeById.NodeType = NodeType.Container;
            nodeById.Tips = this.TxtTips.Text;
            nodeById.Description = this.TxtDescription.Text;
            nodeById.MetaDescription = this.TxtMetaDescription.Text;
            nodeById.MetaKeywords = this.TxtMetaKeywords.Text;
            if (this.RadOpenType0.Checked)
            {
                nodeById.OpenType = 0;
            }
            if (this.RadOpenType1.Checked)
            {
                nodeById.OpenType = 1;
            }
            if (this.RadPurviewType0.Checked)
            {
                nodeById.PurviewType = 0;
            }
            if (this.RadPurviewType1.Checked)
            {
                nodeById.PurviewType = 1;
            }
            if (this.RadPurviewType2.Checked)
            {
                nodeById.PurviewType = 2;
            }
            info2.EnableComment = this.ChkEnableComment.Checked;
            info2.EnableTouristsComment = this.ChkEnableTouristsComment.Checked;
            info2.CommentNeedCheck = this.ChkCommentNeedCheck.Checked;
            nodeById.WorkFlowId = DataConverter.CLng(this.DropWorkFlow.SelectedValue);
            info2.EnableProtect = Convert.ToBoolean(this.RadlEnableProtect.SelectedValue);
            info2.EnableAddWhenHasChild = Convert.ToBoolean(this.RadlEnableAddWhenHasChild.SelectedValue);
            nodeById.HitsOfHot = DataConverter.CLng(this.TxtHitsOfHot.Text);
            if (this.RadNeedCache1.Checked)
            {
                info2.IsSetCache = true;
                info2.CacheTime = DataConverter.CLng(this.TxtCacheTime.Text);
            }
            if (this.RadNeedCache0.Checked)
            {
                info2.IsSetCache = false;
                info2.CacheTime = 0;
            }
            info2.PresentExp = DataConverter.CLng(this.TxtPresentExp.Text);
            info2.DefaultItemPoint = DataConverter.CLng(this.TxtDefaultItemPoint.Text);
            info2.DefaultItemChargeType = this.ShowChargeType.ChargeType;
            info2.DefaultItemPitchTime = this.ShowChargeType.PitchTime;
            info2.DefaultItemReadTimes = this.ShowChargeType.ReadTimes;
            info2.DefaultItemDividePercent = DataConverter.CLng(this.TxtDefaultItemDividePercent.Text);
            nodeById.DefaultTemplateFile = this.FileCdefaultListTmeplate.Text;
            nodeById.ContainChildTemplateFile = this.FileContainChildTemplate.Text;
            nodeById.ShowOnMenu = Convert.ToBoolean(this.RadlShowOnMenu.SelectedValue);
            nodeById.ShowOnPath = Convert.ToBoolean(this.RadlShowOnPath.SelectedValue);
            nodeById.ShowOnMap = Convert.ToBoolean(this.RadlShowOnMap.SelectedValue);
            nodeById.ShowOnListIndex = Convert.ToBoolean(this.RadlShowOnListIndex.SelectedValue);
            nodeById.ShowOnListParent = Convert.ToBoolean(this.RadlShowOnListParent.SelectedValue);
            nodeById.ItemListOrderType = DataConverter.CLng(this.DrpItemListOrderType.SelectedValue);
            nodeById.ItemOpenType = DataConverter.CLng(this.DrpItemOpenType.SelectedValue);
            nodeById.ItemPageSize = DataConverter.CLng(this.CombItemPageSize.Value);
            nodeById.IsCreateContentPage = DataConverter.CBoolean(this.RadlIsContentPageCreate.SelectedValue);
            nodeById.IsCreateListPage = DataConverter.CBoolean(this.RadlIsListPageCreate.SelectedValue);
            nodeById.ListPageSavePathType = (ListPagePathType) Enum.Parse(typeof(ListPagePathType), this.RadlListPageHtmlDirType.SelectedValue);
            nodeById.ListPagePostfix = this.PagePostfix.Value;
            nodeById.AutoCreateHtmlType = (AutoCreateHtmlType) Enum.Parse(typeof(AutoCreateHtmlType), this.RadlAutoCreateHtmlType.SelectedValue);
            nodeById.ContentPageHtmlRule = this.TxtContentHtmlDir.Value + "/" + this.TxtContentHtmlFile.Value + "." + this.TxtContentHtmlExt.Value;
            nodeById.RelateNode = this.GetSelectArrFromListControl(this.LstRelationNodes).ToString();
            nodeById.RelateSpecial = this.GetSelectArrFromListControl(this.LstRelationSpecial).ToString();
            int num = DataConverter.CLng(this.DropCustomNum.SelectedValue);
            StringBuilder builder = new StringBuilder();
            for (int i = 1; i <= num; i++)
            {
                if (builder.Length > 0)
                {
                    builder.Append("{#$$$#}");
                }
                builder.Append(((TextBox) this.PnlCustomFileds.FindControl("Custom_Content" + i)).Text);
            }
            nodeById.CustomContent = builder.ToString();
            nodeById.Settings = info2;
            nodeById.NeedCreateHtml = false;
            return nodeById;
        }

        private StringBuilder GetSelectArrFromListControl(ListControl listControl)
        {
            StringBuilder sb = new StringBuilder();
            foreach (ListItem item in listControl.Items)
            {
                if (item.Selected)
                {
                    StringHelper.AppendString(sb, item.Value);
                }
            }
            return sb;
        }

        private void InitCreateHtmlControl()
        {
            this.RadlAutoCreateHtmlType.Items[4].Attributes.Add("onclick", "ShowSelectRelation(\"0\")");
            this.RadlAutoCreateHtmlType.Items[0].Attributes.Add("onclick", "ShowSelectRelation(\"0\")");
            this.RadlAutoCreateHtmlType.Items[1].Attributes.Add("onclick", "ShowSelectRelation(\"0\")");
            this.RadlAutoCreateHtmlType.Items[2].Attributes.Add("onclick", "ShowSelectRelation(\"0\")");
            this.RadlAutoCreateHtmlType.Items[3].Attributes.Add("onclick", "ShowSelectRelation(\"0\")");
            this.RadlAutoCreateHtmlType.Items[5].Attributes.Add("onclick", "ShowSelectRelation(\"4\")");
        }

        private void InitFromParentNode(int parentId)
        {
            NodeInfo cacheNodeById = EasyOne.Contents.Nodes.GetCacheNodeById(parentId);
            if (!cacheNodeById.IsNull && (cacheNodeById.Settings != null))
            {
                this.TxtMetaDescription.Text = cacheNodeById.MetaDescription;
                this.TxtMetaKeywords.Text = cacheNodeById.MetaKeywords;
                this.ChkEnableComment.Checked = cacheNodeById.Settings.EnableComment;
                this.ChkEnableTouristsComment.Checked = cacheNodeById.Settings.EnableTouristsComment;
                this.ChkCommentNeedCheck.Checked = cacheNodeById.Settings.CommentNeedCheck;
                this.DropWorkFlow.SelectedValue = cacheNodeById.WorkFlowId.ToString();
                this.TxtPresentExp.Text = cacheNodeById.Settings.PresentExp.ToString();
                this.TxtDefaultItemPoint.Text = cacheNodeById.Settings.DefaultItemPoint.ToString();
                this.ShowChargeType.ChargeType = cacheNodeById.Settings.DefaultItemChargeType;
                this.ShowChargeType.PitchTime = cacheNodeById.Settings.DefaultItemPitchTime;
                this.ShowChargeType.ReadTimes = cacheNodeById.Settings.DefaultItemReadTimes;
                this.TxtDefaultItemDividePercent.Text = cacheNodeById.Settings.DefaultItemDividePercent.ToString();
                this.FileCdefaultListTmeplate.Text = cacheNodeById.DefaultTemplateFile;
                this.FileContainChildTemplate.Text = cacheNodeById.ContainChildTemplateFile;
            }
        }

        private void InitialPage()
        {
            bool flag;
            int nodeId = BasePage.RequestInt32("NodeID");
            string action = BasePage.RequestStringToLower("Action", "add");
            this.InitCreateHtmlControl();
            if (action == "modify")
            {
                NodeInfo cacheNodeById = EasyOne.Contents.Nodes.GetCacheNodeById(nodeId);
                this.TrNodeId.Visible = true;
                this.LitNodeId.Text = nodeId.ToString();
                this.HdnNodeId.Value = cacheNodeById.ParentId.ToString();
                switch (cacheNodeById.NodeType)
                {
                    case NodeType.Single:
                        BasePage.ResponseRedirect("Single.aspx?Action=modify&NodeID=" + nodeId);
                        break;

                    case NodeType.Link:
                        BasePage.ResponseRedirect("OutLink.aspx?Action=modify&NodeID=" + nodeId);
                        break;
                }
            }
            int parentId = BasePage.RequestInt32("ParentID");
            this.initDataTable = ModelManager.GetModelListByNodeId(nodeId, true);
            if (!this.Page.IsPostBack)
            {
                this.RadNeedCache0.Attributes.Add("onclick", "javascript:TrSetCacheTime.style.display='none';");
                this.RadNeedCache1.Attributes.Add("onclick", "javascript:TrSetCacheTime.style.display='';");
                IList<NodeInfo> nodeNameForContainerItems = EasyOne.Contents.Nodes.GetNodeNameForContainerItems();
                this.DropParentNode.DataSource = nodeNameForContainerItems;
                this.DropParentNode.DataBind();
                this.LstRelationNodes.DataSource = EasyOne.Contents.Nodes.GetNodeNameForItemsExceptOutLinks();
                this.LstRelationNodes.DataTextField = "NodeName";
                this.LstRelationNodes.DataValueField = "NodeId";
                this.LstRelationNodes.DataBind();
                IList<UserGroupsInfo> userGroupList = UserGroups.GetUserGroupList(0, 0);
                this.EgvPermissions.DataSource = userGroupList;
                this.EgvPermissions.DataBind();
                this.EgvRoleView.DataSource = UserRole.GetRoleList();
                this.EgvRoleView.DataBind();
                IList<ModelInfo> modelList = ModelManager.GetModelList(ModelType.Content, ModelShowType.Enable);
                this.RepContentModelTemplate.DataSource = modelList;
                this.RepContentModelTemplate.DataBind();
                if (string.Compare(SiteConfig.SiteInfo.ProductEdition, "eshop", StringComparison.OrdinalIgnoreCase) == 0)
                {
                    this.RepShopModelTemplate.DataSource = ModelManager.GetModelList(ModelType.Shop, ModelShowType.Enable);
                    this.RepShopModelTemplate.DataBind();
                }
                IList<WorkFlowsInfo> workFlowsList = WorkFlow.GetWorkFlowsList();
                this.DropWorkFlow.DataSource = workFlowsList;
                this.DropWorkFlow.DataBind();
                this.LstRelationSpecial.DataSource = Special.GetSpecialList();
                this.LstRelationSpecial.DataBind();
                ((DropDownList) this.PnlCustomFileds.FindControl("DropCustomNum")).Attributes.Add("onchange", "setFileFileds(this.value)");
                this.DropParentNode.Attributes.Add("onchange", "ShowNodeDir(this.value);");
            }
            this.DetectionPermissions(action);
            string str2 = action;
            if (str2 != null)
            {
                if (!(str2 == "add"))
                {
                    if (str2 == "modify")
                    {
                        this.SmpNavigator.CurrentNode = "修改栏目设置：";
                        this.EBtnModify.Visible = true;
                        this.EbtnDelete.Visible = true;
                        if (!this.Page.IsPostBack)
                        {
                            this.BindNodesInfo(nodeId);
                        }
                        goto Label_0566;
                    }
                    if (str2 == "copy")
                    {
                        if (!this.Page.IsPostBack)
                        {
                            nodeId = EasyOne.Contents.Nodes.GetNodeByIdCopyNode(nodeId);
                            if (nodeId > 0)
                            {
                                this.InputPermissions(nodeId);
                                if (PEContext.Current.Admin.IsSuperAdmin)
                                {
                                    this.InputRolePermission(nodeId);
                                }
                                UpdatePurviewType(nodeId, "Add");
                                IncludeFile.CreateIncludeFileByAssociateType(AssociateType.Node);
                                base.Response.Write("<script type='text/javascript'>parent.frames[\"left\"].location.reload()</script>");
                                AdminPage.WriteSuccessMsg("复制节点成功！", "Category.aspx?Action=Modify&NodeID=" + nodeId);
                            }
                            else if (nodeId == -1)
                            {
                                this.InputPermissions(nodeId);
                                if (PEContext.Current.Admin.IsSuperAdmin)
                                {
                                    this.InputRolePermission(nodeId);
                                }
                                UpdatePurviewType(nodeId, "Add");
                                IncludeFile.CreateIncludeFileByAssociateType(AssociateType.Node);
                                base.Response.Write("<script type='text/javascript'>parent.frames[\"left\"].location.reload()</script>");
                                AdminPage.WriteSuccessMsg("复制节点成功！<br />模型关系复制失败！", "Category.aspx?Action=Modify&NodeID=" + nodeId);
                            }
                            else
                            {
                                AdminPage.WriteErrMsg("发生错误！");
                            }
                        }
                        goto Label_0566;
                    }
                }
                else
                {
                    if (!this.Page.IsPostBack)
                    {
                        if (parentId > 0)
                        {
                            this.DropParentNode.SelectedValue = parentId.ToString();
                            this.InitFromParentNode(parentId);
                            this.SmpNavigator.CurrentNode = "添加子栏目";
                            this.SetPurviewType(parentId);
                        }
                        else if (nodeId > 0)
                        {
                            parentId = EasyOne.Contents.Nodes.GetCacheNodeById(nodeId).ParentId;
                            this.SetPurviewType(parentId);
                            this.InitFromParentNode(parentId);
                            this.SmpNavigator.CurrentNode = "添加平级栏目";
                        }
                        else
                        {
                            if (BasePage.RequestString("AddType") == "BatchCategory")
                            {
                                this.SmpNavigator.CurrentNode = "批量添加栏目";
                            }
                            else
                            {
                                this.SmpNavigator.CurrentNode = "添加栏目";
                            }
                            this.RadPurviewType0.Checked = true;
                        }
                    }
                    this.LblNodeName.Visible = false;
                    this.EBtnAdd.Visible = true;
                    if (BasePage.RequestString("AddType").CompareTo("BatchCategory") == 0)
                    {
                        this.NodeName.Visible = false;
                        this.NodeIdentifier.Visible = false;
                        this.NodeDir.Visible = false;
                        this.BatchCategory.Visible = true;
                        this.TxtNodeNames.Attributes.Add("onchange", "GetBatchInitial();");
                    }
                    else
                    {
                        this.TxtNodeName.Attributes.Add("onchange", "GetInitial();");
                    }
                    goto Label_0566;
                }
            }
            this.SmpNavigator.CurrentNode = "添加栏目";
            this.EBtnAdd.Visible = true;
        Label_0566:
            flag = RolePermissions.AccessCheck(OperateCode.AdministratorManage);
            bool flag2 = RolePermissions.AccessCheck(OperateCode.UserGroupManage);
            if (!flag)
            {
                this.TdRolePermissions.Style.Add("display", "none");
            }
            if (!flag2)
            {
                this.TdGroupPermissions.Style.Add("display", "none");
            }
            if (!flag && !flag2)
            {
                this.TabTitle6.Style.Add("display", "none");
            }
            if (!SiteConfig.SiteOption.EnablePointMoneyExp)
            {
                this.TabTitle3.Style.Add("display", "none");
            }
        }

        private void InputPermissions(int nodeId)
        {
            string selectId = "";
            StringBuilder roleIdList = new StringBuilder();
            StringBuilder builder2 = new StringBuilder();
            StringBuilder builder3 = new StringBuilder();
            for (int i = 0; i < this.EgvPermissions.Rows.Count; i++)
            {
                CheckBox box3 = (CheckBox) this.EgvPermissions.Rows[i].FindControl("ChkNodeSkim");
                CheckBox box = (CheckBox) this.EgvPermissions.Rows[i].FindControl("ChkNodeShow");
                CheckBox box2 = (CheckBox) this.EgvPermissions.Rows[i].FindControl("ChkNodeInput");
                selectId = this.EgvPermissions.DataKeys[i].Value.ToString();
                AppendSelectId(box3.Checked, builder3, selectId);
                AppendSelectId(box.Checked, roleIdList, selectId);
                AppendSelectId(box2.Checked, builder2, selectId);
            }
            if (this.RadPurviewType0.Checked)
            {
                this.AddPermissions(builder2.ToString(), OperateCode.NodeContentInput, nodeId, "栏目录入权限添加失败！");
            }
            if (this.RadPurviewType1.Checked)
            {
                this.AddPermissions(roleIdList.ToString(), OperateCode.NodeContentPreview, nodeId, "栏目查看权限添加失败！");
                this.AddPermissions(builder2.ToString(), OperateCode.NodeContentInput, nodeId, "栏目录入权限添加失败！");
            }
            if (this.RadPurviewType2.Checked)
            {
                this.AddPermissions(builder3.ToString(), OperateCode.NodeContentSkim, nodeId, "栏目浏览权限添加失败！");
                this.AddPermissions(roleIdList.ToString(), OperateCode.NodeContentPreview, nodeId, "栏目查看权限添加失败！");
                this.AddPermissions(builder2.ToString(), OperateCode.NodeContentInput, nodeId, "栏目录入权限添加失败！");
            }
        }

        private void InputRolePermission(int nodeId)
        {
            StringBuilder roleIdList = new StringBuilder();
            StringBuilder builder2 = new StringBuilder();
            StringBuilder builder3 = new StringBuilder();
            StringBuilder builder4 = new StringBuilder();
            StringBuilder builder5 = new StringBuilder();
            StringBuilder builder6 = new StringBuilder();
            for (int i = 0; i < this.EgvRoleView.Rows.Count; i++)
            {
                CheckBox box = (CheckBox) this.EgvRoleView.Rows[i].FindControl("ChkNodePreview");
                CheckBox box2 = (CheckBox) this.EgvRoleView.Rows[i].FindControl("ChkNodeInput");
                CheckBox box3 = (CheckBox) this.EgvRoleView.Rows[i].FindControl("ChkNodeCheck");
                CheckBox box4 = (CheckBox) this.EgvRoleView.Rows[i].FindControl("ChkContentManage");
                CheckBox box5 = (CheckBox) this.EgvRoleView.Rows[i].FindControl("ChkNodeManage");
                CheckBox box6 = (CheckBox) this.EgvRoleView.Rows[i].FindControl("ChkCommentManage");
                string selectId = this.EgvRoleView.DataKeys[i].Value.ToString();
                AppendSelectId(box.Checked, roleIdList, selectId);
                AppendSelectId(box2.Checked, builder2, selectId);
                AppendSelectId(box3.Checked, builder3, selectId);
                AppendSelectId(box4.Checked, builder4, selectId);
                AppendSelectId(box5.Checked, builder5, selectId);
                AppendSelectId(box6.Checked, builder6, selectId);
            }
            RolePermissions.DeleteNodePermissionFromRoles(-1, nodeId);
            RolePermissions.AddNodePermissionToRoles(roleIdList.ToString(), nodeId, OperateCode.NodeContentPreview);
            RolePermissions.AddNodePermissionToRoles(builder2.ToString(), nodeId, OperateCode.NodeContentInput);
            RolePermissions.AddNodePermissionToRoles(builder3.ToString(), nodeId, OperateCode.NodeContentCheck);
            RolePermissions.AddNodePermissionToRoles(builder5.ToString(), nodeId, OperateCode.CurrentNodesManage);
            RolePermissions.AddNodePermissionToRoles(builder4.ToString(), nodeId, OperateCode.NodeContentManage);
            RolePermissions.AddNodePermissionToRoles(builder6.ToString(), nodeId, OperateCode.NodeCommentManage);
        }

        public static bool IsDir(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return false;
            }
            return !Regex.IsMatch(input, "[_a-zA-Z\r\n][_a-zA-Z0-9\r\n]*");
        }

        private void ListBoxSetValue(ListBox Lst, string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                foreach (string str in value.Split(new char[] { ',' }))
                {
                    if (Lst.Items.FindByValue(str) != null)
                    {
                        Lst.Items.FindByValue(str).Selected = true;
                    }
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!EasyOne.Contents.Nodes.CheckRoleNodePurview(BasePage.RequestInt32("NodeID"), OperateCode.CurrentNodesManage))
            {
                AdminPage.WriteErrMsg("<li>对不起，您没有当前栏目的管理权限！</li>");
            }
            this.InitialPage();
        }

        protected void RadPurviewType_CheckedChanged(object sender, EventArgs e)
        {
            IList<UserGroupsInfo> userGroupList = UserGroups.GetUserGroupList(0, 0);
            this.EgvPermissions.DataSource = userGroupList;
            this.EgvPermissions.DataBind();
        }

        protected void RepModelTemplate_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
            {
                int nodeId = BasePage.RequestInt32("NodeID");
                string str = BasePage.RequestStringToLower("Action", "add");
                ModelInfo dataItem = (ModelInfo) e.Item.DataItem;
                if (!string.IsNullOrEmpty(dataItem.DefaultTemplateFile))
                {
                    ((TextBox) e.Item.FindControl("FileCTemplate")).Text = dataItem.DefaultTemplateFile;
                }
                string str2 = str;
                if (str2 != null)
                {
                    if (!(str2 == "add"))
                    {
                        if (!(str2 == "modify"))
                        {
                            return;
                        }
                    }
                    else
                    {
                        NodesModelTemplateRelationShipInfo info2 = ModelManager.GetNodesModelTemplateRelationShip(BasePage.RequestInt32("ParentID"), dataItem.ModelId);
                        if ((info2.NodeId > 0) && (info2.ModelId > 0))
                        {
                            ((CheckBox) e.Item.FindControl("ChkModel")).Checked = true;
                            ((TextBox) e.Item.FindControl("FileCTemplate")).Text = info2.DefaultTemplateFile;
                        }
                        return;
                    }
                    NodesModelTemplateRelationShipInfo nodesModelTemplateRelationShip = ModelManager.GetNodesModelTemplateRelationShip(nodeId, dataItem.ModelId);
                    if ((nodesModelTemplateRelationShip.NodeId > 0) && (nodesModelTemplateRelationShip.ModelId > 0))
                    {
                        ((CheckBox) e.Item.FindControl("ChkModel")).Checked = true;
                        ((TextBox) e.Item.FindControl("FileCTemplate")).Text = nodesModelTemplateRelationShip.DefaultTemplateFile;
                    }
                }
            }
        }

        protected void SetPurviewType(int parentId)
        {
            int purviewType = 0;
            if (parentId > 0)
            {
                purviewType = EasyOne.Contents.Nodes.GetCacheNodeById(parentId).PurviewType;
            }
            switch (purviewType)
            {
                case 0:
                    this.RadPurviewType0.Enabled = true;
                    this.RadPurviewType0.Checked = true;
                    this.RadPurviewType1.Enabled = true;
                    this.RadPurviewType1.Checked = false;
                    this.RadPurviewType2.Enabled = true;
                    this.RadPurviewType2.Checked = false;
                    break;

                case 1:
                    this.RadPurviewType0.Enabled = false;
                    this.RadPurviewType0.Checked = false;
                    this.RadPurviewType1.Enabled = true;
                    this.RadPurviewType1.Checked = true;
                    this.RadPurviewType2.Enabled = true;
                    this.RadPurviewType2.Checked = false;
                    break;

                case 2:
                    this.RadPurviewType0.Enabled = false;
                    this.RadPurviewType0.Checked = false;
                    this.RadPurviewType1.Enabled = false;
                    this.RadPurviewType1.Checked = false;
                    this.RadPurviewType2.Enabled = true;
                    this.RadPurviewType2.Checked = true;
                    break;
            }
            IList<UserGroupsInfo> userGroupList = UserGroups.GetUserGroupList(0, 0);
            this.EgvPermissions.DataSource = userGroupList;
            this.EgvPermissions.DataBind();
        }

        protected void SetPurviewType(int parentId, int purviewType)
        {
            int num = 0;
            if (parentId > 0)
            {
                num = EasyOne.Contents.Nodes.GetCacheNodeById(parentId).PurviewType;
            }
            switch (num)
            {
                case 0:
                    this.RadPurviewType0.Enabled = true;
                    this.RadPurviewType1.Enabled = true;
                    this.RadPurviewType2.Enabled = true;
                    break;

                case 1:
                    this.RadPurviewType0.Enabled = false;
                    this.RadPurviewType1.Enabled = true;
                    this.RadPurviewType2.Enabled = true;
                    break;

                case 2:
                    this.RadPurviewType0.Enabled = false;
                    this.RadPurviewType1.Enabled = false;
                    this.RadPurviewType2.Enabled = true;
                    break;
            }
            switch (purviewType)
            {
                case 0:
                    this.RadPurviewType0.Checked = true;
                    this.RadPurviewType2.Checked = false;
                    this.RadPurviewType1.Checked = false;
                    break;

                case 1:
                    this.RadPurviewType0.Checked = false;
                    this.RadPurviewType1.Checked = true;
                    this.RadPurviewType2.Checked = false;
                    break;

                case 2:
                    this.RadPurviewType0.Checked = false;
                    this.RadPurviewType1.Checked = false;
                    this.RadPurviewType2.Checked = true;
                    break;
            }
            IList<UserGroupsInfo> userGroupList = UserGroups.GetUserGroupList(0, 0);
            this.EgvPermissions.DataSource = userGroupList;
            this.EgvPermissions.DataBind();
        }

        private static void UpdatePurviewType(int nodeId, string action)
        {
            if (!EasyOne.Contents.Nodes.UpdateNodePurviewType(nodeId))
            {
                AdminPage.WriteErrMsg("更新当前栏目权限失败！");
            }
            if (action == "Modify")
            {
                NodeInfo cacheNodeById = EasyOne.Contents.Nodes.GetCacheNodeById(nodeId);
                if (!EasyOne.Contents.Nodes.UpdateChildPurview(cacheNodeById.NodeId, cacheNodeById.PurviewType))
                {
                    AdminPage.WriteErrMsg("更新子栏目权限失败！", "Category.aspx");
                }
            }
        }
    }
}

