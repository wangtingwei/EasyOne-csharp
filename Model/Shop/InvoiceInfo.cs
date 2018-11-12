namespace EasyOne.Model.Shop
{
    using EasyOne.Model;
    using System;

    public class InvoiceInfo : EasyOne.Model.Nullable
    {
        private int m_ClientId;
        private string m_ClientName;
        private string m_Drawer;
        private string m_Email;
        private string m_Inputer;
        private DateTime m_InputTime;
        private string m_InvoiceContent;
        private DateTime m_InvoiceDate;
        private int m_InvoiceId;
        private string m_InvoiceNum;
        private string m_InvoiceTitle;
        private int m_InvoiceType;
        private string m_Memo;
        private decimal m_MoneyReceipt;
        private decimal m_MoneyTotal;
        private int m_OrderId;
        private string m_OrderNum;
        private decimal m_TotalMoney;
        private string m_UserName;

        public InvoiceInfo()
        {
        }

        public InvoiceInfo(bool value)
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

        public string Drawer
        {
            get
            {
                return this.m_Drawer;
            }
            set
            {
                this.m_Drawer = value;
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

        public string Inputer
        {
            get
            {
                return this.m_Inputer;
            }
            set
            {
                this.m_Inputer = value;
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

        public string InvoiceContent
        {
            get
            {
                return this.m_InvoiceContent;
            }
            set
            {
                this.m_InvoiceContent = value;
            }
        }

        public DateTime InvoiceDate
        {
            get
            {
                return this.m_InvoiceDate;
            }
            set
            {
                this.m_InvoiceDate = value;
            }
        }

        public int InvoiceId
        {
            get
            {
                return this.m_InvoiceId;
            }
            set
            {
                this.m_InvoiceId = value;
            }
        }

        public string InvoiceNum
        {
            get
            {
                return this.m_InvoiceNum;
            }
            set
            {
                this.m_InvoiceNum = value;
            }
        }

        public string InvoiceTitle
        {
            get
            {
                return this.m_InvoiceTitle;
            }
            set
            {
                this.m_InvoiceTitle = value;
            }
        }

        public int InvoiceType
        {
            get
            {
                return this.m_InvoiceType;
            }
            set
            {
                this.m_InvoiceType = value;
            }
        }

        public string Memo
        {
            get
            {
                return this.m_Memo;
            }
            set
            {
                this.m_Memo = value;
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

        public decimal TotalMoney
        {
            get
            {
                return this.m_TotalMoney;
            }
            set
            {
                this.m_TotalMoney = value;
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

