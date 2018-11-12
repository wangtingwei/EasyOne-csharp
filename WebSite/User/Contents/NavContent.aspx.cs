namespace EasyOne.WebSite.User.Contents
{
    using EasyOne.AccessManage;
    using EasyOne.Common;
    using EasyOne.CommonModel;
    using EasyOne.Components;
    using EasyOne.Contents;
    using EasyOne.Controls;
    using EasyOne.Enumerations;
    using EasyOne.Model.Contents;
    using EasyOne.Web.UI;
    using System;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public partial class NavContent : DynamicPage
    {

        protected void BtnSelectNode_Click(object sender, EventArgs e)
        {
            string str = BasePage.RequestString("Action");
            if (string.IsNullOrEmpty(str))
            {
                str = "add";
            }
            if (string.IsNullOrEmpty(this.HdnNodeId.Value))
            {
                DynamicPage.WriteUserErrMsg("您没有发布信息的权限！", "NavContent.aspx");
            }
            else if (string.IsNullOrEmpty(this.HdnModelId.Value))
            {
                DynamicPage.WriteUserErrMsg("没有选择内容模型！", "NavContent.aspx");
            }
            else
            {
                NodeInfo cacheNodeById = Nodes.GetCacheNodeById(DataConverter.CLng(this.HdnNodeId.Value));
                if (cacheNodeById.IsNull)
                {
                    DynamicPage.WriteUserErrMsg("<li>节点不存在！</li>", "NavContent.aspx");
                }
                else
                {
                    bool flag = UserPermissions.AccessCheck(OperateCode.NodeContentInput, cacheNodeById.NodeId);
                    if (!flag)
                    {
                        string checkStr = UserPermissions.GetRoleNodeId(PEContext.Current.User.RoleId, OperateCode.NodeContentInput, PEContext.Current.User.UserInfo.IsInheritGroupRole ? 1 : 0);
                        string findStr = cacheNodeById.NodeId.ToString();
                        if ((cacheNodeById.ParentId > 0) && !flag)
                        {
                            findStr = cacheNodeById.ParentPath + "," + cacheNodeById.NodeId.ToString();
                            flag = StringHelper.FoundCharInArr(checkStr, findStr);
                        }
                    }
                    if (!flag)
                    {
                        DynamicPage.WriteUserErrMsg("您没有此节点的添加权限！", "NavContent.aspx");
                    }
                    if (ModelManager.GetCacheModelById(DataConverter.CLng(this.HdnModelId.Value)).IsEshop)
                    {
                        BasePage.ResponseRedirect(SiteConfig.SiteInfo.VirtualPath + "User/Shop/Product.aspx?nodeId=" + this.HdnNodeId.Value + "&modelId=" + this.HdnModelId.Value + "&Action=" + str);
                    }
                    else
                    {
                        BasePage.ResponseRedirect("Content.aspx?nodeId=" + this.HdnNodeId.Value + "&modelId=" + this.HdnModelId.Value + "&Action=" + str);
                    }
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }
    }
}

