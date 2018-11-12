namespace EasyOne.Controls
{
    using System;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    [ToolboxData("<{0}:NumberValidator ID=\"Vnum\" runat=\"server\"></{0}:NumberValidator>")]
    public class NumberValidator : RegularExpressionValidator
    {
        private const string validationExpression = "^[0-9]*$";

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            base.ValidationExpression = "^[0-9]*$";
            if (string.IsNullOrEmpty(base.ErrorMessage))
            {
                base.ErrorMessage = "填写的数据必须是数字！";
            }
        }
    }
}

