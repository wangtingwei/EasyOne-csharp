namespace EasyOne.WebSite
{
    using EasyOne.AccessManage;
    using EasyOne.Common;
    using EasyOne.Contents;
    using EasyOne.Enumerations;
    using EasyOne.Model.Contents;
    using EasyOne.Model.TemplateProc;
    using EasyOne.Templates;
    using EasyOne.Web.UI;
    using System;
    using System.Collections.Specialized;
    using System.Text;
    using System.Web;

    public partial class Category : TemplatePage
    {
        private int m_Page;
        private NodeInfo nodeInfo;

        private void CheckIsAuthenticated()
        {
            if (!HttpContext.Current.User.Identity.IsAuthenticated)
            {
                TemplatePage.WriteErrMsg("该页需要登录才能查看！", base.BasePath + "User/Login.aspx");
            }
        }

        public override void OnInitTemplateInfo(EventArgs e)
        {
            string containChildTemplateFile = "";
            if (this.m_Page >= 1)
            {
                containChildTemplateFile = this.nodeInfo.ContainChildTemplateFile;
            }
            else if (string.IsNullOrEmpty(this.nodeInfo.DefaultTemplateFile))
            {
                containChildTemplateFile = this.nodeInfo.ContainChildTemplateFile;
            }
            else
            {
                containChildTemplateFile = this.nodeInfo.DefaultTemplateFile;
            }
            if (!string.IsNullOrEmpty(containChildTemplateFile))
            {
                NameValueCollection queryString = base.Request.QueryString;
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < queryString.Count; i++)
                {
                    if ((queryString.GetKey(i) != "id") && (queryString.GetKey(i) != "page"))
                    {
                        builder.Append(queryString.GetKey(i));
                        builder.Append("=");
                        builder.Append(DataSecurity.UrlEncode(queryString[i]));
                        if (i != (queryString.Count - 1))
                        {
                            builder.Append("&");
                        }
                    }
                }
                TemplateInfo info = new TemplateInfo();
                info.QueryList = base.Request.QueryString;
                info.PageName = "index_{$pageid/}.aspx";
                if (!string.IsNullOrEmpty(builder.ToString()))
                {
                    info.PageName = info.PageName + "?" + builder.ToString();
                }
                info.TemplateContent = Template.GetTemplateContent(containChildTemplateFile);
                info.RootPath = HttpContext.Current.Request.PhysicalApplicationPath;
                info.CurrentPage = this.m_Page;
                info.IsDynamicPage = true;
                info.PageType = 1;
                base.TemplateInfo = info;
            }
            else
            {
                TemplatePage.WriteErrMsg("您查看的栏目未设置模板！");
            }
        }

        public override void OnInitTemplatePage(EventArgs e)
        {
            this.m_Page = BasePage.RequestInt32("page", 0);
            int nodeId = BasePage.RequestInt32("id");
            if (nodeId <= 0)
            {
                TemplatePage.WriteErrMsg("您访问的栏目不存在");
            }
            else
            {
                this.nodeInfo = Nodes.GetCacheNodeById(nodeId);
                if (this.nodeInfo.IsNull)
                {
                    TemplatePage.WriteErrMsg("您访问的栏目不存在！");
                }
                else
                {
                    switch (this.nodeInfo.PurviewType)
                    {
                        case 2:
                            this.CheckIsAuthenticated();
                            if (!UserPermissions.AccessCheck(OperateCode.NodeContentPreview, nodeId))
                            {
                                TemplatePage.WriteErrMsg("您没有查看该页的权限，请与网站管理员联系");
                            }
                            break;
                    }
                    if ((this.m_Page == 0) || (this.m_Page == 1))
                    {
                        base.IsCache = true;
                        base.CacheKey = "CK_Page_Category_" + base.Request.QueryString["id"];
                        base.CacheTime = 12 * this.nodeInfo.Settings.CacheTime;
                    }
                }
            }
        }
    }
}

