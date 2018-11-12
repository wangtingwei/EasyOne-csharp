namespace EasyOne.WebSite.Admin.User
{
    using AjaxControlToolkit;
    using EasyOne.AccessManage;
    using EasyOne.Common;
    using EasyOne.Components;
    using EasyOne.Controls;
    using EasyOne.Enumerations;
    using EasyOne.ExtendedControls;
    using EasyOne.Model.UserManage;
    using EasyOne.UserManage;
    using EasyOne.Web.UI;
    using System;
    using System.Collections.Generic;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public partial class Administrator : AdminPage
    {

        private void AddAdmin()
        {
            UserInfo usersByUserName = Users.GetUsersByUserName(this.TxtUserName.Text.Trim());
            if (usersByUserName.IsNull)
            {
                AdminPage.WriteErrMsg("此前台用户名不存在！");
            }
            AdministratorInfo adminInfo = new AdministratorInfo();
            adminInfo.AdminName = this.TxtAdminName.Text.Trim();
            adminInfo.UserName = this.TxtUserName.Text.Trim();
            adminInfo.IsLock = this.ChkIsLock.Checked;
            adminInfo.EnableModifyPassword = this.ChkEnableModifyPassword.Checked;
            if (string.IsNullOrEmpty(this.TxtPassword.Text.Trim()))
            {
                AdminPage.WriteErrMsg("管理员密码不能为空");
            }
            else
            {
                adminInfo.AdminPassword = StringHelper.MD5(this.TxtPassword.Text);
            }
            adminInfo.EnableMultiLogOn = this.ChkEnableMultiLogin.Checked;
            if (Administrators.IsExist(adminInfo.AdminName))
            {
                AdminPage.WriteErrMsg("已经存在同样的管理员名！");
            }
            if (!Administrators.GetAdministratorByUserName(usersByUserName.UserName).IsNull)
            {
                AdminPage.WriteErrMsg("此前台用户已经被添加为管理员了！");
            }
            if (Administrators.Add(adminInfo))
            {
                if (this.RadPurview1.Checked)
                {
                    RoleMembers.AddMemberToRoles(adminInfo.AdminId, "0");
                }
                else
                {
                    RoleMembers.AddMemberToRoles(adminInfo.AdminId, this.HdnBelongToRole.Value);
                }
                AdminPage.WriteSuccessMsg("添加管理员成功！", "AdministratorManage.aspx");
            }
            else
            {
                AdminPage.WriteErrMsg("添加管理员失败！");
            }
        }

        protected void BelongToRole(ListControl dropName, int adminId)
        {
            if (adminId != 0)
            {
                IList<RoleInfo> roleListByRoleId = UserRole.GetRoleListByRoleId(adminId);
                if (roleListByRoleId.Count > 0)
                {
                    dropName.Items.Clear();
                    dropName.DataSource = roleListByRoleId;
                    dropName.DataBind();
                }
                else
                {
                    dropName.Items.Clear();
                }
            }
        }

        protected void BtnCancle_Click(object sender, EventArgs e)
        {
            HttpContext.Current.Server.Transfer("AdministratorManage.aspx");
        }

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            if (base.IsValid)
            {
                if (string.Compare(BasePage.RequestString("Action"), "Modify", StringComparison.OrdinalIgnoreCase) == 0)
                {
                    this.ModifyAdmin();
                }
                else
                {
                    this.AddAdmin();
                }
            }
        }

        private void InitModify()
        {
            int adminId = BasePage.RequestInt32("AdminId");
            AdministratorInfo administratorByAdminId = Administrators.GetAdministratorByAdminId(adminId);
            if (administratorByAdminId.IsNull)
            {
                AdminPage.WriteErrMsg("不存在此管理员");
            }
            if (string.CompareOrdinal("," + RoleMembers.GetRoleIdListByAdminId(adminId) + ",", ",0,") == 0)
            {
                this.RadPurview1.Checked = true;
                this.RadPurview2.Checked = false;
                this.RolePurview.Style.Add("display", "none");
            }
            this.TxtAdminName.Text = administratorByAdminId.AdminName;
            this.TxtUserName.Text = administratorByAdminId.UserName;
            this.ChkEnableMultiLogin.Checked = administratorByAdminId.EnableMultiLogOn;
            this.ViewState["AdminName"] = administratorByAdminId.AdminName;
            this.ViewState["UserName"] = administratorByAdminId.UserName;
            this.ChkEnableModifyPassword.Checked = administratorByAdminId.EnableModifyPassword;
            this.ChkIsLock.Checked = administratorByAdminId.IsLock;
            if (PEContext.Current.Admin.AdministratorInfo.AdminId == adminId)
            {
                this.ChkIsLock.Enabled = false;
            }
            this.TxtAdminName.Enabled = false;
            this.ViewState["Password"] = administratorByAdminId.AdminPassword;
            this.ValrUserPassword.Enabled = false;
            this.CompareValidator1.Enabled = false;
            this.TrPassword.Style.Add("display", "none");
            this.LabTip.Text = "<font color=red>不修改密码请保持为空！</font>";
            if (!administratorByAdminId.EnableModifyPassword && !PEContext.Current.Admin.IsSuperAdmin)
            {
                this.TxtPassword.Enabled = false;
                this.TxtPassword2.Enabled = false;
                this.LabTip.Visible = false;
                this.ChkEnableModifyPassword.Enabled = false;
            }
        }

        private void ModifyAdmin()
        {
            AdministratorInfo administratorByAdminId = Administrators.GetAdministratorByAdminId(BasePage.RequestInt32("AdminId"));
            if (string.IsNullOrEmpty(this.TxtPassword.Text.Trim()))
            {
                administratorByAdminId.AdminPassword = this.ViewState["Password"].ToString();
            }
            else
            {
                administratorByAdminId.AdminPassword = StringHelper.MD5(this.TxtPassword.Text);
            }
            if ((!administratorByAdminId.EnableModifyPassword && (administratorByAdminId.AdminPassword != this.ViewState["Password"].ToString())) && !PEContext.Current.Admin.IsSuperAdmin)
            {
                AdminPage.WriteErrMsg("没有修改密码的权限！");
            }
            if (administratorByAdminId.AdminPassword != this.ViewState["Password"].ToString())
            {
                administratorByAdminId.LastModifyPasswordTime = new DateTime?(DateTime.Now);
            }
            if (string.Compare(this.TxtUserName.Text.Trim(), this.ViewState["UserName"].ToString(), StringComparison.OrdinalIgnoreCase) != 0)
            {
                UserInfo usersByUserName = Users.GetUsersByUserName(this.TxtUserName.Text.Trim());
                if (usersByUserName.IsNull)
                {
                    AdminPage.WriteErrMsg("此前台用户名不存在！");
                }
                if (!Administrators.GetAdministratorByUserName(usersByUserName.UserName).IsNull)
                {
                    AdminPage.WriteErrMsg("此前台用户已经被添加为管理员了！");
                }
            }
            administratorByAdminId.UserName = this.TxtUserName.Text.Trim();
            administratorByAdminId.IsLock = this.ChkIsLock.Checked;
            administratorByAdminId.EnableModifyPassword = this.ChkEnableModifyPassword.Checked;
            administratorByAdminId.EnableMultiLogOn = this.ChkEnableMultiLogin.Checked;
            if (Administrators.Update(administratorByAdminId))
            {
                if (this.RadPurview1.Checked)
                {
                    RoleMembers.AddMemberToRoles(administratorByAdminId.AdminId, "0");
                }
                else
                {
                    RoleMembers.AddMemberToRoles(administratorByAdminId.AdminId, this.HdnBelongToRole.Value);
                }
                AdminPage.WriteSuccessMsg("修改管理员成功！", "AdministratorManage.aspx");
            }
            else
            {
                AdminPage.WriteErrMsg("修改管理员失败！");
            }
        }

        public void NotBelongRoleDataBind(ListControl dropName, int adminId)
        {
            IList<RoleInfo> roleList;
            if (adminId == 0)
            {
                roleList = UserRole.GetRoleList(0, 0);
                roleList.RemoveAt(roleList.Count - 1);
            }
            else
            {
                roleList = UserRole.GetRoleListNotInRole(adminId);
            }
            if (roleList.Count > 0)
            {
                dropName.Items.Clear();
                dropName.DataSource = roleList;
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
            RolePermissions.BusinessAccessCheck(OperateCode.AdministratorManage);
            string str = BasePage.RequestString("UserName");
            if (!string.IsNullOrEmpty(str))
            {
                this.TxtUserName.Text = str;
            }
            if (!base.IsPostBack)
            {
                int adminId = BasePage.RequestInt32("AdminId");
                this.RadPurview1.Attributes.Add("onclick", "javascript:RadPurview(0);");
                this.RadPurview2.Attributes.Add("onclick", "javascript:RadPurview(1);");
                this.NotBelongRoleDataBind(this.LstNotBelongRole, adminId);
                this.BelongToRole(this.LstBelongToRole, adminId);
                this.BtnSubmit.OnClientClick = "return GetBelongToRole(" + this.LstBelongToRole.ClientID + ");";
                if (string.Compare(BasePage.RequestString("Action"), "Modify", StringComparison.OrdinalIgnoreCase) == 0)
                {
                    this.InitModify();
                }
            }
        }
    }
}

