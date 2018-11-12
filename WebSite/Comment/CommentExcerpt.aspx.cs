namespace EasyOne.WebSite.CommentUI
{
    using EasyOne.Common;
    using EasyOne.Components;
    using EasyOne.Contents;
    using EasyOne.Controls;
    using EasyOne.ExtendedControls;
    using EasyOne.Model.Contents;
    using EasyOne.Model.UserManage;
    using EasyOne.UserManage;
    using EasyOne.Web.UI;
    using EasyOne.WebSite.Controls;
    using System;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public partial class CommentExcerpt : DynamicPage
    {
        private string m_Path;

        protected void BtnReset_Click(object sender, EventArgs e)
        {
            base.Response.Redirect(base.BasePath + "Item/" + BasePage.RequestInt32("ID").ToString() + ".aspx");
        }

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            CommentInfo commentInfo = new CommentInfo();
            commentInfo.GeneralId = BasePage.RequestInt32("ID");
            commentInfo.TopicId = 1;
            commentInfo.NodeId = 0;
            commentInfo.CommentTitle = StringHelper.RemoveXss(this.TxtCommentTitle.Text);
            commentInfo.Content = StringHelper.RemoveXss(this.TxtCommentRestore.Text);
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
            if (!string.IsNullOrEmpty(PEContext.Current.User.UserName))
            {
                commentInfo.UserName = PEContext.Current.User.UserName;
                commentInfo.ReplyUserName = this.TxtUserName.Text;
            }
            else
            {
                commentInfo.UserName = "游客";
                commentInfo.ReplyUserName = this.TxtUserName.Text;
            }
            commentInfo.Face = "";
            if (Comment.Add(commentInfo))
            {
                DynamicPage.WriteSuccessMsg("<li>添加评论成功！</li>", base.BasePath + "Item/" + BasePage.RequestInt32("ID").ToString() + ".aspx");
            }
            else
            {
                DynamicPage.WriteErrMsg("<li>添加评论失败，请返回！</li>");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.m_Path = base.BasePath;
            this.m_Path = this.Page.Request.Url.Scheme + "://" + this.Page.Request.Url.Authority + this.m_Path;
            if (!string.IsNullOrEmpty(PEContext.Current.User.UserName))
            {
                UserInfo usersByUserName = Users.GetUsersByUserName(PEContext.Current.User.UserName);
                this.TxtUserName.Text = usersByUserName.UserName;
                this.TxtEmail.Text = usersByUserName.Email;
                this.TxtUserName.Enabled = false;
                this.TxtEmail.Enabled = false;
            }
            if (!base.IsPostBack)
            {
                int num = BasePage.RequestInt32("ID");
                int commentId = BasePage.RequestInt32("CommentID");
                string str = BasePage.RequestString("Title");
                if (num == 0)
                {
                    DynamicPage.WriteErrMsg("<li>没有找到隶属信息评论，请返回。</li>");
                }
                else
                {
                    CommentInfo extendCommentInfo = Comment.GetExtendCommentInfo(commentId);
                    if (!base.User.Identity.IsAuthenticated)
                    {
                        this.ChkIsPrivate.Enabled = false;
                    }
                    this.LblTitle.Text = str;
                    if (!string.IsNullOrEmpty(extendCommentInfo.UserFace))
                    {
                        this.ImgUserFace.ImageUrl = DataSecurity.UrlEncode(extendCommentInfo.UserFace);
                        this.ImgUserFace.Width = extendCommentInfo.FaceWidth;
                        this.ImgUserFace.Height = extendCommentInfo.FaceHeight;
                    }
                    else
                    {
                        this.ImgUserFace.ImageUrl = this.m_Path + "Images/Comment/01.gif";
                        this.ImgUserFace.Width = Unit.Parse("80");
                        this.ImgUserFace.Height = Unit.Parse("90");
                    }
                    this.LblUserName.Text = extendCommentInfo.UserName;
                    this.LblPassedItems.Text = extendCommentInfo.PassedItems.ToString();
                    this.LblUserExp.Text = extendCommentInfo.UserExp.ToString();
                    this.LblUserRegTime.Text = extendCommentInfo.UserRegTime.ToString("yyyy-MM-dd");
                    if (PEContext.Current.User.Identity.IsAuthenticated)
                    {
                        this.LblMessage.Text = "<a href='" + this.m_Path + "User/Message/Message.aspx?inceptUser=" + base.Server.UrlEncode(extendCommentInfo.UserName) + "'><img alt='发送短信' src='" + this.m_Path + "Images/Comment/message.gif' border='0' /></a>";
                    }
                    this.LblEmail.Text = "<a href='mailto:" + extendCommentInfo.Email + "'><img alt='邮箱' src='" + this.m_Path + "Images/Comment/email.gif' border='0' /></a>";
                    this.LblContent.Text = extendCommentInfo.Content;
                    this.LblUpdateDateTime.Text = extendCommentInfo.UpdateDateTime.ToString("yyyy-MM-dd");
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

