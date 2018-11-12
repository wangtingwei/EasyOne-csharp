namespace EasyOne.WebSite.User
{
    using EasyOne.Components;
    using EasyOne.Controls;
    using EasyOne.Model.UserManage;
    using EasyOne.UserManage;
    using EasyOne.Web.UI;
    using EasyOne.WebSite.Controls;
    using System;
    using System.Text;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public partial class FriendGroup : DynamicPage
    {
        private string m_Action;
        private int m_GroupId;
        private string m_UserName;

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            if (this.Page.IsValid)
            {
                string text = this.TxtGroupName.Text;
                if (this.m_Action != "modify")
                {
                    UserInfo usersByUserName = Users.GetUsersByUserName(this.m_UserName);
                    if (usersByUserName.IsNull)
                    {
                        DynamicPage.WriteErrMsg("<li>找不到对应的会员信息！</li>");
                    }
                    else if (string.IsNullOrEmpty(usersByUserName.UserFriendGroup))
                    {
                        DynamicPage.WriteErrMsg("<li>数据库信息错误或删除了网站默认组！</li>");
                    }
                    else
                    {
                        int length = usersByUserName.UserFriendGroup.Split(new char[] { '$' }).Length;
                        if ((length < 1) || (length > 7))
                        {
                            DynamicPage.WriteErrMsg("<li>数据库信息错误或删除了网站默认组或添加组超过8个了！</li>");
                        }
                        else
                        {
                            StringBuilder builder = new StringBuilder();
                            builder.Append(usersByUserName.UserFriendGroup);
                            if (!usersByUserName.UserFriendGroup.EndsWith("$", StringComparison.Ordinal))
                            {
                                builder.Append("$");
                            }
                            builder.Append(text);
                            if (Users.UpdateUserFriendGroup(this.m_UserName, builder.ToString()))
                            {
                                DynamicPage.WriteSuccessMsg("添加成功！", "FriendGroupManage.aspx");
                            }
                        }
                    }
                }
                else
                {
                    if (this.m_GroupId == 0)
                    {
                        DynamicPage.WriteErrMsg("<li>该成员组名称不允许修改</li>");
                    }
                    if (Users.UpdateFriendGroupName(this.m_UserName, this.m_GroupId, text))
                    {
                        DynamicPage.WriteSuccessMsg("修改成功！", "FriendGroupManage.aspx");
                    }
                    else
                    {
                        DynamicPage.WriteErrMsg("<li>修改失败！</li>");
                    }
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.m_UserName = PEContext.Current.User.UserName;
            this.m_Action = BasePage.RequestStringToLower("Action");
            this.m_GroupId = BasePage.RequestInt32("GroupID", -1);
            if (this.m_Action == "modify")
            {
                base.Title = "修改成员组";
                this.LblTitle.Text = "修改成员组";
                this.BtnSubmit.Text = "修改";
                if (!this.Page.IsPostBack)
                {
                    this.TxtGroupName.Text = BasePage.RequestString("GroupName");
                }
            }
        }
    }
}

