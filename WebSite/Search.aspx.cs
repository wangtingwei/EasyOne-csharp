namespace EasyOne.WebSite
{
    using EasyOne.Accessories;
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
    using System.Collections.Specialized;
    using System.Globalization;
    using System.Web;

    public partial class Search : TemplatePage
    {
        private string GetAdvanceTemplateFile(int modelId)
        {
            string str = "";
            ModelInfo cacheModelById = ModelManager.GetCacheModelById(modelId);
            if (!cacheModelById.IsNull)
            {
                if (!string.IsNullOrEmpty(cacheModelById.AdvanceSearchTemplate))
                {
                    return cacheModelById.AdvanceSearchTemplate;
                }
                TemplatePage.WriteErrMsg("指定的模型未设置模板！");
                return str;
            }
            TemplatePage.WriteErrMsg("找不到指定的模型！");
            return str;
        }

        private string GetAdvanceTemplateForm(int modelId)
        {
            string str = "";
            ModelInfo cacheModelById = ModelManager.GetCacheModelById(modelId);
            if (!cacheModelById.IsNull)
            {
                if (!string.IsNullOrEmpty(cacheModelById.AdvanceSearchFormTemplate))
                {
                    return cacheModelById.AdvanceSearchFormTemplate;
                }
                TemplatePage.WriteErrMsg("指定的模型未设置模板！");
                return str;
            }
            TemplatePage.WriteErrMsg("找不到指定的模型！");
            return str;
        }

        private string GetSpecialTemplate(int specialid, int specialcategoryid)
        {
            string str = "";
            if (specialcategoryid > 0)
            {
                SpecialCategoryInfo specialCategoryInfoById = Special.GetSpecialCategoryInfoById(specialcategoryid);
                if (!specialCategoryInfoById.IsNull)
                {
                    if (!string.IsNullOrEmpty(specialCategoryInfoById.SearchTemplatePath))
                    {
                        return specialCategoryInfoById.SearchTemplatePath;
                    }
                    TemplatePage.WriteErrMsg("没有设置专题类别模板！", "Special.aspx");
                    return str;
                }
                TemplatePage.WriteErrMsg("找不到指定的专题类别！", "Special.aspx");
                return str;
            }
            if (specialid > 0)
            {
                SpecialInfo specialInfoById = Special.GetSpecialInfoById(specialid);
                if (!specialInfoById.IsNull)
                {
                    if (!string.IsNullOrEmpty(specialInfoById.SearchTemplatePath))
                    {
                        return specialInfoById.SearchTemplatePath;
                    }
                    TemplatePage.WriteErrMsg("没有设置专题模板！", "Special.aspx");
                    return str;
                }
                TemplatePage.WriteErrMsg("找不到指定的专题！", "Special.aspx");
                return str;
            }
            TemplatePage.WriteErrMsg("没有设置任何专题模板！", "Special.aspx");
            return str;
        }

        private string GetTemplateFile(int modelId)
        {
            string str = "";
            ModelInfo cacheModelById = ModelManager.GetCacheModelById(modelId);
            if (!cacheModelById.IsNull)
            {
                if (!string.IsNullOrEmpty(cacheModelById.SearchTemplate))
                {
                    return cacheModelById.SearchTemplate;
                }
                TemplatePage.WriteErrMsg("指定的模型未设置模板！");
                return str;
            }
            TemplatePage.WriteErrMsg("找不到指定的模型！");
            return str;
        }

        private string GetTemplatePath(string fileName)
        {
            string str = string.Empty;
            string str2 = string.Empty;
            foreach (FrontTemplate template in SiteConfig.FrontTemplateList)
            {
                if (string.Compare(fileName, template.Key, true, CultureInfo.CurrentCulture) == 0)
                {
                    str = template.Value;
                }
                else if (string.Compare("DynamicPageDefault", template.Key, true, CultureInfo.CurrentCulture) == 0)
                {
                    str2 = template.Value;
                }
            }
            if (string.IsNullOrEmpty(str))
            {
                return str2;
            }
            return str;
        }

        public override void OnInitTemplateInfo(EventArgs e)
        {
            TemplateInfo info = new TemplateInfo();
            int num = BasePage.RequestInt32("searchtype");
            string str = DataSecurity.FilterBadChar(base.Request.QueryString["keyword"]);
            int modelId = BasePage.RequestInt32("ModelId");
            string dynamicConfigTemplatePath = "";
            NameValueCollection queryString = new NameValueCollection();
            switch (num)
            {
                case 0:
                    if (!string.IsNullOrEmpty(str))
                    {
                        this.SaveKeyword(str);
                    }
                    queryString = base.Request.QueryString;
                    dynamicConfigTemplatePath = TemplatePage.GetDynamicConfigTemplatePath("Search");
                    break;

                case 1:
                    if ((DataSecurity.FilterBadChar(base.Request.QueryString["fieldoption"]) == "keyword") && !string.IsNullOrEmpty(str))
                    {
                        this.SaveKeyword(str);
                    }
                    queryString = base.Request.QueryString;
                    dynamicConfigTemplatePath = this.GetTemplateFile(modelId);
                    break;

                case 2:
                    if (BasePage.RequestInt32("showtype") != 1)
                    {
                        dynamicConfigTemplatePath = this.GetAdvanceTemplateForm(modelId);
                        break;
                    }
                    queryString = base.Request.QueryString;
                    dynamicConfigTemplatePath = this.GetAdvanceTemplateFile(modelId);
                    break;

                case 3:
                {
                    string[] strArray = DataSecurity.FilterBadChar(base.Request.QueryString["specialid"]).Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries);
                    int specialid = DataConverter.CLng(strArray[1]);
                    int specialcategoryid = DataConverter.CLng(strArray[0]);
                    queryString.Add("specialid", specialid.ToString());
                    queryString.Add("specialcategoryid", specialcategoryid.ToString());
                    string str5 = DataSecurity.FilterBadChar(base.Request.QueryString["fieldoption"]);
                    queryString.Add("fieldoption", str5);
                    if ((str5 == "keyword") && !string.IsNullOrEmpty(str))
                    {
                        this.SaveKeyword(str);
                    }
                    queryString.Add("keyword", str);
                    dynamicConfigTemplatePath = this.GetSpecialTemplate(specialid, specialcategoryid);
                    break;
                }
                default:
                    queryString = base.Request.QueryString;
                    if (!string.IsNullOrEmpty(str))
                    {
                        this.SaveKeyword(str);
                    }
                    dynamicConfigTemplatePath = this.GetTemplatePath("Search");
                    break;
            }
            if (!string.IsNullOrEmpty(dynamicConfigTemplatePath))
            {
                info.QueryList = queryString;
                info.PageName = TemplatePage.RebuildPageName(base.Request.Url.LocalPath, base.Request.QueryString);
                info.TemplateContent = Template.GetTemplateContent(dynamicConfigTemplatePath);
                info.RootPath = HttpContext.Current.Request.PhysicalApplicationPath;
                info.CurrentPage = DataConverter.CLng(base.Request.QueryString["page"], 1);
                info.IsDynamicPage = true;
                info.PageType = 1;
                base.TemplateInfo = info;
            }
            else
            {
                TemplatePage.WriteErrMsg("全站搜索结果页未设置模板！", "Default.aspx");
            }
        }

        private void SaveKeyword(string keyword)
        {
            if (Keywords.Exists(keyword))
            {
                Keywords.UpdateHitsByKeywordName(keyword);
            }
        }
    }
}

