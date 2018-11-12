namespace EasyOne.Controls
{
    using System;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    [ToolboxData("<{0}:MoneyValidator ID=\"Vmoney\" runat=\"server\"></{0}:MoneyValidator>")]
    public class MoneyValidator : RegularExpressionValidator
    {
        private const string validationExpression = @"^[1-9]+[0-9]*(\.?[0-9]+)?|0\.0*[1-9]+0*|[1-9]$ ";

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            base.ValidationExpression = @"^[1-9]+[0-9]*(\.?[0-9]+)?|0\.0*[1-9]+0*|[1-9]$ ";
            if (string.IsNullOrEmpty(base.ErrorMessage))
            {
                base.ErrorMessage = "金额必须是大于0的数字！";
            }
        }
    }
}

