namespace EasyOne.IDal.Shop
{
    using EasyOne.Model.Shop;
    using System;
    using System.Collections.Generic;

    public interface ICoupon
    {
        bool Add(CouponInfo couponInfo);
        bool Delete(string couponId);
        IList<CouponDetailInfo> GetAllDetailList(int startRowIndexId, int maxNumberRows, int searchType, string keyword);
        int GetAllTotalOfCoupon();
        CouponInfo GetCouponInfoById(int couponId);
        IList<CouponDetailInfo> GetDetailList(int startRowIndexId, int maxNumberRows, int userId);
        IList<CouponInfo> GetList();
        IList<CouponInfo> GetList(int startRowIndexId, int maxNumberRows);
        int GetTotalOfCoupon();
        bool SetState(int couponId, int state);
        bool Update(CouponInfo couponInfo);
    }
}

