namespace EasyOne.WebSite
{
    using EasyOne.Common;
    using EasyOne.Contents;
    using EasyOne.Model.Contents;
    using EasyOne.Model.TemplateProc;
    using EasyOne.Templates;
    using EasyOne.Web.UI;
    using System;
    using System.Web;

    public partial class Default : TemplatePage
    {
        /// <summary>
        /// 基类事件方法  初始模板信息
        /// </summary>
        /// <param name="e"></param>
        public override void OnInitTemplateInfo(EventArgs e)
        {
            //创建首页页节点对象
            NodeInfo cacheNodeById = Nodes.GetCacheNodeById(-2);
            if (cacheNodeById.IsCreateListPage)
            {///将正斜杠 (/) 追加到虚拟路径的末尾（如果尚不存在正斜杠）
                BasePage.ResponseRedirect(VirtualPathUtility.AppendTrailingSlash(base.Request.ApplicationPath) + cacheNodeById.ListHtmlPageName("0"));
            }
            //创建模板对象
            TemplateInfo info2 = new TemplateInfo();
            info2.QueryList = base.Request.QueryString;
            info2.PageName = TemplatePage.RebuildPageName(base.Request.Url.LocalPath, base.Request.QueryString);
            if (cacheNodeById.IsNull)
            {
                TemplatePage.WriteErrMsg("首页节点未找到");
            }
            info2.TemplateContent = Template.GetTemplateContent(cacheNodeById.DefaultTemplateFile);
            info2.RootPath = HttpContext.Current.Request.PhysicalApplicationPath;
            info2.CurrentPage = DataConverter.CLng(base.Request.QueryString["page"], 1);
            info2.IsDynamicPage = true;
            //设置其模板信息
            base.TemplateInfo = info2;
        }
    }
}

