namespace EasyOne.Controls
{
    using System;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    [ToolboxData("<{0}:FieldValidator ID=\"Vfld\" runat=\"server\"></{0}:FieldValidator>")]
    public class FieldValidator : RegularExpressionValidator
    {
        private const string validationExpression = "^([a-zA-Z]|[a-zA-Z][a-zA-Z_0-9]*[a-zA-Z0-9])$";

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            base.ValidationExpression = "^([a-zA-Z]|[a-zA-Z][a-zA-Z_0-9]*[a-zA-Z0-9])$";
            if (string.IsNullOrEmpty(base.ErrorMessage))
            {
                base.ErrorMessage = "填写的字段格式不正确！";
            }
        }
    }
}

