namespace EasyOne.Model.Shop
{
    using EasyOne.Enumerations;
    using EasyOne.Model;
    using System;

    [Serializable]
    public class OrderInfo : EasyOne.Model.Nullable
    {
        private string m_Address;
        private string m_AdminName;
        private string m_AgentName;
        private DateTime m_BeginDate;
        private decimal m_Charge_Deliver;
        private int m_ClientId;
        private string m_ClientName;
        private string m_ContacterName;
        private int m_CouponId;
        private EasyOne.Enumerations.DeliverStatus m_DeliverStatus;
        private int m_DeliverType;
        private string m_DeliveryTime;
        private double m_Discount_Payment;
        private string m_Email;
        private bool m_EnableDownload;
        private string m_Functionary;
        private DateTime m_InputTime;
        private string m_InvoiceContent;
        private bool m_Invoiced;
        private string m_Memo;
        private string m_Mobile;
        private decimal m_MoneyGoods;
        private decimal m_MoneyReceipt;
        private decimal m_MoneyTotal;
        private bool m_NeedInvoice;
        private int m_OrderId;
        private string m_OrderNum;
        private int m_OrderType;
        private EasyOne.Enumerations.OutOfStockProject m_OutOfStockProject;
        private int m_PaymentType;
        private string m_Phone;
        private int m_PresentExp;
        private decimal m_PresentMoney;
        private int m_PresentPoint;
        private string m_Remark;
        private OrderStatus m_Status;
        private string m_UserName;
        private string m_ZipCode;

        public OrderInfo()
        {
        }

        public OrderInfo(bool value)
        {
            base.IsNull = value;
        }

        public string Address
        {
            get
            {
                return this.m_Address;
            }
            set
            {
                this.m_Address = value;
            }
        }

        public string AdminName
        {
            get
            {
                return this.m_AdminName;
            }
            set
            {
                this.m_AdminName = value;
            }
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

        public decimal ChargeDeliver
        {
            get
            {
                return this.m_Charge_Deliver;
            }
            set
            {
                this.m_Charge_Deliver = value;
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

        public int CouponId
        {
            get
            {
                return this.m_CouponId;
            }
            set
            {
                this.m_CouponId = value;
            }
        }

        public EasyOne.Enumerations.DeliverStatus DeliverStatus
        {
            get
            {
                return this.m_DeliverStatus;
            }
            set
            {
                this.m_DeliverStatus = value;
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

        public double DiscountPayment
        {
            get
            {
                return this.m_Discount_Payment;
            }
            set
            {
                this.m_Discount_Payment = value;
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

        public bool EnableDownload
        {
            get
            {
                return this.m_EnableDownload;
            }
            set
            {
                this.m_EnableDownload = value;
            }
        }

        public string Functionary
        {
            get
            {
                return this.m_Functionary;
            }
            set
            {
                this.m_Functionary = value;
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

        public bool Invoiced
        {
            get
            {
                return this.m_Invoiced;
            }
            set
            {
                this.m_Invoiced = value;
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

        public string Mobile
        {
            get
            {
                return this.m_Mobile;
            }
            set
            {
                this.m_Mobile = value;
            }
        }

        public decimal MoneyGoods
        {
            get
            {
                return this.m_MoneyGoods;
            }
            set
            {
                this.m_MoneyGoods = value;
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
                this.m_MoneyTotal = decimal.Round(value, 2);
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

        public int OrderType
        {
            get
            {
                return this.m_OrderType;
            }
            set
            {
                this.m_OrderType = value;
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

        public string Phone
        {
            get
            {
                return this.m_Phone;
            }
            set
            {
                this.m_Phone = value;
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

        public decimal PresentMoney
        {
            get
            {
                return this.m_PresentMoney;
            }
            set
            {
                this.m_PresentMoney = value;
            }
        }

        public int PresentPoint
        {
            get
            {
                return this.m_PresentPoint;
            }
            set
            {
                this.m_PresentPoint = value;
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

        public OrderStatus Status
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

        public string ZipCode
        {
            get
            {
                return this.m_ZipCode;
            }
            set
            {
                this.m_ZipCode = value;
            }
        }
    }
}

