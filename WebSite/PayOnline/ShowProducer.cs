namespace EasyOne.WebSite.Shop
{
    using EasyOne.Common;
    using EasyOne.Components;
    using EasyOne.Model.TemplateProc;
    using EasyOne.Shop;
    using EasyOne.Templates;
    using EasyOne.Web.UI;
    using System;
    using System.Collections.Specialized;
    using System.Globalization;
    using System.IO;
    using System.Text;
    using System.Web;
    using System.Web.UI;

    public class ShowProducer : BasePage
    {
        private string GetTemplatePath()
        {
            string str = string.Empty;
            string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(this.Page.Request.FilePath);
            string str3 = string.Empty;
            foreach (FrontTemplate template in SiteConfig.FrontTemplateList)
            {
                if (string.Compare(fileNameWithoutExtension, template.Key, true, CultureInfo.CurrentCulture) == 0)
                {
                    str = template.Value;
                }
                else if (string.Compare("DynamicPageDefault", template.Key, true, CultureInfo.CurrentCulture) == 0)
                {
                    str3 = template.Value;
                }
            }
            if (string.IsNullOrEmpty(str))
            {
                return str3;
            }
            return str;
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
        }

        public static string RebuildPageName(string fileName, NameValueCollection query)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                return string.Empty;
            }
            string[] strArray = fileName.Split(new char[] { '/' });
            if (strArray.Length > 0)
            {
                fileName = strArray[strArray.Length - 1];
            }
            if (fileName.IndexOf('?') > 0)
            {
                fileName = fileName.Substring(0, fileName.IndexOf('?') - 1);
            }
            StringBuilder builder = new StringBuilder(fileName);
            if (query.Count > 0)
            {
                bool flag = false;
                for (int i = 0; i < query.Count; i++)
                {
                    if (i == 0)
                    {
                        builder.Append("?");
                    }
                    else
                    {
                        builder.Append("&");
                    }
                    if (query.GetKey(i) == "page")
                    {
                        builder.Append("page={$pageid/}");
                        flag = true;
                    }
                    else
                    {
                        builder.Append(query.GetKey(i) + "=" + DataSecurity.FilterBadChar(query.Get(i)));
                    }
                }
                if (!flag)
                {
                    if (builder.Length > fileName.Length)
                    {
                        builder.Append("&page={$pageid/}");
                    }
                    else
                    {
                        builder.Append("?page={$pageid/}");
                    }
                }
            }
            else
            {
                builder.Append("?page={$pageid/}");
            }
            return builder.ToString();
        }

        protected override void Render(HtmlTextWriter writer)
        {
            string producerName = BasePage.RequestStringToLower("producername");
            if (!Producer.ProducerNameExists(producerName))
            {
                WriteErrMsg("您要查看的厂商不存在！", SiteConfig.SiteInfo.VirtualPath + "Default.aspx");
            }
            string templatePath = this.GetTemplatePath();
            NameValueCollection values = new NameValueCollection();
            values.Add("producername", DataSecurity.FilterBadChar(producerName));
            if (!string.IsNullOrEmpty(templatePath))
            {
                TemplateInfo templateInfo = new TemplateInfo();
                templateInfo.QueryList = values;
                templateInfo.PageName = RebuildPageName(base.Request.Url.LocalPath, base.Request.QueryString);
                templateInfo.TemplateContent = Template.GetTemplateContent(templatePath);
                templateInfo.RootPath = HttpContext.Current.Request.PhysicalApplicationPath;
                templateInfo.CurrentPage = DataConverter.CLng(base.Request.QueryString["page"], 1);
                templateInfo.PageType = 1;
                templateInfo = TemplateTransform.GetHtml(templateInfo);
                writer.Write(templateInfo.TemplateContent);
            }
            else
            {
                WriteErrMsg("您查看的厂商未设置模板！", SiteConfig.SiteInfo.VirtualPath + "Default.aspx");
            }
        }

        public static void WriteErrMsg(string errorMessage)
        {
            WriteErrMsg(errorMessage, string.Empty);
        }

        public static void WriteErrMsg(string errorMessage, string returnurl)
        {
            HttpContext.Current.Items["ErrorMessage"] = errorMessage;
            HttpContext.Current.Items["ReturnUrl"] = returnurl;
            HttpContext.Current.Server.Transfer("~/Prompt/ShowError.aspx");
        }
    }
}

