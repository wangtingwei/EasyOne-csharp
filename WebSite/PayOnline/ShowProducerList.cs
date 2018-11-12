namespace EasyOne.WebSite.Shop
{
    using EasyOne.Common;
    using EasyOne.Model.TemplateProc;
    using EasyOne.Templates;
    using EasyOne.Web.UI;
    using System;
    using System.Web;
    using System.Web.UI;

    public class ShowProducerList : DynamicPage
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
        }

        protected override void Render(HtmlTextWriter writer)
        {
            string templatePath = base.TemplatePath;
            if (!string.IsNullOrEmpty(templatePath))
            {
                TemplateInfo templateInfo = new TemplateInfo();
                templateInfo.QueryList = base.Request.QueryString;
                templateInfo.PageName = DynamicPage.RebuildPageName(base.Request.Url.LocalPath, base.Request.QueryString);
                templateInfo.TemplateContent = Template.GetTemplateContent(templatePath);
                templateInfo.RootPath = HttpContext.Current.Request.PhysicalApplicationPath;
                templateInfo.CurrentPage = DataConverter.CLng(base.Request.QueryString["page"], 1);
                templateInfo.PageType = 1;
                templateInfo = TemplateTransform.GetHtml(templateInfo);
                writer.Write(templateInfo.TemplateContent);
            }
            else
            {
                DynamicPage.WriteErrMsg("您查看的品牌列表页未设置模板！", "Default.aspx");
            }
        }
    }
}

