namespace EasyOne.WebSite.Controls.FieldControl
{
    using EasyOne.Controls;
    using System;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using EasyOne.ModelControls;

    public partial class NumberType : BaseFieldControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.TxtNumber.TextMode = TextBoxMode.SingleLine;
            if (base.Settings[3] == "True")
            {
                this.LitPercent.Text = " %";
                this.LitPercent.Visible = true;
            }
            if (base.EnableNull)
            {
                this.ReqTxtNumber.Visible = true;
            }
            switch (base.Settings[2])
            {
                case "0":
                    this.RegTxtNumber.ValidationExpression = "^[0-9]*$";
                    this.RegTxtNumber.ErrorMessage = "只能够输入0-9的数字";
                    break;

                case "1":
                    this.RegTxtNumber.ValidationExpression = @"^-?[0-9]+(\.?[0-9])?$";
                    this.RegTxtNumber.ErrorMessage = "最多只能够输入一位小数位数";
                    break;

                case "2":
                    this.RegTxtNumber.ValidationExpression = @"^-?[0-9]+(\.?[0-9]{1,2})?$";
                    this.RegTxtNumber.ErrorMessage = "最多只能够输入两位小数位数";
                    break;

                case "3":
                    this.RegTxtNumber.ValidationExpression = @"^-?[0-9]+(\.?[0-9]{1,3})?$";
                    this.RegTxtNumber.ErrorMessage = "最多只能够输入三位数位数";
                    break;

                case "4":
                    this.RegTxtNumber.ValidationExpression = @"^-?[0-9]+(\.?[0-9]{1,4})?$";
                    this.RegTxtNumber.ErrorMessage = "最多只能够输入四位小数位数";
                    break;

                case "5":
                    this.RegTxtNumber.ValidationExpression = @"^-?[0-9]+(\.?[0-9]{1,5})?$";
                    this.RegTxtNumber.ErrorMessage = "最多只能够输入五位小数位数";
                    break;

                default:
                    this.RegTxtNumber.ValidationExpression = @"^-?[0-9]+(\.?[0-9]+)?$";
                    this.RegTxtNumber.ErrorMessage = "只能够输入数字";
                    break;
            }
            if (!string.IsNullOrEmpty(base.Settings[0]) || !string.IsNullOrEmpty(base.Settings[1]))
            {
                if (string.IsNullOrEmpty(base.Settings[0]))
                {
                    this.ValRangeTxtNumber.MinimumValue = int.MinValue.ToString("F");
                }
                else
                {
                    this.ValRangeTxtNumber.MinimumValue = base.Settings[0];
                }
                if (string.IsNullOrEmpty(base.Settings[1]))
                {
                    this.ValRangeTxtNumber.MaximumValue = int.MaxValue.ToString("F");
                }
                else
                {
                    this.ValRangeTxtNumber.MaximumValue = base.Settings[1];
                }
                this.ValRangeTxtNumber.Type = ValidationDataType.Double;
                this.ValRangeTxtNumber.ErrorMessage = "数字不能小于" + base.Settings[0] + "大于" + base.Settings[1];
                this.ValRangeTxtNumber.Visible = true;
            }
            if (!base.IsPostBack)
            {
                this.TxtNumber.Text = this.FieldValue;
            }
            else
            {
                this.FieldValue = this.TxtNumber.Text;
            }
        }
    }
}

