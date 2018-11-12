namespace EasyOne.WebSite.Admin.Profile
{
    using AjaxControlToolkit;
    using EasyOne.Common;
    using EasyOne.Components;
    using EasyOne.Controls;
    using EasyOne.Model.UserManage;
    using EasyOne.UserManage;
    using EasyOne.Web.UI;
    using System;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public partial class ModifyPassword : AdminPage
    {

        protected void BtnCancle_Click(object sender, EventArgs e)
        {
            BasePage.ResponseRedirect("MyWorktable.aspx");
        }

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            AdministratorInfo administratorByAdminName = Administrators.GetAdministratorByAdminName(PEContext.Current.Admin.AdministratorInfo.AdminName);
            if (StringHelper.ValidateMD5(StringHelper.MD5(this.TxtOldPassword.Text), administratorByAdminName.AdminPassword))
            {
                administratorByAdminName.AdminPassword = StringHelper.MD5(this.TxtPassword.Text);
                administratorByAdminName.LastModifyPasswordTime = new DateTime?(DateTime.Now);
                if (Administrators.Update(administratorByAdminName))
                {
                    AdminPage.WriteSuccessMsg("修改密码成功！", "MyWorktable.aspx");
                }
                else
                {
                    AdminPage.WriteErrMsg("修改密码失败！");
                }
            }
            else
            {
                AdminPage.WriteErrMsg("您的旧密码不对，请与超级管理员联系！");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (PEContext.Current.Admin.AdministratorInfo.IsLock || !PEContext.Current.Admin.AdministratorInfo.EnableModifyPassword)
            {
                AdminPage.WriteErrMsg("您不可以修改自己的密码，请与超级管理员联系！");
            }
        }
    }
}

