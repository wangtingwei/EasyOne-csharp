namespace EasyOne.Model.Shop
{
    using EasyOne.Model;
    using System;

    public class SaleCountInfo : EasyOne.Model.Nullable
    {
        private int m_NoDeliverAmount;
        private int m_ProductId;
        private string m_ProductName;
        private decimal m_SubTotal;
        private string m_TableName;
        private int m_TotalAmount;
        private string m_Unit;

        public SaleCountInfo()
        {
        }

        public SaleCountInfo(bool value)
        {
            base.IsNull = value;
        }

        public int NoDeliverAmount
        {
            get
            {
                return this.m_NoDeliverAmount;
            }
            set
            {
                this.m_NoDeliverAmount = value;
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

        public int TotalAmount
        {
            get
            {
                return this.m_TotalAmount;
            }
            set
            {
                this.m_TotalAmount = value;
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

