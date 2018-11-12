namespace EasyOne.WebSite.Admin.User
{
    using EasyOne.Common;
    using EasyOne.Controls;
    using EasyOne.Enumerations;
    using EasyOne.Model.UserManage;
    using EasyOne.UserManage;
    using EasyOne.Web.UI;
    using EasyOne.WebSite.Controls;
    using System;
    using System.Web.UI.WebControls;

    public partial class UserGroupPermissions : AdminPage
    {
        private int groupId;

        protected void ButtonCancel_Click(object sender, EventArgs e)
        {
            BasePage.ResponseRedirect("UserGroupManage.aspx", true);
        }

        protected void ButtonSubmit_Click(object sender, EventArgs e)
        {
            UserGroupsInfo userGroupById = UserGroups.GetUserGroupById(DataConverter.CLng(this.HdnGroupId.Value));
            userGroupById.GroupSetting = new Serialize<UserPurviewInfo>().SerializeField(this.UserIndividuation.PurviewInfo);
            switch (UserGroups.Update(userGroupById))
            {
                case DataActionState.Successed:
                    AdminPage.WriteSuccessMsg("<li>设置" + userGroupById.GroupName + "会员组权限成功！</li>", "UserGroupManage.aspx");
                    return;

                case DataActionState.Exist:
                    break;

                case DataActionState.Unknown:
                    AdminPage.WriteErrMsg("<li>设置" + userGroupById.GroupName + "会员组权限失败！</li>", "UserGroupManage.aspx");
                    break;

                default:
                    return;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.groupId = BasePage.RequestInt32("GroupID");
            this.HdnGroupId.Value = BasePage.RequestString("GroupID");
            if (!this.Page.IsPostBack)
            {
                UserGroupsInfo userGroupById = UserGroups.GetUserGroupById(this.groupId);
                this.LblGroupName.Text = userGroupById.GroupName;
                this.LblDescription.Text = userGroupById.Description;
                this.HdnGroupName.Value = userGroupById.GroupName;
                this.LblGropType.Text = BasePage.EnumToHtml<GroupType>(userGroupById.GroupType);
                if (!string.IsNullOrEmpty(userGroupById.GroupSetting))
                {
                    this.UserIndividuation.PurviewInfo = UserGroups.GetGroupSetting(userGroupById.GroupSetting);
                }
            }
        }
    }
}

