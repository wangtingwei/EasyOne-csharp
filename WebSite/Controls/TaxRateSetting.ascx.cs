namespace EasyOne.WebSite.Controls
{
    using EasyOne.Common;
    using EasyOne.Enumerations;
    using EasyOne.Web.UI;
    using System;
    using System.Web.UI.WebControls;

    public partial class TaxRateSetting : BaseUserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                this.RadlTaxRateDataBind();
            }
        }

        private void RadlTaxRateDataBind()
        {
            if (this.RadlTaxRateType.Items.Count == 0)
            {
                foreach (TaxRateType type in Enum.GetValues(typeof(TaxRateType)))
                {
                    ListItem item = new ListItem();
                    item.Text = BaseUserControl.EnumToHtml<TaxRateType>(type);
                    item.Value = ((int) type).ToString();
                    if (type == TaxRateType.BarringTaxNeedInvoiceNoTax)
                    {
                        item.Selected = true;
                    }
                    this.RadlTaxRateType.Items.Add(item);
                }
            }
        }

        public void SetEnabled(bool value)
        {
            this.RadlTaxRateType.Enabled = value;
        }

        public TaxRateType TaxRate
        {
            get
            {
                return (TaxRateType) DataConverter.CLng(this.RadlTaxRateType.SelectedValue);
            }
            set
            {
                this.RadlTaxRateDataBind();
                this.RadlTaxRateType.SelectedValue = ((int) value).ToString();
            }
        }
    }
}

