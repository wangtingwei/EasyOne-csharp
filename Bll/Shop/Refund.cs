namespace EasyOne.Shop
{
    using EasyOne.IDal.Shop;
    using EasyOne.Model.Shop;
    using System;
    using EasyOne.DalFactory;

    public sealed class Refund
    {
        private static readonly IRefund dal = DataAccess.CreateRefund();

        private Refund()
        {
        }

        public static bool Add(RefundInfo refundInfo)
        {
            return dal.Add(refundInfo);
        }

        public static RefundInfo GetByOrderId(int orderId)
        {
            return dal.GetByOrderId(orderId);
        }
    }
}

