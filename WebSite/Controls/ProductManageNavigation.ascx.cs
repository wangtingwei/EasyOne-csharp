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

    public partial class ProductManageNavigation : BaseUserControl
    {

        protected void ELnkProductBatchModify_Click(object sender, EventArgs e)
        {
            BaseUserControl.ResponseRedirect("ProductBatchModify.aspx");
        }

        protected void ELnkProductHtml_Click(object sender, EventArgs e)
        {
            int num = BaseUserControl.RequestInt32("NodeID");
            if (num > 0)
            {
                BaseUserControl.ResponseRedirect("ProductHtml.aspx?NodeID=" + num.ToString());
            }
            else
            {
                BaseUserControl.ResponseRedirect("ProductHtml.aspx");
            }
        }

        protected void ELnkProductImport_Click(object sender, EventArgs e)
        {
            BaseUserControl.ResponseRedirect("ProductImport.aspx");
        }

        protected void ELnkProductList_Click(object sender, EventArgs e)
        {
            BaseUserControl.ResponseRedirect("ProductManage.aspx?NodeID=" + BaseUserControl.RequestInt32("NodeID").ToString());
        }

        protected void ELnkProductRecycle_Click(object sender, EventArgs e)
        {
            BaseUserControl.ResponseRedirect("ProductRecycle.aspx?NodeID=" + BaseUserControl.RequestInt32("NodeID").ToString());
        }

        protected void ELnkProductStockManage_Click(object sender, EventArgs e)
        {
            BaseUserControl.ResponseRedirect("StockManage.aspx");
        }

        protected void ELnkReplaceManage_Click(object sender, EventArgs e)
        {
            BaseUserControl.ResponseRedirect("ProductBatchModify.aspx?NodeID=" + BaseUserControl.RequestInt32("NodeID").ToString());
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
            if (!isSuperAdmin)
            {
                string roleNodeId = RolePermissions.GetRoleNodeId(PEContext.Current.Admin.Roles, OperateCode.NodeContentInput);
                string checkStr = RolePermissions.GetRoleNodeId(PEContext.Current.Admin.Roles, OperateCode.NodeContentManage);
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
                    flag2 = StringHelper.FoundCharInArr(roleNodeId, findStr);
                }
                else
                {
                    flag3 = RolePermissions.AccessCheckNodePermission(OperateCode.NodeContentManage, -1);
                    flag2 = RolePermissions.AccessCheckNodePermission(OperateCode.NodeContentInput, -1);
                }
                if (!flag2)
                {
                    this.LblAddProduct.Enabled = false;
                    this.ELnkProductImport.Enabled = false;
                }
                if (!flag3)
                {
                    this.ELnkContentList.Enabled = false;
                    this.ELnkProductRecycle.Enabled = false;
                    this.ELnkProductStockManage.Enabled = false;
                    this.ELnkProductBatchModify.Enabled = false;
                    this.ELnkProductHtml.Enabled = false;
                }
            }
            if ((nodeId > 0) && ((flag2 || flag3) || isSuperAdmin))
            {
                DataTable shopModelListByNodeId = ModelManager.GetShopModelListByNodeId(nodeId, true);
                if (shopModelListByNodeId.Rows.Count <= 1)
                {
                    this.LblAddProduct.Visible = false;
                }
                else
                {
                    this.RptModelList.DataSource = shopModelListByNodeId;
                    this.RptModelList.DataBind();
                }
                if (shopModelListByNodeId.Rows.Count == 1)
                {
                    this.HlkModel.Visible = true;
                    string str5 = "";
                    str5 = string.Concat(new object[] { "~/Admin/Shop/", shopModelListByNodeId.Rows[0]["AddInfoFilePath"].ToString(), "?Action=add&modelId=", shopModelListByNodeId.Rows[0]["ModelId"].ToString(), "&NodeID=", BaseUserControl.RequestInt32("NodeID") });
                    this.HlkModel.NavigateUrl = str5;
                    this.HlkModel.Text = "添加" + shopModelListByNodeId.Rows[0]["ItemName"].ToString();
                }
                if (!this.LblAddProduct.Visible && !this.HlkModel.Visible)
                {
                    this.LitSeparator.Visible = false;
                }
            }
            else
            {
                this.LblAddProduct.Visible = false;
                this.HlkModel.Visible = false;
                this.LitSeparator.Visible = false;
            }
        }
    }
}

