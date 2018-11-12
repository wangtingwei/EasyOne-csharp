namespace EasyOne.WebSite.Admin.Contents
{
    using EasyOne.AccessManage;
    using EasyOne.Common;
    using EasyOne.Components;
    using EasyOne.Contents;
    using EasyOne.Controls;
    using EasyOne.Enumerations;
    using EasyOne.ExtendedControls;
    using EasyOne.Model.Contents;
    using EasyOne.Web.UI;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Web;
    using System.Web.UI.WebControls;

    public partial class CommentManage : AdminPage
    {
        private string commonlink = ("CommentID=" + BasePage.RequestInt32("CommentID").ToString() + "&GeneralID=" + BasePage.RequestInt32("GeneralID").ToString() + "&NodeID=" + BasePage.RequestInt32("NodeID").ToString());
        private bool m_Administrator;
        private string m_NodeCommentCheck = "";
        private string m_NodeCommentManage = "";
        private string m_NodeCommentReply = "";
        private int m_PrevId;

        protected void BtnAllComment_Click(object sender, EventArgs e)
        {
            this.HdnSearchType.Value = "0";
            this.CommentBindData();
        }

        protected void BtnAuditedComment_Click(object sender, EventArgs e)
        {
            this.HdnSearchType.Value = "1";
            this.CommentBindData();
        }

        protected void BtnSubmit1_Click(object sender, EventArgs e)
        {
            if (Comment.Delete(base.Request.Form["CommentID"]))
            {
                AdminPage.WriteSuccessMsg("<li>删除指定评论成功！</li>", "CommentManage.aspx?" + this.commonlink);
            }
            else
            {
                AdminPage.WriteSuccessMsg("<li>删除指定评论失败！</li>");
            }
        }

        protected void BtnSubmit2_Click(object sender, EventArgs e)
        {
            if (Comment.SetStatus(base.Request.Form["CommentID"], true))
            {
                AdminPage.WriteSuccessMsg("<li>指定信息评论审核成功！</li>", "CommentManage.aspx?" + this.commonlink);
            }
        }

        protected void BtnSubmit3_Click(object sender, EventArgs e)
        {
            if (Comment.SetStatus(base.Request.Form["CommentID"], false))
            {
                AdminPage.WriteSuccessMsg("<li>已取消指定评论审核！</li>", "CommentManage.aspx?" + this.commonlink);
            }
        }

        protected void BtnUNAuditedComment_Click(object sender, EventArgs e)
        {
            this.HdnSearchType.Value = "2";
            this.CommentBindData();
        }

        private string CheckCommentPermissions(string nodeIds, string rolesNodeIds, string href)
        {
            string str = href;
            if (!this.m_Administrator && !StringHelper.FoundCharInArr(nodeIds, rolesNodeIds))
            {
                str = " disabled='true'";
            }
            return str;
        }

        private void CommentBindData()
        {
            this.RptCommentList.DataSource = Comment.GetListByNodeId((this.Pager.CurrentPageIndex - 1) * this.Pager.PageSize, this.Pager.PageSize, BasePage.RequestInt32("NodeID"), DataConverter.CLng(this.HdnSearchType.Value));
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
                        if (Comment.Delete(commentId))
                        {
                            AdminPage.WriteSuccessMsg("<li>删除指定评论成功！</li>", "CommentManage.aspx?" + this.commonlink);
                        }
                        else
                        {
                            AdminPage.WriteSuccessMsg("<li>删除指定评论失败！</li>");
                        }
                    }
                    else if (str == "CancelPassed")
                    {
                        if (Comment.SetStatus(commentId, false))
                        {
                            AdminPage.WriteSuccessMsg("<li>已取消指定评论审核！</li>", "CommentManage.aspx?" + this.commonlink);
                        }
                    }
                    else if (str == "SetPassed")
                    {
                        if (Comment.SetStatus(commentId, true))
                        {
                            AdminPage.WriteSuccessMsg("<li>指定信息评论审核成功！</li>", "CommentManage.aspx?" + this.commonlink);
                        }
                    }
                    else if (str == "DelReply")
                    {
                        CommentInfo commentInfo = Comment.GetCommentInfo(BasePage.RequestInt32("CommentID"));
                        commentInfo.Reply = "";
                        if (Comment.Update(commentInfo))
                        {
                            AdminPage.WriteSuccessMsg("<li>删除管理员回复成功！</li>", "CommentManage.aspx?" + this.commonlink);
                        }
                    }
                }
                else if (Comment.DeleteByGeneralId(BasePage.RequestInt32("GeneralID")))
                {
                    AdminPage.WriteSuccessMsg("<li>删除该信息的全部评论成功！</li>", "CommentManage.aspx?" + this.commonlink);
                }
                else
                {
                    AdminPage.WriteSuccessMsg("<li>删除指定信息的全部评论失败！</li>");
                }
            }
            this.SmpNavigator.CurrentNode = ShowNodeNavigation(BasePage.RequestInt32("NodeID"));
            if (DataConverter.CBoolean(BasePage.RequestString("Enquiries")))
            {
                this.HdnSearchType.Value = BasePage.RequestInt32("SearchType").ToString();
            }
            this.CommentBindData();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (PEContext.Current.Admin.IsSuperAdmin)
            {
                this.m_Administrator = true;
            }
            if (!this.m_Administrator)
            {
                this.m_NodeCommentReply = RolePermissions.GetRoleNodeId(PEContext.Current.Admin.Roles, OperateCode.NodeCommentReply);
                this.m_NodeCommentCheck = RolePermissions.GetRoleNodeId(PEContext.Current.Admin.Roles, OperateCode.NodeCommentCheck);
                this.m_NodeCommentManage = RolePermissions.GetRoleNodeId(PEContext.Current.Admin.Roles, OperateCode.NodeCommentManage);
                bool flag = false;
                int nodeId = BasePage.RequestInt32("NodeID");
                if (nodeId > 0)
                {
                    string findStr = nodeId.ToString();
                    NodeInfo cacheNodeById = Nodes.GetCacheNodeById(nodeId);
                    if (cacheNodeById.IsNull)
                    {
                        AdminPage.WriteErrMsg("当前栏目不存在，可能被删除了请返回！");
                    }
                    if (cacheNodeById.ParentId > 0)
                    {
                        findStr = findStr + "," + cacheNodeById.ParentPath;
                    }
                    if (StringHelper.FoundCharInArr(this.m_NodeCommentManage, findStr))
                    {
                        flag = true;
                    }
                }
                else
                {
                    flag = RolePermissions.AccessCheckNodePermission(OperateCode.NodeCommentManage, -1);
                }
                if (!flag)
                {
                    this.BtnSubmit1.Enabled = false;
                    this.BtnSubmit2.Enabled = false;
                    this.BtnSubmit3.Enabled = false;
                }
            }
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
                ExtendedLiteral literal2 = e.Item.FindControl("LblRestore") as ExtendedLiteral;
                Label label3 = e.Item.FindControl("LblRestoreBottom") as Label;
                ExtendedLiteral literal3 = e.Item.FindControl("LblContent") as ExtendedLiteral;
                ExtendedLiteral literal4 = e.Item.FindControl("LblContentTitle") as ExtendedLiteral;
                ExtendedLiteral literal5 = e.Item.FindControl("LblRestoreTitle") as ExtendedLiteral;
                CommentInfo dataItem = (CommentInfo) e.Item.DataItem;
                string nodeIds = dataItem.NodeId.ToString();
                if (!this.m_Administrator)
                {
                    NodeInfo cacheNodeById = Nodes.GetCacheNodeById(dataItem.NodeId);
                    if (cacheNodeById.ParentId > 0)
                    {
                        nodeIds = cacheNodeById.ParentPath + "," + nodeIds;
                    }
                }
                string title = ContentManage.GetCommonModelInfoById(dataItem.GeneralId).Title;
                string str3 = "CommentID=" + dataItem.CommentId.ToString() + "&GeneralID=" + dataItem.GeneralId.ToString() + "&NodeID=" + BasePage.RequestInt32("NodeID").ToString() + "&Title=" + base.Server.UrlEncode(title) + "&UserId=" + dataItem.UserId.ToString();
                if ((this.m_PrevId != dataItem.GeneralId) && (this.m_PrevId != 0))
                {
                    label3.Visible = true;
                    label3.Text = "</table></td></tr></table><br/>";
                }
                if ((this.m_PrevId != dataItem.GeneralId) || (this.m_PrevId == 0))
                {
                    StringBuilder builder = new StringBuilder();
                    builder.Append("<table class='border' width='100%' border='0' align='center' cellpadding='0' cellspacing='0'>");
                    builder.Append("<tr class='title'>");
                    builder.Append("<td width='80%' height='22'>");
                    builder.Append("&nbsp;&nbsp;<a " + this.CheckCommentPermissions(nodeIds, this.m_NodeCommentManage, "href='ContentView.aspx?GeneralID=" + dataItem.GeneralId.ToString() + "'") + " >");
                    StringBuilder builder2 = new StringBuilder();
                    builder2.Append("</a> 总评分：" + Comment.ScoreCount(dataItem.GeneralId).ToString() + "</td>");
                    builder2.Append("<td width='20%' align='right'>");
                    builder2.Append("<a " + this.CheckCommentPermissions(nodeIds, this.m_NodeCommentManage, "href='CommentManage.aspx?Action=DelAll&" + str3 + "'  onclick=\"return confirm('确定要删除此项目下的所有评论吗？');\"") + ">删除此项目下的所有评论</a>&nbsp;&nbsp;</td>");
                    builder2.Append("</tr><tr><td colspan='2'>");
                    builder2.Append("<table border='0' cellspacing='1' width='100%' cellpadding='0' style='word-break:break-all'>");
                    builder2.Append("</td>");
                    builder2.Append("</tr>");
                    literal.Visible = true;
                    literal.BeginTag = builder.ToString();
                    literal.Text = title;
                    literal.EndTag = builder2.ToString();
                }
                this.m_PrevId = dataItem.GeneralId;
                StringBuilder builder3 = new StringBuilder();
                string replyUserName = "";
                if (dataItem.UserName == "游客")
                {
                    builder3.Append("[游客]&nbsp;");
                    replyUserName = dataItem.ReplyUserName;
                }
                else
                {
                    builder3.Append("[会员]&nbsp;");
                    replyUserName = dataItem.UserName;
                }
                StringBuilder builder4 = new StringBuilder();
                builder4.Append("&nbsp;于&nbsp;" + dataItem.UpdateDateTime.ToString("yyyy年MM月dd日 HH时mm分ss秒") + "&nbsp;发表如下评论内容，同时评分：" + dataItem.Score.ToString() + "分");
                builder4.Append("<br/>");
                literal4.BeginTag = builder3.ToString();
                literal4.Text = replyUserName;
                literal4.EndTag = builder4.ToString();
                literal3.BeginTag = "&nbsp;&nbsp;<span >";
                if (dataItem.Content.Length > 120)
                {
                    literal3.Text = dataItem.Content.Substring(0, 120) + "...";
                }
                else
                {
                    literal3.Text = dataItem.Content;
                }
                literal3.EndTag = "</span>";
                if (!dataItem.Status)
                {
                    label.Text = "<span style='color:red'>\x00d7</span>";
                }
                else
                {
                    label.Text = "√";
                }
                StringBuilder builder5 = new StringBuilder();
                builder5.Append("<table width='100%' border='0' cellpadding='0' cellspacing='0'><tr>");
                builder5.Append("<td align='center' style='width:30px;'>");
                if (string.IsNullOrEmpty(dataItem.Reply))
                {
                    builder5.Append("<a " + this.CheckCommentPermissions(nodeIds, this.m_NodeCommentReply, "href='CommentRestore.aspx?" + str3 + "'") + ">回复</a>");
                }
                builder5.Append("</td>");
                builder5.Append("<td align='center' style='width:35px;'><a " + this.CheckCommentPermissions(nodeIds, this.m_NodeCommentManage, "href='CommentModify.aspx?Action=Modify&" + str3 + "'") + " >修改</a></td>");
                builder5.Append("<td align='center' style='width:35px;'><a " + this.CheckCommentPermissions(nodeIds, this.m_NodeCommentManage, "href='CommentManage.aspx?Action=Del&" + str3 + "' onclick=\"return confirm('确定要删除此评论吗？');\"") + " >删除</a></td>");
                builder5.Append("<td align='center' style='width:50px;'>");
                if (!dataItem.Status)
                {
                    builder5.Append("<a " + this.CheckCommentPermissions(nodeIds, this.m_NodeCommentCheck, "href='CommentManage.aspx?Action=SetPassed&" + str3 + "'") + "  onclick=\"if(!confirm('确定要通过此评论吗？')){return false;}\" >通过审核</a>");
                }
                else
                {
                    builder5.Append("<a " + this.CheckCommentPermissions(nodeIds, this.m_NodeCommentCheck, "href='CommentManage.aspx?Action=CancelPassed&" + str3 + "'") + " onclick=\"if(!confirm('确定要取消此评论吗？')){return false;}\" >取消审核</a>");
                }
                builder5.Append("</td>");
                builder5.Append("</tr></table>");
                label2.Text = builder5.ToString();
                if (!string.IsNullOrEmpty(dataItem.Reply))
                {
                    string reply = dataItem.Reply;
                    if (dataItem.Reply.Length > 20)
                    {
                        reply = reply.Substring(0, 20) + "..";
                    }
                    StringBuilder builder6 = new StringBuilder();
                    builder6.Append("<tr class='tdbg' onmouseout=\"this.className='tdbg'\" onmouseover=\"this.className='tdbgmouseover'\">");
                    builder6.Append("<td align='center'> </td>");
                    builder6.Append(" <td colspan='2' align='left'>[管理员] ");
                    literal5.BeginTag = builder6.ToString();
                    literal5.Text = dataItem.ReplyAdmin;
                    literal5.EndTag = " 于 " + dataItem.ReplyDateTime.ToString("yyyy年MM月dd日 HH时mm分ss秒");
                    literal2.BeginTag = " 回复：<br/>";
                    literal2.Text = dataItem.Reply;
                    StringBuilder builder7 = new StringBuilder();
                    builder7.Append("<td align='center'>");
                    builder7.Append("<table width='100%' border='0' cellpadding='0' cellspacing='0'><tr><td align='center' style='width:30px;'></td>");
                    builder7.Append("<td align='center' style='width:35px;'><a " + this.CheckCommentPermissions(nodeIds, this.m_NodeCommentManage, "href='CommentReplyModify.aspx?Action=Reply&" + str3 + "'") + " >修改</a></td>");
                    builder7.Append("<td align='center' style='width:35px;'><a " + this.CheckCommentPermissions(nodeIds, this.m_NodeCommentManage, "href='CommentManage.aspx?Action=DelReply&" + str3 + "'  onclick=\"return confirm('确定要删除此评论的管理员回复吗？');\" ") + " >删除</a></td>");
                    builder7.Append("<td align='center' style='width:50px;'></td></tr></table></td>");
                    builder7.Append("</tr>");
                    literal2.EndTag = builder7.ToString();
                    literal2.Visible = true;
                    literal5.Visible = true;
                }
            }
        }

        private static string ShowNodeNavigation(int nodeId)
        {
            NodeInfo cacheNodeById = Nodes.GetCacheNodeById(nodeId);
            if (cacheNodeById.IsNull)
            {
                return string.Empty;
            }
            IList<NodeInfo> nodesListInParentPath = Nodes.GetNodesListInParentPath(cacheNodeById.ParentPath);
            StringBuilder builder = new StringBuilder();
            builder.Append("评论管理");
            if (nodesListInParentPath.Count > 0)
            {
                foreach (NodeInfo info2 in nodesListInParentPath)
                {
                    builder.Append(" >> ");
                    builder.Append(string.Concat(new object[] { "<a href='CommentManage.aspx?NodeID=", info2.NodeId, "&NodeName=", HttpContext.Current.Server.UrlEncode(info2.NodeName), "'>" }));
                    builder.Append(info2.NodeName);
                    builder.Append("</a>");
                }
            }
            if (builder.Length > 0)
            {
                builder.Append(" >> ");
            }
            builder.Append(cacheNodeById.NodeName);
            return builder.ToString();
        }
    }
}

