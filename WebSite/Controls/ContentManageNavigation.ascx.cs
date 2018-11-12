namespace EasyOne.WebSite.Controls
{
    using AjaxControlToolkit;
    using EasyOne.AccessManage;
    using EasyOne.Common;
    using EasyOne.CommonModel;
    using EasyOne.Components;
    using EasyOne.Contents;
    using EasyOne.Enumerations;
    using EasyOne.ExtendedControls;
    using EasyOne.Model.Contents;
    using EasyOne.Web.UI;
    using System;
    using System.Data;
    using System.Web.UI.WebControls;

    public partial class ContentManageNavigation : BaseUserControl
    {

        protected void ELnkArchiving_Click(object sender, EventArgs e)
        {
            BaseUserControl.ResponseRedirect("ContentArchivingManage.aspx?NodeID=" + BaseUserControl.RequestInt32("NodeID").ToString() + "&Status=102&ListType=0");
        }

        protected void ELnkCheckContent_Click(object sender, EventArgs e)
        {
            BaseUserControl.ResponseRedirect("ContentManage.aspx?NodeID=" + BaseUserControl.RequestInt32("NodeID").ToString() + "&Status=101&ListType=0");
        }

        protected void ELnkContentList_Click(object sender, EventArgs e)
        {
            BaseUserControl.ResponseRedirect("ContentManage.aspx?NodeID=" + BaseUserControl.RequestInt32("NodeID").ToString());
        }

        protected void ELnkContentRecycle_Click(object sender, EventArgs e)
        {
            BaseUserControl.ResponseRedirect("ContentRecycle.aspx?NodeID=" + BaseUserControl.RequestInt32("NodeID").ToString());
        }

        protected void ELnkHtmlManage_Click(object sender, EventArgs e)
        {
            int num = BaseUserControl.RequestInt32("NodeID");
            if (num > 0)
            {
                BaseUserControl.ResponseRedirect("ContentHtml.aspx?NodeID=" + num.ToString());
            }
            else
            {
                BaseUserControl.ResponseRedirect("ContentHtml.aspx");
            }
        }

        protected void ELnkReplaceManage_Click(object sender, EventArgs e)
        {
            BaseUserControl.ResponseRedirect("ContentBatchModfiy.aspx");
        }

        protected void ELnkSiginManage_Click(object sender, EventArgs e)
        {
            BaseUserControl.ResponseRedirect("ContentSignin.aspx?NodeID=" + BaseUserControl.RequestInt32("NodeID").ToString());
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            string basePath = "";
            basePath = base.BasePath;
            basePath = this.Page.Request.Url.Scheme + "://" + this.Page.Request.Url.Authority + basePath;
            this.Adrp.DropArrowImageUrl = basePath + "Admin/Images/sitelist.gif";
            int nodeId = BaseUserControl.RequestInt32("NodeID");
            bool isSuperAdmin = PEContext.Current.Admin.IsSuperAdmin;
            bool flag2 = false;
            bool flag3 = false;
            bool flag4 = false;
            if (!isSuperAdmin)
            {
                string roleNodeId = RolePermissions.GetRoleNodeId(PEContext.Current.Admin.Roles, OperateCode.NodeContentInput);
                string checkStr = RolePermissions.GetRoleNodeId(PEContext.Current.Admin.Roles, OperateCode.NodeContentCheck);
                string str4 = RolePermissions.GetRoleNodeId(PEContext.Current.Admin.Roles, OperateCode.NodeContentManage);
                if (nodeId > 0)
                {
                    string findStr = nodeId.ToString();
                    NodeInfo cacheNodeById = EasyOne.Contents.Nodes.GetCacheNodeById(nodeId);
                    if (cacheNodeById.IsNull)
                    {
                        BaseUserControl.WriteErrMsg("当前栏目不存在，可能被删除了请返回！");
                    }
                    if (cacheNodeById.ParentId > 0)
                    {
                        findStr = findStr + "," + cacheNodeById.ParentPath;
                    }
                    flag3 = StringHelper.FoundCharInArr(checkStr, findStr);
                    flag4 = StringHelper.FoundCharInArr(str4, findStr);
                    flag2 = StringHelper.FoundCharInArr(roleNodeId, findStr);
                }
                else
                {
                    flag3 = RolePermissions.AccessCheckNodePermission(OperateCode.NodeContentCheck, -1);
                    flag4 = RolePermissions.AccessCheckNodePermission(OperateCode.NodeContentManage, -1);
                    flag2 = RolePermissions.AccessCheckNodePermission(OperateCode.NodeContentInput, -1);
                }
                if (!flag2)
                {
                    this.LblAddContent.Enabled = false;
                }
                if (!flag4)
                {
                    this.ELnkContentList.Enabled = false;
                    this.ELnkContentRecycle.Enabled = false;
                    this.ELnkHtmlManage.Enabled = false;
                    this.ELnkSiginManage.Enabled = false;
                    this.ELnkArchiving.Enabled = false;
                }
                if (!flag3)
                {
                    this.ELnkCheckContent.Enabled = false;
                }
            }
            else
            {
                this.ELnkReplace.Visible = true;
            }
            if ((nodeId > 0) && ((flag2 || flag4) || isSuperAdmin))
            {
                DataTable contentModelListByNodeId = ModelManager.GetContentModelListByNodeId(nodeId, true);
                if (contentModelListByNodeId.Rows.Count <= 1)
                {
                    this.LblAddContent.Visible = false;
                }
                else
                {
                    this.RptModelList.DataSource = contentModelListByNodeId;
                    this.RptModelList.DataBind();
                }
                if (contentModelListByNodeId.Rows.Count == 1)
                {
                    this.HlkModel.Visible = true;
                    string str6 = "";
                    str6 = string.Concat(new object[] { "~/Admin/Contents/", contentModelListByNodeId.Rows[0]["AddInfoFilePath"].ToString(), "?Action=add&modelId=", contentModelListByNodeId.Rows[0]["ModelId"].ToString(), "&NodeID=", BaseUserControl.RequestInt32("NodeID") });
                    this.HlkModel.NavigateUrl = str6;
                    this.HlkModel.Text = "添加" + contentModelListByNodeId.Rows[0]["ItemName"].ToString();
                }
                if (!this.LblAddContent.Visible && !this.HlkModel.Visible)
                {
                    this.LitSeparator.Visible = false;
                }
                NodeInfo info2 = EasyOne.Contents.Nodes.GetCacheNodeById(nodeId);
                if (!info2.IsNull && info2.IsCreateContentPage)
                {
                    this.ELnkHtmlManage.Enabled = true;
                }
                else
                {
                    this.ELnkHtmlManage.Enabled = false;
                }
            }
            else
            {
                this.LblAddContent.Visible = false;
                this.HlkModel.Visible = false;
                this.LitSeparator.Visible = false;
            }
        }
    }
}

