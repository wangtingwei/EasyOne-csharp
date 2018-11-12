namespace EasyOne.WebSite.CommentUI
{
    using EasyOne.Common;
    using EasyOne.Components;
    using EasyOne.Contents;
    using EasyOne.Controls;
    using EasyOne.Model.Contents;
    using EasyOne.Model.UserManage;
    using EasyOne.Web.UI;
    using System;
    using System.Web;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public partial class AddComment : DynamicPage
    {

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            int num = BasePage.RequestInt32("NodeId");
            if (num < 1)
            {
                DynamicPage.WriteErrMsg("<li>请选择隶属栏目！</li>");
            }
            CommentInfo commentInfo = new CommentInfo();
            if (!HttpContext.Current.User.Identity.IsAuthenticated)
            {
                commentInfo.UserName = "游客";
            }
            else
            {
                commentInfo.UserName = PEContext.Current.User.UserName;
            }
            commentInfo.GeneralId = BasePage.RequestInt32("ID");
            commentInfo.TopicId = 1;
            commentInfo.NodeId = num;
            commentInfo.CommentTitle = StringHelper.RemoveXss(this.TxtCommentTitle.Text);
            commentInfo.Content = StringHelper.RemoveXss(this.TxtCommentRestore.Text);
            commentInfo.UpdateDateTime = DateTime.Now;
            commentInfo.Score = DataConverter.CLng(this.RadlScore.SelectedValue);
            commentInfo.Position = DataConverter.CLng(this.RadlPosition.SelectedValue);
            commentInfo.IP = PEContext.Current.UserHostAddress;
            commentInfo.IsPrivate = this.ChkIsPrivate.Checked;
            int num2 = string.Compare(commentInfo.UserName, "游客", StringComparison.CurrentCultureIgnoreCase);
            CommonModelInfo commonModelInfoById = ContentManage.GetCommonModelInfoById(commentInfo.GeneralId);
            if (commonModelInfoById.IsNull)
            {
                DynamicPage.WriteErrMsg("<li>评论不存在，请检查评论是否被删除！</li>");
            }
            NodeInfo cacheNodeById = Nodes.GetCacheNodeById(commonModelInfoById.NodeId);
            if (cacheNodeById.IsNull)
            {
                DynamicPage.WriteErrMsg("<li>栏目不存在，请检查栏目！</li>");
            }
            commentInfo.Status = cacheNodeById.Settings.CommentNeedCheck;
            UserPurviewInfo userPurview = null;
            if (num2 != 0)
            {
                userPurview = PEContext.Current.User.UserInfo.UserPurview;
                if (userPurview.CommentNeedCheck)
                {
                    commentInfo.Status = true;
                }
                else
                {
                    commentInfo.Status = !cacheNodeById.Settings.CommentNeedCheck;
                }
            }
            else if (!cacheNodeById.Settings.EnableTouristsComment)
            {
                DynamicPage.WriteErrMsg("<li>此栏目已禁止游客发表评论！</li>");
            }
            else
            {
                commentInfo.Status = !cacheNodeById.Settings.CommentNeedCheck;
            }
            bool enableComment = false;
            bool commentNeedCheck = false;
            if (userPurview != null)
            {
                enableComment = userPurview.EnableComment;
                commentNeedCheck = userPurview.CommentNeedCheck;
            }
            if (cacheNodeById.Settings.EnableComment || enableComment)
            {
                if (Comment.Add(commentInfo))
                {
                    string returnurl = "../Item/" + BasePage.RequestInt32("ID").ToString() + ".aspx";
                    if (commentInfo.Status || commentNeedCheck)
                    {
                        DynamicPage.WriteSuccessMsg("<li>添加评论成功！</li>", returnurl);
                    }
                    else
                    {
                        DynamicPage.WriteSuccessMsg("<li>发表评论成功，请等待管理员审核。</li>", returnurl);
                    }
                }
                else
                {
                    DynamicPage.WriteErrMsg("<li>添加评论失败，请返回！</li>");
                }
            }
            else
            {
                DynamicPage.WriteErrMsg("<li>此栏目已禁止发表评论！</li>");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                int nodeId = BasePage.RequestInt32("NodeId");
                if (nodeId < 1)
                {
                    DynamicPage.WriteErrMsg("<li>请选择隶属栏目！</li>");
                }
                NodeInfo cacheNodeById = Nodes.GetCacheNodeById(nodeId);
                if (cacheNodeById.IsNull)
                {
                    DynamicPage.WriteErrMsg("<li>栏目不存在，请检查栏目！</li>");
                }
                if (!cacheNodeById.Settings.EnableComment)
                {
                    DynamicPage.WriteErrMsg("<li>该栏目没有发表评论的权限！</li>");
                }
            }
        }
    }
}

