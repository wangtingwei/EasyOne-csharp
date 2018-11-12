namespace EasyOne.Shop
{
    using EasyOne.Common;
    using EasyOne.IDal.Shop;
    using EasyOne.Model.Shop;
    using System;
    using EasyOne.DalFactory;

    public sealed class CouponItem
    {
        private static readonly ICouponItem dal = DataAccess.CreateCouponItem();

        private CouponItem()
        {
        }

        public static bool Add(CouponItemInfo couponItemInfo)
        {
            return dal.Add(couponItemInfo);
        }

        public static bool AddUseTimes(string couponNum, int userId)
        {
            return dal.AddUseTimes(couponNum, userId);
        }

        public static bool Delete(string couponId)
        {
            if (!DataValidator.IsValidId(couponId))
            {
                return false;
            }
            return dal.Delete(couponId);
        }

        public static CouponItemInfo GetCouponItemInfo(string couponNum, int userId)
        {
            return dal.GetCouponItemInfo(couponNum, userId);
        }
    }
}

