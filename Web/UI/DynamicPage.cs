namespace EasyOne.Web.UI
{
    using EasyOne.Common;
    using EasyOne.Components;
    using EasyOne.Enumerations;
    using EasyOne.Model.TemplateProc;
    using EasyOne.Templates;
    using EasyOne.Web;
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Globalization;
    using System.IO;
    using System.Text.RegularExpressions;
    using System.Web;
    using System.Web.Configuration;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;

    public class DynamicPage : BasePage
    {
        private HtmlHead m_Header;
        private bool m_IsAddHeader;
        private string m_TemplateContent = string.Empty;
        private string m_TemplatePath = string.Empty;
        private static Regex s_RegexControl = new Regex(@"{PE\.Control\.([^/}]+)(/}|}([\s\S]*){/PE\.Control\.\1})", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        private static Regex s_RegexHead = new Regex(@"<head[^>]*>([\s\S]*)</head>", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        private static Regex s_RegexHtml = new Regex("(<html[^>]*>)", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        private static Regex s_RegexHtmlHead = new Regex(@"(<html[^>]*>[\s\S]*)<head[^>]*>[\s\S]*</head>", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        private static Regex s_RegexTitle = new Regex(@"<title>([\s\S]*)</title>", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        private const string StyleSheetThemeSessionName = "DynamicPage_StyleSheetTheme";
        private const string ThemesDirectoryName = "App_Themes";

        public DynamicPage()
        {
            base.Refreshed += new EventHandler(this.DynamicPage_Refreshed);
        }

        private void DynamicPage_Refreshed(object sender, EventArgs e)
        {
            throw new CustomException(PEExceptionType.RefreshedError);
        }

        private string GetTemplatePath()
        {
            string str = string.Empty;
            string pageFileName = this.PageFileName;
            string str3 = string.Empty;
            foreach (FrontTemplate template in SiteConfig.FrontTemplateList)
            {
                if (string.Compare(pageFileName, template.Key, true, CultureInfo.CurrentCulture) == 0)
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

        protected virtual void InitializeTemplate()
        {
            TemplateInfo templateInfo = new TemplateInfo();
            templateInfo.QueryList = base.Request.QueryString;
            templateInfo.PageName = RebuildPageName(base.Request.Url.LocalPath, base.Request.QueryString);
            if (string.IsNullOrEmpty(this.m_TemplatePath))
            {
                this.m_TemplatePath = this.GetTemplatePath();
            }
            templateInfo.TemplateContent = Template.GetTemplateContent(this.m_TemplatePath);
            templateInfo.RootPath = HttpContext.Current.Request.PhysicalApplicationPath;
            templateInfo.CurrentPage = DataConverter.CLng(base.Request.QueryString["page"], 1);
            templateInfo = TemplateTransform.GetHtml(templateInfo);
            this.TemplateContent = templateInfo.TemplateContent;
        }

        protected override void OnPreInit(EventArgs e)
        {
            this.InitializeTemplate();
            this.m_IsAddHeader = false;
            foreach (Control control in this.Controls)
            {
                HtmlHead head = control as HtmlHead;
                if (head != null)
                {
                    this.m_Header = head;
                    break;
                }
            }
            if (this.m_Header == null)
            {
                this.m_Header = new HtmlHead();
            }
            Match match = s_RegexHead.Match(this.m_TemplateContent);
            string input = "";
            if (match.Success)
            {
                input = match.Groups[1].Value;
                this.m_TemplateContent = s_RegexHtmlHead.Replace(this.m_TemplateContent, "$1{PE.Control.Header/}");
                Match match2 = s_RegexTitle.Match(input);
                if (match2.Success)
                {
                    input = input.Replace(match2.Value, "");
                    HtmlTemplateTitle child = new HtmlTemplateTitle();
                    if (!string.IsNullOrEmpty(match2.Groups[1].Value))
                    {
                        child.Template = match2.Groups[1].Value;
                    }
                    child.Text = this.m_Header.Title;
                    this.m_Header.InnerHtml = input;
                    this.m_Header.Controls.Add(child);
                }
                else
                {
                    HtmlTitle title2 = new HtmlTitle();
                    title2.Text = this.m_Header.Title;
                    this.m_Header.InnerHtml = input;
                    this.m_Header.Controls.Add(title2);
                }
            }
            else
            {
                this.m_TemplateContent = s_RegexHtml.Replace(this.m_TemplateContent, "$1{PE.Control.Header/}");
            }
            if (!this.m_TemplateContent.Contains("{PE.Control.Header/}"))
            {
                this.m_TemplateContent = "{PE.Control.Header/}" + this.m_TemplateContent;
            }
            this.RebuildControls(this, this.m_TemplateContent);
            if (!this.m_IsAddHeader)
            {
                this.Controls.AddAt(0, this.m_Header);
            }
            base.OnPreInit(e);
        }

        private void RebuildControls(Control control, string template)
        {
            if (!string.IsNullOrEmpty(template))
            {
                string[] strArray = s_RegexControl.Replace(template, "||||").Split(new string[] { "||||" }, StringSplitOptions.None);
                MatchCollection matchs = s_RegexControl.Matches(template);
                List<Control> list = new List<Control>();
                foreach (Match match in matchs)
                {
                    Control header = null;
                    if (match.Groups[1].Value == "Header")
                    {
                        header = this.m_Header;
                    }
                    else
                    {
                        header = control.FindControl(match.Groups[1].Value);
                        if (header == null)
                        {
                            header = new LiteralControl(match.Value);
                        }
                    }
                    this.RebuildControls(header, match.Groups[3].Value);
                    list.Add(header);
                }
                control.Controls.Clear();
                for (int i = 0; i < strArray.Length; i++)
                {
                    LiteralControl child = new LiteralControl();
                    child.Text = strArray[i];
                    control.Controls.Add(child);
                    if (i < list.Count)
                    {
                        Control control4 = list[i];
                        if (control != null)
                        {
                            if (control4 is HtmlHead)
                            {
                                if (!this.m_IsAddHeader)
                                {
                                    control.Controls.Add(control4);
                                    this.m_IsAddHeader = true;
                                }
                            }
                            else
                            {
                                control.Controls.Add(control4);
                            }
                        }
                    }
                }
            }
        }

        public static string RebuildPageName(string fileName, NameValueCollection querylist)
        {
            return EasyOne.Web.Utility.RebuildPageName(fileName, querylist);
        }

        public static void WriteErrMsg(string errorMessage)
        {
            if (HttpContext.Current.Request.UrlReferrer != null)
            {
                WriteErrMsg(errorMessage, HttpContext.Current.Request.UrlReferrer.AbsolutePath);
            }
            else
            {
                WriteErrMsg(errorMessage, string.Empty);
            }
        }

        public static void WriteErrMsg(string errorMessage, string returnurl)
        {
            HttpContext.Current.Items["ErrorMessage"] = errorMessage;
            HttpContext.Current.Items["ReturnUrl"] = returnurl;
            HttpContext.Current.Server.Transfer("~/Prompt/ShowError.aspx");
        }

        public static void WriteMessage(string message)
        {
            WriteMessage(message, string.Empty, string.Empty);
        }

        public static void WriteMessage(string message, string returnurl)
        {
            WriteMessage(message, returnurl, string.Empty);
        }

        public static void WriteMessage(string message, string returnurl, string messageTitle)
        {
            EasyOne.Web.Utility.WriteMessage(message, returnurl, messageTitle);
        }

        public static void WriteSuccessMsg(string successMessage)
        {
            WriteSuccessMsg(successMessage, string.Empty);
        }

        public static void WriteSuccessMsg(string successMessage, string returnurl)
        {
            HttpContext.Current.Items["SuccessMessage"] = successMessage;
            HttpContext.Current.Items["ReturnUrl"] = returnurl;
            HttpContext.Current.Server.Transfer("~/Prompt/ShowSuccess.aspx");
        }

        public static void WriteUserErrMsg(string errorMessage)
        {
            WriteUserErrMsg(errorMessage, string.Empty);
        }

        public static void WriteUserErrMsg(string errorMessage, string returnurl)
        {
            HttpContext.Current.Items["ErrorMessage"] = errorMessage;
            HttpContext.Current.Items["ReturnUrl"] = returnurl;
            HttpContext.Current.Server.Transfer("~/Prompt/ShowError.aspx?Action=User");
        }

        public static void WriteUserSuccessMsg(string successMessage)
        {
            WriteUserSuccessMsg(successMessage, string.Empty);
        }

        public static void WriteUserSuccessMsg(string successMessage, string returnurl)
        {
            HttpContext.Current.Items["SuccessMessage"] = successMessage;
            HttpContext.Current.Items["ReturnUrl"] = returnurl;
            HttpContext.Current.Server.Transfer("~/Prompt/ShowSuccess.aspx?Action=User");
        }

        protected virtual string PageFileName
        {
            get
            {
                return Path.GetFileNameWithoutExtension(this.Page.Request.FilePath);
            }
        }

        public override string StyleSheetTheme
        {
            get
            {
                if (HttpContext.Current.Session == null)
                {
                    return base.StyleSheetTheme;
                }
                if (this.Session["DynamicPage_StyleSheetTheme"] == null)
                {
                    PagesSection section = (PagesSection) WebConfigurationManager.GetSection("system.web/pages");
                    if (!string.IsNullOrEmpty(section.StyleSheetTheme))
                    {
                        this.Session.Add("DynamicPage_StyleSheetTheme", section.StyleSheetTheme);
                    }
                    else
                    {
                        this.Session.Add("DynamicPage_StyleSheetTheme", "UserDefaultTheme");
                    }
                }
                return (string) this.Session["DynamicPage_StyleSheetTheme"];
            }
        }

        protected string TemplateContent
        {
            get
            {
                return this.m_TemplateContent;
            }
            set
            {
                this.m_TemplateContent = value;
            }
        }

        public string TemplatePath
        {
            get
            {
                return this.m_TemplatePath;
            }
            set
            {
                this.m_TemplatePath = value;
            }
        }
    }
}

