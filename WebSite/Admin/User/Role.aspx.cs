namespace EasyOne.WebSite.Admin.User
{
    using EasyOne.Components;
    using EasyOne.Controls;
    using EasyOne.Model.UserManage;
    using EasyOne.UserManage;
    using EasyOne.Web.UI;
    using System;
    using System.Web.UI.WebControls;

    public partial class RoleUI : AdminPage
    {

        protected void BtnCancle_Click(object sender, EventArgs e)
        {
            BasePage.ResponseRedirect("RoleManage.aspx");
        }

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            if (base.IsValid)
            {
                RoleInfo roleInfo = new RoleInfo();
                roleInfo.RoleId = BasePage.RequestInt32("RoleId");
                roleInfo.RoleName = this.TxtRoleName.Text.Trim();
                roleInfo.Description = this.TxtDescription.Text.Trim();
                if (roleInfo.Description.Length > 0xff)
                {
                    AdminPage.WriteErrMsg("角色简介不能超过255个字符！");
                }
                if (string.Compare(BasePage.RequestString("Action"), "Modify", StringComparison.OrdinalIgnoreCase) == 0)
                {
                    if ((roleInfo.RoleName != this.ViewState["RoleName"].ToString()) && UserRole.IsExist(roleInfo.RoleName))
                    {
                        AdminPage.WriteErrMsg("已经存在同样的角色名！");
                    }
                    if (UserRole.Update(roleInfo))
                    {
                        BasePage.ResponseRedirect("RolePermissions.aspx?Action=Modify&RoleId=" + roleInfo.RoleId.ToString());
                    }
                }
                else
                {
                    if (UserRole.IsExist(roleInfo.RoleName))
                    {
                        AdminPage.WriteErrMsg("已经存在同样的角色名！");
                    }
                    if (UserRole.Add(roleInfo))
                    {
                        BasePage.ResponseRedirect("RolePermissions.aspx?Action=Add&RoleId=" + roleInfo.RoleId.ToString());
                    }
                }
            }
        }

        private void Modify()
        {
            if (!base.IsPostBack)
            {
                RoleInfo roleInfoByRoleId = UserRole.GetRoleInfoByRoleId(BasePage.RequestInt32("RoleId"));
                this.TxtRoleName.Text = roleInfoByRoleId.RoleName;
                this.TxtDescription.Text = roleInfoByRoleId.Description;
                this.ViewState["RoleName"] = roleInfoByRoleId.RoleName;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!PEContext.Current.Admin.IsSuperAdmin)
            {
                AdminPage.WriteErrMsg("<li>本模块只有超级管理员身份才能访问！</li>");
            }
            this.Modify();
        }
    }
}

