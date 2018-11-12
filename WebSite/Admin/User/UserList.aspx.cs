namespace EasyOne.WebSite.Admin.User
{
    using EasyOne.Common;
    using EasyOne.Components;
    using EasyOne.Controls;
    using EasyOne.ExtendedControls;
    using EasyOne.Model.UserManage;
    using EasyOne.UserManage;
    using EasyOne.Web.UI;
    using System;
    using System.Collections.Generic;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public partial class UserList : AdminPage
    {
        protected int i;
        protected string m_allUser;
        protected string m_allUserName;
        protected int m_GroupID;
        protected string m_UserInput;
        private void BindData(int userGroupId, string keyword)
        {
            IList<string> list = Users.GetUserNameList((this.Pager.CurrentPageIndex - 1) * this.Pager.PageSize, this.Pager.PageSize, userGroupId, keyword);
            if (list.Count == 0)
            {
                this.DivAdd.Visible = false;
                this.DivUserName.Visible = true;
            }
            else
            {
                this.DivUserName.Visible = false;
            }
            this.RepUser.DataSource = list;
            this.Pager.RecordCount = Users.GetUserNameListTotal(userGroupId, keyword);
            this.RepUser.DataBind();
        }

        private void CheckKeyword()
        {
            string str = DataSecurity.FilterBadChar(base.Request.Form["TxtKeyWord"]);
            if (string.IsNullOrEmpty(str))
            {
                this.BindData(0, this.m_GroupID.ToString());
            }
            else
            {
                this.BindData(1, str);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!PEContext.Current.Admin.Identity.IsAuthenticated)
            {
                AdminPage.WriteErrMsg("<li>对不起，您还没有登录不能访问此页面。</li>");
            }
            this.m_GroupID = BasePage.RequestInt32("GroupID");
            if (this.m_GroupID == 0)
            {
                this.m_GroupID = 1;
            }
            this.m_UserInput = BasePage.RequestString("OpenerText");
            this.Pager.PageSize = 20;
            this.CheckKeyword();
        }

        protected void Pager_PageChanged(object src, PageChangedEventArgs e)
        {
            this.Pager.CurrentPageIndex = e.NewPageIndex;
            this.CheckKeyword();
        }

        protected void RepUser_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
            {
                if (string.IsNullOrEmpty(this.m_allUser))
                {
                    this.m_allUser = e.Item.DataItem.ToString();
                }
                else
                {
                    this.m_allUser = this.m_allUser + "," + e.Item.DataItem.ToString();
                }
            }
        }

        protected void RepUserGroup_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
            {
                ExtendedLabel label = (ExtendedLabel) e.Item.FindControl("LblTitle");
                if (this.m_GroupID == ((UserGroupsInfo) e.Item.DataItem).GroupId)
                {
                    label.BeginTag = "| <span style='color:#ff6600'>";
                    label.Text = ((UserGroupsInfo) e.Item.DataItem).GroupName;
                    label.EndTag = "</span>";
                }
                else
                {
                    label.BeginTag = "| <a href=\"UserList.aspx?OpenerText=" + this.m_UserInput + "&GroupID=" + ((UserGroupsInfo) e.Item.DataItem).GroupId.ToString() + "\">";
                    label.Text = ((UserGroupsInfo) e.Item.DataItem).GroupName;
                    label.EndTag = "</a>";
                }
            }
        }
    }
}

