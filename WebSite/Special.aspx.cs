namespace EasyOne.WebSite
{
    using EasyOne.Common;
    using EasyOne.Contents;
    using EasyOne.Model.Contents;
    using EasyOne.Model.TemplateProc;
    using EasyOne.Templates;
    using EasyOne.Web.UI;
    using System;
    using System.Collections.Specialized;
    using System.Text;
    using System.Web;

    public partial class SpecialIndex : TemplatePage
    {
        private SpecialCategoryInfo specialCategoryInfo;
        private SpecialInfo specialInfo;

        public override void OnInitTemplateInfo(EventArgs e)
        {
            string str = BasePage.RequestStringToLower("action");
            string dynamicConfigTemplatePath = "";
            string str4 = str;
            if (str4 != null)
            {
                if (!(str4 == "special"))
                {
                    if (str4 == "specialcategory")
                    {
                        if (this.specialCategoryInfo.IsNull)
                        {
                            TemplatePage.WriteErrMsg("您查看的专题类别不存在！", base.BasePath + "Default.aspx");
                        }
                        dynamicConfigTemplatePath = this.specialCategoryInfo.SpecialTemplatePath;
                        goto Label_00AF;
                    }
                }
                else
                {
                    if (this.specialInfo.IsNull)
                    {
                        TemplatePage.WriteErrMsg("您查看的专题不存在！", base.BasePath + "Default.aspx");
                    }
                    dynamicConfigTemplatePath = this.specialInfo.SpecialTemplatePath;
                    goto Label_00AF;
                }
            }
            string fileName = "Special";
            dynamicConfigTemplatePath = TemplatePage.GetDynamicConfigTemplatePath(fileName);
        Label_00AF:
            if (!string.IsNullOrEmpty(dynamicConfigTemplatePath))
            {
                TemplateInfo info = new TemplateInfo();
                info.QueryList = base.Request.QueryString;
                NameValueCollection queryString = base.Request.QueryString;
                StringBuilder builder = new StringBuilder();
                if (queryString.Count > 3)
                {
                    for (int i = 2; i < queryString.Count; i++)
                    {
                        builder.Append(queryString[i]);
                        builder.Append("&");
                    }
                }
                info.PageName = "index_{$pageid/}.aspx";
                if (!string.IsNullOrEmpty(builder.ToString()))
                {
                    info.PageName = info.PageName + "?" + builder.ToString();
                }
                info.PageType = 1;
                info.TemplateContent = Template.GetTemplateContent(dynamicConfigTemplatePath);
                info.RootPath = HttpContext.Current.Request.PhysicalApplicationPath;
                info.CurrentPage = DataConverter.CLng(base.Request.QueryString["page"], 1);
                info.IsDynamicPage = true;
                base.TemplateInfo = info;
            }
            else
            {
                TemplatePage.WriteErrMsg("您查看的专题未设置模板！", base.BasePath + "Default.aspx");
            }
        }

        public override void OnInitTemplatePage(EventArgs e)
        {
            int specialId = BasePage.RequestInt32("id");
            switch (BasePage.RequestStringToLower("action"))
            {
                case "special":
                    this.specialInfo = Special.GetSpecialInfoById(specialId);
                    break;

                case "specialcategory":
                    this.specialCategoryInfo = Special.GetSpecialCategoryInfoById(specialId);
                    break;
            }
        }
    }
}

