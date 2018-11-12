namespace EasyOne.Controls
{
    using System;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    [ToolboxData("<{0}:MobileValidator ID=\"Vmbl\" runat=\"server\"></{0}:MobileValidator>")]
    public class MobileValidator : RegularExpressionValidator
    {
        private const string validationExpression = @"0?1[35][0-9]{9}|(\(\d{3,4}\)|\d{3,4}-?)?\d{7,8}(-\d{1,5})?";

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            base.ValidationExpression = @"0?1[35][0-9]{9}|(\(\d{3,4}\)|\d{3,4}-?)?\d{7,8}(-\d{1,5})?";
            if (string.IsNullOrEmpty(base.ErrorMessage))
            {
                base.ErrorMessage = "填写的手机号码格式不正确！";
            }
        }
    }
}

