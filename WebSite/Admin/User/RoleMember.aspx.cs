namespace EasyOne.WebSite.Admin.User
{
    using EasyOne.Components;
    using EasyOne.Controls;
    using EasyOne.Model.UserManage;
    using EasyOne.UserManage;
    using EasyOne.Web.UI;
    using System;
    using System.Collections.Generic;
    using System.Web.UI.WebControls;

    public partial class RoleMember : AdminPage
    {
        protected void BelongToRole(ListControl dropName, int roleId)
        {
            IList<AdministratorInfo> memberListByRoleId = RoleMembers.GetMemberListByRoleId(roleId);
            if (memberListByRoleId.Count > 0)
            {
                dropName.Items.Clear();
                dropName.DataSource = memberListByRoleId;
                dropName.DataBind();
            }
            else
            {
                dropName.Items.Clear();
            }
        }

        protected void BtnConfirm_Click(object sender, EventArgs e)
        {
            if (this.Page.IsValid)
            {
                int roleId = BasePage.RequestInt32("RoleId");
                string str = BasePage.RequestString("RoleName");
                RoleMembers.AddMembersToRole(this.HdnBelongToRole.Value, roleId);
                AdminPage.WriteSuccessMsg("<li>成功保存了" + str + "角色的成员！</li>", "RoleManage.aspx");
            }
        }

        public void NotBelongRoleDataBind(ListControl dropName, int roleId)
        {
            IList<AdministratorInfo> memberListNotInRole = RoleMembers.GetMemberListNotInRole(roleId);
            if (memberListNotInRole.Count > 0)
            {
                dropName.Items.Clear();
                dropName.DataSource = memberListNotInRole;
                dropName.DataBind();
            }
            else
            {
                dropName.Items.Clear();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!PEContext.Current.Admin.IsSuperAdmin)
            {
                AdminPage.WriteErrMsg("<li>本模块只有超级管理员身份才能访问！</li>");
            }
            int roleId = BasePage.RequestInt32("RoleId");
            if (roleId != 0)
            {
                RoleInfo roleInfoByRoleId = UserRole.GetRoleInfoByRoleId(roleId);
                if (roleInfoByRoleId.IsNull)
                {
                    AdminPage.WriteErrMsg("没有建立角色，请检查该角色是否存在！");
                }
                this.LblRoleName.Text = roleInfoByRoleId.RoleName;
                this.LblDescription.Text = roleInfoByRoleId.Description;
                this.LblRoleName2.Text = roleInfoByRoleId.RoleName;
            }
            else
            {
                this.LblRoleName.Text = "超级管理员";
                this.LblDescription.Text = "";
                this.LblRoleName2.Text = "超级管理员";
            }
            if (!this.Page.IsPostBack)
            {
                this.NotBelongRoleDataBind(this.LstNotBelongRole, roleId);
                this.BelongToRole(this.LstBelongToRole, roleId);
                this.BtnConfirm.OnClientClick = "return GetBelongToRole(" + this.LstBelongToRole.ClientID + ");";
            }
        }
    }
}

