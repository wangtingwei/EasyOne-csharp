namespace EasyOne.Model.Shop
{
    using EasyOne.Model;
    using System;

    [Serializable]
    public class RemittanceInfo : EasyOne.Model.Nullable
    {
        private string m_Bank;
        private int m_ClientId;
        private string m_ClientName;
        private string m_Email;
        private decimal m_Money;
        private decimal m_MoneyReceipt;
        private decimal m_MoneyTotal;
        private int m_OrderId;
        private string m_OrderNum;
        private string m_Remark;
        private DateTime m_RemittanceData;
        private string m_UserName;

        public RemittanceInfo()
        {
        }

        public RemittanceInfo(bool value)
        {
            base.IsNull = value;
        }

        public string Bank
        {
            get
            {
                return this.m_Bank;
            }
            set
            {
                this.m_Bank = value;
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

        public DateTime RemittanceData
        {
            get
            {
                return this.m_RemittanceData;
            }
            set
            {
                this.m_RemittanceData = value;
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

