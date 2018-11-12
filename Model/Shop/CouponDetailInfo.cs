namespace EasyOne.Model.Shop
{
    using EasyOne.Model;
    using System;

    public class CouponDetailInfo : EasyOne.Model.Nullable
    {
        private EasyOne.Model.Shop.CouponInfo m_CouponInfo;
        private EasyOne.Model.Shop.CouponItemInfo m_CouponItemInfo;
        private string m_UserName;

        public CouponDetailInfo()
        {
            this.m_CouponInfo = new EasyOne.Model.Shop.CouponInfo();
            this.m_CouponItemInfo = new EasyOne.Model.Shop.CouponItemInfo();
        }

        public CouponDetailInfo(bool isNull)
        {
            base.IsNull = isNull;
            if (isNull)
            {
                this.m_CouponInfo = new EasyOne.Model.Shop.CouponInfo(true);
                this.m_CouponItemInfo = new EasyOne.Model.Shop.CouponItemInfo(true);
            }
            else
            {
                this.m_CouponInfo = new EasyOne.Model.Shop.CouponInfo();
                this.m_CouponItemInfo = new EasyOne.Model.Shop.CouponItemInfo();
            }
        }

        public EasyOne.Model.Shop.CouponInfo CouponInfo
        {
            get
            {
                return this.m_CouponInfo;
            }
            set
            {
                this.m_CouponInfo = value;
            }
        }

        public EasyOne.Model.Shop.CouponItemInfo CouponItemInfo
        {
            get
            {
                return this.m_CouponItemInfo;
            }
            set
            {
                this.m_CouponItemInfo = value;
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

