namespace EasyOne.Model.Shop
{
    using EasyOne.Model;
    using System;

    [Serializable]
    public class ProductDataInfo : EasyOne.Model.Nullable
    {
        private int m_AlarmNum;
        private int m_BuyTimes;
        private int m_Id;
        private bool m_IsValid;
        private int m_OrderNum;
        private EasyOne.Model.Shop.PriceInfo m_PriceInfo;
        private int m_ProductId;
        private string m_PropertyValue;
        private int m_Stocks;
        private string m_TableName;

        public ProductDataInfo()
        {
            if (this.m_PriceInfo == null)
            {
                this.m_PriceInfo = new EasyOne.Model.Shop.PriceInfo(true);
            }
        }

        public ProductDataInfo(bool value)
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

        public int Id
        {
            get
            {
                return this.m_Id;
            }
            set
            {
                this.m_Id = value;
            }
        }

        public bool IsValid
        {
            get
            {
                return this.m_IsValid;
            }
            set
            {
                this.m_IsValid = value;
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

        public string PropertyValue
        {
            get
            {
                return this.m_PropertyValue;
            }
            set
            {
                this.m_PropertyValue = value;
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
    }
}

