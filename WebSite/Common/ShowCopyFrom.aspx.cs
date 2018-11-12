namespace EasyOne.WebSite
{
    using EasyOne.Accessories;
    using EasyOne.Common;
    using EasyOne.Components;
    using EasyOne.Model.TemplateProc;
    using EasyOne.Templates;
    using EasyOne.Web.UI;
    using System;
    using System.Collections.Specialized;
    using System.IO;
    using System.Web;

    public partial class ShowCopyFrom : TemplatePage
    {
        public override void OnInitTemplateInfo(EventArgs e)
        {
            string sourceName = BasePage.RequestString("copyfrom");
            if (!Source.ExistsPassedSource(sourceName))
            {
                TemplatePage.WriteErrMsg("指定的来源不存在！");
            }
            string dynamicConfigTemplatePath = TemplatePage.GetDynamicConfigTemplatePath(Path.GetFileNameWithoutExtension(this.Page.Request.FilePath));
            NameValueCollection values = new NameValueCollection();
            values.Add("copyfrom", DataSecurity.FilterBadChar(sourceName));
            if (!string.IsNullOrEmpty(dynamicConfigTemplatePath))
            {
                TemplateInfo info = new TemplateInfo();
                info.QueryList = values;
                info.PageName = TemplatePage.RebuildPageName(base.Request.Url.LocalPath, base.Request.QueryString);
                info.TemplateContent = Template.GetTemplateContent(dynamicConfigTemplatePath);
                info.RootPath = HttpContext.Current.Request.PhysicalApplicationPath;
                info.CurrentPage = DataConverter.CLng(base.Request.QueryString["page"], 1);
                info.PageType = 1;
                base.TemplateInfo = info;
            }
            else
            {
                TemplatePage.WriteErrMsg("您查看的来源未设置模板！", SiteConfig.SiteInfo.VirtualPath + "Default.aspx");
            }
        }
    }
}

