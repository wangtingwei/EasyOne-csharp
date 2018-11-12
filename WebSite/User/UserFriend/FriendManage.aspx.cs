namespace EasyOne.WebSite.User
{
    using EasyOne.Common;
    using EasyOne.Components;
    using EasyOne.Controls;
    using EasyOne.UserManage;
    using EasyOne.Web.UI;
    using EasyOne.WebSite.Controls;
    using System;
    using System.Data;
    using System.Text;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public partial class FriendManage : DynamicPage
    {

        private DataTable m_FriendGroup;
        private string m_UserName;

        protected void BtnDelete_Click(object sender, EventArgs e)
        {
            StringBuilder selectList = new StringBuilder();
            selectList = this.EgvFriend.SelectList;
            if (selectList.Length == 0)
            {
                DynamicPage.WriteErrMsg("<li>对不起，您还没有选择要删除的用户</li>");
            }
            else if (UserFriend.Delete(selectList.ToString()))
            {
                DynamicPage.WriteSuccessMsg("批量删除成功！", "FriendManage.aspx");
            }
            else
            {
                DynamicPage.WriteErrMsg("<li>删除失败</li>");
            }
        }

        protected void BtnMoveFriend_Click(object sender, EventArgs e)
        {
            int groupId = DataConverter.CLng(this.DropFriendGroup.SelectedValue, -1);
            if (groupId < 0)
            {
                DynamicPage.WriteErrMsg("<li>请选择移动到的组别</li>");
            }
            else
            {
                string str = this.EgvFriend.SelectList.ToString();
                if (string.IsNullOrEmpty(str))
                {
                    DynamicPage.WriteErrMsg("<li>请选择要移动的用户</li>");
                }
                else if (UserFriend.MoveByGroupId(str, groupId))
                {
                    DynamicPage.WriteSuccessMsg("批量移动操作成功！", "FriendManage.aspx");
                }
                else
                {
                    DynamicPage.WriteErrMsg("<li>批量移动操作失败！</li>");
                }
            }
        }

        private void DropFriendGroupDataBind(DataTable dataTable)
        {
            this.DropFriendGroup.DataSource = dataTable;
            this.DropFriendGroup.DataBind();
            this.DropFriendGroup.SelectedValue = "-1";
        }

        protected void EgvFriend_RowCommand(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "Del")
            {
                if (UserFriend.Delete(e.CommandArgument.ToString()))
                {
                    DynamicPage.WriteSuccessMsg("删除成功！", "FriendManage.aspx");
                }
                else
                {
                    DynamicPage.WriteErrMsg("<li>删除失败</li>");
                }
            }
        }

        protected string GetGroupName(int groupId)
        {
            string str = string.Empty;
            if ((this.m_FriendGroup != null) && (this.m_FriendGroup.Rows.Count > groupId))
            {
                str = this.m_FriendGroup.Rows[groupId]["FriendGroupName"].ToString();
            }
            return str;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.m_UserName = PEContext.Current.User.UserName;
            if (!this.Page.IsPostBack)
            {
                this.HdnUserName.Value = this.m_UserName;
                this.m_FriendGroup = Users.GetFriendGroup(this.m_UserName);
                if (this.m_FriendGroup == null)
                {
                    DynamicPage.WriteErrMsg("数据库信息错误或删除了网站默认组！");
                }
                this.RptFriendGroup.DataSource = this.m_FriendGroup;
                this.RptFriendGroup.DataBind();
                this.DropFriendGroupDataBind(this.m_FriendGroup);
            }
        }
    }
}

