namespace EasyOne.WebSite
{
    using EasyOne.Accessories;
    using EasyOne.Common;
    using EasyOne.Model.TemplateProc;
    using EasyOne.Templates;
    using EasyOne.Web.UI;
    using System;
    using System.Web;

    public partial class Tag : TemplatePage
    {
        public override void OnInitTemplateInfo(EventArgs e)
        {
            TemplateInfo info = new TemplateInfo();
            string keywordText = DataSecurity.FilterBadChar(BasePage.RequestString("keyword"));
            if (Keywords.Exists(keywordText))
            {
                Keywords.UpdateHitsByKeywordName(keywordText);
            }
            info.QueryList = base.Request.QueryString;
            info.PageName = TemplatePage.RebuildPageName(base.Request.Url.LocalPath, base.Request.QueryString);
            string path = "";
            path = "/其他模板/Tag标签页模板.html";
            info.TemplateContent = Template.GetTemplateContent(path);
            info.RootPath = HttpContext.Current.Request.PhysicalApplicationPath;
            info.CurrentPage = DataConverter.CLng(base.Request.QueryString["page"], 1);
            info.IsDynamicPage = true;
            info.PageType = 1;
            base.TemplateInfo = info;
        }
    }
}

