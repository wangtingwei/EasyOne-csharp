namespace EasyOne.Model.Shop
{
    using EasyOne.Model;
    using System;

    public class DeliverItemInfo : EasyOne.Model.Nullable
    {
        private int m_ClientId;
        private string m_ClientName;
        private string m_ContacterName;
        private int m_CourierId;
        private DateTime m_DeliverDate;
        private int m_DeliverDirection;
        private int m_DeliverId;
        private int m_DeliverTypeId;
        private string m_DeliverTypeName;
        private string m_Email;
        private string m_ExpressNumber;
        private string m_HandlerName;
        private string m_Inputer;
        private string m_Memo;
        private decimal m_MoneyReceipt;
        private decimal m_MoneyTotal;
        private int m_OrderId;
        private string m_OrderNum;
        private bool m_Received;
        private string m_Remark;
        private string m_UserName;

        public DeliverItemInfo()
        {
        }

        public DeliverItemInfo(bool value)
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

        public string ContacterName
        {
            get
            {
                return this.m_ContacterName;
            }
            set
            {
                this.m_ContacterName = value;
            }
        }

        public int CourierId
        {
            get
            {
                return this.m_CourierId;
            }
            set
            {
                this.m_CourierId = value;
            }
        }

        public DateTime DeliverDate
        {
            get
            {
                return this.m_DeliverDate;
            }
            set
            {
                this.m_DeliverDate = value;
            }
        }

        public int DeliverDirection
        {
            get
            {
                return this.m_DeliverDirection;
            }
            set
            {
                this.m_DeliverDirection = value;
            }
        }

        public int DeliverId
        {
            get
            {
                return this.m_DeliverId;
            }
            set
            {
                this.m_DeliverId = value;
            }
        }

        public int DeliverTypeId
        {
            get
            {
                return this.m_DeliverTypeId;
            }
            set
            {
                this.m_DeliverTypeId = value;
            }
        }

        public string DeliverTypeName
        {
            get
            {
                return this.m_DeliverTypeName;
            }
            set
            {
                this.m_DeliverTypeName = value;
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

        public string ExpressNumber
        {
            get
            {
                return this.m_ExpressNumber;
            }
            set
            {
                this.m_ExpressNumber = value;
            }
        }

        public string HandlerName
        {
            get
            {
                return this.m_HandlerName;
            }
            set
            {
                this.m_HandlerName = value;
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

        public bool Received
        {
            get
            {
                return this.m_Received;
            }
            set
            {
                this.m_Received = value;
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

