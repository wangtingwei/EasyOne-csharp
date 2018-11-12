namespace EasyOne.Shop
{
    using EasyOne.IDal.Shop;
    using EasyOne.Model.Shop;
    using System;
    using EasyOne.DalFactory;

    public sealed class Remittance
    {
        private static readonly IRemittance dal = DataAccess.CreateRemittance();

        private Remittance()
        {
        }

        public static bool Add(RemittanceInfo remittanceInfo)
        {
            return dal.Add(remittanceInfo);
        }

        public static RemittanceInfo GetByOrderId(int orderId)
        {
            return dal.GetByOrderId(orderId);
        }
    }
}

