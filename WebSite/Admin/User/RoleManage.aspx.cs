namespace EasyOne.WebSite.Admin.User
{
    using EasyOne.Common;
    using EasyOne.Components;
    using EasyOne.Controls;
    using EasyOne.Model.UserManage;
    using EasyOne.UserManage;
    using EasyOne.Web.UI;
    using System;
    using System.Web.UI.WebControls;

    public partial class RoleManage : AdminPage
    {

        protected void Egv_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                RoleInfo dataItem = (RoleInfo) e.Row.DataItem;
                if (dataItem.RoleId == 0)
                {
                    ((LinkButton) e.Row.FindControl("LnkModify")).Enabled = false;
                    ((LinkButton) e.Row.FindControl("LnkModifyCommonPermissions")).Enabled = false;
                    ((LinkButton) e.Row.FindControl("LnkModifyFieldPermissions")).Enabled = false;
                    LinkButton button = (LinkButton) e.Row.FindControl("LnkDelete");
                    button.Enabled = false;
                    button.OnClientClick = "";
                }
            }
        }

        protected void EgvUserRole_RowCommand(object sender, CommandEventArgs e)
        {
            bool flag = false;
            int roleId = DataConverter.CLng(e.CommandArgument);
            string commandName = e.CommandName;
            if (commandName != null)
            {
                if (!(commandName == "ModifyRole"))
                {
                    if (commandName == "DeleteRole")
                    {
                        flag = UserRole.Delete(roleId);
                    }
                    else if (commandName == "CommonPermissions")
                    {
                        BasePage.ResponseRedirect("RolePermissions.aspx?Action=Modify&RoleId=" + roleId);
                    }
                    else if (commandName == "FieldPermissions")
                    {
                        BasePage.ResponseRedirect("RoleFieldPermissions.aspx?PermissionsType=Role&RoleId=" + roleId);
                    }
                }
                else
                {
                    BasePage.ResponseRedirect("Role.aspx?Action=Modify&RoleId=" + roleId);
                }
            }
            if (flag)
            {
                AdminPage.WriteSuccessMsg("删除角色成功！", "RoleManage.aspx");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!PEContext.Current.Admin.IsSuperAdmin)
            {
                AdminPage.WriteErrMsg("<li>本模块只有超级管理员身份才能访问！</li>");
            }
        }
    }
}

