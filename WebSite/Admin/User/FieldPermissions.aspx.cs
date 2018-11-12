namespace EasyOne.WebSite.Admin.User
{
    using EasyOne.AccessManage;
    using EasyOne.Common;
    using EasyOne.CommonModel;
    using EasyOne.Enumerations;
    using EasyOne.ExtendedControls;
    using EasyOne.Model.AccessManage;
    using EasyOne.Model.CommonModel;
    using EasyOne.Web.UI;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public partial class FieldPermissions : AdminPage
    {
        public StringBuilder m_ArrModelTr = new StringBuilder();
        public StringBuilder m_ArrTable = new StringBuilder();
        private StringBuilder m_fieldNameList = new StringBuilder();
        private int m_IdType;
        private StringBuilder m_modelIdList = new StringBuilder();
        private string m_PermissionsType = "";
        private int m_RoleId;

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            string str;
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
            if (((str = this.m_PermissionsType) != null) && (str == "User"))
            {
                UserPermissions.DeleteFieldPermissions(this.m_RoleId, this.m_IdType);
                UserPermissions.AddFieldPermissions(this.m_RoleId, this.m_modelIdList.ToString(), this.m_fieldNameList.ToString(), OperateCode.ContentFieldInput, this.m_IdType);
            }
            this.Page.ClientScript.RegisterClientScriptBlock(base.GetType(), "windowsclose", "<script type='text/javascript'>window.close();</script>");
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
                    ExtendedLabel label = (ExtendedLabel) e.Item.FindControl("modellist");
                    label.BeginTag = "<a href=\"javascript:Hidd(" + e.Item.ItemIndex.ToString() + ")\">";
                    label.Text = ((ModelInfo) e.Item.DataItem).ModelName;
                    label.EndTag = "</a>";
                }
            }
        }

        protected void RptModelList2_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
            {
                string str;
                Repeater repeater = (Repeater) e.Item.FindControl("RptFieldList");
                repeater.DataSource = ModelManager.GetFieldListByModelId(((ModelInfo) e.Item.DataItem).ModelId);
                repeater.DataBind();
                IList<RoleFieldPermissionsInfo> fieldPermissionsByGroupId = new List<RoleFieldPermissionsInfo>();
                if (((str = this.m_PermissionsType) != null) && (str == "User"))
                {
                    fieldPermissionsByGroupId = UserPermissions.GetFieldPermissionsByGroupId(this.m_RoleId, this.m_IdType);
                }
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
    }
}

