namespace EasyOne.WebSite.Admin.User
{
    using EasyOne.Common;
    using EasyOne.Controls;
    using EasyOne.Enumerations;
    using EasyOne.Model.UserManage;
    using EasyOne.UserManage;
    using EasyOne.Web.UI;
    using System;
    using System.Web.UI.WebControls;

    public partial class UserGroup : AdminPage
    {
        private int m_groupId;

        protected void ButtonCancel_Click(object sender, EventArgs e)
        {
            BasePage.ResponseRedirect("UserGroupManage.aspx", true);
        }

        protected void ButtonSubmit_Click(object sender, EventArgs e)
        {
            UserGroupsInfo userGroupById;
            GroupType type = (GroupType) Enum.Parse(typeof(GroupType), this.DropGropType.SelectedValue);
            if (this.HdnAction.Value == "Modify")
            {
                userGroupById = UserGroups.GetUserGroupById(DataConverter.CLng(this.HdnGroupId.Value));
            }
            else
            {
                userGroupById = new UserGroupsInfo();
            }
            userGroupById.GroupName = this.TxtGroupName.Text;
            userGroupById.Description = this.TxtDescription.Text;
            userGroupById.GroupType = type;
            DataActionState unknown = DataActionState.Unknown;
            if (this.Page.IsValid)
            {
                if (this.HdnAction.Value == "Modify")
                {
                    unknown = DataActionState.Exist;
                    if ((userGroupById.GroupName != this.HdnGroupName.Value) && UserGroups.GroupNameIsExist(userGroupById.GroupName))
                    {
                        this.ShowMessage(unknown);
                    }
                    unknown = UserGroups.Update(userGroupById);
                }
                else
                {
                    unknown = UserGroups.Add(userGroupById);
                    this.m_groupId = userGroupById.GroupId;
                }
                this.ShowMessage(unknown);
            }
        }

        private void InitData()
        {
            this.HdnGroupId.Value = BasePage.RequestString("GroupID");
            UserGroupsInfo userGroupById = UserGroups.GetUserGroupById(this.m_groupId);
            BasePage.SetSelectedIndexByValue(this.DropGropType, userGroupById.GroupType.ToString());
            this.TxtGroupName.Text = userGroupById.GroupName;
            this.TxtDescription.Text = userGroupById.Description;
            this.HdnGroupName.Value = userGroupById.GroupName;
            if (BasePage.RequestInt32("GroupID") == -2)
            {
                this.DropGropType.Enabled = false;
                this.TxtGroupName.Enabled = false;
            }
        }

        private void InitGrouType()
        {
            ListItem item = new ListItem();
            item.Text = BasePage.EnumToHtml<GroupType>(GroupType.Register);
            item.Value = GroupType.Register.ToString();
            this.DropGropType.Items.Add(item);
            item = new ListItem();
            item.Text = BasePage.EnumToHtml<GroupType>(GroupType.Agent);
            item.Value = GroupType.Agent.ToString();
            this.DropGropType.Items.Add(item);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.m_groupId = BasePage.RequestInt32("GroupId");
            if (!this.Page.IsPostBack)
            {
                this.InitGrouType();
                if (BasePage.RequestString("Action") == "Modify")
                {
                    this.HdnAction.Value = "Modify";
                    this.InitData();
                }
            }
        }

        private void ShowMessage(DataActionState flag)
        {
            switch (flag)
            {
                case DataActionState.Successed:
                    BasePage.ResponseRedirect("UserGroupPermissions.aspx?GroupID=" + this.m_groupId.ToString());
                    return;

                case DataActionState.Exist:
                    AdminPage.WriteErrMsg("<li>该会员组已经存在，请使用另一会员组名！</li>", "");
                    return;

                case DataActionState.Unknown:
                    AdminPage.WriteErrMsg("<li>会员组信息保存失败！</li>", "");
                    return;
            }
        }
    }
}

