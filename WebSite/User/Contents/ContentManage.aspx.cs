namespace EasyOne.WebSite.User.Contents
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
    using EasyOne.Model.UserManage;
    using EasyOne.ModelControls;
    using EasyOne.UserManage;
    using EasyOne.Web.UI;
    using System;
    using System.Collections.Generic;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public partial class ContentManageUI : DynamicPage
    {
        private string m_arrNodeIdInput = "";
        private bool m_IsInput;
        private bool m_IsManageStatusPassContent;
        private int m_NodeId;
        protected Dictionary<int, NodeInfo> m_NodeInfoDictionary = new Dictionary<int, NodeInfo>();
        private UserInfo m_UserInfo;

        private bool CheckUserConentInputPurview(int nodeId)
        {
            UserPrincipal user = PEContext.Current.User;
            this.m_UserInfo = Users.GetUsersByUserName(user.UserName);
            this.m_IsManageStatusPassContent = this.m_UserInfo.UserPurview.ManageSelfPublicInfo;
            if (this.m_IsManageStatusPassContent)
            {
                this.m_arrNodeIdInput = UserPermissions.GetRoleNodeId(user.RoleId, OperateCode.NodeContentInput, user.UserInfo.IsInheritGroupRole ? 1 : 0);
                if (StringHelper.FoundCharInArr(this.m_arrNodeIdInput, "-1"))
                {
                    return true;
                }
                if (nodeId > 0)
                {
                    string findStr = nodeId.ToString();
                    NodeInfo cacheNodeById = Nodes.GetCacheNodeById(nodeId);
                    if (cacheNodeById.IsNull)
                    {
                        DynamicPage.WriteErrMsg("当前栏目不存在，可能被删除了请返回！");
                    }
                    if (cacheNodeById.ParentId > 0)
                    {
                        findStr = findStr + "," + cacheNodeById.ParentPath;
                    }
                    return StringHelper.FoundCharInArr(this.m_arrNodeIdInput, findStr);
                }
            }
            return false;
        }

        protected void EBtnBatchDelete_Click(object sender, EventArgs e)
        {
            this.m_IsInput = this.CheckUserConentInputPurview(0);
            if (!this.m_IsInput)
            {
                DynamicPage.WriteUserErrMsg("<li>没有删除权限！</li>");
            }
            if (ContentManage.UpdateStatusByUserName(this.EgvContent.SelectList.ToString(), -3))
            {
                DynamicPage.WriteUserSuccessMsg("<li>批量删除成功！</li>", "ContentManage.aspx?NodeID=" + BasePage.RequestInt32("NodeID"));
            }
            else
            {
                DynamicPage.WriteUserErrMsg("<li>批量删除项目失败！</li>");
            }
        }

        protected void EgvContent_RowCommand(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "DeleteContent")
            {
                int generalId = DataConverter.CLng(e.CommandArgument.ToString());
                CommonModelInfo commonModelInfoById = ContentManage.GetCommonModelInfoById(generalId);
                this.m_IsInput = this.CheckUserConentInputPurview(commonModelInfoById.NodeId);
                if (!this.m_IsInput)
                {
                    DynamicPage.WriteUserErrMsg("<li>没有删除权限！</li>");
                }
                if (ContentManage.UpdateStatusByUserName(e.CommandArgument.ToString(), -3))
                {
                    PermissionContent.Delete(generalId);
                    ContentCharge.Delete(generalId);
                    DynamicPage.WriteUserSuccessMsg("<li>删除成功！</li>", "ContentManage.aspx?NodeID=" + BasePage.RequestInt32("NodeID"));
                }
                else
                {
                    DynamicPage.WriteUserErrMsg("<li>删除失败！</li>");
                }
            }
        }

        protected void EgvContent_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                CommonModelInfo dataItem = (CommonModelInfo) e.Row.DataItem;
                int length = 0;
                NodeInfo cacheNodeById = null;
                if (this.m_NodeInfoDictionary.ContainsKey(dataItem.NodeId))
                {
                    cacheNodeById = this.m_NodeInfoDictionary[dataItem.NodeId];
                }
                else
                {
                    cacheNodeById = Nodes.GetCacheNodeById(dataItem.NodeId);
                    if (!cacheNodeById.IsNull)
                    {
                        this.m_NodeInfoDictionary.Add(dataItem.NodeId, cacheNodeById);
                    }
                }
                if (dataItem.NodeId != this.m_NodeId)
                {
                    ExtendedHyperLink link = e.Row.FindControl("LnkNodeLink") as ExtendedHyperLink;
                    link.BeginTag = "<strong>[";
                    link.Text = cacheNodeById.NodeName;
                    link.EndTag = "]</strong>";
                    link.NavigateUrl = "ContentManage.aspx?NodeID=" + dataItem.NodeId.ToString() + "&NodeName=" + base.Server.UrlEncode(cacheNodeById.NodeName);
                    length = StringHelper.SubStringLength(cacheNodeById.NodeName) + 2;
                }
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
                HyperLink link2 = (HyperLink) e.Row.FindControl("ContentModify");
                LinkButton button = (LinkButton) e.Row.FindControl("LtnDelete");
                string str2 = dataItem.NodeId.ToString();
                if (cacheNodeById.ParentId > 0)
                {
                    str2 = str2 + "," + cacheNodeById.ParentPath;
                }
                if (!StringHelper.FoundCharInArr(this.m_arrNodeIdInput, dataItem.NodeId.ToString()))
                {
                    link2.Enabled = false;
                    button.Enabled = false;
                    button.OnClientClick = "";
                }
                else if (!this.m_IsManageStatusPassContent && (dataItem.Status == 0x63))
                {
                    CheckBox box = (CheckBox) e.Row.FindControl("CheckBoxButton");
                    link2.Enabled = false;
                    button.Enabled = false;
                    button.OnClientClick = "";
                    box.Enabled = false;
                }
                if (ModelManager.GetCacheModelById(dataItem.ModelId).Disabled)
                {
                    link2.Enabled = false;
                    button.Enabled = false;
                    button.OnClientClick = "";
                }
                if (dataItem.IsEshop)
                {
                    link2.NavigateUrl = string.Concat(new object[] { "../Shop/Product.aspx?Action=Modify&NodeID=", dataItem.NodeId.ToString(), "&GeneralID=", dataItem.GeneralId, "&ModelID=", dataItem.ModelId.ToString() });
                }
                else
                {
                    link2.NavigateUrl = string.Concat(new object[] { "Content.aspx?Action=Modify&NodeID=", dataItem.NodeId.ToString(), "&GeneralID=", dataItem.GeneralId, "&ModelID=", dataItem.ModelId.ToString() });
                }
                HyperLink link3 = e.Row.FindControl("LnkItem") as HyperLink;
                length = 0x25 - length;
                link3.Text = StringHelper.SubString(dataItem.Title, length, "...");
                link3.ToolTip = dataItem.Title;
                link3.Target = "_blank";
                link3.NavigateUrl = base.FullBasePath + "Item/" + dataItem.GeneralId.ToString() + ".aspx";
                if (dataItem.Status != 0x63)
                {
                    link3.Target = "";
                    link3.NavigateUrl = link2.NavigateUrl;
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
            this.m_NodeId = BasePage.RequestInt32("NodeID");
            this.OdsContents.SelectParameters["userName"].DefaultValue = PEContext.Current.User.UserName;
            int num = BasePage.RequestInt32("Status");
            if (((num != 0) && (num < 0x66)) && (num > -3))
            {
                this.HdnStatus.Value = BasePage.RequestInt32("Status").ToString();
            }
            if (!base.IsPostBack)
            {
                this.m_IsInput = this.CheckUserConentInputPurview(this.m_NodeId);
                if (this.m_IsInput)
                {
                    this.EBtnBatchDelete.Visible = true;
                }
                else
                {
                    this.EBtnBatchDelete.Visible = false;
                }
            }
            string str = BasePage.RequestString("NodeName");
            if (string.IsNullOrEmpty(str))
            {
                str = "根目录";
            }
            this.LblNavigation.Text = str;
        }
    }
}

