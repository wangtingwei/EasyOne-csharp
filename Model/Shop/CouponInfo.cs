namespace EasyOne.Model.Shop
{
    using EasyOne.Enumerations;
    using EasyOne.Model;
    using System;

    public class CouponInfo : EasyOne.Model.Nullable
    {
        private DateTime m_BeginDate;
        private string m_CouponCreateContent;
        private EasyOne.Enumerations.CouponCreateType m_CouponCreateType;
        private int m_CouponId;
        private string m_CouponName;
        private string m_CouponNumPattern;
        private DateTime m_EndDate;
        private int m_LimitNum;
        private decimal m_Money;
        private decimal m_OrderTotalMoney;
        private string m_ProductLimitContent;
        private EasyOne.Enumerations.ProductLimitType m_ProductLimitType;
        private int m_State;
        private DateTime m_UseEndDate;
        private string m_UserGroup;

        public CouponInfo()
        {
        }

        public CouponInfo(bool isNull)
        {
            base.IsNull = isNull;
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

        public string CouponCreateContent
        {
            get
            {
                return this.m_CouponCreateContent;
            }
            set
            {
                this.m_CouponCreateContent = value;
            }
        }

        public EasyOne.Enumerations.CouponCreateType CouponCreateType
        {
            get
            {
                return this.m_CouponCreateType;
            }
            set
            {
                this.m_CouponCreateType = value;
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

        public string CouponName
        {
            get
            {
                return this.m_CouponName;
            }
            set
            {
                this.m_CouponName = value;
            }
        }

        public string CouponNumPattern
        {
            get
            {
                return this.m_CouponNumPattern;
            }
            set
            {
                this.m_CouponNumPattern = value;
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

        public int LimitNum
        {
            get
            {
                return this.m_LimitNum;
            }
            set
            {
                this.m_LimitNum = value;
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

        public decimal OrderTotalMoney
        {
            get
            {
                return this.m_OrderTotalMoney;
            }
            set
            {
                this.m_OrderTotalMoney = value;
            }
        }

        public string ProductLimitContent
        {
            get
            {
                return this.m_ProductLimitContent;
            }
            set
            {
                this.m_ProductLimitContent = value;
            }
        }

        public EasyOne.Enumerations.ProductLimitType ProductLimitType
        {
            get
            {
                return this.m_ProductLimitType;
            }
            set
            {
                this.m_ProductLimitType = value;
            }
        }

        public int State
        {
            get
            {
                return this.m_State;
            }
            set
            {
                this.m_State = value;
            }
        }

        public DateTime UseEndDate
        {
            get
            {
                return this.m_UseEndDate;
            }
            set
            {
                this.m_UseEndDate = value;
            }
        }

        public string UserGroup
        {
            get
            {
                return this.m_UserGroup;
            }
            set
            {
                this.m_UserGroup = value;
            }
        }
    }
}

