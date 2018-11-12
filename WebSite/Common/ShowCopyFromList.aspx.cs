namespace EasyOne.WebSite
{
    using EasyOne.Common;
    using EasyOne.Model.TemplateProc;
    using EasyOne.Templates;
    using EasyOne.Web.UI;
    using System;
    using System.IO;
    using System.Web;

    public partial class ShowCopyFromList : TemplatePage
    {
        public override void OnInitTemplateInfo(EventArgs e)
        {
            string dynamicConfigTemplatePath = TemplatePage.GetDynamicConfigTemplatePath(Path.GetFileNameWithoutExtension(this.Page.Request.FilePath));
            if (!string.IsNullOrEmpty(dynamicConfigTemplatePath))
            {
                TemplateInfo info = new TemplateInfo();
                info.QueryList = base.Request.QueryString;
                info.PageName = TemplatePage.RebuildPageName(base.Request.Url.LocalPath, base.Request.QueryString);
                info.TemplateContent = Template.GetTemplateContent(dynamicConfigTemplatePath);
                info.RootPath = HttpContext.Current.Request.PhysicalApplicationPath;
                info.CurrentPage = DataConverter.CLng(base.Request.QueryString["page"], 1);
                info.PageType = 1;
                base.TemplateInfo = info;
            }
            else
            {
                TemplatePage.WriteErrMsg("您查看的来源列表页未设置模板！", "Default.aspx");
            }
        }
    }
}

