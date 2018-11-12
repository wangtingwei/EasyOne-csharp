namespace EasyOne.Model.Shop
{
    using EasyOne.Enumerations;
    using EasyOne.Model;
    using System;
    using System.Data;

    [Serializable]
    public class ProductInfo : EasyOne.Model.Nullable
    {
        private int m_AlarmNum;
        private string m_BarCode;
        private int m_BuyTimes;
        private string m_DependentProducts;
        private double m_Discount;
        private string m_DownloadUrl;
        private bool m_EnableBuyWhenOutofstock;
        private bool m_EnableSale;
        private bool m_EnableSingleSell;
        private bool m_EnableWholesale;
        private DataTable m_Fields;
        private TaxRateType m_IncludeTax;
        private bool m_IsBest;
        private bool m_IsHot;
        private bool m_IsNew;
        private string m_Keyword;
        private int m_LimitNum;
        private int m_Minimum;
        private int m_MinNumber;
        private int m_OrderNum;
        private int m_PresentExp;
        private int m_PresentId;
        private decimal m_PresentMoney;
        private int m_PresentNumber;
        private int m_PresentPoint;
        private EasyOne.Model.Shop.PriceInfo m_PriceInfo;
        private decimal m_PriceMarket;
        private string m_ProducerName;
        private EasyOne.Enumerations.ProductCharacter m_ProductCharacter;
        private string m_ProductExplain;
        private int m_ProductId;
        private string m_ProductIntro;
        private EasyOne.Enumerations.ProductKind m_ProductKind;
        private string m_ProductName;
        private string m_ProductNum;
        private string m_ProductPic;
        private string m_ProductThumb;
        private EasyOne.Enumerations.ProductType m_ProductType;
        private string m_Properties;
        private string m_Remark;
        private int m_SalePromotionType;
        private int m_ServiceTerm;
        private EasyOne.Enumerations.ServiceTermUnit m_ServiceTermUnit;
        private int m_Stars;
        private int m_Stocks;
        private EasyOne.Enumerations.StocksProject m_StocksProject;
        private string m_TableName;
        private double m_TaxRate;
        private string m_TrademarkName;
        private string m_Unit;
        private double m_Weight;

        public ProductInfo()
        {
            if (this.m_PriceInfo == null)
            {
                this.m_PriceInfo = new EasyOne.Model.Shop.PriceInfo(true);
            }
        }

        public ProductInfo(bool value)
        {
            base.IsNull = value;
            if (this.m_PriceInfo == null)
            {
                this.m_PriceInfo = new EasyOne.Model.Shop.PriceInfo(true);
            }
        }

        public int AlarmNum
        {
            get
            {
                return this.m_AlarmNum;
            }
            set
            {
                this.m_AlarmNum = value;
            }
        }

        public string BarCode
        {
            get
            {
                return this.m_BarCode;
            }
            set
            {
                this.m_BarCode = value;
            }
        }

        public int BuyTimes
        {
            get
            {
                return this.m_BuyTimes;
            }
            set
            {
                this.m_BuyTimes = value;
            }
        }

        public string DependentProducts
        {
            get
            {
                return this.m_DependentProducts;
            }
            set
            {
                this.m_DependentProducts = value;
            }
        }

        public double Discount
        {
            get
            {
                return this.m_Discount;
            }
            set
            {
                this.m_Discount = value;
            }
        }

        public string DownloadUrl
        {
            get
            {
                return this.m_DownloadUrl;
            }
            set
            {
                this.m_DownloadUrl = value;
            }
        }

        public bool EnableBuyWhenOutofstock
        {
            get
            {
                return this.m_EnableBuyWhenOutofstock;
            }
            set
            {
                this.m_EnableBuyWhenOutofstock = value;
            }
        }

        public bool EnableSale
        {
            get
            {
                return this.m_EnableSale;
            }
            set
            {
                this.m_EnableSale = value;
            }
        }

        public bool EnableSingleSell
        {
            get
            {
                return this.m_EnableSingleSell;
            }
            set
            {
                this.m_EnableSingleSell = value;
            }
        }

        public bool EnableWholesale
        {
            get
            {
                return this.m_EnableWholesale;
            }
            set
            {
                this.m_EnableWholesale = value;
            }
        }

        public DataTable Fields
        {
            get
            {
                return this.m_Fields;
            }
            set
            {
                this.m_Fields = value;
            }
        }

        public TaxRateType IncludeTax
        {
            get
            {
                return this.m_IncludeTax;
            }
            set
            {
                this.m_IncludeTax = value;
            }
        }

        public bool IsBest
        {
            get
            {
                return this.m_IsBest;
            }
            set
            {
                this.m_IsBest = value;
            }
        }

        public bool IsHot
        {
            get
            {
                return this.m_IsHot;
            }
            set
            {
                this.m_IsHot = value;
            }
        }

        public bool IsNew
        {
            get
            {
                return this.m_IsNew;
            }
            set
            {
                this.m_IsNew = value;
            }
        }

        public string Keyword
        {
            get
            {
                return this.m_Keyword;
            }
            set
            {
                this.m_Keyword = value;
            }
        }

        public int LimitNum
        {
            get
            {
                return this.m_LimitNum;
            }
            set
            {
                this.m_LimitNum = value;
            }
        }

        public int Minimum
        {
            get
            {
                return this.m_Minimum;
            }
            set
            {
                this.m_Minimum = value;
            }
        }

        public int MinNumber
        {
            get
            {
                return this.m_MinNumber;
            }
            set
            {
                this.m_MinNumber = value;
            }
        }

        public int OrderNum
        {
            get
            {
                return this.m_OrderNum;
            }
            set
            {
                this.m_OrderNum = value;
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

        public int PresentId
        {
            get
            {
                return this.m_PresentId;
            }
            set
            {
                this.m_PresentId = value;
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

        public int PresentNumber
        {
            get
            {
                return this.m_PresentNumber;
            }
            set
            {
                this.m_PresentNumber = value;
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

        public EasyOne.Model.Shop.PriceInfo PriceInfo
        {
            get
            {
                return this.m_PriceInfo;
            }
            set
            {
                this.m_PriceInfo = value;
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

        public string ProducerName
        {
            get
            {
                return this.m_ProducerName;
            }
            set
            {
                this.m_ProducerName = value;
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

        public string ProductExplain
        {
            get
            {
                return this.m_ProductExplain;
            }
            set
            {
                this.m_ProductExplain = value;
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

        public string ProductIntro
        {
            get
            {
                return this.m_ProductIntro;
            }
            set
            {
                this.m_ProductIntro = value;
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

        public string ProductNum
        {
            get
            {
                return this.m_ProductNum;
            }
            set
            {
                this.m_ProductNum = value;
            }
        }

        public string ProductPic
        {
            get
            {
                return this.m_ProductPic;
            }
            set
            {
                this.m_ProductPic = value;
            }
        }

        public string ProductThumb
        {
            get
            {
                return this.m_ProductThumb;
            }
            set
            {
                this.m_ProductThumb = value;
            }
        }

        public EasyOne.Enumerations.ProductType ProductType
        {
            get
            {
                return this.m_ProductType;
            }
            set
            {
                this.m_ProductType = value;
            }
        }

        public string Properties
        {
            get
            {
                return this.m_Properties;
            }
            set
            {
                this.m_Properties = value;
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

        public int SalePromotionType
        {
            get
            {
                return this.m_SalePromotionType;
            }
            set
            {
                this.m_SalePromotionType = value;
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

        public int Stars
        {
            get
            {
                return this.m_Stars;
            }
            set
            {
                this.m_Stars = value;
            }
        }

        public int Stocks
        {
            get
            {
                return this.m_Stocks;
            }
            set
            {
                this.m_Stocks = value;
            }
        }

        public EasyOne.Enumerations.StocksProject StocksProject
        {
            get
            {
                return this.m_StocksProject;
            }
            set
            {
                this.m_StocksProject = value;
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

        public double TaxRate
        {
            get
            {
                return this.m_TaxRate;
            }
            set
            {
                this.m_TaxRate = value;
            }
        }

        public string TrademarkName
        {
            get
            {
                return this.m_TrademarkName;
            }
            set
            {
                this.m_TrademarkName = value;
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

