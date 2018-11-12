namespace EasyOne.WebSite.CommentUI
{
    using AjaxControlToolkit;
    using EasyOne.Common;
    using EasyOne.Components;
    using EasyOne.Contents;
    using EasyOne.Controls;
    using EasyOne.ExtendedControls;
    using EasyOne.Model.Contents;
    using EasyOne.Web.UI;
    using System;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public partial class CommentPKZoneManage : DynamicPage
    {
        protected string Path = "";

        protected void AgreePager_PageChanged(object src, PageChangedEventArgs e)
        {
            this.AgreePager.CurrentPageIndex = e.NewPageIndex;
            this.AgreePagerBindData();
        }

        private void AgreePagerBindData()
        {
            this.RptAgreeNetizen.DataSource = CommentPKZone.GetList((this.AgreePager.CurrentPageIndex - 1) * this.AgreePager.PageSize, this.AgreePager.PageSize, BasePage.RequestInt32("CommentID"), 1);
            this.AgreePager.RecordCount = CommentPKZone.GetTotalOfCommentPKZoneInfo(BasePage.RequestInt32("CommentID"), 1);
            this.RptAgreeNetizen.DataBind();
        }

        protected void NeutralismPager_PageChanged(object src, PageChangedEventArgs e)
        {
            this.NeutralismPager.CurrentPageIndex = e.NewPageIndex;
            this.NeutralismPagerBindData();
        }

        private void NeutralismPagerBindData()
        {
            this.RptNeutralismNetizen.DataSource = CommentPKZone.GetList((this.NeutralismPager.CurrentPageIndex - 1) * this.NeutralismPager.PageSize, this.NeutralismPager.PageSize, BasePage.RequestInt32("CommentID"), 0);
            this.NeutralismPager.RecordCount = CommentPKZone.GetTotalOfCommentPKZoneInfo(BasePage.RequestInt32("CommentID"), 0);
            this.RptNeutralismNetizen.DataBind();
        }

        protected void OkButton_Click(object sender, EventArgs e)
        {
            int num = BasePage.RequestInt32("CommentID");
            int num2 = BasePage.RequestInt32("ID");
            CommentPKZoneInfo commentPKZoneInfo = new CommentPKZoneInfo();
            commentPKZoneInfo.CommentId = num;
            commentPKZoneInfo.Title = base.Request["ItemTitle"];
            commentPKZoneInfo.Content = base.Request["ItemContent"];
            commentPKZoneInfo.Position = DataConverter.CLng(base.Request["RadlPosition"]);
            commentPKZoneInfo.IP = PEContext.Current.UserHostAddress;
            commentPKZoneInfo.UpdateTime = DateTime.Now;
            if (!string.IsNullOrEmpty(PEContext.Current.User.UserName))
            {
                commentPKZoneInfo.UserName = PEContext.Current.User.UserName;
            }
            else
            {
                commentPKZoneInfo.UserName = "匿名发表";
            }
            CommentPKZone.Add(commentPKZoneInfo);
            DynamicPage.WriteSuccessMsg("<li>添加辩论成功！</li>", string.Concat(new object[] { "CommentPKZoneManage.aspx?Id=", num2, "&CommentID=", num.ToString(), "&Title=", base.Server.UrlEncode(base.Request["Title"]) }));
        }

        protected void OpposePager_PageChanged(object src, PageChangedEventArgs e)
        {
            this.OpposePager.CurrentPageIndex = e.NewPageIndex;
            this.OpposePagerBindData();
        }

        private void OpposePagerBindData()
        {
            this.RptOpposeNetizen.DataSource = CommentPKZone.GetList((this.OpposePager.CurrentPageIndex - 1) * this.OpposePager.PageSize, this.OpposePager.PageSize, BasePage.RequestInt32("CommentID"), -1);
            this.OpposePager.RecordCount = CommentPKZone.GetTotalOfCommentPKZoneInfo(BasePage.RequestInt32("CommentID"), -1);
            this.RptOpposeNetizen.DataBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            string str5;
            this.Path = base.BasePath;
            this.Path = this.Page.Request.Url.Scheme + "://" + this.Page.Request.Url.Authority + this.Path;
            int commentId = BasePage.RequestInt32("CommentID");
            int num2 = BasePage.RequestInt32("ID");
            string s = base.Request["Title"];
            if (((str5 = BasePage.RequestString("Action")) != null) && (str5 == "AddPKZone"))
            {
                CommentPKZoneInfo commentPKZoneInfo = new CommentPKZoneInfo();
                commentPKZoneInfo.CommentId = commentId;
                commentPKZoneInfo.Content = base.Request["ItemContent"];
                commentPKZoneInfo.Position = DataConverter.CLng(base.Request["RadlPosition"]);
                commentPKZoneInfo.IP = PEContext.Current.UserHostAddress;
                commentPKZoneInfo.UpdateTime = DateTime.Now;
                commentPKZoneInfo.UserName = "匿名发表";
                CommentPKZone.Add(commentPKZoneInfo);
                DynamicPage.WriteSuccessMsg("<li>感谢您的参与，添加辩论成功！</li>", string.Concat(new object[] { "CommentPKZoneManage.aspx?Id=", num2, "&CommentID=", commentId.ToString(), "&Title=", base.Server.UrlEncode(s) }));
            }
            if (!base.IsPostBack)
            {
                if (commentId == 0)
                {
                    DynamicPage.WriteErrMsg("<li>没有找到隶属信息评论，请返回！</li>", string.Concat(new object[] { "CommentPKZoneManage.aspx?Id=", num2, "&CommentID=", commentId.ToString(), "&Title=", base.Server.UrlEncode(s) }));
                }
                else
                {
                    CommentInfo extendCommentInfo = Comment.GetExtendCommentInfo(commentId);
                    if (extendCommentInfo.IsNull)
                    {
                        DynamicPage.WriteErrMsg("<li>没有找到隶属信息评论，请返回！</li>", string.Concat(new object[] { "CommentPKZoneManage.aspx?Id=", num2, "&CommentID=", commentId.ToString(), "&Title=", base.Server.UrlEncode(s) }));
                    }
                    else
                    {
                        string str2 = CommentPKZone.GetPKCount(commentId, 1).ToString();
                        string str3 = CommentPKZone.GetPKCount(commentId, -1).ToString();
                        string str4 = CommentPKZone.GetPKCount(commentId, 0).ToString();
                        this.LblTitle.Text = "<a href='" + base.BasePath + "Item/" + BasePage.RequestInt32("ID").ToString() + ".aspx'>" + s + "</a>";
                        this.LblUserName.Text = extendCommentInfo.UserName;
                        this.LblContent.Text = extendCommentInfo.Content;
                        this.LblTime.Text = extendCommentInfo.UpdateDateTime.ToString();
                        this.LblSustain.Text = str2;
                        this.LblOppose.Text = str3;
                        this.LblSustain2.Text = str2;
                        this.LblOppose2.Text = str3;
                        this.LblNeutralismNetizen.Text = str4;
                        this.AgreePagerBindData();
                        this.OpposePagerBindData();
                        this.NeutralismPagerBindData();
                        this.LblItemTitle.Text = s;
                        if (extendCommentInfo.Content.Length > 30)
                        {
                            this.LblItemContent.Text = extendCommentInfo.Content.Substring(0, 30) + "..";
                        }
                        else
                        {
                            this.LblItemContent.Text = extendCommentInfo.Content;
                        }
                        if (!string.IsNullOrEmpty(PEContext.Current.User.UserName))
                        {
                            this.LblState.Text = PEContext.Current.User.UserName;
                        }
                        else
                        {
                            this.LblState.Text = "匿名发表";
                        }
                    }
                }
            }
        }

        protected void RptAgreeNetizen_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
            {
                Label lblNetizen = e.Item.FindControl("LblAgreeNetizen") as Label;
                Label lblNetizenContent = e.Item.FindControl("LblAgreeNetizenContent") as Label;
                Label lblNetizenTime = e.Item.FindControl("LblAgreeNetizenTime") as Label;
                Label lblNetizenIp = e.Item.FindControl("LblAgreeNetizenIp") as Label;
                this.ShowPkZoneInfo(e, lblNetizen, lblNetizenContent, lblNetizenTime, lblNetizenIp);
            }
        }

        protected void RptNeutralismNetizen_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
            {
                Label lblNetizen = e.Item.FindControl("LblNeutralismNetizen") as Label;
                Label lblNetizenContent = e.Item.FindControl("LblNeutralismNetizenContent") as Label;
                Label lblNetizenTime = e.Item.FindControl("LblNeutralismNetizenTime") as Label;
                Label lblNetizenIp = e.Item.FindControl("LblNeutralismNetizenIp") as Label;
                this.ShowPkZoneInfo(e, lblNetizen, lblNetizenContent, lblNetizenTime, lblNetizenIp);
            }
        }

        protected void RptOpposeNetizen_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
            {
                Label lblNetizen = e.Item.FindControl("LblOpposeNetizen") as Label;
                Label lblNetizenContent = e.Item.FindControl("LblOpposeNetizenContent") as Label;
                Label lblNetizenTime = e.Item.FindControl("LblOpposeNetizenTime") as Label;
                Label lblNetizenIp = e.Item.FindControl("LblOpposeNetizenIp") as Label;
                this.ShowPkZoneInfo(e, lblNetizen, lblNetizenContent, lblNetizenTime, lblNetizenIp);
            }
        }

        private void ShowPkZoneInfo(RepeaterItemEventArgs e, Label LblNetizen, Label LblNetizenContent, Label LblNetizenTime, Label LblNetizenIp)
        {
            CommentPKZoneInfo dataItem = (CommentPKZoneInfo) e.Item.DataItem;
            LblNetizen.Text = dataItem.UserName;
            LblNetizenContent.Text = dataItem.Content;
            LblNetizenTime.Text = dataItem.UpdateTime.ToString();
            LblNetizenIp.Text = dataItem.IP;
        }
    }
}

