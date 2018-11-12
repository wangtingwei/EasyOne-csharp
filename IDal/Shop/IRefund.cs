namespace EasyOne.IDal.Shop
{
    using EasyOne.Model.Shop;
    using System;

    public interface IRefund
    {
        bool Add(RefundInfo refundInfo);
        RefundInfo GetByOrderId(int orderId);
    }
}

