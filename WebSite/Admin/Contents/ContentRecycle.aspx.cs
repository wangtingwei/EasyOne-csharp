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
    using EasyOne.Model.Contents;
    using EasyOne.ModelControls;
    using EasyOne.UserManage;
    using EasyOne.Web.UI;
    using EasyOne.WebSite.Controls;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Text;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public partial class ContentRecycle : AdminPage
    {
        private bool m_Administrator;
        private string m_arrContentNodeIdManage = "";
        protected Dictionary<int, string> m_NodeNameDictionary = new Dictionary<int, string>();
        private int nodeId;

        protected void BtnClear_Click(object sender, EventArgs e)
        {
            if (this.nodeId > 0)
            {
                ContentManage.DeleteByNodeId(Nodes.GetCacheNodeById(this.nodeId).ArrChildId, -3);
                AdminPage.WriteSuccessMsg("<li>删除成功！</li>", "ContentRecycle.aspx?NodeID=" + this.nodeId);
            }
            else
            {
                StringBuilder sb = new StringBuilder();
                foreach (CommonModelInfo info2 in ContentManage.GetCommonModelInfoList(0, 0, -1, ContentSortType.None, -3))
                {
                    StringHelper.AppendString(sb, info2.GeneralId.ToString());
                }
                ContentManage.Delete(sb.ToString());
                AdminPage.WriteSuccessMsg("<li>删除成功！</li>", "ContentRecycle.aspx");
            }
        }

        protected void BtnRecycle_Click(object sender, EventArgs e)
        {
            if (this.nodeId > 0)
            {
                ContentManage.RecycleAll(Nodes.GetCacheNodeById(this.nodeId).ArrChildId);
                if (SiteConfig.SmsConfig.IsAutoSendStateMessage)
                {
                    AdminPage.WriteSuccessMsg("<li>还原成功！</li><br /> " + this.ChangeStateSendMessageToUser(0), "ContentRecycle.aspx?NodeID=" + this.nodeId);
                }
                else
                {
                    AdminPage.WriteSuccessMsg("<li>还原成功！</li>", "ContentRecycle.aspx?NodeID=" + this.nodeId);
                }
            }
            else
            {
                ContentManage.RecycleAll("0");
                if (SiteConfig.SmsConfig.IsAutoSendStateMessage)
                {
                    AdminPage.WriteSuccessMsg("<li>还原成功！</li><br /> " + this.ChangeStateSendMessageToUser(0), "ContentRecycle.aspx?NodeID=" + this.nodeId);
                }
                else
                {
                    AdminPage.WriteSuccessMsg("<li>还原成功！</li>", "ContentRecycle.aspx?NodeID=" + this.nodeId);
                }
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
            if (string.IsNullOrEmpty(this.EgvContentRecycle.SelectList.ToString()))
            {
                for (int i = 0; i < this.EgvContentRecycle.PageCount; i++)
                {
                    for (int j = 0; j < this.EgvContentRecycle.Rows.Count; j++)
                    {
                        HyperLink link = (HyperLink) this.EgvContentRecycle.Rows[j].FindControl("HypTitle");
                        builder.Append(Users.ChangeStateSendMessageToUser(this.EgvContentRecycle.Rows[j].Cells[3].Text, changeStateMessage, link.Text, state.ToString(CultureInfo.CurrentCulture)));
                    }
                }
            }
            else
            {
                string[] strArray = this.EgvContentRecycle.SelectList.ToString().Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                if (strArray.Length == this.EgvContentRecycle.Rows.Count)
                {
                    for (int k = 0; k < this.EgvContentRecycle.Rows.Count; k++)
                    {
                        HyperLink link2 = (HyperLink) this.EgvContentRecycle.Rows[k].FindControl("HypTitle");
                        builder.Append(Users.ChangeStateSendMessageToUser(this.EgvContentRecycle.Rows[k].Cells[3].Text, changeStateMessage, link2.Text, state.ToString(CultureInfo.CurrentCulture)));
                    }
                }
                else
                {
                    foreach (string str2 in strArray)
                    {
                        for (int m = 0; m < this.EgvContentRecycle.Rows.Count; m++)
                        {
                            if (str2 == this.EgvContentRecycle.Rows[m].Cells[1].Text)
                            {
                                HyperLink link3 = (HyperLink) this.EgvContentRecycle.Rows[m].FindControl("HypTitle");
                                builder.Append(Users.ChangeStateSendMessageToUser(this.EgvContentRecycle.Rows[m].Cells[3].Text, changeStateMessage, link3.Text, state.ToString(CultureInfo.CurrentCulture)));
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
            this.EgvContentRecycle.PageIndex = 0;
        }

        protected void EBtnDelete_Click(object sender, EventArgs e)
        {
            if (ContentManage.Delete(this.EgvContentRecycle.SelectList.ToString()))
            {
                AdminPage.WriteSuccessMsg("<li>彻底删除成功</li>", "ContentRecycle.aspx?NodeID=" + BasePage.RequestInt32("NodeID"));
            }
            else
            {
                AdminPage.WriteErrMsg("<li>彻底删除失败</li>");
            }
        }

        protected void EBtnRestore_Click(object sender, EventArgs e)
        {
            string[] strArray = this.EgvContentRecycle.SelectList.ToString().Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < strArray.Length; i++)
            {
                ContentManage.UpdateStatus(DataConverter.CLng(strArray[i]), 0);
            }
            if (SiteConfig.SmsConfig.IsAutoSendStateMessage)
            {
                AdminPage.WriteSuccessMsg("<li>还原成功</li><br /> " + this.ChangeStateSendMessageToUser(0), "ContentRecycle.aspx?NodeID=" + this.nodeId);
            }
            else
            {
                AdminPage.WriteSuccessMsg("<li>还原成功</li>", "ContentRecycle.aspx?NodeID=" + this.nodeId);
            }
        }

        protected void EgvContentRecycle_RowCommand(object sender, CommandEventArgs e)
        {
            int generalId = DataConverter.CLng(e.CommandArgument.ToString());
            if (e.CommandName == "RestoreContent")
            {
                if (ContentManage.UpdateStatus(generalId, 0))
                {
                    AdminPage.WriteSuccessMsg("<li>还原成功！</li>", "ContentRecycle.aspx?NodeID=" + this.nodeId);
                }
                else
                {
                    AdminPage.WriteErrMsg("<li>还原失败！</li>");
                }
            }
            if (e.CommandName == "DeleteContent")
            {
                if (ContentManage.Delete(generalId.ToString()))
                {
                    AdminPage.WriteSuccessMsg("<li>删除成功！</li>", "ContentRecycle.aspx?NodeID=" + this.nodeId);
                }
                else
                {
                    AdminPage.WriteErrMsg("<li>删除失败！</li>");
                }
            }
        }

        protected void EgvContentRecycle_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                CommonModelInfo dataItem = new CommonModelInfo();
                dataItem = (CommonModelInfo) e.Row.DataItem;
                NodeInfo cacheNodeById = new NodeInfo(true);
                int length = 0;
                string nodeName = "";
                if (dataItem.NodeId != this.nodeId)
                {
                    this.nodeId = dataItem.NodeId;
                    if (this.m_NodeNameDictionary.ContainsKey(dataItem.NodeId))
                    {
                        nodeName = this.m_NodeNameDictionary[dataItem.NodeId];
                    }
                    else
                    {
                        cacheNodeById = Nodes.GetCacheNodeById(dataItem.NodeId);
                        if (cacheNodeById != null)
                        {
                            nodeName = cacheNodeById.NodeName;
                            this.m_NodeNameDictionary.Add(dataItem.NodeId, nodeName);
                        }
                    }
                    ExtendedHyperLink link = e.Row.FindControl("LnkNodeLink") as ExtendedHyperLink;
                    link.BeginTag = "<strong>[";
                    link.Text = nodeName;
                    link.EndTag = "]</strong>";
                    link.NavigateUrl = "ContentManage.aspx?NodeID=" + dataItem.NodeId.ToString() + "&NodeName=" + base.Server.UrlEncode(nodeName);
                    length = StringHelper.SubStringLength(nodeName) + 2;
                }
                HyperLink link2 = e.Row.FindControl("HypTitle") as HyperLink;
                length = 0x25 - length;
                link2.Text = StringHelper.SubString(dataItem.Title, length, "...");
                link2.ToolTip = dataItem.Title;
                link2.NavigateUrl = "ContentView.aspx?GeneralID=" + dataItem.GeneralId.ToString();
                Label label = e.Row.FindControl("LblIsCreateHtml") as Label;
                label.Text = "<span style=\"color:Red\"><b>\x00d7</b></span>";
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
                if (!this.m_Administrator)
                {
                    string checkStr = this.nodeId.ToString();
                    if (cacheNodeById.IsNull)
                    {
                        cacheNodeById = Nodes.GetCacheNodeById(this.nodeId);
                    }
                    if (cacheNodeById.ParentId > 0)
                    {
                        checkStr = checkStr + "," + cacheNodeById.ParentPath;
                    }
                    if (!StringHelper.FoundCharInArr(checkStr, this.m_arrContentNodeIdManage))
                    {
                        ((LinkButton) e.Row.FindControl("DeleteContent")).Enabled = false;
                        ((LinkButton) e.Row.FindControl("RestoreContent")).Enabled = false;
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
            this.nodeId = BasePage.RequestInt32("NodeID");
            if (!base.IsPostBack)
            {
                NodeInfo cacheNodeById = new NodeInfo(true);
                if (this.nodeId > 0)
                {
                    this.SmpNavigator.AdditionalNode = Nodes.ShowNodeNavigation(this.nodeId, "ContentRecycle.aspx");
                    cacheNodeById = Nodes.GetCacheNodeById(this.nodeId);
                    this.BtnClear.Text = "清空" + cacheNodeById.NodeName + "下的信息";
                    this.BtnRecycle.Text = "还原" + cacheNodeById.NodeName + "下的信息";
                }
                this.DropRescentQuery.SelectedValue = BasePage.RequestStringToLower("ListType");
                this.HdnListType.Value = BasePage.RequestStringToLower("ListType");
                if (!this.m_Administrator)
                {
                    this.m_arrContentNodeIdManage = RolePermissions.GetRoleNodeId(PEContext.Current.Admin.Roles, OperateCode.NodeContentManage);
                    bool flag = false;
                    if (this.nodeId > 0)
                    {
                        string findStr = this.nodeId.ToString();
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
                        this.EBtnDelete.Enabled = false;
                        this.EBtnRestore.Enabled = false;
                        this.BtnClear.Enabled = false;
                        this.BtnRecycle.Enabled = false;
                    }
                }
            }
        }
    }
}

