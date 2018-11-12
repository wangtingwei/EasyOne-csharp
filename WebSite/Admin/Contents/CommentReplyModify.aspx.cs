namespace EasyOne.WebSite.Admin.Contents
{
    using EasyOne.Contents;
    using EasyOne.Controls;
    using EasyOne.Model.Contents;
    using EasyOne.Model.UserManage;
    using EasyOne.UserManage;
    using EasyOne.Web.UI;
    using System;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public partial class CommentReplyModify : AdminPage
    {
        private string commonlink = ("CommentID=" + BasePage.RequestInt32("CommentID").ToString() + "&GeneralID=" + BasePage.RequestInt32("GeneralID").ToString() + "&NodeID=" + BasePage.RequestInt32("NodeID").ToString() + "&Title=" + BasePage.RequestInt32("Title").ToString());

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
                commentInfo.Reply = this.TxtCommentReply.Text;
                if (Comment.Update(commentInfo))
                {
                    AdminPage.WriteSuccessMsg("<li>管理员修改回复评论成功。</li>", "CommentManage.aspx?" + this.commonlink);
                }
                else
                {
                    AdminPage.WriteErrMsg("<li>管理员修改回复评论失败，请返回。</li>");
                }
            }
        }

        protected void InitComment()
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
                this.LblUserName.Text = userById.UserName;
                this.LblEmail.Text = userById.Email;
            }
            else
            {
                if (!string.IsNullOrEmpty(commentInfo.UserName))
                {
                    this.LblUserName.Text = commentInfo.UserName;
                }
                this.LblEmail.Text = commentInfo.Email;
            }
            this.LblScore.Text = commentInfo.Score.ToString();
            this.TxtCommentReply.Text = commentInfo.Reply;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                this.InitComment();
            }
        }
    }
}

