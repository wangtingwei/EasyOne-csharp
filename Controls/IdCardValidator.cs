namespace EasyOne.Controls
{
    using System;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    [ToolboxData("<{0}:IdCardValidator ID=\"VIdc\" runat=\"server\"></{0}:IdCardValidator>")]
    public class IdCardValidator : RegularExpressionValidator
    {
        private const string validationExpression = @"\d{17}[\d|X]|\d{15}";

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            base.ValidationExpression = @"\d{17}[\d|X]|\d{15}";
            if (string.IsNullOrEmpty(base.ErrorMessage))
            {
                base.ErrorMessage = "填写的身份证号码格式不正确！";
            }
        }
    }
}

