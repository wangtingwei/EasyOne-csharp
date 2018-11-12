namespace EasyOne.IDal.Shop
{
    using EasyOne.Model.Shop;
    using System;
    using System.Collections.Generic;

    public interface ICourier
    {
        bool Add(CourierInfo courier);
        bool CourierIdExists(int courierId);
        bool Delete(int courierId);
        CourierInfo GetCourier(int courierId);
        IList<CourierInfo> GetCourierList();
        bool Update(CourierInfo courier);
    }
}

