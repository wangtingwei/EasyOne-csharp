namespace EasyOne.WebSite.Admin.Contents
{
    using EasyOne.AccessManage;
    using EasyOne.Common;
    using EasyOne.CommonModel;
    using EasyOne.Components;
    using EasyOne.Contents;
    using EasyOne.Controls;
    using EasyOne.Enumerations;
    using EasyOne.ExtendedControls;
    using EasyOne.Model.CommonModel;
    using EasyOne.Model.Contents;
    using EasyOne.Model.WorkFlow;
    using EasyOne.Web.UI;
    using EasyOne.WebSite.Controls;
    using EasyOne.WorkFlows;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Text;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public partial class ContentView : AdminPage
    {
        protected StringBuilder arrTrs0 = new StringBuilder();
        protected StringBuilder arrTrs1 = new StringBuilder();
        private int m_floorNumber;
        private int m_GeneralId;
        private bool m_IsEshop;
        protected string Path = "";
        private DataTable contentDataTable;

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

        protected void EBtnBack_Click(object sender, EventArgs e)
        {
        }

        protected void EBtnCheck_Click(object sender, EventArgs e)
        {
            ExtendedNodeButton button = (ExtendedNodeButton) sender;
            if (button != null)
            {
                int status = DataConverter.CLng(button.CommandArgument);
                if (ContentManage.UpdateStatus(this.m_GeneralId, status))
                {
                    AdminPage.WriteSuccessMsg("<li>审核操作成功！</li>", "ContentManage.aspx?NodeID=" + this.contentDataTable.Rows[0]["NodeId"].ToString());
                }
                else
                {
                    AdminPage.WriteErrMsg("<li>审核操作失败！</li>");
                }
            }
        }

        protected void EBtnCPass_Click(object sender, EventArgs e)
        {
        }

        protected void EBtnDelete_Click(object sender, EventArgs e)
        {
            RolePermissions.BusinessAccessCheck(OperateCode.NodeContentManage, DataConverter.CLng(this.contentDataTable.Rows[0]["NodeId"]));
            if (ContentManage.UpdateStatus(this.m_GeneralId, -3))
            {
                AdminPage.WriteSuccessMsg("<li>删除成功！</li>", "ContentManage.aspx?NodeID=" + this.contentDataTable.Rows[0]["NodeId"].ToString());
            }
            else
            {
                AdminPage.WriteErrMsg("<li>删除失败！</li>");
            }
        }

        protected void EBtnEltiy_Click(object sender, EventArgs e)
        {
        }

        protected void EBtnModify_Click(object sender, EventArgs e)
        {
            RolePermissions.BusinessAccessCheck(OperateCode.NodeContentManage, DataConverter.CLng(this.contentDataTable.Rows[0]["NodeId"]));
            StringBuilder builder = new StringBuilder();
            if (ModelManager.GetModelInfoById(DataConverter.CLng(this.contentDataTable.Rows[0]["ModelID"].ToString())).IsEshop)
            {
                builder.Append("../shop/Product.aspx");
            }
            else
            {
                builder.Append("Content.aspx");
            }
            builder.Append("?Action=Modify&NodeID=");
            builder.Append(this.contentDataTable.Rows[0]["NodeId"].ToString());
            builder.Append("&GeneralID=");
            builder.Append(this.m_GeneralId.ToString());
            builder.Append("&ModelID=");
            builder.Append(this.contentDataTable.Rows[0]["ModelID"].ToString());
            builder.Append("&LinkType=");
            builder.Append(this.contentDataTable.Rows[0]["LinkType"].ToString());
            BasePage.ResponseRedirect(builder.ToString());
        }

        protected void EBtnMove_Click(object sender, EventArgs e)
        {
        }

        protected void EBtnTop_Click(object sender, EventArgs e)
        {
        }

        protected string GetStatusShow(string status)
        {
            int num = DataConverter.CLng(status);
            switch (num)
            {
                case -3:
                    return "回收站中";

                case -2:
                    return "退稿";

                case -1:
                    return "草稿";

                case 0:
                    return "待审核";

                case 0x63:
                    return "终审通过";
            }
            return "审核中";
        }

        protected string GetValue(string col)
        {
            return this.contentDataTable.Rows[0][col].ToString();
        }

        protected void InitComment()
        {
            if (this.m_GeneralId == 0)
            {
                AdminPage.WriteErrMsg("<li>没有找到隶属信息评论，请返回！</li>");
            }
            else
            {
                string s = BasePage.RequestString("Title");
                this.LblTitle.Text = s;
                string returnurl = "ContentView.aspx?GeneralID=" + this.m_GeneralId.ToString() + "&title=" + base.Server.UrlEncode(s);
                int commentId = BasePage.RequestInt32("CommentID");
                switch (BasePage.RequestString("Action"))
                {
                    case "Delete":
                        if (Comment.Delete(commentId))
                        {
                            AdminPage.WriteSuccessMsg("<li>删除指定信息评论成功！</li>", returnurl);
                        }
                        break;

                    case "Audited":
                        if (Comment.SetStatus(commentId, true))
                        {
                            AdminPage.WriteSuccessMsg("<li>指定信息评论审核成功！</li>", returnurl);
                        }
                        break;

                    case "UnAudited":
                        if (Comment.SetStatus(commentId, false))
                        {
                            AdminPage.WriteSuccessMsg("<li>已取消指定评论审核！</li>", returnurl);
                        }
                        break;

                    case "Premier":
                        if (Comment.Elite(commentId, true))
                        {
                            AdminPage.WriteSuccessMsg("<li>设定指定评论精华成功！</li>", returnurl);
                        }
                        break;

                    case "UnPremier":
                        if (Comment.Elite(commentId, false))
                        {
                            AdminPage.WriteSuccessMsg("<li>已取消指定评论精华！</li>", returnurl);
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
                        AdminPage.WriteSuccessMsg("<li>感谢您的参与，添加辩论成功！</li>", returnurl);
                        break;
                    }
                }
                this.CommentBindData();
            }
        }

        protected void InitContent()
        {
            IList<FieldInfo> fieldList = Field.GetFieldList(DataConverter.CLng(this.contentDataTable.Rows[0]["ModelID"].ToString()), false);
            this.RptContent.DataSource = fieldList;
            this.RptContent.DataBind();
        }

        private void InitPage()
        {
            this.Path = this.Page.Request.ApplicationPath.Equals("/") ? string.Empty : this.Page.Request.ApplicationPath;
            this.Path = this.Page.Request.Url.Scheme + "://" + this.Page.Request.Url.Authority + this.Path;
            if (base.Request.UrlReferrer != null)
            {
                this.ViewState["UrlReferrer"] = base.Request.UrlReferrer.ToString();
            }
            this.contentDataTable = ContentManage.GetContentDataById(this.m_GeneralId);
            if ((this.contentDataTable == null) || (this.contentDataTable.Rows.Count == 0))
            {
                AdminPage.WriteErrMsg("<li>指定项目不存在！</li>");
            }
            int nodeId = DataConverter.CLng(this.contentDataTable.Rows[0]["NodeId"]);
            ModelInfo modelInfoById = ModelManager.GetModelInfoById(DataConverter.CLng(this.contentDataTable.Rows[0]["ModelID"].ToString()));
            if (modelInfoById.IsEshop)
            {
                this.m_IsEshop = true;
                this.TabTitle5.Style.Add("display", "none");
            }
            else
            {
                this.m_IsEshop = false;
                if (!modelInfoById.EnableCharge)
                {
                    this.TabTitle5.Style.Add("display", "none");
                }
                if (!modelInfoById.EnableSignIn)
                {
                    this.TabTitle6.Style.Add("display", "none");
                }
                this.InitSigin(this.m_GeneralId, modelInfoById.EnableSignIn);
            }
            if (!this.Page.IsPostBack)
            {
                int nodeWorkFlowId = EasyOne.Contents.Nodes.GetNodeWorkFlowId(nodeId);
                string roles = PEContext.Current.Admin.Roles;
                FlowProcessInfo flowProcessByRoles = FlowProcess.GetFlowProcessByRoles(nodeWorkFlowId, roles);
                int passActionStatus = 0;
                if (PEContext.Current.Admin.IsSuperAdmin || (nodeWorkFlowId == -1))
                {
                    passActionStatus = 0x63;
                }
                else if (!flowProcessByRoles.IsNull)
                {
                    passActionStatus = flowProcessByRoles.PassActionStatus;
                }
                else
                {
                    this.EBtnCheck.Visible = false;
                }
                if ((passActionStatus > 0) && (passActionStatus > DataConverter.CLng(this.contentDataTable.Rows[0]["Status"])))
                {
                    this.EBtnCheck.CommandArgument = passActionStatus.ToString();
                    this.EBtnCheck.Text = "审核通过";
                }
                else
                {
                    this.EBtnCheck.CommandArgument = "0";
                    this.EBtnCheck.Text = "取消审核";
                }
            }
            this.InitContent();
        }

        private void InitSigin(int generalId, bool enableSignin)
        {
            if (enableSignin)
            {
                SignInContentInfo signInContentByGeneralId = SignInContent.GetSignInContentByGeneralId(generalId);
                if (!signInContentByGeneralId.IsNull)
                {
                    this.LblSigninType.Text = BasePage.EnumToHtml<SignInType>(signInContentByGeneralId.SignInType);
                    this.LblEndTime.Text = signInContentByGeneralId.EndTime.ToString();
                    this.LblPriority.Text = signInContentByGeneralId.Priority.ToString();
                    this.LblStatus.Text = BasePage.EnumToHtml<SignInStatus>(signInContentByGeneralId.Status);
                }
                this.RptSigninLog.DataSource = SignInLog.GetList(generalId);
                this.RptSigninLog.DataBind();
            }
        }

        private void InitTabByFieldType(RepeaterItemEventArgs e, FieldInfo fieldInfo)
        {
            HtmlTableRow row = (HtmlTableRow) e.Item.FindControl("Tab");
            if (fieldInfo.FieldLevel == 1)
            {
                row.Style.Add("display", "none");
                if (this.arrTrs1.Length == 0)
                {
                    this.arrTrs1.Append("\"" + row.ClientID + "\"");
                }
                else
                {
                    this.arrTrs1.Append(",\"" + row.ClientID + "\"");
                }
            }
            else if (this.arrTrs0.Length == 0)
            {
                this.arrTrs0.Append("\"" + row.ClientID + "\"");
            }
            else
            {
                this.arrTrs0.Append(",\"" + row.ClientID + "\"");
            }
        }

        protected string IsShow()
        {
            string str = "";
            if (!this.m_IsEshop)
            {
                str = "display:none";
            }
            return str;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.m_GeneralId = BasePage.RequestInt32("GeneralID");
            this.InitPage();
            this.InitComment();
            int nodeId = DataConverter.CLng(this.contentDataTable.Rows[0]["NodeId"]);
            if (!PEContext.Current.Admin.IsSuperAdmin)
            {
                bool flag = false;
                string roleNodeId = RolePermissions.GetRoleNodeId(PEContext.Current.Admin.Roles, OperateCode.NodeContentManage);
                if (nodeId > 0)
                {
                    string findStr = nodeId.ToString();
                    NodeInfo cacheNodeById = EasyOne.Contents.Nodes.GetCacheNodeById(nodeId);
                    if (cacheNodeById.IsNull)
                    {
                        AdminPage.WriteErrMsg("当前栏目不存在，可能被删除了请返回！");
                    }
                    if (!RolePermissions.AccessCheckNodePermission(OperateCode.NodeContentPreview, cacheNodeById.NodeId))
                    {
                        AdminPage.WriteErrMsg("您没有查看该信息的权限！");
                    }
                    if (cacheNodeById.ParentId > 0)
                    {
                        findStr = findStr + "," + cacheNodeById.ParentPath;
                    }
                    flag = StringHelper.FoundCharInArr(roleNodeId, findStr);
                }
                else
                {
                    flag = RolePermissions.AccessCheckNodePermission(OperateCode.NodeContentManage, -1);
                }
                if (!flag)
                {
                    this.EBtnModify.Enabled = false;
                    this.EBtnDelete.Enabled = false;
                }
            }
            CommonModelInfo prevInfo = ContentManage.GetPrevInfo(nodeId, this.m_GeneralId);
            if (!prevInfo.IsNull)
            {
                this.LnkGetPrevInfo.Text = prevInfo.Title;
                this.LnkGetPrevInfo.NavigateUrl = ModelManager.GetModelInfoById(prevInfo.ModelId).PreviewInfoFilePath + "?GeneralID=" + prevInfo.GeneralId.ToString();
            }
            else
            {
                this.LnkGetPrevInfo.Text = "没有了";
            }
            CommonModelInfo nextInfo = ContentManage.GetNextInfo(nodeId, this.m_GeneralId);
            if (!nextInfo.IsNull)
            {
                this.LnkGetNextInfo.Text = nextInfo.Title;
                this.LnkGetNextInfo.NavigateUrl = ModelManager.GetModelInfoById(prevInfo.ModelId).PreviewInfoFilePath + "?GeneralID=" + nextInfo.GeneralId.ToString();
            }
            else
            {
                this.LnkGetNextInfo.Text = "没有了";
            }
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
                Label label2 = e.Item.FindControl("LblUserFace") as Label;
                Label label3 = e.Item.FindControl("LblCommentContent") as Label;
                CommentInfo dataItem = (CommentInfo) e.Item.DataItem;
                Label label4 = e.Item.FindControl("LblSustain") as Label;
                Label label5 = e.Item.FindControl("LblOppose") as Label;
                Label label6 = e.Item.FindControl("LblNeutralismNetizen") as Label;
                Label label7 = e.Item.FindControl("LblPKZone") as Label;
                Label label8 = e.Item.FindControl("LblPKAgree") as Label;
                Label label9 = e.Item.FindControl("LblPKOppose") as Label;
                Label label10 = e.Item.FindControl("LblExcerpt") as Label;
                Label label11 = e.Item.FindControl("LblRestore") as Label;
                Label label12 = e.Item.FindControl("LblDelete") as Label;
                string str = "CommentID=" + dataItem.CommentId.ToString() + "&GeneralId=" + this.m_GeneralId.ToString() + "&Title=" + base.Server.UrlEncode(BasePage.RequestString("Title"));
                this.m_floorNumber++;
                label4.Text = CommentPKZone.GetPKCount(dataItem.CommentId, 1).ToString();
                label5.Text = CommentPKZone.GetPKCount(dataItem.CommentId, -1).ToString();
                label6.Text = CommentPKZone.GetPKCount(dataItem.CommentId, 0).ToString();
                label.Text = "第<span style='color:Red'>" + this.m_floorNumber.ToString() + "</span>楼";
                if (!string.IsNullOrEmpty(dataItem.UserFace))
                {
                    label2.Text = "<img alt='' src='" + DataSecurity.UrlEncode(dataItem.UserFace) + "' width='80px;' />";
                }
                else
                {
                    label2.Text = "<img alt='' src='" + this.Path + "/Images/Comment/01.gif' width='80' height='90' />";
                }
                Label label13 = e.Item.FindControl("LblContent") as Label;
                label13.Text = dataItem.Content;
                StringBuilder builder = new StringBuilder();
                builder.Append("信息：" + dataItem.PassedItems + "<br/>");
                if (SiteConfig.SiteOption.EnablePointMoneyExp)
                {
                    builder.Append("积分：" + dataItem.UserExp + "<br/>");
                }
                builder.Append("时间：" + dataItem.UserRegTime.ToString("yyyy-MM-dd"));
                label3.Text = builder.ToString();
                StringBuilder builder2 = new StringBuilder();
                builder2.Append("<table class='Reply' cellspacing='0' cellpadding='6' width='95%' border='0'>");
                builder2.Append("<tr>");
                builder2.Append("  <td class='ReplyAdminTd' >");
                builder2.Append("    <span class='ReplyAdmin'>管理员回复</span>：<br/>");
                builder2.Append(dataItem.Reply);
                builder2.Append("<br/>");
                builder2.Append("<p align='right'>" + dataItem.ReplyDateTime.ToString() + "</span>");
                builder2.Append("</td>");
                builder2.Append("</tr>");
                builder2.Append("</table>");
                if (!dataItem.ReplyIsPrivate && !string.IsNullOrEmpty(dataItem.Reply))
                {
                    label13.Text = label13.Text + builder2.ToString();
                }
                label7.Text = " <a href='CommentPKZoneManage.aspx?" + str.ToString() + "'> PK Zone</a>";
                label8.Text = " <a href='CommentPKZoneManage.aspx?" + str.ToString() + "' onkeydown=\"return Agree(event);\" onmouseover=\"PopupArea(event, 'Agree" + dataItem.CommentId.ToString() + "')\"   onmouseout = \"jsAreaMouseOut(event)\"> 支持</a>";
                label9.Text = " <a href='CommentPKZoneManage.aspx?" + str.ToString() + "' onkeydown=\"return Oppose(event);\" onmouseover=\"PopupArea(event, 'Oppose" + dataItem.CommentId.ToString() + "')\"  onmouseout = \"jsAreaMouseOut(event)\"> 反对</a>";
                label10.Text = " <a href='CommentExcerpt.aspx?" + str.ToString() + "'> 信息引用</a>";
                label11.Text = " <a href='CommentRestore.aspx?" + str.ToString() + "'> 回复</a>";
                if (!string.IsNullOrEmpty(PEContext.Current.Admin.UserName))
                {
                    label12.Text = "<a href='" + AdminPage.AppendSecurityCode("ContentView.aspx?Action=Delete&" + str) + "' onclick=\"return confirm('确定要删除此评论吗？');\">删除</a>";
                    Label label14 = e.Item.FindControl("LblAuditing") as Label;
                    if (dataItem.Status)
                    {
                        label14.Text = "<span style='color:green'><a href='" + AdminPage.AppendSecurityCode("ContentView.aspx?Action=UnAudited&" + str) + "'>取消审核</a></span>";
                    }
                    else
                    {
                        label14.Text = "<span style='color:blue'><a href='" + AdminPage.AppendSecurityCode("ContentView.aspx?Action=Audited&" + str) + "'>通过审核</a></span>";
                    }
                    Label label15 = e.Item.FindControl("LblIsElite") as Label;
                    if (dataItem.IsElite)
                    {
                        label15.Text = "<span style='color:green'><a href='" + AdminPage.AppendSecurityCode("ContentView.aspx?Action=UnPremier&" + str) + "'>取消精华</a></span>";
                    }
                    else
                    {
                        label15.Text = "<span style='color:blue'><a href='" + AdminPage.AppendSecurityCode("ContentView.aspx?Action=Premier&" + str) + "'>设置为精华</a></span>";
                    }
                }
            }
        }

        protected void RptContent_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if ((e.Item.ItemType == ListItemType.AlternatingItem) || (e.Item.ItemType == ListItemType.Item))
            {
                Literal literal2;
                StringBuilder builder;
                FieldInfo dataItem = e.Item.DataItem as FieldInfo;
                this.InitTabByFieldType(e, dataItem);
                ExtendedLiteral literal = e.Item.FindControl("LitContentText") as ExtendedLiteral;
                Panel panel = e.Item.FindControl("PnlContent") as Panel;
                string input = this.contentDataTable.Rows[0][dataItem.FieldName].ToString();
                switch (dataItem.FieldType)
                {
                    case FieldType.MultipleHtmlTextType:
                        literal.Text = input;
                        return;

                    case FieldType.BoolType:
                        if (DataConverter.CBoolean(input))
                        {
                            literal.Text = "是";
                            return;
                        }
                        literal.Text = "否";
                        return;

                    case FieldType.NodeType:
                        literal.BeginTag = EasyOne.Contents.Nodes.ShowNodeNavigation(DataConverter.CLng(input));
                        return;

                    case FieldType.InfoType:
                    {
                        Literal child = new Literal();
                        StringBuilder builder2 = new StringBuilder();
                        if (this.m_GeneralId > 0)
                        {
                            foreach (CommonModelInfo info4 in ContentManage.GetInfoList(this.m_GeneralId))
                            {
                                builder2.Append("<span>");
                                builder2.Append(EasyOne.Contents.Nodes.ShowNodesAndRootNavigation(info4.NodeId));
                                builder2.Append("<br /></span>");
                            }
                        }
                        if (builder2.Length <= 0)
                        {
                            builder2.Append("<span>无其它节点<br /></span>");
                        }
                        literal.BeginTag = "<div style=\"margin: 0; padding: 0; float: left;\">" + builder2.ToString() + "</div>";
                        panel.Controls.Add(child);
                        return;
                    }
                    case FieldType.SpecialType:
                        literal2 = new Literal();
                        builder = new StringBuilder();
                        if (this.m_GeneralId > 0)
                        {
                            string[] strArray = Special.GetSpecialInfoIds(this.m_GeneralId).Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                            for (int i = 0; i < strArray.Length; i++)
                            {
                                SpecialInfo specialInfoById = Special.GetSpecialInfoById(DataConverter.CLng(strArray[i]));
                                SpecialCategoryInfo specialCategoryInfoById = Special.GetSpecialCategoryInfoById(specialInfoById.SpecialCategoryId);
                                builder.Append("\n<span>");
                                builder.Append(specialCategoryInfoById.SpecialCategoryName + ">>" + specialInfoById.SpecialName);
                                builder.Append("<br /></span>");
                            }
                        }
                        break;

                    case FieldType.StatusType:
                        literal.Text = this.GetStatusShow(input);
                        return;

                    case FieldType.ContentType:
                        literal.BeginTag = "<iframe marginwidth='0' marginheight='0' frameborder='0' name='ContentPreview' width='100%' height='500px' src='ContentPreview.aspx?GeneralID=" + this.m_GeneralId.ToString() + "&fieldName=" + base.Server.UrlEncode(dataItem.FieldName) + "'></iframe>";
                        return;

                    default:
                        literal.Text = input;
                        return;
                }
                if (builder.Length <= 0)
                {
                    builder.Append("<span id='SpecialSpanId0'>无专题<br /></span>");
                }
                literal.BeginTag = "<div style=\"margin: 0; padding: 0; float: left;\">" + builder.ToString() + "</div>";
                panel.Controls.Add(literal2);
            }
        }
    }
}

