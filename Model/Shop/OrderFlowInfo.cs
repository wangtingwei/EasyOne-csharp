namespace EasyOne.Model.Shop
{
    using EasyOne.Enumerations;
    using EasyOne.Model.Crm;
    using System;

    [Serializable]
    public class OrderFlowInfo : AddressInfo
    {
        private string m_AgentName;
        private DateTime m_BeginDate;
        private int m_DeliverType;
        private string m_DeliveryTime;
        private string m_Email;
        private string m_InvoiceContent;
        private bool m_NeedInvoice;
        private int m_OrderId;
        private EasyOne.Enumerations.OutOfStockProject m_OutOfStockProject;
        private int m_PaymentType;
        private int m_PresentId;
        private string m_Remark;
        private string m_ShoppingCartId;

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

        public DateTime BeginDate
        {
            get
            {
                return this.m_BeginDate;
            }
            set
            {
                this.m_BeginDate = value;
            }
        }

        public int DeliverType
        {
            get
            {
                return this.m_DeliverType;
            }
            set
            {
                this.m_DeliverType = value;
            }
        }

        public string DeliveryTime
        {
            get
            {
                return this.m_DeliveryTime;
            }
            set
            {
                this.m_DeliveryTime = value;
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

        public bool NeedInvoice
        {
            get
            {
                return this.m_NeedInvoice;
            }
            set
            {
                this.m_NeedInvoice = value;
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

        public EasyOne.Enumerations.OutOfStockProject OutOfStockProject
        {
            get
            {
                return this.m_OutOfStockProject;
            }
            set
            {
                this.m_OutOfStockProject = value;
            }
        }

        public int PaymentType
        {
            get
            {
                return this.m_PaymentType;
            }
            set
            {
                this.m_PaymentType = value;
            }
        }

        public int PresentId
        {
            get
            {
                return this.m_PresentId;
            }
            set
            {
                this.m_PresentId = value;
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

        public string ShoppingCartId
        {
            get
            {
                return this.m_ShoppingCartId;
            }
            set
            {
                this.m_ShoppingCartId = value;
            }
        }
    }
}

