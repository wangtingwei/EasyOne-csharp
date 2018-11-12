namespace EasyOne.WebSite.Admin.User
{
    using EasyOne.Components;
    using EasyOne.Web.UI;
    using System;

    public partial class RoleGuide : AdminPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!PEContext.Current.Admin.IsSuperAdmin)
            {
                AdminPage.WriteErrMsg("<li>本模块只有超级管理员身份才能访问！</li>");
            }
        }
    }
}

