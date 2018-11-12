namespace EasyOne.WebSite.Controls.Shop
{
    using EasyOne.Common;
    using EasyOne.Model.Shop;
    using EasyOne.ModelControls;
    using EasyOne.Shop;
    using EasyOne.Web.UI;
    using System;
    using System.Web.UI.WebControls;

    public partial class SalePromotion : BaseUserControl
    {

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        public void SetPresent(ProductInfo productInfo)
        {
            switch (productInfo.SalePromotionType)
            {
                case 0:
                    this.RadSalePromotionType1.Checked = true;
                    return;

                case 1:
                    this.RadSalePromotionType2.Checked = true;
                    this.TxtMinNumber1.Text = productInfo.MinNumber.ToString();
                    this.TxtPresentNumber1.Text = productInfo.PresentNumber.ToString();
                    return;

                case 2:
                    this.RadSalePromotionType3.Checked = true;
                    this.TxtMinNumber2.Text = productInfo.MinNumber.ToString();
                    this.TxtPresentNumber2.Text = productInfo.PresentNumber.ToString();
                    this.SelectPresent1.Text = Present.GetPresentById(DataConverter.CLng(productInfo.PresentId)).PresentName;
                    this.SelectPresent1.DataKey = productInfo.PresentId.ToString();
                    return;

                case 3:
                    this.RadSalePromotionType4.Checked = true;
                    this.TxtPresentNumber3.Text = productInfo.PresentNumber.ToString();
                    return;

                case 4:
                    this.RadSalePromotionType5.Checked = true;
                    this.TxtPresentNumber4.Text = productInfo.PresentNumber.ToString();
                    this.SelectPresent2.Text = Present.GetPresentById(DataConverter.CLng(productInfo.PresentId)).PresentName;
                    this.SelectPresent2.DataKey = productInfo.PresentId.ToString();
                    return;
            }
            this.RadSalePromotionType1.Checked = true;
        }

        public int MinNumber
        {
            get
            {
                if (this.RadSalePromotionType2.Checked)
                {
                    return DataConverter.CLng(this.TxtMinNumber1.Text, 1);
                }
                if (this.RadSalePromotionType3.Checked)
                {
                    return DataConverter.CLng(this.TxtMinNumber2.Text, 1);
                }
                return 1;
            }
        }

        public string PresentId
        {
            get
            {
                if (this.RadSalePromotionType3.Checked)
                {
                    if (string.IsNullOrEmpty(this.SelectPresent1.DataKey))
                    {
                        BaseUserControl.WriteErrMsg("<li>请指定赠品ID</li>");
                    }
                    return this.SelectPresent1.DataKey;
                }
                if (!this.RadSalePromotionType5.Checked)
                {
                    return string.Empty;
                }
                if (string.IsNullOrEmpty(this.SelectPresent2.DataKey))
                {
                    BaseUserControl.WriteErrMsg("<li>请指定赠品ID</li>");
                }
                return this.SelectPresent2.DataKey;
            }
        }

        public int PresentNumber
        {
            get
            {
                if (this.RadSalePromotionType2.Checked)
                {
                    return DataConverter.CLng(this.TxtPresentNumber1.Text, 1);
                }
                if (this.RadSalePromotionType3.Checked)
                {
                    return DataConverter.CLng(this.TxtPresentNumber2.Text, 1);
                }
                if (this.RadSalePromotionType4.Checked)
                {
                    return DataConverter.CLng(this.TxtPresentNumber3.Text);
                }
                if (this.RadSalePromotionType5.Checked)
                {
                    return DataConverter.CLng(this.TxtPresentNumber4.Text);
                }
                return 1;
            }
        }

        public int SalePromotionType
        {
            get
            {
                if (this.RadSalePromotionType2.Checked)
                {
                    return 1;
                }
                if (this.RadSalePromotionType3.Checked)
                {
                    return 2;
                }
                if (this.RadSalePromotionType4.Checked)
                {
                    return 3;
                }
                if (this.RadSalePromotionType5.Checked)
                {
                    return 4;
                }
                return 0;
            }
        }
    }
}

