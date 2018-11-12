namespace EasyOne.Shop
{
    using EasyOne.Common;
    using EasyOne.Enumerations;
    using EasyOne.Model.Shop;
    using System;
    using System.Collections.Generic;

    public abstract class AbstractItemInfo
    {
        internal bool isPresent;
        private int m_Amount;
        private DateTime m_BeginDate;
        private int m_ID;
        private bool m_IsNull;
        private bool m_NeedTaxRateCompute;
        private int m_PresentExp;
        private decimal m_PresentMoney;
        private int m_PresentPoint;
        private decimal m_Price;
        private decimal m_PriceMarket;
        private EasyOne.Enumerations.ProductCharacter m_ProductCharacter;
        private int m_ProductId;
        private EasyOne.Enumerations.ProductKind m_ProductKind;
        private string m_ProductName;
        private string m_Property;
        private string m_Remark;
        private int m_SaleType;
        private int m_ServiceTerm;
        private EasyOne.Enumerations.ServiceTermUnit m_ServiceTermUnit;
        private decimal m_SubTotal;
        private string m_TableName;
        private double m_TotalWeight;
        private string m_Unit;
        private double m_Weight;

        protected AbstractItemInfo()
        {
        }

        protected static bool FoundInCart(IList<ShoppingCartInfo> infoList, int productId)
        {
            foreach (ShoppingCartInfo info in infoList)
            {
                if (info.ProductId == productId)
                {
                    return true;
                }
            }
            return false;
        }

        public abstract void GetItemInfo();
        public OrderItemInfo GetOrderItemInfo()
        {
            return this.GetOrderItemInfo(0);
        }

        public OrderItemInfo GetOrderItemInfo(int orderId)
        {
            OrderItemInfo info = new OrderItemInfo();
            if (!this.IsNull)
            {
                info.ProductName = this.ProductName;
                if (this.isPresent)
                {
                    info.ProductId = this.Id;
                }
                else
                {
                    info.ProductId = this.ProductId;
                }
                info.TableName = this.TableName;
                info.Unit = this.Unit;
                info.Amount = this.Amount;
                info.PriceMarket = this.PriceMarket;
                info.Price = this.Price;
                info.TruePrice = this.Price;
                info.ServiceTerm = this.ServiceTerm;
                info.ServiceTermUnit = this.ServiceTermUnit;
                info.Remark = this.Remark;
                info.BeginDate = this.BeginDate;
                info.PresentExp = this.PresentExp;
                info.PresentMoney = this.PresentMoney;
                info.PresentPoint = this.PresentPoint;
                info.ProductKind = this.ProductKind;
                info.SubTotal = this.SubTotal;
                info.SaleType = this.SaleType;
                info.OrderId = orderId;
                info.Property = this.Property;
                info.ProductCharacter = this.ProductCharacter;
                info.Weight = this.TotalWeight;
            }
            return info;
        }

        protected static decimal TaxRateCompute(bool needInvoice, ProductInfo productInfo, decimal truePrice)
        {
            if ((productInfo.IncludeTax == TaxRateType.IncludeTaxNoInvoiceFavourable) || (productInfo.IncludeTax == TaxRateType.IncludeTaxNoInvoiceNoFavourable))
            {
                if (!needInvoice && (productInfo.IncludeTax == TaxRateType.IncludeTaxNoInvoiceFavourable))
                {
                    truePrice = (truePrice * (100M - DataConverter.CDecimal(productInfo.TaxRate))) / 100M;
                }
                return truePrice;
            }
            if (needInvoice && (productInfo.IncludeTax == TaxRateType.BarringTaxNeedInvoiceAddTax))
            {
                truePrice = (truePrice * (100M + DataConverter.CDecimal(productInfo.TaxRate))) / 100M;
            }
            return truePrice;
        }

        public int Amount
        {
            get
            {
                return this.m_Amount;
            }
            set
            {
                this.m_Amount = value;
            }
        }

        public DateTime BeginDate
        {
            get
            {
                return this.m_BeginDate;
            }
            set
            {
                this.m_BeginDate = value;
            }
        }

        public int Id
        {
            get
            {
                return this.m_ID;
            }
            set
            {
                this.m_ID = value;
            }
        }

        public bool IsNull
        {
            get
            {
                return this.m_IsNull;
            }
            protected set
            {
                this.m_IsNull = value;
            }
        }

        public bool NeedTaxRateCompute
        {
            get
            {
                return this.m_NeedTaxRateCompute;
            }
            set
            {
                this.m_NeedTaxRateCompute = value;
            }
        }

        public int PresentExp
        {
            get
            {
                return this.m_PresentExp;
            }
            set
            {
                this.m_PresentExp = value;
            }
        }

        public decimal PresentMoney
        {
            get
            {
                return this.m_PresentMoney;
            }
            set
            {
                this.m_PresentMoney = value;
            }
        }

        public int PresentPoint
        {
            get
            {
                return this.m_PresentPoint;
            }
            set
            {
                this.m_PresentPoint = value;
            }
        }

        public decimal Price
        {
            get
            {
                return this.m_Price;
            }
            set
            {
                this.m_Price = value;
            }
        }

        public decimal PriceMarket
        {
            get
            {
                return this.m_PriceMarket;
            }
            set
            {
                this.m_PriceMarket = value;
            }
        }

        public EasyOne.Enumerations.ProductCharacter ProductCharacter
        {
            get
            {
                return this.m_ProductCharacter;
            }
            set
            {
                this.m_ProductCharacter = value;
            }
        }

        public int ProductId
        {
            get
            {
                return this.m_ProductId;
            }
            set
            {
                this.m_ProductId = value;
            }
        }

        public EasyOne.Enumerations.ProductKind ProductKind
        {
            get
            {
                return this.m_ProductKind;
            }
            set
            {
                this.m_ProductKind = value;
            }
        }

        public string ProductName
        {
            get
            {
                return this.m_ProductName;
            }
            set
            {
                this.m_ProductName = value;
            }
        }

        public string Property
        {
            get
            {
                return this.m_Property;
            }
            set
            {
                this.m_Property = value;
            }
        }

        public string Remark
        {
            get
            {
                return this.m_Remark;
            }
            set
            {
                this.m_Remark = value;
            }
        }

        public int SaleType
        {
            get
            {
                return this.m_SaleType;
            }
            set
            {
                this.m_SaleType = value;
            }
        }

        public int ServiceTerm
        {
            get
            {
                return this.m_ServiceTerm;
            }
            set
            {
                this.m_ServiceTerm = value;
            }
        }

        public EasyOne.Enumerations.ServiceTermUnit ServiceTermUnit
        {
            get
            {
                return this.m_ServiceTermUnit;
            }
            set
            {
                this.m_ServiceTermUnit = value;
            }
        }

        public decimal SubTotal
        {
            get
            {
                return this.m_SubTotal;
            }
            set
            {
                this.m_SubTotal = value;
            }
        }

        public string TableName
        {
            get
            {
                return this.m_TableName;
            }
            set
            {
                this.m_TableName = value;
            }
        }

        public double TotalWeight
        {
            get
            {
                return this.m_TotalWeight;
            }
            set
            {
                this.m_TotalWeight = value;
            }
        }

        public string Unit
        {
            get
            {
                return this.m_Unit;
            }
            set
            {
                this.m_Unit = value;
            }
        }

        public double Weight
        {
            get
            {
                return this.m_Weight;
            }
            set
            {
                this.m_Weight = value;
            }
        }
    }
}

