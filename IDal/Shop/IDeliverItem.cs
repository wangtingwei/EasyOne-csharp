namespace EasyOne.IDal.Shop
{
    using EasyOne.Model.Shop;
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public interface IDeliverItem
    {
        bool Add(DeliverItemInfo deliverItemInfo);
        DeliverItemInfo GetDeliverItemById(int deliverItemId);
        DeliverItemInfo GetDeliverItemByOrderId(int orderId);
        DeliverItemInfo GetDeliverItemByOrderId(int orderId, int deliverDirection);
        ArrayList GetExpressCompannyList();
        IList<DeliverItemInfo> GetList(int startRowIndexId, int maxNumberRows, int searchType, string keyword, int quickSearch);
        int GetTotalOfDeliverItem();
        void UpdateReceive(int orderId);
    }
}

