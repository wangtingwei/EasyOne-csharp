namespace EasyOne.Controls.Editor
{
    using System;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.Configuration;
    using System.Globalization;
    using System.Text.RegularExpressions;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    [DefaultProperty("Value"), ValidationProperty("Value"), ToolboxData("<{0}:Editor runat=server></{0}:Editor>"), Designer("EasyOne.Controls.Editor.EasyOneDesigner"), ParseChildren(false)]
    public class PEeditor : Control, IPostBackDataHandler
    {
        public bool CheckBrowserCompatibility()
        {
            HttpBrowserCapabilities browser = this.Page.Request.Browser;
            if (((browser.Browser == "IE") && ((browser.MajorVersion >= 6) || ((browser.MajorVersion == 5) && (browser.MinorVersion >= 0.5)))) && browser.Win32)
            {
                return true;
            }
            Match match = Regex.Match(this.Page.Request.UserAgent, @"(?<=Gecko/)\d{8}");
            return (match.Success && (int.Parse(match.Value, CultureInfo.InvariantCulture) >= 0x131a302));
        }

        public bool LoadPostData(string postDataKey, NameValueCollection postCollection)
        {
            if (postCollection[postDataKey] != this.Value)
            {
                this.Value = postCollection[postDataKey];
                return true;
            }
            return false;
        }

        public void RaisePostDataChangedEvent()
        {
        }

        protected override void Render(HtmlTextWriter writer)
        {
            writer.Write("<div>");
            if (this.CheckBrowserCompatibility())
            {
                string basePath = this.BasePath;
                if (basePath.StartsWith("~", StringComparison.Ordinal))
                {
                    basePath = base.ResolveUrl(basePath);
                }
                string str2 = (this.Page.Request.QueryString["fcksource"] == "true") ? "fckeditor.original.html" : "fckeditor.html";
                string str3 = basePath;
                basePath = str3 + "editor/" + str2 + "?InstanceName=" + this.ClientID;
                if (this.ToolbarSet.Length > 0)
                {
                    basePath = basePath + "&amp;Toolbar=" + this.ToolbarSet;
                }
                writer.Write("<input type=\"hidden\" id=\"{0}\" name=\"{1}\" value=\"{2}\" />", this.ClientID, this.UniqueID, HttpUtility.HtmlEncode(this.Value));
                writer.Write("<input type=\"hidden\" id=\"{0}___Config\" value=\"{1}\" />", this.ClientID, this.Config.GetHiddenFieldString());
                writer.Write("<iframe id=\"{0}___Frame\" src=\"{1}\" width=\"{2}\" height=\"{3}\" frameborder=\"no\" scrolling=\"no\"></iframe>", new object[] { this.ClientID, basePath, this.Width, this.Height });
            }
            else
            {
                writer.Write("<textarea name=\"{0}\" rows=\"4\" cols=\"40\" style=\"width: {1}; height: {2}\" wrap=\"virtual\">{3}</textarea>", new object[] { this.UniqueID, this.Width, this.Height, HttpUtility.HtmlEncode(this.Value) });
            }
            writer.Write("</div>");
        }

        [Category("Configurations")]
        public bool AutoDetectLanguage
        {
            set
            {
                this.Config["AutoDetectLanguage"] = value ? "true" : "false";
            }
        }

        [Category("Configurations")]
        public string BaseHref
        {
            set
            {
                this.Config["BaseHref"] = value;
            }
        }

        [DefaultValue("~/editor/")]
        public string BasePath
        {
            get
            {
                object obj2 = this.ViewState["BasePath"];
                if (obj2 == null)
                {
                    obj2 = ConfigurationManager.AppSettings["EasyOne:BasePath"];
                }
                if (obj2 != null)
                {
                    return (string) obj2;
                }
                return "~/editor/";
            }
            set
            {
                this.ViewState["BasePath"] = value;
            }
        }

        [Browsable(false)]
        public EasyOneConfigurations Config
        {
            get
            {
                if (this.ViewState["Config"] == null)
                {
                    this.ViewState["Config"] = new EasyOneConfigurations();
                }
                return (EasyOneConfigurations) this.ViewState["Config"];
            }
        }

        [Category("Configurations")]
        public LanguageDirection ContentLangDirection
        {
            set
            {
                this.Config["ContentLangDirection"] = (value == LanguageDirection.LeftToRight) ? "ltr" : "rtl";
            }
        }

        [Category("Configurations")]
        public string CustomConfigurationsPath
        {
            set
            {
                this.Config["CustomConfigurationsPath"] = value;
            }
        }

        [Category("Configurations")]
        public bool Debug
        {
            set
            {
                this.Config["Debug"] = value ? "true" : "false";
            }
        }

        [Category("Configurations")]
        public string DefaultLanguage
        {
            set
            {
                this.Config["DefaultLanguage"] = value;
            }
        }

        [Category("Configurations")]
        public string EditorAreaCss
        {
            set
            {
                this.Config["EditorAreaCSS"] = value;
            }
        }

        [Category("Configurations")]
        public bool EnableSourceXhtml
        {
            set
            {
                this.Config["EnableSourceXHTML"] = value ? "true" : "false";
            }
        }

        [Category("Configurations")]
        public bool EnableXhtml
        {
            set
            {
                this.Config["EnableXHTML"] = value ? "true" : "false";
            }
        }

        [Category("FieldName")]
        public string FieldName
        {
            set
            {
                this.Config["FieldName"] = value;
            }
        }

        [Category("Configurations")]
        public bool FillEmptyBlocks
        {
            set
            {
                this.Config["FillEmptyBlocks"] = value ? "true" : "false";
            }
        }

        [Category("Configurations")]
        public string FlashUploadAllowedExtensions
        {
            set
            {
                this.Config["FlashUploadAllowedExtensions"] = ".(" + value + ")$";
            }
        }

        [Category("Configurations")]
        public string FontColors
        {
            set
            {
                this.Config["FontColors"] = value;
            }
        }

        [Category("Configurations")]
        public string FontFormats
        {
            set
            {
                this.Config["FontFormats"] = value;
            }
        }

        [Category("Configurations")]
        public string FontNames
        {
            set
            {
                this.Config["FontNames"] = value;
            }
        }

        [Category("Configurations")]
        public string FontSizes
        {
            set
            {
                this.Config["FontSizes"] = value;
            }
        }

        [Category("Configurations")]
        public bool ForcePasteAsPlainText
        {
            set
            {
                this.Config["ForcePasteAsPlainText"] = value ? "true" : "false";
            }
        }

        [Category("Configurations")]
        public bool ForceSimpleAmpersand
        {
            set
            {
                this.Config["ForceSimpleAmpersand"] = value ? "true" : "false";
            }
        }

        [Category("Configurations")]
        public string FormatIndentator
        {
            set
            {
                this.Config["FormatIndentator"] = value;
            }
        }

        [Category("Configurations")]
        public bool FormatOutput
        {
            set
            {
                this.Config["FormatOutput"] = value ? "true" : "false";
            }
        }

        [Category("Configurations")]
        public bool FormatSource
        {
            set
            {
                this.Config["FormatSource"] = value ? "true" : "false";
            }
        }

        [Category("Configurations")]
        public bool FullPage
        {
            set
            {
                this.Config["FullPage"] = value ? "true" : "false";
            }
        }

        [Category("Configurations")]
        public bool GeckoUseSpan
        {
            set
            {
                this.Config["GeckoUseSPAN"] = value ? "true" : "false";
            }
        }

        [Category("Appearence"), DefaultValue("200px")]
        public Unit Height
        {
            get
            {
                object obj2 = this.ViewState["Height"];
                if (obj2 != null)
                {
                    return (Unit) obj2;
                }
                return Unit.Pixel(200);
            }
            set
            {
                this.ViewState["Height"] = value;
            }
        }

        [Category("Configurations")]
        public string ImageBrowserUrl
        {
            set
            {
                this.Config["ImageBrowserURL"] = value;
            }
        }

        [Category("Configurations")]
        public string ImageUploadAllowedExtensions
        {
            set
            {
                this.Config["ImageUploadAllowedExtensions"] = ".(" + value + ")$";
            }
        }

        [Category("ImgPreview")]
        public string ImgPreview
        {
            set
            {
                this.Config["ImgPreview"] = value;
            }
        }

        [Category("IsThumb")]
        public string IsThumb
        {
            set
            {
                this.Config["IsThumb"] = value;
            }
        }

        [Category("IsUpload")]
        public string IsUpload
        {
            set
            {
                this.Config["LinkUpload"] = value;
                this.Config["ImageUpload"] = value;
                this.Config["FlashUpload"] = value;
            }
        }

        [Category("IsWatermark")]
        public string IsWatermark
        {
            set
            {
                this.Config["IsWatermark"] = value;
            }
        }

        [Category("Configurations")]
        public string LinkBrowserUrl
        {
            set
            {
                this.Config["LinkBrowserURL"] = value;
            }
        }

        [Category("Configurations")]
        public string LinkUploadAllowedExtensions
        {
            set
            {
                this.Config["LinkUploadAllowedExtensions"] = ".(" + value + ")$";
            }
        }

        [Category("ModelId")]
        public string ModelId
        {
            set
            {
                this.Config["ModelId"] = value;
            }
        }

        [Category("Configurations")]
        public string PluginsPath
        {
            set
            {
                this.Config["PluginsPath"] = value;
            }
        }

        [Category("Configurations")]
        public string SkinPath
        {
            set
            {
                this.Config["SkinPath"] = value;
            }
        }

        [Category("Configurations")]
        public bool StartupFocus
        {
            set
            {
                this.Config["StartupFocus"] = value ? "true" : "false";
            }
        }

        [Category("Configurations")]
        public string StylesXmlPath
        {
            set
            {
                this.Config["StylesXmlPath"] = value;
            }
        }

        [Category("Configurations")]
        public int TabSpaces
        {
            set
            {
                this.Config["TabSpaces"] = value.ToString(CultureInfo.InvariantCulture);
            }
        }

        [Category("Configurations")]
        public bool ToolbarCanCollapse
        {
            set
            {
                this.Config["ToolbarCanCollapse"] = value ? "true" : "false";
            }
        }

        [DefaultValue("Default")]
        public string ToolbarSet
        {
            get
            {
                object obj2 = this.ViewState["ToolbarSet"];
                if (obj2 != null)
                {
                    return (string) obj2;
                }
                return "Default";
            }
            set
            {
                this.ViewState["ToolbarSet"] = value;
            }
        }

        [Category("Configurations")]
        public bool ToolbarStartExpanded
        {
            set
            {
                this.Config["ToolbarStartExpanded"] = value ? "true" : "false";
            }
        }

        [Category("UploadPath")]
        public string UploadPath
        {
            set
            {
                this.Config["UploadPath"] = value;
            }
        }

        [Category("Configurations")]
        public bool UseBROnCarriageReturn
        {
            set
            {
                this.Config["UseBROnCarriageReturn"] = value ? "true" : "false";
            }
        }

        [DefaultValue("")]
        public string Value
        {
            get
            {
                object obj2 = this.ViewState["Value"];
                if (obj2 != null)
                {
                    return (string) obj2;
                }
                return "";
            }
            set
            {
                this.ViewState["Value"] = value;
            }
        }

        [DefaultValue("100%"), Category("Appearence")]
        public Unit Width
        {
            get
            {
                object obj2 = this.ViewState["Width"];
                if (obj2 != null)
                {
                    return (Unit) obj2;
                }
                return Unit.Percentage(100.0);
            }
            set
            {
                this.ViewState["Width"] = value;
            }
        }
    }
}

