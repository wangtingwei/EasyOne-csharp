namespace EasyOne.WebSite
{
    using EasyOne.Common;
    using EasyOne.Components;
    using EasyOne.Model.TemplateProc;
    using EasyOne.Templates;
    using EasyOne.Web.UI;
    using System;
    using System.Globalization;
    using System.Text;
    using System.Web;

    public partial class Wap : DynamicPage
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            base.Response.Clear();
            base.Response.Buffer = true;
            base.Response.Charset = "utf-8";
            base.Response.AddHeader("contenttype", "text/vnd.wap.wml; charset=utf-8");
            base.Response.ContentEncoding = Encoding.GetEncoding("utf-8");
            base.Response.ContentType = "text/vnd.wap.wml; charset=utf-8";
            TemplateInfo templateInfo = new TemplateInfo();
            templateInfo.QueryList = base.Request.QueryString;
            templateInfo.PageName = DynamicPage.RebuildPageName(base.Request.Url.LocalPath, base.Request.QueryString);
            string path = "/其他模板/默认wap页模板.html";
            foreach (FrontTemplate template in SiteConfig.FrontTemplateList)
            {
                if (string.Compare(this.PageFileName, template.Key, true, CultureInfo.CurrentCulture) == 0)
                {
                    path = template.Value;
                    break;
                }
            }
            templateInfo.TemplateContent = Template.GetTemplateContent(path);
            templateInfo.RootPath = HttpContext.Current.Request.PhysicalApplicationPath;
            templateInfo.CurrentPage = DataConverter.CLng(base.Request.QueryString["page"], 1);
            templateInfo = TemplateTransform.GetHtml(templateInfo);
            base.Response.Write(templateInfo.TemplateContent);
            base.Response.End();
        }
    }
}

