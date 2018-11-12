namespace EasyOne.WebSite.Controls.FieldControl
{
    using EasyOne.Controls;
    using System;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using EasyOne.ExtendedControls;
    using EasyOne.ModelControls;

    public partial class MoneyType : BaseFieldControl
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            this.TxtMoney.TextMode = TextBoxMode.SingleLine;
            if (base.EnableNull)
            {
                this.ReqTxtMoney.Visible = true;
            }
            if (!string.IsNullOrEmpty(base.Settings[0]) || !string.IsNullOrEmpty(base.Settings[1]))
            {
                if (string.IsNullOrEmpty(base.Settings[0]))
                {
                    this.ValRangeTxtMoney.MinimumValue = int.MinValue.ToString("F");
                }
                else
                {
                    this.ValRangeTxtMoney.MinimumValue = base.Settings[0];
                }
                if (string.IsNullOrEmpty(base.Settings[1]))
                {
                    this.ValRangeTxtMoney.MaximumValue = int.MaxValue.ToString("F");
                }
                else
                {
                    this.ValRangeTxtMoney.MaximumValue = base.Settings[1];
                }
                this.ValRangeTxtMoney.Type = ValidationDataType.Double;
                this.ValRangeTxtMoney.ErrorMessage = "货币不能小于" + base.Settings[0] + "大于" + base.Settings[1];
                this.ValRangeTxtMoney.Visible = true;
            }
            if (!base.IsPostBack)
            {
                this.TxtMoney.Text = this.FieldValue;
            }
            else
            {
                this.FieldValue = this.TxtMoney.Text;
            }
        }
    }
}

