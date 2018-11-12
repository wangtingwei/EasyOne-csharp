namespace EasyOne.Controls
{
    using System;
    using System.ComponentModel;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    [ToolboxData("<{0}:ValidateCode ID=\"Vcode\" runat=server></{0}:ValidateCode>")]
    public class ValidateCode : Image
    {
        private string m_ValidateCodeSessionName = "ValidateCodeSession";
        private const string VALIDATE_CODE_REFRESH_HOOK = "refreshValidateCodeImage(this);";
        private const string VALIDATE_CODE_REFRESH_SCRIPT = "<script language='javascript'>\r\n                    function refreshValidateCodeImage(ValidateCodeImageControl)\r\n                    {\r\n                        ValidateCodeImageControl.src =  ValidateCodeImageControl.src + '?code=' + randomNum(10);\r\n                    }\r\n                    function randomNum(n){ \r\n                        var rnd=''; \r\n                        for(var i=0;i<n;i++)\r\n                             rnd+=Math.floor(Math.random()*10);\r\n                        return rnd;\r\n                    }\r\n                    </script>";
        private const string VALIDATE_CODE_REFRESH_SCRIPT_ID = "1382a047-3f1d-4d12-8e19-6f698c81d7cd";

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            this.Page.ClientScript.GetPostBackClientHyperlink(this, "");
            if (!this.Page.ClientScript.IsClientScriptBlockRegistered("1382a047-3f1d-4d12-8e19-6f698c81d7cd"))
            {
                this.Page.ClientScript.RegisterClientScriptBlock(base.GetType(), "1382a047-3f1d-4d12-8e19-6f698c81d7cd", "<script language='javascript'>\r\n                    function refreshValidateCodeImage(ValidateCodeImageControl)\r\n                    {\r\n                        ValidateCodeImageControl.src =  ValidateCodeImageControl.src + '?code=' + randomNum(10);\r\n                    }\r\n                    function randomNum(n){ \r\n                        var rnd=''; \r\n                        for(var i=0;i<n;i++)\r\n                             rnd+=Math.floor(Math.random()*10);\r\n                        return rnd;\r\n                    }\r\n                    </script>");
            }
        }

        protected override void Render(HtmlTextWriter writer)
        {
            this.ImageUrl = this.ImagePageUrl;
            base.Attributes.Add("onclick", "refreshValidateCodeImage(this);");
            base.Attributes.CssStyle.Add("cursor", "hand");
            this.ToolTip = this.RefreshLinkToolTip;
            base.Render(writer);
        }

        [Category("行为"), Description("读取或设置验证码图片地址"), DefaultValue("~/Controls/ValidateCodeImage.aspx")]
        public string ImagePageUrl
        {
            get
            {
                object obj2 = this.ViewState["ValidateCodeImagePageUrl"];
                if (obj2 != null)
                {
                    return obj2.ToString();
                }
                return "~/Controls/ValidateCodeImage.aspx";
            }
            set
            {
                this.ViewState["ValidateCodeImagePageUrl"] = value;
            }
        }

        [Description("读取或设置刷新验证码的链接文字"), DefaultValue("看不清楚换个图片"), Category("行为")]
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

        [DefaultValue("ValidateCodeSession"), Description("读取或设置验证码Session保存的名称，如果不设置则默认为ValidateCodeSession。注意：必须和ValidateCodeImage里的ValidateCodeSessionName保持一致，否则会出现错误。")]
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

        [Description("验证码的值")]
        public string ValidateCodeValue
        {
            get
            {
                if (HttpContext.Current.Session[this.ValidateCodeSessionName] == null)
                {
                    return string.Empty;
                }
                return HttpContext.Current.Session[this.ValidateCodeSessionName].ToString();
            }
        }
    }
}

