namespace EasyOne.Model.Shop
{
    using EasyOne.Model;
    using System;

    public class CouponItemInfo : EasyOne.Model.Nullable
    {
        private int m_CouponId;
        private string m_CouponNum;
        private int m_Id;
        private int m_OrderId;
        private int m_UserId;
        private int m_UseTimes;

        public CouponItemInfo()
        {
        }

        public CouponItemInfo(bool isNull)
        {
            base.IsNull = isNull;
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

        public string CouponNum
        {
            get
            {
                return this.m_CouponNum;
            }
            set
            {
                this.m_CouponNum = value;
            }
        }

        public int Id
        {
            get
            {
                return this.m_Id;
            }
            set
            {
                this.m_Id = value;
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

        public int UserId
        {
            get
            {
                return this.m_UserId;
            }
            set
            {
                this.m_UserId = value;
            }
        }

        public int UseTimes
        {
            get
            {
                return this.m_UseTimes;
            }
            set
            {
                this.m_UseTimes = value;
            }
        }
    }
}

