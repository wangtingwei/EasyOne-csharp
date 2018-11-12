namespace EasyOne.Model.Shop
{
    using EasyOne.Model;
    using System;

    [Serializable]
    public class StockItemInfo : EasyOne.Model.Nullable
    {
        private int m_Amount;
        private int m_ItemId;
        private decimal m_Price;
        private int m_ProductId;
        private string m_ProductName;
        private string m_ProductNum;
        private string m_Property;
        private int m_StockId;
        private string m_TableName;
        private string m_Unit;

        public StockItemInfo()
        {
        }

        public StockItemInfo(bool value)
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

        public int StockId
        {
            get
            {
                return this.m_StockId;
            }
            set
            {
                this.m_StockId = value;
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
    }
}

