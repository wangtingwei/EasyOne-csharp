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
    using System.IO;
    using System.Text;
    using System.Web;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using System.Xml;

    public partial class Purview : BaseUserControl
    {
        private string m_Action;
        public StringBuilder m_ArrModelTr = new StringBuilder();
        public StringBuilder m_ArrSpecialCategoryTr = new StringBuilder();
        public StringBuilder m_ArrSpecialTable = new StringBuilder();
        public StringBuilder m_ArrTable = new StringBuilder();
        public StringBuilder m_ArrTabs = new StringBuilder();
        public StringBuilder m_ArrTitle = new StringBuilder();
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
        private StringBuilder m_fieldNameList = new StringBuilder();
        private bool m_inputSpecialAll;
        private string m_inputSpecialId;
        private StringBuilder m_inputSpecialIds = new StringBuilder();
        private bool m_manageSpecialAll;
        private string m_manageSpecialId;
        private StringBuilder m_manageSpecialIds = new StringBuilder();
        private StringBuilder m_modelIdList = new StringBuilder();
        private bool m_NodeCheckAll;
        private string m_NodeCheckId;
        private bool m_NodeInputAll;
        private string m_NodeInputId;
        private bool m_NodePreviewAll;
        private string m_NodePreviewId;
        private int m_RoleId;
        private StringBuilder m_strMsg = new StringBuilder();
        private XmlDocument xmlDoc;
        private string xmlPath = "menu";
        private CheckBox m_ChkNodeCommentReply;
        private CheckBox m_ChkNodeInput;
        private CheckBox m_ChkNodeCheck;
        private CheckBox m_ChkContentManage;
        private CheckBox m_ChkNodePreview;
        private CheckBox m_ChkNodeCommentCheck;
        private CheckBox m_ChkNodeCommentManage;
        private CheckBox m_ChkCurrentNodesManage;
        private CheckBox m_ChildNodeManage;

        private static void AppendSelectId(bool isChecked, string selectId, ref StringBuilder roleIdList)
        {
            if (isChecked)
            {
                StringHelper.AppendString(roleIdList, selectId);
            }
        }

        private string Checked(XmlNode child)
        {
            string str = "";
            if (this.GetAttributeValue(child, "IsChoose") == "true")
            {
                str = " Checked ";
            }
            return str;
        }

        private string Description(XmlNode child)
        {
            string attributeValue = "";
            attributeValue = this.GetAttributeValue(child, "Description");
            if (!string.IsNullOrEmpty(attributeValue))
            {
                attributeValue = "<span style='color:green'>" + attributeValue + "</span>";
            }
            return attributeValue;
        }

        protected void EgvContents_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            NodeInfo dataItem = new NodeInfo();
            dataItem = (NodeInfo) e.Row.DataItem;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                CheckBox box = (CheckBox) e.Row.FindControl("ChkNodePreview");
                CheckBox box2 = (CheckBox) e.Row.FindControl("ChkNodeInput");
                CheckBox box3 = (CheckBox) e.Row.FindControl("ChkNodeCheck");
                CheckBox box4 = (CheckBox) e.Row.FindControl("ChkContentManage");
                Label label = (Label) e.Row.FindControl("LabNodeShowTree");
                label.Text = Nodes.GetTreeLine(dataItem.Depth, dataItem.ParentPath, dataItem.NextId, dataItem.Child) + dataItem.NodeName + Nodes.GetNodeDir(dataItem.Child, dataItem.NodeType, dataItem.NodeDir);
                if (dataItem.NodeId == -1)
                {
                    this.m_NodePreviewId = box.ClientID;
                    this.m_NodeInputId = box2.ClientID;
                    this.m_NodeCheckId = box3.ClientID;
                    this.m_ContentManageId = box4.ClientID;
                    box.Attributes.Add("onclick", "ChkNodeAll(this.form,'" + box.ID + "'," + this.m_NodePreviewId + ",'" + this.Tabs1.ClientID + "')");
                    box2.Attributes.Add("onclick", "ChkNodeAll(this.form,'" + box2.ID + "'," + this.m_NodeInputId + ",'" + this.Tabs1.ClientID + "')");
                    box3.Attributes.Add("onclick", "ChkNodeAll(this.form,'" + box3.ID + "'," + this.m_NodeCheckId + ",'" + this.Tabs1.ClientID + "')");
                    box4.Attributes.Add("onclick", "ChkNodeAll(this.form,'" + box4.ID + "'," + this.m_ContentManageId + ",'" + this.Tabs1.ClientID + "')");
                }
                else
                {
                    box.Attributes.Add("onclick", "ChkWipeOffNodeAll(" + this.m_NodePreviewId + ")");
                    box2.Attributes.Add("onclick", "ChkWipeOffNodeAll(" + this.m_NodeInputId + ")");
                    box3.Attributes.Add("onclick", "ChkWipeOffNodeAll(" + this.m_NodeCheckId + ")");
                    box4.Attributes.Add("onclick", "ChkWipeOffNodeAll(" + this.m_ContentManageId + ")");
                }
            }
        }

        protected void EgvNodeComments_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            NodeInfo dataItem = new NodeInfo();
            dataItem = (NodeInfo) e.Row.DataItem;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                CheckBox box = (CheckBox) e.Row.FindControl("ChkNodeCommentReply");
                CheckBox box2 = (CheckBox) e.Row.FindControl("ChkNodeCommentCheck");
                CheckBox box3 = (CheckBox) e.Row.FindControl("ChkNodeCommentManage");
                Label label = (Label) e.Row.FindControl("LabNodeShowTree");
                label.Text = Nodes.GetTreeLine(dataItem.Depth, dataItem.ParentPath, dataItem.NextId, dataItem.Child) + dataItem.NodeName + Nodes.GetNodeDir(dataItem.Child, dataItem.NodeType, dataItem.NodeDir);
                if (dataItem.NodeId == -1)
                {
                    this.m_ChkNodeCommentReplyId = box.ClientID;
                    this.m_ChkNodeCommentCheckId = box2.ClientID;
                    this.m_ChkNodeCommentManageId = box3.ClientID;
                    box.Attributes.Add("onclick", "ChkNodeAll(this.form,'" + box.ID + "'," + this.m_ChkNodeCommentReplyId + ",'" + this.Tabs5.ClientID + "')");
                    box2.Attributes.Add("onclick", "ChkNodeAll(this.form,'" + box2.ID + "'," + this.m_ChkNodeCommentCheckId + ",'" + this.Tabs5.ClientID + "')");
                    box3.Attributes.Add("onclick", "ChkNodeAll(this.form,'" + box3.ID + "'," + this.m_ChkNodeCommentManageId + ",'" + this.Tabs5.ClientID + "')");
                }
                else
                {
                    box.Attributes.Add("onclick", "ChkWipeOffNodeAll(" + this.m_ChkNodeCommentReplyId + ")");
                    box2.Attributes.Add("onclick", "ChkWipeOffNodeAll(" + this.m_ChkNodeCommentCheckId + ")");
                    box3.Attributes.Add("onclick", "ChkWipeOffNodeAll(" + this.m_ChkNodeCommentManageId + ")");
                }
            }
        }

        protected void EgvNodes_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            NodeInfo dataItem = new NodeInfo();
            dataItem = (NodeInfo) e.Row.DataItem;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                CheckBox box = (CheckBox) e.Row.FindControl("ChkCurrentNodesManage");
                CheckBox box2 = (CheckBox) e.Row.FindControl("ChkChildNodeManage");
                Label label = (Label) e.Row.FindControl("LabNodeShowTree");
                label.Text = Nodes.GetTreeLine(dataItem.Depth, dataItem.ParentPath, dataItem.NextId, dataItem.Child) + dataItem.NodeName + Nodes.GetNodeDir(dataItem.Child, dataItem.NodeType, dataItem.NodeDir);
                if (dataItem.NodeId == -1)
                {
                    this.m_CurrentNodesManageId = box.ClientID;
                    this.m_ChildNodeManageId = box2.ClientID;
                    box.Attributes.Add("onclick", "ChkNodeAll(this.form,'" + box.ID + "'," + this.m_CurrentNodesManageId + ",'" + this.Tabs4.ClientID + "')");
                    box2.Attributes.Add("onclick", "ChkNodeAll(this.form,'" + box2.ID + "'," + this.m_ChildNodeManageId + ",'" + this.Tabs4.ClientID + "')");
                }
                else
                {
                    box.Attributes.Add("onclick", "ChkWipeOffNodeAll(" + this.m_CurrentNodesManageId + ")");
                    box2.Attributes.Add("onclick", "ChkWipeOffNodeAll(" + this.m_ChildNodeManageId + ")");
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
                CheckBox box2 = (CheckBox) e.Row.FindControl("ChkSpecialManage");
                HiddenField field = (HiddenField) e.Row.FindControl("HdnSpecialId");
                if (dataItem != null)
                {
                    label.Text = Special.TreeLine(dataItem.TreeLineType) + dataItem.Name;
                    if (dataItem.IsSpecialCategory)
                    {
                        field.Value = "0";
                        box.Visible = false;
                        box2.Visible = false;
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
                            this.m_manageSpecialId = box2.ClientID;
                            box.Attributes.Add("onclick", "ChkSpecialAll(this.form,'" + box.ID + "'," + this.m_inputSpecialId + ")");
                            box2.Attributes.Add("onclick", "ChkSpecialAll(this.form,'" + box2.ID + "'," + this.m_manageSpecialId + ")");
                        }
                        else
                        {
                            box.Attributes.Add("onclick", "ChkWipeOffSpecialAll(" + this.m_inputSpecialId + ")");
                            box2.Attributes.Add("onclick", "ChkWipeOffSpecialAll(" + this.m_manageSpecialId + ")");
                        }
                        IList<RoleSpecialPermissionsInfo> specialPermissionsByRoleId = RolePermissions.GetSpecialPermissionsByRoleId(this.m_RoleId, OperateCode.SpecialContentInput);
                        IList<RoleSpecialPermissionsInfo> list2 = RolePermissions.GetSpecialPermissionsByRoleId(this.m_RoleId, OperateCode.SepcialContentManage);
                        foreach (RoleSpecialPermissionsInfo info in specialPermissionsByRoleId)
                        {
                            if (info.SpecialId == DataConverter.CLng(field.Value))
                            {
                                if (dataItem.Id == -1)
                                {
                                    this.m_inputSpecialAll = true;
                                    box.Checked = true;
                                }
                                else if (!this.m_inputSpecialAll)
                                {
                                    box.Checked = true;
                                }
                            }
                        }
                        foreach (RoleSpecialPermissionsInfo info2 in list2)
                        {
                            if (info2.SpecialId == DataConverter.CLng(field.Value))
                            {
                                if (dataItem.Id == -1)
                                {
                                    this.m_manageSpecialAll = true;
                                    box2.Checked = true;
                                }
                                else if (!this.m_manageSpecialAll)
                                {
                                    box2.Checked = true;
                                }
                            }
                        }
                    }
                }
            }
        }

        private string GetAttributeValue(XmlNode xmlNode, string attributeName)
        {
            string str = "";
            if (xmlNode != null)
            {
                XmlAttribute attribute = xmlNode.Attributes[attributeName];
                if (attribute != null)
                {
                    str = attribute.Value;
                }
            }
            return str;
        }

        private void GetContentNodeSelectPermission()
        {
            string selectId = "";
            StringBuilder roleIdList = new StringBuilder();
            StringBuilder builder2 = new StringBuilder();
            StringBuilder builder3 = new StringBuilder();
            StringBuilder builder4 = new StringBuilder();
            this.m_NodePreviewAll = ((CheckBox) this.EgvContents.Rows[0].FindControl("ChkNodePreview")).Checked;
            this.m_NodeInputAll = ((CheckBox) this.EgvContents.Rows[0].FindControl("ChkNodeInput")).Checked;
            this.m_NodeCheckAll = ((CheckBox) this.EgvContents.Rows[0].FindControl("ChkNodeCheck")).Checked;
            this.m_ContentManageAll = ((CheckBox) this.EgvContents.Rows[0].FindControl("ChkContentManage")).Checked;
            for (int i = 0; i < this.EgvContents.Rows.Count; i++)
            {
                this.m_ChkNodePreview = (CheckBox) this.EgvContents.Rows[i].Cells[2].FindControl("ChkNodePreview");
                this.m_ChkNodeInput = (CheckBox) this.EgvContents.Rows[i].Cells[3].FindControl("ChkNodeInput");
                this.m_ChkNodeCheck = (CheckBox) this.EgvContents.Rows[i].Cells[4].FindControl("ChkNodeCheck");
                this.m_ChkContentManage = (CheckBox) this.EgvContents.Rows[i].Cells[5].FindControl("ChkContentManage");
                selectId = this.EgvContents.DataKeys[i].Value.ToString();
                AppendSelectId(this.m_ChkNodePreview.Checked || this.m_NodePreviewAll, selectId, ref roleIdList);
                AppendSelectId(this.m_ChkNodeInput.Checked || this.m_NodeInputAll, selectId, ref builder2);
                AppendSelectId(this.m_ChkNodeCheck.Checked || this.m_NodeCheckAll, selectId, ref builder3);
                AppendSelectId(this.m_ChkContentManage.Checked || this.m_ContentManageAll, selectId, ref builder4);
            }
            RolePermissions.AddNodePermissionToRoles(this.m_RoleId, OperateCode.NodeContentPreview, roleIdList.ToString());
            RolePermissions.AddNodePermissionToRoles(this.m_RoleId, OperateCode.NodeContentInput, builder2.ToString());
            RolePermissions.AddNodePermissionToRoles(this.m_RoleId, OperateCode.NodeContentCheck, builder3.ToString());
            RolePermissions.AddNodePermissionToRoles(this.m_RoleId, OperateCode.NodeContentManage, builder4.ToString());
        }

        private void GetFieldPermission()
        {
            foreach (RepeaterItem item in this.RptModelList2.Items)
            {
                Repeater repeater = item.FindControl("RptFieldList") as Repeater;
                HiddenField field = item.FindControl("HdnModelId") as HiddenField;
                if (repeater != null)
                {
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
                    continue;
                }
            }
            if (this.m_Action == "Add")
            {
                if (!RolePermissions.AddFieldPermissions(this.m_RoleId, OperateCode.ContentFieldInput, this.m_modelIdList.ToString(), this.m_fieldNameList.ToString()))
                {
                    this.m_strMsg.Append("<li>模型字段权限添加失败！</li>");
                }
            }
            else
            {
                RolePermissions.DeleteFieldPermissionFromRoles(this.m_RoleId);
                if (!RolePermissions.AddFieldPermissions(this.m_RoleId, OperateCode.ContentFieldInput, this.m_modelIdList.ToString(), this.m_fieldNameList.ToString()))
                {
                    this.m_strMsg.Append("<li>模型字段权限修改失败！</li>");
                }
            }
        }

        private void GetModelPermission()
        {
            string str = base.Request.Form["ModelPurview"];
            RolePermissions.DeletePermissionFromRoles(this.m_RoleId);
            if (!string.IsNullOrEmpty(str))
            {
                RolePermissions.AddModelPermissionToRoles(this.m_RoleId, str.Trim());
            }
        }

        private void GetNodeCommentSelectPermission()
        {
            string selectId = "";
            StringBuilder roleIdList = new StringBuilder();
            StringBuilder builder2 = new StringBuilder();
            StringBuilder builder3 = new StringBuilder();
            this.m_ChkNodeCommentReplyAll = ((CheckBox) this.EgvNodeComments.Rows[0].FindControl("ChkNodeCommentReply")).Checked;
            this.m_ChkNodeCommentCheckAll = ((CheckBox) this.EgvNodeComments.Rows[0].FindControl("ChkNodeCommentCheck")).Checked;
            this.m_ChkNodeCommentManageAll = ((CheckBox) this.EgvNodeComments.Rows[0].FindControl("ChkNodeCommentManage")).Checked;
            for (int i = 0; i < this.EgvNodeComments.Rows.Count; i++)
            {
                this.m_ChkNodeCommentReply = (CheckBox) this.EgvNodeComments.Rows[i].Cells[2].FindControl("ChkNodeCommentReply");
                this.m_ChkNodeCommentCheck = (CheckBox) this.EgvNodeComments.Rows[i].Cells[3].FindControl("ChkNodeCommentCheck");
                this.m_ChkNodeCommentManage = (CheckBox) this.EgvNodeComments.Rows[i].Cells[4].FindControl("ChkNodeCommentManage");
                selectId = this.EgvNodeComments.DataKeys[i].Value.ToString();
                AppendSelectId(this.m_ChkNodeCommentReply.Checked || this.m_ChkNodeCommentReplyAll, selectId, ref roleIdList);
                AppendSelectId(this.m_ChkNodeCommentCheck.Checked || this.m_ChkNodeCommentCheckAll, selectId, ref builder2);
                AppendSelectId(this.m_ChkNodeCommentManage.Checked || this.m_ChkNodeCommentManageAll, selectId, ref builder3);
            }
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
            RolePermissions.AddNodePermissionToRoles(this.m_RoleId, OperateCode.CurrentNodesManage, roleIdList.ToString());
            RolePermissions.AddNodePermissionToRoles(this.m_RoleId, OperateCode.ChildNodesManage, builder2.ToString());
        }

        private void GetSpecialPermissions()
        {
            this.m_inputSpecialAll = ((CheckBox) this.EgvSpecial.Rows[0].FindControl("ChkSpecialInput")).Checked;
            this.m_manageSpecialAll = ((CheckBox) this.EgvSpecial.Rows[0].FindControl("ChkSpecialManage")).Checked;
            foreach (GridViewRow row in this.EgvSpecial.Rows)
            {
                CheckBox box = (CheckBox) row.FindControl("ChkSpecialInput");
                CheckBox box2 = (CheckBox) row.FindControl("ChkSpecialManage");
                HiddenField field = (HiddenField) row.FindControl("HdnSpecialId");
                if ((box.Checked && (field.Value != "0")) || (this.m_inputSpecialAll && (field.Value != "0")))
                {
                    StringHelper.AppendString(this.m_inputSpecialIds, field.Value);
                }
                if ((box2.Checked && (field.Value != "0")) || (this.m_inputSpecialAll && (field.Value != "0")))
                {
                    StringHelper.AppendString(this.m_manageSpecialIds, field.Value);
                }
            }
            RolePermissions.DeleteSpecialPermissionFromRoles(this.m_RoleId);
            RolePermissions.AddSepcialPermissionToRoles(this.m_RoleId, this.m_inputSpecialIds.ToString(), OperateCode.SpecialContentInput);
            RolePermissions.AddSepcialPermissionToRoles(this.m_RoleId, this.m_manageSpecialIds.ToString(), OperateCode.SepcialContentManage);
        }

        private void InitChannelMenuLi(StringBuilder sb, string channelMenuId)
        {
            string xpath = this.xmlPath + "/channelMenu[@id='" + channelMenuId + "']";
            XmlNode node = this.xmlDoc.SelectSingleNode(xpath);
            if ((node != null) && node.HasChildNodes)
            {
                foreach (XmlNode node2 in node)
                {
                    string attributeValue = this.GetAttributeValue(node2, "operateCode");
                    string subModel = channelMenuId + "_" + this.GetAttributeValue(node2, "id");
                    string str4 = this.Checked(node2);
                    if (!(node2.Name == "mainMenu") || !(this.GetAttributeValue(node2, "ShowOnForm") == "true"))
                    {
                        goto Label_019B;
                    }
                    sb.Append("<tr>");
                    sb.Append("  <td style='padding-left:30px;'>");
                    sb.Append("     <input type='checkbox' name='ModelPurview' value='" + this.GetAttributeValue(node2, "operateCode") + "' " + str4 + " id='" + subModel + "'  onclick=\"javascript:CheckModel(this);\"  />&nbsp;");
                    sb.Append(this.GetAttributeValue(node2, "title"));
                    string str5 = attributeValue;
                    if (str5 != null)
                    {
                        if (!(str5 == "NodesManage"))
                        {
                            if (str5 == "CommentManage")
                            {
                                goto Label_015F;
                            }
                        }
                        else
                        {
                            sb.Append("&nbsp;&nbsp;&lt;=【<a onclick=\"javascript:ShowTabs(4)\" href='#' ><span style='color:red'>详细设置</span></a>】");
                        }
                    }
                    goto Label_016B;
                Label_015F:
                    sb.Append("&nbsp;&nbsp;&lt;=【<a onclick=\"javascript:ShowTabs(5)\" href='#' ><span style='color:red'>详细设置</span></a>】");
                Label_016B:
                    sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;" + this.Description(node2));
                    sb.Append("  </td>");
                    sb.Append("</tr>");
                Label_019B:
                    this.InitMainMenu(sb, xpath + "/mainMenu[@id='" + this.GetAttributeValue(node2, "id") + "']", subModel);
                }
            }
        }

        private void InitData()
        {
            IList<RoleNodePermissionsInfo> roleNodePermissionsList = new List<RoleNodePermissionsInfo>();
            roleNodePermissionsList = RolePermissions.GetNodePermissionsById(this.m_RoleId, -2);
            this.SetContentNodeAll(roleNodePermissionsList);
            this.SetContentNode(roleNodePermissionsList);
            this.SetNodeAll(roleNodePermissionsList);
            this.SetNode(roleNodePermissionsList);
            this.SetNodeCommentAll(roleNodePermissionsList);
            this.SetNodeComment(roleNodePermissionsList);
        }

        private void InitMainMenu(StringBuilder sb, string path, string subModel)
        {
            XmlNode node = this.xmlDoc.SelectSingleNode(path);
            if ((node != null) && node.HasChildNodes)
            {
                foreach (XmlNode node2 in node)
                {
                    string attributeValue = this.GetAttributeValue(node2, "operateCode");
                    if (!Enum.IsDefined(typeof(OperateCode), attributeValue) || !(this.GetAttributeValue(node2, "ShowOnForm") == "true"))
                    {
                        continue;
                    }
                    string purviewModel = subModel + "_" + attributeValue;
                    string str3 = this.Checked(node2);
                    sb.Append("<tr>");
                    sb.Append("  <td style='padding-left:60px;'>");
                    sb.Append("     <input type='checkbox' name='ModelPurview' value='" + attributeValue + "' id='" + purviewModel + "' " + str3 + " onclick=\"javascript:CheckModel(this);\" />&nbsp;");
                    sb.Append(this.GetAttributeValue(node2, "title"));
                    string str4 = attributeValue;
                    if (str4 != null)
                    {
                        if (!(str4 == "CategoryInfoManage"))
                        {
                            if (str4 == "SpecialInfoManage")
                            {
                                goto Label_0130;
                            }
                        }
                        else
                        {
                            sb.Append("&nbsp;&nbsp;&lt;=【<a onclick=\"javascript:ShowTabs(1)\" href='#' ><span style='color:red'>详细设置</span></a>】");
                        }
                    }
                    goto Label_013C;
                Label_0130:
                    sb.Append("&nbsp;&nbsp;&lt;=【<a onclick=\"javascript:ShowTabs(2)\" href='#' ><span style='color:red'>详细设置</span></a>】");
                Label_013C:
                    sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;" + this.Description(node2));
                    sb.Append("  </td>");
                    sb.Append("</tr>");
                    this.InitSubMenu(sb, path + "/subMenu[@id='" + this.GetAttributeValue(node2, "id") + "']", purviewModel);
                }
            }
        }

        private void InitPurview()
        {
            string str;
            this.xmlDoc = new XmlDocument();
            HttpContext current = HttpContext.Current;
            if (current != null)
            {
                str = current.Server.MapPath("~/Admin/Common/MainMenu.xml");
            }
            else
            {
                str = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Admin/Common/MainMenu.xml");
            }
            try
            {
                this.xmlDoc.Load(str);
            }
            catch (XmlException exception)
            {
                BaseUserControl.WriteErrMsg("MainMenu.xml配置文件不符合XML规范，具体错误信息：" + exception.Message);
            }
            XmlNode node = this.xmlDoc.SelectSingleNode(this.xmlPath);
            StringBuilder sb = new StringBuilder();
            if (node == null)
            {
                BaseUserControl.WriteErrMsg("MainMenu.xml配置文件不存在menu根元素");
            }
            if ((this.m_Action == "Modify") && !this.Page.IsPostBack)
            {
                foreach (RoleModulePermissionsInfo info in RolePermissions.GetModelPermissionsById(this.m_RoleId))
                {
                    string name = Enum.GetName(typeof(OperateCode), info.OperateCode);
                    foreach (XmlNode node2 in this.xmlDoc.SelectNodes("//*[@operateCode='" + name + "']"))
                    {
                        if (node2 != null)
                        {
                            ((XmlElement) node2).SetAttribute("IsChoose", "true");
                        }
                    }
                }
            }
            if (node.HasChildNodes)
            {
                foreach (XmlNode node3 in node)
                {
                    string str3 = this.Checked(node3);
                    string attributeValue = this.GetAttributeValue(node3, "id");
                    if ((attributeValue != "MenuMyDeskTop") && (this.GetAttributeValue(node3, "ShowOnForm") == "true"))
                    {
                        sb.Append("<tr>");
                        sb.Append("  <td style='width:100%;'>");
                        sb.Append("     <input type='checkbox' name='ModelPurview' value='" + this.GetAttributeValue(node3, "operateCode") + "' " + str3 + " id='" + attributeValue + "'  onclick=\"javascript:CheckModel(this);\" />&nbsp;");
                        sb.Append(this.GetAttributeValue(node3, "title"));
                        sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;" + this.Description(node3));
                        sb.Append("  </td>");
                        sb.Append("</tr>");
                        this.InitChannelMenuLi(sb, attributeValue);
                    }
                }
            }
            this.LblModelPurview.Text = sb.ToString();
        }

        private void InitSubMenu(StringBuilder sb, string path, string purviewModel)
        {
            XmlNode node = this.xmlDoc.SelectSingleNode(path);
            if ((node != null) && node.HasChildNodes)
            {
                foreach (XmlNode node2 in node)
                {
                    string attributeValue = this.GetAttributeValue(node2, "operateCode");
                    if (Enum.IsDefined(typeof(OperateCode), attributeValue) && (this.GetAttributeValue(node2, "ShowOnForm") == "true"))
                    {
                        string str2 = purviewModel + "_" + attributeValue;
                        string str3 = this.Checked(node2);
                        sb.Append("<tr>");
                        sb.Append("  <td style='padding-left:90px;'>");
                        sb.Append("     <input type='checkbox' name='ModelPurview' value='" + attributeValue + "' id='" + str2 + "' " + str3 + " onclick=\"javascript:CheckModel(this);\" />&nbsp;");
                        sb.Append(node2.Attributes["title"].Value);
                        sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;" + this.Description(node2));
                        sb.Append("  </td>");
                        sb.Append("</tr>");
                    }
                }
            }
        }

        private void InitTabsArray()
        {
            this.m_ArrTabs.Append("\"" + this.Tabs0.ClientID + "\",");
            this.m_ArrTabs.Append("\"" + this.Tabs1.ClientID + "\",");
            this.m_ArrTabs.Append("\"" + this.Tabs2.ClientID + "\",");
            this.m_ArrTabs.Append("\"" + this.Tabs3.ClientID + "\",");
            this.m_ArrTabs.Append("\"" + this.Tabs4.ClientID + "\",");
            this.m_ArrTabs.Append("\"" + this.Tabs5.ClientID + "\"");
        }

        private void InitTitleArray()
        {
            this.m_ArrTitle.Append("\"" + this.TabTitle0.ClientID + "\",");
            this.m_ArrTitle.Append("\"" + this.TabTitle1.ClientID + "\",");
            this.m_ArrTitle.Append("\"" + this.TabTitle2.ClientID + "\",");
            this.m_ArrTitle.Append("\"" + this.TabTitle3.ClientID + "\",");
            this.m_ArrTitle.Append("\"" + this.TabTitle4.ClientID + "\",");
            this.m_ArrTitle.Append("\"" + this.TabTitle5.ClientID + "\"");
        }

        public void InputPermissions()
        {
            if (this.m_RoleId != 0)
            {
                RolePermissions.DeleteNodePermissionFromRoles(this.m_RoleId, -2);
                this.GetContentNodeSelectPermission();
                this.GetNodeSelectPermission();
                this.GetNodeCommentSelectPermission();
                this.GetSpecialPermissions();
                this.GetFieldPermission();
                this.GetModelPermission();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.InitTabsArray();
            this.InitTitleArray();
            this.InitPurview();
            if ((this.m_Action == "Modify") && !this.Page.IsPostBack)
            {
                this.InitData();
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
                IList<RoleFieldPermissionsInfo> fieldPermissionsById = new List<RoleFieldPermissionsInfo>();
                fieldPermissionsById = RolePermissions.GetFieldPermissionsById(this.m_RoleId);
                HiddenField field = e.Item.FindControl("HdnModelId") as HiddenField;
                int num = DataConverter.CLng(field.Value);
                foreach (RepeaterItem item in repeater.Items)
                {
                    HiddenField field2 = item.FindControl("HdnFieldName") as HiddenField;
                    CheckBox box = item.FindControl("ChkFieldPurview") as CheckBox;
                    foreach (RoleFieldPermissionsInfo info in fieldPermissionsById)
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

        public bool SetAttributesValue(string nodeName, string key, string val)
        {
            XmlNode node = this.xmlDoc.SelectSingleNode(nodeName);
            if (node == null)
            {
                return false;
            }
            ((XmlElement) node).SetAttribute(key, val);
            return true;
        }

        private void SetContentNode(IList<RoleNodePermissionsInfo> roleNodePermissionsList)
        {
            for (int i = 0; i < this.EgvContents.Rows.Count; i++)
            {
                this.m_ChkNodePreview = (CheckBox) this.EgvContents.Rows[i].Cells[2].FindControl("ChkNodePreview");
                this.m_ChkNodeInput = (CheckBox) this.EgvContents.Rows[i].Cells[3].FindControl("ChkNodeInput");
                this.m_ChkNodeCheck = (CheckBox) this.EgvContents.Rows[i].Cells[4].FindControl("ChkNodeCheck");
                this.m_ChkContentManage = (CheckBox) this.EgvContents.Rows[i].Cells[5].FindControl("ChkContentManage");
                int num2 = DataConverter.CLng(this.EgvContents.Rows[i].Cells[0].Text);
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
            CheckBox box = (CheckBox) this.EgvContents.Rows[0].Cells[2].FindControl("ChkNodePreview");
            CheckBox box2 = (CheckBox) this.EgvContents.Rows[0].Cells[3].FindControl("ChkNodeInput");
            CheckBox box3 = (CheckBox) this.EgvContents.Rows[0].Cells[4].FindControl("ChkNodeCheck");
            CheckBox box4 = (CheckBox) this.EgvContents.Rows[0].Cells[5].FindControl("ChkContentManage");
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
            for (int i = 0; i < this.EgvNodeComments.Rows.Count; i++)
            {
                this.m_ChkNodeCommentReply = (CheckBox) this.EgvNodeComments.Rows[i].Cells[2].FindControl("ChkNodeCommentReply");
                this.m_ChkNodeCommentCheck = (CheckBox) this.EgvNodeComments.Rows[i].Cells[3].FindControl("ChkNodeCommentCheck");
                this.m_ChkNodeCommentManage = (CheckBox) this.EgvNodeComments.Rows[i].Cells[4].FindControl("ChkNodeCommentManage");
                int num2 = DataConverter.CLng(this.EgvNodeComments.Rows[i].Cells[0].Text);
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
            CheckBox box = (CheckBox) this.EgvNodeComments.Rows[0].Cells[2].FindControl("ChkNodeCommentReply");
            CheckBox box2 = (CheckBox) this.EgvNodeComments.Rows[0].Cells[3].FindControl("ChkNodeCommentCheck");
            CheckBox box3 = (CheckBox) this.EgvNodeComments.Rows[0].Cells[4].FindControl("ChkNodeCommentManage");
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

        public string Action
        {
            get
            {
                return this.m_Action;
            }
            set
            {
                this.m_Action = value;
            }
        }

        public int RoleId
        {
            get
            {
                return this.m_RoleId;
            }
            set
            {
                this.m_RoleId = value;
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

