namespace EasyOne.WebSite
{
    using EasyOne.AccessManage;
    using EasyOne.Common;
    using EasyOne.CommonModel;
    using EasyOne.Components;
    using EasyOne.Contents;
    using EasyOne.Enumerations;
    using EasyOne.Model.CommonModel;
    using EasyOne.Model.Contents;
    using EasyOne.Model.TemplateProc;
    using EasyOne.Templates;
    using EasyOne.Web.UI;
    using System;
    using System.Web;

    public partial class Print : TemplatePage
    {
        private int m_GeneralId;
        private CommonModelInfo m_ItemInfo;
        private NodeInfo m_NodeInfo;
        private ModelInfo m_ModelInfo;
        private void CheckIsAuthenticated()
        {
            if (!PEContext.Current.User.Identity.IsAuthenticated)
            {
                TemplatePage.WriteErrMsg("该页需要登录才能查看！", "User/Login.aspx");
            }
        }

        private void CheckPermission()
        {
            int permissionType;
            ContentPermissionInfo contentPermissionInfoById = PermissionContent.GetContentPermissionInfoById(this.m_GeneralId);
            if (contentPermissionInfoById == null)
            {
                permissionType = 0;
            }
            else
            {
                permissionType = contentPermissionInfoById.PermissionType;
            }
            switch (permissionType)
            {
                case 0:
                    switch (this.m_NodeInfo.PurviewType)
                    {
                        case 1:
                            this.CheckIsAuthenticated();
                            return;

                        case 2:
                            this.CheckIsAuthenticated();
                            if (UserPermissions.AccessCheck(OperateCode.NodeContentPreview, this.m_NodeInfo.NodeId))
                            {
                                break;
                            }
                            TemplatePage.WriteErrMsg("您没有查看该页的权限，请与网站管理员联系", "Default.aspx");
                            return;
                    }
                    return;

                case 1:
                    this.CheckIsAuthenticated();
                    return;

                case 2:
                    this.CheckIsAuthenticated();
                    if (!StringHelper.FoundCharInArr(contentPermissionInfoById.ArrGroupId, PEContext.Current.User.GroupId.ToString()))
                    {
                        TemplatePage.WriteErrMsg("您没有查看该页的权限，请与网站管理员联系", "Default.aspx");
                    }
                    break;

                default:
                    return;
            }
        }

        public override void OnInitTemplateInfo(EventArgs e)
        {
            TemplateInfo info = new TemplateInfo();
            info.QueryList = base.Request.QueryString;
            info.PageName = TemplatePage.RebuildPageName(base.Request.Url.LocalPath, base.Request.QueryString);
            info.IsDynamicPage = true;
            info.PageType = 0;
            info.TemplateContent = Template.GetTemplateContent(this.m_ModelInfo.PrintTemplate);
            info.RootPath = HttpContext.Current.Request.PhysicalApplicationPath;
            info.CurrentPage = DataConverter.CLng(base.Request.QueryString["page"], 1);
            base.TemplateInfo = info;
        }

        public override void OnInitTemplatePage(EventArgs e)
        {
            this.m_GeneralId = BasePage.RequestInt32("id");
            if (this.m_GeneralId <= 0)
            {
                TemplatePage.WriteErrMsg("您访问的内容信息不存在", "Default.aspx");
            }
            this.m_ItemInfo = ContentManage.GetCommonModelInfoById(this.m_GeneralId);
            if (this.m_ItemInfo == null)
            {
                TemplatePage.WriteErrMsg("您访问的内容信息不存在", "Default.aspx");
            }
            this.m_NodeInfo = Nodes.GetCacheNodeById(this.m_ItemInfo.NodeId);
            this.m_ModelInfo = ModelManager.GetModelInfoById(this.m_ItemInfo.ModelId);
            if (this.m_ModelInfo.IsNull || string.IsNullOrEmpty(this.m_ModelInfo.PrintTemplate))
            {
                TemplatePage.WriteErrMsg("没有指定模型打印页模板", "Default.aspx");
            }
            this.CheckPermission();
        }
    }
}

