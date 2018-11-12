namespace EasyOne.WebSite.Admin.Contents
{
    using EasyOne.Common;
    using EasyOne.Components;
    using EasyOne.Contents;
    using EasyOne.Controls;
    using EasyOne.Model.Contents;
    using EasyOne.Web.UI;
    using EasyOne.WebSite.Controls;
    using System;
    using System.Web;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public partial class CommentExcerpt : AdminPage
    {
        protected string Path = "";

        protected void BtnReset_Click(object sender, EventArgs e)
        {
            BasePage.ResponseRedirect("ContentView.aspx?GeneralId=" + BasePage.RequestInt32("GeneralId").ToString());
        }

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            CommentInfo commentInfo = new CommentInfo();
            commentInfo.CommentTitle = this.TxtCommentTitle.Text.Trim();
            commentInfo.GeneralId = BasePage.RequestInt32("GeneralId");
            commentInfo.TopicId = 1;
            commentInfo.NodeId = 0;
            commentInfo.Content = this.TxtCommentRestore.Text;
            commentInfo.UpdateDateTime = DateTime.Now;
            commentInfo.Score = this.ScoreControl.Score;
            commentInfo.Position = DataConverter.CLng(this.RadlPosition.SelectedValue);
            commentInfo.Status = true;
            commentInfo.IP = PEContext.Current.UserHostAddress;
            if (this.ChkIsPrivate.Checked)
            {
                commentInfo.IsPrivate = true;
            }
            else
            {
                commentInfo.IsPrivate = false;
            }
            if (!string.IsNullOrEmpty(PEContext.Current.Admin.UserName))
            {
                commentInfo.UserName = PEContext.Current.Admin.UserName;
            }
            else
            {
                commentInfo.UserName = "匿名发表";
            }
            commentInfo.Face = "";
            if (Comment.Add(commentInfo))
            {
                AdminPage.WriteSuccessMsg("<li>添加评论成功！</li>", "ContentView.aspx?GeneralId=" + BasePage.RequestInt32("GeneralId").ToString());
            }
            else
            {
                AdminPage.WriteErrMsg("<li>添加评论失败，请返回！</li>");
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
                    CommentInfo extendCommentInfo = Comment.GetExtendCommentInfo(commentId);
                    this.LblTitle.Text = str;
                    if (!string.IsNullOrEmpty(extendCommentInfo.UserFace))
                    {
                        this.LblUserFace.Text = string.Concat(new object[] { "<img alt='' src='", DataSecurity.UrlEncode(extendCommentInfo.UserFace), "' width='", extendCommentInfo.FaceWidth, "' height='", extendCommentInfo.FaceHeight, "' />" });
                    }
                    else
                    {
                        this.LblUserFace.Text = "<img alt='' src='" + this.Path + "Images/Comment/01.gif' width='80' height='90' />";
                    }
                    this.LblUserName.Text = extendCommentInfo.UserName;
                    this.LblPassedItems.Text = extendCommentInfo.PassedItems.ToString();
                    this.LblUserExp.Text = extendCommentInfo.UserExp.ToString();
                    this.LblUserRegTime.Text = extendCommentInfo.UserRegTime.ToString("yyyy-MM-dd");
                    this.LblMessage.Text = "<a href='../Accessories/MessageSend.aspx?UserName=" + HttpUtility.UrlEncode(extendCommentInfo.UserName) + "'><img alt='发送短信' src='" + this.Path + "Images/Comment/message.gif' border='0' /></a>";
                    this.LblUserShow.Text = "<a href='../User/UserShow.aspx?UserId=" + extendCommentInfo.UserId.ToString() + "'><img alt='用户信息' src='" + this.Path + "Images/Comment/profile.gif' border='0' /></a>";
                    this.LblEmail.Text = "<a href='mailto:" + extendCommentInfo.Email + "'><img alt='邮箱' src='" + this.Path + "Images/Comment/email.gif' border='0' /></a>";
                    this.LblContent.Text = extendCommentInfo.Content;
                    this.lblCommentTime.Text = " <img src='" + this.Path + "Images/Comment/ip.gif' alt='评论发布时间' />";
                    this.LblUpdateDateTime.Text = extendCommentInfo.UpdateDateTime.ToString("yyyy-MM-dd");
                    this.TxtCommentTitle.Text = extendCommentInfo.CommentTitle;
                    this.TxtCommentRestore.Text = extendCommentInfo.Content;
                }
                if (!SiteConfig.SiteOption.EnablePointMoneyExp)
                {
                    this.UserExp.Style.Add("display", "none");
                }
            }
        }
    }
}

