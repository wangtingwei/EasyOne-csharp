namespace EasyOne.Model.Shop
{
    using EasyOne.Enumerations;
    using EasyOne.Model;
    using System;

    [Serializable]
    public class PresentInfo : EasyOne.Model.Nullable
    {
        private int m_AlarmNum;
        private int m_BuyTimes;
        private string m_DownloadUrl;
        private int m_OrderNum;
        private string m_PresentExplain;
        private int m_PresentId;
        private string m_PresentIntrol;
        private string m_PresentName;
        private string m_PresentNum;
        private string m_PresentPic;
        private string m_PresentThumb;
        private decimal m_Price;
        private decimal m_PriceMarket;
        private EasyOne.Enumerations.ProductCharacter m_ProductCharacter;
        private string m_Remark;
        private int m_ServiceTerm;
        private EasyOne.Enumerations.ServiceTermUnit m_ServiceTermUnit;
        private int m_Stocks;
        private string m_Unit;
        private double m_Weight;

        public PresentInfo()
        {
        }

        public PresentInfo(bool value)
        {
            base.IsNull = value;
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

        public string PresentExplain
        {
            get
            {
                return this.m_PresentExplain;
            }
            set
            {
                this.m_PresentExplain = value;
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

        public string PresentIntro
        {
            get
            {
                return this.m_PresentIntrol;
            }
            set
            {
                this.m_PresentIntrol = value;
            }
        }

        public string PresentName
        {
            get
            {
                return this.m_PresentName;
            }
            set
            {
                this.m_PresentName = value;
            }
        }

        public string PresentNum
        {
            get
            {
                return this.m_PresentNum;
            }
            set
            {
                this.m_PresentNum = value;
            }
        }

        public string PresentPic
        {
            get
            {
                return this.m_PresentPic;
            }
            set
            {
                this.m_PresentPic = value;
            }
        }

        public string PresentThumb
        {
            get
            {
                return this.m_PresentThumb;
            }
            set
            {
                this.m_PresentThumb = value;
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

