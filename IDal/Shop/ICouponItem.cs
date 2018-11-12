namespace EasyOne.IDal.Shop
{
    using EasyOne.Model.Shop;
    using System;

    public interface ICouponItem
    {
        bool Add(CouponItemInfo couponItemInfo);
        bool AddUseTimes(string couponNum, int userId);
        bool Delete(string couponId);
        CouponItemInfo GetCouponItemInfo(string couponNum, int userId);
    }
}

