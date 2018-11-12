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

    public partial class ArticlePreview : AdminPage
    {
        protected StringBuilder arrTrs0 = new StringBuilder();
        protected DataTable contentDataTable;
        private int m_GeneralId;

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
            return this.contentDataTable.Rows[0][col].ToString().Trim();
        }

        protected void InitContent()
        {
            IList<FieldInfo> fieldList = Field.GetFieldList(DataConverter.CLng(this.contentDataTable.Rows[0]["ModelID"].ToString()), false);
            this.RptContent.DataSource = fieldList;
            this.RptContent.DataBind();
            this.LtrTitle.Text = this.GetValue("Title");
            this.LtrAuthor.Text = this.GetValue("Author");
            this.LtrCopyFrom.Text = this.GetValue("CopyFrom");
            this.LtrHits.Text = this.GetValue("Hits");
            this.LtrUpdateTime.Text = this.GetValue("UpdateTime");
            this.LtrStars.Text = this.GetValue("Stars");
        }

        private void InitPage()
        {
            this.m_GeneralId = BasePage.RequestInt32("GeneralID");
            if (base.Request.UrlReferrer != null)
            {
                this.ViewState["UrlReferrer"] = base.Request.UrlReferrer.ToString();
            }
            this.contentDataTable = ContentManage.GetContentDataById(this.m_GeneralId);
            if ((this.contentDataTable == null) || (this.contentDataTable.Rows.Count == 0))
            {
                AdminPage.WriteErrMsg("<li>指定项目不存在！</li>");
            }
            this.InitContent();
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
            this.SmpNavigator.AdditionalNode = EasyOne.Contents.Nodes.ShowNodeNavigation(nodeId);
            if (!string.IsNullOrEmpty(this.SmpNavigator.AdditionalNode))
            {
                this.SmpNavigator.AdditionalNode = this.SmpNavigator.AdditionalNode.Replace("根节点 >>", "");
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
        }

        private void InitTabByFieldType(RepeaterItemEventArgs e, FieldInfo fieldInfo)
        {
            HtmlTableRow row = (HtmlTableRow) e.Item.FindControl("Tab");
            row.Style.Add("display", "none");
            if (this.arrTrs0.Length == 0)
            {
                this.arrTrs0.Append("\"" + row.ClientID + "\"");
            }
            else
            {
                this.arrTrs0.Append(",\"" + row.ClientID + "\"");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.InitPage();
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
                string status = this.contentDataTable.Rows[0][dataItem.FieldName].ToString();
                if (dataItem.FieldType == FieldType.KeywordType)
                {
                    status = StringHelper.ReplaceChar(this.contentDataTable.Rows[0][dataItem.FieldName].ToString(), '|', ' ');
                }
                switch (dataItem.FieldType)
                {
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
                                builder.Append(DataSecurity.HtmlEncode(specialCategoryInfoById.SpecialCategoryName) + ">>" + DataSecurity.HtmlEncode(specialInfoById.SpecialName));
                                builder.Append("<br /></span>");
                            }
                        }
                        break;

                    case FieldType.StatusType:
                        literal.Text = this.GetStatusShow(status);
                        return;

                    case FieldType.ContentType:
                        literal.BeginTag = "<iframe marginwidth='0' marginheight='0' frameborder='0' name='ContentPreview' width='650px' height='500px' src='ContentPreview.aspx?GeneralID=" + this.m_GeneralId.ToString() + "&fieldName=" + base.Server.UrlEncode(dataItem.FieldName) + "'></iframe>";
                        return;

                    case FieldType.NodeType:
                        literal.BeginTag = EasyOne.Contents.Nodes.ShowNodeNavigation(DataConverter.CLng(status));
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
                    case FieldType.MultipleHtmlTextType:
                        literal.BeginTag = status;
                        return;

                    default:
                        literal.Text = status;
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

