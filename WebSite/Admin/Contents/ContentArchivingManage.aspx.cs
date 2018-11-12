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
    using EasyOne.ModelControls;
    using EasyOne.UserManage;
    using EasyOne.Web.UI;
    using EasyOne.WebSite.Controls;
    using EasyOne.WorkFlows;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Text;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public partial class ContentArchivingManage : AdminPage
    {
        private bool m_Administrator;
        protected Dictionary<int, string> m_ModelPreviewDictionary = new Dictionary<int, string>();
        private int m_NodeId;
        protected Dictionary<int, NodeInfo> m_NodeInfoDictionary = new Dictionary<int, NodeInfo>();
        protected Dictionary<int, string> m_StatusDictionary = new Dictionary<int, string>();

        protected void BtnCancel_Click(object sender, EventArgs e)
        {
            this.PermissionDetection();
            StringBuilder selectList = this.EgvContent.SelectList;
            if (selectList.Length == 0)
            {
                AdminPage.WriteErrMsg("<li>对不起，您还没选择要取消归档的项目！</li>", string.Concat(new object[] { "ContentArchivingManage.aspx?NodeID=", BasePage.RequestInt32("NodeID"), "&ListType=", this.HdnListType.Value }));
            }
            else
            {
                this.UpdateStatus(selectList.ToString(), 0);
            }
        }

        protected void BtnEmpty_Click(object sender, EventArgs e)
        {
            ContentManage.EmptyContentArchiving();
            if (SiteConfig.SmsConfig.IsAutoSendStateMessage)
            {
                AdminPage.WriteSuccessMsg("<li>清空归档成功！</li><br /> " + this.ChangeStateSendMessageToUser(-3), string.Concat(new object[] { "ContentArchivingManage.aspx?NodeID=", BasePage.RequestInt32("NodeID"), "&Status=102&ListType=", this.HdnListType.Value }));
            }
            else
            {
                AdminPage.WriteSuccessMsg("<li>清空归档成功！</li>", string.Concat(new object[] { "ContentArchivingManage.aspx?NodeID=", BasePage.RequestInt32("NodeID"), "&Status=102&ListType=", this.HdnListType.Value }));
            }
        }

        private string ChangeStateSendMessageToUser(int state)
        {
            string changeStateMessage = SiteConfig.SmsConfig.ChangeStateMessage;
            if (string.IsNullOrEmpty(changeStateMessage))
            {
                return "没有配置回馈信息";
            }
            StringBuilder builder = new StringBuilder();
            if (string.IsNullOrEmpty(this.EgvContent.SelectList.ToString()))
            {
                for (int i = 0; i < this.EgvContent.PageCount; i++)
                {
                    for (int j = 0; j < this.EgvContent.Rows.Count; j++)
                    {
                        HyperLink link = (HyperLink) this.EgvContent.Rows[j].FindControl("HypTitle");
                        builder.Append(Users.ChangeStateSendMessageToUser(this.EgvContent.Rows[j].Cells[3].Text, changeStateMessage, link.Text, state.ToString(CultureInfo.CurrentCulture)));
                    }
                }
            }
            else
            {
                string[] strArray = this.EgvContent.SelectList.ToString().Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                if (strArray.Length == this.EgvContent.Rows.Count)
                {
                    for (int k = 0; k < this.EgvContent.Rows.Count; k++)
                    {
                        HyperLink link2 = (HyperLink) this.EgvContent.Rows[k].FindControl("HypTitle");
                        builder.Append(Users.ChangeStateSendMessageToUser(this.EgvContent.Rows[k].Cells[3].Text, changeStateMessage, link2.Text, state.ToString(CultureInfo.CurrentCulture)));
                    }
                }
                else
                {
                    foreach (string str2 in strArray)
                    {
                        for (int m = 0; m < this.EgvContent.Rows.Count; m++)
                        {
                            if (str2 == this.EgvContent.Rows[m].Cells[1].Text)
                            {
                                HyperLink link3 = (HyperLink) this.EgvContent.Rows[m].FindControl("HypTitle");
                                builder.Append(Users.ChangeStateSendMessageToUser(this.EgvContent.Rows[m].Cells[3].Text, changeStateMessage, link3.Text, state.ToString(CultureInfo.CurrentCulture)));
                                break;
                            }
                        }
                    }
                }
            }
            return builder.ToString();
        }

        protected void DropRescentQuerySelectedIndex_Changed(object sender, EventArgs e)
        {
            this.HdnListType.Value = this.DropRescentQuery.SelectedValue;
            this.EgvContent.PageIndex = 0;
        }

        protected void EBtnBatchDelete_Click(object sender, EventArgs e)
        {
            this.PermissionDetection();
            StringBuilder selectList = this.EgvContent.SelectList;
            if (selectList.Length == 0)
            {
                AdminPage.WriteErrMsg("<li>对不起，您还没选择要删除的项目！</li>", string.Concat(new object[] { "ContentArchivingManage.aspx?NodeID=", BasePage.RequestInt32("NodeID"), "&ListType=", this.HdnListType.Value }));
            }
            else if (ContentManage.UpdateStatus(selectList.ToString(), -3))
            {
                PermissionContent.Delete(selectList.ToString());
                EasyOne.Contents.ContentCharge.Delete(selectList.ToString());
                SiteCache.Remove("CK_Page_Category_" + this.m_NodeId.ToString());
                if (SiteConfig.SmsConfig.IsAutoSendStateMessage)
                {
                    AdminPage.WriteSuccessMsg("<li>删除成功！</li><br /> " + this.ChangeStateSendMessageToUser(-3), string.Concat(new object[] { "ContentArchivingManage.aspx?NodeID=", BasePage.RequestInt32("NodeID"), "&ListType=", this.HdnListType.Value }));
                }
                else
                {
                    AdminPage.WriteSuccessMsg("<li>删除成功！</li>", string.Concat(new object[] { "ContentArchivingManage.aspx?NodeID=", BasePage.RequestInt32("NodeID"), "&ListType=", this.HdnListType.Value }));
                }
            }
            else
            {
                AdminPage.WriteErrMsg("<li>删除项目失败！</li>", string.Concat(new object[] { "ContentArchivingManage.aspx?NodeID=", BasePage.RequestInt32("NodeID"), "&ListType=", this.HdnListType.Value }));
            }
        }

        protected void EgvContent_RowCommand(object sender, CommandEventArgs e)
        {
            int generalId = DataConverter.CLng(e.CommandArgument.ToString());
            string commandName = e.CommandName;
            if (commandName != null)
            {
                if (!(commandName == "CancelArchiving"))
                {
                    if (!(commandName == "DeleteContent"))
                    {
                        return;
                    }
                }
                else
                {
                    if (ContentManage.UpdateStatus(generalId, 0))
                    {
                        AdminPage.WriteSuccessMsg("<li>取消归档成功！</li>", string.Concat(new object[] { "ContentArchivingManage.aspx?NodeID=", BasePage.RequestInt32("NodeID"), "&Status=102&ListType=", this.HdnListType.Value }));
                        return;
                    }
                    AdminPage.WriteErrMsg("<li>取消归档失败！</li>", string.Concat(new object[] { "ContentArchivingManage.aspx?NodeID=", BasePage.RequestInt32("NodeID"), "&Status=102&ListType=", this.HdnListType.Value }));
                    return;
                }
                if (ContentManage.UpdateStatus(generalId, -3))
                {
                    PermissionContent.Delete(generalId);
                    EasyOne.Contents.ContentCharge.Delete(generalId);
                    SiteCache.Remove("CK_Page_Category_" + this.m_NodeId.ToString());
                    AdminPage.WriteSuccessMsg("<li>删除成功！</li>", string.Concat(new object[] { "ContentArchivingManage.aspx?NodeID=", BasePage.RequestInt32("NodeID"), "&Status=102&ListType=", this.HdnListType.Value }));
                }
                else
                {
                    AdminPage.WriteErrMsg("<li>删除失败！</li>", string.Concat(new object[] { "ContentArchivingManage.aspx?NodeID=", BasePage.RequestInt32("NodeID"), "&Status=102&ListType=", this.HdnListType.Value }));
                }
            }
        }

        protected void EgvContent_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                CommonModelInfo dataItem = (CommonModelInfo) e.Row.DataItem;
                int nodeId = dataItem.NodeId;
                string s = "";
                int length = 0;
                if (this.m_NodeInfoDictionary.ContainsKey(dataItem.NodeId))
                {
                    s = this.m_NodeInfoDictionary[dataItem.NodeId].NodeName;
                }
                else
                {
                    NodeInfo cacheNodeById = Nodes.GetCacheNodeById(dataItem.NodeId);
                    if (!cacheNodeById.IsNull)
                    {
                        s = cacheNodeById.NodeName;
                        this.m_NodeInfoDictionary.Add(dataItem.NodeId, cacheNodeById);
                    }
                }
                if (dataItem.NodeId != this.m_NodeId)
                {
                    ExtendedHyperLink link = e.Row.FindControl("LnkNodeLink") as ExtendedHyperLink;
                    link.BeginTag = "<strong>[";
                    link.Text = s;
                    link.EndTag = "]</strong>";
                    link.NavigateUrl = "ContentManage.aspx?NodeID=" + dataItem.NodeId.ToString() + "&NodeName=" + base.Server.UrlEncode(s);
                    length = StringHelper.SubStringLength(s) + 2;
                }
                HyperLink link2 = e.Row.FindControl("HypTitle") as HyperLink;
                LinkImage image = e.Row.FindControl("LinkImageModel") as LinkImage;
                string itemIcon = ModelManager.GetCacheModelById(dataItem.ModelId).ItemIcon;
                if (string.IsNullOrEmpty(itemIcon))
                {
                    itemIcon = "Default.gif";
                }
                image.Icon = itemIcon;
                if (dataItem.LinkType != 0)
                {
                    image.IsShowLink = true;
                }
                if (this.m_ModelPreviewDictionary.ContainsKey(dataItem.ModelId))
                {
                    link2.NavigateUrl = this.m_ModelPreviewDictionary[dataItem.ModelId] + "?GeneralID=" + dataItem.GeneralId;
                }
                else
                {
                    ModelInfo modelInfoById = ModelManager.GetModelInfoById(dataItem.ModelId);
                    link2.NavigateUrl = modelInfoById.PreviewInfoFilePath + "?GeneralID=" + dataItem.GeneralId;
                    this.m_ModelPreviewDictionary.Add(dataItem.ModelId, modelInfoById.PreviewInfoFilePath);
                }
                dataItem.Title = dataItem.Title;
                length = 0x25 - length;
                link2.Text = StringHelper.SubString(dataItem.Title, length, "...");
                link2.ToolTip = "标    题：" + dataItem.Title + "\r\n录 入 者：" + dataItem.Inputer + "\r\n更新时间：" + dataItem.UpdateTime.ToString();
                Label label = e.Row.FindControl("LblIsCreateHtml") as Label;
                if (!dataItem.CreateTime.HasValue || (dataItem.CreateTime.Value <= dataItem.UpdateTime))
                {
                    label.Text = "<span style=\"color:Red\"><b>\x00d7</b></span>";
                }
                else
                {
                    label.Text = "<b>√</b>";
                }
                LinkButton button = (LinkButton) e.Row.FindControl("CancelArchiving");
                LinkButton button2 = (LinkButton) e.Row.FindControl("ContentDelete");
                button.Text = "取消归档";
                button.CommandName = "CancelArchiving";
                button.CommandArgument = dataItem.GeneralId.ToString();
                button.OnClientClick = "if(!this.disabled) return confirm('确实要还原此归档信息吗？');";
                button2.Text = "删除";
                button2.CommandName = "DeleteContent";
                button2.CommandArgument = dataItem.GeneralId.ToString();
                button2.OnClientClick = "if(!this.disabled) return confirm('确实要删除此信息吗？删除后你还可以从回收站中还原！');";
                if (ModelManager.GetCacheModelById(dataItem.ModelId).Disabled)
                {
                    button2.Enabled = false;
                    button.Enabled = false;
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

                case 100:
                    return "归档稿件";
            }
            return "";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.m_Administrator = PEContext.Current.Admin.IsSuperAdmin;
            this.m_NodeId = BasePage.RequestInt32("NodeID");
            bool flag = false;
            if (!this.m_Administrator)
            {
                if (this.m_NodeId > 0)
                {
                    string findStr = this.m_NodeId.ToString();
                    NodeInfo cacheNodeById = Nodes.GetCacheNodeById(this.m_NodeId);
                    if (cacheNodeById.IsNull)
                    {
                        AdminPage.WriteErrMsg("当前栏目不存在，可能被删除了请返回！");
                    }
                    if (cacheNodeById.ParentId > 0)
                    {
                        findStr = findStr + "," + cacheNodeById.ParentPath;
                    }
                    flag = StringHelper.FoundCharInArr(RolePermissions.GetRoleNodeId(PEContext.Current.Admin.Roles, OperateCode.NodeContentManage), findStr);
                }
                else
                {
                    flag = RolePermissions.AccessCheckNodePermission(OperateCode.NodeContentManage, -1);
                }
                if (!flag)
                {
                    AdminPage.WriteErrMsg("您没有此栏目的管理权限！");
                }
            }
            foreach (StatusInfo info2 in Status.GetStatusList())
            {
                if (!this.m_StatusDictionary.ContainsKey(info2.StatusCode))
                {
                    this.m_StatusDictionary.Add(info2.StatusCode, info2.StatusName);
                }
            }
            if (!base.IsPostBack)
            {
                Nodes.GetNodeWorkFlowId(this.m_NodeId);
                this.DropRescentQuery.SelectedValue = BasePage.RequestStringToLower("ListType");
                this.HdnListType.Value = BasePage.RequestStringToLower("ListType");
                this.HdnStatus.Value = BasePage.RequestStringToLower("status", "102");
                if (this.m_NodeId > 0)
                {
                    this.SmpNavigator.CurrentNode = "<a href=\"ContentManage.aspx\">栏目信息管理</a>";
                    this.SmpNavigator.AdditionalNode = Nodes.ShowNodeNavigation(this.m_NodeId);
                    if (!string.IsNullOrEmpty(this.SmpNavigator.AdditionalNode))
                    {
                        this.SmpNavigator.AdditionalNode = this.SmpNavigator.AdditionalNode.Replace("根节点 >>", "");
                    }
                }
            }
        }

        private void PermissionDetection()
        {
            if (!this.m_Administrator)
            {
                AdminPage.WriteErrMsg("<li>对不起，只有超级管理员角色才能管理归档信息！</li>");
            }
        }

        private void UpdateStatus(string itemIDList, int status)
        {
            if (ContentManage.UpdateStatus(itemIDList, status))
            {
                if (SiteConfig.SmsConfig.IsAutoSendStateMessage)
                {
                    AdminPage.WriteSuccessMsg("<li>取消归档成功！</li><br /> " + this.ChangeStateSendMessageToUser(status), string.Concat(new object[] { "ContentArchivingManage.aspx?NodeID=", BasePage.RequestInt32("NodeID"), "&ListType=", this.HdnListType.Value }));
                }
                else
                {
                    AdminPage.WriteSuccessMsg("<li>取消归档成功！</li>", string.Concat(new object[] { "ContentArchivingManage.aspx?NodeID=", BasePage.RequestInt32("NodeID"), "&ListType=", this.HdnListType.Value }));
                }
            }
            else
            {
                AdminPage.WriteErrMsg("<li>取消归档失败！</li>", string.Concat(new object[] { "ContentArchivingManage.aspx?NodeID=", BasePage.RequestInt32("NodeID"), "&Status=102&ListType=", this.HdnListType.Value }));
            }
        }
    }
}

