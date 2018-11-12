namespace EasyOne.WebSite.Admin.Contents
{
    using EasyOne.Common;
    using EasyOne.Components;
    using EasyOne.Controls;
    using EasyOne.Model.TemplateProc;
    using EasyOne.Templates;
    using EasyOne.Web.UI;
    using System;
    using System.Web;
    using System.Web.UI.WebControls;

    public partial class SiteOtherCreate : AdminPage
    {

        protected void BtnBaidu_Click(object sender, EventArgs e)
        {
            TemplateInfo templateInfo = new TemplateInfo();
            templateInfo.QueryList = base.Request.QueryString;
            string path = "/其他模板/BaiDu地图模板.html";
            templateInfo.PageName = "BaiduSiteMap_Index.xml";
            templateInfo.IsDynamicPage = false;
            templateInfo.CurrentPage = DataConverter.CLng(base.Request.QueryString["page"], 1);
            templateInfo.RootPath = base.Request.PhysicalApplicationPath;
            templateInfo.SiteUrl = SiteConfig.SiteInfo.SiteUrl;
            templateInfo.TemplateContent = Template.GetTemplateContent(path);
            TemplateTransform.GetHtml(templateInfo);
            FileSystemObject.WriteFile(VirtualPathUtility.AppendTrailingSlash(templateInfo.RootPath) + SiteConfig.SiteOption.CreateHtmlPath + templateInfo.PageName, TemplateTransform.GetHtml(templateInfo).TemplateContent);
            if (templateInfo.PageNum > 1)
            {
                int pageNum = templateInfo.PageNum;
                for (int i = 2; i < pageNum; i++)
                {
                    templateInfo.QueryList = base.Request.QueryString;
                    path = "/其他模板/BaiDu地图模板.html";
                    templateInfo.PageName = "BaiduSiteMap_Index_" + i.ToString() + ".xml";
                    templateInfo.IsDynamicPage = false;
                    templateInfo.CurrentPage = i;
                    templateInfo.RootPath = base.Request.PhysicalApplicationPath;
                    templateInfo.SiteUrl = SiteConfig.SiteInfo.SiteUrl;
                    templateInfo.TemplateContent = Template.GetTemplateContent(path);
                    TemplateTransform.GetHtml(templateInfo);
                    FileSystemObject.WriteFile(VirtualPathUtility.AppendTrailingSlash(templateInfo.RootPath) + SiteConfig.SiteOption.CreateHtmlPath + templateInfo.PageName, TemplateTransform.GetHtml(templateInfo).TemplateContent);
                }
            }
            this.HypBaidMap.Visible = true;
            this.HypBaidMap.Text = "生成成功，点击提交地图！";
            this.HypBaidMap.NavigateUrl = "http://news.baidu.com/newsop.html";
        }

        protected void BtnGoogle_Click(object sender, EventArgs e)
        {
            TemplateInfo templateInfo = new TemplateInfo();
            templateInfo.QueryList = base.Request.QueryString;
            string path = "/其他模板/Google地图模板.html";
            templateInfo.PageName = "GoogleSiteMap_Index.xml";
            templateInfo.IsDynamicPage = false;
            templateInfo.CurrentPage = DataConverter.CLng(base.Request.QueryString["page"], 1);
            templateInfo.RootPath = base.Request.PhysicalApplicationPath;
            templateInfo.SiteUrl = SiteConfig.SiteInfo.SiteUrl;
            templateInfo.TemplateContent = Template.GetTemplateContent(path);
            TemplateTransform.GetHtml(templateInfo);
            FileSystemObject.WriteFile(VirtualPathUtility.AppendTrailingSlash(base.Request.PhysicalApplicationPath) + SiteConfig.SiteOption.CreateHtmlPath + templateInfo.PageName, TemplateTransform.GetHtml(templateInfo).TemplateContent);
            if (templateInfo.PageNum > 1)
            {
                int pageNum = templateInfo.PageNum;
                for (int i = 2; i < pageNum; i++)
                {
                    templateInfo.QueryList = base.Request.QueryString;
                    path = "/其他模板/Google地图模板.html";
                    templateInfo.PageName = "GoogleSiteMap_Index_" + i.ToString() + ".xml";
                    templateInfo.IsDynamicPage = false;
                    templateInfo.CurrentPage = i;
                    templateInfo.RootPath = base.Request.PhysicalApplicationPath;
                    templateInfo.SiteUrl = SiteConfig.SiteInfo.SiteUrl;
                    templateInfo.TemplateContent = Template.GetTemplateContent(path);
                    TemplateTransform.GetHtml(templateInfo);
                    FileSystemObject.WriteFile(VirtualPathUtility.AppendTrailingSlash(base.Request.PhysicalApplicationPath) + SiteConfig.SiteOption.CreateHtmlPath + templateInfo.PageName, TemplateTransform.GetHtml(templateInfo).TemplateContent);
                }
            }
            this.HypGoogleMap.Visible = true;
            this.HypGoogleMap.Text = "生成成功，点击提交地图！";
            this.HypGoogleMap.NavigateUrl = "http://www.google.com/webmasters/sitemaps/ping?sitemap=" + templateInfo.SiteUrl + "/" + templateInfo.PageName;
        }

        protected void BtnRss_Click(object sender, EventArgs e)
        {
            TemplateInfo templateInfo = new TemplateInfo();
            templateInfo.QueryList = base.Request.QueryString;
            string path = string.Empty;
            foreach (FrontTemplate template in SiteConfig.FrontTemplateList)
            {
                if (string.Compare("RssIndex", template.Key, StringComparison.OrdinalIgnoreCase) == 0)
                {
                    path = template.Value;
                    break;
                }
            }
            templateInfo.PageName = "Rss.xml";
            templateInfo.CurrentPage = 1;
            templateInfo.RootPath = base.Request.PhysicalApplicationPath;
            templateInfo.SiteUrl = SiteConfig.SiteInfo.SiteUrl;
            templateInfo.IsDynamicPage = false;
            templateInfo.TemplateContent = Template.GetTemplateContent(path, base.Request.PhysicalApplicationPath);
            TemplateTransform.GetHtml(templateInfo);
            FileSystemObject.WriteFile(VirtualPathUtility.AppendTrailingSlash(base.Request.PhysicalApplicationPath) + SiteConfig.SiteOption.CreateHtmlPath + "Rss/Rss.xml", TemplateTransform.GetHtml(templateInfo).TemplateContent);
            AdminPage.WriteSuccessMsg("生成成功！");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                this.BtnRss.Enabled = SiteConfig.SiteOption.RssEnable;
                this.PanelForm.Visible = true;
                this.PanelMsg.Visible = false;
            }
        }
    }
}

