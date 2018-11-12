namespace EasyOne.WebSite.Controls
{
    using EasyOne.Common;
    using EasyOne.Components;
    using EasyOne.Contents;
    using EasyOne.Controls;
    using EasyOne.ExtendedControls;
    using EasyOne.Model.Contents;
    using EasyOne.Web.UI;
    using System;
    using System.Text;
    using System.Web.UI.WebControls;

    public partial class CommentManage : BaseUserControl
    {
        private int m_floorNumber;
        private int m_GeneralId;
        protected string m_ViewFile = "ContentView.aspx";
        protected string Path = "";

        protected void BtnAllComment_Click(object sender, EventArgs e)
        {
            this.HdnListType.Value = "0";
            this.Pager.CurrentPageIndex = 1;
            this.CommentBindData();
        }

        protected void BtnAuditedComment_Click(object sender, EventArgs e)
        {
            this.HdnListType.Value = "1";
            this.Pager.CurrentPageIndex = 1;
            this.CommentBindData();
        }

        protected void BtnUNAuditedComment_Click(object sender, EventArgs e)
        {
            this.HdnListType.Value = "2";
            this.Pager.CurrentPageIndex = 1;
            this.CommentBindData();
        }

        private void CommentBindData()
        {
            this.m_floorNumber = (this.Pager.CurrentPageIndex - 1) * this.Pager.PageSize;
            this.RptCommentList.DataSource = Comment.GetList((this.Pager.CurrentPageIndex - 1) * this.Pager.PageSize, this.Pager.PageSize, this.m_GeneralId, DataConverter.CLng(this.HdnListType.Value));
            this.Pager.RecordCount = Comment.GetTotalOfCommentInfo();
            this.RptCommentList.DataBind();
        }

        protected void InitComment()
        {
            if (this.m_GeneralId == 0)
            {
                BaseUserControl.WriteErrMsg("<li>没有找到隶属信息评论，请返回！</li>");
            }
            else
            {
                string s = BaseUserControl.RequestString("Title");
                this.LblTitle.Text = s;
                string returnurl = this.ViewFile + "?GeneralID=" + this.m_GeneralId.ToString() + "&title=" + base.Server.UrlEncode(s);
                int commentId = BaseUserControl.RequestInt32("CommentID");
                switch (BaseUserControl.RequestString("Action"))
                {
                    case "Delete":
                        if (Comment.Delete(commentId))
                        {
                            BaseUserControl.WriteSuccessMsg("<li>删除指定信息评论成功！</li>", returnurl);
                        }
                        break;

                    case "Audited":
                        if (Comment.SetStatus(commentId, true))
                        {
                            BaseUserControl.WriteSuccessMsg("<li>指定信息评论审核成功！</li>", returnurl);
                        }
                        break;

                    case "UnAudited":
                        if (Comment.SetStatus(commentId, false))
                        {
                            BaseUserControl.WriteSuccessMsg("<li>已取消指定评论审核！</li>", returnurl);
                        }
                        break;

                    case "Premier":
                        if (Comment.Elite(commentId, true))
                        {
                            BaseUserControl.WriteSuccessMsg("<li>设定指定评论精华成功！</li>", returnurl);
                        }
                        break;

                    case "UnPremier":
                        if (Comment.Elite(commentId, false))
                        {
                            BaseUserControl.WriteSuccessMsg("<li>已取消指定评论精华！</li>", returnurl);
                        }
                        break;

                    case "AddPKZone":
                    {
                        CommentPKZoneInfo commentPKZoneInfo = new CommentPKZoneInfo();
                        commentPKZoneInfo.CommentId = commentId;
                        commentPKZoneInfo.Content = base.Request["ItemContent"];
                        commentPKZoneInfo.Position = DataConverter.CLng(base.Request["RadlPosition"]);
                        commentPKZoneInfo.IP = PEContext.Current.UserHostAddress;
                        commentPKZoneInfo.UpdateTime = DateTime.Now;
                        commentPKZoneInfo.UserName = "匿名发表";
                        CommentPKZone.Add(commentPKZoneInfo);
                        BaseUserControl.WriteSuccessMsg("<li>感谢您的参与，添加辩论成功！</li>", returnurl);
                        break;
                    }
                }
                this.CommentBindData();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.m_GeneralId = BaseUserControl.RequestInt32("GeneralID");
            this.Path = this.Page.Request.ApplicationPath.Equals("/") ? string.Empty : this.Page.Request.ApplicationPath;
            this.Path = this.Page.Request.Url.Scheme + "://" + this.Page.Request.Url.Authority + this.Path;
            this.InitComment();
        }

        protected void Pager_PageChanged(object src, PageChangedEventArgs e)
        {
            this.Pager.CurrentPageIndex = e.NewPageIndex;
            this.CommentBindData();
        }

        protected void RptCommentContent_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
            {
                Label label = e.Item.FindControl("LblNum") as Label;
                Image image = e.Item.FindControl("ImgUserFace") as Image;
                Label label2 = e.Item.FindControl("LblCommentContent") as Label;
                ExtendedLabel label3 = e.Item.FindControl("LblUserExp") as ExtendedLabel;
                Label label4 = e.Item.FindControl("LblUserRegTime") as Label;
                ExtendedLabel label5 = e.Item.FindControl("LblReply") as ExtendedLabel;
                CommentInfo dataItem = (CommentInfo) e.Item.DataItem;
                Label label6 = e.Item.FindControl("LblSustain") as Label;
                Label label7 = e.Item.FindControl("LblOppose") as Label;
                Label label8 = e.Item.FindControl("LblNeutralismNetizen") as Label;
                Label label9 = e.Item.FindControl("LblPKZone") as Label;
                Label label10 = e.Item.FindControl("LblPKAgree") as Label;
                Label label11 = e.Item.FindControl("LblPKOppose") as Label;
                Label label12 = e.Item.FindControl("LblExcerpt") as Label;
                Label label13 = e.Item.FindControl("LblRestore") as Label;
                Label label14 = e.Item.FindControl("LblDelete") as Label;
                string str = "CommentID=" + dataItem.CommentId.ToString() + "&GeneralId=" + this.m_GeneralId.ToString() + "&Title=" + base.Server.UrlEncode(BaseUserControl.RequestString("Title"));
                this.m_floorNumber++;
                label6.Text = CommentPKZone.GetPKCount(dataItem.CommentId, 1).ToString();
                label7.Text = CommentPKZone.GetPKCount(dataItem.CommentId, -1).ToString();
                label8.Text = CommentPKZone.GetPKCount(dataItem.CommentId, 0).ToString();
                label.Text = "第<span style='color:Red'>" + this.m_floorNumber.ToString() + "</span>楼";
                if (!string.IsNullOrEmpty(dataItem.UserFace))
                {
                    image.Width = Unit.Pixel(80);
                    image.ImageUrl = DataSecurity.UrlEncode(dataItem.UserFace);
                }
                else
                {
                    image.Width = Unit.Pixel(80);
                    image.Height = Unit.Pixel(90);
                    image.ImageUrl = this.Path + "/Images/Comment/01.gif";
                }
                Label label15 = e.Item.FindControl("LblContent") as Label;
                label15.Text = dataItem.Content;
                label2.Text = "信息：" + dataItem.PassedItems;
                if (SiteConfig.SiteOption.EnablePointMoneyExp)
                {
                    label3.Text = "积分：" + dataItem.UserExp;
                    label3.EndTag = "<br/>";
                }
                label4.Text = "时间：" + dataItem.UserRegTime.ToString("yyyy-MM-dd");
                if (!dataItem.ReplyIsPrivate && !string.IsNullOrEmpty(dataItem.Reply))
                {
                    StringBuilder builder = new StringBuilder();
                    builder.Append("<table class='Reply' cellspacing='0' cellpadding='6' width='95%' border='0'>");
                    builder.Append("<tr>");
                    builder.Append("  <td class='ReplyAdminTd' >");
                    builder.Append("    <span class='ReplyAdmin'>管理员回复</span>：<br/>");
                    StringBuilder builder2 = new StringBuilder();
                    builder2.Append("<br/>");
                    builder2.Append("<p align='right'>" + dataItem.ReplyDateTime.ToString() + "</span>");
                    builder2.Append("</td>");
                    builder2.Append("</tr>");
                    builder2.Append("</table>");
                    label5.BeginTag = builder.ToString();
                    label5.Text = dataItem.Reply;
                    label5.EndTag = builder2.ToString();
                }
                label9.Text = " <a href='../Contents/CommentPKZoneManage.aspx?" + str.ToString() + "'> PK Zone</a>";
                label10.Text = " <a href='../Contents/CommentPKZoneManage.aspx?" + str.ToString() + "' onkeydown=\"return Agree(event);\" onmouseover=\"PopupArea(event, 'Agree" + dataItem.CommentId.ToString() + "')\" onmouseout = \"jsAreaMouseOut(event)\"> 支持</a>";
                label11.Text = " <a href='../Contents/CommentPKZoneManage.aspx?" + str.ToString() + "' onkeydown=\"return Oppose(event);\" onmouseover=\"PopupArea(event, 'Oppose" + dataItem.CommentId.ToString() + "')\" onmouseout = \"jsAreaMouseOut(event)\"> 反对</a>";
                label12.Text = " <a href='../Contents/CommentExcerpt.aspx?" + str.ToString() + "'> 信息引用</a>";
                label13.Text = " <a href='../Contents/CommentRestore.aspx?" + str.ToString() + "'> 回复</a>";
                if (!string.IsNullOrEmpty(PEContext.Current.Admin.UserName))
                {
                    label14.Text = "<a href='" + this.ViewFile + "?Action=Delete&" + str + "' onclick=\"return confirm('确定要删除此评论吗？');\">删除</a>";
                    Label label16 = e.Item.FindControl("LblAuditing") as Label;
                    if (dataItem.Status)
                    {
                        label16.Text = "<span style='color:green'><a href='" + BaseUserControl.AppendSecurityCode(this.ViewFile + "?Action=UnAudited&" + str) + "'>取消审核</a></span>";
                    }
                    else
                    {
                        label16.Text = "<span style='color:blue'><a href='" + BaseUserControl.AppendSecurityCode(this.ViewFile + "?Action=Audited&" + str) + "'>通过审核</a></span>";
                    }
                    Label label17 = e.Item.FindControl("LblIsElite") as Label;
                    if (dataItem.IsElite)
                    {
                        label17.Text = "<span style='color:green'><a href='" + BaseUserControl.AppendSecurityCode(this.ViewFile + "?Action=UnPremier&" + str) + "'>取消精华</a></span>";
                    }
                    else
                    {
                        label17.Text = "<span style='color:blue'><a href='" + BaseUserControl.AppendSecurityCode(this.ViewFile + "?Action=Premier&" + str) + "'>设置为精华</a></span>";
                    }
                }
            }
        }

        public string ViewFile
        {
            get
            {
                if (string.IsNullOrEmpty(this.m_ViewFile))
                {
                    return "ContentView.aspx";
                }
                return this.m_ViewFile;
            }
            set
            {
                this.m_ViewFile = value;
            }
        }
    }
}

