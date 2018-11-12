namespace EasyOne.Model.Accessories
{
    using EasyOne.Model;
    using System;

    public class PaymentLogInfo : EasyOne.Model.Nullable
    {
        private decimal m_MoneyPay;
        private decimal m_MoneyTrue;
        private int m_OrderId;
        private int m_PaymentLogId;
        private string m_PaymentNum;
        private DateTime? m_PayTime;
        private int m_PlatformId;
        private string m_PlatformInfo;
        private int m_Point;
        private string m_Remark;
        private int m_Status;
        private DateTime? m_SuccessTime;
        private string m_UserName;

        public PaymentLogInfo()
        {
        }

        public PaymentLogInfo(bool value)
        {
            base.IsNull = value;
        }

        public decimal MoneyPay
        {
            get
            {
                return this.m_MoneyPay;
            }
            set
            {
                this.m_MoneyPay = value;
            }
        }

        public decimal MoneyTrue
        {
            get
            {
                return this.m_MoneyTrue;
            }
            set
            {
                this.m_MoneyTrue = value;
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

        public int PaymentLogId
        {
            get
            {
                return this.m_PaymentLogId;
            }
            set
            {
                this.m_PaymentLogId = value;
            }
        }

        public string PaymentNum
        {
            get
            {
                return this.m_PaymentNum;
            }
            set
            {
                this.m_PaymentNum = value;
            }
        }

        public DateTime? PayTime
        {
            get
            {
                return this.m_PayTime;
            }
            set
            {
                this.m_PayTime = value;
            }
        }

        public int PlatformId
        {
            get
            {
                return this.m_PlatformId;
            }
            set
            {
                this.m_PlatformId = value;
            }
        }

        public string PlatformInfo
        {
            get
            {
                return this.m_PlatformInfo;
            }
            set
            {
                this.m_PlatformInfo = value;
            }
        }

        public int Point
        {
            get
            {
                return this.m_Point;
            }
            set
            {
                this.m_Point = value;
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

        public int Status
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

        public DateTime? SuccessTime
        {
            get
            {
                return this.m_SuccessTime;
            }
            set
            {
                this.m_SuccessTime = value;
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

