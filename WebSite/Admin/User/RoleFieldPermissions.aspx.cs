namespace EasyOne.WebSite.Admin.User
{
    using EasyOne.AccessManage;
    using EasyOne.Common;
    using EasyOne.CommonModel;
    using EasyOne.Components;
    using EasyOne.Controls;
    using EasyOne.Enumerations;
    using EasyOne.Model.AccessManage;
    using EasyOne.Model.CommonModel;
    using EasyOne.Model.UserManage;
    using EasyOne.UserManage;
    using EasyOne.Web.UI;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public partial class RoleFieldPermissions : AdminPage
    {
        public StringBuilder m_ArrModelTr = new StringBuilder();
        public StringBuilder m_ArrTable = new StringBuilder();
        private StringBuilder m_fieldNameList = new StringBuilder();
        private StringBuilder m_modelIdList = new StringBuilder();
        private int m_RoleId;

        protected void BtnCancle_Click(object sender, EventArgs e)
        {
            BasePage.ResponseRedirect("RoleManage.aspx");
        }

        protected void BtnSubmit_Click(object sender, EventArgs e)
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
            RolePermissions.DeleteFieldPermissionFromRoles(this.m_RoleId);
            RolePermissions.AddFieldPermissions(this.m_RoleId, OperateCode.ContentFieldInput, this.m_modelIdList.ToString(), this.m_fieldNameList.ToString());
            AdminPage.WriteSuccessMsg("<li>设置字段权限成功！</li>", "RoleManage.aspx");
        }

        private void InitRole()
        {
            if (!PEContext.Current.Admin.IsSuperAdmin)
            {
                AdminPage.WriteErrMsg("<li>本模块只有超级管理员身份才能访问！</li>");
            }
            this.m_RoleId = BasePage.RequestInt32("RoleId");
            RoleInfo roleInfoByRoleId = UserRole.GetRoleInfoByRoleId(this.m_RoleId);
            if (roleInfoByRoleId.IsNull)
            {
                AdminPage.WriteErrMsg("没有建立角色，请检查该角色是否存在！");
            }
            this.LblRoleName.Text = roleInfoByRoleId.RoleName;
            this.LblDescription.Text = roleInfoByRoleId.Description;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.InitRole();
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

        protected override object SaveViewState()
        {
            return null;
        }
    }
}

