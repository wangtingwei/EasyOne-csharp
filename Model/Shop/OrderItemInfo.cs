namespace EasyOne.Model.Shop
{
    using EasyOne.Enumerations;
    using EasyOne.Model;
    using System;

    [Serializable]
    public class OrderItemInfo : EasyOne.Model.Nullable
    {
        private int m_Amount;
        private DateTime m_BeginDate;
        private int m_ItemId;
        private int m_OrderId;
        private int m_PresentExp;
        private decimal m_PresentMoney;
        private int m_PresentPoint;
        private decimal m_Price;
        private decimal m_PriceAgent;
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
        private decimal m_TruePrice;
        private string m_Unit;
        private double m_Weight;

        public OrderItemInfo()
        {
        }

        public OrderItemInfo(bool value)
        {
            base.IsNull = value;
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

        public int ItemId
        {
            get
            {
                return this.m_ItemId;
            }
            set
            {
                this.m_ItemId = value;
            }
        }

        public int OrderId
        {
            get
            {
                return this.m_OrderId;
            }
            set
            {
                this.m_OrderId = value;
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

        public decimal PriceAgent
        {
            get
            {
                return this.m_PriceAgent;
            }
            set
            {
                this.m_PriceAgent = value;
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

        public decimal TruePrice
        {
            get
            {
                return this.m_TruePrice;
            }
            set
            {
                this.m_TruePrice = value;
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

