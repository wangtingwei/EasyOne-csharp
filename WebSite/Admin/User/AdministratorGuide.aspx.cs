namespace EasyOne.WebSite.Admin.User
{
    using EasyOne.Components;
    using EasyOne.UserManage;
    using EasyOne.Web.UI;
    using System;
    using System.Web.UI.WebControls;

    public partial class AdministratorGuide : AdminPage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!PEContext.Current.Admin.IsSuperAdmin)
            {
                AdminPage.WriteErrMsg("<li>本模块只有超级管理员身份才能访问！</li>");
            }
            this.RptRoles.DataSource = UserRole.GetRoleList(0, 0);
            this.RptRoles.DataBind();
        }
    }
}

