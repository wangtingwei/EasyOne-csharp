namespace EasyOne.WebSite.Admin.User
{
    using EasyOne.AccessManage;
    using EasyOne.Common;
    using EasyOne.Components;
    using EasyOne.Controls;
    using EasyOne.Enumerations;
    using EasyOne.Model.UserManage;
    using EasyOne.UserManage;
    using EasyOne.Web.UI;
    using EasyOne.WebSite.Controls;
    using System;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public partial class ModifyPurview : AdminPage
    {
        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            int userId = DataConverter.CLng(this.HdnUsersId.Value);
            bool inheritGroupRole = DataConverter.CBoolean(this.RadlIsInheritGroupRole.SelectedValue);
            if (!inheritGroupRole && !Users.SaveUserPurview(this.UserIndividuation.PurviewInfo, userId))
            {
                AdminPage.WriteErrMsg("<li>保存用户权限信息失败！</li>", "");
            }
            if (Users.SaveUserPurview(inheritGroupRole, userId))
            {
                AdminPage.WriteSuccessMsg("<li>修改会员权限成功！</li>", "UserShow.aspx?UserID=" + userId.ToString());
            }
            else
            {
                AdminPage.WriteErrMsg("<li>修改会员权限失败！</li>", "UserShow.aspx?UserID=" + userId.ToString());
            }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            if (!RolePermissions.AccessCheck(OperateCode.UserModifyPermissions))
            {
                throw new CustomException(PEExceptionType.ExceedAuthority);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                UserInfo userById = Users.GetUserById(BasePage.RequestInt32("UserID"));
                if (userById.IsInheritGroupRole)
                {
                    this.UserIndividuation.Visible = false;
                    this.BtnSetNode.Visible = false;
                    this.BtnSpecial.Visible = false;
                    this.BtnField.Visible = false;
                }
                else
                {
                    this.UserIndividuation.Visible = true;
                }
                this.LblUserName.Text = userById.UserName;
                this.LblGroupName.Text = userById.GroupName;
                this.RadlIsInheritGroupRole.SelectedValue = userById.IsInheritGroupRole.ToString();
                this.HdnUsersId.Value = userById.UserId.ToString();
                if (string.IsNullOrEmpty(userById.UserSetting))
                {
                    UserGroupsInfo userGroupById = UserGroups.GetUserGroupById(userById.GroupId);
                    if (userGroupById.GroupId != -2)
                    {
                        this.UserIndividuation.PurviewInfo = UserGroups.GetGroupSetting(userGroupById.GroupSetting);
                    }
                }
                else
                {
                    this.UserIndividuation.PurviewInfo = userById.UserPurview;
                }
            }
        }

        protected void RadlIsInheritGroupRole_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.RadlIsInheritGroupRole.Items[1].Selected)
            {
                this.UserIndividuation.Visible = true;
                this.BtnSetNode.Visible = true;
                this.BtnSpecial.Visible = true;
                this.BtnField.Visible = true;
            }
            else
            {
                this.UserIndividuation.Visible = false;
                this.BtnSetNode.Visible = false;
                this.BtnSpecial.Visible = false;
                this.BtnField.Visible = false;
            }
        }
    }
}

