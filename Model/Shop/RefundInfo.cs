namespace EasyOne.Model.Shop
{
    using EasyOne.Model;
    using System;

    public class RefundInfo : EasyOne.Model.Nullable
    {
        private int m_ClientId;
        private string m_ClientName;
        private string m_Email;
        private decimal m_HandlingCharge;
        private decimal m_Money;
        private decimal m_MoneyReceipt;
        private decimal m_MoneyTotal;
        private int m_OrderId;
        private string m_OrderNum;
        private DateTime m_RefundData;
        private int m_RefundType;
        private string m_Remark;
        private string m_UserName;

        public RefundInfo()
        {
        }

        public RefundInfo(bool value)
        {
            base.IsNull = value;
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

        public string Email
        {
            get
            {
                return this.m_Email;
            }
            set
            {
                this.m_Email = value;
            }
        }

        public decimal HandlingCharge
        {
            get
            {
                return this.m_HandlingCharge;
            }
            set
            {
                this.m_HandlingCharge = value;
            }
        }

        public decimal Money
        {
            get
            {
                return this.m_Money;
            }
            set
            {
                this.m_Money = value;
            }
        }

        public decimal MoneyReceipt
        {
            get
            {
                return this.m_MoneyReceipt;
            }
            set
            {
                this.m_MoneyReceipt = value;
            }
        }

        public decimal MoneyTotal
        {
            get
            {
                return this.m_MoneyTotal;
            }
            set
            {
                this.m_MoneyTotal = value;
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

        public DateTime RefundData
        {
            get
            {
                return this.m_RefundData;
            }
            set
            {
                this.m_RefundData = value;
            }
        }

        public int RefundType
        {
            get
            {
                return this.m_RefundType;
            }
            set
            {
                this.m_RefundType = value;
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

