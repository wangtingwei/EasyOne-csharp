namespace EasyOne.Controls
{
    using System;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    [ToolboxData("<{0}:ZipCodeValidator ID=\"Vzip\" runat=\"server\"></{0}:ZipCodeValidator>")]
    public class ZipCodeValidator : RegularExpressionValidator
    {
        private const string validationExpression = @"\d{6}";

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            base.ValidationExpression = @"\d{6}";
            if (string.IsNullOrEmpty(base.ErrorMessage))
            {
                base.ErrorMessage = "填写的邮政编码格式不正确！";
            }
        }
    }
}

