namespace EasyOne.Controls
{
    using System;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    [ToolboxData("<{0}:ValidateCodeControl ID=\"Vcodecontrol\" runat=server></{0}:ValidateCodeControl>")]
    public class ValidateCodeControl : WebControl, IPostBackDataHandler
    {
        private Image m_ValidateCodeImage;
        private TextBox m_ValidateCodeInput;
        private string m_ValidateCodeSessionName = "ValidateCodeSession";
        private string m_ValidateCodeUserInput = string.Empty;
        private const string VALIDATE_CODE_REFRESH_HOOK = "refreshValidateCodeImage({0}, {1});";
        private const string VALIDATE_CODE_REFRESH_SCRIPT = "<script language='javascript'>\r\n            function refreshValidateCodeImage(ValidateCodeImageControl, NewUrl)\r\n            {\r\n                ValidateCodeImageControl.src = NewUrl + '?code=' + randomNum(10);\r\n            }\r\n            function randomNum(n){ \r\n                var rnd=''; \r\n                for(var i=0;i<n;i++)\r\n                     rnd+=Math.floor(Math.random()*10);\r\n                return rnd;\r\n            }\r\n            </script>";
        private const string VALIDATE_CODE_REFRESH_SCRIPT_ID = "1382a047-3f1d-4d12-8e19-6f698c81d7cd";

        protected override void CreateChildControls()
        {
            this.Controls.Clear();
            base.ClearChildViewState();
            this.CreateControlHierarchy();
            this.PrepareControlHierarchy();
            this.TrackViewState();
            base.ChildControlsCreated = true;
        }

        protected virtual void CreateControlHierarchy()
        {
            this.m_ValidateCodeInput = new TextBox();
            this.m_ValidateCodeImage = new Image();
            this.m_ValidateCodeInput.ID = this.ClientID + "_validateInputControl";
            this.m_ValidateCodeImage.ID = this.ClientID + "_validateImageControl";
            this.m_ValidateCodeInput.CssClass = this.TextBoxCssClass;
            this.m_ValidateCodeImage.CssClass = this.ImageCssClass;
            this.m_ValidateCodeImage.ImageUrl = this.ImagePageUrl;
            this.m_ValidateCodeImage.Attributes.Add("onclick", string.Format("refreshValidateCodeImage({0}, {1});", this.ClientID + "_validateImageControl", "'" + this.ImagePageUrl + "'"));
            this.m_ValidateCodeImage.ToolTip = this.RefreshLinkToolTip;
            base.ChildControlsCreated = true;
        }

        public bool IsMatch()
        {
            return this.IsMatch(this.IgnoreCase);
        }

        public bool IsMatch(bool bIgnoreCase)
        {
            if (HttpContext.Current.Session[this.ValidateCodeSessionName] == null)
            {
            }
            if (!bIgnoreCase)
            {
                return (this.m_ValidateCodeUserInput == HttpContext.Current.Session[this.ValidateCodeSessionName].ToString());
            }
            return (string.Compare(this.m_ValidateCodeUserInput, HttpContext.Current.Session[this.ValidateCodeSessionName].ToString(), StringComparison.OrdinalIgnoreCase) == 0);
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            this.Page.RegisterRequiresPostBack(this);
            this.Page.ClientScript.GetPostBackClientHyperlink(this, "");
            if (!this.Page.ClientScript.IsClientScriptBlockRegistered("1382a047-3f1d-4d12-8e19-6f698c81d7cd"))
            {
                this.Page.ClientScript.RegisterClientScriptBlock(Type.GetType("System.String"), "1382a047-3f1d-4d12-8e19-6f698c81d7cd", "<script language='javascript'>\r\n            function refreshValidateCodeImage(ValidateCodeImageControl, NewUrl)\r\n            {\r\n                ValidateCodeImageControl.src = NewUrl + '?code=' + randomNum(10);\r\n            }\r\n            function randomNum(n){ \r\n                var rnd=''; \r\n                for(var i=0;i<n;i++)\r\n                     rnd+=Math.floor(Math.random()*10);\r\n                return rnd;\r\n            }\r\n            </script>");
            }
        }

        protected virtual void PrepareControlHierarchy()
        {
            Table child = new Table {
                CellSpacing = 0,
                CellPadding = 0,
                BorderWidth = 0,
                CssClass = this.TableCssClass
            };
            TableRow row = new TableRow();
            TableCell cell = new TableCell();
            cell.Controls.Add(this.m_ValidateCodeInput);
            row.Cells.Add(cell);
            cell.Dispose();
            TableCell cell2 = new TableCell();
            cell2.Controls.Add(this.m_ValidateCodeImage);
            row.Cells.Add(cell2);
            cell2.Dispose();
            child.Rows.Add(row);
            this.Controls.Add(child);
            row.Dispose();
            child.Dispose();
        }

        protected override void Render(HtmlTextWriter writer)
        {
            this.EnsureChildControls();
            base.Render(writer);
        }

        bool IPostBackDataHandler.LoadPostData(string postDataKey, NameValueCollection postCollection)
        {
            string str = postCollection[this.ClientID + "_validateInputControl"];
            if (!string.IsNullOrEmpty(str))
            {
                this.m_ValidateCodeUserInput = str;
                return true;
            }
            return false;
        }

        void IPostBackDataHandler.RaisePostDataChangedEvent()
        {
        }

        [DefaultValue(true), Description("读取或设置是否不区分大小写。"), Category("行为")]
        public bool IgnoreCase
        {
            get
            {
                object obj2 = this.ViewState["IgnoreCase"];
                if (obj2 != null)
                {
                    return (bool) obj2;
                }
                return true;
            }
            set
            {
                this.ViewState["IgnoreCase"] = value;
            }
        }

        [Category("外观"), DefaultValue(""), Description("读取或设置验证码的CSS样式")]
        public string ImageCssClass
        {
            get
            {
                object obj2 = this.ViewState["ValidateCodeImageCSSClass"];
                if (obj2 != null)
                {
                    return obj2.ToString();
                }
                return "";
            }
            set
            {
                this.ViewState["ValidateCodeImageCSSClass"] = value;
            }
        }

        [Description("读取或设置验证码图片地址"), DefaultValue("../ValidateCode/ValidateCode.aspx"), Category("行为")]
        public string ImagePageUrl
        {
            get
            {
                object obj2 = this.ViewState["ValidateCodeImagePageUrl"];
                if (obj2 != null)
                {
                    return obj2.ToString();
                }
                return "../ValidateCode/ValidateCode.aspx";
            }
            set
            {
                this.ViewState["ValidateCodeImagePageUrl"] = value;
            }
        }

        [Description("读取或设置刷新链接的CSS样式"), Category("外观"), DefaultValue("")]
        public string RefreshLinkCssClass
        {
            get
            {
                object obj2 = this.ViewState["ValidateCodeRefreshCSSClass"];
                if (obj2 != null)
                {
                    return obj2.ToString();
                }
                return "";
            }
            set
            {
                this.ViewState["ValidateCodeRefreshCSSClass"] = value;
            }
        }

        [Description("读取或设置刷新验证码的链接文字"), Category("行为"), DefaultValue("看不清楚换个图片")]
        public string RefreshLinkToolTip
        {
            get
            {
                object obj2 = this.ViewState["ValidateCodeRefreshText"];
                if (obj2 != null)
                {
                    return obj2.ToString();
                }
                return "看不清楚换个图片";
            }
            set
            {
                this.ViewState["ValidateCodeRefreshText"] = value;
            }
        }

        [Description("读取或设置表格的CSS样式"), Category("外观"), DefaultValue("")]
        public string TableCssClass
        {
            get
            {
                object obj2 = this.ViewState["ValidateCodeTableCSSClass"];
                if (obj2 != null)
                {
                    return obj2.ToString();
                }
                return "";
            }
            set
            {
                this.ViewState["ValidateCodeTableCSSClass"] = value;
            }
        }

        protected override HtmlTextWriterTag TagKey
        {
            get
            {
                return HtmlTextWriterTag.Table;
            }
        }

        [Category("外观"), DefaultValue(""), Description("读取或设置验证码输入文本框的CSS样式")]
        public string TextBoxCssClass
        {
            get
            {
                object obj2 = this.ViewState["ValidateCodeInputTextBoxCSSClass"];
                if (obj2 != null)
                {
                    return obj2.ToString();
                }
                return "";
            }
            set
            {
                this.ViewState["ValidateCodeInputTextBoxCSSClass"] = value;
            }
        }

        [Description("读取或设置验证码Session保存的名称，如果不设置则默认为ValidateCodeSession。注意：必须和ValidateCodeImage里的ValidateCodeSessionName保持一致，否则会出现错误。"), DefaultValue("ValidateCodeSession")]
        public string ValidateCodeSessionName
        {
            get
            {
                return this.m_ValidateCodeSessionName;
            }
            set
            {
                this.m_ValidateCodeSessionName = value;
            }
        }

        [Browsable(false)]
        public string ValidateCodeUserInput
        {
            get
            {
                return this.m_ValidateCodeUserInput;
            }
        }
    }
}

