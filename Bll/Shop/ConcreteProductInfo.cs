namespace EasyOne.Shop
{
    using EasyOne.Model.Shop;
    using EasyOne.Model.UserManage;
    using System;

    public class ConcreteProductInfo : AbstractItemInfo
    {
        private bool m_haveWholesalePurview;
        private bool m_NeedInvoice;
        private ProductInfo m_ProductInfo;
        private string m_Property;
        private int m_Quantity;
        private UserInfo m_UserInfo;

        public ConcreteProductInfo(int quantity, string property, ProductInfo productInfo, UserInfo userInfo, bool needInvoice, bool needTaxRateCompute, bool haveWholesalePurview)
        {
            this.m_Quantity = quantity;
            this.m_Property = property;
            this.m_ProductInfo = productInfo;
            this.m_UserInfo = userInfo;
            if (this.m_UserInfo == null)
            {
                this.m_UserInfo = new UserInfo(true);
            }
            this.m_NeedInvoice = needInvoice;
            base.NeedTaxRateCompute = needTaxRateCompute;
            this.m_haveWholesalePurview = haveWholesalePurview;
        }

        public override void GetItemInfo()
        {
            if ((this.m_haveWholesalePurview && this.m_ProductInfo.EnableWholesale) && (this.m_Quantity >= this.m_ProductInfo.PriceInfo.NumberWholesale1))
            {
                base.SaleType = 4;
            }
            else
            {
                base.SaleType = 1;
            }
            base.Price = ProductPrice.GetTruePrice(this.m_ProductInfo, this.m_Quantity, this.m_UserInfo, this.m_Property, this.m_haveWholesalePurview);
            base.Price = AbstractItemInfo.TaxRateCompute(this.m_NeedInvoice, this.m_ProductInfo, base.Price);
            base.ProductName = this.m_ProductInfo.ProductName;
            base.Unit = this.m_ProductInfo.Unit;
            base.Amount = this.m_Quantity;
            base.PriceMarket = this.m_ProductInfo.PriceMarket;
            base.ServiceTerm = this.m_ProductInfo.ServiceTerm;
            base.ServiceTermUnit = this.m_ProductInfo.ServiceTermUnit;
            base.Remark = "";
            base.BeginDate = DateTime.Today;
            base.PresentExp = this.m_ProductInfo.PresentExp;
            base.PresentMoney = this.m_ProductInfo.PresentMoney;
            base.PresentPoint = this.m_ProductInfo.PresentPoint;
            base.ProductId = this.m_ProductInfo.ProductId;
            base.TableName = this.m_ProductInfo.TableName;
            base.ProductKind = this.m_ProductInfo.ProductKind;
            base.TotalWeight = this.m_ProductInfo.Weight * base.Amount;
            base.SubTotal = base.Price * this.m_Quantity;
            base.Property = this.m_Property;
            base.ProductCharacter = this.m_ProductInfo.ProductCharacter;
            base.Weight = this.m_ProductInfo.Weight;
        }
    }
}

