namespace EasyOne.WebSite.Admin.Contents
{
    using AjaxControlToolkit;
    using EasyOne.Common;
    using EasyOne.Components;
    using EasyOne.Contents;
    using EasyOne.Controls;
    using EasyOne.Model.Contents;
    using EasyOne.Web.UI;
    using System;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public partial class CommentPKZoneManage : AdminPage
    {

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
            int num2 = BasePage.RequestInt32("GeneralId");
            CommentPKZoneInfo commentPKZoneInfo = new CommentPKZoneInfo();
            commentPKZoneInfo.CommentId = num;
            commentPKZoneInfo.Title = base.Request["ItemTitle"];
            commentPKZoneInfo.Content = base.Request["ItemContent"];
            commentPKZoneInfo.Position = DataConverter.CLng(base.Request["RadlPosition"]);
            commentPKZoneInfo.IP = PEContext.Current.UserHostAddress;
            commentPKZoneInfo.UpdateTime = DateTime.Now;
            if (!string.IsNullOrEmpty(PEContext.Current.Admin.UserName))
            {
                commentPKZoneInfo.UserName = PEContext.Current.Admin.UserName;
            }
            else
            {
                commentPKZoneInfo.UserName = "匿名发表";
            }
            CommentPKZone.Add(commentPKZoneInfo);
            AdminPage.WriteSuccessMsg("<li>添加辩论成功！</li>", string.Concat(new object[] { "CommentPKZoneManage.aspx?id=", num2, "&GeneralId=", num2, "&CommentID=", num.ToString(), "&Title=", base.Server.UrlEncode(BasePage.RequestString("Title")) }));
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
            if (!base.IsPostBack)
            {
                int commentId = BasePage.RequestInt32("CommentID");
                string s = BasePage.RequestString("Title");
                int num2 = BasePage.RequestInt32("GeneralId");
                if (commentId == 0)
                {
                    AdminPage.WriteErrMsg("<li>没有找到隶属信息评论，请返回！</li>", string.Concat(new object[] { "CommentPKZoneManage.aspx?id=", num2, "&GeneralId=", num2, "&CommentID=", commentId.ToString(), "&Title=", base.Server.UrlEncode(s) }));
                }
                else
                {
                    string str5;
                    if (((str5 = BasePage.RequestString("Action")) != null) && (str5 == "Delete"))
                    {
                        CommentPKZone.Delete(BasePage.RequestInt32("PKId"));
                        AdminPage.WriteSuccessMsg("<li>删除该信息的辩论成功！</li>", string.Concat(new object[] { "CommentPKZoneManage.aspx?id=", num2, "&GeneralId=", num2, "&CommentID=", commentId.ToString(), "&Title=", base.Server.UrlEncode(s) }));
                    }
                    string str2 = CommentPKZone.GetPKCount(commentId, 1).ToString();
                    string str3 = CommentPKZone.GetPKCount(commentId, -1).ToString();
                    string str4 = CommentPKZone.GetPKCount(commentId, 0).ToString();
                    CommentInfo extendCommentInfo = Comment.GetExtendCommentInfo(commentId);
                    this.LblTitle.Text = s;
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
                    if (!string.IsNullOrEmpty(PEContext.Current.Admin.UserName))
                    {
                        this.LblState.Text = PEContext.Current.Admin.UserName;
                    }
                    else
                    {
                        this.LblState.Text = "匿名发表";
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
                Label lblDelete = e.Item.FindControl("LblAgreeDelete") as Label;
                this.ShowPkZoneInfo(e, lblNetizen, lblNetizenContent, lblNetizenTime, lblNetizenIp, lblDelete);
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
                Label lblDelete = e.Item.FindControl("LblNeutralismDelete") as Label;
                this.ShowPkZoneInfo(e, lblNetizen, lblNetizenContent, lblNetizenTime, lblNetizenIp, lblDelete);
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
                Label lblDelete = e.Item.FindControl("LblOpposeDelete") as Label;
                this.ShowPkZoneInfo(e, lblNetizen, lblNetizenContent, lblNetizenTime, lblNetizenIp, lblDelete);
            }
        }

        private void ShowPkZoneInfo(RepeaterItemEventArgs e, Label LblNetizen, Label LblNetizenContent, Label LblNetizenTime, Label LblNetizenIp, Label LblDelete)
        {
            CommentPKZoneInfo dataItem = (CommentPKZoneInfo) e.Item.DataItem;
            LblNetizen.Text = dataItem.UserName;
            LblNetizenContent.Text = dataItem.Content;
            LblNetizenTime.Text = dataItem.UpdateTime.ToString();
            LblNetizenIp.Text = dataItem.IP;
            if (!string.IsNullOrEmpty(PEContext.Current.Admin.UserName))
            {
                LblDelete.Text = "操作：<a href='" + AdminPage.AppendSecurityCode("CommentPKZoneManage.aspx?Action=Delete&PKId=" + dataItem.PKId.ToString() + "&CommentID=" + BasePage.RequestInt32("CommentID").ToString() + "&Title=" + BasePage.RequestString("Title")) + "' >删除</a>";
            }
        }
    }
}

