namespace EasyOne.Shop
{
    using EasyOne.IDal.Shop;
    using EasyOne.Model.Shop;
    using System;
    using System.Collections.Generic;
    using EasyOne.DalFactory;

    public sealed class Courier
    {
        private static readonly ICourier dal = DataAccess.CreateCourier();

        private Courier()
        {
        }

        public static bool Add(CourierInfo courier)
        {
            return dal.Add(courier);
        }

        public static bool CourierIdExists(int courierId)
        {
            return dal.CourierIdExists(courierId);
        }

        public static bool Delete(int courierId)
        {
            return dal.Delete(courierId);
        }

        public static CourierInfo GetCourier(int courierId)
        {
            return dal.GetCourier(courierId);
        }

        public static IList<CourierInfo> GetCourierList()
        {
            return dal.GetCourierList();
        }

        public static bool Update(CourierInfo courier)
        {
            return dal.Update(courier);
        }
    }
}

