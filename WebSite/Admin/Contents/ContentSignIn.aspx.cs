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
    using EasyOne.Web.UI;
    using EasyOne.WebSite.Controls;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public partial class ContentSignIn : AdminPage
    {
        private bool m_Administrator;
        private string m_arrContentNodeIdManage = "";
        protected Dictionary<int, string> m_NodeNameDictionary = new Dictionary<int, string>();

        protected void BtnSearch_Click(object sender, EventArgs e)
        {
            this.HdnSearchType.Value = this.DrpSearchType.SelectedValue;
            this.HdnSearchKeyword.Value = this.TxtSearchKeyword.Text;
            this.EgvContentSignin.DataBind();
        }

        protected void DropSelectedIndex_Changed(object sender, EventArgs e)
        {
            this.HdnSortType.Value = this.DropRescentQuery.SelectedValue;
            this.EgvContentSignin.PageIndex = 0;
        }

        protected void EgvContentSignin_RowCommand(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "DeleteContent")
            {
                int generalId = DataConverter.CLng(e.CommandArgument.ToString());
                if (ContentManage.UpdateStatus(generalId, -3))
                {
                    PermissionContent.Delete(generalId);
                    EasyOne.Contents.ContentCharge.Delete(generalId);
                    AdminPage.WriteSuccessMsg("<li>删除成功！</li>", "ContentSignin.aspx?NodeID=" + BasePage.RequestInt32("NodeID"));
                }
                else
                {
                    AdminPage.WriteErrMsg("<li>删除失败！</li>");
                }
            }
        }

        protected void EgvContentSignin_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                CommonModelInfo dataItem = (CommonModelInfo) e.Row.DataItem;
                NodeInfo cacheNodeById = new NodeInfo(true);
                int length = 0;
                int nodeId = BasePage.RequestInt32("NodeID");
                string nodeName = "";
                if (dataItem.NodeId != nodeId)
                {
                    nodeId = dataItem.NodeId;
                    if (this.m_NodeNameDictionary.ContainsKey(dataItem.NodeId))
                    {
                        nodeName = this.m_NodeNameDictionary[dataItem.NodeId];
                    }
                    else
                    {
                        cacheNodeById = Nodes.GetCacheNodeById(dataItem.NodeId);
                        if (!cacheNodeById.IsNull)
                        {
                            nodeName = cacheNodeById.NodeName;
                            this.m_NodeNameDictionary.Add(dataItem.NodeId, nodeName);
                        }
                    }
                    ExtendedHyperLink link = e.Row.FindControl("LnkNodeLink") as ExtendedHyperLink;
                    link.BeginTag = "<strong>[";
                    link.Text = nodeName;
                    link.EndTag = "]</strong>";
                    link.NavigateUrl = "ContentHtml.aspx?NodeID=" + dataItem.NodeId.ToString() + "&NodeName=" + base.Server.UrlEncode(nodeName);
                    length = StringHelper.SubStringLength(nodeName) + 2;
                }
                Label label = e.Row.FindControl("LblIsCreateHtml") as Label;
                if (!dataItem.CreateTime.HasValue || (dataItem.CreateTime.Value <= dataItem.UpdateTime))
                {
                    label.Text = "<span style=\"color:Red\"><b>\x00d7</b></span>";
                }
                else
                {
                    label.Text = "<b>√</b>";
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
                Label label2 = e.Row.FindControl("lblSigninStatus") as Label;
                SignInContentInfo signInContentByGeneralId = SignInContent.GetSignInContentByGeneralId(dataItem.GeneralId);
                string signInUsers = SignInLog.GetSignInUsers(dataItem.GeneralId);
                IList<SignInLogInfo> list = SignInLog.GetList(dataItem.GeneralId);
                StringBuilder sb = new StringBuilder();
                StringBuilder builder2 = new StringBuilder();
                foreach (SignInLogInfo info4 in list)
                {
                    if (info4.IsSignIn)
                    {
                        StringHelper.AppendString(sb, info4.UserName);
                    }
                    else
                    {
                        StringHelper.AppendString(builder2, info4.UserName);
                    }
                }
                string str4 = "";
                if (!signInContentByGeneralId.IsNull)
                {
                    str4 = "<font color=" + ((signInContentByGeneralId.Status == SignInStatus.NotSignIn) ? "red" : "green") + ">[" + BasePage.EnumToHtml<SignInStatus>(signInContentByGeneralId.Status) + "]</font>";
                    label2.Text = "<a href='' onclick='return false' title='要求签收用户：" + signInUsers + " &#13;已经签收用户：" + sb.ToString() + "&#13;尚未签收用户：" + builder2.ToString() + "'>" + str4 + "</a>";
                }
                HyperLink link2 = e.Row.FindControl("HypTitle") as HyperLink;
                length = 0x25 - length;
                link2.Text = StringHelper.SubString(dataItem.Title, length, "...");
                link2.ToolTip = dataItem.Title;
                link2.NavigateUrl = "ContentView.aspx?GeneralID=" + dataItem.GeneralId.ToString();
                HyperLink link3 = (HyperLink) e.Row.FindControl("EahContentModify");
                link3.NavigateUrl = "Content.aspx?Action=Modify&GeneralID=" + dataItem.GeneralId.ToString() + "&NodeID=" + dataItem.NodeId.ToString();
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
                        ((HyperLink) e.Row.FindControl("EahContentModify")).Enabled = false;
                        ((LinkButton) e.Row.FindControl("ELbtnDelete")).Enabled = false;
                    }
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (PEContext.Current.Admin.IsSuperAdmin)
            {
                this.m_Administrator = true;
            }
            if (!this.m_Administrator)
            {
                this.m_arrContentNodeIdManage = RolePermissions.GetRoleNodeId(PEContext.Current.Admin.Roles, OperateCode.NodeContentManage);
            }
            if (!base.IsPostBack)
            {
                this.DropRescentQuery.SelectedValue = BasePage.RequestStringToLower("ListType");
                this.HdnListType.Value = BasePage.RequestStringToLower("ListType");
                this.RadlContent.SelectedValue = BasePage.RequestStringToLower("status", "100");
                this.HdnStatus.Value = BasePage.RequestStringToLower("status", "100");
            }
        }

        protected void RadlContent_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.HdnStatus.Value = this.RadlContent.SelectedValue;
            this.EgvContentSignin.PageIndex = 0;
        }

        protected void RadlListType_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.HdnListType.Value = this.RadlListType.SelectedValue;
            this.EgvContentSignin.PageIndex = 0;
        }
    }
}

