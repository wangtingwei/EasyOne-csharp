namespace EasyOne.WebSite.Admin.Prompt
{
    using EasyOne.ExtendedControls;
    using EasyOne.Web.UI;
    using System;
    using System.Web;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public partial class ShowMessage : AdminPage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            this.LtrMessage.Text = HttpContext.Current.Items["Message"] as string;
            string str = HttpContext.Current.Items["ReturnUrl"] as string;
            this.LblMessageTitle.Text = HttpContext.Current.Items["MessageTitle"] as string;
            if (string.IsNullOrEmpty(str))
            {
                if (base.Request.UrlReferrer == null)
                {
                    this.LnkReturnUrl.Text = "关闭";
                    this.LnkReturnUrl.NavigateUrl = "javascript:window.close();";
                }
                else
                {
                    this.LnkReturnUrl.NavigateUrl = "javascript:history.back();";
                }
            }
            else if ((str.StartsWith("http://", StringComparison.CurrentCultureIgnoreCase) || str.StartsWith("https://", StringComparison.CurrentCultureIgnoreCase)) || str.StartsWith("javascript:", StringComparison.CurrentCultureIgnoreCase))
            {
                this.LnkReturnUrl.NavigateUrl = str;
            }
            else if (!string.IsNullOrEmpty(this.Page.Request.RawUrl))
            {
                string rawUrl = this.Page.Request.RawUrl;
                string str3 = rawUrl.Substring(0, rawUrl.LastIndexOf("/", StringComparison.Ordinal) + 1);
                this.LnkReturnUrl.NavigateUrl = str3 + str;
            }
        }
    }
}

