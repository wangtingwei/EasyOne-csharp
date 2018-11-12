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
    using EasyOne.ModelControls;
    using EasyOne.StaticHtml;
    using EasyOne.Web.UI;
    using EasyOne.WebSite.Controls;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public partial class ContentHtml : AdminPage
    {
        private bool m_Administrator;
        private string m_arrContentNodeIdManage = "";
        protected Dictionary<int, string> m_ModelPreviewDictionary = new Dictionary<int, string>();
        protected Dictionary<int, NodeInfo> m_NodeNameDictionary = new Dictionary<int, NodeInfo>();

        protected void BtnCreate_Click(object sender, EventArgs e)
        {
            StringBuilder selectList = this.EgvContents.SelectList;
            if (selectList.Length == 0)
            {
                AdminPage.WriteErrMsg("<li>对不起，您还没选择要删除的项目！</li>", "ContentHtml.aspx?NodeID=" + BasePage.RequestInt32("NodeID"));
            }
            else
            {
                HtmlContent content = new HtmlContent();
                content.CreateMethod = CreateContentType.CreateByGeneralId;
                content.NodeIdArray = string.Empty;
                content.ContentGeneralIdArray = selectList.ToString();
                content.CommonCreateHtml();
                BasePage.ResponseRedirect("CreateHtmlProgress.aspx?workId=" + content.CreateId);
            }
        }

        protected void BtnCreateAll_Click(object sender, EventArgs e)
        {
            HtmlContent content = new HtmlContent();
            content.CreateMethod = CreateContentType.CreateAll;
            content.NodeIdArray = BasePage.RequestString("NodeID", string.Empty);
            content.CommonCreateHtml();
            BasePage.ResponseRedirect("CreateHtmlProgress.aspx?workId=" + content.CreateId);
        }

        protected void BtnDelete_Click(object sender, EventArgs e)
        {
            this.DeleteHtml(this.EgvContents.SelectList.ToString());
            this.EgvContents.DataBind();
        }

        private void DeleteHtml(string generalId)
        {
            IList<CommonModelInfo> commonModelInfoList = ContentManage.GetCommonModelInfoList(BasePage.RequestString("NodeID"), generalId);
            string str = base.Server.MapPath("~/" + VirtualPathUtility.AppendTrailingSlash(SiteConfig.SiteOption.CreateHtmlPath));
            foreach (CommonModelInfo info2 in commonModelInfoList)
            {
                string file = str;
                if (info2.CreateTime.HasValue && (info2.CreateTime.Value >= info2.UpdateTime))
                {
                    NodeInfo cacheNodeById = Nodes.GetCacheNodeById(info2.NodeId);
                    file = file + ContentManage.ContentHtmlName(info2, cacheNodeById, 0);
                    if (FileSystemObject.IsExist(file, FsoMethod.File))
                    {
                        FileSystemObject.Delete(file, FsoMethod.File);
                    }
                    DateTime? createTime = null;
                    ContentManage.UpdateCreateTime(info2.GeneralId, createTime);
                }
            }
        }

        protected void DropSelectedIndex_Changed(object sender, EventArgs e)
        {
            this.HdnListType.Value = this.DropRescentQuery.SelectedValue;
            this.EgvContents.PageIndex = 0;
        }

        protected void EgvContents_RowCommand(object sender, CommandEventArgs e)
        {
            int num = DataConverter.CLng(e.CommandArgument.ToString());
            string commandName = e.CommandName;
            if (commandName != null)
            {
                if (!(commandName == "CreateHtml"))
                {
                    if (!(commandName == "DeleteHtml"))
                    {
                        return;
                    }
                }
                else
                {
                    HtmlContent content = new HtmlContent();
                    content.CreateMethod = CreateContentType.CreateByGeneralId;
                    content.NodeIdArray = string.Empty;
                    content.ContentGeneralIdArray = e.CommandArgument.ToString();
                    content.CommonCreateHtml();
                    BasePage.ResponseRedirect("CreateHtmlProgress.aspx?workId=" + content.CreateId);
                    return;
                }
                this.DeleteHtml(num.ToString());
                this.EgvContents.DataBind();
            }
        }

        protected void EgvContents_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                CommonModelInfo commonModelInfo = new CommonModelInfo();
                commonModelInfo = (CommonModelInfo) e.Row.DataItem;
                int nodeId = BasePage.RequestInt32("NodeID");
                string s = "";
                int length = 0;
                NodeInfo cacheNodeById = new NodeInfo(true);
                if (commonModelInfo.NodeId != nodeId)
                {
                    nodeId = commonModelInfo.NodeId;
                    if (this.m_NodeNameDictionary.ContainsKey(commonModelInfo.NodeId))
                    {
                        cacheNodeById = this.m_NodeNameDictionary[commonModelInfo.NodeId];
                        s = cacheNodeById.NodeName;
                    }
                    else
                    {
                        cacheNodeById = Nodes.GetCacheNodeById(commonModelInfo.NodeId);
                        if (cacheNodeById != null)
                        {
                            s = cacheNodeById.NodeName;
                            this.m_NodeNameDictionary.Add(commonModelInfo.NodeId, cacheNodeById);
                        }
                    }
                    ExtendedHyperLink link = e.Row.FindControl("LnkNodeLink") as ExtendedHyperLink;
                    link.BeginTag = "<strong>[";
                    link.Text = s;
                    link.EndTag = "]</strong>";
                    link.NavigateUrl = "ContentHtml.aspx?NodeID=" + commonModelInfo.NodeId.ToString() + "&NodeName=" + base.Server.UrlEncode(s);
                    length = StringHelper.SubStringLength(s) + 2;
                }
                LinkImage image = e.Row.FindControl("LinkImageModel") as LinkImage;
                string itemIcon = ModelManager.GetCacheModelById(commonModelInfo.ModelId).ItemIcon;
                if (string.IsNullOrEmpty(itemIcon))
                {
                    itemIcon = "Default.gif";
                }
                image.Icon = itemIcon;
                if (commonModelInfo.LinkType != 0)
                {
                    image.IsShowLink = true;
                }
                HyperLink link2 = e.Row.FindControl("HypTitle") as HyperLink;
                if (this.m_ModelPreviewDictionary.ContainsKey(commonModelInfo.ModelId))
                {
                    link2.NavigateUrl = this.m_ModelPreviewDictionary[commonModelInfo.ModelId] + "?GeneralID=" + commonModelInfo.GeneralId;
                }
                else
                {
                    ModelInfo modelInfoById = ModelManager.GetModelInfoById(commonModelInfo.ModelId);
                    link2.NavigateUrl = modelInfoById.PreviewInfoFilePath + "?GeneralID=" + commonModelInfo.GeneralId;
                    this.m_ModelPreviewDictionary.Add(commonModelInfo.ModelId, modelInfoById.PreviewInfoFilePath);
                }
                commonModelInfo.Title = commonModelInfo.Title;
                length = 0x25 - length;
                link2.Text = StringHelper.SubString(commonModelInfo.Title, length, "...");
                link2.ToolTip = commonModelInfo.Title;
                Label label = e.Row.FindControl("LblIsCreateHtml") as Label;
                if (!commonModelInfo.CreateTime.HasValue || (commonModelInfo.CreateTime.Value <= commonModelInfo.UpdateTime))
                {
                    label.Text = "<span style=\"color:Red\"><b>\x00d7</b></span>";
                    ((HyperLink) e.Row.FindControl("LnkHtmlView")).Enabled = false;
                    ((LinkButton) e.Row.FindControl("LnkDeleteHtml")).Enabled = false;
                }
                else
                {
                    if (this.m_NodeNameDictionary.ContainsKey(commonModelInfo.NodeId))
                    {
                        cacheNodeById = this.m_NodeNameDictionary[commonModelInfo.NodeId];
                    }
                    else
                    {
                        cacheNodeById = Nodes.GetCacheNodeById(commonModelInfo.NodeId);
                        if (!cacheNodeById.IsNull)
                        {
                            s = cacheNodeById.NodeName;
                            this.m_NodeNameDictionary.Add(commonModelInfo.NodeId, cacheNodeById);
                        }
                    }
                    label.Text = "<b>√</b>";
                    string str3 = (SiteConfig.SiteInfo.VirtualPath + VirtualPathUtility.AppendTrailingSlash(SiteConfig.SiteOption.CreateHtmlPath) + ContentManage.ContentHtmlName(commonModelInfo, cacheNodeById, 0)).Replace("//", "/");
                    ((HyperLink) e.Row.FindControl("LnkHtmlView")).NavigateUrl = str3;
                }
                if (!this.m_Administrator)
                {
                    string checkStr = nodeId.ToString();
                    if (cacheNodeById.IsNull)
                    {
                        cacheNodeById = Nodes.GetCacheNodeById(nodeId);
                    }
                    if (cacheNodeById.ParentId > 0)
                    {
                        checkStr = checkStr + "," + cacheNodeById.ParentPath;
                    }
                    if (!StringHelper.FoundCharInArr(checkStr, this.m_arrContentNodeIdManage))
                    {
                        ((LinkButton) e.Row.FindControl("LnkCreateHtml")).Enabled = false;
                        ((HyperLink) e.Row.FindControl("LnkHtmlView")).Enabled = false;
                        ((LinkButton) e.Row.FindControl("LnkDeleteHtml")).Enabled = false;
                    }
                }
            }
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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (PEContext.Current.Admin.IsSuperAdmin)
            {
                this.m_Administrator = true;
            }
            else
            {
                this.m_arrContentNodeIdManage = RolePermissions.GetRoleNodeId(PEContext.Current.Admin.Roles, OperateCode.NodeContentManage);
            }
            if (!base.IsPostBack)
            {
                int nodeId = BasePage.RequestInt32("NodeID");
                if (nodeId > 0)
                {
                    this.SmpNavigator.AdditionalNode = Nodes.ShowNodeNavigation(nodeId, "ContentHtml.aspx");
                }
                if (!this.m_Administrator)
                {
                    bool flag = false;
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
                        if (StringHelper.FoundCharInArr(this.m_arrContentNodeIdManage, findStr))
                        {
                            flag = true;
                        }
                    }
                    else
                    {
                        flag = RolePermissions.AccessCheckNodePermission(OperateCode.NodeContentManage, -1);
                    }
                    if (!flag)
                    {
                        this.BtnCreateAll.Enabled = false;
                        this.BtnCreate.Enabled = false;
                        this.BtnDelete.Enabled = false;
                    }
                }
            }
        }

        protected void RadlCreated_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.HdnCreated.Value = this.RadlCreated.SelectedValue;
        }
    }
}

