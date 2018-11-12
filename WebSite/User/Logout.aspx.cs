namespace EasyOne.WebSite
{
    using EasyOne.Api;
    using EasyOne.Components;
    using EasyOne.Model.UserManage;
    using EasyOne.UserManage;
    using EasyOne.Web.UI;
    using System;
    using System.Web.Security;
    using System.Web.UI.HtmlControls;

    public partial class LogOut : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (PEContext.Current.User.Identity.IsAuthenticated)
            {
                UserInfo usersByUserName = Users.GetUsersByUserName(PEContext.Current.User.UserName);
                if (ApiData.IsAPiEnable())
                {
                    ApiFunction.LogOff(usersByUserName.UserName);
                }
                FormsAuthentication.SignOut();
            }
            base.Response.Write("<script language=\"JavaScript\">window.location='../Default.aspx';</script>");
        }
    }
}

