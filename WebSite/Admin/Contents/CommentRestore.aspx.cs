namespace EasyOne.WebSite.Admin.Contents
{
    using EasyOne.Common;
    using EasyOne.Components;
    using EasyOne.Contents;
    using EasyOne.Controls;
    using EasyOne.ExtendedControls;
    using EasyOne.Model.Contents;
    using EasyOne.Web.UI;
    using System;
    using System.Web;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public partial class CommentRestore : AdminPage
    {
        protected string Path = "";

        protected void BtnReset_Click(object sender, EventArgs e)
        {
            BasePage.ResponseRedirect("CommentManage.aspx?NodeId=" + BasePage.RequestInt32("NodeId").ToString() + "&GeneralId=" + BasePage.RequestInt32("GeneralId").ToString());
        }

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            CommentInfo commentInfo = new CommentInfo();
            commentInfo.CommentId = BasePage.RequestInt32("CommentID");
            commentInfo.Reply = this.TxtCommentRestore.Text;
            if (!string.IsNullOrEmpty(PEContext.Current.Admin.AdminName))
            {
                commentInfo.ReplyAdmin = PEContext.Current.Admin.AdminName;
            }
            else
            {
                commentInfo.ReplyAdmin = "匿名发表";
            }
            commentInfo.ReplyDateTime = DateTime.Now;
            if (this.ChkReplyIsPrivate.Checked)
            {
                commentInfo.ReplyIsPrivate = true;
            }
            else
            {
                commentInfo.ReplyIsPrivate = false;
            }
            if (Comment.AdministratorReply(commentInfo))
            {
                AdminPage.WriteSuccessMsg("<li>回复指定评论成功。</li>", "CommentManage.aspx?NodeId=" + BasePage.RequestInt32("NodeId").ToString() + "&GeneralId=" + BasePage.RequestInt32("GeneralId").ToString());
            }
            else
            {
                AdminPage.WriteErrMsg("<li>回复指定失败，请返回。</li>");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                this.Path = base.BasePath;
                this.Path = this.Page.Request.Url.Scheme + "://" + this.Page.Request.Url.Authority + this.Path;
                int num = BasePage.RequestInt32("GeneralId");
                int commentId = BasePage.RequestInt32("CommentID");
                string str = BasePage.RequestString("Title");
                if (num == 0)
                {
                    AdminPage.WriteErrMsg("<li>没有找到隶属信息评论，请返回。</li>");
                }
                else
                {
                    CommentInfo commentInfo = Comment.GetCommentInfo(commentId);
                    if (BasePage.RequestInt32("UserId") <= 0)
                    {
                        this.ChkReplyIsPrivate.Enabled = false;
                    }
                    this.LblTitle.Text = str;
                    if (!string.IsNullOrEmpty(commentInfo.UserFace))
                    {
                        this.ImgUserFace.Width = commentInfo.FaceWidth;
                        this.ImgUserFace.Height = commentInfo.FaceHeight;
                        this.ImgUserFace.ImageUrl = DataSecurity.UrlEncode(commentInfo.UserFace);
                    }
                    else
                    {
                        this.ImgUserFace.Width = Unit.Pixel(80);
                        this.ImgUserFace.Height = Unit.Pixel(90);
                        this.ImgUserFace.ImageUrl = this.Path + "Images/Comment/01.gif";
                    }
                    this.LblUserName.Text = commentInfo.UserName;
                    this.LblPassedItems.Text = commentInfo.PassedItems.ToString();
                    this.LblUserExp.Text = commentInfo.UserExp.ToString();
                    this.LblUserRegTime.Text = commentInfo.UserRegTime.ToString("yyyy-MM-dd");
                    this.LblMessage.Text = "<a href='../Accessories/MessageSend.aspx?UserName=" + HttpUtility.UrlEncode(commentInfo.UserName) + "'><img alt='发送短信' src='" + this.Path + "Images/Comment/message.gif' border='0' /></a>";
                    this.LblUserShow.Text = "<a href='../User/UserShow.aspx?UserId=" + commentInfo.UserId.ToString() + "'><img alt='用户信息' src='" + this.Path + "Images/Comment/profile.gif' border='0' /></a>";
                    this.LblEmail.BeginTag = "<a href='mailto:";
                    this.LblEmail.Text = commentInfo.Email;
                    this.LblEmail.EndTag = "'><img alt='邮箱' src='" + this.Path + "Images/Comment/email.gif' border='0' /></a>";
                    this.LblContent.Text = commentInfo.Content;
                    this.LblUpdateDateTime.Text = commentInfo.UpdateDateTime.ToString("yyyy-MM-dd");
                }
                if (!SiteConfig.SiteOption.EnablePointMoneyExp)
                {
                    this.UserExp.Style.Add("display", "none");
                }
            }
        }
    }
}

