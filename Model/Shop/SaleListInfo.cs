namespace EasyOne.Model.Shop
{
    using EasyOne.Model;
    using System;

    public class SaleListInfo : EasyOne.Model.Nullable
    {
        private int m_Amount;
        private int m_ClientId;
        private string m_ClientName;
        private DateTime m_InputTime;
        private int m_ItemId;
        private int m_OrderId;
        private string m_OrderNum;
        private int m_PresentExp;
        private decimal m_Price;
        private int m_ProductId;
        private string m_ProductName;
        private string m_Property;
        private int m_SaleType;
        private decimal m_SubTotal;
        private string m_TableName;
        private decimal m_TruePrice;
        private string m_Unit;
        private string m_UserName;

        public SaleListInfo()
        {
        }

        public SaleListInfo(bool value)
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

        public int ClientId
        {
            get
            {
                return this.m_ClientId;
            }
            set
            {
                this.m_ClientId = value;
            }
        }

        public string ClientName
        {
            get
            {
                return this.m_ClientName;
            }
            set
            {
                this.m_ClientName = value;
            }
        }

        public DateTime InputTime
        {
            get
            {
                return this.m_InputTime;
            }
            set
            {
                this.m_InputTime = value;
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

        public string OrderNum
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

        public string UserName
        {
            get
            {
                return this.m_UserName;
            }
            set
            {
                this.m_UserName = value;
            }
        }
    }
}

