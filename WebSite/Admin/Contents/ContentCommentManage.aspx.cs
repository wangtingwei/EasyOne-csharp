namespace EasyOne.WebSite.Admin.Contents
{
    using EasyOne.Components;
    using EasyOne.Contents;
    using EasyOne.Controls;
    using EasyOne.Model.Contents;
    using EasyOne.Web.UI;
    using System;
    using System.Text;
    using System.Web.UI.WebControls;

    public partial class ContentCommentManage : AdminPage
    {
        public int floorNumber;

        protected void LbtnAllComment_Click(object sender, EventArgs e)
        {
            BasePage.ResponseRedirect("ContentCommentManage.aspx?ListType=0&GeneralID=" + BasePage.RequestInt32("GeneralId").ToString() + "&title=" + base.Server.UrlEncode(BasePage.RequestString("Title")));
        }

        protected void LbtnAuditedComment_Click(object sender, EventArgs e)
        {
            BasePage.ResponseRedirect("ContentCommentManage.aspx?ListType=1&GeneralID=" + BasePage.RequestInt32("GeneralId").ToString() + "&title=" + base.Server.UrlEncode(BasePage.RequestString("Title")));
        }

        protected void LbtnUNAuditedComment_Click(object sender, EventArgs e)
        {
            BasePage.ResponseRedirect("ContentCommentManage.aspx?ListType=2&GeneralID=" + BasePage.RequestInt32("GeneralId").ToString() + "&title=" + base.Server.UrlEncode(BasePage.RequestString("Title")));
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                if (BasePage.RequestInt32("GeneralId") == 0)
                {
                    AdminPage.WriteErrMsg("<li>没有找到隶属信息评论，请返回。</li>");
                }
                else
                {
                    string str = BasePage.RequestString("Title");
                    this.LblTitle.Text = str;
                    int num2 = BasePage.RequestInt32("ListType");
                    string returnurl = "ContentCommentManage.aspx?ListType=" + BasePage.RequestInt32("ListType").ToString() + "&GeneralID=" + BasePage.RequestInt32("GeneralId").ToString() + "&title=" + base.Server.UrlEncode(BasePage.RequestString("Title"));
                    switch (num2)
                    {
                        case 1:
                            this.LbtnAllComment.CssClass = "closebutton";
                            this.LbtnAuditedComment.CssClass = "openbutton";
                            this.LbtnUNAuditedComment.CssClass = "closebutton";
                            break;

                        case 2:
                            this.LbtnAllComment.CssClass = "closebutton";
                            this.LbtnAuditedComment.CssClass = "closebutton";
                            this.LbtnUNAuditedComment.CssClass = "openbutton";
                            break;

                        default:
                            this.LbtnAllComment.CssClass = "openbutton";
                            this.LbtnAuditedComment.CssClass = "closebutton";
                            this.LbtnUNAuditedComment.CssClass = "closebutton";
                            break;
                    }
                    string str3 = BasePage.RequestString("Action");
                    if (str3 != null)
                    {
                        if (!(str3 == "Delete"))
                        {
                            if (!(str3 == "Audited"))
                            {
                                if (!(str3 == "UnAudited"))
                                {
                                    if (!(str3 == "Premier"))
                                    {
                                        if ((str3 == "UnPremier") && Comment.Elite(BasePage.RequestInt32("CommentID"), false))
                                        {
                                            AdminPage.WriteSuccessMsg("<li>已取消指定评论精华！</li>", returnurl);
                                        }
                                        return;
                                    }
                                    if (Comment.Elite(BasePage.RequestInt32("CommentID"), true))
                                    {
                                        AdminPage.WriteSuccessMsg("<li>设定指定评论精华成功！</li>", returnurl);
                                    }
                                    return;
                                }
                                if (Comment.SetStatus(BasePage.RequestInt32("CommentID"), false))
                                {
                                    AdminPage.WriteSuccessMsg("<li>已取消指定评论审核！</li>", returnurl);
                                }
                                return;
                            }
                        }
                        else
                        {
                            if (Comment.Delete(BasePage.RequestInt32("CommentID")))
                            {
                                AdminPage.WriteSuccessMsg("<li>删除指定信息评论成功！</li>", returnurl);
                            }
                            return;
                        }
                        if (Comment.SetStatus(BasePage.RequestInt32("CommentID"), true))
                        {
                            AdminPage.WriteSuccessMsg("<li>指定信息评论审核成功！</li>", returnurl);
                        }
                    }
                }
            }
        }

        protected void RptCommentContent_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
            {
                Label label = e.Item.FindControl("LblNum") as Label;
                if (this.floorNumber == 0)
                {
                    label.Text = "楼主";
                }
                else
                {
                    label.Text = "第<span style='color:Red'>" + this.floorNumber.ToString() + "</span>楼";
                }
                this.floorNumber++;
                Label label2 = e.Item.FindControl("LblUserFace") as Label;
                if (!string.IsNullOrEmpty(((CommentInfo) e.Item.DataItem).UserFace))
                {
                    label2.Text = string.Concat(new object[] { "<img alt='' src='", ((CommentInfo) e.Item.DataItem).UserFace, "' width='", ((CommentInfo) e.Item.DataItem).FaceWidth, "' height='", ((CommentInfo) e.Item.DataItem).FaceHeight, "' />" });
                }
                else
                {
                    label2.Text = "<img alt='' src='../Images/Comment/01.gif' width='80' height='90' />";
                }
                Label label3 = e.Item.FindControl("LblContent") as Label;
                label3.Text = ((CommentInfo) e.Item.DataItem).Content;
                StringBuilder builder = new StringBuilder();
                builder.Append("<table class='Reply' cellspacing='0' cellpadding='6' width='95%' border='0'>");
                builder.Append("<tr>");
                builder.Append("  <td class='ReplyAdminTd' >");
                builder.Append("    <span class='ReplyAdmin'>管理员回复</span>：<br/>");
                builder.Append(((CommentInfo) e.Item.DataItem).Reply);
                builder.Append("<br/>");
                builder.Append("<p align='right'>" + ((CommentInfo) e.Item.DataItem).ReplyDateTime.ToString() + "</span>");
                builder.Append("</td>");
                builder.Append("</tr>");
                builder.Append("</table>");
                if (!((CommentInfo) e.Item.DataItem).ReplyIsPrivate && !string.IsNullOrEmpty(((CommentInfo) e.Item.DataItem).Reply))
                {
                    label3.Text = label3.Text + builder.ToString();
                }
                string str = "ListType=" + BasePage.RequestInt32("ListType").ToString() + "&CommentID=" + ((CommentInfo) e.Item.DataItem).CommentId.ToString() + "&GeneralID=" + BasePage.RequestInt32("GeneralId").ToString() + "&title=" + base.Server.UrlEncode(BasePage.RequestString("Title"));
                Label label4 = e.Item.FindControl("LblAuditing") as Label;
                if (((CommentInfo) e.Item.DataItem).Status)
                {
                    label4.Text = "<span style='color:green'><a href='" + AdminPage.AppendSecurityCode("ContentCommentManage.aspx?Action=UnAudited&" + str) + "'>取消审核</a></span>";
                }
                else
                {
                    label4.Text = "<span style='color:blue'><a href='" + AdminPage.AppendSecurityCode("ContentCommentManage.aspx?Action=Audited&" + str) + "'>通过审核</a></span>";
                }
                Label label5 = e.Item.FindControl("LblIsElite") as Label;
                if (((CommentInfo) e.Item.DataItem).IsElite)
                {
                    label5.Text = "<span style='color:green'><a href='" + AdminPage.AppendSecurityCode("ContentCommentManage.aspx?Action=UnPremier&" + str) + "'>取消精华</a></span>";
                }
                else
                {
                    label5.Text = "<span style='color:blue'><a href='" + AdminPage.AppendSecurityCode("ContentCommentManage.aspx?Action=Premier&" + str) + "'>设置为精华</a></span>";
                }
                if (!SiteConfig.SiteOption.EnablePointMoneyExp)
                {
                    e.Item.FindControl("UserExp").Visible = false;
                }
            }
        }
    }
}

