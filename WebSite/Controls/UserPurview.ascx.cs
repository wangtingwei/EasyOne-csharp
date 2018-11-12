namespace EasyOne.WebSite.Controls
{
    using EasyOne.AccessManage;
    using EasyOne.Common;
    using EasyOne.CommonModel;
    using EasyOne.Contents;
    using EasyOne.Controls;
    using EasyOne.Enumerations;
    using EasyOne.Model.AccessManage;
    using EasyOne.Model.CommonModel;
    using EasyOne.Model.Contents;
    using EasyOne.Web.UI;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public partial class UserPurview : BaseUserControl
    {
        private string ChkNodeInputId;
        private string ChkNodeShowId;
        private string ChkNodeSkimId;
        public StringBuilder m_ArrModelTr = new StringBuilder();
        public StringBuilder m_ArrTable = new StringBuilder();
        public StringBuilder m_ArrTabs = new StringBuilder();
        public StringBuilder m_ArrTitle = new StringBuilder();
        private StringBuilder m_fieldNameList = new StringBuilder();
        private int m_GroupId;
        private int m_IdType;
        private StringBuilder m_inputNodeIdList = new StringBuilder();
        private bool m_inputSpecialAll;
        private string m_inputSpecialId;
        private StringBuilder m_inputSpecialIds = new StringBuilder();
        private StringBuilder m_modelIdList = new StringBuilder();
        private bool m_NodeInputAll;
        private bool m_NodeShowAll;
        private bool m_NodeSkimAll;
        private StringBuilder m_showNodeIdList = new StringBuilder();
        private StringBuilder m_SkimNodeIdList = new StringBuilder();
        private StringBuilder m_strMsg = new StringBuilder();

        private void AddContentFieldManagePermission()
        {
            if (!UserPermissions.AddFieldPermissions(this.m_GroupId, this.m_modelIdList.ToString(), this.m_fieldNameList.ToString(), OperateCode.ContentFieldInput, this.IdType))
            {
                this.m_strMsg.Append("<li>模型字段权限添加失败！</li>");
            }
        }

        private void AddNodesPermission()
        {
            UserPermissions.DeleteNodePermissions(this.m_GroupId, this.IdType);
            if (!UserPermissions.AddNodePermissions(this.m_GroupId, OperateCode.NodeContentInput, this.m_inputNodeIdList.ToString(), this.IdType))
            {
                this.m_strMsg.Append("<li>节点录入权限添加失败！</li>");
            }
            if (!UserPermissions.AddNodePermissions(this.m_GroupId, OperateCode.NodeContentSkim, this.m_SkimNodeIdList.ToString(), this.IdType))
            {
                this.m_strMsg.Append("<li>节点录入审核添加失败！</li>");
            }
            if (!UserPermissions.AddNodePermissions(this.m_GroupId, OperateCode.NodeContentPreview, this.m_showNodeIdList.ToString(), this.IdType))
            {
                this.m_strMsg.Append("<li>节点操作管理修改失败！</li>");
            }
        }

        private void AddSpecialInfosPermissions()
        {
            if (!UserPermissions.AddSpecialPermissions(this.m_GroupId, OperateCode.SpecialContentInput, this.m_inputSpecialIds.ToString(), this.IdType))
            {
                this.m_strMsg.Append("<li>专题信息录入权限添加失败！</li>");
            }
        }

        private static void AppendSelectId(bool isChecked, string selectId, StringBuilder roleIdList)
        {
            if (isChecked)
            {
                StringHelper.AppendString(roleIdList, selectId);
            }
        }

        protected void EgvNodes_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                NodeInfo dataItem = (NodeInfo) e.Row.DataItem;
                CheckBox box = (CheckBox) e.Row.FindControl("ChkNodeShow");
                CheckBox box2 = (CheckBox) e.Row.FindControl("ChkNodeInput");
                CheckBox box3 = (CheckBox) e.Row.FindControl("ChkNodeSkim");
                Label label = (Label) e.Row.FindControl("LabNodeShowTree");
                label.Text = Nodes.GetTreeLine(dataItem.Depth, dataItem.ParentPath, dataItem.NextId, dataItem.Child) + dataItem.NodeName + Nodes.GetNodeDir(dataItem.Child, dataItem.NodeType, dataItem.NodeDir);
                if (dataItem.NodeId == -1)
                {
                    this.ChkNodeSkimId = box3.ClientID;
                    this.ChkNodeShowId = box.ClientID;
                    this.ChkNodeInputId = box2.ClientID;
                    box3.Attributes.Add("onclick", "ChkNodeAll(this.form,'" + box3.ID + "'," + box3.ClientID + ")");
                    box.Attributes.Add("onclick", "ChkNodeAll(this.form,'" + box.ID + "'," + box.ClientID + ")");
                    box2.Attributes.Add("onclick", "ChkNodeAll(this.form,'" + box2.ID + "'," + box2.ClientID + ")");
                }
                else
                {
                    box3.Attributes.Add("onclick", "ChkWipeOffNodeAll(" + this.ChkNodeSkimId + ")");
                    box.Attributes.Add("onclick", "ChkWipeOffNodeAll(" + this.ChkNodeShowId + ")");
                    box2.Attributes.Add("onclick", "ChkWipeOffNodeAll(" + this.ChkNodeInputId + ")");
                }
            }
        }

        protected void EgvSpecial_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                SpecialTree dataItem = (SpecialTree) e.Row.DataItem;
                Label label = (Label) e.Row.FindControl("LabName");
                CheckBox box = (CheckBox) e.Row.FindControl("ChkSpecialInput");
                HiddenField field = (HiddenField) e.Row.FindControl("HdnSpecialId");
                if (dataItem != null)
                {
                    label.Text = Special.TreeLine(dataItem.TreeLineType) + dataItem.Name;
                    if (dataItem.IsSpecialCategory)
                    {
                        field.Value = "0";
                        box.Visible = false;
                    }
                    else
                    {
                        field.Value = dataItem.Id.ToString();
                    }
                    if (!dataItem.IsSpecialCategory)
                    {
                        if (dataItem.Id == -1)
                        {
                            this.m_inputSpecialId = box.ClientID;
                            box.Attributes.Add("onclick", "ChkSpecialAll(this.form,'" + box.ID + "'," + this.m_inputSpecialId + ")");
                        }
                        else
                        {
                            box.Attributes.Add("onclick", "ChkWipeOffSpecialAll(" + this.m_inputSpecialId + ")");
                        }
                        foreach (RoleSpecialPermissionsInfo info in UserPermissions.GetSpecialPermissionsBySpecialId(dataItem.Id, this.IdType))
                        {
                            if ((info.OperateCode == OperateCode.SpecialContentInput) && (info.GroupId == this.m_GroupId))
                            {
                                if (dataItem.Id == -1)
                                {
                                    this.m_inputSpecialAll = true;
                                    box.Checked = true;
                                    break;
                                }
                                if (!this.m_inputSpecialAll)
                                {
                                    box.Checked = true;
                                }
                            }
                        }
                    }
                }
            }
        }

        private void GetModelPermission()
        {
            foreach (RepeaterItem item in this.RptModelList2.Items)
            {
                Repeater repeater = item.FindControl("RptFieldList") as Repeater;
                HiddenField field = item.FindControl("HdnModelId") as HiddenField;
                if (repeater == null)
                {
                    return;
                }
                foreach (RepeaterItem item2 in repeater.Items)
                {
                    CheckBox box = item2.FindControl("ChkFieldPurview") as CheckBox;
                    HiddenField field2 = item2.FindControl("HdnFieldName") as HiddenField;
                    if (box.Checked)
                    {
                        StringHelper.AppendString(this.m_modelIdList, field.Value);
                        StringHelper.AppendString(this.m_fieldNameList, field2.Value);
                    }
                }
            }
            this.AddContentFieldManagePermission();
        }

        private void GetNodeSelectPermission()
        {
            string selectId = "";
            CheckBox box5 = (CheckBox) this.EgvNodes.Rows[0].FindControl("ChkNodeSkim");
            CheckBox box4 = (CheckBox) this.EgvNodes.Rows[0].FindControl("ChkNodeShow");
            CheckBox box6 = (CheckBox) this.EgvNodes.Rows[0].FindControl("ChkNodeInput");
            for (int i = 0; i < this.EgvNodes.Rows.Count; i++)
            {
                selectId = this.EgvNodes.DataKeys[i].Value.ToString();
                CheckBox box3 = (CheckBox) this.EgvNodes.Rows[i].FindControl("ChkNodeSkim");
                CheckBox box = (CheckBox) this.EgvNodes.Rows[i].FindControl("ChkNodeShow");
                CheckBox box2 = (CheckBox) this.EgvNodes.Rows[i].FindControl("ChkNodeInput");
                AppendSelectId(box3.Checked || box5.Checked, selectId, this.m_SkimNodeIdList);
                AppendSelectId(box.Checked || box4.Checked, selectId, this.m_showNodeIdList);
                AppendSelectId(box2.Checked || box6.Checked, selectId, this.m_inputNodeIdList);
            }
            this.AddNodesPermission();
        }

        private void GetSpecialPermissions()
        {
            this.m_inputSpecialAll = ((CheckBox) this.EgvSpecial.Rows[0].FindControl("ChkSpecialInput")).Checked;
            foreach (GridViewRow row in this.EgvSpecial.Rows)
            {
                CheckBox box = (CheckBox) row.FindControl("ChkSpecialInput");
                HiddenField field = (HiddenField) row.FindControl("HdnSpecialId");
                if ((box.Checked && (field.Value != "0")) || (this.m_inputSpecialAll && (field.Value != "0")))
                {
                    StringHelper.AppendString(this.m_inputSpecialIds, field.Value);
                }
            }
            this.AddSpecialInfosPermissions();
        }

        private void InitTabsArray()
        {
            this.m_ArrTabs.Append("\"" + this.Tabs0.ClientID + "\",");
            this.m_ArrTabs.Append("\"" + this.Tabs1.ClientID + "\",");
            this.m_ArrTabs.Append("\"" + this.Tabs2.ClientID + "\"");
        }

        private void InitTitleArray()
        {
            this.m_ArrTitle.Append("\"" + this.TabTitle0.ClientID + "\",");
            this.m_ArrTitle.Append("\"" + this.TabTitle1.ClientID + "\",");
            this.m_ArrTitle.Append("\"" + this.TabTitle2.ClientID + "\"");
        }

        public void InputPermissions()
        {
            this.GetNodeSelectPermission();
            this.GetSpecialPermissions();
            this.GetModelPermission();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.InitTabsArray();
            this.InitTitleArray();
            if (this.GroupId == -2)
            {
                this.TabTitle1.Style.Add("display", "none");
                this.EgvNodes.Columns[2].Visible = false;
                this.EgvNodes.Columns[3].Visible = false;
            }
            IList<RoleNodePermissionsInfo> roleNodePermissionsList = UserPermissions.GetNodePermissionsById(this.GroupId, -2, this.IdType);
            this.SetNodeAll(roleNodePermissionsList);
            for (int i = 0; i < this.EgvNodes.Rows.Count; i++)
            {
                CheckBox box = (CheckBox) this.EgvNodes.Rows[i].FindControl("ChkNodeSkim");
                CheckBox box2 = (CheckBox) this.EgvNodes.Rows[i].FindControl("ChkNodeShow");
                CheckBox box3 = (CheckBox) this.EgvNodes.Rows[i].FindControl("ChkNodeInput");
                int num = DataConverter.CLng(this.EgvNodes.Rows[i].Cells[0].Text);
                for (int j = 0; j < roleNodePermissionsList.Count; j++)
                {
                    if (roleNodePermissionsList[j].NodeId == num)
                    {
                        if ((roleNodePermissionsList[j].OperateCode == OperateCode.NodeContentSkim) && !this.m_NodeSkimAll)
                        {
                            box.Checked = true;
                        }
                        if ((roleNodePermissionsList[j].OperateCode == OperateCode.NodeContentPreview) && !this.m_NodeShowAll)
                        {
                            box2.Checked = true;
                        }
                        if ((roleNodePermissionsList[j].OperateCode == OperateCode.NodeContentInput) && !this.m_NodeInputAll)
                        {
                            box3.Checked = true;
                        }
                    }
                }
            }
        }

        protected void RptModelList1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
            {
                HtmlTableRow row = (HtmlTableRow) e.Item.FindControl("ModelTr");
                if (this.m_ArrModelTr.Length == 0)
                {
                    this.m_ArrModelTr.Append("\"" + row.ClientID + "\"");
                }
                else
                {
                    this.m_ArrModelTr.Append(",\"" + row.ClientID + "\"");
                }
                if (e.Item.ItemIndex > 0)
                {
                    row.Attributes.Add("class", "tdbg");
                }
                else
                {
                    row.Attributes.Add("class", "title");
                }
                if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
                {
                    Label label = (Label) e.Item.FindControl("modellist");
                    label.Text = "<a href=\"javascript:Hidd(" + e.Item.ItemIndex.ToString() + ")\">" + ((ModelInfo) e.Item.DataItem).ModelName + "</a>";
                }
            }
        }

        protected void RptModelList2_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
            {
                Repeater repeater = (Repeater) e.Item.FindControl("RptFieldList");
                repeater.DataSource = ModelManager.GetFieldListByModelId(((ModelInfo) e.Item.DataItem).ModelId);
                repeater.DataBind();
                IList<RoleFieldPermissionsInfo> fieldPermissionsByGroupId = new List<RoleFieldPermissionsInfo>();
                fieldPermissionsByGroupId = UserPermissions.GetFieldPermissionsByGroupId(this.m_GroupId);
                HiddenField field = e.Item.FindControl("HdnModelId") as HiddenField;
                int num = DataConverter.CLng(field.Value);
                foreach (RepeaterItem item in repeater.Items)
                {
                    HiddenField field2 = item.FindControl("HdnFieldName") as HiddenField;
                    CheckBox box = item.FindControl("ChkFieldPurview") as CheckBox;
                    foreach (RoleFieldPermissionsInfo info in fieldPermissionsByGroupId)
                    {
                        if ((info.FieldName == field2.Value) && (info.ModelId == num))
                        {
                            box.Checked = true;
                        }
                    }
                    if (((field2.Value == "Title") || (field2.Value == "Status")) || (field2.Value == "NodeId"))
                    {
                        box.Enabled = false;
                    }
                }
                HtmlTable table = (HtmlTable) e.Item.FindControl("model");
                if (e.Item.ItemIndex > 0)
                {
                    table.Style.Add("display", "none");
                }
                if (this.m_ArrTable.Length == 0)
                {
                    this.m_ArrTable.Append("\"" + table.ClientID + "\"");
                }
                else
                {
                    this.m_ArrTable.Append(",\"" + table.ClientID + "\"");
                }
            }
        }

        private void SetNodeAll(IList<RoleNodePermissionsInfo> roleNodePermissionsList)
        {
            CheckBox box = (CheckBox) this.EgvNodes.Rows[0].FindControl("ChkNodeSkim");
            CheckBox box2 = (CheckBox) this.EgvNodes.Rows[0].FindControl("ChkNodeShow");
            CheckBox box3 = (CheckBox) this.EgvNodes.Rows[0].FindControl("ChkNodeInput");
            for (int i = 0; i < roleNodePermissionsList.Count; i++)
            {
                if (roleNodePermissionsList[i].NodeId == -1)
                {
                    if (roleNodePermissionsList[i].OperateCode == OperateCode.NodeContentSkim)
                    {
                        box.Checked = true;
                        this.m_NodeSkimAll = true;
                    }
                    if (roleNodePermissionsList[i].OperateCode == OperateCode.NodeContentPreview)
                    {
                        box2.Checked = true;
                        this.m_NodeShowAll = true;
                    }
                    if (roleNodePermissionsList[i].OperateCode == OperateCode.NodeContentInput)
                    {
                        box3.Checked = true;
                        this.m_NodeInputAll = true;
                    }
                }
            }
        }

        public int GroupId
        {
            get
            {
                return this.m_GroupId;
            }
            set
            {
                this.m_GroupId = value;
            }
        }

        public int IdType
        {
            get
            {
                return this.m_IdType;
            }
            set
            {
                this.m_IdType = value;
            }
        }

        public string StrMsg
        {
            get
            {
                return this.m_strMsg.ToString();
            }
        }
    }
}

