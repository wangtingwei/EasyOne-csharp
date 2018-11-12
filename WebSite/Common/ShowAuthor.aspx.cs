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

    public partial class ShowAuthor : TemplatePage
    {
        public override void OnInitTemplateInfo(EventArgs e)
        {
            string str = BasePage.RequestString("updatetime");
            DateTime? nullable = null;
            if (!string.IsNullOrEmpty(str))
            {
                nullable = new DateTime?(DataConverter.CDate(str));
            }
            string dynamicConfigTemplatePath = TemplatePage.GetDynamicConfigTemplatePath(Path.GetFileNameWithoutExtension(this.Page.Request.FilePath));
            NameValueCollection values = new NameValueCollection();
            values.Add("authorname", DataSecurity.FilterBadChar(BasePage.RequestString("authorname")));
            if (!nullable.HasValue)
            {
                values.Add("updatetime", string.Empty);
            }
            else
            {
                values.Add("updatetime", nullable.Value.ToString("yyyy-MM-dd"));
            }
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
                TemplatePage.WriteErrMsg("您查看的作者未设置模板！", SiteConfig.SiteInfo.VirtualPath + "Default.aspx");
            }
        }

        public override void OnInitTemplatePage(EventArgs e)
        {
            if (!Author.ExistsPassedAuthor(BasePage.RequestString("authorname")))
            {
                TemplatePage.WriteErrMsg("您要查看的作者不存在！", SiteConfig.SiteInfo.VirtualPath + "Default.aspx");
            }
        }
    }
}

