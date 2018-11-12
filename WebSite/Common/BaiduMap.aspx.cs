namespace EasyOne.WebSite
{
    using EasyOne.Common;
    using EasyOne.Model.TemplateProc;
    using EasyOne.Templates;
    using EasyOne.Web.UI;
    using System;
    using System.Text;
    using System.Web;

    public partial class BaiduMap : TemplatePage
    {
        public override void OnInitTemplateInfo(EventArgs e)
        {
            TemplateInfo info = new TemplateInfo();
            info.QueryList = base.Request.QueryString;
            info.PageName = TemplatePage.RebuildPageName(base.Request.Url.LocalPath, base.Request.QueryString);
            info.TemplateContent = Template.GetTemplateContent("/其他模板/BaiDu地图模板.html");
            info.RootPath = HttpContext.Current.Request.PhysicalApplicationPath;
            info.CurrentPage = DataConverter.CLng(base.Request.QueryString["page"], 1);
            info.IsDynamicPage = true;
            base.TemplateInfo = info;
        }

        public override void OnInitTemplatePage(EventArgs e)
        {
            base.Response.Clear();
            base.Response.Buffer = true;
            base.Response.Charset = "utf-8";
            base.Response.AddHeader("contenttype", "text/xml");
            base.Response.ContentEncoding = Encoding.GetEncoding("utf-8");
            base.Response.ContentType = "text/xml";
        }
    }
}

