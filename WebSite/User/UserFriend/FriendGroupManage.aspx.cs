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
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public partial class FriendGroupManage : DynamicPage
    {

        protected string m_UserName;

        protected void EgvFriendGroup_RowCommand(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "Del")
            {
                int friendGroupId = DataConverter.CLng(e.CommandArgument, -1);
                if (friendGroupId < 0)
                {
                    DynamicPage.WriteErrMsg("<li>请选择要删除的好友组别！</li>");
                }
                else if (Users.DeleteFriendGroup(this.m_UserName, friendGroupId))
                {
                    DynamicPage.WriteSuccessMsg("删除好友组别成功", "FriendGroupManage.aspx");
                }
                else
                {
                    DynamicPage.WriteErrMsg("<li>删除不成功！</li>");
                }
            }
        }

        protected void EgvFriendGroup_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView dataItem = e.Row.DataItem as DataRowView;
                e.Row.Cells[2].Text = UserFriend.GetFriendCount(DataConverter.CLng(dataItem["FriendGroupID"]), this.m_UserName).ToString();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.m_UserName = PEContext.Current.User.UserName;
            if (!this.Page.IsPostBack)
            {
                DataTable friendGroup = Users.GetFriendGroup(this.m_UserName);
                if (friendGroup != null)
                {
                    this.EgvFriendGroup.DataSource = friendGroup;
                    this.EgvFriendGroup.DataBind();
                }
                else
                {
                    DynamicPage.WriteErrMsg("<li>数据库信息错误或删除了网站默认组！</li>");
                }
            }
        }
    }
}

