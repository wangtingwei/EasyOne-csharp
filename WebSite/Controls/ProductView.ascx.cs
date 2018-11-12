namespace EasyOne.WebSite.Controls
{
    using EasyOne.Common;
    using EasyOne.Controls;
    using EasyOne.Enumerations;
    using EasyOne.Model.Shop;
    using EasyOne.Shop;
    using EasyOne.Web.UI;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Web.UI.WebControls;

    public partial class ProductView : BaseUserControl
    {
        private int m_GeneralId;

        public void GenteralId(int value)
        {
            this.m_GeneralId = value;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.ShowProductInfo();
        }

        private void ShowProductInfo()
        {
            Product product = new Product();
            product.GetProductAllDataById(this.m_GeneralId);
            ProductInfo productInfoData = product.ProductInfoData;
            IList<ProductDataInfo> productDataInfoList = product.ProductDataInfoList;
            this.LblProductKind.Text = BaseUserControl.EnumToHtml<ProductKind>(productInfoData.ProductKind);
            StringBuilder sb = new StringBuilder();
            foreach (ProductCharacter character in Enum.GetValues(typeof(ProductCharacter)))
            {
                if (Product.CharacterIsExists(productInfoData.ProductCharacter, character))
                {
                    StringHelper.AppendString(sb, BaseUserControl.EnumToHtml<ProductCharacter>(character));
                }
            }
            this.LblCharacter.Text = sb.ToString();
            this.LblUnit.Text = productInfoData.Unit;
            switch (productInfoData.StocksProject)
            {
                case StocksProject.ActualStock:
                    this.LblStocksProject.Text = "实际库存";
                    break;

                case StocksProject.VirtualStock:
                    this.LblStocksProject.Text = "虚拟库存";
                    break;
            }
            this.EgvStocks.DataSource = productDataInfoList;
            this.EgvStocks.DataBind();
            this.LblIncludeTax.Text = BaseUserControl.EnumToHtml<TaxRateType>(productInfoData.IncludeTax);
            this.LblTaxRate.Text = productInfoData.TaxRate.ToString();
            this.LblWeight.Text = productInfoData.Weight.ToString();
            this.LblServiceTerm.Text = productInfoData.ServiceTerm.ToString() + BaseUserControl.EnumToHtml<ServiceTermUnit>(productInfoData.ServiceTermUnit);
            this.LblProductType.Text = BaseUserControl.EnumToHtml<ProductType>(productInfoData.ProductType);
            this.LblProperties.Text = productInfoData.Properties;
            this.LblPrice.Text = productInfoData.PriceInfo.Price.ToString("0.00");
            this.LblPrice_Market.Text = productInfoData.PriceMarket.ToString("0.00");
            this.LblDownloadUrl.Text = productInfoData.DownloadUrl;
            this.LblDownloadUrlRemark.Text = productInfoData.Remark;
            if (productInfoData.PriceInfo.PriceMember > 0M)
            {
                this.LblPrice_Member.Text = productInfoData.PriceInfo.PriceMember.ToString("0.00");
            }
            else if (productInfoData.PriceInfo.PriceMember == -1M)
            {
                this.LblPrice_Member.Text = "按会员组价格计算";
            }
            else
            {
                this.LblPrice_Member.Text = "按会员组折扣率计算";
            }
            if (productInfoData.PriceInfo.PriceAgent > 0M)
            {
                this.LblPrice_Agent.Text = productInfoData.PriceInfo.PriceAgent.ToString("0.00");
            }
            else if (productInfoData.PriceInfo.PriceAgent == -1M)
            {
                this.LblPrice_Agent.Text = "按代理商组价格计算";
            }
            else
            {
                this.LblPrice_Agent.Text = "按代理商组折扣率计算";
            }
            switch (productInfoData.SalePromotionType)
            {
                case 0:
                    this.LblSalePromotionType.Text = "不促销";
                    break;

                case 1:
                    this.LblSalePromotionType.Text = string.Concat(new object[] { "买 ", productInfoData.MinNumber, "送", productInfoData.PresentNumber, "同样商品" });
                    break;

                case 2:
                    this.LblSalePromotionType.Text = string.Concat(new object[] { "买 ", productInfoData.MinNumber, "送", productInfoData.PresentNumber, "其它商品" });
                    break;

                case 3:
                    this.LblSalePromotionType.Text = "买就送" + productInfoData.PresentNumber + "同样商品";
                    break;

                case 4:
                    this.LblSalePromotionType.Text = "买就送" + productInfoData.PresentNumber + "其它商品";
                    break;
            }
            this.LblPresentPoint.Text = productInfoData.PresentPoint.ToString();
            this.LblPresentExp.Text = productInfoData.PresentExp.ToString();
            this.LblPresentMoney.Text = productInfoData.PresentMoney.ToString("0.00");
            this.LblEnableSale.Text = productInfoData.EnableSale ? "立即销售" : "停止销售";
        }
    }
}

