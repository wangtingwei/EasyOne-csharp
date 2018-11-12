namespace EasyOne.WebSite.Admin.User
{
    using EasyOne.AccessManage;
    using EasyOne.Common;
    using EasyOne.Contents;
    using EasyOne.Controls;
    using EasyOne.Enumerations;
    using EasyOne.ExtendedControls;
    using EasyOne.Model.Contents;
    using EasyOne.Web.UI;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Web.UI.WebControls;
    using EasyOne.Model.AccessManage;

    public partial class NodePermissions : AdminPage
    {
        private bool m_ChildNodeManageAll;
        private string m_ChildNodeManageId;
        private bool m_ChkNodeCommentCheckAll;
        private string m_ChkNodeCommentCheckId;
        private bool m_ChkNodeCommentManageAll;
        private string m_ChkNodeCommentManageId;
        private bool m_ChkNodeCommentReplyAll;
        private string m_ChkNodeCommentReplyId;
        private bool m_ContentManageAll;
        private string m_ContentManageId;
        private bool m_CurrentNodesManageAll;
        private string m_CurrentNodesManageId;
        private int m_IdType;
        private bool m_NodeCheckAll;
        private string m_NodeCheckId;
        private bool m_NodeInputAll;
        private string m_NodeInputId;
        private bool m_NodePreviewAll;
        private string m_NodePreviewId;
        private bool m_NodeSkimAll;
        private string m_NodeSkimId;
        private string m_PermissionsType = "";
        private int m_RoleId;
        private string m_Type = "";
        private CheckBox m_ChkNodeCommentReply;
        private CheckBox m_ChkNodeCommentCheck;
        private CheckBox m_ChkNodeCommentManage;
        private CheckBox m_ChkCurrentNodesManage;
        private CheckBox m_ChildNodeManage;
        private CheckBox m_ChkNodePreview;
        private CheckBox m_ChkNodeInput;
        private CheckBox m_ChkNodeCheck;
        private CheckBox m_ChkContentManage;
        private CheckBox m_ChkNodeSkim;

        private static void AppendAllId(StringBuilder roleIdList, int countNum)
        {
            if (roleIdList.Length > 0)
            {
                string str = roleIdList.ToString();
                if ((str.IndexOf(",", StringComparison.Ordinal) > 0) && (str.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries).Length == countNum))
                {
                    StringHelper.AppendString(roleIdList, "-1");
                }
            }
        }

        private static void AppendSelectId(bool isChecked, string selectId, ref StringBuilder roleIdList)
        {
            if (isChecked)
            {
                StringHelper.AppendString(roleIdList, selectId);
            }
        }

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            string permissionsType = this.m_PermissionsType;
            if (permissionsType != null)
            {
                if (!(permissionsType == "Role"))
                {
                    if (permissionsType == "User")
                    {
                        this.SaveUserFieldPermissions();
                    }
                }
                else
                {
                    this.SaveRoleFieldPermissions();
                }
            }
            this.Page.ClientScript.RegisterClientScriptBlock(base.GetType(), "windowsclose", "<script type='text/javascript'>window.close();</script>");
        }

        protected void EgvNodes_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            NodeInfo nodeInfo = new NodeInfo();
            nodeInfo = (NodeInfo) e.Row.DataItem;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ExtendedLabel label = (ExtendedLabel) e.Row.FindControl("LabNodeShowTree");
                label.BeginTag = Nodes.GetTreeLine(nodeInfo.Depth, nodeInfo.ParentPath, nodeInfo.NextId, nodeInfo.Child);
                label.EndTag = Nodes.GetNodeDir(nodeInfo.Child, nodeInfo.NodeType, nodeInfo.NodeDir);
                if (nodeInfo.NodeName == "所有栏目")
                {
                    label.BeginTag = label.BeginTag + "<span style='color:red'>";
                    label.EndTag = "</span>" + label.EndTag;
                }
                label.Text = nodeInfo.NodeName;
                string permissionsType = this.m_PermissionsType;
                if (permissionsType != null)
                {
                    if (!(permissionsType == "Role"))
                    {
                        if (permissionsType == "User")
                        {
                            this.UserNodeContentSelectPermission(e, nodeInfo);
                        }
                    }
                    else
                    {
                        string type = this.m_Type;
                        if (type != null)
                        {
                            if (!(type == "Content"))
                            {
                                if (!(type == "Comment"))
                                {
                                    if (type == "Node")
                                    {
                                        this.NodeSelectPermission(e, nodeInfo);
                                    }
                                    return;
                                }
                            }
                            else
                            {
                                this.NodeContentSelectPermission(e, nodeInfo);
                                return;
                            }
                            this.NodeCommentSelectPermission(e, nodeInfo);
                        }
                    }
                }
            }
        }

        private void GetContentNodeSelectPermission()
        {
            string selectId = "";
            StringBuilder roleIdList = new StringBuilder();
            StringBuilder builder2 = new StringBuilder();
            StringBuilder builder3 = new StringBuilder();
            StringBuilder builder4 = new StringBuilder();
            this.m_NodePreviewAll = ((CheckBox) this.EgvNodes.Rows[0].FindControl("ChkNodePreview")).Checked;
            this.m_NodeInputAll = ((CheckBox) this.EgvNodes.Rows[0].FindControl("ChkNodeInput")).Checked;
            this.m_NodeCheckAll = ((CheckBox) this.EgvNodes.Rows[0].FindControl("ChkNodeCheck")).Checked;
            this.m_ContentManageAll = ((CheckBox) this.EgvNodes.Rows[0].FindControl("ChkContentManage")).Checked;
            for (int i = 0; i < this.EgvNodes.Rows.Count; i++)
            {
                this.m_ChkNodePreview = (CheckBox) this.EgvNodes.Rows[i].Cells[2].FindControl("ChkNodePreview");
                this.m_ChkNodeInput = (CheckBox) this.EgvNodes.Rows[i].Cells[3].FindControl("ChkNodeInput");
                this.m_ChkNodeCheck = (CheckBox) this.EgvNodes.Rows[i].Cells[4].FindControl("ChkNodeCheck");
                this.m_ChkContentManage = (CheckBox) this.EgvNodes.Rows[i].Cells[5].FindControl("ChkContentManage");
                selectId = this.EgvNodes.DataKeys[i].Value.ToString();
                AppendSelectId(this.m_ChkNodePreview.Checked || this.m_NodePreviewAll, selectId, ref roleIdList);
                AppendSelectId(this.m_ChkNodeInput.Checked || this.m_NodeInputAll, selectId, ref builder2);
                AppendSelectId(this.m_ChkNodeCheck.Checked || this.m_NodeCheckAll, selectId, ref builder3);
                AppendSelectId(this.m_ChkContentManage.Checked || this.m_ContentManageAll, selectId, ref builder4);
            }
            AppendAllId(roleIdList, this.EgvNodes.Rows.Count - 1);
            AppendAllId(builder2, this.EgvNodes.Rows.Count - 1);
            AppendAllId(builder3, this.EgvNodes.Rows.Count - 1);
            AppendAllId(builder4, this.EgvNodes.Rows.Count - 1);
            RolePermissions.DeleteNodePermissionFromRoles(this.m_RoleId, -2, OperateCode.NodeContentPreview);
            RolePermissions.DeleteNodePermissionFromRoles(this.m_RoleId, -2, OperateCode.NodeContentInput);
            RolePermissions.DeleteNodePermissionFromRoles(this.m_RoleId, -2, OperateCode.NodeContentCheck);
            RolePermissions.DeleteNodePermissionFromRoles(this.m_RoleId, -2, OperateCode.NodeContentManage);
            RolePermissions.AddNodePermissionToRoles(this.m_RoleId, OperateCode.NodeContentPreview, roleIdList.ToString());
            RolePermissions.AddNodePermissionToRoles(this.m_RoleId, OperateCode.NodeContentInput, builder2.ToString());
            RolePermissions.AddNodePermissionToRoles(this.m_RoleId, OperateCode.NodeContentCheck, builder3.ToString());
            RolePermissions.AddNodePermissionToRoles(this.m_RoleId, OperateCode.NodeContentManage, builder4.ToString());
        }

        private void GetNodeCommentSelectPermission()
        {
            string selectId = "";
            StringBuilder roleIdList = new StringBuilder();
            StringBuilder builder2 = new StringBuilder();
            StringBuilder builder3 = new StringBuilder();
            this.m_ChkNodeCommentReplyAll = ((CheckBox) this.EgvNodes.Rows[0].FindControl("ChkNodeCommentReply")).Checked;
            this.m_ChkNodeCommentCheckAll = ((CheckBox) this.EgvNodes.Rows[0].FindControl("ChkNodeCommentCheck")).Checked;
            this.m_ChkNodeCommentManageAll = ((CheckBox) this.EgvNodes.Rows[0].FindControl("ChkNodeCommentManage")).Checked;
            for (int i = 0; i < this.EgvNodes.Rows.Count; i++)
            {
                this.m_ChkNodeCommentReply = (CheckBox) this.EgvNodes.Rows[i].Cells[2].FindControl("ChkNodeCommentReply");
                this.m_ChkNodeCommentCheck = (CheckBox) this.EgvNodes.Rows[i].Cells[3].FindControl("ChkNodeCommentCheck");
                this.m_ChkNodeCommentManage = (CheckBox) this.EgvNodes.Rows[i].Cells[4].FindControl("ChkNodeCommentManage");
                selectId = this.EgvNodes.DataKeys[i].Value.ToString();
                AppendSelectId(this.m_ChkNodeCommentReply.Checked || this.m_ChkNodeCommentReplyAll, selectId, ref roleIdList);
                AppendSelectId(this.m_ChkNodeCommentCheck.Checked || this.m_ChkNodeCommentCheckAll, selectId, ref builder2);
                AppendSelectId(this.m_ChkNodeCommentManage.Checked || this.m_ChkNodeCommentManageAll, selectId, ref builder3);
            }
            AppendAllId(roleIdList, this.EgvNodes.Rows.Count - 1);
            AppendAllId(builder2, this.EgvNodes.Rows.Count - 1);
            AppendAllId(builder3, this.EgvNodes.Rows.Count - 1);
            RolePermissions.DeleteNodePermissionFromRoles(this.m_RoleId, -2, OperateCode.NodeCommentReply);
            RolePermissions.DeleteNodePermissionFromRoles(this.m_RoleId, -2, OperateCode.NodeCommentCheck);
            RolePermissions.DeleteNodePermissionFromRoles(this.m_RoleId, -2, OperateCode.NodeCommentManage);
            RolePermissions.AddNodePermissionToRoles(this.m_RoleId, OperateCode.NodeCommentReply, roleIdList.ToString());
            RolePermissions.AddNodePermissionToRoles(this.m_RoleId, OperateCode.NodeCommentCheck, builder2.ToString());
            RolePermissions.AddNodePermissionToRoles(this.m_RoleId, OperateCode.NodeCommentManage, builder3.ToString());
        }

        private void GetNodeSelectPermission()
        {
            StringBuilder roleIdList = new StringBuilder();
            StringBuilder builder2 = new StringBuilder();
            string selectId = "";
            this.m_CurrentNodesManageAll = ((CheckBox) this.EgvNodes.Rows[0].FindControl("ChkCurrentNodesManage")).Checked;
            this.m_ChildNodeManageAll = ((CheckBox) this.EgvNodes.Rows[0].FindControl("ChkChildNodeManage")).Checked;
            for (int i = 0; i < this.EgvNodes.Rows.Count; i++)
            {
                this.m_ChkCurrentNodesManage = (CheckBox) this.EgvNodes.Rows[i].Cells[2].FindControl("ChkCurrentNodesManage");
                this.m_ChildNodeManage = (CheckBox) this.EgvNodes.Rows[i].Cells[3].FindControl("ChkChildNodeManage");
                selectId = this.EgvNodes.DataKeys[i].Value.ToString();
                AppendSelectId(this.m_ChkCurrentNodesManage.Checked || this.m_CurrentNodesManageAll, selectId, ref roleIdList);
                AppendSelectId(this.m_ChildNodeManage.Checked || this.m_ChildNodeManageAll, selectId, ref builder2);
            }
            AppendAllId(roleIdList, this.EgvNodes.Rows.Count - 1);
            AppendAllId(builder2, this.EgvNodes.Rows.Count - 1);
            RolePermissions.DeleteNodePermissionFromRoles(this.m_RoleId, -2, OperateCode.CurrentNodesManage);
            RolePermissions.DeleteNodePermissionFromRoles(this.m_RoleId, -2, OperateCode.ChildNodesManage);
            RolePermissions.AddNodePermissionToRoles(this.m_RoleId, OperateCode.CurrentNodesManage, roleIdList.ToString());
            RolePermissions.AddNodePermissionToRoles(this.m_RoleId, OperateCode.ChildNodesManage, builder2.ToString());
        }

        private void InitData()
        {
            IList<RoleNodePermissionsInfo> roleNodePermissionsList = new List<RoleNodePermissionsInfo>();
            string permissionsType = this.m_PermissionsType;
            if (permissionsType != null)
            {
                if (!(permissionsType == "Role"))
                {
                    if (permissionsType == "User")
                    {
                        string str3;
                        roleNodePermissionsList = UserPermissions.GetNodePermissionsById(this.m_RoleId, -2, this.m_IdType);
                        if (((str3 = this.m_Type) != null) && (str3 == "Content"))
                        {
                            this.EgvNodes.Columns[2].Visible = true;
                            this.EgvNodes.Columns[3].Visible = true;
                            this.EgvNodes.Columns[4].Visible = true;
                            this.SetUserContentNodeAll(roleNodePermissionsList);
                            this.SetUserContentNode(roleNodePermissionsList);
                        }
                    }
                }
                else
                {
                    roleNodePermissionsList = RolePermissions.GetNodePermissionsById(this.m_RoleId, -2);
                    string type = this.m_Type;
                    if (type != null)
                    {
                        if (!(type == "Content"))
                        {
                            if (!(type == "Node"))
                            {
                                if (type == "Comment")
                                {
                                    this.EgvNodes.Columns[9].Visible = true;
                                    this.EgvNodes.Columns[10].Visible = true;
                                    this.EgvNodes.Columns[11].Visible = true;
                                    this.SetNodeCommentAll(roleNodePermissionsList);
                                    this.SetNodeComment(roleNodePermissionsList);
                                }
                                return;
                            }
                        }
                        else
                        {
                            this.EgvNodes.Columns[3].Visible = true;
                            this.EgvNodes.Columns[4].Visible = true;
                            this.EgvNodes.Columns[5].Visible = true;
                            this.EgvNodes.Columns[6].Visible = true;
                            this.SetContentNodeAll(roleNodePermissionsList);
                            this.SetContentNode(roleNodePermissionsList);
                            return;
                        }
                        this.EgvNodes.Columns[7].Visible = true;
                        this.EgvNodes.Columns[8].Visible = true;
                        this.SetNodeAll(roleNodePermissionsList);
                        this.SetNode(roleNodePermissionsList);
                    }
                }
            }
        }

        private void NodeCommentSelectPermission(GridViewRowEventArgs e, NodeInfo nodeInfo)
        {
            CheckBox box = (CheckBox) e.Row.FindControl("ChkNodeCommentReply");
            CheckBox box2 = (CheckBox) e.Row.FindControl("ChkNodeCommentCheck");
            CheckBox box3 = (CheckBox) e.Row.FindControl("ChkNodeCommentManage");
            if (nodeInfo.NodeId == -1)
            {
                this.m_ChkNodeCommentReplyId = box.ClientID;
                this.m_ChkNodeCommentCheckId = box2.ClientID;
                this.m_ChkNodeCommentManageId = box3.ClientID;
                box.Attributes.Add("onclick", "ChkNodeAll(this.form,'" + box.ID + "'," + this.m_ChkNodeCommentReplyId + ")");
                box2.Attributes.Add("onclick", "ChkNodeAll(this.form,'" + box2.ID + "'," + this.m_ChkNodeCommentCheckId + ")");
                box3.Attributes.Add("onclick", "ChkNodeAll(this.form,'" + box3.ID + "'," + this.m_ChkNodeCommentManageId + ")");
            }
            else
            {
                box.Attributes.Add("onclick", "ChkWipeOffNodeAll(" + this.m_ChkNodeCommentReplyId + ")");
                box2.Attributes.Add("onclick", "ChkWipeOffNodeAll(" + this.m_ChkNodeCommentCheckId + ")");
                box3.Attributes.Add("onclick", "ChkWipeOffNodeAll(" + this.m_ChkNodeCommentManageId + ")");
            }
        }

        private void NodeContentSelectPermission(GridViewRowEventArgs e, NodeInfo nodeInfo)
        {
            CheckBox box = (CheckBox) e.Row.FindControl("ChkNodePreview");
            CheckBox box2 = (CheckBox) e.Row.FindControl("ChkNodeInput");
            CheckBox box3 = (CheckBox) e.Row.FindControl("ChkNodeCheck");
            CheckBox box4 = (CheckBox) e.Row.FindControl("ChkContentManage");
            if (nodeInfo.NodeId == -1)
            {
                this.m_NodePreviewId = box.ClientID;
                this.m_NodeInputId = box2.ClientID;
                this.m_NodeCheckId = box3.ClientID;
                this.m_ContentManageId = box4.ClientID;
                box.Attributes.Add("onclick", "ChkNodeAll2('" + box.ID + "'," + this.m_NodePreviewId + "," + box4.ClientID + ")");
                box2.Attributes.Add("onclick", "ChkNodeAll2('" + box2.ID + "'," + this.m_NodeInputId + "," + box4.ClientID + ")");
                box3.Attributes.Add("onclick", "ChkNodeAll2('" + box3.ID + "'," + this.m_NodeCheckId + "," + box4.ClientID + ")");
                box4.Attributes.Add("onclick", "ChkNodeManageAll(this.form,'" + box4.ID + "'," + this.m_ContentManageId + ")");
            }
            else
            {
                box.Attributes.Add("onclick", "ChkWipeOffNodePermissionsAll(" + this.m_NodePreviewId + ",'" + box4.ClientID + "'," + box.ClientID + ")");
                box2.Attributes.Add("onclick", "ChkWipeOffNodePermissionsAll(" + this.m_NodeInputId + ",'" + box4.ClientID + "'," + box2.ClientID + ")");
                box3.Attributes.Add("onclick", "ChkWipeOffNodePermissionsAll(" + this.m_NodeCheckId + ",'" + box4.ClientID + "'," + box3.ClientID + ")");
                box4.Attributes.Add("onclick", "ChkWipeOffNodeManageAll(" + this.m_ContentManageId + ",'" + box4.ClientID + "'," + this.m_NodePreviewId + "," + this.m_NodeInputId + "," + this.m_NodeCheckId + ")");
            }
        }

        private void NodeSelectPermission(GridViewRowEventArgs e, NodeInfo nodeInfo)
        {
            CheckBox box = (CheckBox) e.Row.FindControl("ChkCurrentNodesManage");
            CheckBox box2 = (CheckBox) e.Row.FindControl("ChkChildNodeManage");
            if (nodeInfo.NodeId == -1)
            {
                this.m_CurrentNodesManageId = box.ClientID;
                this.m_ChildNodeManageId = box2.ClientID;
                box.Attributes.Add("onclick", "ChkNodeAll(this.form,'" + box.ID + "'," + this.m_CurrentNodesManageId + ")");
                box2.Attributes.Add("onclick", "ChkNodeAll(this.form,'" + box2.ID + "'," + this.m_ChildNodeManageId + ")");
            }
            else
            {
                box.Attributes.Add("onclick", "ChkWipeOffNodeAll(" + this.m_CurrentNodesManageId + ")");
                box2.Attributes.Add("onclick", "ChkWipeOffNodeAll(" + this.m_ChildNodeManageId + ")");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.m_PermissionsType = BasePage.RequestString("PermissionsType");
            this.m_RoleId = BasePage.RequestInt32("RoleId");
            this.m_Type = BasePage.RequestString("Type");
            this.m_IdType = BasePage.RequestInt32("IdType");
            if ((this.m_RoleId <= 0) && (this.m_RoleId != -2))
            {
                AdminPage.WriteErrMsg("当前ID不存在！");
            }
            if (!this.Page.IsPostBack)
            {
                this.InitData();
            }
        }

        private void SaveRoleFieldPermissions()
        {
            string type = this.m_Type;
            if (type != null)
            {
                if (!(type == "Content"))
                {
                    if (!(type == "Comment"))
                    {
                        if (type == "Node")
                        {
                            this.GetNodeSelectPermission();
                        }
                        return;
                    }
                }
                else
                {
                    this.GetContentNodeSelectPermission();
                    return;
                }
                this.GetNodeCommentSelectPermission();
            }
        }

        private void SaveUserFieldPermissions()
        {
            string selectId = "";
            StringBuilder roleIdList = new StringBuilder();
            StringBuilder builder2 = new StringBuilder();
            StringBuilder builder3 = new StringBuilder();
            this.m_NodeSkimAll = ((CheckBox) this.EgvNodes.Rows[0].FindControl("ChkNodeSkim")).Checked;
            this.m_NodePreviewAll = ((CheckBox) this.EgvNodes.Rows[0].FindControl("ChkNodePreview")).Checked;
            this.m_NodeInputAll = ((CheckBox) this.EgvNodes.Rows[0].FindControl("ChkNodeInput")).Checked;
            for (int i = 0; i < this.EgvNodes.Rows.Count; i++)
            {
                this.m_ChkNodeSkim = (CheckBox) this.EgvNodes.Rows[i].Cells[2].FindControl("ChkNodeSkim");
                this.m_ChkNodePreview = (CheckBox) this.EgvNodes.Rows[i].Cells[3].FindControl("ChkNodePreview");
                this.m_ChkNodeInput = (CheckBox) this.EgvNodes.Rows[i].Cells[4].FindControl("ChkNodeInput");
                selectId = this.EgvNodes.DataKeys[i].Value.ToString();
                AppendSelectId(this.m_ChkNodeSkim.Checked || this.m_NodeSkimAll, selectId, ref roleIdList);
                AppendSelectId(this.m_ChkNodePreview.Checked || this.m_NodePreviewAll, selectId, ref builder2);
                AppendSelectId(this.m_ChkNodeInput.Checked || this.m_NodeInputAll, selectId, ref builder3);
            }
            AppendAllId(roleIdList, this.EgvNodes.Rows.Count - 1);
            AppendAllId(builder2, this.EgvNodes.Rows.Count - 1);
            AppendAllId(builder3, this.EgvNodes.Rows.Count - 1);
            UserPermissions.DeleteNodePermissions(this.m_RoleId, this.m_IdType);
            UserPermissions.AddNodePermissions(this.m_RoleId, OperateCode.NodeContentSkim, roleIdList.ToString(), this.m_IdType);
            UserPermissions.AddNodePermissions(this.m_RoleId, OperateCode.NodeContentPreview, builder2.ToString(), this.m_IdType);
            UserPermissions.AddNodePermissions(this.m_RoleId, OperateCode.NodeContentInput, builder3.ToString(), this.m_IdType);
        }

        private void SetContentNode(IList<RoleNodePermissionsInfo> roleNodePermissionsList)
        {
            for (int i = 0; i < this.EgvNodes.Rows.Count; i++)
            {
                this.m_ChkNodePreview = (CheckBox) this.EgvNodes.Rows[i].Cells[2].FindControl("ChkNodePreview");
                this.m_ChkNodeInput = (CheckBox) this.EgvNodes.Rows[i].Cells[3].FindControl("ChkNodeInput");
                this.m_ChkNodeCheck = (CheckBox) this.EgvNodes.Rows[i].Cells[4].FindControl("ChkNodeCheck");
                this.m_ChkContentManage = (CheckBox) this.EgvNodes.Rows[i].Cells[5].FindControl("ChkContentManage");
                int num2 = DataConverter.CLng(this.EgvNodes.Rows[i].Cells[0].Text);
                for (int j = 0; j < roleNodePermissionsList.Count; j++)
                {
                    if (roleNodePermissionsList[j].NodeId == num2)
                    {
                        if ((roleNodePermissionsList[j].OperateCode == OperateCode.NodeContentPreview) && !this.m_NodePreviewAll)
                        {
                            this.m_ChkNodePreview.Checked = true;
                        }
                        if ((roleNodePermissionsList[j].OperateCode == OperateCode.NodeContentInput) && !this.m_NodeInputAll)
                        {
                            this.m_ChkNodeInput.Checked = true;
                        }
                        if ((roleNodePermissionsList[j].OperateCode == OperateCode.NodeContentCheck) && !this.m_NodeCheckAll)
                        {
                            this.m_ChkNodeCheck.Checked = true;
                        }
                        if ((roleNodePermissionsList[j].OperateCode == OperateCode.NodeContentManage) && !this.m_ContentManageAll)
                        {
                            this.m_ChkContentManage.Checked = true;
                        }
                    }
                }
            }
        }

        private void SetContentNodeAll(IList<RoleNodePermissionsInfo> roleNodePermissionsList)
        {
            CheckBox box = (CheckBox) this.EgvNodes.Rows[0].Cells[2].FindControl("ChkNodePreview");
            CheckBox box2 = (CheckBox) this.EgvNodes.Rows[0].Cells[3].FindControl("ChkNodeInput");
            CheckBox box3 = (CheckBox) this.EgvNodes.Rows[0].Cells[4].FindControl("ChkNodeCheck");
            CheckBox box4 = (CheckBox) this.EgvNodes.Rows[0].Cells[5].FindControl("ChkContentManage");
            for (int i = 0; i < roleNodePermissionsList.Count; i++)
            {
                if (roleNodePermissionsList[i].NodeId == -1)
                {
                    if (roleNodePermissionsList[i].OperateCode == OperateCode.NodeContentPreview)
                    {
                        box.Checked = true;
                        this.m_NodePreviewAll = true;
                    }
                    if (roleNodePermissionsList[i].OperateCode == OperateCode.NodeContentInput)
                    {
                        box2.Checked = true;
                        this.m_NodeInputAll = true;
                    }
                    if (roleNodePermissionsList[i].OperateCode == OperateCode.NodeContentCheck)
                    {
                        box3.Checked = true;
                        this.m_NodeCheckAll = true;
                    }
                    if (roleNodePermissionsList[i].OperateCode == OperateCode.NodeContentManage)
                    {
                        box4.Checked = true;
                        this.m_ContentManageAll = true;
                    }
                }
            }
        }

        private void SetNode(IList<RoleNodePermissionsInfo> roleNodePermissionsList)
        {
            for (int i = 0; i < this.EgvNodes.Rows.Count; i++)
            {
                this.m_ChkCurrentNodesManage = (CheckBox) this.EgvNodes.Rows[i].Cells[2].FindControl("ChkCurrentNodesManage");
                this.m_ChildNodeManage = (CheckBox) this.EgvNodes.Rows[i].Cells[3].FindControl("ChkChildNodeManage");
                int num2 = DataConverter.CLng(this.EgvNodes.Rows[i].Cells[0].Text);
                for (int j = 0; j < roleNodePermissionsList.Count; j++)
                {
                    if (roleNodePermissionsList[j].NodeId == num2)
                    {
                        if ((roleNodePermissionsList[j].OperateCode == OperateCode.CurrentNodesManage) && !this.m_CurrentNodesManageAll)
                        {
                            this.m_ChkCurrentNodesManage.Checked = true;
                        }
                        if ((roleNodePermissionsList[j].OperateCode == OperateCode.ChildNodesManage) && !this.m_ChildNodeManageAll)
                        {
                            this.m_ChildNodeManage.Checked = true;
                        }
                    }
                }
            }
        }

        private void SetNodeAll(IList<RoleNodePermissionsInfo> roleNodePermissionsList)
        {
            CheckBox box = (CheckBox) this.EgvNodes.Rows[0].Cells[2].FindControl("ChkCurrentNodesManage");
            CheckBox box2 = (CheckBox) this.EgvNodes.Rows[0].Cells[3].FindControl("ChkChildNodeManage");
            for (int i = 0; i < roleNodePermissionsList.Count; i++)
            {
                if (roleNodePermissionsList[i].NodeId == -1)
                {
                    if (roleNodePermissionsList[i].OperateCode == OperateCode.CurrentNodesManage)
                    {
                        box.Checked = true;
                        this.m_CurrentNodesManageAll = true;
                    }
                    if (roleNodePermissionsList[i].OperateCode == OperateCode.ChildNodesManage)
                    {
                        box2.Checked = true;
                        this.m_ChildNodeManageAll = true;
                    }
                }
            }
        }

        private void SetNodeComment(IList<RoleNodePermissionsInfo> roleNodePermissionsList)
        {
            for (int i = 0; i < this.EgvNodes.Rows.Count; i++)
            {
                this.m_ChkNodeCommentReply = (CheckBox) this.EgvNodes.Rows[i].Cells[2].FindControl("ChkNodeCommentReply");
                this.m_ChkNodeCommentCheck = (CheckBox) this.EgvNodes.Rows[i].Cells[3].FindControl("ChkNodeCommentCheck");
                this.m_ChkNodeCommentManage = (CheckBox) this.EgvNodes.Rows[i].Cells[4].FindControl("ChkNodeCommentManage");
                int num2 = DataConverter.CLng(this.EgvNodes.Rows[i].Cells[0].Text);
                for (int j = 0; j < roleNodePermissionsList.Count; j++)
                {
                    if (roleNodePermissionsList[j].NodeId == num2)
                    {
                        if ((roleNodePermissionsList[j].OperateCode == OperateCode.NodeCommentReply) && !this.m_ChkNodeCommentReplyAll)
                        {
                            this.m_ChkNodeCommentReply.Checked = true;
                        }
                        if ((roleNodePermissionsList[j].OperateCode == OperateCode.NodeCommentCheck) && !this.m_ChkNodeCommentCheckAll)
                        {
                            this.m_ChkNodeCommentCheck.Checked = true;
                        }
                        if ((roleNodePermissionsList[j].OperateCode == OperateCode.NodeCommentManage) && !this.m_ChkNodeCommentManageAll)
                        {
                            this.m_ChkNodeCommentManage.Checked = true;
                        }
                    }
                }
            }
        }

        private void SetNodeCommentAll(IList<RoleNodePermissionsInfo> roleNodePermissionsList)
        {
            CheckBox box = (CheckBox) this.EgvNodes.Rows[0].Cells[2].FindControl("ChkNodeCommentReply");
            CheckBox box2 = (CheckBox) this.EgvNodes.Rows[0].Cells[3].FindControl("ChkNodeCommentCheck");
            CheckBox box3 = (CheckBox) this.EgvNodes.Rows[0].Cells[4].FindControl("ChkNodeCommentManage");
            for (int i = 0; i < roleNodePermissionsList.Count; i++)
            {
                if (roleNodePermissionsList[i].NodeId == -1)
                {
                    if (roleNodePermissionsList[i].OperateCode == OperateCode.NodeCommentReply)
                    {
                        box.Checked = true;
                        this.m_ChkNodeCommentReplyAll = true;
                    }
                    if (roleNodePermissionsList[i].OperateCode == OperateCode.NodeCommentCheck)
                    {
                        box2.Checked = true;
                        this.m_ChkNodeCommentCheckAll = true;
                    }
                    if (roleNodePermissionsList[i].OperateCode == OperateCode.NodeCommentManage)
                    {
                        box3.Checked = true;
                        this.m_ChkNodeCommentManageAll = true;
                    }
                }
            }
        }

        private void SetUserContentNode(IList<RoleNodePermissionsInfo> roleNodePermissionsList)
        {
            for (int i = 0; i < this.EgvNodes.Rows.Count; i++)
            {
                CheckBox box = (CheckBox) this.EgvNodes.Rows[i].FindControl("ChkNodeSkim");
                CheckBox box2 = (CheckBox) this.EgvNodes.Rows[i].FindControl("ChkNodePreview");
                CheckBox box3 = (CheckBox) this.EgvNodes.Rows[i].FindControl("ChkNodeInput");
                int num2 = DataConverter.CLng(this.EgvNodes.Rows[i].Cells[0].Text);
                for (int j = 0; j < roleNodePermissionsList.Count; j++)
                {
                    if (roleNodePermissionsList[j].NodeId == num2)
                    {
                        if ((roleNodePermissionsList[j].OperateCode == OperateCode.NodeContentSkim) && !this.m_NodeSkimAll)
                        {
                            box.Checked = true;
                        }
                        if ((roleNodePermissionsList[j].OperateCode == OperateCode.NodeContentPreview) && !this.m_NodePreviewAll)
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

        private void SetUserContentNodeAll(IList<RoleNodePermissionsInfo> roleNodePermissionsList)
        {
            CheckBox box = (CheckBox) this.EgvNodes.Rows[0].FindControl("ChkNodeSkim");
            CheckBox box2 = (CheckBox) this.EgvNodes.Rows[0].FindControl("ChkNodePreview");
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
                        this.m_NodePreviewAll = true;
                    }
                    if (roleNodePermissionsList[i].OperateCode == OperateCode.NodeContentInput)
                    {
                        box3.Checked = true;
                        this.m_NodeInputAll = true;
                    }
                }
            }
        }

        private void UserNodeContentSelectPermission(GridViewRowEventArgs e, NodeInfo nodeInfo)
        {
            CheckBox box = (CheckBox) e.Row.FindControl("ChkNodeSkim");
            CheckBox box2 = (CheckBox) e.Row.FindControl("ChkNodePreview");
            CheckBox box3 = (CheckBox) e.Row.FindControl("ChkNodeInput");
            if (nodeInfo.NodeId == -1)
            {
                this.m_NodeSkimId = box.ClientID;
                this.m_NodePreviewId = box2.ClientID;
                this.m_NodeInputId = box3.ClientID;
                box.Attributes.Add("onclick", "ChkNodeAll(this.form,'" + box.ID + "'," + this.m_NodeSkimId + ")");
                box2.Attributes.Add("onclick", "ChkNodeAll(this.form,'" + box2.ID + "'," + this.m_NodePreviewId + ")");
                box3.Attributes.Add("onclick", "ChkNodeAll(this.form,'" + box3.ID + "'," + this.m_NodeInputId + ")");
            }
            else
            {
                box.Attributes.Add("onclick", "ChkWipeOffNodeAll(" + this.m_NodeSkimId + ")");
                box2.Attributes.Add("onclick", "ChkWipeOffNodeAll(" + this.m_NodePreviewId + ")");
                box3.Attributes.Add("onclick", "ChkWipeOffNodeAll(" + this.m_NodeInputId + ")");
            }
        }
    }
}

