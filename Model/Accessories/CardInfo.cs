namespace EasyOne.Model.Accessories
{
    using EasyOne.Model;
    using System;

    [Serializable]
    public class CardInfo : EasyOne.Model.Nullable
    {
        private string m_AgentName;
        private int m_CardId;
        private string m_CardNum;
        private int m_CardType;
        private DateTime m_CreateTime;
        private DateTime m_EndDate;
        private decimal m_Money;
        private int m_OrderItemId;
        private string m_Password;
        private int m_ProductId;
        private string m_ProductName;
        private string m_TableName;
        private string m_UserName;
        private DateTime? m_UseTime;
        private int m_ValidNum;
        private int m_ValidUnit;

        public CardInfo()
        {
        }

        public CardInfo(bool value)
        {
            base.IsNull = value;
        }

        public string AgentName
        {
            get
            {
                return this.m_AgentName;
            }
            set
            {
                this.m_AgentName = value;
            }
        }

        public int CardId
        {
            get
            {
                return this.m_CardId;
            }
            set
            {
                this.m_CardId = value;
            }
        }

        public string CardNum
        {
            get
            {
                return this.m_CardNum;
            }
            set
            {
                this.m_CardNum = value;
            }
        }

        public int CardType
        {
            get
            {
                return this.m_CardType;
            }
            set
            {
                this.m_CardType = value;
            }
        }

        public DateTime CreateTime
        {
            get
            {
                return this.m_CreateTime;
            }
            set
            {
                this.m_CreateTime = value;
            }
        }

        public DateTime EndDate
        {
            get
            {
                return this.m_EndDate;
            }
            set
            {
                this.m_EndDate = value;
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

        public int OrderItemId
        {
            get
            {
                return this.m_OrderItemId;
            }
            set
            {
                this.m_OrderItemId = value;
            }
        }

        public string Password
        {
            get
            {
                return this.m_Password;
            }
            set
            {
                this.m_Password = value;
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

        public DateTime? UseTime
        {
            get
            {
                return this.m_UseTime;
            }
            set
            {
                this.m_UseTime = value;
            }
        }

        public int ValidNum
        {
            get
            {
                return this.m_ValidNum;
            }
            set
            {
                this.m_ValidNum = value;
            }
        }

        public int ValidUnit
        {
            get
            {
                return this.m_ValidUnit;
            }
            set
            {
                this.m_ValidUnit = value;
            }
        }
    }
}

