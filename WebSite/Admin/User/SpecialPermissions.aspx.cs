namespace EasyOne.WebSite.Admin.User
{
    using EasyOne.AccessManage;
    using EasyOne.Common;
    using EasyOne.Contents;
    using EasyOne.Controls;
    using EasyOne.Enumerations;
    using EasyOne.ExtendedControls;
    using EasyOne.Model.AccessManage;
    using EasyOne.Web.UI;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Web.UI.WebControls;

    public partial class SpecialPermissions : AdminPage
    {
        private int m_IdType;
        private bool m_inputSpecialAll;
        private string m_inputSpecialId;
        private StringBuilder m_inputSpecialIds = new StringBuilder();
        private bool m_manageSpecialAll;
        private string m_manageSpecialId;
        private StringBuilder m_manageSpecialIds = new StringBuilder();
        private string m_PermissionsType = "";
        private int m_RoleId;

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

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            this.m_inputSpecialAll = ((CheckBox) this.EgvSpecial.Rows[0].FindControl("ChkSpecialInput")).Checked;
            string permissionsType = this.m_PermissionsType;
            if (permissionsType != null)
            {
                if (!(permissionsType == "Role"))
                {
                    if (permissionsType == "User")
                    {
                        this.SaveUserSpecialPermissions();
                    }
                }
                else
                {
                    this.SaveRoleSpecialPermissions();
                }
            }
            this.Page.ClientScript.RegisterClientScriptBlock(base.GetType(), "windowsclose", "<script type='text/javascript'>window.close();</script>");
        }

        protected void EgvSpecial_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            string str;
            if ((e.Row.RowType == DataControlRowType.DataRow) && ((str = this.m_PermissionsType) != null))
            {
                if (!(str == "Role"))
                {
                    if (!(str == "User"))
                    {
                        return;
                    }
                }
                else
                {
                    this.RoleSpecialPermissions(e);
                    return;
                }
                this.UserSpecialPermissions(e);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.m_PermissionsType = BasePage.RequestString("PermissionsType");
            this.m_RoleId = BasePage.RequestInt32("RoleId");
            this.m_IdType = BasePage.RequestInt32("IdType");
            if ((this.m_RoleId <= 0) && (this.m_RoleId != -2))
            {
                AdminPage.WriteErrMsg("当前ID不存在！");
            }
            if (this.m_PermissionsType == "User")
            {
                this.EgvSpecial.Columns[3].Visible = false;
            }
        }

        private void RoleSpecialPermissions(GridViewRowEventArgs e)
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
                        box.Attributes.Add("onclick", "ChkSpecialAll2('" + box.ID + "'," + this.m_inputSpecialId + "," + box2.ClientID + ")");
                        box2.Attributes.Add("onclick", "ChkSpecialManageAll(this.form,'" + box2.ID + "'," + this.m_manageSpecialId + ")");
                    }
                    else
                    {
                        box.Attributes.Add("onclick", "ChkWipeOffSpecialPermissionsAll(" + this.m_inputSpecialId + ",'" + box2.ClientID + "'," + box.ClientID + ")");
                        box2.Attributes.Add("onclick", "ChkWipeOffSpecialManageAll(" + this.m_manageSpecialId + ",'" + box2.ClientID + "'," + this.m_inputSpecialId + ")");
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

        private void SaveRoleSpecialPermissions()
        {
            this.m_inputSpecialAll = ((CheckBox) this.EgvSpecial.Rows[0].FindControl("ChkSpecialInput")).Checked;
            this.m_manageSpecialAll = ((CheckBox) this.EgvSpecial.Rows[0].FindControl("ChkSpecialManage")).Checked;
            int num = 0;
            foreach (GridViewRow row in this.EgvSpecial.Rows)
            {
                CheckBox box = (CheckBox) row.FindControl("ChkSpecialInput");
                CheckBox box2 = (CheckBox) row.FindControl("ChkSpecialManage");
                HiddenField field = (HiddenField) row.FindControl("HdnSpecialId");
                if ((box.Checked && (field.Value != "0")) || (this.m_inputSpecialAll && (field.Value != "0")))
                {
                    StringHelper.AppendString(this.m_inputSpecialIds, field.Value);
                }
                if ((box2.Checked && (field.Value != "0")) || (this.m_manageSpecialAll && (field.Value != "0")))
                {
                    StringHelper.AppendString(this.m_manageSpecialIds, field.Value);
                }
                if (field.Value != "0")
                {
                    num++;
                }
            }
            AppendAllId(this.m_inputSpecialIds, num - 1);
            AppendAllId(this.m_manageSpecialIds, num - 1);
            RolePermissions.DeleteSpecialPermissionFromRoles(this.m_RoleId);
            RolePermissions.AddSepcialPermissionToRoles(this.m_RoleId, this.m_inputSpecialIds.ToString(), OperateCode.SpecialContentInput);
            RolePermissions.AddSepcialPermissionToRoles(this.m_RoleId, this.m_manageSpecialIds.ToString(), OperateCode.SepcialContentManage);
        }

        private void SaveUserSpecialPermissions()
        {
            this.m_inputSpecialAll = ((CheckBox) this.EgvSpecial.Rows[0].FindControl("ChkSpecialInput")).Checked;
            int num = 0;
            foreach (GridViewRow row in this.EgvSpecial.Rows)
            {
                CheckBox box = (CheckBox) row.FindControl("ChkSpecialInput");
                HiddenField field = (HiddenField) row.FindControl("HdnSpecialId");
                if ((box.Checked && (field.Value != "0")) || (this.m_inputSpecialAll && (field.Value != "0")))
                {
                    StringHelper.AppendString(this.m_inputSpecialIds, field.Value);
                }
                if (field.Value != "0")
                {
                    num++;
                }
            }
            AppendAllId(this.m_inputSpecialIds, num - 1);
            UserPermissions.DeleteSpecialPermissions(this.m_RoleId);
            UserPermissions.AddSpecialPermissions(this.m_RoleId, OperateCode.SpecialContentInput, this.m_inputSpecialIds.ToString(), this.m_IdType);
        }

        private void UserSpecialPermissions(GridViewRowEventArgs e)
        {
            SpecialTree dataItem = (SpecialTree) e.Row.DataItem;
            ExtendedLabel label = (ExtendedLabel) e.Row.FindControl("LabName");
            CheckBox box = (CheckBox) e.Row.FindControl("ChkSpecialInput");
            HiddenField field = (HiddenField) e.Row.FindControl("HdnSpecialId");
            if (dataItem != null)
            {
                label.Text = dataItem.Name;
                label.BeginTag = Special.TreeLine(dataItem.TreeLineType);
                if (dataItem.Name == "所有专题")
                {
                    label.BeginTag = label.BeginTag + "<span style='color:red'>";
                    label.EndTag = "</span>";
                }
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
                    foreach (RoleSpecialPermissionsInfo info in UserPermissions.GetSpecialPermissionsBySpecialId(dataItem.Id, this.m_IdType))
                    {
                        if ((info.OperateCode == OperateCode.SpecialContentInput) && (info.GroupId == this.m_RoleId))
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
}

