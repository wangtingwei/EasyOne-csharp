namespace EasyOne.IDal.Shop
{
    using EasyOne.Model.Shop;
    using System;
    using System.Collections.Generic;

    public interface IOrderItem
    {
        bool Add(OrderItemInfo orderItemInfo);
        bool Delete(int orderId);
        bool ExistsPresent(int presentId);
        bool ExistsProduct(string tableName);
        bool ExistsProduct(string tableName, int productId);
        OrderItemInfo GetInfoByItemId(int itemId);
        IList<OrderItemInfo> GetInfoListByOrderId(int orderId);
        int GetMaxId();
        IList<string> GetProductNameList(int orderId);
        bool Update(OrderItemInfo orderItemInfo);
    }
}

