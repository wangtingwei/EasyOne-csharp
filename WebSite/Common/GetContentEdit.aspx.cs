namespace EasyOne.WebSite.Common
{
    using EasyOne.AccessManage;
    using EasyOne.CommonModel;
    using EasyOne.Components;
    using EasyOne.Contents;
    using EasyOne.Enumerations;
    using EasyOne.Model.CommonModel;
    using EasyOne.Model.Contents;
    using EasyOne.Web.UI;
    using System;
    using System.Web.UI.HtmlControls;

    public partial class GetContentEdit : BasePage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            int generalId = BasePage.RequestInt32("itemid");
            if (PEContext.Current.Admin.Identity.IsAuthenticated)
            {
                CommonModelInfo commonModelInfoById = ContentManage.GetCommonModelInfoById(generalId);
                int nodeId = commonModelInfoById.NodeId;
                bool flag = false;
                if (PEContext.Current.Admin.IsSuperAdmin)
                {
                    flag = true;
                }
                else
                {
                    bool flag2 = false;
                    bool flag3 = false;
                    bool flag4 = false;
                    flag2 = RolePermissions.AccessCheckNodePermission(OperateCode.NodeContentCheck, -1) || RolePermissions.AccessCheckNodePermission(OperateCode.NodeContentCheck, nodeId);
                    flag3 = RolePermissions.AccessCheckNodePermission(OperateCode.NodeContentManage, -1) || RolePermissions.AccessCheckNodePermission(OperateCode.NodeContentManage, nodeId);
                    flag4 = RolePermissions.AccessCheckNodePermission(OperateCode.NodeContentInput, -1) || RolePermissions.AccessCheckNodePermission(OperateCode.NodeContentInput, nodeId);
                    if ((flag2 || flag4) || flag3)
                    {
                        flag = true;
                    }
                }
                if (flag)
                {
                    string manageDir = SiteConfig.SiteOption.ManageDir;
                    ModelInfo modelInfoById = ModelManager.GetModelInfoById(commonModelInfoById.ModelId);
                    if (modelInfoById.IsEshop)
                    {
                        this.Session["IndexRightUrl"] = string.Concat(new object[] { base.BasePath, manageDir, "/shop/", modelInfoById.AddInfoFilePath, "?Action=Modify&NodeID=", commonModelInfoById.NodeId.ToString(), "&GeneralID=", generalId, "&ModelID=", commonModelInfoById.ModelId.ToString(), "&LinkType=", commonModelInfoById.LinkType });
                    }
                    else
                    {
                        this.Session["IndexRightUrl"] = string.Concat(new object[] { base.BasePath, manageDir, "/Contents/", modelInfoById.AddInfoFilePath, "?Action=Modify&NodeID=", commonModelInfoById.NodeId.ToString(), "&GeneralID=", generalId, "&ModelID=", commonModelInfoById.ModelId.ToString(), "&LinkType=", commonModelInfoById.LinkType });
                    }
                    BasePage.ResponseRedirect("~/" + manageDir + "/Index.aspx");
                }
                else
                {
                    BasePage.ResponseRedirect("~/Item/" + generalId + ".aspx");
                }
            }
            else
            {
                BasePage.ResponseRedirect("~/Item/" + generalId + ".aspx");
            }
        }
    }
}

