namespace EasyOne.WebSite.Admin
{
    using EasyOne.Components;
    using EasyOne.Controls;
    using EasyOne.Model.UserManage;
    using EasyOne.UserManage;
    using EasyOne.Web.UI;
    using System;
    using System.Web.UI.WebControls;

    public partial class ChangeTheme : AdminPage
    {
        protected int m_ItemIndex;

        protected void BtnChangeTheme_Click(object sender, EventArgs e)
        {
            string adminName = PEContext.Current.Admin.AdminName;
            AdminProfileInfo adminProfile = AdminProfile.GetAdminProfile(adminName);
            adminProfile.AdminName = adminName;
            adminProfile.Theme = base.Request["Theme"];
            if (!adminProfile.IsNull)
            {
                AdminProfile.Update(adminProfile);
            }
            else
            {
                AdminProfile.Add(adminProfile);
            }
            this.Session.Add("AdminPage_StyleSheetTheme", base.Request["Theme"]);
            this.Session["IndexRightUrl"] = "Profile/ChangeTheme.aspx";
            base.Response.Write("<script type='text/javascript'>parent.location.reload()</script>");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                this.Session["IndexRightUrl"] = null;
            }
        }
    }
}

