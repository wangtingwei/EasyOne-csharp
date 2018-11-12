namespace EasyOne.WebSite.Admin.User
{
    using EasyOne.Common;
    using EasyOne.Controls;
    using EasyOne.Model.UserManage;
    using EasyOne.UserManage;
    using EasyOne.Web.UI;
    using System;
    using System.Web.UI.WebControls;
    using EasyOne.Enumerations;

    public partial class UserGroupManage : AdminPage
    {

        protected void EgvUserGroup_RowCommand(object sender, CommandEventArgs e)
        {
            string str;
            bool flag = false;
            int num = DataConverter.CLng(e.CommandArgument);
            if (((str = e.CommandName) != null) && (str == "DeleteUserGroup"))
            {
                BasePage.ResponseRedirect(AdminPage.AppendSecurityCode("UserGroupManage.aspx?Action=Delete&GroupId=" + num.ToString()));
            }
            if (flag)
            {
                AdminPage.WriteSuccessMsg("删除角色成功！", "RoleManage.aspx");
            }
        }

        protected void GdvUserGroup_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                UserGroupsInfo dataItem = (UserGroupsInfo) e.Row.DataItem;
                Label label = (Label) e.Row.FindControl("LblGroupType");
                label.Text = BasePage.EnumToHtml<GroupType>(dataItem.GroupType);
                HyperLink link = (HyperLink) e.Row.FindControl("HypUserList");
                link.Text = "列出会员";
                link.NavigateUrl = "UserManage.aspx?listType=10&GroupName=" + base.Server.UrlEncode(dataItem.GroupName) + "&GroupId=" + dataItem.GroupId.ToString();
                LinkButton button = (LinkButton) e.Row.FindControl("LnkDelete");
                if (dataItem.GroupId == -2)
                {
                    button.Enabled = false;
                    button.OnClientClick = "";
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (BasePage.RequestString("Action") == "Delete")
            {
                int id = BasePage.RequestInt32("GroupId");
                if (id == -2)
                {
                    AdminPage.WriteErrMsg("匿名会员组不能删除！", "UserGroupManage.aspx");
                }
                if (UserGroups.Delete(id))
                {
                    AdminPage.WriteSuccessMsg("删除成功！", "UserGroupManage.aspx");
                }
            }
        }
    }
}

