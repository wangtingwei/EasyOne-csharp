namespace EasyOne.WebSite.Admin.User
{
    using EasyOne.Common;
    using EasyOne.Controls;
    using EasyOne.UserManage;
    using EasyOne.Web.UI;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Web.UI.WebControls;
    using EasyOne.Model.UserManage;

    public partial class BatchMove : AdminPage
    {

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            bool flag = true;
            string userId = this.TxtBatchUserID.Text.Trim();
            string text = this.TxtBatchUserName.Text;
            int startUserId = DataConverter.CLng(this.TxtStartUserId.Text);
            int endUserId = DataConverter.CLng(this.TxtEndUserId.Text);
            int groupId = DataConverter.CLng(this.LstUserGroupID.SelectedValue);
            if (groupId == 0)
            {
                AdminPage.WriteErrMsg("<li>请选择要移动的会员组！</li>", "UserManage.aspx");
            }
            if (this.RadUserType1.Checked)
            {
                flag = Users.MoveByUsers(userId, groupId);
            }
            else if (this.RadUserType2.Checked)
            {
                flag = Users.MoveByUserName(text, groupId);
            }
            else if (this.RadUserType3.Checked)
            {
                flag = Users.MoveBetweenUserId(startUserId, endUserId, groupId);
            }
            else if (this.RadUserType4.Checked)
            {
                StringBuilder sb = new StringBuilder("");
                for (int i = 0; i < this.LstBatchUserGroupID.Items.Count; i++)
                {
                    if (this.LstBatchUserGroupID.Items[i].Selected)
                    {
                        StringHelper.AppendString(sb, this.LstBatchUserGroupID.Items[i].Value);
                    }
                }
                if (sb.Length == 0)
                {
                    AdminPage.WriteErrMsg("<li>请选择要移动的会员组！</li>", "UserManage.aspx");
                }
                else
                {
                    flag = Users.MoveByGroups(sb.ToString(), groupId);
                }
            }
            if (flag)
            {
                AdminPage.WriteSuccessMsg("<li>批量移动用户成功！</li>", "UserManage.aspx");
            }
            else
            {
                AdminPage.WriteErrMsg("<li>批量移动用户失败！</li>", "UserManage.aspx");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                if (!string.IsNullOrEmpty(BasePage.RequestString("UserID")))
                {
                    this.TxtBatchUserID.Text = BasePage.RequestString("UserID");
                }
                this.UserGroupList(this.LstBatchUserGroupID);
                this.UserGroupList(this.LstUserGroupID);
            }
        }

        protected void UserGroupList(ListControl chklUserGroupList)
        {
            IList<UserGroupsInfo> userGroupList = UserGroups.GetUserGroupList(0, 0);
            chklUserGroupList.Items.Clear();
            chklUserGroupList.DataSource = userGroupList;
            chklUserGroupList.DataTextField = "GroupName";
            chklUserGroupList.DataValueField = "GroupId";
            chklUserGroupList.DataBind();
        }
    }
}

