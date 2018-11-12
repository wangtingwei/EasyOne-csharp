namespace EasyOne.Controls
{
    using System;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    [ToolboxData("<{0}:UrlValidator ID=\"Vurl\" runat=\"server\"></{0}:UrlValidator>")]
    public class UrlValidator : RegularExpressionValidator
    {
        private const string validationExpression = @"http(s)?://([\w-]+\.)+[\w-]+(:\d{1,5})?(/[\w- ./?%&=]*)?";

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            base.ValidationExpression = @"http(s)?://([\w-]+\.)+[\w-]+(:\d{1,5})?(/[\w- ./?%&=]*)?";
            if (string.IsNullOrEmpty(base.ErrorMessage))
            {
                base.ErrorMessage = "填写的URL地址格式不正确！";
            }
        }
    }
}

