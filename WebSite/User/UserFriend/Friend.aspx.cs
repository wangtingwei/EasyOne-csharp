namespace EasyOne.WebSite.User
{
    using EasyOne.Common;
    using EasyOne.Components;
    using EasyOne.Controls;
    using EasyOne.Model.UserManage;
    using EasyOne.UserManage;
    using EasyOne.Web.UI;
    using EasyOne.WebSite.Controls;
    using System;
    using System.Data;
    using System.Text;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public partial class Friend : DynamicPage
    {
        private string m_UserName;

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            string selectedValue = this.DropFriendGroup.SelectedValue;
            if (string.IsNullOrEmpty(selectedValue))
            {
                DynamicPage.WriteErrMsg("<li>成员组ID不能为空</li>");
            }
            if (string.IsNullOrEmpty(this.TxtFriendName.Text))
            {
                DynamicPage.WriteErrMsg("<li>好友用户名不能为空！</li>");
            }
            string[] strArray = this.TxtFriendName.Text.Split(new char[] { ',' });
            if (strArray.Length > 5)
            {
                DynamicPage.WriteErrMsg("<li>最多只能同时添加5个用户</li>");
            }
            StringBuilder builder = new StringBuilder();
            bool flag = true;
            foreach (string str2 in strArray)
            {
                if (Users.Exists(str2))
                {
                    if (!UserFriend.Exists(str2, this.m_UserName))
                    {
                        UserFriendInfo userFriendInfo = new UserFriendInfo();
                        userFriendInfo.FriendName = str2;
                        userFriendInfo.UserName = this.m_UserName;
                        userFriendInfo.GroupId = DataConverter.CLng(selectedValue);
                        if (UserFriend.Add(userFriendInfo))
                        {
                            flag = false;
                        }
                    }
                    else
                    {
                        builder.Append("<li>");
                        builder.Append(str2);
                        builder.Append("已经存在，不能重复添加！</li>");
                    }
                }
                else
                {
                    builder.Append("<li>");
                    builder.Append(str2);
                    builder.Append("用户不存在，只能添加存在的用户！</li>");
                }
            }
            string errorMessage = builder.ToString();
            if (flag)
            {
                DynamicPage.WriteErrMsg(errorMessage);
            }
            else if (string.IsNullOrEmpty(errorMessage))
            {
                DynamicPage.WriteSuccessMsg("添加成功！", "FriendManage.aspx");
            }
            else
            {
                DynamicPage.WriteSuccessMsg("添加成功！同时" + errorMessage, "FriendManage.aspx");
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
                    this.DropFriendGroup.DataSource = friendGroup;
                    this.DropFriendGroup.DataBind();
                    this.DropFriendGroup.SelectedValue = "1";
                }
                else
                {
                    DynamicPage.WriteErrMsg("<li>数据库信息错误或删除了网站默认组！</li>");
                }
            }
        }
    }
}

