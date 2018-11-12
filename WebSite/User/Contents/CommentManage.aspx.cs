namespace EasyOne.WebSite.User.Contents
{
    using EasyOne.Components;
    using EasyOne.Contents;
    using EasyOne.Controls;
    using EasyOne.ExtendedControls;
    using EasyOne.Model.Contents;
    using EasyOne.Web.UI;
    using System;
    using System.Text;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public partial class CommentManage : DynamicPage
    {
        private string commonlink = ("CommentID=" + BasePage.RequestInt32("CommentID").ToString() + "&GeneralID=" + BasePage.RequestInt32("GeneralID").ToString() + "&NodeID=" + BasePage.RequestInt32("NodeID").ToString());
        private int m_PrevId;

        private void CommentBindData()
        {
            this.RptCommentList.DataSource = Comment.GetUserCommentList((this.Pager.CurrentPageIndex - 1) * this.Pager.PageSize, this.Pager.PageSize, BasePage.RequestInt32("NodeID"), PEContext.Current.User.UserName);
            this.Pager.RecordCount = Comment.GetTotalOfCommentInfo();
            this.RptCommentList.DataBind();
        }

        protected void InitComment()
        {
            int commentId = BasePage.RequestInt32("CommentID");
            string str = BasePage.RequestString("Action");
            if (str != null)
            {
                if (!(str == "DelAll"))
                {
                    if (str == "Del")
                    {
                        if (Comment.DeleteByUserName(commentId))
                        {
                            DynamicPage.WriteUserSuccessMsg("<li>删除指定评论成功！</li>", "CommentManage.aspx?" + this.commonlink);
                        }
                        else
                        {
                            DynamicPage.WriteUserErrMsg("<li>删除指定评论失败！</li>");
                        }
                    }
                }
                else if (Comment.DeleteByGeneralIdAndUserName(BasePage.RequestInt32("GeneralID")))
                {
                    DynamicPage.WriteUserSuccessMsg("<li>删除该信息的全部评论成功！</li>", "CommentManage.aspx?" + this.commonlink);
                }
                else
                {
                    DynamicPage.WriteUserErrMsg("<li>删除指定信息的全部评论失败！</li>");
                }
            }
            this.CommentBindData();
            if (this.Pager.RecordCount <= 0)
            {
                this.NotAssignment.Style.Add("display", "");
                this.Pager.Visible = false;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                this.InitComment();
            }
        }

        protected void Pager_PageChanged(object src, PageChangedEventArgs e)
        {
            this.Pager.CurrentPageIndex = e.NewPageIndex;
            this.CommentBindData();
        }

        protected void RptCommentManage_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
            {
                ExtendedLiteral literal = e.Item.FindControl("LblCommentHead") as ExtendedLiteral;
                Label label = e.Item.FindControl("LblStatus") as Label;
                Label label2 = e.Item.FindControl("LblManage") as Label;
                ExtendedLabel label3 = e.Item.FindControl("LblRestore") as ExtendedLabel;
                Label label4 = e.Item.FindControl("LblRestoreBottom") as Label;
                ExtendedLabel label5 = e.Item.FindControl("LblContent") as ExtendedLabel;
                Label label6 = e.Item.FindControl("LblUserRegTime") as Label;
                CommentInfo dataItem = (CommentInfo) e.Item.DataItem;
                string str = "CommentID=" + dataItem.CommentId.ToString() + "&GeneralID=" + dataItem.GeneralId.ToString() + "&NodeID=" + BasePage.RequestInt32("NodeID").ToString();
                if ((this.m_PrevId != dataItem.GeneralId) && (this.m_PrevId != 0))
                {
                    label4.Visible = true;
                    label4.Text = "</table></td></tr></table><br/>";
                }
                if ((this.m_PrevId != dataItem.GeneralId) || (this.m_PrevId == 0))
                {
                    string title = ContentManage.GetCommonModelInfoById(dataItem.GeneralId).Title;
                    StringBuilder builder = new StringBuilder();
                    builder.Append("<table class='border' width='100%' border='0' align='center' cellpadding='0' cellspacing='0'>");
                    builder.Append("<tr class='title'>");
                    builder.Append("<td width='80%' height='22'>");
                    builder.Append("&nbsp;&nbsp;");
                    if (!string.IsNullOrEmpty(title) && (title.Length > 20))
                    {
                        title = title.Substring(0, 20) + "..";
                    }
                    StringBuilder builder2 = new StringBuilder();
                    builder2.Append(" 总评分：" + Comment.ScoreCount(dataItem.GeneralId).ToString() + "</td>");
                    builder2.Append("<td width='20%' align='right'></td>");
                    builder2.Append("</tr><tr><td colspan='2'>");
                    builder2.Append("<table border='0' cellspacing='1' width='100%' cellpadding='0' style='word-break:break-all'>");
                    builder2.Append("<tr class='tdbg'>");
                    builder2.Append("  <td style='width:30px;' align='center'>ID</td>");
                    builder2.Append("  <td  align='center'>评论内容</td>");
                    builder2.Append("  <td style='width:70px;' align='center'>评分</td>");
                    builder2.Append("  <td style='width:120px;' align='center'>发表日期</td>");
                    builder2.Append("  <td style='width:60px;' align='center'>审核状态</td>");
                    builder2.Append("  <td style='width:150px;' align='center'>评论操作</td>");
                    builder2.Append("</tr>");
                    builder2.Append("</td>");
                    builder2.Append("</tr>");
                    literal.Visible = true;
                    literal.BeginTag = builder.ToString();
                    literal.Text = title;
                    literal.EndTag = builder2.ToString();
                }
                this.m_PrevId = dataItem.GeneralId;
                if (dataItem.Content.Length > 20)
                {
                    label5.BeginTag = "<span title='" + dataItem.Content + "'>";
                    label5.Text = dataItem.Content.Substring(0, 20);
                    label5.EndTag = "..</span>";
                }
                else
                {
                    label5.Text = dataItem.Content;
                }
                label6.Text = dataItem.UpdateDateTime.ToString("yyyy年MM月dd日 HH时mm分ss秒");
                if (!dataItem.Status)
                {
                    label.Text = "<span style='color:red'>\x00d7</span>";
                }
                else
                {
                    label.Text = "√";
                }
                if (dataItem.Status)
                {
                    label2.Text = "";
                }
                else
                {
                    StringBuilder builder3 = new StringBuilder();
                    builder3.Append("<table width='100%' border='0' cellpadding='0' cellspacing='0'><tr>");
                    builder3.Append("<td align='center' style='width:30px;'>");
                    builder3.Append("</td>");
                    builder3.Append("<td align='center' style='width:35px;'><a href='CommentModify.aspx?Action=Modify&" + str + "'>修改</a></td>");
                    builder3.Append("<td align='center' style='width:35px;'><a  href='CommentManage.aspx?Action=Del&" + str + "' onclick=\"return confirm('确定要删除此评论吗？');\">删除</a></td>");
                    builder3.Append("<td align='center' style='width:50px;'>");
                    builder3.Append("</td>");
                    builder3.Append("</tr></table>");
                    label2.Text = builder3.ToString();
                }
                if (!string.IsNullOrEmpty(dataItem.Reply))
                {
                    string reply = dataItem.Reply;
                    if (dataItem.Reply.Length > 20)
                    {
                        reply = reply.Substring(0, 20) + "..";
                    }
                    StringBuilder builder4 = new StringBuilder();
                    builder4.Append("<tr class='tdbg' onmouseout=\"this.className='tdbg'\" onmouseover=\"this.className='tdbgmouseover'\">");
                    builder4.Append("<td align='center'> </td>");
                    builder4.Append("<td colspan='2'>                回复：");
                    StringBuilder builder5 = new StringBuilder();
                    builder5.Append("<BR>                </td>");
                    builder5.Append("<td width='200px' align='center'>" + dataItem.ReplyDateTime.ToString("yyyy年MM月dd日 HH时mm分ss秒") + "</td>");
                    builder5.Append("<td width='30px' align='center'> </td>");
                    builder5.Append("<td align='center'>");
                    builder5.Append("<table width='100%' border='0' cellpadding='0' cellspacing='0'><tr><td align='center' style='width:30px;'></td>");
                    builder5.Append("<td align='center' style='width:35px;'></td>");
                    builder5.Append("<td align='center' style='width:35px;'></td>");
                    builder5.Append("<td align='center' style='width:50px;'></td></tr></table></td>");
                    builder5.Append("</tr>");
                    label3.Visible = true;
                    label3.BeginTag = builder4.ToString();
                    label3.Text = reply;
                    label3.EndTag = builder5.ToString();
                }
            }
        }
    }
}

