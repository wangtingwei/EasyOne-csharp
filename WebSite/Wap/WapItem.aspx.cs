namespace EasyOne.WebSite
{
    using EasyOne.Common;
    using EasyOne.CommonModel;
    using EasyOne.Components;
    using EasyOne.Contents;
    using EasyOne.Model.CommonModel;
    using EasyOne.Model.Contents;
    using EasyOne.Model.TemplateProc;
    using EasyOne.Templates;
    using EasyOne.Web.UI;
    using System;
    using System.Globalization;
    using System.Text;
    using System.Web;

    public partial class WapItem : DynamicPage
    {
        private int m_GeneralId;
        private CommonModelInfo m_ItemInfo;
        private ModelInfo m_ModelInfo;

        protected void Page_PreRender(object sender, EventArgs e)
        {
            base.Response.Clear();
            base.Response.Buffer = true;
            base.Response.Charset = "utf-8";
            base.Response.AddHeader("contenttype", "text/vnd.wap.wml");
            base.Response.ContentEncoding = Encoding.GetEncoding("utf-8");
            base.Response.ContentType = "text/vnd.wap.wml";
            this.m_GeneralId = BasePage.RequestInt32("id");
            if (this.m_GeneralId <= 0)
            {
                base.Response.End();
            }
            this.m_ItemInfo = ContentManage.GetCommonModelInfoById(this.m_GeneralId);
            if (this.m_ItemInfo.IsNull)
            {
                base.Response.End();
            }
            else
            {
                this.m_ModelInfo = ModelManager.GetModelInfoById(this.m_ItemInfo.ModelId);
                if (!this.m_ModelInfo.IsEshop && (this.m_ItemInfo.Status != 0x63))
                {
                    base.Response.End();
                }
            }
            TemplateInfo templateInfo = new TemplateInfo();
            templateInfo.QueryList = base.Request.QueryString;
            templateInfo.PageName = DynamicPage.RebuildPageName(base.Request.Url.LocalPath, base.Request.QueryString);
            string path = "/其他模板/Wap内容页模板.html";
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

