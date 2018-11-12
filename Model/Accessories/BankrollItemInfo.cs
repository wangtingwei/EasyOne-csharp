namespace EasyOne.Model.Accessories
{
    using EasyOne.Enumerations;
    using EasyOne.Model;
    using System;

    [Serializable]
    public class BankrollItemInfo : EasyOne.Model.Nullable
    {
        private string m_Bank;
        private int m_ClientId;
        private string m_ClientName;
        private int m_CurrencyType;
        private DateTime? m_DateAndTime;
        private int m_EBankId;
        private string m_Inputer;
        private string m_IP;
        private int m_ItemId;
        private DateTime? m_LogTime;
        private string m_Memo;
        private decimal m_Money;
        private int m_MoneyType;
        private int m_OrderId;
        private int m_PaymentId;
        private string m_Remark;
        private BankrollItemStatus m_Status;
        private string m_UserName;

        public BankrollItemInfo()
        {
            this.m_Status = BankrollItemStatus.Confirm;
        }

        public BankrollItemInfo(bool value)
        {
            this.m_Status = BankrollItemStatus.Confirm;
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

        public int CurrencyType
        {
            get
            {
                if (this.m_CurrencyType <= 0)
                {
                    return 1;
                }
                return this.m_CurrencyType;
            }
            set
            {
                this.m_CurrencyType = value;
            }
        }

        public DateTime? DateAndTime
        {
            get
            {
                return this.m_DateAndTime;
            }
            set
            {
                this.m_DateAndTime = value;
            }
        }

        public int EBankId
        {
            get
            {
                return this.m_EBankId;
            }
            set
            {
                this.m_EBankId = value;
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

        public string IP
        {
            get
            {
                return this.m_IP;
            }
            set
            {
                this.m_IP = value;
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

        public DateTime? LogTime
        {
            get
            {
                return this.m_LogTime;
            }
            set
            {
                this.m_LogTime = value;
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

        public int MoneyType
        {
            get
            {
                return this.m_MoneyType;
            }
            set
            {
                this.m_MoneyType = value;
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

        public int PaymentId
        {
            get
            {
                return this.m_PaymentId;
            }
            set
            {
                this.m_PaymentId = value;
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

        public BankrollItemStatus Status
        {
            get
            {
                return this.m_Status;
            }
            set
            {
                this.m_Status = value;
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

