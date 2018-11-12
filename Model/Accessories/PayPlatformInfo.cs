namespace EasyOne.Model.Accessories
{
    using EasyOne.Model;
    using System;

    public class PayPlatformInfo : EasyOne.Model.Nullable
    {
        private string m_AccountsId;
        private bool m_IsDefault;
        private bool m_IsDisabled;
        private string m_Md5;
        private int m_OrderId;
        private int m_PayPlatformId;
        private string m_PayPlatformName;
        private double m_Rate;

        public PayPlatformInfo()
        {
        }

        public PayPlatformInfo(bool value)
        {
            base.IsNull = value;
        }

        public string AccountsId
        {
            get
            {
                return this.m_AccountsId;
            }
            set
            {
                this.m_AccountsId = value;
            }
        }

        public bool IsDefault
        {
            get
            {
                return this.m_IsDefault;
            }
            set
            {
                this.m_IsDefault = value;
            }
        }

        public bool IsDisabled
        {
            get
            {
                return this.m_IsDisabled;
            }
            set
            {
                this.m_IsDisabled = value;
            }
        }

        public string MD5
        {
            get
            {
                return this.m_Md5;
            }
            set
            {
                this.m_Md5 = value;
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

        public int PayPlatformId
        {
            get
            {
                return this.m_PayPlatformId;
            }
            set
            {
                this.m_PayPlatformId = value;
            }
        }

        public string PayPlatformName
        {
            get
            {
                return this.m_PayPlatformName;
            }
            set
            {
                this.m_PayPlatformName = value;
            }
        }

        public double Rate
        {
            get
            {
                return this.m_Rate;
            }
            set
            {
                this.m_Rate = value;
            }
        }
    }
}

