namespace EasyOne.WebSite.Admin.Contents
{
    using EasyOne.AccessManage;
    using EasyOne.Common;
    using EasyOne.CommonModel;
    using EasyOne.Components;
    using EasyOne.Contents;
    using EasyOne.Controls;
    using EasyOne.Enumerations;
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
    using System.Text;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using EasyOne.Model.WorkFlow;

    public partial class CategoryBatchSet : AdminPage
    {

        private static void AppendSelectId(bool isChecked, StringBuilder roleIdList, string selectId)
        {
            if (isChecked)
            {
                StringHelper.AppendString(roleIdList, selectId);
            }
        }

        protected void BtnCancel_Click(object sender, EventArgs e)
        {
            BasePage.ResponseRedirect("CategoryManage.aspx");
        }

        protected void EBtnBacthSet_Click(object sender, EventArgs e)
        {
            if (this.Page.IsValid)
            {
                StringBuilder sb = new StringBuilder();
                foreach (ListItem item in this.LstNodes.Items)
                {
                    int num;
                    if (item.Selected && int.TryParse(item.Value, out num))
                    {
                        StringHelper.AppendString(sb, item.Value);
                    }
                }
                if (sb.Length <= 0)
                {
                    AdminPage.WriteErrMsg("请先选择要批量设置的节点！");
                }
                if (Nodes.BatchUpdate(this.GetNodesInfo(), sb.ToString(), this.GetCheckItem()))
                {
                    if (this.ChkPermissions.Checked)
                    {
                        foreach (string str in sb.ToString().Split(new char[] { ',' }))
                        {
                            this.InputPermissions(DataConverter.CLng(str));
                            this.InputRolePermission(DataConverter.CLng(str));
                        }
                    }
                    if (this.ChkFileCTemplate.Checked)
                    {
                        IList<NodesModelTemplateRelationShipInfo> dataFromRepeater = this.GetDataFromRepeater();
                        foreach (string str2 in sb.ToString().Split(new char[] { ',' }))
                        {
                            ModelManager.UpdateNodesModelTemplateRelationShip(DataConverter.CLng(str2), dataFromRepeater);
                        }
                    }
                    IncludeFile.CreateIncludeFileByAssociateType(AssociateType.Node);
                    base.Response.Write("<script type='text/javascript'>parent.frames[\"left\"].location.reload()</script>");
                    AdminPage.WriteSuccessMsg("批量设置成功！", "CategoryManage.aspx");
                }
                else
                {
                    AdminPage.WriteErrMsg("批量设置失败！");
                }
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
                    if (info2.OperateCode == OperateCode.ChildNodesManage)
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

        private Dictionary<string, bool> GetCheckItem()
        {
            Dictionary<string, bool> dictionary = new Dictionary<string, bool>();
            dictionary.Add("OpenType", this.ChkOpenType.Checked);
            dictionary.Add("PurviewType", this.ChkPurviewType.Checked);
            dictionary.Add("EnableComment", this.ChkEnableComment2.Checked);
            dictionary.Add("WorkFlowId", this.ChkWorkFlow.Checked);
            dictionary.Add("EnableProtect", this.ChkEnableProtect.Checked);
            dictionary.Add("EnableAddWhenHasChild", this.ChkEnableAddWhenHasChild.Checked);
            dictionary.Add("HitsOfHot", this.ChkHitsOfHot.Checked);
            dictionary.Add("IsSetCache", this.ChkIsSetCache.Checked);
            dictionary.Add("FileCdefaultListTmeplate", this.ChkFileCdefaultListTmeplate.Checked);
            dictionary.Add("FileContainChildTemplate", this.ChkFileContainChildTemplate.Checked);
            dictionary.Add("FileCTemplate", this.ChkFileCTemplate.Checked);
            dictionary.Add("PresentExp", this.ChkPresentExp.Checked);
            dictionary.Add("DefaultItemPoint", this.ChkDefaultItemPoint.Checked);
            dictionary.Add("ShowChargeType", this.ChkShowChargeType.Checked);
            dictionary.Add("DefaultItemDividePercent", this.ChkDefaultItemDividePercent.Checked);
            dictionary.Add("ShowOnMenu", this.ChkShowOnMenu.Checked);
            dictionary.Add("ShowOnPath", this.ChkShowOnPath.Checked);
            dictionary.Add("ShowOnMap", this.ChkShowOnMap.Checked);
            dictionary.Add("ShowOnListIndex", this.ChkShowOnListIndex.Checked);
            dictionary.Add("ShowOnListParent", this.ChkShowOnListParent.Checked);
            dictionary.Add("ItemPageSize", this.ChkItemPageSize.Checked);
            dictionary.Add("ItemOpenType", this.ChkItemOpenType.Checked);
            dictionary.Add("ItemListOrderType", this.ChkItemListOrderType.Checked);
            dictionary.Add("ListPageCreateHtmlType", this.ChkListPageCreateHtmlType.Checked);
            dictionary.Add("AutoCreateHtmlType", this.ChkAutoCreateHtmlType.Checked);
            dictionary.Add("Relation", this.ChkRelation.Checked);
            dictionary.Add("ListPageHtmlDirType", this.ChkListPageHtmlDirType.Checked);
            dictionary.Add("PagePostfix", this.ChkPagePostfix.Checked);
            dictionary.Add("ContentPageCreateHtmlType", this.ChkContentPageCreateHtmlType.Checked);
            dictionary.Add("ContentHtmlDir", this.ChkContentHtmlDir.Checked);
            return dictionary;
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
            NodeInfo info = new NodeInfo();
            NodeSettingInfo info2 = new NodeSettingInfo();
            if (this.RadOpenType0.Checked)
            {
                info.OpenType = 0;
            }
            if (this.RadOpenType1.Checked)
            {
                info.OpenType = 1;
            }
            if (this.RadPurviewType0.Checked)
            {
                info.PurviewType = 0;
            }
            if (this.RadPurviewType1.Checked)
            {
                info.PurviewType = 1;
            }
            if (this.RadPurviewType2.Checked)
            {
                info.PurviewType = 2;
            }
            info2.EnableComment = this.ChkEnableComment.Checked;
            info2.CommentNeedCheck = this.ChkCommentNeedCheck.Checked;
            info2.EnableTouristsComment = this.ChkEnableTouristsComment.Checked;
            info.WorkFlowId = DataConverter.CLng(this.DropWorkFlow.SelectedValue);
            info2.EnableProtect = Convert.ToBoolean(this.RadlEnableProtect.SelectedValue);
            info2.EnableAddWhenHasChild = Convert.ToBoolean(this.RadlEnableAddWhenHasChild.SelectedValue);
            info.HitsOfHot = DataConverter.CLng(this.TxtHitsOfHot.Text);
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
            info.DefaultTemplateFile = this.FileCdefaultListTmeplate.Text;
            info.ContainChildTemplateFile = this.FileContainChildTemplate.Text;
            info.ShowOnMenu = Convert.ToBoolean(this.RadlShowOnMenu.SelectedValue);
            info.ShowOnPath = Convert.ToBoolean(this.RadlShowOnPath.SelectedValue);
            info.ShowOnMap = Convert.ToBoolean(this.RadlShowOnMap.SelectedValue);
            info.ShowOnListIndex = Convert.ToBoolean(this.RadlShowOnListIndex.SelectedValue);
            info.ShowOnListParent = Convert.ToBoolean(this.RadlShowOnListParent.SelectedValue);
            info.ItemListOrderType = DataConverter.CLng(this.DrpItemListOrderType.SelectedValue);
            info.ItemOpenType = DataConverter.CLng(this.DrpItemOpenType.SelectedValue);
            info.ItemPageSize = DataConverter.CLng(this.CombItemPageSize.Value);
            info.IsCreateContentPage = DataConverter.CBoolean(this.RadlIsContentPageCreate.SelectedValue);
            info.IsCreateListPage = DataConverter.CBoolean(this.RadlIsListPageCreate.SelectedValue);
            info.ListPageSavePathType = (ListPagePathType) Enum.Parse(typeof(ListPagePathType), this.RadlListPageHtmlDirType.SelectedValue);
            info.ListPagePostfix = this.PagePostfix.Value;
            info.AutoCreateHtmlType = (AutoCreateHtmlType) Enum.Parse(typeof(AutoCreateHtmlType), this.RadlAutoCreateHtmlType.SelectedValue);
            info.ContentPageHtmlRule = this.TxtContentHtmlDir.Value + "/" + this.TxtContentHtmlFile.Value + "." + this.TxtContentHtmlExt.Value;
            info.RelateNode = this.GetSelectArrFromListControl(this.LstRelationNodes).ToString();
            info.RelateSpecial = this.GetSelectArrFromListControl(this.LstRelationSpecial).ToString();
            info.Settings = info2;
            info.RootId = 0;
            return info;
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
            if (!this.Page.IsPostBack)
            {
                string text = "列表文件分目录保存在所属栏目的文件夹中<br/><span style=\"color: blue\">例：Article/News/Index.html（栏目首页）<br /> Article/News/List_1.html<span>（栏目列表页）";
                string str2 = "列表文件统一保存在指定的“List”文件夹中<br/><span style=\"color: blue\">例：Article/List/List_236.html（栏目首页）<br /> Article/List/List_236_1.html（第二页）";
                string str3 = "列表文件统一保存在一级栏目文件夹中<br/><span style=\"color: blue\">例：Article/List_236.html（栏目首页）<br /> Article/List_236_1.html<span>（第二页）";
                this.RadlListPageHtmlDirType.Items.Add(new ListItem(text, "NodePath"));
                this.RadlListPageHtmlDirType.Items.Add(new ListItem(str2, "ListPath"));
                this.RadlListPageHtmlDirType.Items.Add(new ListItem(str3, "RootPath"));
                BasePage.SetSelectedIndexByValue(this.RadlListPageHtmlDirType, "NodePath");
            }
        }

        private void Initial()
        {
            this.InitCreateHtmlControl();
            if (!this.Page.IsPostBack)
            {
                this.RadNeedCache0.Attributes.Add("onclick", "javascript:TrSetCacheTime.style.display='none';");
                this.RadNeedCache1.Attributes.Add("onclick", "javascript:TrSetCacheTime.style.display='';");
                IList<NodeInfo> nodeNameForContainerItems = Nodes.GetNodeNameForContainerItems();
                if (nodeNameForContainerItems.Count < 1)
                {
                    ListItem item = new ListItem("无节点，请先添加节点", "0");
                    item.Enabled = false;
                }
                else
                {
                    this.LstNodes.DataSource = nodeNameForContainerItems;
                    this.LstNodes.DataBind();
                }
            }
            StringBuilder builder = new StringBuilder();
            builder.Append("<script type=\"text/javascript\">");
            builder.Append("function SelectAll(){");
            builder.Append("for(var i=0;i<document.getElementById('");
            builder.Append(this.LstNodes.ClientID);
            builder.Append("').length;i++){");
            builder.Append("document.getElementById('");
            builder.Append(this.LstNodes.ClientID);
            builder.Append("').options[i].selected=true;}}");
            builder.Append("function UnSelectAll(){");
            builder.Append("for(var i=0;i<document.getElementById('");
            builder.Append(this.LstNodes.ClientID);
            builder.Append("').length;i++){");
            builder.Append("document.getElementById('");
            builder.Append(this.LstNodes.ClientID);
            builder.Append("').options[i].selected=false;}}");
            builder.Append("</script>");
            base.ClientScript.RegisterClientScriptBlock(base.GetType(), "Select", builder.ToString());
            if (!this.Page.IsPostBack)
            {
                IList<NodeInfo> nodeNameForItemsExceptOutLinks = Nodes.GetNodeNameForItemsExceptOutLinks();
                this.LstRelationNodes.DataSource = nodeNameForItemsExceptOutLinks;
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
                this.RepShopModelTemplate.DataSource = ModelManager.GetModelList(ModelType.Shop, ModelShowType.Enable);
                this.RepShopModelTemplate.DataBind();
                IList<WorkFlowsInfo> workFlowsList = WorkFlow.GetWorkFlowsList();
                this.DropWorkFlow.DataSource = workFlowsList;
                this.DropWorkFlow.DataBind();
                this.LstRelationSpecial.DataSource = Special.GetSpecialList();
                this.LstRelationSpecial.DataBind();
            }
            bool flag = RolePermissions.AccessCheck(OperateCode.AdministratorManage);
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
                this.TabTitle5.Style.Add("display", "none");
            }
            if (!SiteConfig.SiteOption.EnablePointMoneyExp)
            {
                this.TabTitle2.Style.Add("display", "none");
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
            UserPermissions.AddNodePermissions(builder2.ToString(), OperateCode.NodeContentInput, nodeId, 1);
            UserPermissions.AddNodePermissions(roleIdList.ToString(), OperateCode.NodeContentPreview, nodeId, 1);
            UserPermissions.AddNodePermissions(builder3.ToString(), OperateCode.NodeContentSkim, nodeId, 1);
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
            RolePermissions.AddNodePermissionToRoles(builder5.ToString(), nodeId, OperateCode.ChildNodesManage);
            RolePermissions.AddNodePermissionToRoles(builder4.ToString(), nodeId, OperateCode.NodeContentManage);
            RolePermissions.AddNodePermissionToRoles(builder6.ToString(), nodeId, OperateCode.NodeCommentManage);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Nodes.CheckRoleNodePurview(BasePage.RequestInt32("NodeID"), OperateCode.CurrentNodesManage))
            {
                AdminPage.WriteErrMsg("<li>对不起，您没有当前栏目的管理权限！</li>");
            }
            this.Initial();
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
                ModelInfo dataItem = (ModelInfo) e.Item.DataItem;
                if (!string.IsNullOrEmpty(dataItem.DefaultTemplateFile))
                {
                    ((TextBox) e.Item.FindControl("FileCTemplate")).Text = dataItem.DefaultTemplateFile;
                }
            }
        }
    }
}

