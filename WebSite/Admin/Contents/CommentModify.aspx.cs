namespace EasyOne.WebSite.Admin.Contents
{
    using EasyOne.Contents;
    using EasyOne.Controls;
    using EasyOne.Model.Contents;
    using EasyOne.Model.UserManage;
    using EasyOne.UserManage;
    using EasyOne.Web.UI;
    using EasyOne.WebSite.Controls;
    using System;
    using System.Text;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public partial class CommentModify : AdminPage
    {
        private string commonlink = ("CommentID=" + BasePage.RequestInt32("CommentID").ToString() + "&GeneralID=" + BasePage.RequestInt32("GeneralID").ToString() + "&NodeID=" + BasePage.RequestInt32("NodeID").ToString());

        protected void BtnReset_Click(object sender, EventArgs e)
        {
            BasePage.ResponseRedirect("CommentManage.aspx?" + this.commonlink);
        }

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            if (this.Page.IsValid)
            {
                CommentInfo commentInfo = Comment.GetCommentInfo(BasePage.RequestInt32("CommentID"));
                if (commentInfo.IsNull)
                {
                    AdminPage.WriteErrMsg("<li>该评论不存在，请返回！</li>");
                }
                if (commentInfo.UserId != 0)
                {
                    UserInfo userById = Users.GetUserById(commentInfo.UserId);
                    if (userById.IsNull)
                    {
                        AdminPage.WriteErrMsg("<li>该评论所属的用户不存在，请返回！</li>");
                    }
                    commentInfo.UserName = userById.UserName;
                    commentInfo.Email = userById.Email;
                }
                else
                {
                    commentInfo.Email = this.TxtEmail.Text;
                }
                commentInfo.CommentTitle = this.TxtCommentTitle.Text;
                commentInfo.Content = this.TxtCommentContent.Text;
                commentInfo.Score = this.ScoreControl.Score;
                if (this.ChkReplyIsPrivate.Checked)
                {
                    commentInfo.IsPrivate = true;
                }
                else
                {
                    commentInfo.IsPrivate = false;
                }
                if (Comment.Update(commentInfo))
                {
                    AdminPage.WriteSuccessMsg("<li>修改评论成功。</li>", "CommentManage.aspx?" + this.commonlink);
                }
                else
                {
                    AdminPage.WriteErrMsg("<li>修改评论失败，请返回。</li>");
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (BasePage.RequestInt32("UserId") <= 0)
            {
                this.ChkReplyIsPrivate.Enabled = false;
            }
            if (!base.IsPostBack)
            {
                CommentInfo commentInfo = Comment.GetCommentInfo(BasePage.RequestInt32("CommentID"));
                if (commentInfo.IsNull)
                {
                    AdminPage.WriteErrMsg("<li>该评论不存在，请返回！</li>");
                }
                if (commentInfo.UserId != 0)
                {
                    this.LblUserInfo.Visible = true;
                    StringBuilder builder = new StringBuilder();
                    UserInfo userById = Users.GetUserById(commentInfo.UserId);
                    if (userById.IsNull)
                    {
                        AdminPage.WriteErrMsg("<li>该评论所属的用户不存在，请返回！</li>");
                    }
                    builder.Append("<tr class='tdbg'>");
                    builder.Append("<td class='tdbgleft' align='right' style='width: 150px; '>");
                    builder.Append("<strong>用户名：&nbsp;</strong></td>");
                    builder.Append("<td class='tdbg' align='left' >");
                    builder.Append(userById.UserName);
                    builder.Append("</td></tr>");
                    builder.Append("<tr class='tdbg'>");
                    builder.Append("<td class='tdbgleft' align='right' style='width: 150px; '>");
                    builder.Append("<strong>邮件：&nbsp;</strong></td>");
                    builder.Append("<td class='tdbg' align='left' >");
                    builder.Append(userById.Email);
                    builder.Append("</td></tr>");
                    this.LblUserInfo.Text = builder.ToString();
                    this.trUser.Visible = false;
                    this.trEmail.Visible = false;
                }
                this.TxtUserName.Text = commentInfo.ReplyUserName;
                this.TxtEmail.Text = commentInfo.Email;
                this.TxtCommentTitle.Text = commentInfo.CommentTitle;
                this.TxtCommentContent.Text = commentInfo.Content;
                this.ScoreControl.Score = commentInfo.Score;
                if (commentInfo.IsPrivate)
                {
                    this.ChkReplyIsPrivate.Checked = true;
                }
            }
        }
    }
}

