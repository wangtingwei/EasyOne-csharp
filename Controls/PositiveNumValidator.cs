namespace EasyOne.Controls
{
    using System;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    [ToolboxData("<{0}:PositiveNumValidator ID=\"Vpnum\" runat=\"server\"></{0}:PositiveNumValidator>")]
    public class PositiveNumValidator : RegularExpressionValidator
    {
        private const string validationExpression = @"\d+[.]?\d*";

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            base.ValidationExpression = @"\d+[.]?\d*";
            if (string.IsNullOrEmpty(base.ErrorMessage))
            {
                base.ErrorMessage = "填写的数字格式不正确！";
            }
        }
    }
}

