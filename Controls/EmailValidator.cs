namespace EasyOne.Controls
{
    using System;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    [ToolboxData("<{0}:EmailValidator ID=\"Vmail\" runat=\"server\"></{0}:EmailValidator>")]
    public class EmailValidator : RegularExpressionValidator
    {
        private const string validationExpression = @"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*";

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            base.ValidationExpression = @"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*";
            if (string.IsNullOrEmpty(base.ErrorMessage))
            {
                base.ErrorMessage = "填写的电子邮箱格式不正确！";
            }
        }
    }
}

